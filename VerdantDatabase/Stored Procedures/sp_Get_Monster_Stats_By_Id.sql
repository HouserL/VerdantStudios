CREATE PROCEDURE [dbo].[sp_Get_Monster_Stats_By_Id]
	@Id int = Id
AS

	SELECT *
	FROM [dbo].[Stats]
	Where [Stats].Id = @Id

RETURN 0
