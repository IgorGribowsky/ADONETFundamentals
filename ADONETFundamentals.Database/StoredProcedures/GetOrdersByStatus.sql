CREATE PROCEDURE [dbo].[GetOrdersByStatus]
	@StatusName varchar(40)
AS BEGIN
	SELECT * FROM Orders
	WHERE Status = @StatusName
END