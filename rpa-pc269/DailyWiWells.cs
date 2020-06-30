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
        public short? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public short? Whp { get; set; }
        public short? Wht { get; set; }
        public short? Bhp { get; set; }
        public short? BhpLimit { get; set; }
        public short? Bht { get; set; }
        public decimal? WaterInjectionAllocated { get; set; }
        public decimal? WaterInjectionTarget { get; set; }
        public decimal? WaterInjectionMeasured { get; set; }
    }
}
