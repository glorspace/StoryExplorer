CREATE TABLE [dbo].[RegionDesignatedAuthor]
(
	[RegionId] INT NOT NULL , 
    [DesignatedAuthorId] INT NOT NULL, 
    CONSTRAINT [PK_RegionDesignatedAuthor] PRIMARY KEY ([RegionId], [DesignatedAuthorId]), 
    CONSTRAINT [FK_RegionDesignatedAuthor_Region] FOREIGN KEY ([RegionId]) REFERENCES [Region]([Id]), 
    CONSTRAINT [FK_RegionDesignatedAuthor_Adventurer] FOREIGN KEY ([DesignatedAuthorId]) REFERENCES [Adventurer]([Id])
)
