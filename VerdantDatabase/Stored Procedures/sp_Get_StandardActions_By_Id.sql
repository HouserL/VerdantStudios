CREATE PROCEDURE [dbo].[sp_Get_StandardActions_By_Id]
	@Id int = Id
AS
	Select *
	From [dbo].StandardActions
	Where StandardActions.MonsterId = @Id

RETURN 0
