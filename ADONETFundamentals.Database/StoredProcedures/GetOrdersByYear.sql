CREATE PROCEDURE [dbo].[GetOrdersByYear]
	@Year int = 0
AS BEGIN
	SELECT * FROM Orders
	WHERE YEAR(CreatedDate) = @Year
END
