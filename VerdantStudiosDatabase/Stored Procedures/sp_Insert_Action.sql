CREATE PROCEDURE [dbo].[sp_Insert_Action]
	@MonsterId int = MonsterId,
	@Name nvarchar(50) = [MonsterName],
	@Description nvarchar(4000) = [Description]
AS
BEGIN

	INSERT INTO [dbo].[StandardActions]([MonsterId],[Name],[Description])
	VALUES (@MonsterId, @Name, @Description)

END