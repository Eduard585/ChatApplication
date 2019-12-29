Create PROCEDURE [dbo].[spGetUsersByFilter]
	-- Add the parameters for the stored procedure here
	@Id bigint,
	@Login nvarchar(30) = null,
	@Email nvarchar(30) = null,
	@IsBlocked bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select [Id],[Login],[Email],[IsBlocked],[UpdDate],[PasswordHash],[Salt]
	from dbo.Users
	where
	(@Id = 0 or [Id] = @Id) AND
	(@Login is null or [Login] = @Login) AND
	(@Email is null or [Email] = @Email) AND
	(@IsBlocked is null or [IsBlocked] = @IsBlocked)
END