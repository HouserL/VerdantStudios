CREATE TABLE [dbo].[StandardActions]
(
	[Id] INT NOT NULL IDENTITY (1,1),
    [MonsterId] INT NOT NULL,

	[Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(4000) NOT NULL, 

    CONSTRAINT PK_Standard_Action PRIMARY KEY (Id), 
    CONSTRAINT FK_StandardAction_Monster_MonsterId FOREIGN KEY (MonsterId) REFERENCES [dbo].[Monsters] (Id) ON DELETE CASCADE
)
