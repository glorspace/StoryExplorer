DELETE FROM Height

SET IDENTITY_INSERT [dbo].[Height] ON
INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (1, N'Short')
INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (2, N'Average')
INSERT INTO [dbo].[Height] ([Id], [Name]) VALUES (3, N'Tall')
SET IDENTITY_INSERT [dbo].[Height] OFF
