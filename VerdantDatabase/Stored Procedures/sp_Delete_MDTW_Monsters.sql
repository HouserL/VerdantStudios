CREATE PROCEDURE [dbo].[sp_Delete_MDTW_Monsters]
	@Id int = Id

AS

	DELETE FROM dbo.Monster_DamageType_Weaknesses
	WHERE Monster_DamageType_Weaknesses.MonsterId = @Id

RETURN 0
