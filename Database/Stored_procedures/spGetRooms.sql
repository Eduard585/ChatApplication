CREATE PROCEDURE [dbo].[spGetRooms]
	@userId bigint = null,
	@roomId bigint = null
AS
	SELECT [Id],[Name],[CreateDate],[IsActive]
	FROM dbo.Rooms
	WHERE (@userId is null) or Id IN (SELECT RoomId FROM dbo.UserRooms WHERE UserId = @userId) and
	(@roomId is null) or Id = @roomId

