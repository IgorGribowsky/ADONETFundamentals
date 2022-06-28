CREATE PROCEDURE [dbo].[GetOrdersByProduct]
	@ProductName varchar(100)
AS BEGIN
	SELECT * FROM Orders
	WHERE ProductId IN 
	(SELECT Id FROM Products
	WHERE Name = @ProductName)
END