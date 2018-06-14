using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using WSS.DBUtility;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Data.Linq;
using Coolite.Ext.Web;
using WebServiceWSS.OtherUtil;

namespace WebServiceWSS
{
    /// <summary>
    /// WebServiceGame 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceGame : System.Web.Services.WebService
    {
        string ConnStrUserCoreDB = ConfigurationManager.AppSettings["ConnStrUserCoreDB"];//用户数据库连接字符串
        string ConnStrGameCoreDB = ConfigurationManager.AppSettings["ConnStrGameCoreDB"];//游戏数据库连接字条串

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetGameUsers(string query)//得到用户信息
        {
            string jsons = "{totalCount:0,success:true,data:[{}]}";
            if (query.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 5 * ");
                strSql.Append(" FROM T_User  with(nolock)");

                strSql.Append(" where F_UserName like '" + query + "%' order by F_UserID");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrUserCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_UserID", dr["F_UserID"].ToString());
                    json.AddItem("F_UserName", dr["F_UserName"].ToString().Trim());
                    json.AddItem("F_IsAdult", dr["F_IsAdult"].ToString());
                    json.AddItem("F_IsLock", dr["F_IsLock"].ToString());
                    json.AddItem("F_Level", dr["F_Level"].ToString());
                    json.AddItem("F_ActiveTime", dr["F_ActiveTime"].ToString());
                    json.AddItem("F_IsProtect", dr["F_IsProtect"].ToString());
                    json.AddItem("F_SpreaderID", dr["F_SpreaderID"].ToString());
                    json.AddItem("F_PwdType", dr["F_PwdType"].ToString());

                    string F_UserDetail = "<h3><span>编号:" + dr["F_UserID"].ToString() + "</span>  用户名:" + dr["F_UserName"].ToString() + "</h3> 成年:" + WSUtil.YesOrNO(dr["F_IsAdult"].ToString()) + " 封停:" + WSUtil.YesOrNO(dr["F_IsLock"].ToString()) + " 等级:" + dr["F_Level"].ToString() + " 密保:" + WSUtil.YesOrNO(dr["F_IsProtect"].ToString()) + "<br />活动时间:" + dr["F_ActiveTime"].ToString() + "";
                    json.AddItem("F_UserDetail", F_UserDetail);
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount == 0)
                {
                    jsons = "{totalCount:0,success:true,data:[]}";
                }
                else
                {
                    jsons = json.ToString();
                }


            }
            return jsons;
        }
        [WebMethod]
        public string GetGameRoles(string userid)//得到角色信息
        {
            string jsons = "{totalCount:0,success:true,data:[]}";
            if (userid.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM T_RoleBaseData_" + Convert.ToInt32(userid) % 5 + "");

                strSql.Append(" with(nolock) where F_UserID =" + userid + " order by F_RoleID");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_RoleID", dr["F_RoleID"].ToString());
                    json.AddItem("F_RoleName", dr["F_RoleName"].ToString().Trim());
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString().Trim());
                    json.AddItem("F_CampID", dr["F_CampID"].ToString().Trim());
                    json.AddItem("F_Pro", dr["F_Pro"].ToString().Trim());
                    json.AddItem("F_Sex", dr["F_Sex"].ToString().Trim());
                    json.AddItem("F_Level", dr["F_Level"].ToString().Trim());
                    json.AddItem("F_LastScene", dr["F_LastScene"].ToString().Trim());
                    json.AddItem("F_StoreMoney", dr["F_StoreMoney"].ToString().Trim());
                    json.AddItem("F_GameMoney", dr["F_GameMoney"].ToString().Trim());
                    json.AddItem("F_RealMoney", dr["F_RealMoney"].ToString().Trim());
                    json.AddItem("F_CreateTime", dr["F_CreateTime"].ToString().Trim());
                    json.AddItem("F_UpdateTime", dr["F_UpdateTime"].ToString().Trim());
                    json.AddItem("F_yRolePos", dr["F_yRolePos"].ToString().Trim());
                    json.AddItem("F_HaveRoleData", dr["F_HaveRoleData"].ToString().Trim());
                    string F_RoleDetail = "<h3><span>编号:" + dr["F_RoleID"].ToString() + "</span>  角色名:" + dr["F_RoleName"].ToString() + "</h3> 所属战区:" + dr["F_ZoneID"].ToString() + "  阵营/国家:" + dr["F_CampID"].ToString() + " 职业:" + dr["F_Pro"].ToString() + " 性别:" + dr["F_Sex"].ToString() + "<br />等级:" + dr["F_Level"].ToString() + " 最后场景:" + dr["F_LastScene"].ToString() + " 仓库金钱:" + dr["F_StoreMoney"].ToString() + " 游戏金钱:" + dr["F_GameMoney"].ToString() + " 真实金钱:" + dr["F_RealMoney"].ToString() + " 角色面板位置:" + dr["F_yRolePos"].ToString() + " 是否有角色数据:" + WSUtil.YesOrNO(dr["F_HaveRoleData"].ToString()) + "<br /> 创建时间:" + dr["F_CreateTime"].ToString() + " 更新时间:" + dr["F_UpdateTime"].ToString() + "";
                    json.AddItem("F_RoleDetail", F_RoleDetail);
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount == 0)
                {
                    jsons = "{totalCount:0,success:true,data:[]}";
                }
                else
                {
                    jsons = json.ToString();
                }


            }
            return jsons;
        }

        [WebMethod]
        public string GetGameRoleDetail(string userid)//得到角色详细信息(暂停使用)
        {
            string jsons = "{totalCount:0,success:true,data:[]}";
            if (userid.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM T_RoleBaseData_" + Convert.ToInt32(userid) % 5 + "");

                strSql.Append(" with(nolock) where F_UserID =" + userid + " order by F_RoleID");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_RoleID", dr["F_RoleID"].ToString());
                    json.AddItem("F_RoleName", dr["F_RoleName"].ToString().Trim());
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString().Trim());
                    json.AddItem("F_CampID", dr["F_CampID"].ToString().Trim());
                    json.AddItem("F_Pro", dr["F_Pro"].ToString().Trim());
                    json.AddItem("F_Sex", dr["F_Sex"].ToString().Trim());
                    json.AddItem("F_Level", dr["F_Level"].ToString().Trim());
                    json.AddItem("F_LastScene", dr["F_LastScene"].ToString().Trim());
                    json.AddItem("F_StoreMoney", dr["F_StoreMoney"].ToString().Trim());
                    json.AddItem("F_GameMoney", dr["F_GameMoney"].ToString().Trim());
                    json.AddItem("F_RealMoney", dr["F_RealMoney"].ToString().Trim());
                    json.AddItem("F_CreateTime", dr["F_CreateTime"].ToString().Trim());
                    json.AddItem("F_UpdateTime", dr["F_UpdateTime"].ToString().Trim());
                    json.AddItem("F_yRolePos", dr["F_yRolePos"].ToString().Trim());
                    json.AddItem("F_HaveRoleData", dr["F_HaveRoleData"].ToString().Trim());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount == 0)
                {
                    jsons = "{totalCount:0,success:true,data:[]}";
                }
                else
                {
                    jsons = json.ToString();
                }


            }
            return jsons;
        }

        [WebMethod]
        public string SetUserLock(string userid, string username, string locktype)//封停用户
        {
            string jsons = "false";
            try
            {
                if (userid.Trim() != string.Empty && username.Trim() != string.Empty && locktype.Trim() != string.Empty)
                {
                    DateTime lockstarttime = DateTime.Now;
                    DateTime lockendtime = lockstarttime;
                    switch (locktype)
                    {
                        case "0":
                            lockendtime = lockstarttime.AddMinutes(15);
                            break;
                        case "1":
                            lockendtime = lockstarttime.AddMinutes(30);
                            break;
                        case "2":
                            lockendtime = lockstarttime.AddHours(1);
                            break;
                        case "3":
                            lockendtime = lockstarttime.AddHours(2);
                            break;
                        case "4":
                            lockendtime = lockstarttime.AddHours(6);
                            break;
                        case "5":
                            lockendtime = lockstarttime.AddHours(12);
                            break;
                        case "6":
                            lockendtime = lockstarttime.AddDays(1);
                            break;
                        case "7":
                            lockendtime = lockstarttime.AddDays(2);
                            break;
                        case "8":
                            lockendtime = lockstarttime.AddDays(3);
                            break;
                        case "9":
                            lockendtime = lockstarttime.AddDays(4);
                            break;
                        case "10":
                            lockendtime = lockstarttime.AddDays(5);
                            break;
                        case "11":
                            lockendtime = lockstarttime.AddDays(6);
                            break;
                        case "12":
                            lockendtime = lockstarttime.AddDays(7);
                            break;
                        case "13":
                            lockendtime = lockstarttime.AddDays(15);
                            break;
                        case "14":
                            lockendtime = lockstarttime.AddDays(30);
                            break;
                        case "15":
                            lockendtime = lockstarttime.AddMonths(6);
                            break;
                        case "16":
                            lockendtime = lockstarttime.AddYears(1);
                            break;
                        case "17":
                            lockendtime = lockstarttime.AddYears(100);
                            break;
                    }
                    int result = 0;

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC _WSS_User_LockTime ");
                    strSql.Append("@F_UserID,@F_UserName,@F_LockType,@F_LockStartTime,@F_LockEndTime,@Result OUTPUT");
                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@F_UserName", SqlDbType.NChar),
					new SqlParameter("@F_LockType", SqlDbType.TinyInt),
					new SqlParameter("@F_LockStartTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_LockEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Result", SqlDbType.Char,32)};
                    parameters[0].Value = userid;
                    parameters[1].Value = username;
                    parameters[2].Value = locktype;
                    parameters[3].Value = lockstarttime;
                    parameters[4].Value = lockendtime;
                    parameters[5].Direction = ParameterDirection.Output;

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    result = Convert.ToInt32(parameters[5].Value);

                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }

                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;
        }

        private enum USER_LOCKTYPE //封停类型,另外18类型为IP封停
        {
            E_LOCK_TYPE_0 = 0,   //第一种类型封号  15分钟
            E_LOCK_TYPE_1 = 1,   //30分钟
            E_LOCK_TYPE_2 = 2,   //1小时
            E_LOCK_TYPE_3 = 3,   //2小时
            E_LOCK_TYPE_4 = 4,   //6小时
            E_LOCK_TYPE_5 = 5,   //12小时
            E_LOCK_TYPE_6 = 6,   //一天
            E_LOCK_TYPE_7 = 7,   //二天
            E_LOCK_TYPE_8 = 8,   //三天
            E_LOCK_TYPE_9 = 9,   //四天
            E_LOCK_TYPE_10 = 10,  //五天
            E_LOCK_TYPE_11 = 11,  //6天
            E_LOCK_TYPE_12 = 12,  //7天
            E_LOCK_TYPE_13 = 13,  //15天
            E_LOCK_TYPE_14 = 14,  //30天
            E_LOCK_TYPE_15 = 15,  //半年
            E_LOCK_TYPE_16 = 16,  //一年
            E_LOCK_TYPE_17 = 17,  //永久
        }

        [WebMethod]
        public string GetUserlocktype()//得到封停类型(时间类型)
        {
            string jsons = "{totalCount:0,success:true,data:[{name:\"15分钟\",value:\"0\"},{name:\"30分钟\",value:\"1\"},{name:\"1小时\",value:\"2\"},{name:\"2小时\",value:\"3\"},{name:\"6小时\",value:\"4\"},{name:\"12时\",value:\"5\"},{name:\"一天\",value:\"6\"},{name:\"二天\",value:\"7\"},{name:\"三天\",value:\"8\"},{name:\"四天\",value:\"9\"},{name:\"五天\",value:\"10\"},{name:\"6天\",value:\"11\"},{name:\"7天\",value:\"12\"},{name:\"15天\",value:\"13\"},{name:\"30天\",value:\"14\"},{name:\"半年\",value:\"15\"},{name:\"一年\",value:\"16\"},{name:\"永久\",value:\"17\"}]}";
            return jsons;
        }

        [WebMethod]
        public string GetGameZones()//得到游戏区域信息(战区)
        {
            string jsons = "{totalCount:0,success:true,data:[{}]}";

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT F_ZoneID, F_ZoneName, F_ZoneState, F_ZoneLine, F_ZoneAttrib, F_ChargeType, F_CurVersion, F_BigZoneID ");
            strSql.Append("  FROM T_BattleZone");

            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStrGameCoreDB;
            DataSet ds = sp.Query(strSql.ToString());
            JSONHelper json = new JSONHelper();
            json.success = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                json.AddItem("F_ZoneName", dr["F_ZoneName"].ToString().Trim());

                string F_ZoneDetail = "<h3><span>编号:" + dr["F_ZoneID"].ToString() + "</span>  战区:" + dr["F_ZoneName"].ToString() + "</h3> 状态:" + dr["F_ZoneState"].ToString() + " 线路:" + dr["F_ZoneLine"].ToString() + " 属性:" + dr["F_ZoneAttrib"].ToString() + " 收费类型:" + dr["F_ChargeType"].ToString() + "<br />当前版本:" + dr["F_CurVersion"].ToString() + " 大区编号:" + dr["F_BigZoneID"].ToString() + "";
                json.AddItem("F_ZoneDetail", F_ZoneDetail);
                json.ItemOk();
            }
            json.totlalCount = ds.Tables[0].Rows.Count;

            if (json.totlalCount != 0)
            {
                jsons = json.ToString();
            }

            return jsons;
        }

        [WebMethod]
        public string SetRoleLock(string roleid, string rolename, string locktype)//封停角色
        {
            string jsons = "false";
            try
            {
                if (roleid.Trim() != string.Empty && rolename.Trim() != string.Empty && locktype.Trim() != string.Empty)
                {
                    DateTime lockstarttime = DateTime.Now;
                    DateTime lockendtime = lockstarttime;
                    switch (locktype)
                    {
                        case "0":
                            lockendtime = lockstarttime.AddMinutes(15);
                            break;
                        case "1":
                            lockendtime = lockstarttime.AddMinutes(30);
                            break;
                        case "2":
                            lockendtime = lockstarttime.AddHours(1);
                            break;
                        case "3":
                            lockendtime = lockstarttime.AddHours(2);
                            break;
                        case "4":
                            lockendtime = lockstarttime.AddHours(6);
                            break;
                        case "5":
                            lockendtime = lockstarttime.AddHours(12);
                            break;
                        case "6":
                            lockendtime = lockstarttime.AddDays(1);
                            break;
                        case "7":
                            lockendtime = lockstarttime.AddDays(2);
                            break;
                        case "8":
                            lockendtime = lockstarttime.AddDays(3);
                            break;
                        case "9":
                            lockendtime = lockstarttime.AddDays(4);
                            break;
                        case "10":
                            lockendtime = lockstarttime.AddDays(5);
                            break;
                        case "11":
                            lockendtime = lockstarttime.AddDays(6);
                            break;
                        case "12":
                            lockendtime = lockstarttime.AddDays(7);
                            break;
                        case "13":
                            lockendtime = lockstarttime.AddDays(15);
                            break;
                        case "14":
                            lockendtime = lockstarttime.AddDays(30);
                            break;
                        case "15":
                            lockendtime = lockstarttime.AddMonths(6);
                            break;
                        case "16":
                            lockendtime = lockstarttime.AddYears(1);
                            break;
                        case "17":
                            lockendtime = lockstarttime.AddYears(100);
                            break;
                    }
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
					new SqlParameter("@F_LockType", SqlDbType.TinyInt,1),
					new SqlParameter("@LockMinute", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = roleid;
                    parameters[1].Value = rolename;
                    parameters[2].Value = locktype;
                    parameters[3].Value = LockMinute;
                    parameters[4].Direction = ParameterDirection.Output;



                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    rowcount = sp.ExecuteSql(strSql.ToString(), parameters);
                    result = Convert.ToInt32(parameters[4].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);

                    }


                }
            }
            catch //(System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetRoleNoLock(string roleid)//解封角色
        {
            string jsons = "false";
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
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);

                    }


                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetUserNoLock(string userid)//解封用户
        {
            string jsons = "false";
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
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);

                    }


                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string GetLockInfo(string userid, string roleid)//获取用户或角色封停信息
        {
            string jsons = "";
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
                        jsons = "<b>编号:" + userid + " 用户:" + dr["F_UserName"].ToString() + " </b><br />封停开始时间:" + dr["F_LockStartTime"].ToString() + " 封停结束时间:" + dr["F_LockEndTime"].ToString() + " 封停IP:" + dr["F_LockIPBegin"].ToString() + " 至 " + dr["F_LockIPEnd"].ToString() + " ";
                    }
                    else
                    {
                        jsons = "提示:暂无封停信息";
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
                        jsons = "<b>编号:" + userid + " 角色:" + dr["F_RoleName"].ToString() + " </b><br />封停开始时间:" + dr["F_LockStartTime"].ToString() + " 封停结束时间:" + dr["F_LockEndTime"].ToString() + " 封停IP:" + dr["F_LockIP"].ToString() + " ";
                    }
                    else
                    {
                        jsons = "提示:暂无封停信息";
                    }

                }
                if (jsons.Trim() == string.Empty)
                {
                    jsons = "提示:暂无封停信息";
                }
            }
            catch (System.Exception ex)
            {
                jsons = "提示:获取封停信息,数据库错误";
            }

            return jsons;


        }

        [WebMethod]
        public string GetChildDisInfo(string userid)//获取防沉迷信息
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"SELECT F_UserName, F_DayLoginTime, F_LastLoginTime, F_RemaindTime FROM T_ChildUser with(nolock) WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    DataSet ds = sp.Query(sql);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        jsons = "<b>编号:" + userid + " 用户:" + dr["F_UserName"].ToString() + " 剩余时间(分钟):" + dr["F_RemaindTime"].ToString() + " </b><br />当天登录时间:" + dr["F_DayLoginTime"].ToString() + " 最后登录时间:" + dr["F_LastLoginTime"].ToString() + " ";
                    }
                    else
                    {
                        jsons = "提示:暂无防沉迷信息";
                    }

                }

                if (jsons.Trim() == string.Empty)
                {
                    jsons = "提示:暂无防沉迷信息";
                }
            }
            catch (System.Exception ex)
            {
                jsons = "提示:获取防沉迷信息,数据库错误";
            }

            return jsons;


        }

        [WebMethod]
        public string ClearChildDisInfo(string userid)//清除防沉迷信息
        {
            string jsons = "";
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
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:清除防沉迷信息,数据库错误";
            }

            return jsons;


        }

        [WebMethod]
        public string GetUserOnline(string userid)//获取在线信息
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"SELECT F_UserName, F_LoginTime, F_LoginNGSID, F_ZoneID, F_LoginIP, F_LoginType FROM T_UserLoged with(nolock) WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    DataSet ds = sp.Query(sql);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        jsons = "<b>编号:" + userid + " 用户:" + dr["F_UserName"].ToString() + " 登录时间:" + dr["F_LoginTime"].ToString() + " </b><br />NGS编号:" + dr["F_LoginNGSID"].ToString() + " 战区:" + dr["F_ZoneID"].ToString() + "  登录IP:" + dr["F_LoginIP"].ToString() + "  登录方式:" + dr["F_LoginType"].ToString() + "";
                    }

                    string sql1 = @"SELECT F_RoleId, F_LogInTime, F_ZoneID, F_GSID, F_LoginType FROM T_LogInRole_" + Convert.ToInt32(userid) % 5 + "  with(nolock) WHERE (F_UserID = " + userid + ")";

                    sp.connectionString = ConnStrGameCoreDB;
                    DataSet ds1 = sp.Query(sql1);

                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        DataRow dr1 = ds1.Tables[0].Rows[0];
                        jsons += "<br /><b>编号:" + dr1["F_RoleId"].ToString() + " 角色:" + dr1["F_RoleId"].ToString() + " 登录时间:" + dr1["F_LoginTime"].ToString() + " </b><br /> 战区:" + dr1["F_ZoneID"].ToString() + "  GS编号:" + dr1["F_GSID"].ToString() + "  登录方式:" + dr1["F_LoginType"].ToString() + "";
                    }

                }

                if (jsons.Trim() == string.Empty)
                {
                    jsons = "提示:暂无在线信息";
                }
            }
            catch (System.Exception ex)
            {
                jsons = "提示:获取在线信息,数据库错误";
            }

            return jsons;


        }

        /// <summary>
        /// 踢号,清除在线信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetUserOffline(string userid)//踢号,清除在线信息
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_OffLine ");
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
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);

                    }

                    StringBuilder strSql1 = new StringBuilder();
                    strSql1.Append("EXEC  _WSS_Role_OffLine" + Convert.ToInt32(userid) % 5 + " ");
                    strSql1.Append("@F_UserID,@Result OUTPUT");

                    SqlParameter[] parameters1 = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters1[0].Value = userid;
                    parameters1[1].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp1 = new DbHelperSQLP();
                    sp1.connectionString = ConnStrGameCoreDB;
                    sp1.ExecuteSql(strSql1.ToString(), parameters1);
                    result = Convert.ToInt32(parameters1[1].Value);
                    if (result == 0 && jsons == "true")
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons += GetErroStr(sp1, result);
                    }
                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:踢号,清除在线信息,数据库错误";
            }

            return jsons;


        }

        [WebMethod]
        public string SetUserNameChange(string userid, string username, string newname)//用户名更改
        {
            string jsons = "false";
            try
            {
                if (newname.Trim().Length < 2 || newname.Trim().Length > 16)
                {
                    return "新名字不能小于2位,大于16位";
                }
                if (userid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_UserNameChange ");
                    strSql.Append("@F_UserID,@F_UserName,@NewUserName,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
                    new SqlParameter("@F_UserName", SqlDbType.NVarChar),
                    new SqlParameter("@NewUserName", SqlDbType.NVarChar),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = userid;
                    parameters[1].Value = username;
                    parameters[2].Value = newname;
                    parameters[3].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[3].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }


                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetRoleNameChange(string userid, string roleid, string rolename, string newname)//角色名更改
        {
            string jsons = "false";
            try
            {
                if (newname.Trim().Length < 2 || newname.Trim().Length > 16)
                {
                    return "新名字不能小于2位,大于16位";
                }
                if (roleid.Trim() != string.Empty && userid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_Role_RoleNameChange" + Convert.ToInt32(userid) % 5 + " ");
                    strSql.Append("@F_RoleID,@F_RoleName,@NewRoleName,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int),
                    new SqlParameter("@F_RoleName", SqlDbType.NVarChar),
                    new SqlParameter("@NewRoleName", SqlDbType.NVarChar),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = roleid;
                    parameters[1].Value = rolename;
                    parameters[2].Value = newname;
                    parameters[3].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[3].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }

                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetRoleZoneChange(string userid, string roleid, string zoneid)//角色改服
        {
            string jsons = "false";
            try
            {
                if (roleid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_Role_ZoneChange" + Convert.ToInt32(userid) % 5 + " ");
                    strSql.Append("@F_RoleID,@F_ZoneID,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,10),
                    new SqlParameter("@F_ZoneID", SqlDbType.SmallInt),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = roleid;
                    parameters[1].Value = zoneid;
                    parameters[2].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrGameCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[2].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }


                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetUserGMUse(string userid, string newuserpsw, string gmname)//帐号借用
        {
            string jsons = "false";
            try
            {
                if (userid.Trim() != string.Empty)
                {
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


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[3].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }
                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;
        }

        [WebMethod]
        public string SetUserGMBack(string userid)//帐号归还
        {
            string jsons = "false";
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
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }
                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;
        }

        [WebMethod]
        public string SetUserLockIP(string userid, string username, string ipbegin, string ipend)//帐号锁定IP
        {
            string jsons = "false";
            try
            {
                if (userid.Trim() != string.Empty)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("EXEC	_WSS_User_LockIP ");
                    strSql.Append("@F_UserID,@F_UserName,@F_LockIPBegin,@F_LockIPEnd,@Result OUTPUT");

                    SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
                    new SqlParameter("@F_UserName", SqlDbType.NVarChar),
                    new SqlParameter("@F_LockIPBegin", SqlDbType.NVarChar),
                    new SqlParameter("@F_LockIPEnd", SqlDbType.NVarChar),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                    parameters[0].Value = userid;
                    parameters[1].Value = username;
                    parameters[2].Value = ipbegin;
                    parameters[3].Value = ipend;
                    parameters[4].Direction = ParameterDirection.Output;


                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    sp.ExecuteSql(strSql.ToString(), parameters);
                    int result = Convert.ToInt32(parameters[4].Value);
                    if (result == 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = GetErroStr(sp, result);
                    }
                }
            }
            catch (System.Exception ex)
            {
                jsons = "数据库错误";
            }

            return jsons;
        }

        [WebMethod]
        public string ClearUserPersonID(string userid)//清除身份证
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"UPDATE T_UserExInfo SET F_PersonID = NULL WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "没有身份证信息";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:清除身份证,数据库错误";
            }

            return jsons;
        }

        [WebMethod]
        public string ClearUserEmail(string userid)//清空邮箱
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"UPDATE T_UserExInfo SET F_Email = NULL WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "没有邮箱信息";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:清空邮箱,数据库错误";
            }

            return jsons;


        }

        [WebMethod]
        public string ClearUserPSWProtect(string userid)//清空密保
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"UPDATE T_User SET F_IsProtect = 0 WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "没有密保信息";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:清空密保,数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string ClearUserSecondPSW(string userid)//清空二级密码
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"UPDATE T_User SET F_IsProtect = 0 WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "没有二级密码信息";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:清空二级密码,数据库错误";
            }

            return jsons;

        }

        [WebMethod]
        public string SetUserPSWinit(string userid)//密码初始化
        {
            string jsons = "";
            try
            {
                if (userid.Trim() != string.Empty)
                {

                    string sql = @"UPDATE T_User SET F_PassWord = '96e79218965eb72c92a549dd5a330112' WHERE (F_UserID = " + userid + ")";

                    DbHelperSQLP sp = new DbHelperSQLP();
                    sp.connectionString = ConnStrUserCoreDB;
                    int rowcount = sp.ExecuteSql(sql);

                    if (rowcount != 0)
                    {
                        jsons = "true";
                    }
                    else
                    {
                        jsons = "没有密码信息";
                    }

                }

            }
            catch (System.Exception ex)
            {
                jsons = "提示:密码初始化,数据库错误";
            }

            return jsons;

        }


        //得到错误字符串
        protected string GetErroStr(DbHelperSQLP sp, int erroid)
        {
            string sql = "SELECT F_Vaule FROM  T_ErroVal where F_ID=" + erroid + " and F_IsUsed=1";
            return sp.GetSingle(sql).ToString();
        }

        /**/
        /// <summary> 

        /// 将DataTable对象转换成XML字符串 

        /// </summary> 

        /// <param name="dt">DataTable对象</param> 

        /// <returns>XML字符串</returns> 

        public static string CDataToXml(DataTable dt)
        {

            if (dt != null)
            {

                MemoryStream ms = null;

                XmlTextWriter XmlWt = null;

                try
                {

                    ms = new MemoryStream();

                    //根据ms实例化XmlWt 

                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);

                    //获取ds中的数据 

                    dt.WriteXml(XmlWt);

                    int count = (int)ms.Length;

                    byte[] temp = new byte[count];

                    ms.Seek(0, SeekOrigin.Begin);

                    ms.Read(temp, 0, count);

                    //返回Unicode编码的文本 

                    UnicodeEncoding ucode = new UnicodeEncoding();

                    string returnValue = ucode.GetString(temp).Trim();

                    return returnValue;

                }

                catch (System.Exception ex)
                {

                    throw ex;

                }

                finally
                {

                    //释放资源 

                    if (XmlWt != null)
                    {

                        XmlWt.Close();

                        ms.Close();

                        ms.Dispose();

                    }

                }

            }

            else
            {

                return "";

            }

        } 



        [WebMethod]
        public string  GetBattleZones(int start, int limit, string sort, string dir)//得到战区配置列表
        {
            string jsons = "{totalCount:0,success:true,data:[]}";
            try
            {
                //if (sort.Trim().Length == 0)
                //{
                //    sort = "F_ZoneID";
                //}
                //StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT *  FROM (SELECT ROW_NUMBER() OVER (ORDER BY " + sort + " " + dir + ") AS pos,");
                //strSql.Append("  F_ZoneID, F_ZoneName, F_ZoneState, F_ZoneLine, F_ZoneAttrib, F_ChargeType, F_CurVersion, F_BigZoneID  ");
                //strSql.Append("  FROM T_BattleZone where 1=1) AS sp  ");
                //strSql.Append("  WHERE pos BETWEEN " + (start + 1).ToString() + " AND " + (start + limit) + " ");



                //string sqlcount = "SELECT COUNT(1) FROM  T_BattleZone  WHERE  1=1";

                //DbHelperSQLP sp = new DbHelperSQLP();
                //sp.connectionString = ConnStrGameCoreDB;
                //DataSet ds = sp.Query(strSql.ToString());
                //JSONHelper json = new JSONHelper();
                //json.success = true;
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                //    json.AddItem("F_ZoneName", dr["F_ZoneName"].ToString().Trim());
                //    json.AddItem("F_ZoneState", dr["F_ZoneState"].ToString());
                //    json.AddItem("F_ZoneLine", dr["F_ZoneLine"].ToString());
                //    json.AddItem("F_ZoneAttrib", dr["F_ZoneAttrib"].ToString());
                //    json.AddItem("F_ChargeType", dr["F_ChargeType"].ToString());
                //    json.AddItem("F_CurVersion", dr["F_CurVersion"].ToString());
                //    json.AddItem("F_BigZoneID", dr["F_BigZoneID"].ToString());
                //    json.ItemOk();
                //}
                //int rcount = Convert.ToInt32(sp.GetSingle(sqlcount));
                //json.totlalCount = rcount;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_ZoneID, F_ZoneName, F_ZoneState, F_ZoneLine, F_ZoneAttrib, F_ChargeType, F_CurVersion, F_BigZoneID");
                strSql.Append(" FROM T_BattleZone ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                    json.AddItem("F_ZoneName", dr["F_ZoneName"].ToString().Trim());
                    json.AddItem("F_ZoneState", dr["F_ZoneState"].ToString());
                    json.AddItem("F_ZoneLine", dr["F_ZoneLine"].ToString());
                    json.AddItem("F_ZoneAttrib", dr["F_ZoneAttrib"].ToString());
                    json.AddItem("F_ChargeType", dr["F_ChargeType"].ToString());
                    json.AddItem("F_CurVersion", dr["F_CurVersion"].ToString());
                    json.AddItem("F_BigZoneID", dr["F_BigZoneID"].ToString());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount != 0)
                {
                    jsons = json.ToString();
                }
            }
            catch (System.Exception ex)
            {
            }
            return jsons;
        }

        [WebMethod]
        public Coolite.Ext.Web.Response SaveBattleZones(string data)//保存战区信息
        {
            Response sr = new Response(true);

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_BattleZone> dataList = dataHandler.ObjectData<T_BattleZone>();

                foreach (T_BattleZone battlezone in dataList.Deleted)
                {
                    db.T_BattleZone.Attach(battlezone);
                    db.T_BattleZone.DeleteOnSubmit(battlezone);
                }

                foreach (T_BattleZone battlezone in dataList.Updated)
                {
                    db.T_BattleZone.Attach(battlezone);
                    db.Refresh(RefreshMode.KeepCurrentValues, battlezone);
                }

                foreach (T_BattleZone battlezone in dataList.Created)
                {
                    db.T_BattleZone.InsertOnSubmit(battlezone);
                }

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
            }

            return sr;
        }

        [WebMethod]
        public string GetBattleLines()//得到单条线列表
        {
            string jsons = "{totalCount:0,success:true,data:[]}";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_NGSID, F_Name, F_ZoneID, F_MaxUser, F_Ip, F_Port, F_State, F_DBISID, F_PingPort");
                strSql.Append(" FROM T_BattleLine ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_NGSID", dr["F_NGSID"].ToString());
                    json.AddItem("F_Name", dr["F_Name"].ToString().Trim());
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                    json.AddItem("F_MaxUser", dr["F_MaxUser"].ToString());
                    json.AddItem("F_Ip", dr["F_Ip"].ToString().Trim());
                    json.AddItem("F_Port", dr["F_Port"].ToString());
                    json.AddItem("F_State", dr["F_State"].ToString());
                    json.AddItem("F_DBISID", dr["F_DBISID"].ToString());
                    json.AddItem("F_PingPort", dr["F_PingPort"].ToString());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount != 0)
                {
                    jsons = json.ToString();
                }
            }
            catch (System.Exception ex)
            {
            }
            return jsons;
        }

        [WebMethod]
        public Coolite.Ext.Web.Response SaveBattleLines(string data)//保存单条线
        {
            Response sr = new Response(true);

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_BattleLine> dataList = dataHandler.ObjectData<T_BattleLine>();

                foreach (T_BattleLine battleline in dataList.Deleted)
                {
                    db.T_BattleLine.Attach(battleline);
                    db.T_BattleLine.DeleteOnSubmit(battleline);
                }

                foreach (T_BattleLine battleline in dataList.Updated)
                {
                    db.T_BattleLine.Attach(battleline);
                    db.Refresh(RefreshMode.KeepCurrentValues, battleline);
                }

                foreach (T_BattleLine battleline in dataList.Created)
                {
                    db.T_BattleLine.InsertOnSubmit(battleline);
                }

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
            }

            return sr;
        }

        [WebMethod]
        public string GetGameServers()//得到GS配置列表
        {
            string jsons = "{totalCount:0,success:true,data:[]}";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_GSID, F_IP, F_GSNAME, F_NGSID, F_ZONEID, F_CampID, F_GSSceneID ");
                strSql.Append(" FROM T_GameServer ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                JSONHelper json = new JSONHelper();
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_GSID", dr["F_GSID"].ToString());
                    json.AddItem("F_IP", dr["F_IP"].ToString().Trim());
                    json.AddItem("F_GSNAME", dr["F_GSNAME"].ToString().Trim());
                    json.AddItem("F_NGSID", dr["F_NGSID"].ToString());
                    json.AddItem("F_ZONEID", dr["F_ZONEID"].ToString());
                    json.AddItem("F_CampID", dr["F_CampID"].ToString());
                    json.AddItem("F_GSSceneID", dr["F_GSSceneID"].ToString().Trim());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                if (json.totlalCount != 0)
                {
                    jsons = json.ToString();
                }
            }
            catch (System.Exception ex)
            {
            }
            return jsons;
        }

        [WebMethod]
        public Coolite.Ext.Web.Response SaveGameServers(string data)//保存GS信息
        {
            Response sr = new Response(true);

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_GameServer> dataList = dataHandler.ObjectData<T_GameServer>();

                foreach (T_GameServer gameserver in dataList.Deleted)
                {
                    db.T_GameServer.Attach(gameserver);
                    db.T_GameServer.DeleteOnSubmit(gameserver);
                }

                foreach (T_GameServer gameserver in dataList.Updated)
                {
                    db.T_GameServer.Attach(gameserver);
                    db.Refresh(RefreshMode.KeepCurrentValues, gameserver);
                }

                foreach (T_GameServer gameserver in dataList.Created)
                {
                    db.T_GameServer.InsertOnSubmit(gameserver);
                }

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
            }

            return sr;
        }

    }
}
