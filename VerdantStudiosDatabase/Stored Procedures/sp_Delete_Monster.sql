CREATE PROCEDURE [dbo].[sp_Delete_Monster]
	
	@Id int = Id

AS

BEGIN

	DELETE 
	FROM [dbo].[Monsters]
	WHERE [Monsters].[Id] = @Id

	DELETE 
	FROM [dbo].[Stats]
	WHERE [Stats].[Id] = @Id

	DELETE
	FROM [dbo].[StandardActions]
	WHERE [StandardActions].[MonsterId] = @Id

END
