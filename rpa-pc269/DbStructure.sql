DROP TABLE [WellsTests]
GO

DROP TABLE [WaterInjectionWells]
GO

DROP TABLE [GasInjectionWells]
GO

DROP TABLE [Comments]
GO

DROP TABLE [DailyReports]
GO

DROP TABLE [Assets]
GO

CREATE TABLE [Assets] (
	asset_Id int IDENTITY(1,1) NOT NULL,
	country nvarchar(40) NOT NULL,
	asset_name nvarchar(50) NOT NULL UNIQUE,
	inst_type nvarchar(25) NOT NULL,
	verifier varchar(40) NOT NULL,
	abbyy_batch varchar(40) NOT NULL,
	prod_oil_default_unit varchar(10),
    prod_gas_default_unit varchar(10), 
    prod_water_default_unit varchar(10),
    injection_water_unit_default_unit varchar(10),
    injection_gas_unit_default_unit varchar(10),
    fuel_gas_default_unit varchar(10),
    flare_gas_default_unit varchar(10)
  CONSTRAINT [PK_ASSETS] PRIMARY KEY CLUSTERED
  (
  [asset_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [DailyReports] (
	dailyreport_Id bigint IDENTITY(1,1) NOT NULL,
	asset_Id int NOT NULL,
	prod_oil int NOT NULL,
	prod_oil_unit varchar(10),
	prod_gas int NOT NULL,
	prod_gas_unit varchar(10),
	prod_water int NOT NULL,
	prod_water_unit varchar(10),
	injection_water int,
	injection_water_unit varchar(10),
	injection_gas int,
	injection_gas_unit varchar(10),
	fuel_gas int,
	fuel_gas_unit varchar(10),
	flare_gas int,
	flare_gas_unit varchar(10),
	production_date date NOT NULL,
	production_report_uri varchar(255) NOT NULL
  CONSTRAINT [PK_DAILYREPORT] PRIMARY KEY CLUSTERED
  (
  [dailyreport_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [WellsTests] (
	welltest_Id bigint IDENTITY(1,1) NOT NULL,
	dailyreport_Id bigint NOT NULL,
	well_name nvarchar(25) NOT NULL,
	last_testdate date NOT NULL,
	hours_test smallint NOT NULL,
	oil_rate int,
	wellhead_press int,
	wellhead_temp smallint,
	choke_open decimal,
	test_sep smallint,
	water_rate int,
	bsw decimal,
	gas_rate int,
	gor int,
	glr int,
  CONSTRAINT [PK_WELLSTEST] PRIMARY KEY CLUSTERED
  (
  [welltest_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [WaterInjectionWells] (
	waterinjection_Id bigint IDENTITY(1,1) NOT NULL,
	dailyreport_Id bigint NOT NULL,
	well_name nvarchar(25) NOT NULL,
	prod_date date NOT NULL,
	hours_on smallint,
	choke_open decimal,
	wellhead_press int,
	fl int,
	wellhead_temp smallint,
	metered_vol int,
	alloc_volume int,
	month_cumulative int,
	year_cumulative bigint,
  CONSTRAINT [PK_WATERINJECTIONWELLS] PRIMARY KEY CLUSTERED
  (
  [waterinjection_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [GasInjectionWells] (
	gasinjection_Id bigint IDENTITY(1,1) NOT NULL,
	dailyreport_Id bigint NOT NULL,
	well_name nvarchar(25) NOT NULL,
	prod_date date NOT NULL,
	hours_on smallint,
	choke_open decimal,
	wellhead_press int,
	fl int,
	wellhead_temp smallint,
	metered_vol int,
	alloc_volume int,
	month_cumulative int,
	year_cumulative bigint,
  CONSTRAINT [PK_GASINJECTIONWELLS] PRIMARY KEY CLUSTERED
  (
  [gasinjection_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Comments] (
	comment_Id bigint IDENTITY(1,1) NOT NULL,
	dailyreport_Id bigint NOT NULL,
	comment nvarchar(4000),
  CONSTRAINT [PK_COMMENTS] PRIMARY KEY CLUSTERED
  (
  [comment_Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

