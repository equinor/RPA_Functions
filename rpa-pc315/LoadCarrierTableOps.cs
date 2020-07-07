using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services;

namespace rpa_functions.rpa_pc315
{
    class LoadCarrierTableOps
    {
        string tableName = Environment.GetEnvironmentVariable("PC315_TABLENAME");
        CommonTable table;

        public LoadCarrierTableOps()
        {

            this.table = new CommonTable();
        }

        public async Task<TableResult> insertItem(LoadCarrierEntity loadCarrier)
        {

            TableResult tr = await table.InsertorReplace(Mappings.ToTableEntity(loadCarrier), tableName);
            return tr;
  
        }

    }
}
