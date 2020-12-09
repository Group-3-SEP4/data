--Clear the temporary table before loading new data
DELETE FROM EnviormentDatabase.Staging.F_Measurement

--Insert new data that arrived since the last data ware house update
INSERT INTO EnviormentDatabase.Staging.F_Measurement(
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
where m.timestamp > (SELECT TOP (1) DW.LastUpdated.Date
                     FROM  DW.LastUpdated
                     ORDER BY Date DESC);

--Look up keys in DW
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

-- Populate the dw fact table using the temporary table
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

-- Update "LastUpdated" table with new date

INSERT INTO DW.LastUpdated
(
    Date
)
VALUES
(
    GETDATE()
);

--GET SOME DATES 
SET NOCOUNT ON
DECLARE @StartDate DATETIME = '2020-12-9'
DECLARE @EndDate DATETIME = '2020-12-09'
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