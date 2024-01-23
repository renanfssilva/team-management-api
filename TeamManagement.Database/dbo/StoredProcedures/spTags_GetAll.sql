CREATE PROCEDURE [dbo].[spTags_GetAll]
AS
BEGIN
	SELECT id, name	FROM dbo.tags;
END
