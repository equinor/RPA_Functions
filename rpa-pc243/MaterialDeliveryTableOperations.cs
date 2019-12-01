using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            return returnCode;
        }


        public async Task<List<MaterialDeliveryTableEntity>> QueryMaterialDeliveryOnWebid(string webid)
        {

            List<MaterialDeliveryTableEntity> queryResult = await table.RetrieveEntities<MaterialDeliveryTableEntity>(MaterialDeliveryConstants.WEBID_FIELD_NAME,
                                                                                     QueryComparisons.Equal,
                                                                                     webid, tableName);

            // Add logic to add filter on status

            return queryResult;
        }
        public async Task<List<MaterialDeliveryTableEntity>> QueryMaterialDeliveryOnGuid(string guid)
        {

            List<MaterialDeliveryTableEntity> queryResult = await table.RetrieveEntities<MaterialDeliveryTableEntity>(MaterialDeliveryConstants.ID_FIELD_NAME,
                                                                                     QueryComparisons.Equal,
                                                                                     guid, tableName);



            return queryResult;
        }


        
        public  async Task<Object> UpdateMaterialDelivery(string guid, dynamic bodyData)
        {

            // Find package, if found - update else fail.
            List<MaterialDeliveryTableEntity> result = await QueryMaterialDeliveryOnGuid(guid);

            if (result.Count == 1)
            {
                MaterialDeliveryTableEntity updatedMaterialDelivery = Mappings.updateMaterialDelivery(result[0], bodyData);
                TableResult tr = await table.InsertorReplace(updatedMaterialDelivery, tableName);

                return tr.Result;
            
            }
            else
            {
                return null;
            }

        }
        

    }
}