using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class DailyWiWells
    {
        public int WiwellId { get; set; }
        public int DailyreportId { get; set; }
        public string WellName { get; set; }
        public decimal? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public decimal? Whp { get; set; }
        public decimal? Wht { get; set; }
        public decimal? Bhp { get; set; }
        public decimal? BhpLimit { get; set; }
        public decimal? Bht { get; set; }
        public decimal? WaterInjectionAllocated { get; set; }
        public decimal? WaterInjectionTarget { get; set; }
        public decimal? WaterInjectionMeasured { get; set; }
    }
}
