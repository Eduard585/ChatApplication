CREATE PROCEDURE [dbo].[spSaveUser] 
	-- Add the parameters for the stored procedure here
	@Id bigint = 0,
	@Login nvarchar(50) = null,	
	@Email nvarchar(50) = null,
	@IsBlocked bit,
	@UpdDate datetime,
	@passwordHash nvarchar(250) = null,
	@salt nvarchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if @Id > 0
	begin
	update dbo.Users
	set 
	[Login] = @Login,	
	[Email] = @Email,
	[IsBlocked] = @IsBlocked,
	[UpdDate] = @UpdDate,
	[PasswordHash] = @passwordHash,
	[Salt] = @salt
where Id = @Id
select @Id
end
else
begin
insert into dbo.Users([Login],[Email],[IsBlocked],[UpdDate],[PasswordHash],[Salt])
values(@Login,@Email,@IsBlocked,@UpdDate,@passwordHash,@salt)
select @@IDENTITY
end
END