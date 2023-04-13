CREATE PROCEDURE [dbo].[sp_Get_All_Monster_Weakness]
AS
BEGIN
	
	SELECT *
	FROM [dbo].[Monster_DamageType_Weaknesses] mdw
	JOIN [dbo].DamageTypes dt ON mdw.DamageTypeId = dt.Id 

END