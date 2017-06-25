DELETE FROM Personality

SET IDENTITY_INSERT [dbo].[Personality] ON
INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (1, N'Stoic')
INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (2, N'Mischievous')
INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (3, N'Boisterous')
INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (4, N'Melancholic')
INSERT INTO [dbo].[Personality] ([Id], [Name]) VALUES (5, N'Whimsical')
SET IDENTITY_INSERT [dbo].[Personality] OFF
