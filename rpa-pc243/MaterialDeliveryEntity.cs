using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace rpa_functions.rpa_pc243
{
    
    public static class MaterialDeliveryConstants
    {
        public const string ID_FIELD_NAME = "RowKey";
        public const string WEBID_FIELD_NAME = "PartitionKey";

        public const string STATUS_FIELD_NAME = "status";
        public const int STATUS_WAITING = 0;
        public const int STATUS_DONE = 1;
        public const int STATUS_FETCHED = 2;
        public const int STATUS_EXPIRED = 3;

    }

    public class MaterialDeliveryEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string webguid { get; set; }
        public string vendor_number { get; set; }
        public string vendor_name { get; set; }
        public string po { get; set; }
        public string item { get; set; }
        public string material { get; set; }
        public string shorttext { get; set; }
        public string order_qty { get; set; }
        public string order_unit { get; set; }
        public DateTime delivery_date { get; set; } 
        public string delivered_ondate { get; set; }
        public DateTime? new_delivery_date { get; set; }
        public string tracking_nr { get; set; }
        public string freight_name { get; set; }
        public int status { get; set; } = MaterialDeliveryConstants.STATUS_WAITING;
    }

    public class MaterialDeliveryTableEntity : TableEntity
    {
        public string vendor_number { get; set; }
        public string vendor_name { get; set; }
        public string po { get; set; }
        public string item { get; set; }
        public string material { get; set; }
        public string shorttext { get; set; }
        public string order_qty { get; set; }
        public string order_unit { get; set; }
        public DateTime delivery_date { get; set; }
        public string delivered_ondate { get; set; }
        public DateTime? new_delivery_date { get; set; }
        public string tracking_nr { get; set; }
        public string freight_name { get; set; }
        public int status { get; set; }
    }

    public static class Mappings
    {
        public static MaterialDeliveryTableEntity ToMaterialDeliveryTableEntity(this MaterialDeliveryEntity materialDeliveryEntry)
        {
            return new MaterialDeliveryTableEntity()
            {
                PartitionKey = materialDeliveryEntry.webguid,
                RowKey = materialDeliveryEntry.id,
                vendor_number = materialDeliveryEntry.vendor_number,
                vendor_name = materialDeliveryEntry.vendor_name,
                po = materialDeliveryEntry.po,
                item = materialDeliveryEntry.item,
                material = materialDeliveryEntry.material,
                shorttext = materialDeliveryEntry.shorttext,
                order_qty = materialDeliveryEntry.order_qty,
                order_unit = materialDeliveryEntry.order_unit,
                delivery_date = materialDeliveryEntry.delivery_date,
                delivered_ondate = materialDeliveryEntry.delivered_ondate,
                new_delivery_date = materialDeliveryEntry.new_delivery_date,
                tracking_nr = materialDeliveryEntry.tracking_nr,
                freight_name = materialDeliveryEntry.freight_name,
                status = materialDeliveryEntry.status
            };
        }

        public static MaterialDeliveryEntity ToMaterialDeliveryEntity(this MaterialDeliveryTableEntity packageCheckTableEntity)
        {
            return new MaterialDeliveryEntity()
            {
                webguid = packageCheckTableEntity.PartitionKey,
                id = packageCheckTableEntity.RowKey,
                vendor_number = packageCheckTableEntity.vendor_number,
                vendor_name = packageCheckTableEntity.vendor_name,
                po = packageCheckTableEntity.po,
                item = packageCheckTableEntity.item,
                material = packageCheckTableEntity.material,
                shorttext = packageCheckTableEntity.shorttext,
                order_qty = packageCheckTableEntity.order_qty,
                order_unit = packageCheckTableEntity.order_unit,
                delivery_date = packageCheckTableEntity.delivery_date,
                delivered_ondate = packageCheckTableEntity.delivered_ondate,
                new_delivery_date = packageCheckTableEntity.new_delivery_date,
                tracking_nr = packageCheckTableEntity.tracking_nr,
                freight_name = packageCheckTableEntity.freight_name,
                status = packageCheckTableEntity.status
            };
        }

        public static List<MaterialDeliveryEntity> toMaterialDeliveryEntityList(List<MaterialDeliveryTableEntity> materialDeliveryTableEntityList)
        {
            var tmpMaterialDeliveryEntities = new List<MaterialDeliveryEntity>();

            foreach (MaterialDeliveryTableEntity ent in materialDeliveryTableEntityList)
            {
                tmpMaterialDeliveryEntities.Add(ToMaterialDeliveryEntity(ent));
            }

            return tmpMaterialDeliveryEntities;
        }

        public static List<MaterialDeliveryEntity> getMaterialDeliveryEntities(dynamic bodyData)
        {
            List<MaterialDeliveryEntity> MaterialDelivieres = new List<MaterialDeliveryEntity>();

            string newWebGuid = Guid.NewGuid().ToString();

            foreach (var element in bodyData)
            {
                MaterialDeliveryEntity tmpEnt = castInsertDeliveryFromBodyData(element);

                tmpEnt.webguid = newWebGuid;

                MaterialDelivieres.Add(tmpEnt);
            }

            return MaterialDelivieres;
        }

        private static MaterialDeliveryEntity castInsertDeliveryFromBodyData(dynamic bodyData)
        {
            MaterialDeliveryEntity returnEntity = new MaterialDeliveryEntity();

            returnEntity.vendor_number = bodyData.vendor_number;
            returnEntity.vendor_name = bodyData.vendor_name;
            returnEntity.po = bodyData.po;
            returnEntity.item = bodyData.item;
            returnEntity.material = bodyData.material;
            returnEntity.shorttext = bodyData.shorttext;
            returnEntity.order_qty = bodyData.order_qty;
            returnEntity.order_unit = bodyData.order_unit;
            returnEntity.delivery_date = ConvertToDate(bodyData.delivery_date);

            return returnEntity;

        }

        // FIX THIS::::
        private static MaterialDeliveryEntity castFromBodyData(dynamic bodyData)
        {
            MaterialDeliveryEntity returnEntity = new MaterialDeliveryEntity();


            returnEntity.delivered_ondate = bodyData.delivered_ondate;
            if (Convert.ToString(bodyData.new_delivery_date) != "") returnEntity.new_delivery_date = DateTime.Parse(Convert.ToString(bodyData.new_delivery_date));
            returnEntity.tracking_nr = bodyData.tracking_nr;
            returnEntity.freight_name = bodyData.freight_name;

            return returnEntity;
        }

        public static MaterialDeliveryTableEntity updateMaterialDelivery(MaterialDeliveryTableEntity materialDelivery, dynamic bodyData)
        {

            materialDelivery.delivered_ondate = bodyData.delivered_ondate;
            materialDelivery.new_delivery_date = ConvertToDate(bodyData.new_delivery_date);
            materialDelivery.tracking_nr = bodyData.tracking_nr;
            materialDelivery.freight_name = bodyData.freight_name;

            // Define as done
            materialDelivery.status = MaterialDeliveryConstants.STATUS_DONE;

            return materialDelivery;
        }

    

        // Convert List of MaterialDeliveryTableEntities to JSON
        public static string toMaterialDeliveryJSON(List<MaterialDeliveryTableEntity> materialDeliveryEntities)
        {
            var tmpMaterialDeliveryEntities = new List<MaterialDeliveryEntity>();

            foreach (MaterialDeliveryTableEntity ent in materialDeliveryEntities)
            {
                //tmpMaterialDeliveryEntities.Add(ToPackageCheckEntity(ent));
            }

            return JsonConvert.SerializeObject(tmpMaterialDeliveryEntities);
        }



        private static DateTime? ConvertToDate(dynamic dateDynamic)
        {
            DateTime outputDate;
            CultureInfo MyCultureInfo = new CultureInfo("no-NO");

            if (DateTime.TryParse(Convert.ToString(dateDynamic), out outputDate)) {

                outputDate = new DateTime(outputDate.Year, outputDate.Month, outputDate.Day, 5,0,0);
                return outputDate;
            }

            return null; // default return value if missing or invalid type

        }




    }
}