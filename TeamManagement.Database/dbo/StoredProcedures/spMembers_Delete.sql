CREATE PROCEDURE [dbo].[spMembers_Delete]
	@Id int
AS
BEGIN
    DELETE FROM dbo.members
    WHERE id = @Id;
END;
