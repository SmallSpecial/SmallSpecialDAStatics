USE [GSSDB]
GO
if(OBJECT_ID('SP_AddAwardToZone','p') is not null)
	drop procedure SP_AddAwardToZone
go
if(OBJECT_ID('SP_AddAwardToMysql','p')is not null)
	drop procedure SP_AddAwardToMysql
go
create procedure  SP_AddAwardToMysql
(
	@bigZone smallint,
	@zoneID smallint,
	@logicJson nvarchar(max),--该工单的逻辑数据
	@taskId int
)
as
	declare @logicId uniqueidentifier 
	set @logicId=NEWID()
	insert into dbo.WorkOrderLogicData([Id],[TaskId],[LogicData],[CreateTime],[IsDelete],[SyncConfig])
	values(@logicId,@taskId,@logicJson,GETDATE(),0,0)
	if(@zoneID=-1) 
	select  name,provider_string from sys.servers where name in
	(
		select 'LKSV_GSS_7_'+F_DBName+'_'+CONVERT(varchar(3),F_BigZoneID)+'_'+CONVERT(varchar(3),F_BattleZoneID)
		 from dbo.T_BaseParamDB where F_DBType=7 and F_BigZoneID=@bigZone   
	)
	else 
	select  name,provider_string from sys.servers where name in
	(
		select 'LKSV_GSS_7_'+F_DBName+'_'+CONVERT(varchar(3),F_BigZoneID)+'_'+CONVERT(varchar(3),F_BattleZoneID)
		 from dbo.T_BaseParamDB where F_DBType=7 and F_BigZoneID=@bigZone and F_BattleZoneID=@zoneID
	)
go 