DROP DATABASE IF EXISTS OP_EnviormentMonitoring;
CREATE DATABASE OP_EnviormentMonitoring
GO

USE [OP_EnviormentMonitoring]
GO

DROP TABLE IF EXISTS [OP_EnviormentMonitoring].dbo.TemperatureReading
CREATE TABLE [dbo].[TemperatureReading](
	[tempr_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NOT NULL,
	[timestamp] [datetime] NOT NULL,
	[value] [float] NOT NULL,
	CONSTRAINT [PK_TemperatureReading] PRIMARY KEY CLUSTERED ([tempr_id] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [OP_EnviormentMonitoring].dbo.CarbonDioxideReading
CREATE TABLE [dbo].[CarbonDioxideReading](
	[carbr_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NOT NULL,
	[timestamp] int NOT NULL,
	[value] int NOT NULL,
	CONSTRAINT [PK_CarbonDioxideReading] PRIMARY KEY CLUSTERED ([carbr_id] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [OP_EnviormentMonitoring].dbo.HumidityReading
CREATE TABLE [dbo].[HumidityReading](
	[humr_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NOT NULL,
	[timestamp] int NOT NULL,
	[value] int NOT NULL,
	CONSTRAINT [PK_HumidityReading] PRIMARY KEY CLUSTERED ([humr_id] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [OP_EnviormentMonitoring].dbo.Room
CREATE TABLE [dbo].[Room](
	[room_id] [int] IDENTITY(1,1) NOT NULL,
	[settings_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([room_id] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [OP_EnviormentMonitoring].dbo.Settings
CREATE TABLE [dbo].[Settings](
	[settings_id] [int] IDENTITY(1,1) NOT NULL,
	[lastUpdated] datetime NOT NULL,
	[temperatureSetpoint] float NOT NULL,
	[ppmMin] int NOT NULL,
	[ppmMax] int NOT NULL,
	CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([settings_id] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TemperatureReading]  WITH CHECK ADD CONSTRAINT [FK_temperature_room] FOREIGN KEY ([room_id])
REFERENCES [dbo].[Room] ([room_id])
GO

ALTER TABLE [dbo].[TemperatureReading] CHECK CONSTRAINT [FK_temperature_room]
GO

ALTER TABLE [dbo].[CarbonDioxideReading]  WITH CHECK ADD CONSTRAINT [FK_carbondDioxide_room] FOREIGN KEY ([room_id])
REFERENCES [dbo].[Room] ([room_id])
GO

ALTER TABLE [dbo].[CarbonDioxideReading] CHECK CONSTRAINT [FK_carbondDioxide_room]
GO

ALTER TABLE [dbo].[HumidityReading]  WITH CHECK ADD CONSTRAINT [FK_humidity_room] FOREIGN KEY ([room_id])
REFERENCES [dbo].[Room] ([room_id])
GO

ALTER TABLE [dbo].[HumidityReading] CHECK CONSTRAINT [FK_humidity_room]
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD CONSTRAINT [FK_room_settings] FOREIGN KEY ([settings_id])
REFERENCES [dbo].[Settings] ([settings_id])
GO

ALTER TABLE [dbo].[HumidityReading] CHECK CONSTRAINT [FK_humidity_room]
GO