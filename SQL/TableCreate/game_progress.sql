USE [WorkoutHunter]
GO

/****** Object:  Table [dbo].[game_progress]    Script Date: 2021/7/20 下午 12:03:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[game_progress](
	[UID] [char](10) NOT NULL,
	[StartTime] [nvarchar](20) NULL,
	[SavePoint] [varchar] (50) NULL,
 CONSTRAINT [PK_game_progress] PRIMARY KEY CLUSTERED
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[game_progress]  WITH CHECK ADD  CONSTRAINT [FK_game_progress_user_info] FOREIGN KEY([UID])
REFERENCES [dbo].[user_info] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[game_progress] CHECK CONSTRAINT [FK_game_progress_user_info]
GO
