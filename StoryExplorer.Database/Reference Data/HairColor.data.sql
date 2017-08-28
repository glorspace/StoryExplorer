SET IDENTITY_INSERT [dbo].[HairColor] ON


IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairColor] WHERE [Id] = 1)
	INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (1, N'Blonde')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairColor] WHERE [Id] = 2)
	INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (2, N'Brunette')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairColor] WHERE [Id] = 3)
	INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (3, N'Auburn')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairColor] WHERE [Id] = 4)
	INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (4, N'Ebony')

SET IDENTITY_INSERT [dbo].[HairColor] OFF
