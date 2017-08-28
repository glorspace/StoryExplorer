SET IDENTITY_INSERT [dbo].[HairStyle] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 1)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (1, N'Cropped')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 2)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (2, N'Pigtails')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 3)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (3, N'Ponytail')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 4)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (4, N'PageBoy')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 5)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (5, N'Bun')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 6)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (6, N'Pixie')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 7)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (7, N'PixieWithBangs')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 8)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (8, N'Long')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[HairStyle] WHERE [Id] = 9)
	INSERT INTO [dbo].[HairStyle] ([Id], [Name]) VALUES (9, N'CrewCut')

SET IDENTITY_INSERT [dbo].[HairStyle] OFF
