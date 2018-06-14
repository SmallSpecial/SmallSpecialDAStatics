数据统计说明
注意事项：
一。基础数据表
1.[DigGameDB].[dbo].[T_BaseParamDB]
2.[DigGameDB].[dbo].[T_BaseIPAddress]  用户录入
3.[DigGameDB].[dbo].[T_BaseGameTask] 策划的任务表

4.cdk类型创建
修改配置文件的配置项CDKeyType
二、初始化数据对象服务器
1.使用存储过程
[dbo].[_Init_LinkServerAndJob]
sp_addlinkedserver
sp_addlinkedsrvlogin
2.数据库访问形式
DB->DB odbc名
Code->DB 数据库连接字符串

SQL 调用的是服务器对象，而服务器调用odbc


wss创建的服务器对象名前缀  LKSV_7_gspara_db_0_1   LKSV_库dbtypeID_库名称_大区ID_战区
gss创建的服务器对象名前缀  LKSV_GSS_5_UserCoreDB_0_1    LKSV_GSS_库dbtypeID_库名称_大区ID_战区
三、代码结构说明
路径：CodeAndDoc\GameServicePlat\code
1. wss主站点
WSS\WebWSS
2.战区架设工具站点
WSS\WebZoneConfig
3.战区架设工具webservice接口（对于战区数据调整）
WSS\WebServiceZoneConfig
4.gss调用webservice接口
GSS\GSSWebServiceZone
5.gss调用接口(战区架设时使用该接口进行登录系统)
GSS\GSSWebServiceOut
6.gss调用接口（gss服务端调用该接口完成对于游戏数据和MySQL日志、配置调用的接口）
GSS\GSSWebService
7.gss服务端
GSS\GSSServer
8.gss客户端
GSS\GSSClient

目前使用的公共服IP 192.168.1.224  MySQL  账户 ：root 没有密码，在diggamedb库添加战区服务器基础数据时设置一个无效密码即可
游戏相关库的脚本路径：平台\数据库\SQL版20130910 (注意：目前此处的diggamedb和gssdb库未进行升级，创建dig和gss库的脚本在项目内 2017-09-06)
四、gss服务端处理业务
1.情形1服务端直接处理
Code\gss\gssserver\webservicelib.cs
2.服务端需要调用接口（操作usercore，gamecore数据）
code\gss\gsswebservice\servicexlj.asmx.cs

注意： 接口中将查询结果转换为文件流 code\GSS\GSSCSFramework\DataSerialize.cs
五、配置文件配置
1、需要修改配置文件中数据库连接字符串的程序：
1）WSS\WebServiceZoneConfig 此处需要配置GameCoreDBConnectionString连接游戏中游戏战区连接库的连接地址。
2）GSS客服系统开放接口GSS\GSSWebServiceOut数据库连接配置ConnStrGSSDB
3）WSS程序配置文件修改WSS\WebWSS
2、需要在配置文件中修改连接站点URL程序
1）WSS\WebZoneConfig中修改战区架设服务的api入口配置项WebServiceZoneConfigURL
以及公共开放的接口配置项GSSWebServiceOutURL

【开发日志】
一、游戏库配置
1.战区架设
每增加一项战区，都需要在diggamedb库中添加4条数据（F_BigZoneID中值来自于gamecoredb中T_BattleZone的战区ID）
USE [DigGameDB]
GO
INSERT [dbo].[T_BaseParamDB] ([F_DBType], [F_BigZoneID], [F_BattleZoneID], [F_DBIP], [F_DBPort], [F_DBName], [F_DBUser], [F_DBPSW], [F_Note], [F_State]) VALUES (  1, 0, 1, N'192.168.1.224', N'1134', N'GameLogDB', N'sa', N'5225qs5a5a#', N'Sql_20170814', 1)
INSERT [dbo].[T_BaseParamDB] ( [F_DBType], [F_BigZoneID], [F_BattleZoneID], [F_DBIP], [F_DBPort], [F_DBName], [F_DBUser], [F_DBPSW], [F_Note], [F_State]) VALUES (  8, 0, 1, N'192.168.1.224', N'1134',N'MainDB', N'sa', N'5225qs5a5a#', N'Sql_20170814', 1)
INSERT [dbo].[T_BaseParamDB] ( [F_DBType], [F_BigZoneID], [F_BattleZoneID], [F_DBIP], [F_DBPort], [F_DBName], [F_DBUser], [F_DBPSW], [F_Note], [F_State]) VALUES (  2, 0, 1, N'192.168.1.224', N'1134',N'GameCoreDB', N'sa', N'5225qs5a5a#', N'Sql_20170814', 1)
INSERT [dbo].[T_BaseParamDB] ( [F_DBType], [F_BigZoneID], [F_BattleZoneID], [F_DBIP], [F_DBPort], [F_DBName], [F_DBUser], [F_DBPSW], [F_Note], [F_State]) VALUES (  5, 0, 1, N'192.168.1.224', N'1134',N'UserCoreDB', N'sa', N'5225qs5a5a#', N'Sql_20170814', 1)

然后在odbc中创建相应的odbc实例名（dbname+F_BattleZoneID）
注：此处设计将增大维护工作量，建议将存储过程中[_Init_LinkServerAndJob]该四个系统共用的库连接实例@datasrc设置为统一共用，如此只需要添加战区数据到T_BaseParamDB即可【尚未如此处理】
二.统计
1、宠物统计
宠物等级数据只有在对宠物张经验时才会存储，
Other_log ：Opid=50137  and param1=0
Other_log ：Opid=50137 and param<>0 时不会存储宠物的等级数据
三、库缺陷
1.缺少表T_Trade_GSLog_Rank,T_ShopSaleCount[商城统计 购买次数排行],T_CountDetail [推广 系统分辨率统计]
2.需要第三方提供数据
1）T_BaseGameTask 需要策划提供基础数据 游戏任务数据
2）T_BaseIPAddress需要用户提供IP地址库
四、bug修复
1.由于系统中GameLogDB，MainDB，GameCoreDB，UserCoreDB是系统唯一的，因此在存储过程中_Dig_GameLogDB，_Dig_GameCoreDB获取连接服务器名称时需要过滤T_BaseParamDB 中的代表全部战区的ID项


五、在查看邮件数据时发现在MySQL中数据存储存在差异性

select * from openquery(LKSV_GSS224__0_GSLOG_DB_0_1,
'select * from  2017_08_28_other_log   where Opid=10071')--存储邮件数据 含有的是空格
六、待恢复功能
1.在商城中对于道具进行操作   Shop\ShopBaseItemImport.aspx路径下（ ShopBaseItemImport->ShopBaseItemList-ShopItemAdd）
