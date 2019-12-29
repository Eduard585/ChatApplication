Create PROCEDURE [dbo].[spCreateUserPassword]
	-- Add the parameters for the stored procedure here
	@login nvarchar(30),
	@passwordHash nvarchar(250),
	@salt nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.Users
	SET [PasswordHash] = @passwordHash, 
	[Salt] = @salt
	WHERE [Login] = @login
END