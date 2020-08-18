using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace rpa_functions.rpa_pc269
{
    class PC269Mappings
    {
        /**
         * DailyProductionTotal
         */
        public static DailyReportsTotal ObjectToDailyReportsTotal(dynamic newDailyReport, int assetId)
        {
            DailyReportsTotal dailyReportTotal = new DailyReportsTotal();

            dailyReportTotal.AssetId = assetId;
            dailyReportTotal.Date = Convert.ToDateTime(newDailyReport.date);
            dailyReportTotal.FacilityName = Convert.ToString(newDailyReport.facility_name);
            dailyReportTotal.OilProdAllocated = convertToDecimal(newDailyReport.oil_prod_allocated);
            dailyReportTotal.OilProdTarget = convertToDecimal(newDailyReport.oil_prod_target);
            dailyReportTotal.OilProdMtd = convertToDecimal(newDailyReport.oil_prod_MTD);
            dailyReportTotal.OilProdYtd = convertToDecimal(newDailyReport.oil_prod_YTD);
            dailyReportTotal.CondensateProdAllocated = convertToDecimal(newDailyReport.condensate_prod_allocated);
            dailyReportTotal.CondensateExportAllocated = convertToDecimal(newDailyReport.condensate_export_allocated);
            dailyReportTotal.CondensateStockInitial = convertToDecimal(newDailyReport.condensate_stock_initial);
            dailyReportTotal.CondensateStockFinal = convertToDecimal(newDailyReport.condensate_stock_final);
            dailyReportTotal.LpgProdAllocated = convertToDecimal(newDailyReport.LPG_prod_allocated);
            dailyReportTotal.LpgExportAllocated = convertToDecimal(newDailyReport.LPG_export_allocated);
            dailyReportTotal.LpgStockInitial = convertToDecimal(newDailyReport.LPG_stock_initial);
            dailyReportTotal.LpgStockFinal = convertToDecimal(newDailyReport.LPG_stock_final);
            dailyReportTotal.LpgSpiking = convertToDecimal(newDailyReport.LPG_spiking);
            dailyReportTotal.GasProdAllocated = convertToDecimal(newDailyReport.gas_prod_allocated);
            dailyReportTotal.GasProdTarget = convertToDecimal(newDailyReport.gas_prod_target);
            dailyReportTotal.GasProdMtd = convertToDecimal(newDailyReport.gas_prod_MTD);
            dailyReportTotal.GasProdYtd = convertToDecimal(newDailyReport.gas_prod_YTD);
            dailyReportTotal.GasExportAllocated = convertToDecimal(newDailyReport.gas_export_allocated);
            dailyReportTotal.GasExportTarget = convertToDecimal(newDailyReport.gas_export_target);
            dailyReportTotal.GasExportMtd = convertToDecimal(newDailyReport.gas_export_MTD);
            dailyReportTotal.GasExportYtd = convertToDecimal(newDailyReport.gas_export_YTD);
            dailyReportTotal.GasImportAllocated = convertToDecimal(newDailyReport.gas_import_allocated);
            dailyReportTotal.GasImportMtd = convertToDecimal(newDailyReport.gas_import_MTD);
            dailyReportTotal.GasInjAllocated = convertToDecimal(newDailyReport.gas_inj_allocated);
            dailyReportTotal.GasInjTarget = convertToDecimal(newDailyReport.gas_inj_target);
            dailyReportTotal.GasInjMtd = convertToDecimal(newDailyReport.gas_inj_MTD);
            dailyReportTotal.GasInjYtd = convertToDecimal(newDailyReport.gas_inj_YTD);
            dailyReportTotal.WaterProdAllocated = convertToDecimal(newDailyReport.water_prod_allocated);
            dailyReportTotal.WaterProdTarget = convertToDecimal(newDailyReport.water_prod_target);
            dailyReportTotal.WaterProdMtd = convertToDecimal(newDailyReport.water_prod_MTD);
            dailyReportTotal.WaterProdYtd = convertToDecimal(newDailyReport.water_prod_YTD);
            dailyReportTotal.WaterInjAllocated = convertToDecimal(newDailyReport.water_inj_allocated);
            dailyReportTotal.WaterInjProduced = convertToDecimal(newDailyReport.water_inj_produced);
            dailyReportTotal.WaterInjTarget = convertToDecimal(newDailyReport.water_inj_target);
            dailyReportTotal.WaterInjMtd = convertToDecimal(newDailyReport.water_inj_MTD);
            dailyReportTotal.WaterInjYtd = convertToDecimal(newDailyReport.water_inj_YTD);
            dailyReportTotal.GasLiftAllocated = convertToDecimal(newDailyReport.gas_lift_allocated);
            dailyReportTotal.OilLossAllocated = convertToDecimal(newDailyReport.oil_loss_allocated);
            dailyReportTotal.FlareGasAllocated = convertToDecimal(newDailyReport.flare_gas_allocated);
            dailyReportTotal.FuelGasAllocated = convertToDecimal(newDailyReport.fuel_gas_allocated);
            dailyReportTotal.Co2Extracted = convertToDecimal(newDailyReport.CO2_extracted);
            dailyReportTotal.WaterDischarged = convertToDecimal(newDailyReport.water_discharged);
            dailyReportTotal.BsW = convertToDecimal(newDailyReport.BS_W);


            return dailyReportTotal;
        }

        /**
         * DailyProductionWells
         */
        public static List<DailyProductionWells> ObjectToDailyProductionWellsList(dynamic newDailyProductionWellsList, int dailyReportTotalId)
        {
            List<DailyProductionWells> dailyProdWellList = new List<DailyProductionWells>();

            foreach(dynamic newDailyProdWell in newDailyProductionWellsList)
            {
                DailyProductionWells dailyProdWell = ObjectToDailyProductionWell(newDailyProdWell, dailyReportTotalId);
                dailyProdWellList.Add(dailyProdWell);
            }

            return dailyProdWellList;

        }

        private static DailyProductionWells ObjectToDailyProductionWell(dynamic newDailyProductionWell, int dailyReportTotalId)
        {
            DailyProductionWells dailyProdWell = new DailyProductionWells();

            dailyProdWell.DailyreportId = dailyReportTotalId;
            dailyProdWell.WellName = Convert.ToString(newDailyProductionWell.well_name);
            dailyProdWell.WellTrunkline = Convert.ToString(newDailyProductionWell.well_trunkline);
            dailyProdWell.OnlineTime = convertToShort(newDailyProductionWell.online_time);
            dailyProdWell.ChokeOpening = convertToDecimal(newDailyProductionWell.choke_opening);
            dailyProdWell.Whp = convertToShort(newDailyProductionWell.WHP);
            dailyProdWell.Wht = convertToShort(newDailyProductionWell.WHT);
            dailyProdWell.Bhp = convertToShort(newDailyProductionWell.BHP);
            dailyProdWell.Bht = convertToShort(newDailyProductionWell.BHT);
            dailyProdWell.OilProdAllocated = convertToDecimal(newDailyProductionWell.oil_prod_allocated);
            dailyProdWell.OilProdTarget = convertToDecimal(newDailyProductionWell.oil_prod_target);
            dailyProdWell.GasProdAllocated = convertToDecimal(newDailyProductionWell.gas_prod_allocated);
            dailyProdWell.CondensateProdAllocated = convertToDecimal(newDailyProductionWell.codensate_prod_allocated);
            dailyProdWell.LpgProdAllocated = convertToDecimal(newDailyProductionWell.LPG_prod_allocated);
            dailyProdWell.WaterProductionAllocated = convertToInt(newDailyProductionWell.water_production_allocated);
            dailyProdWell.Gor = convertToDecimal(newDailyProductionWell.GOR);
            dailyProdWell.WaterCut = convertToDecimal(newDailyProductionWell.water_cut);
            dailyProdWell.GasLift = convertToShort(newDailyProductionWell.gas_lift);
            dailyProdWell.AnnulusPressure = convertToShort(newDailyProductionWell.annulus_pressure);
            dailyProdWell.Zone = convertToShort(newDailyProductionWell.zone);

            return dailyProdWell;

        }


        /**
         * DailyProductionWells
         */
        public static List<DailyWiWells> ObjectToDailyWaterInjectionWellList(dynamic newDailyWaterInjectionWellsList, int dailyReportTotalId)
        {
            List<DailyWiWells> dailyWaterInjectionWellList = new List<DailyWiWells>();

            foreach (dynamic newDailyWaterInjectionWell in newDailyWaterInjectionWellsList)
            {
                DailyWiWells dailyWaterInjectionWell = ObjectToDailyProductionWell(newDailyWaterInjectionWell, dailyReportTotalId);
                dailyWaterInjectionWellList.Add(dailyWaterInjectionWell);
            }

            return dailyWaterInjectionWellList;

        }

        private static DailyWiWells ObjectToDailyWaterInjectionWell(dynamic newDailyWaterInjectionWell, int dailyReportTotalId)
        {
            DailyWiWells dailyWaterInjectionWell = new DailyWiWells();

            dailyWaterInjectionWell.DailyreportId = dailyReportTotalId;
            dailyWaterInjectionWell.WellName = Convert.ToString(newDailyWaterInjectionWell.well_name);
            dailyWaterInjectionWell.OnlineTime = convertToShort(newDailyWaterInjectionWell.online_time);
            dailyWaterInjectionWell.ChokeOpening = convertToDecimal(newDailyWaterInjectionWell.choke_opening);
            dailyWaterInjectionWell.Whp = convertToShort(newDailyWaterInjectionWell.WHP);
            dailyWaterInjectionWell.Wht = convertToShort(newDailyWaterInjectionWell.WHT);
            dailyWaterInjectionWell.Bhp = convertToShort(newDailyWaterInjectionWell.BHP);
            dailyWaterInjectionWell.BhpLimit = convertToShort(newDailyWaterInjectionWell.BHT_limit);
            dailyWaterInjectionWell.Bht = convertToShort(newDailyWaterInjectionWell.BHT);
            dailyWaterInjectionWell.WaterInjectionAllocated = convertToDecimal(newDailyWaterInjectionWell.water_injection_allocated);
            dailyWaterInjectionWell.WaterInjectionTarget = convertToDecimal(newDailyWaterInjectionWell.water_injection_target);
            dailyWaterInjectionWell.WaterInjectionMeasured = convertToDecimal(newDailyWaterInjectionWell.water_injection_measured);

            return dailyWaterInjectionWell;

        }

        /**
         * DailyGasInjectionWells
         */
        public static List<DailyGiWells> ObjectToDailyGasInjectionWellList(dynamic newDailyGasInjectionWellsList, int dailyReportTotalId)
        {
            List<DailyGiWells> dailyGasInjectionWellList = new List<DailyGiWells>();

            foreach (dynamic newDailyGasInjectionWell in newDailyGasInjectionWellsList)
            {
                DailyGiWells dailyGasInjectionWell = ObjectToDailyProductionWell(newDailyGasInjectionWell, dailyReportTotalId);
                dailyGasInjectionWellList.Add(dailyGasInjectionWell);
            }

            return dailyGasInjectionWellList;

        }

        private static DailyGiWells ObjectToDailyGasInjectionWell(dynamic newDailyGasInjectionWell, int dailyReportTotalId)
        {
            DailyGiWells dailyGasInjectionWell = new DailyGiWells();

            dailyGasInjectionWell.DailyreportId = dailyReportTotalId;
            dailyGasInjectionWell.WellName = Convert.ToString(newDailyGasInjectionWell.well_name);
            dailyGasInjectionWell.OnlineTime = convertToShort(newDailyGasInjectionWell.online_time);
            dailyGasInjectionWell.ChokeOpening = convertToDecimal(newDailyGasInjectionWell.choke_opening);
            dailyGasInjectionWell.Whp = convertToShort(newDailyGasInjectionWell.WHP);
            dailyGasInjectionWell.Wht = convertToShort(newDailyGasInjectionWell.WHT);
            dailyGasInjectionWell.Bhp = convertToShort(newDailyGasInjectionWell.BHP);
            dailyGasInjectionWell.Bht = convertToShort(newDailyGasInjectionWell.BHT);
            dailyGasInjectionWell.GasInjectionAllocated = convertToDecimal(newDailyGasInjectionWell.gas_injection_allocated);
            dailyGasInjectionWell.GasInjectionTarget = convertToDecimal(newDailyGasInjectionWell.gas_injection_target);
            dailyGasInjectionWell.GasInjectionMeasured = convertToDecimal(newDailyGasInjectionWell.gas_injection_measured);

            return dailyGasInjectionWell;

        }

        /**
         * Comments 
         */
        public static List<Comments> ObjectToCommentsList(dynamic newComments, int dailyReportTotalId)
        {
            List<Comments> commentsList = new List<Comments>();

            foreach (dynamic newComment in newComments)
            {
                Comments comment = ObjectToComment(newComment, dailyReportTotalId);
                commentsList.Add(comment);
            }

            return commentsList;

        }

        private static Comments ObjectToComment(dynamic newComment, int dailyReportTotalId)
        {
            Comments comment = new Comments();

            
            // Trim and remove extra spaces
            string mainEventsTrimmed = Convert.ToString(newComment.main_events);
            mainEventsTrimmed = mainEventsTrimmed.TrimStart().TrimEnd();
            mainEventsTrimmed = Regex.Replace(mainEventsTrimmed, @"\s+", " ");

            comment.MainEvents = mainEventsTrimmed;
            comment.DailyreportId = dailyReportTotalId;

            return comment;
        }



            /**
            * WellTest
            */

            public static List<WellTests> ObjectToWellTestList(dynamic newWellTests, int dailyReportTotalId)
        {
            List<WellTests> wellTestList = new List<WellTests>();

            foreach (dynamic newWellTest in newWellTests)
            {
                WellTests wellTest = ObjectToWellTest(newWellTest, dailyReportTotalId);
                wellTestList.Add(wellTest);
            }

            return wellTestList;

        }

        private static WellTests ObjectToWellTest(dynamic newWellTest, int dailyReportTotalId)
        {
            WellTests wellTest = new WellTests();

            wellTest.DailyreportId = dailyReportTotalId;
            wellTest.WellName = Convert.ToString(newWellTest.well_name);
            wellTest.TestDate = convertToDate(newWellTest.test_date);
            wellTest.OnlineTime = convertToShort(newWellTest.online_time);
            wellTest.ChokeOpening = convertToDecimal(newWellTest.choke_opening);
            wellTest.OilProdRate = convertToDecimal(newWellTest.oil_prod_rate);
            wellTest.GasProdRate = convertToDecimal(newWellTest.gas_prod_rate);
            wellTest.WaterProdRate = convertToDecimal(newWellTest.water_prod_rate);
            wellTest.Whp = convertToShort(newWellTest.WHP);
            wellTest.Wht = convertToShort(newWellTest.WHT);
            wellTest.Bhp = convertToShort(newWellTest.BHP);
            wellTest.Bht = convertToShort(newWellTest.BHT);
            wellTest.SeparatorPressure = convertToDecimal(newWellTest.separator_pressure);
            wellTest.Bsw = convertToDecimal(newWellTest.BSW);
            wellTest.Gor = convertToDecimal(newWellTest.GOR);
            wellTest.SandProd = convertToDecimal(newWellTest.gas_injection_measured);
            wellTest.Glr = convertToDecimal(newWellTest.GLR);

            return wellTest;

        }

        /**
         * Internal methods for converting  
         */

        private static int convertToInt(dynamic strInt)
        {
            int outputInt;


            if (Int32.TryParse(Convert.ToString(strInt), out outputInt)) return outputInt;

            return 0; // default return value if missing or invalid type
        }

        private static short convertToShort(dynamic strInt)
        {
            short outputShort;

            if (Int16.TryParse(Convert.ToString(strInt), out outputShort)) return outputShort;

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
