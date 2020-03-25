using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc239
{
     public class ReturnForCreditEntityConstants
    {
        public const string ID_FIELD_NAME = "RowKey";
        public const string WEBID_FIELD_NAME = "PartitionKey";

        public const string STATUS_FIELD_NAME = "status";
        public const int STATUS_WAITING = 0;
        public const int STATUS_DONE = 1;
        public const string RETURNS_NUMBER_FIELD_NAME = "ReturnsNumber";
        public const string PARTITION_KEY_VALUE = "PC0239";
    }
    public class ReturnForCreditEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string webguid { get; set; }
        public string ReturnsNumber { get; set; }
        public string POResponsibleName { get; set; }
        public string PurchaseDocument { get; set; }
        public string Item { get; set; }
        public string Text { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
        public string CreditAmount { get; set; }
        public string ContactPerson { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string RITM { get; set; }
        public bool ReturnApproved { get; set; }
        public bool ReturnRejected { get; set; }
        public string AddressPostCode { get; set; }
        public string Comments { get; set; }
        public string ReasonForReturn { get; set; }
        public string ConditionOfGoods { get; set; }
        public string TransportationCoveredBy { get; set; }
        public int Status { get; set; } = ReturnForCreditEntityConstants.STATUS_WAITING;
    }
    public class ReturnForCreditEntityTableEntity : TableEntity
    {
        public string ReturnsNumber { get; set; }
        public string POResponsibleName { get; set; }
        public string PurchaseDocument { get; set; }
        public string Item { get; set; }
        public string Text { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
        public string CreditAmount { get; set; }
        public string ContactPerson { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string RITM { get; set; }
        public bool ReturnApproved { get; set; }
        public bool ReturnRejected { get; set; }
        public string AddressPostCode { get; set; }
        public string Comments{ get; set; }
        public string ReasonForReturn { get; set; }
        public string ConditionOfGoods { get; set; }
        public string TransportationCoveredBy { get; set; }
        public int Status { get; set; }
    }
    public static class Mappings
    {
        public static ReturnForCreditEntityTableEntity ToReturnForCreditEntityTableEntity(this ReturnForCreditEntity returnForCreditEntry)
        {
            return new ReturnForCreditEntityTableEntity()
            {
                PartitionKey = returnForCreditEntry.webguid,
                RowKey = returnForCreditEntry.id,
                ReturnsNumber = returnForCreditEntry.ReturnsNumber,
                AddressCity = returnForCreditEntry.AddressCity,
                AddressPostCode = returnForCreditEntry.AddressPostCode,
                AddressStreet = returnForCreditEntry.AddressStreet,
                Comments = returnForCreditEntry.Comments,
                ConditionOfGoods = returnForCreditEntry.ConditionOfGoods,
                ContactPerson = returnForCreditEntry.ContactPerson,
                CreditAmount = returnForCreditEntry.CreditAmount,
                Item = returnForCreditEntry.Item,
                RITM = returnForCreditEntry.RITM,
                ReturnApproved = returnForCreditEntry.ReturnApproved,
                ReturnRejected = returnForCreditEntry.ReturnRejected,
                POResponsibleName = returnForCreditEntry.POResponsibleName,
                PurchaseDocument = returnForCreditEntry.PurchaseDocument,
                Quantity = returnForCreditEntry.Quantity,
                ReasonForReturn = returnForCreditEntry.ReasonForReturn,
                Status = returnForCreditEntry.Status,
                Text = returnForCreditEntry.Text,
                Total = returnForCreditEntry.Total,
                TransportationCoveredBy = returnForCreditEntry.TransportationCoveredBy
            };
        }
        public static ReturnForCreditEntity ToReturnForCreditEntity(this ReturnForCreditEntityTableEntity returnGoodsTableEntity)
        {
            return new ReturnForCreditEntity()
            {
                webguid = returnGoodsTableEntity.PartitionKey,
                id = returnGoodsTableEntity.RowKey,
                AddressCity = returnGoodsTableEntity.AddressCity,
                AddressPostCode = returnGoodsTableEntity.AddressPostCode,
                AddressStreet = returnGoodsTableEntity.AddressStreet,
                Comments = returnGoodsTableEntity.Comments,
                ConditionOfGoods = returnGoodsTableEntity.ConditionOfGoods,
                ContactPerson = returnGoodsTableEntity.ContactPerson,
                CreditAmount = returnGoodsTableEntity.CreditAmount,
                Item = returnGoodsTableEntity.Item,
                RITM = returnGoodsTableEntity.RITM,
                ReturnApproved = returnGoodsTableEntity.ReturnApproved,
                ReturnRejected = returnGoodsTableEntity.ReturnRejected,
                POResponsibleName = returnGoodsTableEntity.POResponsibleName,
                PurchaseDocument = returnGoodsTableEntity.PurchaseDocument,
                Quantity = returnGoodsTableEntity.Quantity,
                ReasonForReturn = returnGoodsTableEntity.ReasonForReturn,
                ReturnsNumber = returnGoodsTableEntity.ReturnsNumber,
                Status = returnGoodsTableEntity.Status,
                Text = returnGoodsTableEntity.Text,
                Total = returnGoodsTableEntity.Total,
                TransportationCoveredBy = returnGoodsTableEntity.TransportationCoveredBy
            };
        }
        private static ReturnForCreditEntity castInsertReturnForCreditFromBodyData(dynamic bodyData)
        {
            ReturnForCreditEntity returnEntity = new ReturnForCreditEntity();

            returnEntity.ReturnsNumber = bodyData.ReturnsNumber;
            returnEntity.POResponsibleName = bodyData.POResponsibleName;
            returnEntity.PurchaseDocument = bodyData.PurchaseDocument;
            returnEntity.Item = bodyData.Item;
            returnEntity.Text = bodyData.Text;
            returnEntity.Quantity = bodyData.Quantity;
            returnEntity.RITM = bodyData.RITM;
            returnEntity.Total = bodyData.Total;
            returnEntity.Status = ReturnForCreditEntityConstants.STATUS_WAITING;

            return returnEntity;
        }
        public static List<ReturnForCreditEntity> ToReturnForCreditEntityList(dynamic bodyData)
        {
            List<ReturnForCreditEntity> tmpReturnForCreditEntityList = new List<ReturnForCreditEntity>();

            string newgGuid;

            foreach(var ent in bodyData)
            {
                newgGuid = Guid.NewGuid().ToString(); 
                ReturnForCreditEntity tmpEnt = castInsertReturnForCreditFromBodyData(ent);
                tmpEnt.id = newgGuid;
                tmpEnt.webguid = ReturnForCreditEntityConstants.PARTITION_KEY_VALUE;
                tmpReturnForCreditEntityList.Add(tmpEnt);
            }
            return tmpReturnForCreditEntityList;
        }
        public static List<ReturnForCreditEntityTableEntity> ToReturnForCreditEntityTableEntityList(List<ReturnForCreditEntity> returnForCreditEntityList)
        {
            var tmpReturnForCreditEntityTableEntityList = new List<ReturnForCreditEntityTableEntity>();

            foreach(var ent in returnForCreditEntityList)
            {
                tmpReturnForCreditEntityTableEntityList.Add(ToReturnForCreditEntityTableEntity(ent));
            }

            return tmpReturnForCreditEntityTableEntityList;
        }
        public static ReturnForCreditEntityTableEntity updateReturnForCreditEntityTableEntity(ReturnForCreditEntityTableEntity returnForCredit, dynamic bodyData)
        {
            returnForCredit.CreditAmount = bodyData.CreditAmount;
            returnForCredit.ContactPerson = bodyData.ContactPerson;
            returnForCredit.AddressStreet = bodyData.AddressStreet;
            returnForCredit.AddressPostCode = bodyData.AddressPostCode;
            returnForCredit.AddressCity = bodyData.AddressCity;
            returnForCredit.Comments = bodyData.Comments;
            //define as done
            returnForCredit.Status = ReturnForCreditEntityConstants.STATUS_DONE;
            return returnForCredit;
        }
    }
}
