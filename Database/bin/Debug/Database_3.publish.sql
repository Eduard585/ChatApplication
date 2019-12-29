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
/*
Удаляется столбец [dbo].[UserRooms].[CreateDate], возможна потеря данных.

Удаляется столбец [dbo].[UserRooms].[IsActive], возможна потеря данных.

Удаляется столбец [dbo].[UserRooms].[RoomIdentifier], возможна потеря данных.
*/

IF EXISTS (select top 1 1 from [dbo].[UserRooms])
    RAISERROR (N'Обнаружены строки. Обновление схемы завершено из-за возможной потери данных.', 16, 127) WITH NOWAIT

GO
PRINT N'Указанная ниже операция создана из файла журнала рефакторинга 0ae8db72-9bcd-486b-94f5-18cd094757d9';

PRINT N'Переименование [dbo].[UserRooms].[UserIdentifier] в UserId';


GO
EXECUTE sp_rename @objname = N'[dbo].[UserRooms].[UserIdentifier]', @newname = N'UserId', @objtype = N'COLUMN';


GO
PRINT N'Выполняется создание [dbo].[ArrayBigint]...';


GO
CREATE TYPE [dbo].[ArrayBigint] AS TABLE (
    [ID] BIGINT NULL);


GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[UserRooms]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserRooms] (
    [Id]      BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserId]  BIGINT   NULL,
    [RoomId]  BIGINT   NULL,
    [UpdDate] DATETIME NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_UserRooms1] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserRooms])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserRooms] ON;
        INSERT INTO [dbo].[tmp_ms_xx_UserRooms] ([Id], [UserId], [RoomId])
        SELECT   [Id],
                 [UserId],
                 [RoomId]
        FROM     [dbo].[UserRooms]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserRooms] OFF;
    END

DROP TABLE [dbo].[UserRooms];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UserRooms]', N'UserRooms';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_UserRooms1]', N'PK_UserRooms', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Выполняется создание [dbo].[spGetRooms]...';


GO
CREATE PROCEDURE [dbo].[spGetRooms]
	@userId bigint = null,
	@roomId bigint = null
AS
	SELECT [Id],[Name],[CreateDate],[IsActive]
	FROM dbo.Rooms
	WHERE (@userId is null) or Id IN (SELECT RoomId FROM dbo.UserRooms WHERE UserId = @userId) and
	(@roomId is null) or Id = @roomId
GO
PRINT N'Выполняется создание [dbo].[spSaveFriendStatus]...';


GO
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
GO
PRINT N'Выполняется создание [dbo].[spSaveRoom]...';


GO
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
GO
-- Выполняется этап рефакторинга для обновления развернутых журналов транзакций на целевом сервере

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '0ae8db72-9bcd-486b-94f5-18cd094757d9')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('0ae8db72-9bcd-486b-94f5-18cd094757d9')

GO

GO
PRINT N'Обновление завершено.';


GO
