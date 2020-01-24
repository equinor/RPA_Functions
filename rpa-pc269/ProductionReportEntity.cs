using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rpa_functions.rpa_pc269
{

    public class PC269Context : DbContext
    {
        public PC269Context(DbContextOptions<PC269Context> options)
            : base(options)
        { }
        
        public DbSet<Asset> Assets { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<WellsDetails> WellsDetails { get; set; } // Change db name and modify table
        public DbSet<GasInjectionWell> GasInjectionWells { get; set; }
        public DbSet<WaterInjectionWell> WaterInjectionWells { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }

    public class Asset
    {
        [Key]
        public int asset_Id { get; set; }
        public string country { get; set; }
        public string asset_name { get; set; }
        public string inst_type { get; set; }
        public string verifier { get; set; }
        public short sla { get; set; }
        public string abbyy_batch { get; set; }
        public string prod_oil_default_unit { get; set; }
        public string prod_gas_default_unit { get; set; }
        public string prod_water_default_unit { get; set; }
        public string injection_water_unit_default_unit { get; set; }
        public string injection_gas_unit_default_unit { get; set; }
        public string fuel_gas_default_unit { get; set; }
        public string flare_gas_default_unit { get; set; }

    }

    public class DailyReport
    {
        [Key]
        public long dailyreport_Id { get; set;  }
        public int asset_Id { get; set; }
        public int prod_oil { get; set; }
        public int prod_gas { get; set; }
        public int prod_water { get; set; }
        public int injection_water { get; set; }
        public int injection_gas { get; set; }
        public int fuel_gas { get; set; }
        public int flare_gas { get; set; }
        public DateTime production_date { get; set; }
        public string production_report_uri { get; set; }

    }

    public class WellsDetails // change name
    {
        [Key]
        public long wellTest_Id { get; set; }
        public long dailyreport_Id { get; set; }
        public string well_name { get; set; }
        public DateTime last_testdate { get; set; }
        public int hours_test { get; set; }
        public int oil_rate { get; set; }
        public int wellhead_press { get; set; }
        public int wellhead_temp { get; set; }
        public decimal choke_open { get; set; }
        public int test_sep { get; set; }
        public int water_rate { get; set; }
        public decimal bsw { get; set; }
        public int gas_rate { get; set; }
        public int gor { get; set; }
        public int glr { get; set; }
        // Production 24 hours on well
        public int prodHoursOn { get; set; }
        public DateTime prodDay { get; set; }
        public int prodOil { get; set; }
        public int prodWater { get; set; }
        public int prodGas { get; set; }
        public decimal prodChokeOpen { get; set; }
        public int prodWellHeadPress {get;set;}
        public int prodWellHeadTemp { get; set; }
        public int prodAnnulusPress { get; set; }

    }

    public class GasInjectionWell
    {
        [Key]
        public long gasinjection_Id { get; set; }
        public long dailyreport_Id { get; set; }
        public string well_name { get; set; }
        public DateTime prod_date { get; set; }
        public int hours_on { get; set; }
        public decimal choke_open { get; set; }
        public int wellhead_press { get; set; }
        public int fl { get; set; }
        public int wellhead_temp { get; set; }
        public int metered_vol { get; set; }
        public int alloc_volume { get; set; }
        public int month_cumulative { get; set; }
        public long year_cumulative { get; set; }
    }

    public class WaterInjectionWell
    {
        [Key]
        public long waterinjection_Id { get; set; }
        public long dailyreport_Id { get; set; }
        public string well_name { get; set; }
        public DateTime prod_date { get; set; }
        public int hours_on { get; set; }
        public decimal choke_open { get; set; }
        public int wellhead_press { get; set; }
        public int fl { get; set; }
        public int wellhead_temp { get; set; }
        public int metered_vol { get; set; }
        public int alloc_volume { get; set; }
        public int month_cumulative { get; set; }
        public long year_cumulative { get; set; }
    }


    public class Comment
    {
        [Key]
        public long comment_Id { get; set; }
        public long dailyreport_Id { get; set; }
        public string comment { get; set; }

    }

    public class PC269ContextFactory : IDesignTimeDbContextFactory<PC269Context>
    {
        public PC269Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PC269Context>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new PC269Context(optionsBuilder.Options);
        }
    }

    public static class PC269Mappings
    {
        
        // -----
        // Comment does not require mapping. Implement in API
        // -----

        // Implement in API
        public static List<WaterInjectionWell> ObjectToWaterInjectionWellList(dynamic waterInjectionWells, int reportId)
        {
            List<WaterInjectionWell> waterInjectionWellList = new List<WaterInjectionWell>();

            foreach (var element in waterInjectionWells)
            {
                WaterInjectionWell waterInjectionWell = new WaterInjectionWell();

                waterInjectionWell.dailyreport_Id = reportId;
                waterInjectionWell.well_name = Convert.ToString(element.well_name);
                waterInjectionWell.prod_date = convertToDate(element.prod_date);
                waterInjectionWell.hours_on = convertToInt(element.hours_on);
                waterInjectionWell.choke_open = convertToDecimal(element.choke_open);
                waterInjectionWell.wellhead_press = convertToInt(element.wellhead_press);
                waterInjectionWell.fl = convertToInt(element.fl);
                waterInjectionWell.wellhead_temp = convertToInt(element.wellhead_temp);
                waterInjectionWell.metered_vol = convertToInt(element.metered_vol);
                waterInjectionWell.alloc_volume = convertToInt(element.alloc_volume);
                waterInjectionWell.month_cumulative = convertToInt(element.month_cumulative);
                waterInjectionWell.year_cumulative = convertToInt(element.year_cumulative);

                waterInjectionWellList.Add(waterInjectionWell);

            }


            return waterInjectionWellList;
        }


        // Implement in API
        public static List<GasInjectionWell> ObjectToGasInjectionWellList(dynamic gasInjectionWells, int reportId)
        {
            List<GasInjectionWell> gasInjectionWellList = new List<GasInjectionWell>();

            foreach (var element in gasInjectionWells)
            {
                GasInjectionWell gasInjectionWell = new GasInjectionWell();

                gasInjectionWell.dailyreport_Id = reportId;
                gasInjectionWell.well_name = Convert.ToString(element.well_name);
                gasInjectionWell.prod_date = convertToDate(element.prod_date);
                gasInjectionWell.hours_on = convertToInt(element.hours_on);
                gasInjectionWell.choke_open = convertToDecimal(element.choke_open);
                gasInjectionWell.wellhead_press = convertToInt(element.wellhead_press);
                gasInjectionWell.fl = convertToInt(element.fl);
                gasInjectionWell.wellhead_temp = convertToInt(element.wellhead_temp);
                gasInjectionWell.metered_vol = convertToInt(element.metered_vol);
                gasInjectionWell.alloc_volume = convertToInt(element.alloc_volume);
                gasInjectionWell.month_cumulative = convertToInt(element.month_cumulative);
                gasInjectionWell.year_cumulative = convertToInt(element.year_cumulative);

                gasInjectionWellList.Add(gasInjectionWell);
            }


            return gasInjectionWellList;
        }

        public static List<WellsDetails> ObjectToWellsDetailsList(dynamic wellsDetailsObject, int reportId)
        {
            List<WellsDetails> wellsDetailsList = new List<WellsDetails>();
            
   
            foreach (var element in wellsDetailsObject)
            {
                WellsDetails wellsDetails = new WellsDetails();

                wellsDetails.dailyreport_Id = reportId;
                wellsDetails.well_name = Convert.ToString(element.well_name);
                wellsDetails.last_testdate = convertToDate(element.last_testdate);
                wellsDetails.hours_test = convertToInt(element.hours_test);
                wellsDetails.oil_rate = convertToInt(element.oil_rate);
                wellsDetails.wellhead_press = convertToInt(element.wellhead_press);
                wellsDetails.wellhead_temp = convertToInt(element.wellhead_temp);
                wellsDetails.choke_open = convertToDecimal(element.choke_open);
                wellsDetails.test_sep = convertToInt(element.test_sep);
                wellsDetails.water_rate = convertToInt(element.water_rate);
                wellsDetails.bsw = convertToDecimal(element.bsw);
                wellsDetails.gas_rate = convertToInt(element.gas_rate);
                wellsDetails.gor = convertToInt(element.gor);
                wellsDetails.glr = convertToInt(element.glr);
                wellsDetails.prodHoursOn = convertToInt(element.HrsOn);
                wellsDetails.prodDay = convertToDate(element.ProductionDay);
                wellsDetails.prodOil = convertToInt(element.Oil);
                wellsDetails.prodWater = convertToInt(element.Water);
                wellsDetails.prodGas = convertToInt(element.Gas);
                wellsDetails.prodChokeOpen = convertToDecimal(element.ChokeOpen);
                wellsDetails.prodWellHeadPress = convertToInt(element.WellHeadPress);
                wellsDetails.prodWellHeadTemp = convertToInt(element.WellHeadTemp);
                wellsDetails.prodAnnulusPress = convertToInt(element.AnnulusPress);


                wellsDetailsList.Add(wellsDetails);
            }


            return wellsDetailsList;
        }



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
