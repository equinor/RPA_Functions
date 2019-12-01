using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rpa_functions.rpa_pc185
{
    class AppleDepTableOperations
    {
        string tableName = Environment.GetEnvironmentVariable("PC185_TABLENAME");
        CommonTable table;

        public AppleDepTableOperations()
        {
            this.table = new CommonTable();
        }


        public async Task<string> InsertSerial(string user, string serial)
        {
            string returCode = "";

            AppleDepEntity serial_entry = Mappings.PopulateAppleDepEntity(serial, user);

            TableResult tr = await table.InsertorReplace(Mappings.ToAppleDepTableEntity(serial_entry), tableName);


            return returCode;
        }

        public async Task<string> QueryPendingSerials()
        {

            List<AppleDepTableEntity> queryResult = await table.RetrieveEntities<AppleDepTableEntity>(AppleDepConstants.STATUS_FIELD_NAME,
                                                                                     QueryComparisons.Equal,
                                                                                     AppleDepConstants.STATUS_PENDING, tableName);

            return (Mappings.ToEntityJSON(queryResult));

        }

        private async Task<List<AppleDepTableEntity>> QuerySerial(string serial)
        {

            List<AppleDepTableEntity> queryResult = await table.RetrieveEntities<AppleDepTableEntity>(AppleDepConstants.ID_FIELD_NAME,
                                                                                     QueryComparisons.Equal,
                                                                                     serial, tableName);

            return queryResult;
        }

        public async Task<Boolean> UpdatePackage(string serial, string user, int status)
        {

            // Find package, if found - update else fail.
            List<AppleDepTableEntity> result = await QuerySerial(serial);

            if (result.Count == 1)
            {
                AppleDepTableEntity updatedPackage = Mappings.updateSerialStatus(result[0], serial, user, status);
                TableResult tr = await table.InsertorReplace(updatedPackage, tableName);

                return true;
            }
            else
            {
                return false;
            }

        }

    }


}


