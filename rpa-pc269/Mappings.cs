using System;
using System.Collections.Generic;
using System.Text;

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
            List < DailyProductionWells > dailyProdWellList = null;

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
            dailyProdWell.OnlineTime = convertToInt(newDailyProductionWell.online_time);
            dailyProdWell.ChokeOpening = convertToDecimal(newDailyProductionWell.choke_opening);
            dailyProdWell.Whp = convertToInt(newDailyProductionWell.WHP);
            dailyProdWell.Wht = convertToInt(newDailyProductionWell.WHT);
            dailyProdWell.Bhp = convertToInt(newDailyProductionWell.BHP);
            dailyProdWell.Bht = convertToInt(newDailyProductionWell.BHT);
            dailyProdWell.OilProdAllocated = convertToDecimal(newDailyProductionWell.oil_prod_allocated);
            dailyProdWell.OilProdTarget = convertToDecimal(newDailyProductionWell.oil_prod_target);
            dailyProdWell.GasProdAllocated = convertToDecimal(newDailyProductionWell.gas_prod_allocated);
            dailyProdWell.CondensateProdAllocated = convertToDecimal(newDailyProductionWell.codensate_prod_allocated);
            dailyProdWell.LpgProdAllocated = convertToDecimal(newDailyProductionWell.LPG_prod_allocated);
            dailyProdWell.WaterProductionAllocated = convertToInt(newDailyProductionWell.water_production_allocated);
            dailyProdWell.Gor = convertToDecimal(newDailyProductionWell.GOR);
            dailyProdWell.WaterCut = convertToDecimal(newDailyProductionWell.water_cut);
            dailyProdWell.GasLift = convertToInt(newDailyProductionWell.gas_lift);
            dailyProdWell.AnnulusPressure = convertToInt(newDailyProductionWell.annulus_pressure);
            dailyProdWell.Zone = convertToInt(newDailyProductionWell.zone);

            return dailyProdWell;

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
