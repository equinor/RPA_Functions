﻿using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace rpa_functions.rpa_pc315
{
    public static class LoadCarrierConstants
    {
        const string UID_FIELD_NAME = "RowKey";
        const string CONTAINER_ID_FIELD_NAME = "PartitionKey";

    }

    public class LoadCarrierEntity
    {
        public int LCyear { get; set; }
        public int LCmonth { get; set; }
        public DateTime LoggedTime { get; set; } = DateTime.Now;
        public string UId { get; set; } = Guid.NewGuid().ToString();
        public string Location { get; set; }
        public int Plant { get; set; }
        public string ContainerId { get; set; }
        public int LoadInDays { get; set; }
        public string WBS { get; set; }
    }

    public class LoadCarrierTableEntity : TableEntity
    {
        public int LCyear { get; set; }
        public int LCmonth { get; set; }
        public DateTime LoggedTime { get; set; }
        //public Guid UId { get; set; } 
        public string Location { get; set; }
        public int Plant { get; set; }
        //public string ContainerId { get; set; }
        public int LoadInDays { get; set; }
        public string WBS { get; set; }
    }

    public static class Mappings
    {
        public static LoadCarrierTableEntity ToTableEntity(this LoadCarrierEntity loadCarrierEntry)
        {
            return new LoadCarrierTableEntity()
            {
                PartitionKey = loadCarrierEntry.ContainerId, // ContainerID
                RowKey = loadCarrierEntry.UId,  // UID
                LCyear = loadCarrierEntry.LCyear,
                LCmonth = loadCarrierEntry.LCmonth,
                LoggedTime = loadCarrierEntry.LoggedTime,
                Location = loadCarrierEntry.Location,
                Plant = loadCarrierEntry.Plant,
                LoadInDays = loadCarrierEntry.LoadInDays,
                WBS = loadCarrierEntry.WBS
            };
        }

        public static LoadCarrierEntity ToEntity(this LoadCarrierTableEntity loadCarrierTableEntry)
        {
            return new LoadCarrierEntity()
            {
                UId = loadCarrierTableEntry.RowKey,
                ContainerId = loadCarrierTableEntry.PartitionKey,
                LCyear = loadCarrierTableEntry.LCyear,
                LCmonth = loadCarrierTableEntry.LCmonth,
                LoggedTime = loadCarrierTableEntry.LoggedTime,
                Location = loadCarrierTableEntry.Location,
                Plant = loadCarrierTableEntry.Plant,
                LoadInDays = loadCarrierTableEntry.LoadInDays,
                WBS = loadCarrierTableEntry.WBS

            };
        }

        public static LoadCarrierEntity dynamicToloadCarrierEntity(dynamic bodyData)
        {
            return new LoadCarrierEntity()
            {
                ContainerId = bodyData.ContainerId,
                LCyear = bodyData.LCyear,
                LCmonth = bodyData.LCmonth,
                Location = bodyData.Location,
                Plant = bodyData.Plant,
                LoadInDays = bodyData.LoadInDays,
                WBS = bodyData.WBS
            };
        }


    }
}
