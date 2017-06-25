CREATE TABLE [dbo].[Region]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(MAX) NULL, 
    [OwnerId] INT NULL
)
