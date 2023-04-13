CREATE PROCEDURE [dbo].[sp_Update_Action]
	@Id int = Id,
	@MonsterId int = MonsterId,
	@Name nvarchar(50) = [MonsterName],
	@Description nvarchar(4000) = [Description]
AS
BEGIN

	UPDATE [dbo].[StandardActions]
	SET 
		[MonsterId] = @MonsterId,
		[Name] = @Name,
		[Description] = @Description
	WHERE [dbo].[StandardActions].Id = @Id

END