/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\EyeColor.data.sql
:r .\Gender.data.sql
:r .\HairColor.data.sql
:r .\HairStyle.data.sql
:r .\Height.data.sql
:r .\Personality.data.sql
:r .\SkinColor.data.sql
