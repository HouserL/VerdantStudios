CREATE PROCEDURE [dbo].[sp_Get_Monster_Weaknesses]
	@Id int = Id
AS
BEGIN
	Select Id, [Name], [Description] FROM
Monster_DamageType_Weaknesses mdw
join DamageTypes dt ON mdw.DamageTypeId = dt.Id
Where MonsterId = @Id

END
