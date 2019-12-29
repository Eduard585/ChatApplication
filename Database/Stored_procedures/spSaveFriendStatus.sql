 CREATE PROCEDURE [dbo].[spSaveFriendStatus]
	@userId1 bigint,
	@userId2 bigint,
	@status int
AS
	if EXISTS(SELECT TOP 1 1 FROM dbo.UserRelations WHERE User1Id = @userId1 and User2Id = @userId2)
	begin
	UPDATE dbo.UserRelations SET User1Id = @userId1,User2Id = @userId2,[Status] = @status,ActionUserId = @userId1	
	end
	else begin
	INSERT INTO dbo.UserRelations(User1Id,User2Id,[Status],ActionUserId)
	VALUES(@userId1,@userId2,@status,@userId1)	
	end

