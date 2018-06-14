use DigGameDB
go
if(OBJECT_ID('GatherOtherLog','u') is null)
	create table GatherOtherLog
	(
		DaySign int,
		Userid int,
		roleid int,
		BigzoneId int,
		ZoneId int,
		param1 int,
		param2 int,
		Opid int,
		opBak varchar(512),
		OpTime datetime
	)
go

if(OBJECT_ID('SP_VerifyMysqlTable','p') is not null)
	drop procedure SP_VerifyMysqlTable
go
create procedure SP_VerifyMysqlTable
(
	@linkServer varchar(64),
	@db varchar(64),
	@table varchar(64),
	@exists bit output
)
as

declare @havatable int
declare @check nvarchar(1000)
set @check='
select @exists=tab from openquery({link},
''select count(table_name) as tab from information_Schema.columns where table_Schema=''''{db}'''' and table_name=''''{tab}'''''')'
 set @check=REPLACE(@check,'{link}',@linkServer)
 set @check=REPLACE(@check,'{db}',@db)
 set @check=REPLACE(@check,'{tab}',@table)
 print @check
 exec sp_Executesql @check,N'@exists int output',@exists output
go
if(OBJECT_ID('SP_GatherMountData','p') is not null)
	drop procedure SP_GatherMountData
go
create procedure SP_GatherMountData
(
	@linkServer varchar(64),
	@bigZoneId tinyint,
	@zoneId tinyint,
	@time datetime
)
as
declare @span varchar(10)
set @span= CONVERT(varchar(10),@time,120)
declare @dayInt int 
set @dayInt=CONVERT(int,replace(@span,'-',''))
set @span=REPLACE(@span,'-','_')
declare @table varchar(64)
set @table=@span+'_other_log'

--首先检测当天是否存在otherlog表

declare @exits bit
exec dbo.SP_VerifyMysqlTable @linkServer,'GSLOG_DB',@table,@exists=@exits output
if(@exits=0)
	begin
		insert into [dbo].[T_BaseJobMsg]
		 ( [F_RunID],[F_Msg] ,[F_DateTime]) values(NEWID(),'Lack table GSLOG_DB.'+@table,GETDATE())
		 return
	end
 
declare @cmd varchar(1000)
set @cmd='insert into GatherOtherLog
([DaySign],[BigzoneId],[ZoneId],[Userid],[roleid],[param1],[param2],[Opid],[opBak],[OpTime])
select {day}, {big},{zone},* from openquery({@link},''{@cmd}'')'
declare @sql varchar(500)
set @sql='select uid as userid,cid,para_1,para_2,opid, replace(op_bak,''''\t'''',''''_'''') as op_bak,op_time from {@table} where uid>0 '
set @cmd=REPLACE(@cmd,'{day}',CONVERT(varchar(10),@dayInt))
set @cmd=REPLACE(@cmd,'{big}',CONVERT(varchar(4),@bigZoneId))
set @cmd=REPLACE(@cmd,'{zone}',CONVERT(varchar(4),@zoneId))
set @sql=REPLACE(@sql,'{@table}',@table)

set @cmd=REPLACE(@cmd,'{@link}',@linkServer)
set @cmd=REPLACE(@cmd,'{@cmd}',@sql)
delete from GatherOtherLog where DaySign=@dayInt and BigzoneId=@bigZoneId and ZoneId=@zoneId
exec( @cmd)--数据采集

insert into [dbo].[T_BaseJobMsg]  ( [F_RunID],[F_Msg] ,[F_DateTime]) values(NEWID(),'ok>other_log ',GETDATE())
go
if(OBJECT_ID('SP_UserActiveMount','p') is  not null)
	drop procedure SP_UserActiveMount
go
create procedure SP_UserActiveMount
(
	@bigZoneId smallint,
	@zoneId smallint,
	@dayInt int,
	@userid int=null
)
as
if(nullif(@userid,'') is not null)
	begin
		select * from dbo.gatherOtherLog where Userid=@userid and  daysign=@dayint and  param1=0 and opid=50136 
		and bigzoneid=@bigZoneId and zoneId=@zoneId  
		return
	end
--筛选出某天的激活坐骑信息
select * from dbo.gatherOtherLog where daysign=@dayint and  param1=0 and opid=50136 and bigzoneid=@bigZoneId and zoneId=@zoneId  
go
if(OBJECT_ID('SP_MountLevel','p') is not null)
	drop procedure SP_MountLevel
go
create procedure SP_MountLevel
(
	@bigZoneId smallint,
	@zoneId smallint,
	@dayInt int
)
as
select Userid,roleid,opBak,OpTime from DigGameDB.dbo.GatherOtherLog where BigzoneId=@bigZoneId and ZoneId=@zoneId 
and DaySign=@dayInt   and Opid=50136 and param1=3