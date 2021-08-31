USE [WorkoutHunter]
GO

/****** Object:  Table [dbo].[user_info]    Script Date: 2021/7/20 下午 12:05:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_info](
	[UID] [char](10) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
	[Role] [char](1) NULL,
	[SignDate] [varchar](10) NULL,
	[Class] [char](1) NULL,
	[salt] [varchar](50) NULL,
	[PT] [char](10) NULL,
 CONSTRAINT [PK_user_Info] PRIMARY KEY CLUSTERED
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
