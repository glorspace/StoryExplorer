SET IDENTITY_INSERT [dbo].[SkinColor] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[SkinColor] WHERE [Id] = 1)
	INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (1, N'Cream')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[SkinColor] WHERE [Id] = 2)
	INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (2, N'Olive')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[SkinColor] WHERE [Id] = 3)
	INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (3, N'Golden')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[SkinColor] WHERE [Id] = 4)
	INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (4, N'Chocolate')

SET IDENTITY_INSERT [dbo].[SkinColor] OFF
