using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class DailyReportsTotal
    {
        public int DailyreportId { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public string FacilityName { get; set; }
        public decimal? OilProdAllocated { get; set; }
        public decimal? OilProdTarget { get; set; }
        public decimal? OilProdMtd { get; set; }
        public decimal? OilProdYtd { get; set; }
        public decimal? CondensateProdAllocated { get; set; }
        public decimal? CondensateExportAllocated { get; set; }
        public decimal? CondensateStockInitial { get; set; }
        public decimal? CondensateStockFinal { get; set; }
        public decimal? LpgProdAllocated { get; set; }
        public decimal? LpgExportAllocated { get; set; }
        public decimal? LpgStockInitial { get; set; }
        public decimal? LpgStockFinal { get; set; }
        public decimal? LpgSpiking { get; set; }
        public decimal? GasProdAllocated { get; set; }
        public decimal? GasProdTarget { get; set; }
        public decimal? GasProdMtd { get; set; }
        public decimal? GasProdYtd { get; set; }
        public decimal? GasExportAllocated { get; set; }
        public decimal? GasExportTarget { get; set; }
        public decimal? GasExportMtd { get; set; }
        public decimal? GasExportYtd { get; set; }
        public decimal? GasImportAllocated { get; set; }
        public decimal? GasImportMtd { get; set; }
        public decimal? GasInjAllocated { get; set; }
        public decimal? GasInjTarget { get; set; }
        public decimal? GasInjMtd { get; set; }
        public decimal? GasInjYtd { get; set; }
        public decimal? WaterProdAllocated { get; set; }
        public decimal? WaterProdTarget { get; set; }
        public decimal? WaterProdMtd { get; set; }
        public decimal? WaterProdYtd { get; set; }
        public decimal? WaterInjAllocated { get; set; }
        public decimal? WaterInjProduced { get; set; }
        public decimal? WaterInjTarget { get; set; }
        public decimal? WaterInjMtd { get; set; }
        public decimal? WaterInjYtd { get; set; }
        public decimal? GasLiftAllocated { get; set; }
        public decimal? OilLossAllocated { get; set; }
        public decimal? FlareGasAllocated { get; set; }
        public decimal? FuelGasAllocated { get; set; }
        public decimal? Co2Extracted { get; set; }
        public decimal? WaterDischarged { get; set; }
        public decimal? BsW { get; set; }
    }
}
