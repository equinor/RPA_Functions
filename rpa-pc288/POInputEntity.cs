using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc288
{
    public static class POInputEntity
    {
    }

    public class PurchaseOrderEntity
    {
        public string UId { get; set; } = Guid.NewGuid().ToString(); //Rowkey
        public string RFX { get; set; } // PartionKey
        public int Item { get; set; }
        public int MaterialNo { get; set; }
        public string Material { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public int RequiredQty { get; set; }
        public string Unit { get; set; }
        public int QuotedQty { get; set; }
        public int UnitOffered { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Per { get; set; }
        public int TotalItem { get; set; }
        public DateTime? ReqDeliveryDate { get; set; }
        public DateTime? QuotedDeliveryDate { get; set; }
        public Boolean IdenticalProduct { get; set; }
        public string IdenticalComment { get; set; }
        public string ItemBidderRemark { get; set; }
        public string Clarification { get; set; }
        public Boolean AcceptedTerms { get; set; }
        public string TermsComment { get; set; }
        public double FairPrices { get; set; }
        public string FairPricesCurrency { get; set; }
        public DateTime? MarkedPriceDate { get; set; }
        public string RFXName { get; set; }
        public DateTime? SubmissionDeadline { get; set; }
        public string Communication { get; set; }
        public string HeaderNote { get; set; }
        public string RespPurchaser { get; set; }
        public int Bidders { get; set; }
        public int Responses { get; set; }
        public int DeniedBids { get; set; }
        public int Reminders { get; set; }
        public DateTime? LastReminder { get; set; }
        public string MailStatus { get; set; }
        public string Status { get; set; }
        public Boolean ClarificationNeeded { get; set; }
        public string PurchasingGroup { get; set; }
        public string PurchasingOrg { get; set; }
        public string InternalCommunication { get; set; }
        public DateTime? EmailSendTime { get; set; }
        public string BiddersEmail { get; set; }


    }

    public class PurchaseOrderTableEntity : TableEntity
    {
        public string UId { get; set; } // RowKey
        public string RFX { get; set; } // PartionKey
        public int Item { get; set; }
        public int MaterialNo { get; set; }
        public string Material { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public int RequiredQty { get; set; }
        public string Unit { get; set; }
        public int QuotedQty { get; set; }
        public int UnitOffered { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Per { get; set; }
        public int TotalItem { get; set; }
        public DateTime? ReqDeliveryDate { get; set; }
        public DateTime? QuotedDeliveryDate { get; set; }
        public Boolean IdenticalProduct { get; set; }
        public string IdenticalComment { get; set; }
        public string ItemBidderRemark { get; set; }
        public string Clarification { get; set; }
        public Boolean AcceptedTerms { get; set; }
        public string TermsComment { get; set; }
        public double FairPrices { get; set; }
        public string FairPricesCurrency { get; set; }
        public DateTime? MarkedPriceDate { get; set; }
        public string RFXName { get; set; }
        public DateTime? SubmissionDeadline { get; set; }
        public string Communication { get; set; }
        public string HeaderNote { get; set; }
        public string RespPurchaser { get; set; }
        public int Bidders { get; set; }
        public int Responses { get; set; }
        public int DeniedBids { get; set; }
        public int Reminders { get; set; }
        public DateTime? LastReminder { get; set; }
        public string MailStatus { get; set; }
        public string Status { get; set; }
        public Boolean ClarificationNeeded { get; set; }
        public string PurchasingGroup { get; set; }
        public string PurchasingOrg { get; set; }
        public string InternalCommunication { get; set; }
        public DateTime? EmailSendTime { get; set; }
        public string BiddersEmail { get; set; }


    }

    public static class Mapping
    {

        private static int convertToInt(dynamic strInt)
        {
            int outputInt;

            if (Int32.TryParse(Convert.ToString(strInt), out outputInt)) return outputInt;

            return 0; // default return value if missing or invalid type
        }

        private static decimal convertToDecimal(dynamic strDecimal)
        {
            Decimal outputDecimal;

            if (Decimal.TryParse(Convert.ToString(strDecimal), out outputDecimal)) return outputDecimal;

            return 0; // default return value if missing or invalid type

        }

        private static DateTime? convertToDate(dynamic strDate)
        {

            DateTime outputDate;

            if (DateTime.TryParse(Convert.ToString(strDate), out outputDate)) return outputDate;

            return null; // default return value if missing or invalid type


        }
    }

}
