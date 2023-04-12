CREATE TYPE [dbo].[udt_Monster] AS TABLE
(
	[Id] INT, 
	[MonsterName] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(50) NOT NULL,
	[STR] INT
)
