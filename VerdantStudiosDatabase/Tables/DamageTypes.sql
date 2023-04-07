CREATE TABLE [dbo].[DamageTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(50) NOT NULL,

	--CONSTRAINT [FK_DamageTypes_Weakness_DamageTypeId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Monster_DamageType_Weaknesses] (DamageTypeId)
)
