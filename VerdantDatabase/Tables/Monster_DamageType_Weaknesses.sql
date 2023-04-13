CREATE TABLE [dbo].[Monster_DamageType_Weaknesses]
(
	[MonsterId] INT NOT NULL,
	[DamageTypeId] INT NOT NULL,

	CONSTRAINT PK_Key_MonsterIS_DamageTypeID PRIMARY KEY (MonsterId, DamageTypeId),
	CONSTRAINT FK_MDW_Monster_MonsterID FOREIGN KEY (MonsterId) REFERENCES [dbo].[Monsters] (Id),
	CONSTRAINT FK_MDW_DamageType_DamageTypeID FOREIGN KEY (DamageTypeId) REFERENCES [dbo].[DamageTypes] (Id)
)
