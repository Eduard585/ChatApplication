CREATE PROCEDURE [dbo].[spAddUsersToRoom]
	@roomId bigint,
	@userList ArrayBigint readonly
AS
BEGIN
	INSERT INTO dbo.UserRooms(UserId,RoomId,UpdDate)
	SELECT ID,@roomId,GETUTCDATE()
	FROM @userList
END