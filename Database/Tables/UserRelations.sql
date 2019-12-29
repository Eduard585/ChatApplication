CREATE TABLE [dbo].[UserRelations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[User1Id] [bigint] NULL,
	[User2Id] [bigint] NULL,
	[Status] [tinyint] NULL,
	[ActionUserId] [bigint] NULL,
 CONSTRAINT [PK_UserRelations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO