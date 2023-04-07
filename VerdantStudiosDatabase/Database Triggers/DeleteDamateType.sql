CREATE TRIGGER [DeleteDamateType]
	ON [dbo].[DamageTypes]
	FOR DELETE
AS

BEGIN
	DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
	WHERE [DamageTypeId] = (SELECT Id FROM deleted)
END
