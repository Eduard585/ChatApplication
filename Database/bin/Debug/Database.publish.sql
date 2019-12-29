/*
Скрипт развертывания для ChatAppDB

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ChatAppDB"
:setvar DefaultFilePrefix "ChatAppDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Выполняется создание [dbo].[Rooms]...';


GO
CREATE TABLE [dbo].[Rooms] (
    [Id]         BIGINT        NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [CreateDate] DATETIME      NULL,
    [IsActive]   BIT           NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Выполняется создание [dbo].[UserRelations]...';


GO
CREATE TABLE [dbo].[UserRelations] (
    [Id]           BIGINT  IDENTITY (1, 1) NOT NULL,
    [User1Id]      BIGINT  NULL,
    [User2Id]      BIGINT  NULL,
    [Status]       TINYINT NULL,
    [ActionUserId] BIGINT  NULL,
    CONSTRAINT [PK_UserRelations] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Выполняется создание [dbo].[UserRooms]...';


GO
CREATE TABLE [dbo].[UserRooms] (
    [Id]             BIGINT   NOT NULL,
    [UserIdentifier] BIGINT   NULL,
    [RoomId]         BIGINT   NULL,
    [RoomIdentifier] BIGINT   NULL,
    [CreateDate]     DATETIME NULL,
    [IsActive]       BIT      NULL,
    CONSTRAINT [PK_UserRooms] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Выполняется создание [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Login]        NVARCHAR (50)  NULL,
    [Email]        NVARCHAR (50)  NULL,
    [IsBlocked]    BIT            NULL,
    [UpdDate]      DATETIME       NULL,
    [PasswordHash] NVARCHAR (250) NULL,
    [Salt]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Выполняется создание [dbo].[spCreateUserPassword]...';


GO
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
GO
PRINT N'Выполняется создание [dbo].[spGetUsersByFilter]...';


GO
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
GO
PRINT N'Выполняется создание [dbo].[spGetUsersByFilterCount]...';


GO
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
GO
PRINT N'Выполняется создание [dbo].[spGetUsersByFilterCountOR]...';


GO
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
GO
PRINT N'Выполняется создание [dbo].[spGetUsersFriends]...';


GO
CREATE PROCEDURE [dbo].[spGetUsersFriends]
	-- Add the parameters for the stored procedure here
	@UserId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [Id], [Login] From dbo.Users
	WHERE [Id] IN(SELECT User2Id as Friends From dbo.UserRelations
	Where [User1Id] = @UserId  AND [Status] = 1
	UNION
	Select User1Id From dbo.UserRelations
	WHERE [User2Id] = @UserId AND [Status] = 1)
END
GO
PRINT N'Выполняется создание [dbo].[spSaveUser]...';


GO
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
GO
PRINT N'Обновление завершено.';


GO
