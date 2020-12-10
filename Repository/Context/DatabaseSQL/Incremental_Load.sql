--********************************************************************************************
--UPDATE DELETED ENTITIES
--********************************************************************************************
update DW.DeviceDim
set ValidTo = GETDATE()
where DW.DeviceDim.DeviceDimKey IN(
    select DeviceDimKey
    from DW.DeviceDim
             inner join dbo.Room on DW.DeviceDim.RoomID != dbo.Room.roomId
    where dbo.Room.name = DW.DeviceDim.Name AND ValidTo = '9999-12-31' OR dbo.Room.deviceEUI = DW.DeviceDim.DeviceEUI AND ValidTo ='9999-12-31'
)

--********************************************************************************************
--AN ENTITY THAT CHANGED SINCE LAST TIME
--********************************************************************************************
--Changes in existing entity detection. I assume here that only name or deviceEUI can change
insert into Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name,
    ValidFrom,
    ValidTo
)
select Room.deviceEUI, Room.roomId, Room.name, GETDATE(), '9999-12-31'
from dbo.Room
         inner join DW.DeviceDim on dbo.Room.roomId = DW.DeviceDim.RoomID
where dbo.Room.deviceEUI != DW.DeviceDim.DeviceEUI AND ValidTo > GETDATE() OR dbo.Room.name != DW.DeviceDim.Name AND ValidTo > GETDATE()

--must differenciate between old enities that have valid to no 9999

--********************************************************************************************
--UPDATE VALID TO DATE OF OLD ENTITIES
--********************************************************************************************
update DW.DeviceDim
set ValidTo = GETDATE()
where DW.DeviceDim.RoomID IN(
    select dbo.Room.roomId
    from dbo.Room
             inner join DW.DeviceDim on dbo.Room.roomId = DW.DeviceDim.RoomID
    where dbo.Room.name != DW.DeviceDim.Name AND ValidTo > GETDATE() OR dbo.Room.deviceEUI != DW.DeviceDim.DeviceEUI AND ValidTo > GETDATE()
)
--must differenciate between old enities that have valid to no 9999

--********************************************************************************************
--INSERTING THE UPDATED OR NEW ENTITIES ADDED ENTITIES INTO THE DATA WAREHOUSE
--********************************************************************************************
INSERT INTO DW.DeviceDim
(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
SELECT
    stage.DeviceEUI,
    stage.RoomID,
    stage.Name,
    stage.ValidFrom,
    stage.ValidTo
FROM Staging.DeviceDim stage;

--Clears temporary table. Important
delete from Staging.DeviceDim

--********************************************************************************************
--NEW ENTIY IN THE SOURCE DATABASE
--********************************************************************************************
delete from Staging.DeviceDim
--New entity detection
insert into Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name,
    ValidFrom,
    ValidTo
)
select Room.deviceEUI, Room.roomId, Room.name, GETDATE(), '9999-12-31'
from dbo.Room
where (
                  roomId IN (((select roomId from dbo.Room )
                              EXCEPT
                              (select DW.DeviceDim.RoomID from DW.DeviceDim ))
                             EXCEPT select DW.DeviceDim.RoomID from DW.DeviceDim where ValidTo <= GETDATE())
              OR
                  name in (((select name from dbo.Room)
                            EXCEPT
                            (select DW.DeviceDim.Name from DW.DeviceDim))
                           EXCEPT select DW.DeviceDim.Name from DW.DeviceDim where ValidTo <= GETDATE())
              OR
                  deviceEUI in (((select deviceEUI from dbo.Room)
                                 EXCEPT
                                 (select DW.DeviceDim.deviceEUI from DW.DeviceDim))
                                EXCEPT select DW.DeviceDim.DeviceEUI from DW.DeviceDim where ValidTo <= GETDATE())
          )
--must differenciate between old enities that have valid to no 9999

--********************************************************************************************
--INSERTING THE UPDATED OR NEW ENTITIES ADDED ENTITIES INTO THE DATA WAREHOUSE
--********************************************************************************************
INSERT INTO DW.DeviceDim
(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
SELECT
    stage.DeviceEUI,
    stage.RoomID,
    stage.Name,
    stage.ValidFrom,
    stage.ValidTo
FROM Staging.DeviceDim stage;

--Clears temporary table. Important
delete from Staging.DeviceDim

--********************************************************************************************
--Initial load for the fact table
--********************************************************************************************
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
FROM EnviormentDatabase.dbo.Measurement m
where m.timestamp > (SELECT TOP (1) DW.LastUpdated.Date
                     FROM  DW.LastUpdated
                     ORDER BY Date DESC);

--GET SOME DATES 
SET NOCOUNT ON
DECLARE @StartDate DATETIME = (SELECT TOP (1) DW.LastUpdated.Date FROM  DW.LastUpdated ORDER BY Date DESC);
DECLARE @EndDate DATETIME = FORMAT(GETDATE(), 'yyyy-MM-dd')
select @EndDate = dateadd(day,1,@enddate)
WHILE @StartDate < @EndDate
    BEGIN
        INSERT INTO DW.DateDim (
            Date,
            DayOfWeek,
            DayOfWeekName,
            Year,
            MonthName,
            WeekNumber
        )
        SELECT
            Date = @StartDate,
            DayOfWeek = DATEPART(WEEKDAY, @StartDate),
            DayOfWeekName = DATENAME(WEEKDAY, @StartDate),
            Year = YEAR(@StartDate),
            MonthName = DATENAME(month, @StartDate),
            WeekNumber = DATEPART(week, @StartDate)

        SET @StartDate = DATEADD(dd, 1, @StartDate)
    END


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

