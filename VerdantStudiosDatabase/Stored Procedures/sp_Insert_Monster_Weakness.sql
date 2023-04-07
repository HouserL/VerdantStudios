CREATE PROCEDURE [dbo].[sp_Insert_Monster_Weakness]
	@MonsterId int = MonsterId,
	@DamageTypeId int = DamageTypeId
AS
BEGIN

	INSERT INTO [dbo].[Monster_DamageType_Weaknesses]
	VALUES (@MonsterId,@DamageTypeId)

END