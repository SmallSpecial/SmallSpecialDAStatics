use gssdb
go
if(OBJECT_ID('SP_QueryGameDBConnString','p')is not null)
	drop procedure SP_QueryGameDBConnString
go
 create procedure SP_QueryGameDBConnString
 (
	@dbName varchar(30) 
 )
 as
 select name,product,provider_string from sys.servers 
where name in(
select 'LKSV_GSS_'+convert(varchar(3),F_DBType)+'_'+F_DBName+'_'+convert(varchar(3),F_BigZoneID)
 from dbo.T_BaseParamDB 
 where F_DBName=@dbName
 )