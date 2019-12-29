CREATE PROCEDURE [dbo].[spGetUsersByFilterCountOR]
	-- Add the parameters for the stored procedure here	
	@Login nvarchar(50) = null,
	@Email nvarchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select count(Id)
	from dbo.Users	
	where		
	(@Login is null or [Login] = @Login) or
	(@Email is null or [Email] = @Email)	
END