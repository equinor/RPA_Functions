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

        public async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnWebid(string webid, bool robotQuery)
        {
            if (!robotQuery)
            {
                string guidFilter = TableQuery.GenerateFilterCondition(ReturnForCreditEntityConstants.WEBID_FIELD_NAME, QueryComparisons.Equal, webid);
                string statusFilter = TableQuery.GenerateFilterConditionForInt(ReturnForCreditEntityConstants.STATUS_FIELD_NAME, QueryComparisons.Equal, ReturnForCreditEntityConstants.STATUS_WAITING_PO_RESPONSE);

                List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntitiesCombinedFilter<ReturnForCreditEntityTableEntity>(guidFilter, TableOperators.And, statusFilter, tableName);

                return queryResult;
            } else if (robotQuery)
            {
                List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.WEBID_FIELD_NAME, QueryComparisons.Equal, webid, tableName);
                return queryResult;
            }
            return null;
        }

        private async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnReturnsNumber(string returnsNumber)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.RETURNS_NUMBER_FIELD_NAME, QueryComparisons.Equal, returnsNumber, tableName);
            return queryResult;
        }

        public async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnGuid(string guid)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.ID_FIELD_NAME, QueryComparisons.Equal, guid, tableName);
            queryResult = await QueryReturnForCreditOnReturnsNumber(queryResult[0].returns_number);
            return queryResult;
        }

        private async Task<List<ReturnForCreditEntityTableEntity>> QueryReturnForCreditOnStatus(int status)
        {
            List<ReturnForCreditEntityTableEntity> queryResult = await table.RetrieveEntities<ReturnForCreditEntityTableEntity>(ReturnForCreditEntityConstants.STATUS_FIELD_NAME, QueryComparisons.Equal, status, tableName);
            return queryResult;   
        }

        public async Task<Object> UpdateReturnForCredit(string guid, dynamic bodyData)
        {
            List<ReturnForCreditEntityTableEntity> result = await QueryReturnForCreditOnGuid(guid);

            if (result.Count == 1)
            {
                ReturnForCreditEntityTableEntity updatedReturnForCredit = Mappings.updateReturnForCreditEntityTableEntity(result[0], bodyData);
                TableResult tr = await table.InsertorReplace(updatedReturnForCredit, tableName);

                return tr.Result;
            }
            else
            {
                return null;
            }
        }
    }
}
