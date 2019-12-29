CREATE PROCEDURE [dbo].[spSaveRoom]
	@id bigint = 0,
	@name nvarchar(50) = null,	
	@isActive bit
AS
BEGIN
	SET NOCOUNT ON
	
	if @id>0
	begin
	update dbo.Rooms
	set
	[Name] = @name,
	[IsActive] = @isActive
	where Id = @id
	select @id
	end
	else begin
	insert into dbo.Rooms([Name],[CreateDate],[IsActive])
	values(@name,GETUTCDATE(),1)
	select @@IDENTITY
	end
END
