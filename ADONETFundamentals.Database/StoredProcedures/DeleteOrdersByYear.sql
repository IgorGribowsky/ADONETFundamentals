CREATE PROCEDURE [dbo].[DeleteOrdersByYear]
	@Year int = 0
AS BEGIN
	DELETE FROM Orders
	WHERE YEAR(CreatedDate) = @Year
END
