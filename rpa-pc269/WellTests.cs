using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class WellTests
    {
        public int WelltestId { get; set; }
        public int DailyreportId { get; set; }
        public string WellName { get; set; }
        public DateTime? TestDate { get; set; }
        public short? OnlineTime { get; set; }
        public decimal? ChokeOpening { get; set; }
        public decimal? OilProdRate { get; set; }
        public decimal? GasProdRate { get; set; }
        public decimal? WaterProdRate { get; set; }
        public short? Bhp { get; set; }
        public short? Bht { get; set; }
        public short? Whp { get; set; }
        public short? Wht { get; set; }
        public decimal? SeparatorPressure { get; set; }
        public decimal? Bsw { get; set; }
        public decimal? Gor { get; set; }
        public decimal? SandProd { get; set; }
        public decimal? Glr { get; set; }
    }
}
