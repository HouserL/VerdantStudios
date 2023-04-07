CREATE PROCEDURE [dbo].[sp_Get_Monster_By_Id]
	@Id int = Id

AS

	SELECT * FROM [dbo].[Monsters] monster
	WHERE monster.[Id] = @Id

	Select Id, [Name], [Description] 
	FROM DamageTypes dt JOIN Monster_DamageType_Weaknesses mdw 
	ON dt.Id = mdw.DamageTypeId 
	WHERE MonsterId = @Id
RETURN 0
