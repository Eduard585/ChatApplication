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
PRINT N'Выполняется создание [dbo].[Attachments]...';


GO
CREATE TABLE [dbo].[Attachments] (
    [Id]   BIGINT          NOT NULL,
    [Name] NVARCHAR (250)  NULL,
    [Size] BIGINT          NULL,
    [Url]  NVARCHAR (2048) NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Выполняется создание [dbo].[Messages]...';


GO
CREATE TABLE [dbo].[Messages] (
    [Id]           BIGINT         NOT NULL,
    [CreatorId]    BIGINT         NULL,
    [RoomId]       BIGINT         NULL,
    [SentDate]     DATETIME       NULL,
    [Body]         NVARCHAR (MAX) NULL,
    [AttachmentId] BIGINT         NULL,
    [Read?]        BIT            NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


GO
PRINT N'Обновление завершено.';


GO
