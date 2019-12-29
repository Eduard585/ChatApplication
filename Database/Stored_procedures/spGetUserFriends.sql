CREATE PROCEDURE [dbo].[spGetUsersFriends]
	-- Add the parameters for the stored procedure here
	@UserId bigint,
	@StatusId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [Id], [Login] From dbo.Users
	WHERE [Id] IN(SELECT User2Id as Friends From dbo.UserRelations
	Where [User1Id] = @UserId  AND [Status] = @StatusId
	UNION
	Select User1Id From dbo.UserRelations
	WHERE [User2Id] = @UserId AND [Status] = @StatusId)
END