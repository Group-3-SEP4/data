/*--------------INITIAL LOAD--------------*/

-- Extract DeviceDim
INSERT INTO Staging.DeviceDim
				(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
				SELECT distinct m.deviceEUI, r.roomId, r.name, '2020-11-01', '9999-12-31'
				FROM EnviormentDatabase.dbo.Measurement m 
				inner join EnviormentDatabase.dbo.Room r on m.deviceEUI = r.deviceEUI;

-- Load DeviceDim
INSERT INTO DW.DeviceDim
				(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
				SELECT 
					stage.DeviceEUI,
					stage.RoomID,
					stage.Name,
					stage.ValidFrom,
					stage.ValidTo
				FROM Staging.DeviceDim stage



-- Extract DateDim
SET NOCOUNT ON
DECLARE @StartDate DATETIME = '2020-11-10'
DECLARE @EndDate DATETIME = '2020-12-08'
WHILE @StartDate <= @EndDate
BEGIN
INSERT INTO DW.DateDim (Date,
						DayOfWeek,
						DayOfWeekName,
						Year,
						MonthName,
						WeekNumber)
SELECT 

Date = @StartDate,
DayOfWeek = DATEPART(WEEKDAY, @StartDate),
DayOfWeekName = DATENAME(WEEKDAY, @StartDate),
Year = YEAR(@StartDate),
MonthName = DATENAME(month, @StartDate),
WeekNumber = DATEPART(week, @StartDate)

SET @StartDate = DATEADD(dd, 1, @StartDate)
END


-- Extract TimeDim
DECLARE @Time as time;
SET @Time = '0:00';
 
DECLARE @counter as int;
SET @counter = 0;

WHILE @counter < 1440
BEGIN
INSERT INTO DW.TimeDim(Time, Hour, Minute)
VALUES (@Time, DATEPART(Hour, @Time) + 1, DATEPART(Minute, @Time) + 1)
SET @Time = DATEADD(minute, 1, @Time);
SET @counter = @counter + 1;
END
SET NOCOUNT OFF

-- Extract Measurement fact
INSERT INTO Staging.F_Measurement
(
	DeviceEUI,
    Time,
    Date,
    HumidityPercentage,
    CarbonDioxide,
	Temperature,
	ServoPosition
)
SELECT
	m.deviceEUI,
	FORMAT(m.timestamp, 'hh:mm'),
	m.timestamp,
	m.humidityPercentage,
	m.carbonDioxide,
	m.temperature,
	m.servoPositionPercentage
FROM dbo.Measurement m

-- Transform Measurement fact

/*
UPDATE Staging.F_Measurement
SET Temperature = 32767
WHERE Temperature is null

UPDATE Staging.F_Measurement
SET Temperature = 0
WHERE Temperature = 32767
*/

-- Extract Dimension keys
UPDATE Staging.F_Measurement
SET DeviceDimKey = (
		SELECT d.DeviceDimKey
		FROM DW.DeviceDim d
		WHERE d.DeviceEUI = F_Measurement.DeviceEUI)

UPDATE Staging.F_Measurement
SET DateDimKey = (
		SELECT d.DateDimKey
		FROM DW.DateDim d
		WHERE d.Date = F_Measurement.Date)

UPDATE Staging.F_Measurement
SET TimeDimKey = (
		SELECT t.TimeDimKey
		FROM DW.TimeDim t
		WHERE t.Time = F_Measurement.Time)

-- Load Measurement fact
INSERT INTO DW.F_Measurement
(
	DeviceDimKey,
    TimeDimKey,
    DateDimKey,
    HumidityPercentage,
    CarbonDioxide,
	Temperature,
	ServoPosition
)
SELECT
	stage.DeviceDimKey,
	stage.TimeDimKey,
	stage.DateDimKey,
	stage.HumidityPercentage,
	stage.CarbonDioxide,
	stage.Temperature,
	stage.ServoPosition
FROM Staging.F_Measurement stage


-- Set last updated
INSERT INTO DW.LastUpdated 
	(
	Date
	)
	VALUES
	(
	'2020-12-08'
	);