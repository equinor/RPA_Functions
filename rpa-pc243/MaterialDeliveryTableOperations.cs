using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services;

namespace rpa_functions.rpa_pc243
{
    internal class MaterialDeliveryTableOperations
    {
        private string tableName = Environment.GetEnvironmentVariable("PC243_TABLENAME");
        private CommonTable table;

        public MaterialDeliveryTableOperations()
        {
            this.table = new CommonTable();
        }

        public async Task<string> InsertBatch(dynamic bodyData)
        {
            List<MaterialDeliveryEntity> materialDeliveries;

            string returnCode = "";
            try
            {
                materialDeliveries = Mappings.getMaterialDeliveryEntities(bodyData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when converting HTTP data to Table format: " + ex.ToString());
                return null;
            }

            foreach (MaterialDeliveryEntity element in materialDeliveries)
            {
                try
                {
                    TableResult tr = await table.InsertorReplace(Mappings.ToMaterialDeliveryTableEntity(element), tableName).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in writing to table storage: " + ex.ToString());
                    return null;
                }
            }

            return materialDeliveries[0].webguid;
        }



        private async Task<List<MaterialDeliveryTableEntity>> QueryMaterialDeliveryOnStatus(int status) 
        {
            List<MaterialDeliveryTableEntity> queryResult = await table.RetrieveEntities<MaterialDeliveryTableEntity>(MaterialDeliveryConstants.STATUS_FIELD_NAME, QueryComparisons.Equal, status, tableName);
            
            return queryResult;

        }

        public async Task<List<MaterialDeliveryTableEntity>> QueryMaterialDeliveryOnStatusAndComplete()
        {
            List<MaterialDeliveryTableEntity> queryResult = QueryMaterialDeliveryOnStatus(MaterialDeliveryConstants.STATUS_DONE).Result;

            foreach(MaterialDeliveryTableEntity element in queryResult)
            {
                element.status = MaterialDeliveryConstants.STATUS_FETCHED;

                try
                {
                    TableResult tr = await table.InsertorReplace(element, tableName);


                } catch (Exception ex)
                {
                    Console.WriteLine("Error in writing to table storage "+ ex.ToString());
                }

            }


            return queryResult;
        }

        public async Task<List<MaterialDeliveryTableEntity>> QueryMaterialDeliveryOnStatusAndRemove() 
        {
            List<MaterialDeliveryTableEntity> queryResult = QueryMaterialDeliveryOnStatus(MaterialDeliveryConstants.STATUS_WAITING).Result;
             
            foreach (MaterialDeliveryTableEntity element in queryResult)
            {
                TimeSpan ts = DateTime.Now - element.Timestamp;
                if ( ts.Days >= 14)
                {
                    try
                    {
                        
                        TableResult trRm = await table.Remove(element, tableName);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error in writing to table storage " + ex.ToString());
                    }                  
                }                
            }
            List<MaterialDeliveryTableEntity> queryResult2 = QueryMaterialDeliveryOnStatus(MaterialDeliveryConstants.STATUS_FETCHED).Result;

            foreach (MaterialDeliveryTableEntity element in queryResult2)
            {
                TimeSpan ts = DateTime.Now - element.Timestamp;
                if (ts.Days >= 14)
                {
                    try
                    {
                        
                        TableResult trRm = await table.Remove(element, tableName);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error in writing to table storage " + ex.ToString());
                    }
                }
            }
            return null;
        }
       
    }
}