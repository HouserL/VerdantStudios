CREATE PROCEDURE [dbo].[sp_Get_All_Monsters]

AS
	SELECT * FROM [dbo].[Monsters]

	Select [Id], [Name], [Description], mdw.MonsterId 
	FROM DamageTypes dt JOIN Monster_DamageType_Weaknesses mdw 
	ON dt.Id = mdw.DamageTypeId 

RETURN 0
