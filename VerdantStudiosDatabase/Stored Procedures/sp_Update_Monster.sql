CREATE PROCEDURE [dbo].[sp_Update_Monster]
	@Id int = Id,
	@MonsterName nvarchar(50) = MonsterName,
	@MonsterDescription nvarchar(50) = MonsterDescription,
	@Strength int = Strength,
	@Dexterity int = Dexterity,
	@Constitution int = Constitution,
	@Intelligence int = Intelligence,
	@Wisdom int = Wisdom,
	@Charisma int = Charisma

AS
	UPDATE Monsters
	SET [Name] = @MonsterName,
		[Description] = @MonsterDescription
	WHERE dbo.Monsters.Id = @Id 
	
	UPDATE [Stats]
	Set [Strength] = @Strength,
		[Dexterity] = @Dexterity,
		[Constitution] = @Constitution,
		[Intelligence] = @Intelligence,
		[Wisdom] = @Wisdom,
		[Charisma] = @Charisma
	WHERE dbo.[Stats].Id = @Id

RETURN 0
