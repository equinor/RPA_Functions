/****** Object:  Database [pc0269v2]    Script Date: 6/30/2020 12:32:44 PM ******/
CREATE DATABASE [pc0269v2]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S2', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [pc0269v2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pc0269v2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pc0269v2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pc0269v2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pc0269v2] SET ARITHABORT OFF 
GO
ALTER DATABASE [pc0269v2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pc0269v2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pc0269v2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pc0269v2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pc0269v2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pc0269v2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pc0269v2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pc0269v2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pc0269v2] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [pc0269v2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pc0269v2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pc0269v2] SET  MULTI_USER 
GO
ALTER DATABASE [pc0269v2] SET ENCRYPTION ON
GO
ALTER DATABASE [pc0269v2] SET QUERY_STORE = ON
GO
ALTER DATABASE [pc0269v2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
/****** Object:  User [rpa]    Script Date: 6/30/2020 12:32:44 PM ******/
CREATE USER [rpa] FOR LOGIN [rpa] WITH DEFAULT_SCHEMA=[PC269]
GO
/****** Object:  Schema [PC269]    Script Date: 6/30/2020 12:32:44 PM ******/
CREATE SCHEMA [PC269]
GO
/****** Object:  Table [PC269].[Assets]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Assets](
	[asset_Id] [int] IDENTITY(1,1) NOT NULL,
	[country] [nvarchar](40) NOT NULL,
	[asset_name] [nvarchar](50) NOT NULL,
	[inst_type] [nvarchar](25) NOT NULL,
	[verifier] [varchar](40) NOT NULL,
	[sla] [smallint] NOT NULL,
	[abbyy_batch] [varchar](40) NOT NULL,
 CONSTRAINT [PK_ASSETS] PRIMARY KEY CLUSTERED 
(
	[asset_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK1_Assets] UNIQUE NONCLUSTERED 
(
	[asset_name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Comments]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Comments](
	[comment_Id] [int] IDENTITY(1,1) NOT NULL,
	[dailyreport_Id] [int] NOT NULL,
	[main_events] [nvarchar](4000) NULL,
 CONSTRAINT [PK_COMMENTS] PRIMARY KEY CLUSTERED 
(
	[comment_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Daily_GI_Wells]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Daily_GI_Wells](
	[giwell_Id] [int] IDENTITY(1,1) NOT NULL,
	[dailyreport_Id] [int] NOT NULL,
	[well_name] [nvarchar](25) NOT NULL,
	[online_time] [smallint] NULL,
	[choke_opening] [decimal](6, 2) NULL,
	[WHP] [smallint] NULL,
	[WHT] [smallint] NULL,
	[BHP] [smallint] NULL,
	[BHT] [smallint] NULL,
	[gas_injection_allocated] [decimal](14, 2) NULL,
	[gas_injection_target] [decimal](14, 2) NULL,
	[gas_injection_measured] [decimal](14, 2) NULL,
 CONSTRAINT [PK_WELLSTEST] PRIMARY KEY CLUSTERED 
(
	[giwell_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Daily_Production_Wells]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Daily_Production_Wells](
	[prodwell_Id] [int] IDENTITY(1,1) NOT NULL,
	[dailyreport_Id] [int] NOT NULL,
	[well_name] [nvarchar](25) NOT NULL,
	[well_trunkline] [nvarchar](25) NULL,
	[online_time] [smallint] NULL,
	[choke_opening] [decimal](6, 2) NULL,
	[WHP] [smallint] NULL,
	[WHT] [smallint] NULL,
	[BHP] [smallint] NULL,
	[BHT] [smallint] NULL,
	[oil_prod_allocated] [decimal](14, 2) NULL,
	[oil_prod_target] [decimal](14, 2) NULL,
	[gas_prod_allocated] [decimal](14, 2) NULL,
	[condensate_prod_allocated] [decimal](14, 2) NULL,
	[LPG_prod_allocated] [decimal](14, 2) NULL,
	[water_production_allocated] [int] NULL,
	[GOR] [decimal](6, 2) NULL,
	[water_cut] [decimal](6, 2) NULL,
	[gas_lift] [smallint] NULL,
	[annulus_pressure] [smallint] NULL,
	[zone] [smallint] NULL,
 CONSTRAINT [PK_WELLSTESTID] PRIMARY KEY CLUSTERED 
(
	[prodwell_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Daily_WI_Wells]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Daily_WI_Wells](
	[wiwell_Id] [int] IDENTITY(1,1) NOT NULL,
	[dailyreport_Id] [int] NOT NULL,
	[well_name] [nvarchar](25) NOT NULL,
	[online_time] [smallint] NULL,
	[choke_opening] [decimal](6, 2) NULL,
	[WHP] [smallint] NULL,
	[WHT] [smallint] NULL,
	[BHP] [smallint] NULL,
	[BHP_limit] [smallint] NULL,
	[BHT] [smallint] NULL,
	[water_injection_allocated] [decimal](14, 2) NULL,
	[water_injection_target] [decimal](14, 2) NULL,
	[water_injection_measured] [decimal](14, 2) NULL,
 CONSTRAINT [PK_WIWELLID] PRIMARY KEY CLUSTERED 
(
	[wiwell_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[DailyReportsTotal]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[DailyReportsTotal](
	[dailyreport_Id] [int] IDENTITY(1,1) NOT NULL,
	[asset_Id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[facility_name] [varchar](50) NULL,
	[oil_prod_allocated] [decimal](14, 2) NULL,
	[oil_prod_target] [decimal](14, 2) NULL,
	[oil_prod_MTD] [decimal](14, 2) NULL,
	[oil_prod_YTD] [decimal](14, 2) NULL,
	[condensate_prod_allocated] [decimal](14, 2) NULL,
	[condensate_export_allocated] [decimal](14, 2) NULL,
	[condensate_stock_initial] [decimal](14, 2) NULL,
	[condensate_stock_final] [decimal](14, 2) NULL,
	[LPG_prod_allocated] [decimal](14, 2) NULL,
	[LPG_export_allocated] [decimal](14, 2) NULL,
	[LPG_stock_initial] [decimal](14, 2) NULL,
	[LPG_stock_final] [decimal](14, 2) NULL,
	[LPG_spiking] [decimal](14, 2) NULL,
	[gas_prod_allocated] [decimal](14, 2) NULL,
	[gas_prod_target] [decimal](14, 2) NULL,
	[gas_prod_MTD] [decimal](14, 2) NULL,
	[gas_prod_YTD] [decimal](14, 2) NULL,
	[gas_export_allocated] [decimal](14, 2) NULL,
	[gas_export_target] [decimal](14, 2) NULL,
	[gas_export_MTD] [decimal](14, 2) NULL,
	[gas_export_YTD] [decimal](14, 2) NULL,
	[gas_import_allocated] [decimal](14, 2) NULL,
	[gas_import_MTD] [decimal](14, 2) NULL,
	[gas_inj_allocated] [decimal](14, 2) NULL,
	[gas_inj_target] [decimal](14, 2) NULL,
	[gas_inj_MTD] [decimal](14, 2) NULL,
	[gas_inj_YTD] [decimal](14, 2) NULL,
	[water_prod_allocated] [decimal](14, 2) NULL,
	[water_prod_target] [decimal](14, 2) NULL,
	[water_prod_MTD] [decimal](14, 2) NULL,
	[water_prod_YTD] [decimal](14, 2) NULL,
	[water_inj_allocated] [decimal](14, 2) NULL,
	[water_inj_produced] [decimal](14, 2) NULL,
	[water_inj_target] [decimal](14, 2) NULL,
	[water_inj_MTD] [decimal](14, 2) NULL,
	[water_inj_YTD] [decimal](14, 2) NULL,
	[gas_lift_allocated] [decimal](14, 2) NULL,
	[oil_loss_allocated] [decimal](14, 2) NULL,
	[flare_gas_allocated] [decimal](14, 2) NULL,
	[fuel_gas_allocated] [decimal](14, 2) NULL,
	[CO2_extracted] [decimal](14, 2) NULL,
	[water_discharged] [decimal](14, 2) NULL,
	[BS_W] [decimal](14, 2) NULL,
 CONSTRAINT [PK_DAILYREPORT] PRIMARY KEY CLUSTERED 
(
	[dailyreport_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Units]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Units](
	[unit_Id] [int] NOT NULL,
	[asset_Id] [int] NOT NULL,
	[oil_unit] [nvarchar](10) NULL,
	[gas_unit] [nvarchar](10) NULL,
	[water_inj_unit] [nvarchar](10) NULL,
	[condensate_unit] [nvarchar](10) NULL,
	[lpg_unit] [nvarchar](10) NULL,
	[co2_extracted_unit] [nvarchar](10) NULL,
	[bs_w_unit] [nvarchar](10) NULL,
	[online_time_unit] [nvarchar](10) NULL,
	[choke_opening_unit] [nvarchar](10) NULL,
	[whp_unit] [nvarchar](10) NULL,
	[wht_unit] [nvarchar](10) NULL,
	[bhp_unit] [nvarchar](10) NULL,
	[bht_unit] [nvarchar](10) NULL,
	[gor_unit] [nvarchar](10) NULL,
	[wct_unit] [nvarchar](10) NULL,
	[annulus_pressure_unit] [nvarchar](10) NULL,
	[separator_pressure_unit] [nvarchar](10) NULL,
	[sand_prod_unit] [nvarchar](10) NULL,
	[glr_unit] [nvarchar](10) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[unit_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PC269].[Well_Tests]    Script Date: 6/30/2020 12:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PC269].[Well_Tests](
	[welltest_Id] [int] IDENTITY(1,1) NOT NULL,
	[dailyreport_Id] [int] NOT NULL,
	[well_name] [nvarchar](25) NOT NULL,
	[test_date] [date] NULL,
	[online_time] [smallint] NULL,
	[choke_opening] [decimal](6, 2) NULL,
	[oil_prod_rate] [decimal](6, 2) NULL,
	[gas_prod_rate] [decimal](6, 2) NULL,
	[water_prod_rate] [decimal](6, 2) NULL,
	[BHP] [smallint] NULL,
	[BHT] [smallint] NULL,
	[WHP] [smallint] NULL,
	[WHT] [smallint] NULL,
	[separator_pressure] [decimal](14, 2) NULL,
	[BSW] [decimal](6, 2) NULL,
	[GOR] [decimal](6, 2) NULL,
	[sand_prod] [decimal](6, 2) NULL,
	[GLR] [decimal](6, 2) NULL,
 CONSTRAINT [PK_WELLSTESTID1] PRIMARY KEY CLUSTERED 
(
	[welltest_Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER DATABASE [pc0269v2] SET  READ_WRITE 
GO
