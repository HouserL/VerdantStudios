CREATE TABLE [dbo].[Monster_DamageType_Weaknesses]
(
	[MonsterId] INT NOT NULL,
	[DamageTypeId] INT NOT NULL,

	CONSTRAINT PK_Key_MonsterIS_DamageTypeID PRIMARY KEY (MonsterId, DamageTypeId)
)
