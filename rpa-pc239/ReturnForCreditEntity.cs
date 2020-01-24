using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc239
{

    public class ReturnForCreditEntityConstants
    {
        // Set your constants
    }

    public class ReturnForCreditEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string vendor_number { get; set; }
    }

    public class ReturnForCreditEntityTableEntity : TableEntity
    {
        public string vendor_number { get; set; }
    }
}
