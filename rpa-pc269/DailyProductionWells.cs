﻿using System;
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
        public decimal? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public decimal? Whp { get; set; }
        public decimal? Wht { get; set; }
        public decimal? Bhp { get; set; }
        public decimal? Bht { get; set; }
        public decimal? OilProdAllocated { get; set; }
        public decimal? OilProdTarget { get; set; }
        public decimal? GasProdAllocated { get; set; }
        public decimal? CondensateProdAllocated { get; set; }
        public decimal? LpgProdAllocated { get; set; }
        public decimal? WaterProductionAllocated { get; set; }
        public decimal? Gor { get; set; }
        public decimal? WaterCut { get; set; }
        public decimal? GasLift { get; set; }
        public decimal? AnnulusPressure { get; set; }
        public string? Zone { get; set; }
    }
}
