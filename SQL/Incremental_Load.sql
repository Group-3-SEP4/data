--********************************************************************************************
--UPDATE DELETED ENTITIES
--********************************************************************************************
delete from Staging.DeviceDim

update DW.DeviceDim
set ValidTo = GETDATE()
where DW.DeviceDim.DeviceDimKey IN(
    select DeviceDimKey
    from DW.DeviceDim
             inner join dbo.Room on DW.DeviceDim.RoomID != dbo.Room.roomId
    where dbo.Room.name = DW.DeviceDim.Name AND ValidTo = '9999-12-31' AND DW.DeviceDim.DeviceEUI not in(select distinct m.deviceEUI from dbo.Measurement m)
       OR dbo.Room.deviceEUI = DW.DeviceDim.DeviceEUI AND ValidTo ='9999-12-31' AND DW.DeviceDim.DeviceEUI not in(select distinct m.deviceEUI from dbo.Measurement m)
       OR dbo.Room.RoomId = DW.DeviceDim.RoomId AND ValidTo ='9999-12-31' AND DW.DeviceDim.DeviceEUI not in(select distinct m.deviceEUI from dbo.Measurement m)
)

--********************************************************************************************
--AN ENTITY THAT CHANGED SINCE LAST TIME
--********************************************************************************************

insert into Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name)
select Room.deviceEUI, Room.roomId, Room.name
from dbo.Room
         inner join DW.DeviceDim on dbo.Room.roomId = DW.DeviceDim.RoomID
where dbo.Room.deviceEUI != DW.DeviceDim.DeviceEUI AND ValidTo > GETDATE()
   OR dbo.Room.name != DW.DeviceDim.Name AND ValidTo > GETDATE()
   OR dbo.Room.RoomId != DW.DeviceDim.RoomId AND ValidTo > GETDATE()

update DW.DeviceDim
set ValidTo = GETDATE()
where DW.DeviceDim.RoomID IN(
    select dbo.Room.roomId
    from dbo.Room
             inner join DW.DeviceDim on dbo.Room.roomId = DW.DeviceDim.RoomID
    where dbo.Room.name != DW.DeviceDim.Name AND ValidTo > GETDATE()
       OR dbo.Room.deviceEUI != DW.DeviceDim.DeviceEUI AND ValidTo > GETDATE()
       OR dbo.Room.deviceEUI != DW.DeviceDim.DeviceEUI AND ValidTo > GETDATE()
)

UPDATE Staging.DeviceDim
SET Name = 'No name'
WHERE Name IS NULL

UPDATE Staging.DeviceDim
SET RoomID = -1
WHERE RoomID IS NULL

INSERT INTO DW.DeviceDim
(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
SELECT
    stage.DeviceEUI,
    stage.RoomID,
    stage.Name,
    GETDATE(),
    '9999-12-31'
FROM Staging.DeviceDim stage;

--Clears temporary table. Important
delete from Staging.DeviceDim

--********************************************************************************************
--NEW ENTIY IN THE SOURCE DATABASE
--********************************************************************************************

delete from Staging.DeviceDim

insert into Staging.DeviceDim(
    DeviceEUI,
    RoomID,
    Name
)
select Room.deviceEUI, Room.roomId, Room.name
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

--Finds new unique device id's that are not in the data warehous and not added to staging in previous step
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
    FROM Staging.DeviceDim r) AND m.DeviceEUI not in (
    select
        DW.DeviceDim.deviceEUI
    from DW.DeviceDim)

UPDATE Staging.DeviceDim
SET Name = 'No name'
WHERE Name IS NULL

UPDATE Staging.DeviceDim
SET RoomID = -1
WHERE RoomID IS NULL

INSERT INTO DW.DeviceDim
(DeviceEUI, RoomID, Name, ValidFrom, ValidTo)
SELECT
    stage.DeviceEUI,
    stage.RoomID,
    stage.Name,
    GETDATE(),
    '9999-12-31'
FROM Staging.DeviceDim stage;

--Clears temporary table. Important
delete from Staging.DeviceDim

--********************************************************************************************
--Generate all new dates sine last update to insert into fact table
--********************************************************************************************
SET NOCOUNT ON
DECLARE @StartDate DATETIME = FORMAT((SELECT TOP (1) DW.LastUpdated.Date FROM  DW.LastUpdated ORDER BY Date DESC), 'yyyy-MM-dd');
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
        where @StartDate not in (select DateDim.Date from DW.DateDim)
        SET @StartDate = DATEADD(dd, 1, @StartDate)
    END

--********************************************************************************************
--Incremental load for the fact table
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
    FORMAT(m.timestamp, 'HH:mm'),
    m.timestamp,
    m.humidityPercentage,
    m.carbonDioxide,
    m.temperature,
    m.servoPositionPercentage
FROM EnviormentDatabase.dbo.Measurement m
where m.timestamp > (SELECT TOP (1) DW.LastUpdated.Date
                     FROM  DW.LastUpdated
                     ORDER BY Date DESC);

--Cleansing the data of illegal values
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
    WHERE d.DeviceEUI = Staging.F_Measurement.DeviceEUI AND d.ValidTo = '9999-12-31')

UPDATE Staging.F_Measurement
SET DateDimKey = (
    SELECT top(1) d.DateDimKey
    FROM DW.DateDim d
    WHERE d.Date = F_Measurement.Date
)

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

