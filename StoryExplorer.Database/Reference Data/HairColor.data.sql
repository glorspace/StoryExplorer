DELETE FROM HairColor

SET IDENTITY_INSERT [dbo].[HairColor] ON
INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (1, N'Blonde')
INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (2, N'Brunette')
INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (3, N'Auburn')
INSERT INTO [dbo].[HairColor] ([Id], [Name]) VALUES (4, N'Ebony')
SET IDENTITY_INSERT [dbo].[HairColor] OFF
