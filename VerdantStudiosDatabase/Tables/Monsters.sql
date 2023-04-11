CREATE TABLE [dbo].[Monsters]
(
	[Id] INT NOT NULL Identity (1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(50) NOT NULL,
	[StatsId] INT NOT NULL,

	CONSTRAINT PK_Monsterkey PRIMARY KEY (Id),
	--CONSTRAINT [FK_Monsters_Stats_StatsId] FOREIGN KEY ([StatsId]) REFERENCES [dbo].[Stats] (Id) ON DELETE CASCADE,
	--CONSTRAINT [FK_Monsters_Weakness_MonsterId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Monster_DamageType_Weaknesses] (MonsterId)
);
GO
