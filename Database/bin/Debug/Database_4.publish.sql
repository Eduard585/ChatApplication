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
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Rooms]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Rooms] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [CreateDate] DATETIME      NULL,
    [IsActive]   BIT           NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Rooms1] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Rooms])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Rooms] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Rooms] ([Id], [Name], [CreateDate], [IsActive])
        SELECT   [Id],
                 [Name],
                 [CreateDate],
                 [IsActive]
        FROM     [dbo].[Rooms]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Rooms] OFF;
    END

DROP TABLE [dbo].[Rooms];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Rooms]', N'Rooms';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Rooms1]', N'PK_Rooms', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Выполняется создание [dbo].[spAddUsersToRoom]...';


GO
CREATE PROCEDURE [dbo].[spAddUsersToRoom]
	@roomId bigint,
	@userList ArrayBigint readonly
AS
BEGIN
	INSERT INTO dbo.UserRooms(UserId,RoomId,UpdDate)
	VALUES((SELECT * FROM @userList),@roomId,GETUTCDATE())
END
GO
PRINT N'Выполняется обновление [dbo].[spGetRooms]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[spGetRooms]';


GO
PRINT N'Выполняется обновление [dbo].[spSaveRoom]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[spSaveRoom]';


GO
PRINT N'Обновление завершено.';


GO
