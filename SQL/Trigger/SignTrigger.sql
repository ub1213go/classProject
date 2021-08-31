USE [WorkoutHunter]
GO

/****** Object:  Trigger [dbo].[sign]    Script Date: 2021/7/21 下午 01:30:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [dbo].[sign] on [dbo].[user_info]
after insert as
begin
	declare @id char(10)
	select @id = UID from inserted

	insert user_status(UID) values (@id)
	insert character_item_skill(UID) values (@id)
	insert game_progress(UID) values (@id)
end
GO

ALTER TABLE [dbo].[user_info] ENABLE TRIGGER [sign]
GO


