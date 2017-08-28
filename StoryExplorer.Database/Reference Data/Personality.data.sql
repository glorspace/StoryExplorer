SET IDENTITY_INSERT [dbo].[Personality] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[Personality] WHERE [Id] = 1)
	INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (1, N'Stoic')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Personality] WHERE [Id] = 2)
	INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (2, N'Mischievous')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Personality] WHERE [Id] = 3)
	INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (3, N'Boisterous')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Personality] WHERE [Id] = 4)
	INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (4, N'Melancholic')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Personality] WHERE [Id] = 5)
	INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (5, N'Whimsical')

SET IDENTITY_INSERT [dbo].[Personality] OFF
