using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;
using GSS.DBUtility;
using GSSCSFrameWork;
using System.Globalization;
using MySql.Data.MySqlClient;
using GSSModel.Request;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
namespace GSSWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServiceXLJ : System.Web.Services.WebService
    {
        string BigZoneID = ConfigurationManager.AppSettings["BigZoneID"];//对应的大区编号
        string RemoteServerIP = ConfigurationManager.AppSettings["RemoteServerIP"];//对应的大区编号
        string ConnStrUserCoreDB
        {
            get
            {
                return GetConnnectionString("UserCoreDB", "ConnStrUserCoreDB");
            }
        } //= PubConstant.GetConnectionString("ConnStrUserCoreDB");//用户数据库连接字符串
        string ConnStrGameCoreDB
        {
            get
            {
                return GetConnnectionString("GameCoreDB", "ConnStrGameCoreDB");
            }
        }// PubConstant.GetConnectionString("ConnStrGameCoreDB");//游戏数据库连接字条串
        string ConnStrGSSDB = PubConstant.GetConnectionString("ConnStrGSSDB");//gssdb 客服库连接串
        string language = ConfigurationManager.AppSettings["AppLanguage"];
        log4net.ILog Log = log4net.LogManager.GetLogger("GSSLog");
        string GetConnnectionString(string dbName, string configItem)
        {//依赖条件 GSSDB连接串可用
            bool connStringFromDB = ConfigurationManager.AppSettings["GameDBStringFromDB"] == "true";
            string.Format("[GetConnnectionString]->{0}\t{1}", dbName, configItem).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
            if (!connStringFromDB)
            {
                string conn = PubConstant.GetConnectionString(configItem);
                string.Format("webconfig:{0}", conn).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                return conn;
            }
            DbHelperSQLP db = new DbHelperSQLP(ConnStrGSSDB);
            try
            {
                SqlParameter param = new SqlParameter("@dbName", SqlDbType.VarChar, 30) { Value = dbName };
                DataSet ds = db.RunProcedure("SP_QueryGameDBConnString", new SqlParameter[] { param }, typeof(ServiceXLJ).Name);
                string dbConnString = string.Empty;
                if (ds == null || ds.Tables.Count == 0)
                {
                    string.Format("db conn:null").WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    return null;
                }
                DataRowCollection rows = ds.Tables[0].Rows;
                if (rows.Count == 0)
                {
                    return null;
                }
                object obj = rows[0]["provider_string"];
                dbConnString = obj == null ? string.Empty : obj.ToString();
                string.Format("db conn:{0}", dbConnString).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                return dbConnString;
            }
            catch (Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
            }
            return string.Empty;
        }
        [WebMethod]
        public string HelloWorld()
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            Log.Warn("执行了测试方法HelloWorld,请确认为合法访问,请求IP" + GetIP4Address() + " ");
            return "Hello World";
        }

        [WebMethod(Description = "得到游戏用户信息列表,客服主页使用")]
        public byte[] GetGameUsersC(string query)
        {

            string.Format("Cmd ->[GetGameUsersC] -> {0}", query).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                string tip = "非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ";
                tip.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                Log.Warn(tip);
                return null;
            }
            try
            {
                string sql = @"SELECT top 10 a.F_UserID, a.F_UserName,F_CreatTime,F_ActiveTime,F_IsProtect,F_IsAdult,F_IsLock,F_Level,F_LastLoginIP,(select count(1) from T_UserLoged with(nolock) where F_UserID=a.F_UserID ) as F_OnLine, F_PersonID FROM T_User as a with(nolock) left join (select * from T_UserState0 with(nolock)
union all
select * from T_UserState1  with(nolock)
union all
select * from T_UserState2  with(nolock)
union all
select * from T_UserState3  with(nolock)
union all
select * from T_UserState4  with(nolock)
) b on a.F_UserID=b.F_UserID left join T_UserExInfo c on a.F_UserID=c.F_UserID where 1=1  " + query;
                DbHelperSQLP sp = new DbHelperSQLP(ConnStrUserCoreDB);
                DataSet ds = sp.Query(sql);

                //     ds.Tables[0].Columns.Add("F_BigZoneName", System.Type.GetType("System.String"), BigZoneID);

                DataTable dtnew = ds.Tables[0].Clone();
                dtnew.TableName = "dtnew";

                dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
                int colCount = dtnew.Columns.Count;
                for (int i = 1; i < colCount; i++)
                {
                    dtnew.Columns[i].DataType = System.Type.GetType("System.String");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DataRow drnew = dtnew.NewRow();

                    for (int i = 0; i < colCount; i++)
                    {
                        drnew[i] = dr[i].ToString().Trim();
                    }
                    //是否
                    string[] drdics = { "F_IsProtect", "F_IsAdult", "F_IsLock" };
                    foreach (string dic in drdics)
                    {
                        drnew[dic] = WSUtil.YesOrNO(drnew[dic].ToString());
                    }

                    //是否
                    string[] dros = { "F_OnLine" };
                    foreach (string dio in dros)
                    {
                        drnew[dio] = WSUtil.IsOnLine(drnew[dio].ToString());
                    }
                    //时间
                    string[] drdates = { "F_CreatTime", "F_ActiveTime" };
                    foreach (string date in drdates)
                    {
                        try
                        {
                            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
                        }
                        catch (System.Exception ex)
                        {
                            drnew[date] = "";
                            //日志记录
                            Log.Warn(ex);
                        }

                    }
                    dtnew.Rows.Add(drnew);

                }
                ds.Tables.Clear();
                ("rows:\t" + dtnew.Rows.Count).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                ds.Tables.Add(dtnew);
                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                Log.Error("GetGameUsersC>>游戏数据操作错误", ex);
                return null;
            }

        }

        [WebMethod(Description = "得到游戏角色信息列表,客服主页使用")]
        public byte[] GetGameRoleCR(string whereStr)
        {
            string.Format("Cmd ->[GetGameRoleCR] -> {0}", whereStr).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                string tip = "非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ";
                tip.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                Log.Warn(tip);
                return null;
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_0 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_0 as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                strSql.Append(" union all ");
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_1 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_1 as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                strSql.Append(" union all ");
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_2 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_2 as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                strSql.Append(" union all ");
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_3 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_3 as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                strSql.Append(" union all ");
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_4 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_4 as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                strSql.Append(" union all ");
                strSql.Append("select top 30 F_UserID,F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_0 with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock ,1 as F_RowState ");
                strSql.Append(" FROM T_RoleBaseDataDeleted as a");
                strSql.Append(" with(nolock) where 1=1 " + whereStr + " ");

                DbHelperSQLP sp = new DbHelperSQLP(ConnStrGameCoreDB);
                DataSet ds = sp.Query(strSql.ToString());

                DataTable dtnew = ds.Tables[0].Clone();
                dtnew.TableName = "dtnew";

                dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
                int colCount = dtnew.Columns.Count;
                for (int i = 1; i < colCount; i++)
                {
                    dtnew.Columns[i].DataType = System.Type.GetType("System.String");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DataRow drnew = dtnew.NewRow();

                    for (int i = 0; i < colCount; i++)
                    {
                        drnew[i] = dr[i].ToString().Trim();
                    }

                    //是否
                    string[] drdics = { "F_IsLock" };
                    foreach (string dic in drdics)
                    {
                        drnew[dic] = WSUtil.YesOrNO(drnew[dic].ToString());
                    }

                    //是否
                    string[] dronline = { "F_OnLine" };
                    foreach (string dic in dronline)
                    {
                        drnew[dic] = WSUtil.IsOnLine(drnew[dic].ToString());
                    }
                    drnew["F_RowState"] = drnew["F_RowState"].ToString() == "0" ? LanguageResource.Language.Tip_NormalStatue : LanguageResource.Language.Tip_DeleteStatue;
                    //时间
                    string[] drdates = { "F_CreateTime", "F_UpdateTime" };
                    foreach (string date in drdates)
                    {
                        try
                        {
                            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
                        }
                        catch (System.Exception ex)
                        {
                            drnew[date] = "";
                            //日志记录
                            Log.Warn(ex);
                        }

                    }
                    dtnew.Rows.Add(drnew);
                }

                ds.Tables.Clear();
                if (dtnew != null)
                {
                    (string.Format("Query role:[{0}]", dtnew.Rows.Count)).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                }
                else
                {
                    (string.Format("Query role:null")).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                }
                ds.Tables.Add(dtnew);
                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                Log.Error("GetGameRoleC>>游戏数据操作错误", ex);
                return null;
            }

        }

        [WebMethod(Description = "得到游戏角色信息列表,客服主页使用")]
        public byte[] GetGameRoleC(string userid)
        {
            string.Format("cmd:[GetGameRoleC]->userid={0}", userid);
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return null;
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 30 F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_" + Convert.ToInt32(userid) % 5 + " with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,0 as F_RowState  ");
                strSql.Append(" FROM T_RoleBaseData_" + Convert.ToInt32(userid) % 5 + " as a");
                strSql.Append(" with(nolock) where F_UserID =" + userid + " ");

                strSql.Append(" union all select top 30 F_RoleID,F_RoleName,F_ZoneID,(select top 1  F_ZoneName from T_BattleZone with(nolock)  where F_ZoneID =a.F_ZoneID) as F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(select count(1) from T_LogInRole_" + Convert.ToInt32(userid) % 5 + " with(nolock) where F_RoleID=a.F_RoleID ) as F_OnLine,(select  count(1) from T_LockRole with(nolock) where F_RoleID=a.F_RoleID ) as F_IsLock,1 as F_RowState   ");
                strSql.Append(" FROM T_RoleBaseDataDeleted as a");
                strSql.Append(" with(nolock) where F_UserID =" + userid + " ");

                DbHelperSQLP sp = new DbHelperSQLP(ConnStrGameCoreDB);
                DataSet ds = sp.Query(strSql.ToString());

                DataTable dtnew = ds.Tables[0].Clone();
                dtnew.TableName = "dtnew";

                dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
                int colCount = dtnew.Columns.Count;
                for (int i = 1; i < colCount; i++)
                {
                    dtnew.Columns[i].DataType = System.Type.GetType("System.String");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DataRow drnew = dtnew.NewRow();

                    for (int i = 0; i < colCount; i++)
                    {
                        drnew[i] = dr[i].ToString().Trim();
                    }

                    //是否
                    string[] drdics = { "F_IsLock" };
                    foreach (string dic in drdics)
                    {
                        drnew[dic] = WSUtil.YesOrNO(drnew[dic].ToString());
                    }

                    //是否
                    string[] dronline = { "F_OnLine" };
                    foreach (string dic in dronline)
                    {
                        drnew[dic] = WSUtil.IsOnLine(drnew[dic].ToString());
                    }
                    drnew["F_RowState"] = drnew["F_RowState"].ToString() == "1" ? LanguageResource.Language.Tip_DeleteStatue : LanguageResource.Language.Tip_NormalStatue;
                    //时间
                    string[] drdates = { "F_CreateTime", "F_UpdateTime" };
                    foreach (string date in drdates)
                    {
                        try
                        {
                            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
                        }
                        catch (System.Exception ex)
                        {
                            drnew[date] = "";
                            //日志记录
                            Log.Warn(ex);
                        }

                    }
                    dtnew.Rows.Add(drnew);
                }

                ds.Tables.Clear();
                ds.Tables.Add(dtnew);
                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                Log.Error("GetGameRoleC>>游戏数据操作错误", ex);
                return null;
            }

        }

        [WebMethod(Description = "得到战区战线列表,公告工单使用")]
        public byte[] GetZoneLine()
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return null;
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT F_ZoneID AS F_GID, F_ZoneName AS F_GName, 0 AS F_GParentID FROM T_BattleZone WHERE (F_ZoneState = 1) UNION ALL ");
                //strSql.Append(" SELECT F_NGSID AS F_GID, F_Name AS F_GName, F_ZoneID AS F_GParentID FROM T_BattleLine WHERE (F_State = 1)");

                strSql.Append("SELECT F_ZoneID AS F_GID, F_ZoneName AS F_GName, 0 AS F_GParentID FROM T_BattleZone with(nolock) UNION ALL ");
                strSql.Append(" SELECT F_NGSID AS F_GID, F_Name AS F_GName, F_ZoneID AS F_GParentID FROM T_BattleLine with(nolock) ");


                DbHelperSQLP sp = new DbHelperSQLP(ConnStrGameCoreDB);
                DataSet ds = sp.Query(strSql.ToString());

                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                Log.Error("GetZoneLine>>游戏数据操作错误", ex);
                return null;
            }
        }

        [WebMethod(Description = "游戏工具:封停用户")]
        public string SetUserLock(string userid, string username, string locktype, string locktime)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (userid.Trim() != string.Empty && username.Trim() != string.Empty && locktype.Trim() != string.Empty)
                {
                    DateTime lockstarttime = DateTime.Now;
                    DateTime lockendtime = lockstarttime;

                    lockendtime = lockendtime.AddHours(Convert.ToInt32(locktime));

                    int result = 0;

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC _WSS_User_LockTime ");
                    strSql.Append("@F_UserID,@F_UserName,@F_LockType,@F_LockStartTime,@F_LockEndTime,@Result OUTPUT");
                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@F_UserName", SqlDbType.NChar),
					new SqlParameter("@F_LockType", SqlDbType.Int),
					new SqlParameter("@F_LockStartTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_LockEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Result", SqlDbType.Char,26)};
                    parameters[0].Value = userid;
                    parameters[1].Value = username;
                    parameters[2].Value = LockTypeIntFromStr(locktype);
                    parameters[3].Value = lockstarttime;
                    parameters[4].Value = lockendtime;
                    parameters[5].Direction = ParameterDirection.Output;

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    result = Convert.ToInt32(parameters[5].Value);

                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else if (result == 2015)
                    { //用户之前已被封停
                        Log.Error("游戏工具:封停用户>>该账户处于封停状态。[_WSS_User_LockTime]\r\n" + userid + "\t" + username + "\t");
                        return result.ToString();
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);
                    }

                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:封停用户>>游戏数据操作错误", ex);
            }

            return returnValue;
        }

        private int LockTypeIntFromStr(string value)
        {
            InitLanguage();
            try
            {
                value = value.Substring(value.Length - 3, 3);
                return Convert.ToInt32(value);
            }
            catch (System.Exception ex)
            {

                Log.Error("LockTypeIntFromStr>>游戏数据操作错误", ex);
                return 0;
            }

        }

        [WebMethod(Description = "游戏工具:封停角色")]
        public string SetRoleLock(string roleid, string rolename, string locktype, string locktime)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (roleid.Trim() != string.Empty && rolename.Trim() != string.Empty && locktype.Trim() != string.Empty)
                {
                    DateTime lockstarttime = DateTime.Now;
                    DateTime lockendtime = lockstarttime;

                    lockendtime = lockendtime.AddHours(Convert.ToInt32(locktime));


                    int rowcount = 0;
                    int result = 0;
                    TimeSpan ts = lockendtime - lockstarttime;
                    int LockMinute = Convert.ToInt32(ts.TotalMinutes);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_DBIS_Role_LockAdd ");
                    strSql.Append("@F_RoleID,@F_RoleName,@F_LockType,@LockMinute,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,10),
					new SqlParameter("@F_RoleName", SqlDbType.NChar,20),
					new SqlParameter("@F_LockType", SqlDbType.Int),
					new SqlParameter("@LockMinute", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = roleid;
                    parameters[1].Value = rolename;
                    parameters[2].Value = LockTypeIntFromStr(locktype);
                    parameters[3].Value = LockMinute;
                    parameters[4].Direction = ParameterDirection.Output;



                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    rowcount = sp.ExecuteSql(strSql.ToString(), parameters);
                    result = Convert.ToInt32(parameters[4].Value);
                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);

                    }


                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:封停角色>>游戏数据操作错误", ex);
            }

            return returnValue;

        }

        [WebMethod(Description = "游戏工具:解封用户")]
        public string SetUserNoLock(string userid)
        {
            string.Format("SetUserNoLock").WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "false";
            try
            {
                if (userid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_LockDel ");
                    strSql.Append("@F_UserID,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = userid;
                    parameters[1].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[1].Value);
                    string.Format("deal with response code:[{0}]", result).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);
                        string.Format("array error statue code:[{0}]", returnValue).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    }


                }
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:解封用户>>游戏数据操作错误", ex);
            }

            return returnValue;

        }


        [WebMethod(Description = "游戏工具:解封角色")]
        public string SetRoleNoLock(string roleid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "false";
            try
            {
                if (roleid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_DBIS_Role_LockDel ");
                    strSql.Append("@F_RoleID,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = roleid;
                    parameters[1].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[1].Value);
                    string.Format("SetRoleNoLock-> code :[{0}]", result).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);
                        string.Format("GameCoreDB response error code :[{0}]", returnValue).WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:解封角色>>游戏数据操作错误", ex);
            }

            return returnValue;

        }

        [WebMethod(Description = "游戏工具:获取用户或角色封停信息")]
        public string GetLockInfo(string userid, string roleid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (userid.Trim() != string.Empty && roleid.Trim() == string.Empty)
                {

                    string sql = @"SELECT top 1 F_UserName, F_LockStartTime, F_LockEndTime, F_LockIPBegin,F_LockIPEnd, F_LockType FROM  T_LockUserLog with(nolock) WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    DataSet ds = sp.Query(sql);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        returnValue = "编号:" + userid + " 用户:" + dr["F_UserName"].ToString() + " \n封停类型:" + dr["F_LockType"].ToString() + " \n封停开始时间:" + dr["F_LockStartTime"].ToString() + " 封停结束时间:" + dr["F_LockEndTime"].ToString() + "";
                    }
                    else
                    {
                        returnValue = LanguageResource.Language.Tip_NoCloseDownInfo;
                    }

                }
                if (roleid.Trim() != string.Empty)
                {

                    string sql = @"SELECT F_RoleName, F_LockType, F_LockStartTime, F_LockEndTime, F_LockIp FROM T_LockRole with(nolock) WHERE (F_RoleID = " + roleid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    DataSet ds = sp.Query(sql);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        returnValue = "编号:" + userid + " 角色:" + dr["F_RoleName"].ToString() + "\n 封停类型:" + dr["F_LockType"].ToString() + " \n封停开始时间:" + dr["F_LockStartTime"].ToString() + " 封停结束时间:" + dr["F_LockEndTime"].ToString() + " ";
                    }
                    else
                    {
                        returnValue = LanguageResource.Language.Tip_NoCloseDownInfo;
                    }

                }
                if (returnValue.Trim() == string.Empty)
                {
                    returnValue = LanguageResource.Language.Tip_NoCloseDownInfo;
                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GetCloseDownFailure;
                Log.Error("游戏工具:获取用户或角色封停信息:游戏数据操作错误", ex);
            }

            return returnValue;


        }

        [WebMethod(Description = "游戏工具:帐号借用")]
        public string SetUserGMUse(string userid, string newuserpsw, string gmname)
        {
            InitLanguage();
            if (VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {
                    newuserpsw = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newuserpsw, "MD5").ToLower();

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_GMUse ");
                    strSql.Append("@F_UserID,@NewUserPSW,@F_GMName,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
                    new SqlParameter("@NewUserPSW", SqlDbType.NVarChar),
                    new SqlParameter("@F_GMName", SqlDbType.NVarChar),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = userid;
                    parameters[1].Value = newuserpsw;
                    parameters[2].Value = gmname;
                    parameters[3].Direction = ParameterDirection.Output;

                    strSql.ToString().WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.connectionString.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[3].Value);
                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.WebLogger("GSSWebService." + typeof(ServiceXLJ).Name);
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:帐号借用:游戏数据操作错误", ex);
            }

            return returnValue;
        }


        [WebMethod(Description = "游戏工具:帐号归还")]
        public string SetUserGMBack(string userid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_GMBack ");
                    strSql.Append("@F_UserID,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = userid;
                    parameters[1].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[1].Value);
                    if (result == 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = GetErroStr(sp, result);
                    }
                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:帐号归还:游戏数据操作错误", ex);
            }

            return returnValue;
        }

        [WebMethod(Description = "游戏工具:角色名更改")]
        public string RoleNameChange(int userid, int roleid, string rolename, string newrolename)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("EXEC	_WSS_Role_RoleNameChange" + userid % 5 + " ");
                strSql.Append("@F_RoleID,@F_RoleName,@NewRoleName,@Result OUTPUT");

                SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int),
                    new SqlParameter("@F_RoleName", SqlDbType.NVarChar,20),
                    new SqlParameter("@NewRoleName", SqlDbType.NVarChar,20),
					new SqlParameter("@Result", SqlDbType.Int)};
                parameters[0].Value = roleid;
                parameters[1].Value = rolename;
                parameters[2].Value = newrolename;
                parameters[3].Direction = ParameterDirection.Output;


                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                sp.ExecuteSql(strSql.ToString(), parameters);
                int result = Convert.ToInt32(parameters[3].Value);
                if (result == 0)
                {
                    returnValue = "操作成功";
                }
                else
                {
                    returnValue = "操作失败:" + GetErroStr(sp, result);
                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError + ex.Message;
                Log.Error("游戏工具:角色名更改:游戏数据操作错误", ex);
            }
            return returnValue;
        }

        [WebMethod(Description = "游戏工具:角色战区更改")]
        public string RoleZoneChange(int userid, int roleid, int zoneid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("EXEC	_WSS_Role_ZoneChange" + userid % 5 + " ");
                strSql.Append("@F_RoleID,@F_ZoneID,@Result OUTPUT");

                SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int),
                    new SqlParameter("@F_ZoneID", SqlDbType.Int),
					new SqlParameter("@Result", SqlDbType.Int)};
                parameters[0].Value = roleid;
                parameters[1].Value = zoneid;
                parameters[2].Direction = ParameterDirection.Output;


                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                sp.ExecuteSql(strSql.ToString(), parameters);
                int result = Convert.ToInt32(parameters[2].Value);
                if (result == 0)
                {
                    returnValue = "操作成功";
                }
                else
                {
                    returnValue = "操作失败:" + GetErroStr(sp, result);
                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError + ex.Message;
                Log.Error("游戏工具:角色战区更改:游戏数据操作错误", ex);
            }
            return returnValue;
        }

        [WebMethod(Description = "游戏工具:帐号角色在线状态清除")]
        public string UserRoleClearOnline(int userid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("EXEC	_WSS_User_OffLine ");
                strSql.Append("@F_UserID,@Result OUTPUT");

                SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@Result", SqlDbType.Int)};
                parameters[0].Value = userid;
                parameters[1].Direction = ParameterDirection.Output;


                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrUserCoreDB;
                sp.ExecuteSql(strSql.ToString(), parameters);
                int result = Convert.ToInt32(parameters[1].Value);
                if (result == 0)
                {
                    StringBuilder strSqlR = new StringBuilder();
                    strSqlR.Append("EXEC	_WSS_Role_OffLine" + userid % 5 + " ");
                    strSqlR.Append("@F_UserID,@Result OUTPUT");

                    SqlParameter[] parametersR = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@Result", SqlDbType.Int)};
                    parametersR[0].Value = userid;
                    parametersR[1].Direction = ParameterDirection.Output;

                    DbHelperSQLP spR = new DbHelperSQLP();
                    spR.connectionString = ConnStrGameCoreDB;
                    spR.ExecuteSql(strSqlR.ToString(), parametersR);
                    int resultR = Convert.ToInt32(parametersR[1].Value);
                    if (resultR == 0)
                    {
                        returnValue = "操作成功";
                    }
                    else
                    {
                        returnValue = "清除帐号在线信息成功,但角色操作失败:" + GetErroStr(sp, result);
                    }

                }
                else
                {
                    returnValue = "操作失败:" + GetErroStr(sp, result);
                }
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError + ex.Message;
                Log.Error("游戏工具:角色战区更改:游戏数据操作错误", ex);
            }
            return returnValue;
        }

        [WebMethod(Description = "游戏工具:清除防沉迷信息")]
        public string ClearChildDisInfo(string userid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"delete FROM T_ChildUser WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        returnValue = "true";
                    }
                    else
                    {
                        returnValue = "没有防沉迷信息,不需要清空";
                    }

                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:清除防沉迷信息:游戏数据操作错误", ex);
            }

            return returnValue;


        }


        [WebMethod(Description = "游戏工具:喊话工具>>停止公告")]
        public string GameNoticeStop(string taskid)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            try
            {
                if (taskid.Trim() != string.Empty)
                {
                    string sql = @"UPDATE T_GameNotice SET F_TaskState = 0 WHERE (F_TaskID = " + taskid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    int rowcount = sp.ExecuteSql(sql);
                    returnValue = "true";
                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("戏工具:喊话工具>>停止公告:游戏数据操作错误", ex);
            }

            return returnValue;
        }

        [WebMethod(Description = "游戏工具:喊话工具>>开始公告")]
        public string GameNoticeStart(string taskid, string area, string msg)
        {

            string.Format("taskId:[{0}], notice message:[{1}],area:[{2}]", taskid, msg, area);
            if (string.IsNullOrEmpty(msg))
            {
                return LanguageResource.Language.Tip_NoticeMsgIsRequired;
            }
            msg = msg.Trim();
            if (msg.Length > 300)
            {
                return LanguageResource.Language.Tip_LimitNoticeMsgLength;
            }
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            int rowcount = 0;
            string sql = "";
            DbHelperSQLP sp = new DbHelperSQLP();
            string big = "select F_ID, F_Name,F_ValueGame from GSSDB.dbo.T_GameConfig where F_ParentID=1000 and F_IsUsed=1";
            sp.connectionString = ConnStrGSSDB;
            DataTable table = sp.Query(big).Tables[0];
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                if (taskid.Trim() != string.Empty && area.Trim() != string.Empty && msg.Trim() != string.Empty)
                {
                    sql = @"select 1 FROM T_GameNotice with(nolock) WHERE (F_TaskID = " + taskid + ") and F_TaskState = 1";
                    rowcount = sp.ExecuteSql(sql);
                    if (rowcount > 0)
                    {
                        return "此工单下的公告正在运行,请停止后再试";
                    }
                    sql = @"delete FROM T_GameNotice WHERE (F_TaskID = " + taskid + ")";
                    rowcount = sp.ExecuteSql(sql);
                    string[] msgAs = msg.Split('\n');
                    string[] areaAs = area.Split('|');
                    foreach (string areaA in areaAs)
                    {
                        string[] areaBs = areaA.Split(',');
                        string bigid = areaBs[0];
                        if (table.Select("F_ValueGame=" + bigid).Length > 0)//BigZoneID == areaBs[0]
                        {
                            foreach (string msgA in msgAs)
                            {
                                string[] msgBs = msgA.Split('|');
                                sql = @"INSERT INTO T_GameNotice (F_ReciveZone, F_ReciveLine, F_ReciveObject, F_MSGLocation, F_Message, F_RunTimeBegin, F_RunTimeEnd, F_RunInterval, F_TaskState,F_TaskID, F_NoticeTimes)
VALUES     (" + areaBs[1] + ", " + areaBs[2] + ", '" + msgBs[1] + "', " + msgBs[2] + ", N'" + msgBs[0] + "', '" + msgBs[3] + "', '" + msgBs[4] + "', " + msgBs[5] + ", 1, " + taskid + ", 0)";
                                rowcount = sp.ExecuteSql(sql);
                            }
                        }
                    }
                    returnValue = "true";
                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:喊话工具>>开始公告:游戏数据操作错误" + sql, ex);

                try
                {
                    sql = @"delete FROM T_GameNotice WHERE (F_TaskID = " + taskid + ") and F_TaskState = 0";
                    rowcount = sp.ExecuteSql(sql);
                }
                catch (System.Exception exx)
                {
                    returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                    Log.Error("游戏工具:喊话工具>>开始公告:游戏数据操作错误", exx);
                }

            }

            return returnValue;
        }

        [WebMethod(Description = "游戏工具:发奖工具")]
        public string GameGiftAwardDo(string taskid, string giftid, string gifttype, string giftnum, byte[] byteuser)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            int rowcount = 0;
            string sql = "";
            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                if (taskid.Trim() != string.Empty && giftid.Trim() != string.Empty && gifttype.Trim() != string.Empty && giftnum.Trim() != string.Empty && byteuser != null)
                {

                    sql = @"select 1 FROM T_GiftAward_List with(nolock) WHERE (F_TaskID = " + taskid + ")";
                    DataSet ds = sp.Query(sql);
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        return "此工单已经执行过发奖";
                    }

                    DataSet dsuser = GSSCSFrameWork.DataSerialize.GetDatasetFromByte(byteuser);
                    if (dsuser == null || dsuser.Tables[0].Rows.Count == 0)
                    {
                        return "发奖用户列表为空";
                    }

                    sql = @"insert into T_GiftAward_List (F_AwardName, F_Note,F_State, F_ExecType,F_JobTime,F_TaskID) VALUES (N'GSS发奖 工单:" + taskid + " (自动)', N'作业10分钟后自动执行发奖',1, 1,'" + DateTime.Now.AddMinutes(10) + "'," + taskid + ") ;select @@IDENTITY";
                    object obj = sp.GetSingle(sql);
                    if (obj == null)
                    {
                        return "WEBSERVICE:奖单建立失败";
                    }
                    else
                    {
                        //加入奖品
                        sql = @"insert into T_GiftAward_Gift (F_AwardID, F_GiftID, F_GiftName, F_GiftNum, F_TaskID) VALUES (" + obj.ToString() + ", " + giftid + ", N'" + gifttype + "'," + giftnum + ", " + taskid + ")";
                        sp.ExecuteSql(sql);
                        //加入用户
                        DataTable dtuser = dsuser.Tables[0];

                        while (dtuser.Columns.Count > 3)
                        {
                            dtuser.Columns.RemoveAt(3);
                        }
                        dtuser.Columns.Add("F_AwardID", typeof(int), obj.ToString());
                        dtuser.Columns.Add("F_TaskID", typeof(int), taskid);
                        dtuser.Columns[0].ColumnName = "F_UserID";
                        dtuser.Columns[1].ColumnName = "F_RoleID";
                        dtuser.Columns[2].ColumnName = "F_ZoneID";
                        dtuser.Columns[3].DefaultValue = Convert.ToInt32(obj);
                        dtuser.Columns[4].DefaultValue = Convert.ToInt32(taskid);
                        //写入[GameCoreDB].dbo.T_GiftAward_User	
                        CopyAwardUserData(dtuser);

                        sql = @"DECLARE	@Result int
EXEC	[dbo].[_WSS_GiftAward_List_Job]
		@F_AwardID = " + obj.ToString() + ",@Result = @Result OUTPUT SELECT	@Result";
                        obj = sp.GetSingle(sql);
                        if (obj == null || obj.ToString() != "0")
                        {
                            return "创建自动执行出现错误" + WSUtil.ConvertNull(obj);
                        }

                    }

                    returnValue = "true";
                }
                else
                {
                    return "WEBSERVICE传入参数不正确";
                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:发奖工具:游戏数据操作错误" + sql, ex);

                try
                {
                    sql = @"delete FROM T_GiftAward_List WHERE (F_TaskID = " + taskid + ") ";
                    rowcount = sp.ExecuteSql(sql);
                }
                catch (System.Exception exx)
                {
                    returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                    Log.Error("游戏工具:发奖工具:游戏数据操作错误", exx);
                }

            }

            return returnValue;
        }


        [WebMethod(Description = "游戏工具:发奖工具new")]
        public string GameGiftAwardDoFor5Num(string taskid, string giftstr, byte[] byteuser)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            int rowcount = 0;
            string sql = "";
            DbHelperSQLP sp = new DbHelperSQLP();

            //查询 邮件标题,发件人,内容信息插入 T_GiftAward_List 供 存储过程使用
            string resGSSDB = "select   F_Title,F_GPeopleName,F_Note,F_PreDutyMan  FROM T_Tasks  WITH(NOLOCK)  where(F_ID = " + taskid + ")";
            sp.connectionString = ConnStrGSSDB;
            DataSet dsGSSDB = sp.Query(resGSSDB);

            //开始操作GameCoreDB
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                if (taskid.Trim() != string.Empty && giftstr.Trim() != string.Empty && byteuser != null)
                {

                    sql = @"select 1 FROM T_GiftAward_List with(nolock) WHERE (F_TaskID = " + taskid + ")";
                    DataSet ds = sp.Query(sql);
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        return "此工单已经执行过发奖";
                    }

                    DataSet dsuser = GSSCSFrameWork.DataSerialize.GetDatasetFromByte(byteuser);
                    if (dsuser == null || dsuser.Tables[0].Rows.Count == 0)
                    {
                        return "发奖用户列表为空";
                    }

                    sql = @"insert into T_GiftAward_List (F_AwardName, F_Note,F_State,F_CreateTime, F_ExecType,F_JobTime,F_TaskID) VALUES (N'GSS发奖 工单:" + taskid + " (自动)', N'作业10分钟后自动执行发奖',1, '"+DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss")+"' ,1,'" + DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "'," + taskid + ") ;select @@IDENTITY";
                    object obj = sp.GetSingle(sql);
                    if (obj == null)
                    {
                        return "WEBSERVICE:奖单建立失败";
                    }
                    else
                    {
                        string[] gifts = giftstr.Split('|');

                        //if (gifts.Length != 12)
                        //{
                        //    return "工单中奖品相关数据不正确";
                        //}
                        //加入奖品
                        sql = @"insert into T_GiftAward_Gift ([F_AwardID],[F_ItemID1],[F_ItemNum1],[F_ItemID2],[F_ItemNum2],[F_ItemID3],[F_ItemNum3],[F_ItemID4],[F_ItemNum4],[F_ItemID5],[F_ItemNum5],[F_TaskID],F_MailTitle,F_MailSendName,F_MailContent,[F_Gold],[F_BindGold]) VALUES (" + obj.ToString() + ", " + gifts[0] + "," + gifts[1] + "," + gifts[2] + "," + gifts[3] + "," + gifts[4] + "," + gifts[5] + "," + gifts[6] + "," + gifts[7] + "," + gifts[8] + "," + gifts[9] + ", " + taskid + ",N'" + dsGSSDB.Tables[0].Rows[0]["F_Title"] + "',N'" + dsGSSDB.Tables[0].Rows[0]["F_GPeopleName"] + "',N'" + dsGSSDB.Tables[0].Rows[0]["F_Note"] + "'," + gifts[11] + "," + gifts[10] + ")";
                        sp.ExecuteSql(sql);
                        //加入用户
                        string strSql = string.Empty;
                        if (dsGSSDB.Tables[0].Rows[0]["F_PreDutyMan"].ToString() == "0")
                        {
                            strSql = "SELECT F_UserID,F_RoleID,-1 F_ZoneID FROM dbo.T_RoleCreate WHERE F_RoleID IN";
                        }
                        else
                        {
                            strSql = "SELECT F_UserID,F_RoleID,-1 F_ZoneID FROM dbo.T_RoleCreate WHERE F_RoleName IN";
                        }
                        DataTable dtuser = dsuser.Tables[0];
                        string startTemp = "(";
                        string endTemp = ")";
                        for (int i = 0; i < dtuser.Rows.Count;i++ )
                        {
                            startTemp += "N'" + dtuser.Rows[i][0] + "',";
                        }
                        startTemp = startTemp.Substring(0, startTemp.Length - 1);
                        startTemp += endTemp;
                        strSql += startTemp;
                        DataSet dsRole = sp.Query(strSql);
                        DataTable dtRole = dsRole.Tables[0];

                        dtRole.Columns.Add("F_AwardID", typeof(int), obj.ToString());
                        dtRole.Columns.Add("F_TaskID", typeof(int), taskid);
                        dtRole.Columns[0].ColumnName = "F_UserID";
                        dtRole.Columns[1].ColumnName = "F_RoleID";
                        dtRole.Columns[2].ColumnName = "F_ZoneID";
                        dtRole.Columns[3].DefaultValue = Convert.ToInt32(obj);
                        dtRole.Columns[4].DefaultValue = Convert.ToInt32(taskid);
                        //写入[GameCoreDB].dbo.T_GiftAward_User	
                        CopyAwardUserData(dtRole);

                        sql = @"DECLARE	@Result int
EXEC	[dbo].[_WSS_GiftAward_List_Job]
		@F_AwardID = " + obj.ToString() + ",@Result = @Result OUTPUT SELECT	@Result";
                        obj = sp.GetSingle(sql);
                        if (obj == null || obj.ToString() != "0")
                        {
                            return "创建自动执行出现错误" + WSUtil.ConvertNull(obj);
                        }

                    }

                    returnValue = "true";
                }
                else
                {
                    return "WEBSERVICE传入参数不正确";
                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:发奖工具:游戏数据操作错误" + sql, ex);

                try
                {
                    sql = @"delete FROM T_GiftAward_List WHERE (F_TaskID = " + taskid + ") ";
                    rowcount = sp.ExecuteSql(sql);
                }
                catch (System.Exception exx)
                {
                    returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                    Log.Error("游戏工具:发奖工具:游戏数据操作错误", exx);
                }

            }

            return returnValue;
        }

        [WebMethod(Description = "游戏工具:发奖工具")]
        public string GameGiftUserAdd(string userid, string roleid, string zoneid, string giftid, string giftnum, string note)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            string sql = "";
            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                if (userid.Trim() != string.Empty && roleid.Trim() != string.Empty && zoneid.Trim() != string.Empty && giftid.Trim() != string.Empty && giftnum.Trim() != string.Empty && note.Trim() != string.Empty)
                {
                    sql = @"DECLARE @Result int EXEC [dbo].[_WSS_Gift_User_Add] " + userid + "," + roleid + "," + zoneid + "," + giftid + "," + giftnum + ", N'" + note + "',@Result OUTPUT SELECT	@Result";

                    object obj = sp.GetSingle(sql);
                    if (obj == null || obj.ToString() != "0")
                    {
                        return "发奖出错" + WSUtil.ConvertNull(obj);
                    }

                    returnValue = "true";
                }
                else
                {
                    return "WEBSERVICE传入参数不正确";
                }

            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError + ex.Message;
                Log.Error("游戏工具:发奖工具:游戏数据操作错误" + sql, ex);
            }

            return returnValue;
        }


        //得到错误字符串
        protected string GetErroStr(DbHelperSQLP sp, int erroid)
        {
            InitLanguage();
            try
            {
                string sql = "SELECT F_Vaule FROM  T_ErroVal where F_ID=" + erroid + " and F_IsUsed=1";//此处如果操作的是用户账户未被封停则只返回第一次操作错误状态码2016
                return string.Format("{0}:{1}", erroid, sp.GetSingle(sql));
            }
            catch (System.Exception ex)
            {
                Log.Error("得到错误字符串:游戏数据操作错误", ex);
                return "E";
            }
        }

        //发奖用户数据
        /// <summary>
        /// 写入[GameCoreDB].dbo.T_GiftAward_User	
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected string CopyAwardUserData(DataTable dt)
        {
            InitLanguage();
            DateTime beginTime = DateTime.Now;

            //声明数据库连接
            SqlConnection conn = new SqlConnection(ConnStrGameCoreDB);
            conn.Open();
            //声明SqlBulkCopy ,using释放非托管资源
            using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            {
                //一次批量的插入的数据量
                sqlBC.BatchSize = 1000;
                //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                sqlBC.BulkCopyTimeout = 60;

                //设置要批量写入的表
                sqlBC.DestinationTableName = "dbo.T_GiftAward_User";

                //自定义的datatable和数据库的字段进行对应
                sqlBC.ColumnMappings.Add("F_AwardID", "F_AwardID");
                sqlBC.ColumnMappings.Add("F_UserID", "F_UserID");
                sqlBC.ColumnMappings.Add("F_RoleID", "F_RoleID");
                sqlBC.ColumnMappings.Add("F_ZoneID", "F_ZoneID");
                sqlBC.ColumnMappings.Add("F_TaskID", "F_TaskID");

                //批量写入
                sqlBC.WriteToServer(dt);
            }
            conn.Dispose();

            DateTime endTime = DateTime.Now;
            TimeSpan useTime = endTime - beginTime;//使用时间
            return "执行时间：" + useTime.TotalSeconds.ToString() + "秒";
        }

        /// <summary>
        /// 得到IPV4
        /// </summary>
        /// <returns></returns>
        public string GetIP4Address()
        {
            InitLanguage();
            string IP4Address = String.Empty;

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        void InitLanguage()
        {
            if (string.IsNullOrEmpty(language))
            { //查询数据库中的配置项【语言】
                DbHelperSQLP sp = new DbHelperSQLP();
                string big = "select F_value from GSSDB.dbo.T_GameConfig where F_Name='SystemTipLanguage' and F_IsUsed=1";
                sp.connectionString = ConnStrGSSDB;
                language = sp.GetSingle(big) as string;
            }
            if (string.IsNullOrEmpty(language))
            {
                language = "zh-cn";
            }
            CultureInfo culture = new CultureInfo(language);
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            LanguageResource.Language.Culture = culture;
        }
        bool VerifyRemoteServiceIP(string remoteIP)
        {
            bool limit = ConfigurationManager.AppSettings["LimitRemoteServer"] == "true";
            if (!limit) return true;
            if (string.IsNullOrEmpty(RemoteServerIP))
            {
                return true;
            }
            string[] ips = RemoteServerIP.Split(',');
            if (ips.Contains(remoteIP))
            {
                return true;
            }
            return false;
        }
        [WebMethod(Description = "游戏工具:查看全服邮件历史>>删除全服邮件")]
        public string DeleteFullServiceEmail(string taskid, string area, string msg)
        {
            string.Format("taskId:[{0}], notice message:[{1}],area:[{2}]", taskid, msg, area);
            if (string.IsNullOrEmpty(msg))
            {
                return LanguageResource.Language.Tip_NoticeMsgIsRequired;
            }
            msg = msg.Trim();
            if (msg.Length > 300)
            {
                return LanguageResource.Language.Tip_LimitNoticeMsgLength;
            }
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            DbHelperSQLP sp = new DbHelperSQLP();
            string big = "select F_ID, F_Name,F_ValueGame from GSSDB.dbo.T_GameConfig where F_ParentID=1000 and F_IsUsed=1";
            sp.connectionString = ConnStrGSSDB;
            DataTable table = sp.Query(big).Tables[0];

            string strSql = @"select  name,provider_string from sys.servers where name in(select 'LKSV_GSS_7_'+F_DBName+'_'+CONVERT(varchar(3),F_BigZoneID)+'_'+CONVERT(varchar(3),F_BattleZoneID) from dbo.T_BaseParamDB where F_DBType=7 )";//and F_BigZoneID=@bigZone and F_BattleZoneID=@zoneID)";

            DataSet ds = sp.Query(strSql);
            if (ds == null || ds.Tables.Count == 0)
            {
                "DeleteFullServiceEmail:the service lack link server".Logger();
                return "删除全服邮件失败！";
            }

            string mysqlCmd = @"DELETE FROM sys_loss_award_table WHERE DBID=9";
            GSS.DBUtility.DbHelperMySQLP sync = new DbHelperMySQLP();
            StringBuilder sb = new StringBuilder();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                string link = item["name"] as string;
                string conn = item["provider_string"] as string;
                (link + "\t" + conn).Logger();
                if (string.IsNullOrEmpty(conn))
                {
                    (link + " :lose db connString").Logger();
                    sb.AppendLine(string.Format("系统建立的远程数据库MySQL【{0}】连接没有保留连接串", link));
                    continue;
                }
                string filter = FilterMySqlDBConnString(conn);
                DbHelperMySQL.connectionString = filter;
                int succ = DbHelperMySQL.ExecuteSql(mysqlCmd);
                if (succ == 0)
                {
                    sb.AppendLine(link + " :sync error");
                }
            }
            string deleteTask = @"SELECT * FROM dbo.T_Tasks WHERE F_ID=560";
            int res = sp.ExecuteSql(deleteTask);
            returnValue = "true";
            return returnValue;
        }
        static string FilterMySqlDBConnString(string dbConstring)
        {
            string[] filter = new string[] { "driver", "option", "stmt" };
            string[] mysqlConfigs = dbConstring.Split(';');
            List<string> connConfig = new List<string>();
            foreach (string item in mysqlConfigs)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (!filter.Contains(item.Split('=')[0].ToLower()))
                {
                    connConfig.Add(item);
                }
            }
            return string.Join(";", connConfig.ToArray());
        }

        [WebMethod(Description = "角色工单:禁言")]
        public string AddDisChat(string F_GUserID, string F_GRoleID, int F_LimitType)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            DbHelperSQLP sp = new DbHelperSQLP();

            //开始操作GameCoreDB
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                //string sql = string.Format("SELECT F_RoleID FROM [dbo].[T_RoleDisableChat] WHERE F_EndTime>=GETDATE() AND F_RoleID={0}", F_GRoleID);
                //object obj = sp.GetSingle(sql);
                //if(!string.IsNullOrEmpty(obj.ToString()))
                //{
                //    returnValue = "该用户处于禁言状态！";
                //    return returnValue;
                //}
                int LockMinute = GetLimitTime(F_LimitType);
                SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Role_ForbidChatBit",SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@LockMinute", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.SmallInt,4)};
                parameters[0].Value = F_GUserID;
                parameters[1].Value = 4;
                parameters[2].Value = F_GRoleID;
                parameters[3].Value = LockMinute;
                parameters[4].Direction = ParameterDirection.Output;

                DataSet ds = sp.RunProcedure("_DBIS_Role_DisChatAdd", parameters, typeof(ServiceXLJ).Name);

                returnValue = "true";
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:禁言工具:游戏数据操作错误", ex);
            }
            return returnValue;
        }
        private int GetLimitTime(int F_LimitType)
        {
            int LockMinute = 0;
            switch (F_LimitType)
            {
                case 10010400:
                    LockMinute = 30;
                    break;
                case 10010401:
                    LockMinute =60;
                    break;
                case 10010402:
                    LockMinute = 120;
                    break;
                case 10010403:
                    LockMinute = 720;
                    break;
                case 10010404:
                    LockMinute = 1440;
                    break;
                case 10010405:
                    LockMinute = 2880;
                    break;
                case 10010406:
                    LockMinute = 10080;
                    break;
                case 10010407:
                    LockMinute = 0;
                    break;
                case 10010408:
                    return 0;
                default:
                    return 0;
            }
            return LockMinute;
        }
        [WebMethod(Description = "角色工单:禁言恢复")]
        public string DisChatDel(string F_GUserID, string F_GRoleID)
        {
            InitLanguage();
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Warn("非法请求，将拒绝对此请求提供服务,请求IP" + GetIP4Address() + " ");
                return LanguageResource.Language.Tip_RequestServiceInvalid;
            }
            string returnValue = "";
            DbHelperSQLP sp = new DbHelperSQLP();

            //开始操作GameCoreDB
            sp.connectionString = ConnStrGameCoreDB;
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.SmallInt,4)};
                parameters[0].Value = F_GUserID;
                parameters[1].Value = F_GRoleID;
                parameters[2].Direction = ParameterDirection.Output;

                DataSet ds = sp.RunProcedure("_DBIS_Role_DisChatDel", parameters, typeof(ServiceXLJ).Name);

                returnValue = "true";
            }
            catch (System.Exception ex)
            {
                returnValue = LanguageResource.Language.Tip_GameDataOperateError;
                Log.Error("游戏工具:禁言恢复:游戏数据操作错误", ex);
            }
            return returnValue;
        }
    }
}
