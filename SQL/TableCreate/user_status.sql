USE [WorkoutHunter]
GO

/****** Object:  Table [dbo].[user_status]    Script Date: 2021/7/20 下午 12:05:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_status](
	[UID] [char](10) NOT NULL,
	[Strength] [int] NULL,
	[Vitality] [int] NULL,
	[Agility] [int] NULL,
	[PunishDay] [int] NULL,
 CONSTRAINT [PK_user_status] PRIMARY KEY CLUSTERED
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[user_status]  WITH CHECK ADD  CONSTRAINT [FK_user_status_user_info] FOREIGN KEY([UID])
REFERENCES [dbo].[user_info] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[user_status] CHECK CONSTRAINT [FK_user_status_user_info]
GO


