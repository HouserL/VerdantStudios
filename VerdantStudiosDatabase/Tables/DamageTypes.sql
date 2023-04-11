CREATE TABLE [dbo].[DamageTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(50) NOT NULL,

	--CONSTRAINT [FK_DamageTypes_Weakness_DamageTypeId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Monster_DamageType_Weaknesses] (DamageTypeId)

	--CREATE TRIGGER [UpDateMonsterWeakness]
--	ON [dbo].[Monsters]
--	FOR DELETE, UPDATE
--	AS
--	BEGIN
--		DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
--		WHERE [MonsterId] = (SELECT Id FROM deleted)
--		DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
--		WHERE [MonsterId] = (SELECT Id FROM inserted)
--	END

--CREATE TRIGGER [DeleteDamateType]
--	ON [dbo].[DamageTypes]
--	FOR DELETE
--AS

--BEGIN
--	DELETE FROM [dbo].[Monster_DamageType_Weaknesses]
--	WHERE [DamageTypeId] = (SELECT Id FROM deleted)
--END

)
