CREATE TRIGGER [trg_Monster_Update_Delete]
	ON [dbo].[Monster]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
	END
