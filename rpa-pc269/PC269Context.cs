using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rpa_functions.rpa_pc269
{

    public partial class PC269UnifiedContext : DbContext
    {
        public PC269UnifiedContext() { }

        public PC269UnifiedContext(DbContextOptions<PC269UnifiedContext> options) : base(options) { }

        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DailyGiWells> DailyGiWells { get; set; }
        public virtual DbSet<DailyProductionWells> DailyProductionWells { get; set; }
        public virtual DbSet<DailyReportsTotal> DailyReportsTotal { get; set; }
        public virtual DbSet<DailyWiWells> DailyWiWells { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<WellTests> WellTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("PC269_DBCONNECTION"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assets>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK_ASSETS");

                entity.ToTable("Assets", "PC269");

                entity.HasIndex(e => e.AssetName)
                    .HasName("AK1_Assets")
                    .IsUnique();

                entity.Property(e => e.AssetId).HasColumnName("asset_Id");

                entity.Property(e => e.AbbyyBatch)
                    .IsRequired()
                    .HasColumnName("abbyy_batch")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasColumnName("asset_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(40);

                entity.Property(e => e.InstType)
                    .IsRequired()
                    .HasColumnName("inst_type")
                    .HasMaxLength(25);

                entity.Property(e => e.Sla).HasColumnName("sla");

                entity.Property(e => e.Verifier)
                    .IsRequired()
                    .HasColumnName("verifier")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_COMMENTS");

                entity.ToTable("Comments", "PC269");

                entity.Property(e => e.CommentId).HasColumnName("comment_Id");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.MainEvents)
                    .HasColumnName("main_events")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<DailyGiWells>(entity =>
            {
                entity.HasKey(e => e.GiwellId)
                    .HasName("PK_WELLSTEST");

                entity.ToTable("Daily_GI_Wells", "PC269");

                entity.Property(e => e.GiwellId).HasColumnName("giwell_Id");

                entity.Property(e => e.Bhp).HasColumnName("BHP");

                entity.Property(e => e.Bht).HasColumnName("BHT");

                entity.Property(e => e.ChokeOpening)
                    .HasColumnName("choke_opening")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.GasInjectionAllocated)
                    .HasColumnName("gas_injection_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjectionMeasured)
                    .HasColumnName("gas_injection_measured")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjectionTarget)
                    .HasColumnName("gas_injection_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OnlineTime).HasColumnName("online_time");

                entity.Property(e => e.WellName)
                    .IsRequired()
                    .HasColumnName("well_name")
                    .HasMaxLength(25);

                entity.Property(e => e.Whp).HasColumnName("WHP");

                entity.Property(e => e.Wht).HasColumnName("WHT");
            });

            modelBuilder.Entity<DailyProductionWells>(entity =>
            {
                entity.HasKey(e => e.ProdwellId)
                    .HasName("PK_WELLSTESTID");

                entity.ToTable("Daily_Production_Wells", "PC269");

                entity.Property(e => e.ProdwellId).HasColumnName("prodwell_Id");

                entity.Property(e => e.AnnulusPressure).HasColumnName("annulus_pressure");

                entity.Property(e => e.Bhp).HasColumnName("BHP");

                entity.Property(e => e.Bht).HasColumnName("BHT");

                entity.Property(e => e.ChokeOpening)
                    .HasColumnName("choke_opening")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.CondensateProdAllocated)
                    .HasColumnName("condensate_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.GasLift).HasColumnName("gas_lift");

                entity.Property(e => e.GasProdAllocated)
                    .HasColumnName("gas_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Gor)
                    .HasColumnName("GOR")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.LpgProdAllocated)
                    .HasColumnName("LPG_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdAllocated)
                    .HasColumnName("oil_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdTarget)
                    .HasColumnName("oil_prod_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OnlineTime).HasColumnName("online_time");

                entity.Property(e => e.WaterCut)
                    .HasColumnName("water_cut")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.WaterProductionAllocated).HasColumnName("water_production_allocated");

                entity.Property(e => e.WellName)
                    .IsRequired()
                    .HasColumnName("well_name")
                    .HasMaxLength(25);

                entity.Property(e => e.WellTrunkline)
                    .HasColumnName("well_trunkline")
                    .HasMaxLength(25);

                entity.Property(e => e.Whp).HasColumnName("WHP");

                entity.Property(e => e.Wht).HasColumnName("WHT");

                entity.Property(e => e.Zone).HasColumnName("zone");
            });

            modelBuilder.Entity<DailyReportsTotal>(entity =>
            {
                entity.HasKey(e => e.DailyreportId)
                    .HasName("PK_DAILYREPORT");

                entity.ToTable("DailyReportsTotal", "PC269");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.AssetId).HasColumnName("asset_Id");

                entity.Property(e => e.BsW)
                    .HasColumnName("BS_W")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Co2Extracted)
                    .HasColumnName("CO2_extracted")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.CondensateExportAllocated)
                    .HasColumnName("condensate_export_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.CondensateProdAllocated)
                    .HasColumnName("condensate_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.CondensateStockFinal)
                    .HasColumnName("condensate_stock_final")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.CondensateStockInitial)
                    .HasColumnName("condensate_stock_initial")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.FacilityName)
                    .HasColumnName("facility_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlareGasAllocated)
                    .HasColumnName("flare_gas_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.FuelGasAllocated)
                    .HasColumnName("fuel_gas_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasExportAllocated)
                    .HasColumnName("gas_export_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasExportMtd)
                    .HasColumnName("gas_export_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasExportTarget)
                    .HasColumnName("gas_export_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasExportYtd)
                    .HasColumnName("gas_export_YTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasImportAllocated)
                    .HasColumnName("gas_import_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasImportMtd)
                    .HasColumnName("gas_import_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjAllocated)
                    .HasColumnName("gas_inj_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjMtd)
                    .HasColumnName("gas_inj_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjTarget)
                    .HasColumnName("gas_inj_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasInjYtd)
                    .HasColumnName("gas_inj_YTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasLiftAllocated)
                    .HasColumnName("gas_lift_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasProdAllocated)
                    .HasColumnName("gas_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasProdMtd)
                    .HasColumnName("gas_prod_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasProdTarget)
                    .HasColumnName("gas_prod_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.GasProdYtd)
                    .HasColumnName("gas_prod_YTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.LpgExportAllocated)
                    .HasColumnName("LPG_export_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.LpgProdAllocated)
                    .HasColumnName("LPG_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.LpgSpiking)
                    .HasColumnName("LPG_spiking")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.LpgStockFinal)
                    .HasColumnName("LPG_stock_final")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.LpgStockInitial)
                    .HasColumnName("LPG_stock_initial")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilLossAllocated)
                    .HasColumnName("oil_loss_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdAllocated)
                    .HasColumnName("oil_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdMtd)
                    .HasColumnName("oil_prod_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdTarget)
                    .HasColumnName("oil_prod_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.OilProdYtd)
                    .HasColumnName("oil_prod_YTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterDischarged)
                    .HasColumnName("water_discharged")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjAllocated)
                    .HasColumnName("water_inj_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjMtd)
                    .HasColumnName("water_inj_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjProduced)
                    .HasColumnName("water_inj_produced")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjTarget)
                    .HasColumnName("water_inj_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjYtd)
                    .HasColumnName("water_inj_YTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterProdAllocated)
                    .HasColumnName("water_prod_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterProdMtd)
                    .HasColumnName("water_prod_MTD")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterProdTarget)
                    .HasColumnName("water_prod_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterProdYtd)
                    .HasColumnName("water_prod_YTD")
                    .HasColumnType("decimal(14, 2)");
            });

            modelBuilder.Entity<DailyWiWells>(entity =>
            {
                entity.HasKey(e => e.WiwellId)
                    .HasName("PK_WIWELLID");

                entity.ToTable("Daily_WI_Wells", "PC269");

                entity.Property(e => e.WiwellId).HasColumnName("wiwell_Id");

                entity.Property(e => e.Bhp).HasColumnName("BHP");

                entity.Property(e => e.BhpLimit).HasColumnName("BHP_limit");

                entity.Property(e => e.Bht).HasColumnName("BHT");

                entity.Property(e => e.ChokeOpening)
                    .HasColumnName("choke_opening")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.OnlineTime).HasColumnName("online_time");

                entity.Property(e => e.WaterInjectionAllocated)
                    .HasColumnName("water_injection_allocated")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjectionMeasured)
                    .HasColumnName("water_injection_measured")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WaterInjectionTarget)
                    .HasColumnName("water_injection_target")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WellName)
                    .IsRequired()
                    .HasColumnName("well_name")
                    .HasMaxLength(25);

                entity.Property(e => e.Whp).HasColumnName("WHP");

                entity.Property(e => e.Wht).HasColumnName("WHT");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("Units", "PC269");

                entity.Property(e => e.UnitId)
                    .HasColumnName("unit_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnnulusPressureUnit)
                    .HasColumnName("annulus_pressure_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.AssetId).HasColumnName("asset_Id");

                entity.Property(e => e.BhpUnit)
                    .HasColumnName("bhp_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.BhtUnit)
                    .HasColumnName("bht_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.BsWUnit)
                    .HasColumnName("bs_w_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.ChokeOpeningUnit)
                    .HasColumnName("choke_opening_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.Co2ExtractedUnit)
                    .HasColumnName("co2_extracted_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.CondensateUnit)
                    .HasColumnName("condensate_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.GasUnit)
                    .HasColumnName("gas_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.GlrUnit)
                    .HasColumnName("glr_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.GorUnit)
                    .HasColumnName("gor_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.LpgUnit)
                    .HasColumnName("lpg_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.OilUnit)
                    .HasColumnName("oil_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.OnlineTimeUnit)
                    .HasColumnName("online_time_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.SandProdUnit)
                    .HasColumnName("sand_prod_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.SeparatorPressureUnit)
                    .HasColumnName("separator_pressure_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.WaterInjUnit)
                    .HasColumnName("water_inj_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.WctUnit)
                    .HasColumnName("wct_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.WhpUnit)
                    .HasColumnName("whp_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.WhtUnit)
                    .HasColumnName("wht_unit")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<WellTests>(entity =>
            {
                entity.HasKey(e => e.WelltestId)
                    .HasName("PK_WELLSTESTID1");

                entity.ToTable("Well_Tests", "PC269");

                entity.Property(e => e.WelltestId).HasColumnName("welltest_Id");

                entity.Property(e => e.Bhp).HasColumnName("BHP");

                entity.Property(e => e.Bht).HasColumnName("BHT");

                entity.Property(e => e.Bsw)
                    .HasColumnName("BSW")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ChokeOpening)
                    .HasColumnName("choke_opening")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.DailyreportId).HasColumnName("dailyreport_Id");

                entity.Property(e => e.GasProdRate)
                    .HasColumnName("gas_prod_rate")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Glr)
                    .HasColumnName("GLR")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Gor)
                    .HasColumnName("GOR")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OilProdRate)
                    .HasColumnName("oil_prod_rate")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OnlineTime).HasColumnName("online_time");

                entity.Property(e => e.SandProd)
                    .HasColumnName("sand_prod")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.SeparatorPressure)
                    .HasColumnName("separator_pressure")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.TestDate)
                    .HasColumnName("test_date")
                    .HasColumnType("date");

                entity.Property(e => e.WaterProdRate)
                    .HasColumnName("water_prod_rate")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.WellName)
                    .IsRequired()
                    .HasColumnName("well_name")
                    .HasMaxLength(25);

                entity.Property(e => e.Whp).HasColumnName("WHP");

                entity.Property(e => e.Wht).HasColumnName("WHT");
            });




            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
