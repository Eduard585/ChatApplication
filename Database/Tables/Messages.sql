CREATE TABLE [dbo].[Messages](
	[Id] [bigint] NOT NULL,
	[CreatorId] [bigint] NULL,
	[RoomId] [bigint] NULL,
	[SentDate] [datetime] NULL,
	[Body] [nvarchar](max) NULL,
	[AttachmentId] [bigint] NULL,
	[Read?] [bit] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO