using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GSSCSFrameWork;
using GSS.DBUtility;
using GSSServerLibrary;
using MySql.Data.MySqlClient;
namespace GSSServer
{
    public abstract class WebServiceLib
    {
        static WebServiceGame.ServiceXLJ webserv = new WebServiceGame.ServiceXLJ();
        static WebServiceZone.WebServiceZone WebServiceZone = new WebServiceZone.WebServiceZone();

        public WebServiceLib()
        {
            "log4net".Logger();
            ShareData.Log = log4net.LogManager.GetLogger("GSSLog");
            "log4net end".Logger();
        }

        /// <summary>
        /// 得到游戏用户列表,客服主页用
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet GetGameUsersC(string query)
        {
            try
            {
                string sql = @"SELECT F_Name,F_Value,F_ValueGame FROM T_GameConfig WITH(NOLOCK) where F_ParentID=1000 and F_IsUsed=1";
                string.Format(typeof(WebServiceLib).Name + "\r\n" + query).Logger();
                DataSet dsc = DbHelperSQL.Query(sql);
                if (dsc == null)
                {
                    "query result:null".Logger();
                    return null;
                }


                string url = "";
                DataSet ds = null;
                "GetGameUsersC->url".Logger();
                foreach (DataRow dr in dsc.Tables[0].Rows)
                {
                    url = dr["F_Value"].ToString();

                    if (url.Trim().Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        webserv.Url = url;
                        (typeof(WebServiceLib).Name + ":" + url).Logger();
                        webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        string.Format("  webserv.Credentials :{0}", webserv.Credentials.ToString()).Logger();
                        string.Format("will call wcf:[GetGameUsersC] ->").Logger();
                        byte[] bytes = webserv.GetGameUsersC(query);
                        string.Format("end call wcf:[GetGameUsersC] ->").Logger();
                        if (bytes != null)
                        {
                            string.Format("call wcf response byte:{0}", bytes.Length).Logger();
                        }
                        else
                        {
                            string.Format("call wcf response byte:null").Logger();
                            return null;
                        }
                        DataSet dstemp = DataSerialize.GetDatasetFromByte(bytes);
                        if (dstemp != null)
                        {
                            DataTable table = dstemp.Tables[0];
                            table.Columns.Add("F_BigZoneName", System.Type.GetType("System.String"));
                            table.Columns.Add("F_ValueGame", System.Type.GetType("System.String"));
                            foreach (DataRow drt in dstemp.Tables[0].Rows)
                            {
                                drt["F_BigZoneName"] = dr["F_Name"].ToString();
                                drt["F_ValueGame"] = dr["F_ValueGame"].ToString();
                            }
                            if (ds == null)
                            {
                                ds = dstemp.Copy();
                            }
                            else
                            {
                                table.Merge(dstemp.Tables[0]);
                            }
                            ("GetGameUsersC Query Rows:" + table.Rows.Count).Logger();
                        }
                        else
                        {
                            ("GetGameUsersC Query Rows: null").Logger();
                        }
                    }

                }
                return ds;
            }
            catch (System.Exception ex)
            {
                ex.Message.Logger();
                //日志记录
                ShareData.Log.Error("得到WEBSERVICE游戏用户列表错误\r\n" + ex.Message, ex);
                return null;
            }

        }
        /// <summary>
        /// 得到游戏角色列表,客服主页用
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataSet GetGameRoleC(string value)
        {
            try
            {
                value = value.Trim();
                string.Format("class:{0},function:{1} is runing,param :[ {2} ]", typeof(WebServiceLib).Name, "GetGameRoleC", value).Logger();
                string[] values = value.Split('|');
                string url = GetWebServUrl("1000", values[0]);
                string userid = values[1];
                if (url.Trim().Length == 0)
                {
                    "query web url is: null".Logger();
                    return null;
                }
                else
                {
                    string.Format("query web url is :", url).Logger();
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                byte[] bytes = webserv.GetGameRoleC(userid);
                if (bytes == null)
                {
                    "call wcf :[GetGameRoleC] ,query result is :null".Logger();
                    return null;
                }
                else
                {
                    string.Format("call wcf :[GetGameRoleC] ,query result is[byte]:{0}", bytes.Length).Logger();
                }
                DataSet ds = DataSerialize.GetDatasetFromByte(bytes);
                return ds;
            }
            catch (System.Exception ex)
            {
                ex.Message.Logger();
                //日志记录
                ShareData.Log.Error("得到WEBSERVICE游戏角色列表错误\r\n" + ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 得到游戏角色列表,客服主页用
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataSet GetGameRoleCR(string value)
        {
            try
            {
                string.Format("GetGameRoleCR->param:[ {0} ]", value).Logger();
                string[] values = value.Split('|');
                string url = GetWebServUrl("1000", values[0]);
                string whereStr = values[1];
                if (url.Trim().Length == 0)
                {
                    string.Format("GetGameRoleCR->query url is null").Logger();
                    return null;
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                string.Format("will call {0}/GetGameRoleCR", url).Logger();
                byte[] bytes = webserv.GetGameRoleCR(whereStr);
                DataSet ds = DataSerialize.GetDatasetFromByte(bytes);
                DataSetRowTotalLog(ds, typeof(WebServiceLib).Name + ".GetGameRoleCR");
                return ds;
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Error("得到WEBSERVICE游戏角色列表错误\r\n" + ex.Message, ex);
                return null;
            }
        }


        /// <summary>
        /// 得到游戏角色列表,客服主页用
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataSet GetGameRoleCRALL(string value)
        {
            try
            {

                string sql = @"SELECT F_Name,F_Value FROM T_GameConfig WITH(NOLOCK) where F_ParentID=1000";
                DataSet dsc = DbHelperSQL.Query(sql);
                DataSetRowsLog(dsc, "GetGameRoleCRALL [step] T_GameConfig");
                if (dsc == null)
                {
                    return null;
                }

                string url = "";
                DataSet ds = null;

                string[] values = value.Split('|');
                foreach (DataRow dr in dsc.Tables[0].Rows)
                {
                    url = dr["F_Value"].ToString();

                    if (url.Trim().Length == 0)
                    {
                        continue;
                    }
                    else
                    {

                        webserv.Url = url;
                        string.Format("{0}/{1}", url, "GetGameRoleCR").Logger();
                        webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        byte[] bytes = webserv.GetGameRoleCR(values[1]);
                        DataSet dstemp = DataSerialize.GetDatasetFromByte(bytes);
                        if (dstemp != null)
                        {
                            dstemp.Tables[0].Columns.Add("F_BigZoneName", System.Type.GetType("System.String"));
                            foreach (DataRow drt in dstemp.Tables[0].Rows)
                            {
                                drt["F_BigZoneName"] = dr["F_Name"].ToString();
                            }
                            if (ds == null)
                            {
                                ds = dstemp.Copy();
                            }
                            else
                            {
                                ds.Tables[0].Merge(dstemp.Tables[0]);
                            }
                        }

                    }

                }
                DataSetRowsLog(ds, "GetGameRoleCRALL");
                return ds;

            }
            catch (System.Exception ex)
            {
                ex.Message.Logger(typeof(GSSServer.WebServiceLib).Name);
                //日志记录
                ShareData.Log.Error("得到WEBSERVICE游戏角色列表错误\r\n" + ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 同步游戏中的战区和战线到GSS系统的配置表
        /// </summary>
        /// <returns></returns>
        public static string SynGameZoneLine()
        {
            try
            {
                string sql = @"SELECT F_ID,F_Name, F_Value, F_ValueGame FROM T_GameConfig WHERE (F_ParentID = 1000)";
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "true";
                }
                string reback = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string url = dr["F_Value"].ToString();
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    DataSet dszl = DataSerialize.GetDatasetFromByte(webserv.GetZoneLine());
                    if (dszl == null || ds.Tables[0].Rows.Count == 0)
                    {
                        continue;
                    }
                    sql = @"delete from T_GameConfig where F_ParentID=" + dr["F_ID"].ToString() + " ";
                    DbHelperSQL.ExecuteSql(sql);
                    int i = 0;
                    foreach (DataRow drz in dszl.Tables[0].Select("F_GParentID=0", "F_GID ASC"))
                    {
                        i++;
                        string id = dr["F_ID"].ToString() + i.ToString().PadLeft(2, '0');
                        sql = @"INSERT INTO T_GameConfig (F_ID,F_ParentID, F_Name, F_Value, F_ValueGame, F_IsUsed, F_Sort) VALUES (" + id + "," + dr["F_ID"] + ", N'" + drz["F_GName"] + "', N'', N'" + drz["F_GID"] + "', 1, 0)";
                        DbHelperSQL.ExecuteSql(sql);


                        sql = @"delete from T_GameConfig where F_ParentID=" + id + " ";
                        DbHelperSQL.ExecuteSql(sql);
                        int ii = 0;
                        foreach (DataRow drl in dszl.Tables[0].Select(" F_GParentID=" + drz["F_GID"] + " and F_GParentID<>0 "))
                        {
                            ii++;
                            string idl = id + ii.ToString().PadLeft(2, '0');
                            sql = @"INSERT INTO T_GameConfig (F_ID,F_ParentID, F_Name, F_Value, F_ValueGame, F_IsUsed, F_Sort) VALUES (" + idl + "," + id + ", N'" + drl["F_GName"] + "', N'', N'" + drl["F_GID"] + "', 1, 0)";
                            DbHelperSQL.ExecuteSql(sql);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
            return "true";
        }

        /// <summary>
        /// 帐户/角色封停,帐户:1,角色:2
        /// </summary>
        public static string URlock(string UserOrRole, string taskid, string locktype)
        {
            if (!WinUtil.IsNumber(taskid) || !WinUtil.IsNumber(locktype))
            {
                return "命令传输错误";
            }
            try
            {
                int uorr = Convert.ToInt32(UserOrRole);

                string sql = @"SELECT F_GameName,F_GameBigZone,F_GUserID,F_GUserName,F_GRoleID,F_GRoleName FROM T_Tasks where F_ID= " + taskid;

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }

                DataRow dr = ds.Tables[0].Rows[0];

                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                if (url.Trim().Length == 0)
                {
                    return "服务器:WEBSERVICE地址配置不正确";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                sql = @"SELECT F_ID,F_ParentID,F_Name,F_Value,F_IsUsed,F_Sort FROM T_GameConfig where F_ID=" + locktype;

                DataSet dsc = DbHelperSQL.Query(sql);

                if (dsc == null)
                {
                    return "服务器WEBSERVICE配置错误";
                }

                DataRow drc = dsc.Tables[0].Rows[0];

                string reback = "";
                if (uorr == 1)
                {
                    reback = webserv.SetUserLock(dr["F_GUserID"].ToString(), dr["F_GUserName"].ToString(), locktype, drc["F_Value"].ToString());
                }
                else if (uorr == 2)
                {
                    reback = webserv.SetRoleLock(dr["F_GRoleID"].ToString(), dr["F_GRoleName"].ToString(), locktype, drc["F_Value"].ToString());
                }
                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 帐户/角色解封,帐户:1,角色:2
        /// </summary>
        public static string URNolock(string UserOrRole, string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {
                string.Format("unlock action is  user or role (1->user,2->role ),input=[{0}]", UserOrRole);
                int uorr = Convert.ToInt32(UserOrRole);

                string sql = @"SELECT F_GameName,F_GameBigZone,F_GUserID,F_GUserName,F_GRoleID,F_GRoleName FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                string.Format("url->{0}", url).Logger();
                if (url.Trim().Length == 0)
                {
                    return "服务器:WEBSERVICE地址配置不正确";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                string reback = string.Empty;
                if (uorr == 1)
                {
                    "will unlock user unlock".Logger();
                    reback = webserv.SetUserNoLock(dr["F_GUserID"].ToString());
                }
                else if (uorr == 2)
                {
                    "will unlock role unlock".Logger();
                    reback = webserv.SetRoleNoLock(dr["F_GRoleID"].ToString());
                }
                string.Format("response result:{0}", reback).Logger();
                return reback;
            }
            catch (System.Exception ex)
            {
                ex.Message.Logger();
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 帐号借用
        /// </summary>
        public static string GameUserUse(string taskid, string newpsw)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {

                string sql = @"SELECT F_GameName,F_GameBigZone,F_GUserID,F_GUserName,F_GRoleID,F_GRoleName FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                if (url.Trim().Length == 0)
                {
                    return "服务器:WEBSERVICE地址配置不正确";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                string reback = webserv.SetUserGMUse(dr["F_GUserID"].ToString(), newpsw, "GSS客服系统工具");

                return reback;
            }
            catch (System.Exception ex)
            {
                ex.Message.Logger();
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }
        /// <summary>
        /// 帐号归还
        /// </summary>
        public static string GameUserNoUse(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {

                string sql = @"SELECT F_GameName,F_GameBigZone,F_GUserID,F_GUserName,F_GRoleID,F_GRoleName FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                if (url.Trim().Length == 0)
                {
                    return "服务器:WEBSERVICE地址配置不正确";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                string reback = "";

                reback = webserv.SetUserGMBack(dr["F_GUserID"].ToString());

                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }
        /// <summary>
        /// 清空防沉迷
        /// </summary>
        public static string GameResetChildInfo(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {

                string sql = @"SELECT F_GameName,F_GameBigZone,F_GUserID,F_GUserName,F_GRoleID,F_GRoleName FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                if (url.Trim().Length == 0)
                {
                    return "服务器:WEBSERVICE地址配置不正确";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                string reback = "";

                reback = webserv.ClearChildDisInfo(dr["F_GUserID"].ToString());

                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 角色名更改
        /// </summary>
        public static string RoleNameChange(string gamename, string bigzonename, int userid, int roleid, string rolename, string newrolename)
        {
            try
            {
                string url = GetWebServUrl(gamename, bigzonename);
                if (url.IndexOf("http") == -1)
                {
                    return string.Format("GSS游戏配置:{0}-{1}WEBSERVICE地址错误 {2}", gamename, bigzonename, url);
                }

                webserv.Url = url;
                webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                return webserv.RoleNameChange(userid, roleid, rolename, newrolename);
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }
        /// <summary>
        /// 角色战区更改
        /// </summary>
        /// <param name="gamename"></param>
        /// <param name="bigzonename"></param>
        /// <param name="roleid"></param>
        /// <param name="zoneid"></param>
        /// <returns></returns>
        public static string RoleZoneChange(string gamename, string bigzonename, int userid, int roleid, int zoneid)
        {
            try
            {
                string url = GetWebServUrl(gamename, bigzonename);
                if (url.IndexOf("http") == -1)
                {
                    return string.Format("GSS游戏配置:{0}-{1}WEBSERVICE地址错误 {2}", gamename, bigzonename, url);
                }

                webserv.Url = url;
                webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                return webserv.RoleZoneChange(userid, roleid, zoneid);
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 公告工具:开始公告
        /// </summary>
        public static string GameNoticeStart(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {



                string sql = @"SELECT F_GameName,F_URInfo,F_TUseData FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string msg = dr["F_URInfo"].ToString();
                string area = dr["F_TUseData"].ToString();
                string[] TareaAs = area.Split('|');
                foreach (string TareaA in TareaAs)
                {
                    string[] TareaBs = TareaA.Split(',');
                    if (TareaBs.Length != 3)
                    {
                        return "公告范围参数不正确";

                    }
                }

                string reback = "";
                string[] areaAs = area.Split('|');
                foreach (string areaA in areaAs)
                {
                    string[] areaBs = areaA.Split(',');
                    if (areaBs.Length == 3)
                    {
                        reback += "游戏大区" + areaBs[0] + ">>" + areaA + "-";
                        string url = GetWebServUrlID(dr["F_GameName"].ToString(), areaBs[0]);
                        if (url.Trim().Length == 0)
                        {

                            reback += "服务器:WEBSERVICE地址配置不正确;";
                        }
                        else
                        {
                            webserv.Url = url;
                            webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            reback += webserv.GameNoticeStart(taskid, area, msg) + ";";
                        }
                    }
                }
                if (reback.Replace("true", "成功").Length + areaAs.Length * 2 == reback.Length)
                {
                    reback = "true";
                }
                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 公告工具:停止公告
        /// </summary>
        public static string GameNoticeStop(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {

                string sql = @"SELECT F_GameName,F_URInfo,F_TUseData FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string area = dr["F_TUseData"].ToString();
                string[] TareaAs = area.Split('|');
                foreach (string TareaA in TareaAs)
                {
                    string[] TareaBs = TareaA.Split(',');
                    if (TareaBs.Length != 3)
                    {
                        return "公告范围参数不正确";

                    }
                }

                string reback = "";
                string[] areaAs = area.Split('|');
                foreach (string areaA in areaAs)
                {
                    string[] areaBs = areaA.Split(',');
                    if (areaBs.Length == 3)
                    {
                        reback += "游戏大区" + areaBs[0] + ">>";
                        string url = GetWebServUrlID(dr["F_GameName"].ToString(), areaBs[0]);
                        if (url.Trim().Length == 0)
                        {

                            reback += "服务器:WEBSERVICE地址配置不正确;";
                        }
                        else
                        {
                            webserv.Url = url;
                            webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            reback += webserv.GameNoticeStop(taskid);

                        }
                    }

                }
                if (reback.Replace("true", "成功").Length + areaAs.Length * 2 == reback.Length)
                {
                    reback = "true";
                }
                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 公告工具:发奖工具
        /// </summary>
        public static string GameGiftAwardDo(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {

                string sql = @"SELECT F_GameName,F_GameBigZone,F_URInfo,F_TUseData,F_TToolUsed,F_COther FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";

                DataSet ds = DbHelperSQL.Query(sql);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                if (ds.Tables[0].Rows[0]["F_TToolUsed"].ToString() == "True")
                {
                    return "此工单下的发奖信息已经执行";
                }
                DataRow dr = ds.Tables[0].Rows[0];

                string giftstr = dr["F_COther"].ToString();

                byte[] dsuser = (byte[])GSSCSFrameWork.DataSerialize.GetObjectFromString(dr["F_URInfo"].ToString());
                if (dsuser == null)
                {
                    return "工单中的游戏用户列表为空";
                }



                string url = GetWebServUrl(dr["F_GameName"].ToString(), dr["F_GameBigZone"].ToString());
                if (url.Trim().Length == 0)
                {

                    return "GSS服务器:WEBSERVICE地址配置不正确;";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    return webserv.GameGiftAwardDoFor5Num(taskid, giftstr, dsuser);

                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 查询GS日志同步库(离线)
        /// </summary>
        public static byte[] QuerySynGSLog(string zoneid, string querysql)
        {
            string url = "";
            try
            {
                url = GetWebServUrlID1(zoneid.ToString());
                if (url.Trim().Length == 0)
                {
                    return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS("WEBSERVICE查询GS日志同步库(离线)错误:获取URL错误"));
                }
                else
                {
                    WebServiceZone.Url = url;
                    WebServiceZone.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                byte[] bytes = WebServiceZone.QuerySynGSLog(querysql);
                return bytes;
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Error("WEBSERVICE查询GS日志同步库(离线)错误\r\n" + url + "" + ex.Message, ex);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS("WEBSERVICE查询GS日志同步库(离线)错误" + ex.Message));
            }
        }
        /// <summary>
        /// 查询GS实时日志库(实时)
        /// </summary>
        public static byte[] QueryLiveGSLog(string zoneid, string querysql)
        {
            try
            {
                string url = GetWebServUrlID(zoneid.ToString());
                if (url.Trim().Length == 0)
                {
                    return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS("WEBSERVICE查询GS日志实时库(实时)错误:获取URL错误"));
                }
                else
                {
                    WebServiceZone.Url = url;
                    WebServiceZone.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                byte[] bytes = WebServiceZone.QueryLiveGSLog(querysql);
                return bytes;
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Error("WEBSERVICE查询GS日志实时库(实时)错误\r\n" + ex.Message, ex);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS("WEBSERVICE查询GS日志实时库(实时)错误" + ex.Message));
            }
        }
        /// <summary>
        /// 得到WEBSERVICE地址
        /// </summary>
        private static string GetWebServUrl(string gamenameid, string zonename)
        {
            try
            {
                string sql = @"SELECT F_Value FROM T_GameConfig WITH(NOLOCK) where F_ParentID=" + gamenameid + " and F_Name=N'" + zonename + "'";
                sql.Logger();
                string.Format("ConnString:-> \t{0}", DbHelperSQL.connectionString).Logger();
                DataSet dsc = DbHelperSQL.Query(sql);

                if (dsc == null || dsc.Tables[0].Rows.Count == 0)
                {
                    "function:GetWebServUrl ->result is null".Logger();
                    return "";
                }
                DataRow drc = dsc.Tables[0].Rows[0];
                return drc[0].ToString();
            }
            catch (System.Exception ex)
            {
                ex.Message.Logger();
                ShareData.Log.Error(ex);
                return "";
            }
        }
        /// <summary>
        /// 得到WEBSERVICE地址
        /// </summary>
        private static string GetWebServUrlID(string gamenameid, string zoneid)
        {
            try
            {
                string sql = @"SELECT F_Value FROM T_GameConfig WITH(NOLOCK) where F_ParentID=" + gamenameid + " and F_ValueGame=" + zoneid + "";

                DataSet dsc = DbHelperSQL.Query(sql);

                if (dsc == null || dsc.Tables[0].Rows.Count == 0)
                {
                    return "";
                }
                DataRow drc = dsc.Tables[0].Rows[0];
                return drc[0].ToString();
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return "";
            }
        }
        /// <summary>
        /// 得到WEBSERVICE地址
        /// </summary>
        private static string GetWebServUrlID(string gamenameid)
        {
            try
            {
                string sql = @"SELECT F_Value FROM T_GameConfig WITH(NOLOCK) where F_ID=" + gamenameid + "";

                DataSet dsc = DbHelperSQL.Query(sql);

                if (dsc == null || dsc.Tables[0].Rows.Count == 0)
                {
                    return "";
                }
                DataRow drc = dsc.Tables[0].Rows[0];
                return drc[0].ToString();
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return "";
            }
        }
        /// <summary>
        /// 得到WEBSERVICE地址
        /// </summary>
        private static string GetWebServUrlID1(string gamenameid)
        {
            try
            {
                string sql = @"SELECT F_Value1 FROM T_GameConfig WITH(NOLOCK) where F_ID=" + gamenameid + "";

                DataSet dsc = DbHelperSQL.Query(sql);

                if (dsc == null || dsc.Tables[0].Rows.Count == 0)
                {
                    return "";
                }
                DataRow drc = dsc.Tables[0].Rows[0];
                return drc[0].ToString();
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return "";
            }
        }


        /// <summary>
        /// 返回错误信息构成的DataSet
        /// </summary>
        private static DataSet GetErrorDS(string erro)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("信息提示");
            ds.Tables[0].Rows.Add();
            ds.Tables[0].Rows[0][0] = erro;
            return ds;
        }
        public static string RecoveryRoleWithRollBack(GSSModel.Request.RoleData role)
        {
            string code = (new DBHandle()).RecoveryRole(role);
            if (code != true.ToString())
            {
                return code;
            }
            //读取MySQL匹配的连接串
            string linkName = string.Format("LKSV_GSS_6_ZoneRoleDataDB_{0}_{1}", role.BigZoneId, role.ZoneId);
            string query = string.Format("select provider_string from sys.servers  where name='{0}'", linkName);
            string connString = string.Empty;
            System.Data.SqlClient.SqlDataReader d = DbHelperSQL.ExecuteReader(query);
            if (d.Read())
            {
                connString = d["provider_string"] as string;
            }
            d.Close();
            if (string.IsNullOrEmpty(connString))
            {// 远程数据库连接实例名出现异常
                return "4042";
            }
            //需要进行特殊项过滤： DRIVER,OPTION,STMT
            string filter = FilterDBConnString(connString);
            DbHelperMySQLP db = new DbHelperMySQLP(filter);
            MySqlParameter[] param = new MySqlParameter[]{
                new MySqlParameter("ROLEID",MySqlDbType.Int32){Value=role.RoleID},
                new MySqlParameter("USERID",MySqlDbType.Int32){Value=role.UserID},
                new MySqlParameter("OverRoleID",MySqlDbType.Int32){Value=role.RoleID},
                new MySqlParameter("OverUserID",MySqlDbType.Int32){Value=role.UserID},
                new MySqlParameter("CODE",MySqlDbType.Int32)
            };
            param[param.Length - 1].Direction = ParameterDirection.Output;
            string msg = db.RunNoQueryProcedure("TakeRoleData", param);
            object outd = param[param.Length - 1].Value;
            if (outd == null)
            {
                return msg;
            }
            int c = (int)outd;
            if (c > 0)
            {
                return true.ToString();
            }
            return msg;
        }
        static string FilterDBConnString(string dbConstring)
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
        public static string SetRolesEmail(GSSModel.SendEmailToRole email)
        {
            //读取MySQL匹配的连接串[此处返回错误码]
            string linkName = string.Format("LKSV_GSS_3_gsdata_db_{0}_{1}", email.BigZoneId, email.ZoneId);
            string error = string.Empty;
            string connString = QueryMysqlLinkConnString(linkName, ref error);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            string filter = FilterDBConnString(connString);
            //查询目前数据库中最大的ID
            DbHelperMySQLP.connectionString = filter;
            //DbHelperMySQLP db = new DbHelperMySQLP(connString);
            string queryIdCmd = "select MAX(mail_id) from maillist_table";
            DbHelperMySQL.connectionString = filter;
            DataSet ds = DbHelperMySQL.Query(queryIdCmd);
            DataRowCollection rows = ds.Tables[0].Rows;
            //此处不考虑连接失败的情况
            long id = 1;
            if (rows.Count > 0)
            {
                object obj = rows[0][0];
                string mid = obj == null ? string.Empty : obj.ToString();
                if (!string.IsNullOrEmpty(mid))//该列数据存储不是为null
                {
                    id = long.Parse(mid);
                }
            }
            string[] rids = email.ReceiveRoles.Split(',');
            List<byte> item1 = new List<byte>();
            if (email.EquipNum > 0)
            {
                item1.AddRange(BitConverter.GetBytes(email.EquipNum));
                item1.AddRange(BitConverter.GetBytes(email.EquipId));
            }
            StringBuilder sbs = new StringBuilder();
            foreach (byte item in item1)
            {
                string i = item.ToString("X2");
                sbs.Append(i);
            }
            for (int i = 0; i < rids.Length; i++)
            {//MySQL 此处提供参数不能使用【底层代码需要进行确认】
                List<byte> checkdata = new List<byte>();
                checkdata.AddRange(BitConverter.GetBytes(0));//第一项4字节int
                checkdata.AddRange(BitConverter.GetBytes((byte)0));//第二项byte
                checkdata.AddRange(BitConverter.GetBytes(0));//第三项4字节int
                checkdata.AddRange(BitConverter.GetBytes(int.Parse(rids[i])));//第四项2字节word
                checkdata.AddRange(BitConverter.GetBytes(0));//第五项4字节int
                checkdata.AddRange(BitConverter.GetBytes((byte)3));//第六项？？ mailtype
                checkdata.AddRange(BitConverter.GetBytes((short)0));//第七项 已读状态 byte[wstate]
                checkdata.AddRange(BitConverter.GetBytes((byte)0));//第八项 byte
                checkdata.AddRange(BitConverter.GetBytes(0));//第九项 byte
                StringBuilder checkDataStr = new StringBuilder();
                foreach (byte item in checkdata)
                {
                    string cd = item.ToString("X2");
                    checkDataStr.Append(cd);
                }
                string cmd = @"insert into maillist_table(Mail_ID,Mail_Title,Mail_Content,Mail_TYPE,Receiver_ID,Sender_ID,Sender_Name,Send_Time,Invalid_Time,State_Modi_Time,Item_Num,item1,checkData)  
                values (" + (id + i + 1) + ",'" + email.Head + "','" + email.Body + "',3," + rids[i] + ",-1,'GSSClient','" + email.StartTime + "','" + email.EndTime + "','"
                          + DateTime.Now + "'," + email.EquipNum + ",{0},0x{1})";
                string equip = sbs.ToString();
                if (string.IsNullOrEmpty(equip)) equip = "null";
                else equip = "0x" + equip;
                cmd = string.Format(cmd, equip, checkDataStr.ToString());
                try
                {
                    DbHelperMySQLP.connectionString = filter;
                    int result = DbHelperMySQLP.ExecuteSql(cmd);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return string.Empty;
        }
        public static string AddActiveFallConfig(GSSModel.Request.ActiveFallGoodsData fall)
        {//LKSV_7_gspara_db_0_1
            //首先将数据写入到GSSDB中
            List<string> scenes = new List<string>();
            foreach (int item in fall.SceneIds)
            {
                scenes.Add(item.ToString());
            }

            GSSModel.Request.ActiveFallGoodLogicData goods = fall.MapObject<GSSModel.Request.ActiveFallGoodsData, GSSModel.Request.ActiveFallGoodLogicData>(true);
            string json = goods.ConvertJson();
            GSSModel.SampleWorkOrder sample = new GSSModel.SampleWorkOrder()
            {
                Title = goods.ActiveName,
                Note = goods.ActiveName,
                BigZoneName = fall.BigZoneName,
                ZoneName = fall.ZoneName,
                TypeId = fall.TaskTypeID,
                AppId = fall.AppId,
                CreateBy = fall.CreateBy,
                LogicData = json
            };
            DBHandle gss = new DBHandle();
            int taskId = gss.AddSampleTaskWithLogicData(sample);
            if (!fall.SyncConfig)
            {//是否同时将配置写入到MySQL？
                return string.Empty;
            }
            string linkName = string.Format("LKSV_GSS_7_gspara_db_{0}_{1}", fall.BigZoneID, fall.ZoneID);
            string conn, error = string.Empty;
            conn = QueryMysqlLinkConnString(linkName, ref error);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            //此处需要确认MySQL中新增了列【最低等级,最高等级,掉落类型】
            string cmd = @"insert into hugeaward_cfg(szName,NGSID,tBeginDate,tEndDate,tBeginTime,tEndTime,nDropItemID,nDropMax,nDropProbability,szDropScene,minLevel,maxLevel,fallType,TaskId ) 
            values('{0}',-1,'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}',{9},{10},{11},{12})";
            //nDropProbability 存储的是概率分母
            cmd = string.Format(cmd, fall.ActiveName, fall.StartTime.ToString(AppConfig.DateFormat), fall.EndTime.ToString(AppConfig.DateFormat),
                fall.StartTime.ToString(AppConfig.TimeFormat), fall.EndTime.ToString(AppConfig.TimeFormat),
                fall.EquipNo, fall.FallGoodNum, fall.GoodsFallPRDenominator, string.Join(",", scenes.ToArray()), fall.MinRoleLevel, fall.MaxRoleLevel, fall.FallType, taskId);
            string filter = FilterDBConnString(conn);
            //查询目前数据库中最大的ID
            DbHelperMySQL.connectionString = filter;
            try
            {
                int result = DbHelperMySQL.ExecuteSql(cmd);
                if (result > 0)
                {
                    gss.FinshSyncLogicData(taskId);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                msg.Logger();
                return msg;
            }
            return string.Empty;
        }
        private static string QueryMysqlLinkConnString(string linkServerName, ref string errorMsg)
        {
            string query = string.Format("select name, provider_string from sys.servers  where name='{0}'", linkServerName);
            string connString = string.Empty, linkname = string.Empty;
            System.Data.SqlClient.SqlDataReader d = DbHelperSQL.ExecuteReader(query);
            if (d.Read())
            {
                connString = d["provider_string"] as string;
                linkname = d["name"] as string;
            }
            d.Close();
            errorMsg = string.Empty;
            if (string.IsNullOrEmpty(linkname))
            { //没有创建连接服务器
                errorMsg = LanguageResource.Language.Tip_LinkServerStatue_4041;
            }
            else if (string.IsNullOrEmpty(connString))
            {
                errorMsg = LanguageResource.Language.Tip_LinkServerStatue_4043;
            }
            return connString;
        }
        static void MySqlParamReplaceValue(List<MySqlParameter> mp, string pname, object value)
        {
            mp = mp.Where(p =>
            {
                if (p.ParameterName == pname)
                {
                    p.Value = value;
                }
                return true;
            }).ToList();
        }
        public static void DataSetRowsLog(DataSet ds, string cmd)
        {
            DataSetRowTotalLog(ds, cmd);
        }
        static void DataSetRowTotalLog(DataSet ds, string cmd)
        {
            int? rows = null;
            if (ds != null || ds.Tables.Count > 0)
            {
                rows = ds.Tables[0].Rows.Count;
            }
            string.Format("{0}->query result={1}", cmd, (rows.HasValue ? rows.Value.ToString() : "null")).Logger();
        }
        /// <summary>
        /// 删除全服邮件
        /// </summary>
        public static string DeleteFullServiceEmail(string taskid)
        {
            if (!WinUtil.IsNumber(taskid))
            {
                return "命令传输错误";
            }
            try
            {
                string reback = string.Empty;
                string sql = @"SELECT F_GameName,F_URInfo,F_TUseData FROM T_Tasks  WITH(NOLOCK)  where F_ID= " + taskid + "";
                DataSet ds = DbHelperSQL.Query(sql);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return "此工单已经不存在";
                }
                string str1 = "DELETE FROM OPENQUERY (LKSV_GSS_7_gspara_db_0_1,'SELECT * FROM sys_loss_award_table WHERE DBID=" + taskid + "' )";
                string str2 = "DELETE FROM dbo.T_Tasks WHERE F_ID=" + taskid;
                int res = DbHelperSQL.ExecuteSql(str1);
                int res1 = DbHelperSQL.ExecuteSql(str2);
                if (res1>0)
                {
                    reback = "true";
                }
                else
                {
                    reback = "false";
                }
                return reback;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }

        public static string DisChatAdd(GSSModel.Tasks task)
        {
            try
            {
                string url = "http://127.0.0.1/GSSWebService/ServiceXLJ.asmx";//GetWebServUrl(task.F_GameName.ToString(), "第一大区");//task.F_GameBigZone);
                if (url.Trim().Length == 0)
                {
                    return "GSS服务器:WEBSERVICE地址配置不正确;";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    return webserv.AddDisChat(task.F_GUserID, task.F_GRoleID,Convert.ToInt32(task.F_LimitType));
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }
        public static string DisChatDel(GSSModel.Tasks task)
        {
            try
            {
                string url = "http://127.0.0.1/GSSWebService/ServiceXLJ.asmx";//GetWebServUrl(task.F_GameName.ToString(), "第一大区");//task.F_GameBigZone);
                if (url.Trim().Length == 0)
                {
                    return "GSS服务器:WEBSERVICE地址配置不正确;";
                }
                else
                {
                    webserv.Url = url;
                    webserv.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    return webserv.DisChatDel(task.F_GUserID, task.F_GRoleID);
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error(ex);
                return ex.Message;
            }
        }
    }
}
