DELETE FROM Gender

SET IDENTITY_INSERT [dbo].[Gender] ON
INSERT INTO [dbo].[Gender] ([Id], [Name]) VALUES (1, N'Male')
INSERT INTO [dbo].[Gender] ([Id], [Name]) VALUES (2, N'Female')
SET IDENTITY_INSERT [dbo].[Gender] OFF
