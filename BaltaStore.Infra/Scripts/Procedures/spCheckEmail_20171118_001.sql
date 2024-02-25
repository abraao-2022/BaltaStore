CREATE PROCEDURE spCheckEmail
    @Email CHAR(160)
AS
    SELECT CASE WHEN EXISTS (
        SELECT [Id]
        FROM [Customer]
        WHERE [Email] = @Email
    )
    THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit) END