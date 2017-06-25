CREATE TABLE [dbo].[Region]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(MAX) NULL, 
    [OwnerId] INT NULL
)
