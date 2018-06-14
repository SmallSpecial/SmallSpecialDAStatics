using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using GSS.DBUtility;
using GSSModel;
using GSSServerLibrary;
using System.Linq;
namespace GSSWebServiceOut
{
    /// <summary>
    /// WebServiceGSS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServiceGSS : System.Web.Services.WebService
    {
        string RemoteServerIP = ConfigurationManager.AppSettings["RemoteServerIP"];//使用此WEBSERVICE的服务端的IP地址
        string ConnStrGSSDB = ConfigurationManager.AppSettings["ConnStrGSSDB"];//GSSDB数据库地址

        log4net.ILog Log = log4net.LogManager.GetLogger("GSSLog");
        [WebMethod]
        public string HelloWorld()
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                return "非法请求，将拒绝对此请求提供服务";
            }
            Log.Warn("执行了测试方法HelloWorld,请确认为合法访问,请求IP:" + GetIP4Address() + "");
            return "Hello World";
        }
        [WebMethod(Description = "战区架设工具用户验证")]
        public string GameZoneToolLogin(string username, string password)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GameZoneToolLogin,not allow for the ip " + username + ",theIP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务";
            }
            if (!CheckQuerySql(username) || !CheckQuerySql(password) || username.Length > 30 || password.Length > 32)
            {
                Log.Info("GameZoneToolLogin,not allow for (PARAMETER) " + username + ",theIP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务(PARAMETER)";
            }
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                string sql = @"SELECT T_Users.F_UserID as F_UserID FROM T_Roles WITH(NOLOCK) INNER JOIN T_Users  WITH(NOLOCK)  ON T_Roles.F_RoleID = T_Users.F_RoleID  
WHERE (T_Users.F_UserName = N'" + username + "') AND (T_Users.F_PassWord = N'" + password + "') AND (T_Users.F_IsUsed = 1) AND (T_Roles.F_IsUsed = 1) AND (T_Roles.F_Power like N'%,105100,%')";
                object userid = sp.GetSingle(sql);

                //Log.Info("run GameZoneToolLogin,connectionString:," + ConnStrGSSDB);

                if (userid == null)
                {
                    Log.Info("run GameZoneToolLogin,failed name:," + username + ",theIP:" + GetIP4Address() + "the userid:" + userid.ToString());
                    return "验证不通过或无此权限";
                }
                else
                {
                    Log.Info("run GameZoneToolLogin,name and pwd is OK!  name:," + username + ",theIP:" + GetIP4Address() + "");
                    return "true";
                }
            }
            catch (System.Exception ex)
            {
                Log.Warn("run GameZoneToolLogin,theIP:" + GetIP4Address() + "", ex);
                return "执行异常";
            }

        }


        [WebMethod(Description = "GSS工单信息提交")]
        public string GSSTaskAdd(string type, string note, string qq, string mobile, string gdata, string file)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务";
            }

            type = type.Replace("'", "’").Replace(",", "，");
            note = note.Replace("'", "’").Replace(",", "，");
            qq = qq.Replace("'", "’").Replace(",", "，");
            mobile = mobile.Replace("'", "’").Replace(",", "，");
            gdata = gdata.Replace("'", "’").Replace(",", "，");
            file = file.Replace("'", "’").Replace(",", "，");

            if (!CheckQuerySql(note) || !CheckQuerySql(type) || !CheckQuerySql(qq) || !CheckQuerySql(mobile) || !CheckQuerySql(gdata) | !CheckQuerySql(file))
            {
                Log.Info("GSSTaskAdd,非法请求(PARAMETER) ,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务(PARAMETER)";
            }
            try
            {

                GSSModel.Tasks task = new GSSModel.Tasks();

                string[] ps = gdata.Split('|');

                task.F_GameName = 1000;
                //task.F_GameBigZone = ps[6];
                task.F_GameBigZone = "第一大区";
                task.F_GameZone = ps[8];

                task.F_GUserID = ps[3];
                task.F_GUserName = ps[4];
                task.F_GRoleID = ps[1];
                task.F_GRoleName = ps[2];



                task.F_URInfo = string.Format("用户编号:{0} 用户名称:{1} 角色编号:{2} 角色名称:{3} 大区名称:{4} 战区名称:{5} 战线:{6}\r\n--来自游戏内嵌反馈系统", task.F_GUserID, task.F_GUserName, task.F_GRoleID, task.F_GRoleName, task.F_GameBigZone, task.F_GameZone, ps[10]);

                task.F_Note = "\n--- 来自游戏内嵌反馈系统\n 玩家联系方式\n 【QQ:" + qq + "】\n 【手机:" + mobile + "】\n";
                task.F_Note += note;
                task.F_Telphone = mobile;

                if (ps.Length == 12)
                {
                    string gamedateStr = ps[11];
                    task.F_URInfo += "\r\n【游戏数据】" + gamedateStr;
                    task.F_Note += "\r\n【游戏数据】" + gamedateStr;
                    if (gamedateStr.IndexOf("ClientIP:") >= 0)
                    {
                        string gip = gamedateStr.Substring(gamedateStr.IndexOf("ClientIP:"), gamedateStr.Length - gamedateStr.IndexOf("ClientIP:")).Replace("ClientIP:", "");
                        string uaddress = GetIPLocation(gip);
                        task.F_URInfo += "\r\n【玩家地址】" + uaddress;
                        task.F_Note += "\r\n【玩家地址】" + uaddress;
                    }
                }
                else if (ps.Length == 11)//游戏版本发出后删除此判断
                {
                    string gamedateStr = ps[10];
                    if (gamedateStr.IndexOf("ClientIP:") >= 0)
                    {
                        string gip = gamedateStr.Substring(gamedateStr.IndexOf("ClientIP:"), gamedateStr.Length - gamedateStr.IndexOf("ClientIP:")).Replace("ClientIP:", "");

                        task.F_URInfo += "\r\n【游戏数据】ClientIP:" + gip;
                        task.F_Note += "\r\n【游戏数据】ClientIP:" + gip;

                        string uaddress = GetIPLocation(gip);
                        task.F_URInfo += "\r\n【玩家地址】" + uaddress;
                        task.F_Note += "\r\n【玩家地址】" + uaddress;
                    }
                }

                task.F_CreatTime = DateTime.Now;
                task.F_LimitTime = DateTime.Now.AddDays(7);
                task.F_EditTime = DateTime.Now;
                task.F_From = 100103103;
                task.F_LimitType = 100104107;
                task.F_OLastLoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                task.F_Rowtype = 0;
                task.F_State = 100100100;
                task.F_Type = Convert.ToInt32(type);
                task.F_VipLevel = 100105003;
                task.F_Title = "来自游戏内嵌反馈系统";




                if (file.Trim().Length > 0)
                {
                    WebClient mywebclient = new WebClient();
                    string downfilepath = file;

                    string savepath = downfilepath.Replace("http://", "");
                    savepath = savepath.Substring(savepath.IndexOf('/'));
                    string savepathp = savepath.Replace(savepath.Substring(savepath.LastIndexOf("/")), "") + "/";

                    // string picurl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + savepath;

                    FileInfo finfo = new FileInfo(Server.MapPath("." + savepathp));
                    if (!finfo.Directory.Exists)
                    {
                        finfo.Directory.Create();
                    }
                    mywebclient.DownloadFile(downfilepath, Server.MapPath("." + savepath));

                    // task.F_TToolUsed = true;
                    task.F_TUseData = savepath;
                }



                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                int result = AddTask(task, sp);
                if (result == 0)
                {
                    Log.Info("执行了GSSTaskAdd,保存失败,请求IP:" + GetIP4Address() + "");
                    return "保存失败,数据ERROR";
                }
                else return "0";
            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAdd,请求IP:" + GetIP4Address() + "", ex);
                return "执行异常GSSWebService" + ex.Message;
            }

        }


        [WebMethod(Description = "GSS工单信息提交(问卷调查)")]
        public string GSSTaskAddRequst(string gdata, string answer)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务";
            }

            gdata = gdata.Replace("'", "’").Replace(",", "，");
            answer = answer.Replace("'", "’").Replace(",", "，");

            if (!CheckQuerySql(answer) || !CheckQuerySql(gdata))
            {
                Log.Info("GSSTaskAdd,非法请求(PARAMETER) ,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务(PARAMETER)";
            }
            try
            {

                GSSModel.Tasks task = new GSSModel.Tasks();

                string[] ps = gdata.Split('|');

                task.F_GameName = 1000;
                //task.F_GameBigZone = ps[6];
                task.F_GameBigZone = "第一大区";
                task.F_GameZone = ps[8];

                task.F_GUserID = ps[3];
                task.F_GUserName = ps[4];
                task.F_GRoleID = ps[1];
                task.F_GRoleName = ps[2];

                if (IsHaveSubRequest(task.F_GUserID) && task.F_GUserName.Trim() != "100077")
                {
                    return "您已经提交过调查问卷! ";
                }



                task.F_URInfo = string.Format("用户编号:{0} 用户名称:{1} 角色编号:{2} 角色名称:{3} 大区名称:{4} 战区名称:{5} 战线:{6}\r\n--来自游戏内嵌反馈系统", task.F_GUserID, task.F_GUserName, task.F_GRoleID, task.F_GRoleName, task.F_GameBigZone, task.F_GameZone, ps[10]);


                task.F_Type = 20000215;
                task.F_Note = "+\r\n --来自游戏内嵌反馈系统【调查问卷】";

                task.F_COther = "1";
                task.F_TUseData = answer;

                string[] items = answer.Split('|');
                //foreach (String item in items)
                //{
                //    task.F_Note += "\n"+GetAnswerItemKey(item);
                //}
                for (int i = 0; i < items.Length; i++)
                {
                    task.F_Note += "\n#" + (i + 1).ToString() + "." + GetAnswerItemKey(items[i]) + "";
                }

                if (ps.Length == 12)
                {
                    string gamedateStr = ps[11];
                    task.F_URInfo += "\r\n【游戏数据】" + gamedateStr;
                    task.F_Note += "\r\n【游戏数据】" + gamedateStr;
                    if (gamedateStr.IndexOf("ClientIP:") >= 0)
                    {
                        string gip = gamedateStr.Substring(gamedateStr.IndexOf("ClientIP:"), gamedateStr.Length - gamedateStr.IndexOf("ClientIP:")).Replace("ClientIP:", "");
                        string uaddress = GetIPLocation(gip);
                        task.F_URInfo += "\r\n【玩家地址】" + uaddress;
                        task.F_Note += "\r\n【玩家地址】" + uaddress;
                    }
                }
                else if (ps.Length == 11)//游戏版本发出后删除此判断
                {
                    string gamedateStr = ps[10];
                    if (gamedateStr.IndexOf("ClientIP:") >= 0)
                    {
                        string gip = gamedateStr.Substring(gamedateStr.IndexOf("ClientIP:"), gamedateStr.Length - gamedateStr.IndexOf("ClientIP:")).Replace("ClientIP:", "");

                        task.F_URInfo += "\r\n【游戏数据】ClientIP:" + gip;
                        task.F_Note += "\r\n【游戏数据】ClientIP:" + gip;

                        string uaddress = GetIPLocation(gip);
                        task.F_URInfo += "\r\n【玩家地址】" + uaddress;
                        task.F_Note += "\r\n【玩家地址】" + uaddress;
                    }
                }

                task.F_CreatTime = DateTime.Now;
                task.F_LimitTime = DateTime.Now.AddDays(7);
                task.F_EditTime = DateTime.Now;
                task.F_From = 100103103;
                task.F_LimitType = 100104107;
                task.F_OLastLoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                task.F_Rowtype = 0;
                task.F_State = 100100107;//直接完成工单
                task.F_VipLevel = 100105003;
                task.F_Title = "来自游戏内嵌反馈系统【调查问卷】";

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                int result = AddTask(task, sp);
                if (result == 0)
                {
                    Log.Info("执行了GSSTaskAddRequst,保存失败,请求IP:" + GetIP4Address() + "");
                    return "保存失败,数据ERROR";
                }
                else
                {
                    string resulta = GiftUserAdd(task.F_GameBigZone, task.F_GUserID, "-1", "-1", "530114", "1", "问卷调查自动发奖 工单编号:" + result + "");
                    Log.Info("添加用户奖品,请求IP:" + GetIP4Address() + " 结果" + resulta + "");
                    if (resulta != "true")
                    {
                        return "问卷调查保存成功,但发奖出错(" + resulta + ")";
                    }
                    return "0";
                }
            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAddRequst,请求IP:" + GetIP4Address() + "", ex);
                return "执行异常GSSWebService" + ex.Message;
            }

        }

        [WebMethod(Description = "GSS统计计数")]
        public int GSSCountDetailAdd(GSSModel.CountDetail model)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return 0;
            }
            try
            {

                SqlParameter[] parameters = {
					new SqlParameter("@F_Year", SqlDbType.Int,4),
					new SqlParameter("@F_Month", SqlDbType.Int,4),
					new SqlParameter("@F_Day", SqlDbType.Int,4),
					new SqlParameter("@F_Hour", SqlDbType.Int,4),
					new SqlParameter("@F_Game", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_Page", SqlDbType.NVarChar,150),
					new SqlParameter("@F_IP", SqlDbType.NVarChar,50),
					new SqlParameter("@F_IESoft", SqlDbType.NVarChar,30),
					new SqlParameter("@F_OS", SqlDbType.NVarChar,30),
					new SqlParameter("@F_CLR", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Language", SqlDbType.NVarChar,30),
					new SqlParameter("@F_WinBit", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Time", SqlDbType.DateTime),
                    new SqlParameter("@F_ID", SqlDbType.BigInt),
                new SqlParameter("@F_ScreenWidth", SqlDbType.Int,6),
                new SqlParameter("@F_ScreenHeight", SqlDbType.Int,6)};
                parameters[0].Value = model.Year;
                parameters[1].Value = model.Month;
                parameters[2].Value = model.Day;
                parameters[3].Value = model.Hour;
                parameters[4].Value = model.Game;
                parameters[5].Value = model.Type;
                parameters[6].Value = model.Page;
                parameters[7].Value = model.IP;
                parameters[8].Value = model.IESoft;
                parameters[9].Value = model.OS;
                parameters[10].Value = model.CLR;
                parameters[11].Value = model.Language;
                parameters[12].Value = model.WinBit;
                parameters[13].Value = model.Time;
                parameters[14].Direction = ParameterDirection.Output;
                parameters[15].Value = model.Screenwidth;
                parameters[16].Value = model.Screenheight;



                int rowsAffected = 0;
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                sp.RunProcedure("GSS_CountDetail_ADD", parameters, out rowsAffected);
                int id = (int)parameters[14].Value;
                return id;

            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAdd,请求IP:" + GetIP4Address() + "", ex);
                return 0;
            }

        }

        [WebMethod(Description = "GSS工单信息查询")]
        public DataSet GSSTaskList(string userid)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return null;
            }

            userid = userid.Replace("'", "’").Replace(",", "，");


            if (!CheckQuerySql(userid))
            {
                Log.Info("GSSTaskAdd,非法请求(PARAMETER) ,请求IP:" + GetIP4Address() + "");
                return null;
            }
            try
            {


                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                int result = AddTask(null, sp);
                if (result == 0)
                {
                    Log.Info("执行了GSSTaskAdd,保存失败,请求IP:" + GetIP4Address() + "");
                    return null;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAdd,请求IP:" + GetIP4Address() + "", ex);
                return null;
            }

        }

        [WebMethod(Description = "GSS工单信息提交（消息）")]
        public string GSSMSGAdd(string note, string gdata)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务";
            }

            note = note.Replace("'", "’").Replace(",", "，");
            gdata = gdata.Replace("'", "’").Replace(",", "，");


            if (!CheckQuerySql(note) || !CheckQuerySql(gdata))
            {
                Log.Info("GSSTaskAdd,非法请求(PARAMETER) ,请求IP:" + GetIP4Address() + "");
                return "非法请求，将拒绝对此请求提供服务(PARAMETER)";
            }
            try
            {

                GSSModel.Tasks task = new GSSModel.Tasks();

                string[] ps = gdata.Split('|');
                //task.F_GameBigZone = ps[6];
                task.F_GameBigZone = "第一大区";
                task.F_GameZone = ps[8];
                task.F_GUserID = ps[3];
                task.F_GUserName = ps[4];
                task.F_GRoleID = ps[1];
                task.F_GRoleName = ps[2];
                task.F_GameName = 1000;
                task.F_CreatTime = DateTime.Now;
                task.F_LimitTime = DateTime.Now.AddDays(2);
                task.F_EditTime = DateTime.Now;
                task.F_From = 100103103;
                task.F_LimitType = 100104107;
                task.F_OLastLoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                task.F_Rowtype = 6;//聊天
                task.F_State = 100100100;
                task.F_Type = 20000216;
                task.F_VipLevel = 100105003;
                task.F_Title = "来自游戏内嵌反馈系统【在线咨询】";
                task.F_TToolUsed = null;
                task.F_OCanRestor = true;



                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                GSSServerLibrary.DBHandle db = new GSSServerLibrary.DBHandle(ConnStrGSSDB);


                DataSet ds = db.GetTask(" and F_Type=" + task.F_Type + " and F_GUserID=" + task.F_GUserID + " and F_GRoleID=" + task.F_GRoleID + "", sp);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    task.F_Note = note + "\n--来自游戏内嵌反馈系统【在线咨询】";
                    task.F_URInfo = string.Format("用户编号:{0} 用户名称:{1} 角色编号:{2} 角色名称:{3} 大区名称:{4} 战区名称:{5} 战线:{6}\r\n--来自游戏内嵌反馈系统【在线咨询】", task.F_GUserID, task.F_GUserName, task.F_GRoleID, task.F_GRoleName, task.F_GameBigZone, task.F_GameZone, ps[10]);

                    if (ps.Length == 12)
                    {
                        string gamedateStr = ps[11];
                        task.F_URInfo += "\r\n【游戏数据】" + gamedateStr;
                        // task.F_Note += "\r\n【游戏数据】" + gamedateStr;
                        if (gamedateStr.IndexOf("ClientIP:") >= 0)
                        {
                            string gip = gamedateStr.Substring(gamedateStr.IndexOf("ClientIP:"), gamedateStr.Length - gamedateStr.IndexOf("ClientIP:")).Replace("ClientIP:", "");
                            string uaddress = GetIPLocation(gip);
                            task.F_URInfo += "\r\n【玩家地址】" + uaddress;
                            //   task.F_Note += "\r\n【玩家地址】" + uaddress;
                        }
                    }
                    int result = db.AddTask(task, sp);
                    if (result == 0)
                    {
                        Log.Info("执行了GSSTaskAdd,保存失败,请求IP:" + GetIP4Address() + "");
                        return "保存失败,数据ERROR";
                    }
                    else return "0";
                }
                else
                {
                    task.F_Note = note;

                    task.F_GameBigZone = null;
                    task.F_GameZone = null;
                    task.F_GUserID = null;
                    task.F_GUserName = null;
                    task.F_GRoleID = null;
                    task.F_GRoleName = null;
                    task.F_GameName = null;
                    task.F_CreatTime = null;
                    task.F_LimitTime = null;
                    task.F_EditTime = DateTime.Now;
                    task.F_From = null;
                    task.F_LimitType = null;
                    task.F_OLastLoginTime = null;
                    task.F_State = null;
                    task.F_Type = null;
                    task.F_VipLevel = null;
                    task.F_Title = null;



                    task.F_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["F_ID"]);
                    int result = db.EditTask(task, sp);
                    if (result == 0)
                    {
                        Log.Info("执行了GSSTaskAdd,保存失败,请求IP:" + GetIP4Address() + "");
                        return "保存失败,数据ERROR";
                    }
                    else return "0";
                }

            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAdd,请求IP:" + GetIP4Address() + "", ex);
                return "执行异常GSSWebService" + ex.Message;
            }

        }

        [WebMethod(Description = "GSS工单信息查询（消息）")]
        public DataSet GSSMSGList(string roleid)
        {
            if (!VerifyRemoteServiceIP(GetIP4Address()))
            {
                Log.Info("GSSTaskAdd,非法请求,请求IP:" + GetIP4Address() + "");
                return null;
            }

            roleid = roleid.Replace("'", "’").Replace(",", "，");

            if (!CheckQuerySql(roleid))
            {
                Log.Info("GSSTaskAdd,非法请求(PARAMETER) ,请求IP:" + GetIP4Address() + "");
                return null;
            }
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                GSSServerLibrary.DBHandle db = new GSSServerLibrary.DBHandle(ConnStrGSSDB);
                DataSet ds = db.GetTaskLog(" and F_Rowtype=6 and F_OCanRestor is null and F_Note is not null and F_ID in (select F_ID from T_Tasks with(nolock) where  F_GRoleID=" + roleid + " and F_Type=20000216)", sp);

                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["F_EditMan"].ToString().Length != 0 && dr["F_OCanRestor"].ToString() == "")
                        {
                            GSSModel.Tasks model = new GSSModel.Tasks();
                            model.F_ID = Convert.ToInt32(dr["F_ID"]);
                            model.F_OCanRestor = true;
                            db.EditTaskLog(model, sp);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                }
                return ds;
            }
            catch (System.Exception ex)
            {
                Log.Warn("执行了GSSTaskAdd,请求IP:" + GetIP4Address() + "", ex);
                return null;
            }

        }


        /// <summary>
        /// 判断请求SQL语句,保证数据安全
        /// </summary>
        private bool CheckQuerySql(string querysql)
        {
            string sql = querysql.ToLower();
            string[] CheckItems = new string[] { "insert", "update", "delete", "into", "create", "alter", "waitfor", "open", "truncate", "drop ", "exec", "holdlock", ",", "'", " or " };//请输入小写特定字符串

            foreach (string CheckItem in CheckItems)
            {
                if (sql.IndexOf(CheckItem) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 得到IPV4
        /// </summary>
        public string GetIP4Address()
        {
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

        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddTask(GSSModel.Tasks model, DbHelperSQLP sp)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.VarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.VarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.VarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.VarChar),
                    new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Direction = ParameterDirection.Output;

            try
            {
                sp.RunProcedure("GSS_Tasks_ADD", parameters, out rowsAffected);
                int id = (int)parameters[34].Value;
                return id;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }



        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTask(string whereStr, string OrderFieldName, string OrderType, int PageSize, int PageIndex, DbHelperSQLP sp)
        {
            //临时使用条件查询分页数据,后续在服务端使用存储过程分页
            int pe = PageSize * PageIndex;
            int pb = pe - PageSize + 1;

            string PwhereStr = " and F_ID in (select F_ID from ( SELECT top 10000000 ROW_NUMBER() OVER(order by F_ID asc) AS rownum,F_ID FROM T_Tasks where 1=1   " + whereStr + ") a where rownum between " + pb + " and " + pe + ") order by F_id asc";

            string sql = @"SELECT top 100 * FROM T_Tasks where 1=1 " + PwhereStr;

            DataSet ds = sp.Query(sql);


            DataTable dtold = ds.Tables[0].Copy();
            DataTable dtnew = ds.Tables[0].Clone();
            dtnew.TableName = "dtnew";

            dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
            int colCount = dtnew.Columns.Count;
            for (int i = 1; i < colCount; i++)
            {
                dtnew.Columns[i].DataType = System.Type.GetType("System.String");
            }

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //    DataRow drnew = dtnew.NewRow();

            //    for (int i = 0; i < colCount; i++)
            //    {
            //        drnew[i] = dr[i].ToString();
            //    }
            //    //字典表
            //    string[] drdics = { "F_From", "F_VipLevel", "F_State" };
            //    foreach (string dic in drdics)
            //    {
            //        drnew[dic] = GetDicValue(drnew[dic].ToString());
            //    }
            //    string[] drdicsp = { "F_Type" };
            //    foreach (string dic in drdicsp)
            //    {
            //        drnew[dic] = GetDicPCValue(drnew[dic].ToString());
            //    }
            //    //游戏配置表
            //    string[] drgcs = { "F_GameName" };
            //    foreach (string gc in drgcs)
            //    {
            //        drnew[gc] = GetGconfigName(drnew[gc].ToString());
            //    }
            //    //用户名
            //    string[] drusers = { "F_DutyMan", "F_PreDutyMan", "F_CreatMan", "F_EditMan" };
            //    foreach (string user in drusers)
            //    {
            //        drnew[user] = GetUserName(drnew[user].ToString());
            //    }

            //    //时间
            //    string[] drdates = { "F_LimitTime", "F_CreatTime", "F_EditTime" };
            //    foreach (string date in drdates)
            //    {
            //        try
            //        {
            //            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
            //        }
            //        catch (System.Exception ex)
            //        {
            //            drnew[date] = "";
            //        }

            //    }

            //    dtnew.Rows.Add(drnew);

            //}
            ds.Tables.Clear();
            ds.Tables.Add(dtnew);
            ds.Tables.Add(dtold);

            return ds;
        }

        /// <summary>
        /// 得到IP地址
        /// </summary>
        private string GetIPLocation(string value)
        {
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                string sql = @"DECLARE @ip NVARCHAR(50);select @ip=dbo.F_IP2int('" + value + "');select top 1 F_Note from T_BaseIPAddress where  F_IPBeginN<= @ip and F_IPEndN>=@ip";
                object rvalue = sp.GetSingle(sql);
                if (rvalue != null)
                {
                    return rvalue.ToString();
                }
                else
                {
                    return "未查询到(null)";
                }
            }
            catch (System.Exception ex)
            {
                return "未查询到(ex)";
            }

        }


        /// <summary>
        /// 得到问卷调查项
        /// </summary>
        private string GetAnswerItemKey(string value)
        {
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                string sql = @"UPDATE T_QuestKey  set F_AnswerNum=F_AnswerNum+1  where F_KeyID=" + value + " SELECT  F_ItemTitle+'['+ F_KeyTitle+']' FROM T_QuestKey where F_KeyID=" + value + "";
                object rvalue = sp.GetSingle(sql);
                if (rvalue != null)
                {
                    return rvalue.ToString();
                }
                else
                {
                    return "未查询到(null)" + value;
                }
            }
            catch (System.Exception ex)
            {
                return "未查询到(ex)" + value;
            }

        }

        /// <summary>
        /// 得到是否已经提交过问卷调查
        /// </summary>
        private bool IsHaveSubRequest(string value)
        {
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                string sql = @"SELECT top 1 1 FROM T_Tasks WHERE (F_Type = 20000215) AND (F_GUserID =" + value + ")";
                object rvalue = sp.GetSingle(sql);
                if (rvalue != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return true;
            }

        }

        /// <summary>
        /// 得到是大区的WEBSERVICE地址
        /// </summary>
        private string GetBigZoneWBUrl(string value)
        {
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGSSDB;
                string sql = @"SELECT top 1 F_Value FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_Name = '" + value + "')";
                object rvalue = sp.GetSingle(sql);
                if (rvalue != null)
                {
                    return rvalue.ToString();
                }
                else
                {
                    return value;
                }
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }


        private string GiftUserAdd(string bigzone, string userid, string roleid, string zoneid, string giftid, string giftnum, string note)
        {
            try
            {
                GSSWebService.ServiceXLJ ws = new GSSWebService.ServiceXLJ();
                ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                ws.Url = GetBigZoneWBUrl(bigzone);
                return ws.GameGiftUserAdd(userid, roleid, zoneid, giftid, giftnum, note);
            }
            catch (System.Exception ex)
            {
                return "调用GSSWebService" + ex.Message;
            }

        }
        private bool VerifyRemoteServiceIP(string remoteIP)
        {
            bool limit = ConfigurationManager.AppSettings["RemoteServerIP"] == "true";
            if (!limit)
                return true;
            if (string.IsNullOrEmpty(RemoteServerIP))
            {
                return true;
            }
            string[] ips = RemoteServerIP.Split(',');
            if (ips.Contains(remoteIP))
                return true;
            return false;
        }
    }

}
