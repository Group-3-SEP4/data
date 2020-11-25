--**************************************************************************
--SOURCE DATABASE
--**************************************************************************
USE [EnviormentDatabase]
GO
USE [OP_EnviornmentMonitoring]
GO

DROP TABLE IF EXISTS [EnviormentDatabase].dbo.Measurement
CREATE TABLE [dbo].[Measurement](
	[measurementId]				[int]		IDENTITY(1,1) NOT NULL,
	[timestamp]					[datetime]	NOT NULL,
	[humidityPercentage]		[int]		NOT NULL,
	[carbonDioxide]				[int]		NOT NULL,
	[temperature]				[float]		NOT NULL,
	[servoPositionPercentage]	[int]		NOT NULL,
	[deviceId]					[nvarchar](16)		NOT NULL,
	CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED ([measurementId] ASC),
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [EnviormentDatabase].dbo.Settings
CREATE TABLE [dbo].[Settings](
	[settingsId] [int] IDENTITY(1,1) NOT NULL,
	[lastUpdated] datetime NOT NULL,
	[temperatureSetpoint] float NOT NULL,
	[ppmMin] int NOT NULL,
	[ppmMax] int NOT NULL,
	CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([settingsId] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [EnviormentDatabase].dbo.Room
CREATE TABLE [dbo].[Room](
	[roomId] [int] IDENTITY(1,1) NOT NULL,
	[settingsId] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
    [deviceEUI] [nvarchar](16) NOT NULL,
	CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([roomId] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD CONSTRAINT [FK_Room_Settings] FOREIGN KEY ([settingsId])
REFERENCES [dbo].[Settings] ([settingsId])
GO

ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Settings]
GO

insert into dbo.Measurement (timestamp, humidityPercentage, carbonDioxide, temperature, servoPositionPercentage, deviceId)
values ('2020-08-23', 52, 534, 18, 0, 0)

/*
DROP TABLE IF EXISTS [EnviormentDatabase].dbo.SystemUser
CREATE TABLE [dbo].[SystemUser](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[settingsId] [int] NOT NULL,
	[username] [nvarchar](25) NOT NULL,
	[password] [nvarchar](25) NOT NULL,
	[email] [nvarchar](25) NOT NULL,
	[firstName] [nvarchar](25) NOT NULL,
	[lastName] [nvarchar](25) NOT NULL,
	CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED ([userId] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SystemUser]  WITH CHECK ADD CONSTRAINT [FK_User_Settings] FOREIGN KEY ([settingsId])
REFERENCES [dbo].[Settings] ([settingsId])
GO

ALTER TABLE [dbo].[SystemUser] CHECK CONSTRAINT [FK_User_Settings]
GO
*/

--**************************************************************************
--STAGING DATABASE
--**************************************************************************





--**************************************************************************
--DATA WAREHOUSE
--**************************************************************************