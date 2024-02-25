-- CREATE PROCEDURE spGetCustomerOrdersCount
--     @Document CHAR(11)
-- AS
    SELECT 
        c.[Id],
        CONCAT(c.[FirstName], ' ', c.[LastName]) AS [Name],
        c.[Document]
    FROM 
        [Customer] c
    INNER JOIN
        [Order] o 
    ON
        o.[CustomerId] = c.[Id]