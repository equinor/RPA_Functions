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

        //public virtual DbSet<Assets> Assets { get; set; }
        //public virtual DbSet<Comments> Comments { get; set; }
        //public virtual DbSet<DailyGiWells> DailyGiWells { get; set; }
        //public virtual DbSet<DailyProductionWells> DailyProductionWells { get; set; }
        public virtual DbSet<DailyReportsTotal> DailyReportsTotal { get; set; }
        //public virtual DbSet<DailyWiWells> DailyWiWells { get; set; }
        //public virtual DbSet<Units> Units { get; set; }
        //public virtual DbSet<WellTests> WellTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("PC269_DBCONNECTION"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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




            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
