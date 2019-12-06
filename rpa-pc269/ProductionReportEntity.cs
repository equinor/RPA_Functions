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
        public DbSet<WellsTest> WellsTests { get; set; }
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
        public string prod_oil_unit { get; set; }
        public int prod_gas { get; set; }
        public string prod_gas_unit { get; set; }
        public int prod_water { get; set; }
        public string prod_water_unit { get; set; }
        public int injection_water { get; set; }
        public string injection_water_unit { get; set; }
        public int injection_gas { get; set; }
        public string injection_gas_unit { get; set; }
        public int fuel_gas { get; set; }
        public string fuel_gas_unit { get; set; }
        public int flare_gas { get; set; }
        public string flare_gas_unit { get; set; }
        public DateTime production_date { get; set; }
        public string production_report_uri { get; set; }

    }

    public class WellsTest
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

}
