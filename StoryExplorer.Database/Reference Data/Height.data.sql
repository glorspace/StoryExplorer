SET IDENTITY_INSERT [dbo].[Height] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[Height] WHERE [Id] = 1)
	INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (1, N'Short')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Height] WHERE [Id] = 2)
	INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (2, N'Average')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Height] WHERE [Id] = 3)
	INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (3, N'Tall')

SET IDENTITY_INSERT [dbo].[Height] OFF
