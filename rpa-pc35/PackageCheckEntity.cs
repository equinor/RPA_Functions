using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace rpa_functions.rpa_pc35
{
    public static class PackageCheckConstants
    {
        public const string STATUS_FIELD_NAME = "Status";
        public const string ID_FIELD_NAME = "RowKey";
        public const int STATUS_PENDING = 0;
        public const string PARTION_KEY = "PC35";

    }
    public class PackageCheckEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PackageId { get; set; }
        public string InstCode { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime ProcessedTime { get; set; } = DateTime.Now;
        public string LCI { get; set; }
        public string SAP { get; set; }
        public string ProcoSys { get; set; }
        public int RunType { get; set; }
        public int Status { get; set; } = 0;
    }

    public class PackageCheckTableEntity : TableEntity
    {
        public string PackageId { get; set; }
        public string InstCode { get; set; }
        public DateTime EntryTime { get; set; } 
        public DateTime ProcessedTime { get; set; }
        public string LCI { get; set; }
        public string SAP { get; set; }
        public string ProcoSys { get; set; }
        public int RunType { get; set; }
        public int Status { get; set; } 
    }

    public static class Validate
    {

    }

    public static class Mappings
    {
        public static PackageCheckTableEntity ToTableEntity(this PackageCheckEntity packageCheckEntry, string partitionKey)
        {
            return new PackageCheckTableEntity()
            {
                PartitionKey = partitionKey,
                RowKey = packageCheckEntry.Id,
                PackageId = packageCheckEntry.PackageId,
                InstCode = packageCheckEntry.InstCode,
                EntryTime = packageCheckEntry.EntryTime,
                ProcessedTime = packageCheckEntry.ProcessedTime,
                LCI = packageCheckEntry.LCI,
                SAP = packageCheckEntry.SAP,
                ProcoSys = packageCheckEntry.ProcoSys,
                RunType = packageCheckEntry.RunType,
                Status = packageCheckEntry.Status
            };
        }

       public static PackageCheckEntity ToPackageCheckEntity(this PackageCheckTableEntity packageCheckTableEntity)
        {
            return new PackageCheckEntity()
            {
                Id = packageCheckTableEntity.RowKey,
                PackageId = packageCheckTableEntity.PackageId,
                InstCode = packageCheckTableEntity.InstCode,
                EntryTime = packageCheckTableEntity.EntryTime,
                ProcessedTime = packageCheckTableEntity.ProcessedTime,
                LCI = packageCheckTableEntity.LCI,
                SAP = packageCheckTableEntity.SAP,
                ProcoSys = packageCheckTableEntity.ProcoSys,
                RunType = packageCheckTableEntity.RunType,
                Status = packageCheckTableEntity.Status
            };
        }

       public static PackageCheckEntity PopulatePackageCheckEntity(dynamic bodyData)
        {
            return new PackageCheckEntity()
            {
                Id = bodyData.Id,
                ProcessedTime = DateTime.Parse(Convert.ToString(bodyData.ProcessedTime)),
                LCI = bodyData.LCI,
                SAP = bodyData.SAP,
                ProcoSys = bodyData.ProcoSys,
                Status = bodyData.Status
            };
        }

       public static List<PackageCheckEntity> InsertPackageCheck(dynamic bodyData)
        {
            List<PackageCheckEntity> Packages = new List<PackageCheckEntity>();

            foreach (var package in bodyData)
            {
                PackageCheckEntity tmpPack = new PackageCheckEntity();
                tmpPack.PackageId = package.PackageId;
                tmpPack.RunType = convertToInt(Convert.ToString(package.RunType));
                tmpPack.InstCode = package.InstCode;
                Packages.Add(tmpPack);
            }

            return Packages;

        }

        private static int convertToInt(string strNumber)
        {
            int strNumb;

            if (Int32.TryParse(strNumber, out strNumb)) return strNumb;

            return 0; // default return value if missing or invalid type
        }

        public static PackageCheckTableEntity updatePackageCheck(PackageCheckTableEntity package, dynamic bodyData)
        {
            //PackageCheckEntity tmpPack = new PackageCheckEntity();

            // Define and handle fields..
            package.ProcessedTime = DateTime.Parse(Convert.ToString(bodyData.ProcessedTime));
            package.LCI = bodyData.LCI;
            package.SAP = bodyData.SAP;
            package.ProcoSys = bodyData.ProcoSys;
            package.Status = bodyData.Status;

            return package;
        }

       public static string ToPackageCheckEntityJSON(List<PackageCheckTableEntity> packageCheckTableEntities)
        {
            var tmpPackageTableEntities = new List<PackageCheckEntity>();

            foreach (PackageCheckTableEntity ent in packageCheckTableEntities)
            {
                tmpPackageTableEntities.Add(ToPackageCheckEntity(ent));
            }

            return JsonConvert.SerializeObject(tmpPackageTableEntities);
        }

        

    }
}
