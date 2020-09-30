using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class DailyGiWells
    {
        public int GiwellId { get; set; }
        public int DailyreportId { get; set; }
        public string WellName { get; set; }
        public decimal? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public decimal? Whp { get; set; }
        public decimal? Wht { get; set; }
        public decimal? Bhp { get; set; }
        public decimal? Bht { get; set; }
        public decimal? GasInjectionAllocated { get; set; }
        public decimal? GasInjectionTarget { get; set; }
        public decimal? GasInjectionMeasured { get; set; }
    }
}

