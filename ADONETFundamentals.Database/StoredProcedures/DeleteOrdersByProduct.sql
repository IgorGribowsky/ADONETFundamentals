CREATE PROCEDURE [dbo].[DeleteOrdersByProduct]
	@ProductName varchar(100)
AS BEGIN
	DELETE FROM Orders
	WHERE ProductId IN 
	(SELECT Id FROM Products
	WHERE Name = @ProductName)
END