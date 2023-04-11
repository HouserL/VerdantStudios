CREATE TABLE [dbo].[Stats]
(
	[Id] INT NOT NULL ,
	[Strength] INT NOT NULL,
	[Dexterity] INT NOT NULL,
	[Constitution] INT NOT NULL,
	[Intelligence] INT NOT NULL,
	[Wisdom] INT NOT NULL,
	[Charisma] INT NOT NULL,

	CONSTRAINT [FK_Monsters_Stats_StatsId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Monsters] (Id) ON DELETE CASCADE
)
