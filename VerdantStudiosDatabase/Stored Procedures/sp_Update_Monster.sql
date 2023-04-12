CREATE PROCEDURE [dbo].[sp_Update_Monster]
	@Id int = Id,
	@Monster udt_Monster READONLY
	--@Name nvarchar,
	--@Strength int = Strength,
	--@Dexterity int = Dexterity,
	--@Constitution int = Constitution,
	--@Intelligence int = Intelligence,
	--@Wisdom int = Wisdom,
	--@Charisma int = Charisma

AS
	UPDATE Monsters
	SET [Name] = (SELECT TOP 1 [Name] From @Monster),
		[Description] = (SELECT TOP 1 [Description] FROM @Monster)
	WHERE dbo.Monsters.Id = @Id 
	
	UPDATE [Stats]
	Set [Strength] = (SELECT TOP 1 [STR] From @Monster)
		--[Dexterity] = @Dexterity,
		--[Constitution] = @Constitution,
		--[Intelligence] = @Intelligence,
		--[Wisdom] = @Wisdom,
		--[Charisma] = @Charisma
	WHERE dbo.[Stats].Id = @Id

RETURN 0
