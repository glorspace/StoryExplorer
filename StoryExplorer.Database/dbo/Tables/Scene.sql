CREATE TABLE [dbo].[Scene]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [X] INT NOT NULL, 
    [Y] INT NOT NULL, 
    [Z] INT NOT NULL, 
    [NorthAllowed] BIT NOT NULL, 
    [EastAllowed] BIT NOT NULL, 
    [SouthAllowed] BIT NOT NULL, 
    [WestAllowed] BIT NOT NULL, 
    [UpAllowed] BIT NOT NULL, 
    [DownAllowed] BIT NOT NULL, 
    [RegionId] INT NOT NULL, 
    CONSTRAINT [FK_Scene_Region] FOREIGN KEY ([RegionId]) REFERENCES [Region]([Id])
)
