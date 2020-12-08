drop table if exists Staging.DeviceDim
drop table if exists Staging.F_Measurement
drop table if exists DW.F_Measurement
drop table if exists DW.DeviceDim
drop table if exists DW.DateDim
drop table if exists DW.TimeDim
drop table if exists DW.LastUpdated

--**************************************************************************
--STAGING TABLES
--**************************************************************************
CREATE TABLE Staging.DeviceDim
(
    DeviceEUI            nvarchar(16) NULL,
	RoomID				 int NULL,
    Name                 nvarchar(50) NULL,
    ValidFrom            Date NULL,
    ValidTo				 Date NULL,
);

CREATE TABLE Staging.F_Measurement
(
	DeviceDimKey         INT NULL,
    TimeDimKey           INT NULL,
    DateDimKey           INT NULL,
    DeviceEUI			 nvarchar(16) NULL,
    Time				 time(0) NULL,
    Date				 date NULL,
    HumidityPercentage   INT NULL,
    CarbonDioxide		 INT NULL,
	Temperature          float NULL,
	ServoPosition        INT NULL,
);

--**************************************************************************
--DATA WAREHOUSE TABLES
--**************************************************************************
CREATE TABLE DW.DeviceDim
(
	DeviceDimKey		 INT IDENTITY(1,1) NOT NULL,
    DeviceEUI            nvarchar(16)  NOT NULL,
	RoomID				 int NOT NULL,
    Name                 varchar(50) NOT NULL,
    ValidFrom            Date NOT NULL,
    ValidTo				 Date NOT NULL,
	PRIMARY KEY (DeviceDimKey)
);

CREATE TABLE DW.DateDim
(
    DateDimKey        INT IDENTITY(1,1) NOT NULL,
    Date              date NOT NULL,
    DayOfWeek         int NOT NULL,
    DayOfWeekName     nvarchar(9) NOT NULL,
	Year              INT NOT NULL,
	MonthName         nvarchar(9) NOT NULL,
	WeekNumber        int NOT NULL,
	PRIMARY KEY (DateDimKey)
);

CREATE TABLE DW.TimeDim
(
    TimeDimKey        INT IDENTITY(1,1) NOT NULL,
	Time			  time(0) NOT NULL,
    Hour			  INT NOT NULL,
	Minute            INT NOT NULL,
	PRIMARY KEY (TimeDimKey)
);


CREATE TABLE DW.F_Measurement
(
	MeasurementID		 INT IDENTITY(1,1) NOT NULL,
    DeviceDimKey         INT NOT NULL,
    TimeDimKey           INT NOT NULL,
    DateDimKey           INT NOT NULL,
    HumidityPercentage   INT NOT NULL,
    CarbonDioxide		 INT NOT NULL,
	Temperature          float NOT NULL,
	ServoPosition        INT NOT NULL,
	CONSTRAINT PK_Measurement PRIMARY KEY (DeviceDimKey, TimeDimKey, DateDimKey, MeasurementID),
	FOREIGN KEY (DeviceDimKey) REFERENCES DW.DeviceDim(DeviceDimKey),
    FOREIGN KEY (DateDimKey) REFERENCES DW.DateDim(DateDimKey),
    FOREIGN KEY (TimeDimKey) REFERENCES DW.TimeDim(TimeDimKey)
);

CREATE TABLE DW.LastUpdated (
	Date datetime not null default ((0))
	)