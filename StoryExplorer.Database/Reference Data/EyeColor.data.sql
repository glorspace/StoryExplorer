SET IDENTITY_INSERT [dbo].[EyeColor] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[EyeColor] WHERE [Id] = 1)
	INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (1, N'Blue')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[EyeColor] WHERE [Id] = 2)
	INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (2, N'Green')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[EyeColor] WHERE [Id] = 3)
	INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (3, N'Grey')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[EyeColor] WHERE [Id] = 4)
	INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (4, N'Brown')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[EyeColor] WHERE [Id] = 5)
	INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (5, N'Hazel')

SET IDENTITY_INSERT [dbo].[EyeColor] OFF
