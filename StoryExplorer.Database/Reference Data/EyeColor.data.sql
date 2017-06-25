DELETE FROM EyeColor

SET IDENTITY_INSERT [dbo].[EyeColor] ON
INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (1, N'Blue')
INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (2, N'Green')
INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (3, N'Grey')
INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (4, N'Brown')
INSERT INTO [dbo].[EyeColor] ([Id], [Name]) VALUES (5, N'Hazel')
SET IDENTITY_INSERT [dbo].[EyeColor] OFF
