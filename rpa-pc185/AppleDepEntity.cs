using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc185
{
    public static class AppleDepConstants
    {
        public const string STATUS_FIELD_NAME = "Status";
        public const string ID_FIELD_NAME = "PartitionKey";

        public const int STATUS_PENDING = 0;
        public const int STATUS_DONE = 1;
        public const int STATUS_ERROR = -1;


    }
    public class AppleDepEntity
    {
        public string Serial { get; set; }
        public string User { get; set; }
        public int Status { get; set; } = 0;
        public DateTime ProcessedTime { get; set; } = DateTime.Now;
    }

    public class AppleDepTableEntity : TableEntity
    {
        public string Serial { get; set; }
        public string User { get; set; }
        public int Status { get; set; }
        public DateTime ProcessedTime { get; set; }
    }

    public static class Mappings
    {
        public static AppleDepTableEntity ToAppleDepTableEntity(this AppleDepEntity appleDepEntry)
        {
            return new AppleDepTableEntity()
            {
                PartitionKey = appleDepEntry.Serial, 
                RowKey = appleDepEntry.User,
                Status = appleDepEntry.Status,
                ProcessedTime = appleDepEntry.ProcessedTime
            };
        }

        public static AppleDepEntity ToAppleDepEntity(this AppleDepTableEntity appleDepTableEntry)
        {
            return new AppleDepEntity()
            {
                User = appleDepTableEntry.RowKey,
                Serial = appleDepTableEntry.PartitionKey,
                Status = appleDepTableEntry.Status,
                ProcessedTime = appleDepTableEntry.ProcessedTime

            };
        }

        public static AppleDepEntity PopulateAppleDepEntity(string serial, string user)
        {
            return new AppleDepEntity()
            {
                User = user,
                Serial = serial
            };
        }

        public static AppleDepTableEntity updateSerialStatus(AppleDepTableEntity updated, string serial, string user, int status)
        {
          
            // Define and handle fields..
            updated.ProcessedTime = DateTime.Now;
            updated.Status = status;

            return updated;
        }


        public static string ToEntityJSON(List<AppleDepTableEntity> appleDeps)
        {
            var outputTables = new List<AppleDepEntity>();

            foreach (AppleDepTableEntity ent in appleDeps)
            {
                outputTables.Add(ToAppleDepEntity(ent));
            }

            return JsonConvert.SerializeObject(outputTables);
        }
    }
}

    