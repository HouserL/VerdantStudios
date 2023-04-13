CREATE TABLE [dbo].[Monsters]
(
	[Id] INT NOT NULL Identity (1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(4000) NOT NULL,

	CONSTRAINT PK_Monsterkey PRIMARY KEY (Id),
);
GO
