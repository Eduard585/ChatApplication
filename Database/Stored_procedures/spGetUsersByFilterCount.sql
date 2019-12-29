CREATE PROCEDURE [dbo].[spGetUsersByFilterCount]
	-- Add the parameters for the stored procedure here	
	@Id bigint = null,
	@Login nvarchar(30) = null,
	@Email nvarchar(30) = null,
	@IsBlocked bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     --Insert statements for procedure here		
	Select count(Id)
	from dbo.Users	
	where	
	(@Id = 0 or @Id is null or [Id] = @Id) and
	(@Login is null or [Login] = @Login) and
	(@Email is null or [Email] = @Email) and
	(@IsBlocked is null or [IsBlocked] = @IsBlocked)		
END