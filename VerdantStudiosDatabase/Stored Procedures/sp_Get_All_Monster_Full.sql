CREATE PROCEDURE [dbo].[sp_Get_All_Monster_Full]

AS
BEGIN

SELECT * FROM [dbo].[Monsters]

SELECT * FROM [dbo].[Stats]

SELECT * FROM [dbo].[StandardActions]

END