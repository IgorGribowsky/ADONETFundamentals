CREATE PROCEDURE [dbo].[DeleteOrdersByMonth]
	@Month int = 0
AS BEGIN
	DELETE FROM Orders
	WHERE MONTH(CreatedDate) = @Month
END
