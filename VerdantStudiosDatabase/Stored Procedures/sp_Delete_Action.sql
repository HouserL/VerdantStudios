CREATE PROCEDURE [dbo].[sp_Delete_Action]
	@Id int = Id
AS
BEGIN
	
	DELETE
	FROM [dbo].[StandardActions]
	WHERE [StandardActions].[Id] = @Id

END