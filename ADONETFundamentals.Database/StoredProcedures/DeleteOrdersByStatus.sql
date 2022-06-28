CREATE PROCEDURE [dbo].[DeleteOrdersByStatus]
	@StatusName varchar(40)
AS BEGIN
	DELETE FROM Orders
	WHERE Status = @StatusName
END