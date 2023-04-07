CREATE TABLE [dbo].[Monster_DamageType_Weaknesses]
(
	[MonsterId] INT NOT NULL,
	[DamageTypeId] INT NOT NULL,

	CONSTRAINT PK_Key PRIMARY KEY (MonsterId, DamageTypeId),
	--CONSTRAINT FK_Monster FOREIGN KEY (MonsterId) REFERENCES [Monsters](Id),
	--CONSTRAINT FK_DamageType FOREIGN KEY (DamageTypeId) REFERENCES [DamageTypes](Id)
)
