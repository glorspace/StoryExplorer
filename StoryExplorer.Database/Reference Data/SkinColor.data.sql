DELETE FROM SkinColor

SET IDENTITY_INSERT [dbo].[SkinColor] ON
INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (1, N'Cream')
INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (2, N'Olive')
INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (3, N'Golden')
INSERT INTO [dbo].[SkinColor] ([Id], [Name]) VALUES (4, N'Chocolate')
SET IDENTITY_INSERT [dbo].[SkinColor] OFF
