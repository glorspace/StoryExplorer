SET IDENTITY_INSERT [dbo].[Gender] ON

IF NOT EXISTS (SELECT [Id] FROM [dbo].[Gender] WHERE [Id] = 1)
	INSERT INTO [dbo].[Gender] ([Id], [Name]) VALUES (1, N'Male')
IF NOT EXISTS (SELECT [Id] FROM [dbo].[Gender] WHERE [Id] = 2)
	INSERT INTO [dbo].[Gender] ([Id], [Name]) VALUES (2, N'Female')

SET IDENTITY_INSERT [dbo].[Gender] OFF
