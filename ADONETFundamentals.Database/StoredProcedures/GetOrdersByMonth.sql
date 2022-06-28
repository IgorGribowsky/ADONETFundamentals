CREATE PROCEDURE [dbo].[GetOrdersByMonth]
	@Month int = 0
AS BEGIN
	SELECT * FROM Orders
	WHERE MONTH(CreatedDate) = @Month
END
