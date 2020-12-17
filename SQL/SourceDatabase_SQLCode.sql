--**************************************************************************
--SOURCE DATABASE
--**************************************************************************
USE [EnviormentDatabase]
GO
USE [OP_EnviornmentMonitoring]
GO

DROP TABLE IF EXISTS [EnviormentDatabase].dbo.Measurement
CREATE TABLE [dbo].[Measurement](
	[measurementId]				[int]		IDENTITY(1,1)   NOT NULL,
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
	[settingsId]                [int]       IDENTITY(1,1)   NOT NULL,
	[lastUpdated]               [datetime]  NOT NULL,
	[temperatureSetpoint]       [float]     NOT NULL,
	[ppmMin]                    [int]       NOT NULL,
	[ppmMax]                    [int]       NOT NULL,
    [sentToDevice]              [datetime]  NULL,
	CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([settingsId] ASC)
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [EnviormentDatabase].dbo.Room
CREATE TABLE [dbo].[Room](
	[roomId]                    [int]       IDENTITY(1,1) NOT NULL,
	[settingsId]                [int]       NOT NULL unique,
	[name]                      [nvarchar](50) NOT NULL,
    [deviceEUI]                 [nvarchar](16) NOT NULL unique,
	CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([roomId] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD CONSTRAINT [FK_Room_Settings] FOREIGN KEY ([settingsId])
REFERENCES [dbo].[Settings] ([settingsId])
GO

ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Settings]
GO