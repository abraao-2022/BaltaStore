CREATE PROCEDURE spCheckDocument
    @Document CHAR(11)
AS
    SELECT CASE WHEN EXISTS (
        SELECT [Id]
        FROM [Customer]
        WHERE [Document] = @Document
    )
    THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit) END