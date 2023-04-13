CREATE PROCEDURE [dbo].[sp_Get_All_Monsters_Full]

AS
SELECT * FROM dbo.Monsters
SELECT * FROM dbo.[Stats]
SELECT * FROM dbo.StandardActions

RETURN 0
