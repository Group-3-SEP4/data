--********************************************************************************************
--INITIAL LOAD
--********************************************************************************************

--********************************************************************************************
--EXTRACT DEVICEDIM
--********************************************************************************************

--Gets all devices from the room 
INSERT INTO Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name)
SELECT
    distinct r.deviceEUI,
             r.roomId,
             r.name
FROM EnviormentDatabase.dbo.Room r

--Get all devices from the measurement that do not have rooms initialized yet
INSERT INTO Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name)
Select
    distinct m.deviceEUI,
             NULL,
             NULL
from EnviormentDatabase.dbo.Measurement m
where m.DeviceEUI not in (
    SELECT
        distinct r.deviceEUI
    FROM Staging.DeviceDim r)

--Replace nulls 
UPDATE Staging.DeviceDim
SET Name = 'No name'
WHERE Name IS NULL

UPDATE Staging.DeviceDim
SET RoomID = -1
WHERE RoomID IS NULL

--Load DeviceDim
INSERT INTO DW.DeviceDim(
    DeviceEUI,
    RoomID,
    Name,
    ValidFrom,
    ValidTo
)
SELECT
    stage.DeviceEUI,
    stage.RoomID,
    stage.Name,
    GETDATE(),
    '9999-12-31'
FROM Staging.DeviceDim stage

--********************************************************************************************
--Extract DateDim
--********************************************************************************************

SET NOCOUNT ON
DECLARE @StartDate DATETIME = '2020-11-10'
DECLARE @EndDate DATETIME = FORMAT(GETDATE(), 'yyyy-MM-dd')
WHILE @StartDate <= @EndDate
    BEGIN
        INSERT INTO DW.DateDim (
            Date,
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

delete from Staging.DeviceDim

--********************************************************************************************
-- Extract TimeDim
--********************************************************************************************

DECLARE @Time as time;
SET @Time = '0:00';

DECLARE @counter as int;
SET @counter = 0;

WHILE @counter < 1440
    BEGIN
        INSERT INTO DW.TimeDim(
            Time,
            Hour,
            Minute)
        VALUES (
                   @Time,
                   DATEPART(Hour, @Time) + 1,
                   DATEPART(Minute, @Time) + 1)
        SET @Time = DATEADD(minute, 1, @Time);
        SET @counter = @counter + 1;
    END
SET NOCOUNT OFF

--********************************************************************************************
--Measurement fact
--********************************************************************************************

--Extract Measurement fact
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
    FORMAT(m.timestamp, 'HH:mm'),
    m.timestamp,
    m.humidityPercentage,
    m.carbonDioxide,
    m.temperature,
    m.servoPositionPercentage
FROM dbo.Measurement m

--Transform Measurement fact
UPDATE Staging.F_Measurement
SET Temperature = 0
WHERE Temperature = 32767

UPDATE Staging.F_Measurement
SET CarbonDioxide = 0
WHERE CarbonDioxide = 32767 OR CarbonDioxide < 0

UPDATE Staging.F_Measurement
SET HumidityPercentage = 0
WHERE HumidityPercentage > 100 OR HumidityPercentage < 0

UPDATE Staging.F_Measurement
SET ServoPosition = 0
WHERE ServoPosition > 100 OR ServoPosition < 0

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

delete from Staging.F_Measurement

--********************************************************************************************
--Update history of last update
--********************************************************************************************
INSERT INTO DW.LastUpdated
(
    Date
)
VALUES
(
    GETDATE()
);