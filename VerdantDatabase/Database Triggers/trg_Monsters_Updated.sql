CREATE TRIGGER [trg_Monsters_Updated]
	ON [dbo].[Monsters]
	FOR DELETE, UPDATE
	AS
	BEGIN
		DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
		WHERE [MonsterId] = (SELECT Id FROM deleted)
		DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
		WHERE [MonsterId] = (SELECT Id FROM inserted)
	END
