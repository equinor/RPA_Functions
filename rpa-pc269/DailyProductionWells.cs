using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class DailyProductionWells
    {
        public int ProdwellId { get; set; }
        public int DailyreportId { get; set; }
        public string WellName { get; set; }
        public string WellTrunkline { get; set; }
        public short? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public short? Whp { get; set; }
        public short? Wht { get; set; }
        public short? Bhp { get; set; }
        public short? Bht { get; set; }
        public decimal? OilProdAllocated { get; set; }
        public decimal? OilProdTarget { get; set; }
        public decimal? GasProdAllocated { get; set; }
        public decimal? CondensateProdAllocated { get; set; }
        public decimal? LpgProdAllocated { get; set; }
        public int? WaterProductionAllocated { get; set; }
        public decimal? Gor { get; set; }
        public decimal? WaterCut { get; set; }
        public short? GasLift { get; set; }
        public short? AnnulusPressure { get; set; }
        public short? Zone { get; set; }
    }
}
