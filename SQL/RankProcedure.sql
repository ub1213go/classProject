USE WorkoutHunter

create procedure RankProcedure as
select UID, Role, Class into #user_info from user_info
select UID, (Strength + Vitality + Agility) as 'mycount' into #user_status from user_status
declare @sum int = (select sum(Strength+Vitality+Agility) from user_status)
select a.UID, a.Class,a.Role, b.mycount, (@sum) as 'mysum' into #temp from #user_info as a 
left join #user_status as b 
on a.uid = b.uid
update Tb1 
set Tb1.Class = Tb2.Class 
from user_info AS Tb1
INNER JOIN (select UID, dbo.RankJ(mysum, mycount) as class from #temp
where role = 'S') AS Tb2
ON Tb1.UID = Tb2.UID
WHERE Tb1.Role = 'S'
select * from user_info
drop table #temp
drop table #user_info
drop table #user_status

CREATE FUNCTION RankJ (@sum int, @num int)
returns char(1)
BEGIN
declare @a int = @sum * 0.8
declare @b int = @sum * 0.6
declare @c int = @sum * 0.4
declare @d int = @sum * 0.2
declare @anser char(1);
if (@num >= @a) set @anser = 'S'
else if (@num >= @b) set @anser = 'A'
else if (@num >= @c) set @anser = 'B'
else if (@num >= @d) set @anser = 'C'
else set @anser = 'D'
return @anser
END;

