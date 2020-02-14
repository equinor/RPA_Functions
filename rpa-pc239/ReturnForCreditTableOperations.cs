using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System.Text;

namespace rpa_functions.rpa_pc239
{
    internal class ReturnForCreditTableOperations
    {
        private string tableName = Environment.GetEnvironmentVariable("PC239_TABLENAME");
        private CommonTable table;

        public ReturnForCreditTableOperations()
        {
            this.table = new CommonTable();
        }
        private async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnReturnsNumber(string returnsNumber)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.RETURNS_NUMBER_FIELD_NAME, QueryComparisons.Equal, returnsNumber, tableName);
            return queryResult;
        }
        public async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnGuid(string guid)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.ID_FIELD_NAME, QueryComparisons.Equal, guid, tableName);
            queryResult = await QueryReturnForCreditOnReturnsNumber(queryResult[0].ReturnsNumber);
            return queryResult;
        }
        private async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnStatus(int status)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.STATUS_FIELD_NAME, QueryComparisons.Equal, status, tableName);
            return queryResult;   
        }
        public async Task<List<string>> InsertBatch(dynamic bodyData)
        {
            List<ReturnForCreditEntity> returnForCreditEntities;
            List<string> returnCodes = new List<string>();

            string returnCode = "";
            try
            {
               returnForCreditEntities  = Mappings.ToReturnForCreditEntityList(bodyData);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                return null;
            }
            foreach (ReturnForCreditEntity element in returnForCreditEntities)
            {
                try
                {
                    TableResult tr = await table.InsertorReplace(Mappings.ToReturnForCreditEntityTableEntity(element), tableName).ConfigureAwait(false);

                    returnCodes.Add(element.id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in writing to table storage: " + ex.ToString());
                    return null;
                }
            }
            return returnCodes;
        }
        public async Task<string> RemoveTableEntityOnGuid(string guid)
        {
            TableResult tr = null;
            List<ReturnForCreditEntityTableEntity> entities = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.ID_FIELD_NAME, QueryComparisons.Equal, guid, tableName);
            foreach(var ent in entities)
            {
                tr = await table.Remove(ent, tableName);
                
            }
            if (tr is null)
            {
                return "Entity not found";
            }
            else
            {
                return "Entity deleted";
            } 
        }
    }
}
