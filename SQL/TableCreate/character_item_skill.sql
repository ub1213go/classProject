USE [WorkoutHunter]
GO

/****** Object:  Table [dbo].[character_item_skill]    Script Date: 2021/7/20 下午 12:03:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[character_item_skill](
	[UID] [char](10) NOT NULL,
	[Items] [nvarchar](100) NULL,
	[Skills] [nvarchar](50) NULL,
	[ChaPic] [nvarchar](10) NULL,
	[Money] [int] NULL,
	[rawPoint] [int] NULL,
	[nowSkill] [int] NULL,
	[nowItem] [int] NULL,
 CONSTRAINT [PK_character_item_skill] PRIMARY KEY CLUSTERED
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[character_item_skill]  WITH CHECK ADD  CONSTRAINT [FK_character_item_skill_user_info] FOREIGN KEY([UID])
REFERENCES [dbo].[user_info] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[character_item_skill] CHECK CONSTRAINT [FK_character_item_skill_user_info]
GO


