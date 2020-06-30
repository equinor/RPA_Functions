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
        public short? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public short? Whp { get; set; }
        public short? Wht { get; set; }
        public short? Bhp { get; set; }
        public short? Bht { get; set; }
        public decimal? GasInjectionAllocated { get; set; }
        public decimal? GasInjectionTarget { get; set; }
        public decimal? GasInjectionMeasured { get; set; }
    }
}

