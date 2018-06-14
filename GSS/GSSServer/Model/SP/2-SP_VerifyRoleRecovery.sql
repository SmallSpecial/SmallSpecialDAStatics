USE [GSSDB]
GO

/****** Object:  StoredProcedure [dbo].[SP_VerifyRoleRecovery]    Script Date: 08/10/2017 17:55:25 ******/
SET ANSI_NULLS ON
GO
if(OBJECT_ID('SP_VerifyRoleRecovery','p') is not null)
	drop procedure SP_VerifyRoleRecovery
go
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[SP_VerifyRoleRecovery]
(
	@bigZone int,
	@zoneID int,
	@uid int,
	@delRid int ,
	@code int output
)
as
declare @linkGameCore varchar(50)
select @linkGameCore='LKSV_GSS_'+CONVERT(varchar(2),F_DBType)+'_'+CONVERT(varchar(30),F_DBName)+'_'+CONVERT(varchar(2),F_BigZoneID) from GSSDB.dbo.T_BaseParamDB 
where F_DBType=2 and F_BigZoneID=@bigZone and F_BattleZoneID=-1
print  @linkGameCore
if(nullif(@linkGameCore,'') is null)
	begin
		set @code=4041
		return
	end
declare @tab varchar(20)
set @tab='T_RoleBaseData_'+CONVERT(varchar(1),@uid%5)
declare @ver nvarchar(1000)
declare @replace int
declare @rec int
set @ver='
select @rec=COUNT( F_RoleID)  from {link}GameCoreDB.dbo.T_RoleBaseDataDeleted where   F_RoleID=@delRid
select @replace =COUNT(r.F_RoleID) from  {link}GameCoreDB.dbo.{t} r,{link}GameCoreDB.dbo.T_RoleBaseDataDeleted d
 where  r.F_yRolePos=d.F_yRolePos and r.F_UserID=d.F_RoleID and d.F_ZoneID=r.F_ZoneID
 and   d.F_RoleID=@delRid '
 set @ver=REPLACE(@ver,'{link}',@linkGameCore+'.')
 set @ver=REPLACE(@ver,'{t}',@tab)
 exec sp_executesql @ver,N'@replace int output,@rec int out,@delRid int',@replace out,@rec out,@delRid
 print @ver
 --can not recovery the role :the role of position is used
 if(@rec=0) -- no refresh data
	begin
		set @code=2
		return 
	end
 if(@replace>0)
	begin
		set @code=1
		return
	end
	declare @cmd nvarchar (2000)
	--{link}GameCoreDB.dbo.T_RoleBaseData_0 
	set @cmd='insert into {link}GameCoreDB.dbo.{t} 
	(F_RoleID,F_UserID,F_RoleName,F_RolePwd,F_ZoneID,F_CampID,F_Pro,F_Sex,F_Level,F_LastScene,F_Exp,F_StoreMoney,F_GameMoney,F_RealMoney,F_CreateTime,F_UpdateTime,F_yRolePos,F_HaveRoleData,F_SkinColor,F_FaceType,F_HairType,F_HairColor,F_EyeBrowType,F_MouthType,F_NoseType,F_EyeType,F_Flag,F_dwCheckNGSCode,F_OnlineNum,F_OnlineTime,F_WeaponID,F_BreastplateID,F_HelmetID,F_WingID,F_WeaponLv,F_BreastplateLv,F_PetID,F_HairStyle,F_FaceStyle)
	select 
	F_RoleID,F_UserID,F_RoleName,F_RolePwd,F_ZoneID,F_CampID,F_Pro,F_Sex,F_Level,F_LastScene,F_Exp,F_StoreMoney,F_GameMoney,F_RealMoney,F_CreateTime,F_UpdateTime,F_yRolePos,F_HaveRoleData,F_SkinColor,F_FaceType,F_HairType,F_HairColor,F_EyeBrowType,F_MouthType,F_NoseType,F_EyeType,F_Flag,F_dwCheckNGSCode,F_OnlineNum,F_OnlineTime,F_WeaponID,F_BreastplateID,F_HelmetID,F_WingID,F_WeaponLv,F_BreastplateLv,F_PetID,F_HairStyle,F_FaceStyle 
	from {link}GameCoreDB.dbo.T_RoleBaseDataDeleted where F_RoleID=@delRid
	delete from {link}GameCoreDB.dbo.T_RoleBaseDataDeleted where F_RoleID=@delRid
	'
	set @cmd=REPLACE(@cmd,'{link}',@linkGameCore+'.')
	set @cmd=REPLACE(@cmd,'{t}',@tab)
	print @cmd
	exec sp_executesql @cmd,N'@delRid int',@delRid
	
	set @code=200
	print @code

GO


