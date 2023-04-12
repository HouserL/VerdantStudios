CREATE TABLE [dbo].[Monster_DamageType_Weaknesses]
(
	[MonsterId] INT NOT NULL,
	[DamageTypeId] INT NOT NULL,

	CONSTRAINT PK_Key_MonsterIS_DamageTypeID PRIMARY KEY (MonsterId, DamageTypeId),
	CONSTRAINT FK_MDTW_Monster_MonsterID FOREIGN KEY (MonsterId) REFERENCES [Monsters](Id) ON DELETE Cascade,
	CONSTRAINT FK_MDTW_DamageType FOREIGN KEY (DamageTypeId) REFERENCES [DamageTypes](Id) ON DELETE CASCADE
)
