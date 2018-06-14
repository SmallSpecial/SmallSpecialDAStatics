using System;
using System.Collections.Generic;
using System.Text;
using WSS.DBUtility;
using System.Web;
using WSS.Model;
using Coolite.Ext.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WSS.BLL
{
    public class  AllOther
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject,object obj,object obj1)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }
        public static int GetConfigInt(string name)
        {
            return 0;
        }
        public static void ComboBoxDic(ComboBox cb, string pid)//绑定下拉控件-字典表
        {
            object objType = GetCache("T_DictionaryPid" + pid);
            List<WSS.Model.Dictionary> listdc = null;
            if (objType == null)
            {
                try
                {
                    listdc = new WSS.BLL.Dictionary().GetModelList("F_ParentID=" + pid + " and F_IsUsed=1 order by F_Sort asc");
                    SetCache("T_DictionaryPid" + pid, listdc);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            else
            {
                listdc = (List< WSS.Model.Dictionary>) objType;
            }

            for (int i = 0; i < listdc.Count; i++)
            {
                cb.Items.Add(new Coolite.Ext.Web.ListItem(listdc[i].F_Value, listdc[i].F_DicID.ToString()));
            }
        }

        public static void ComboBoxUser(ComboBox cb, string where)//绑定下拉控件-用户表
        {
            object objType = GetCache("T_UsersUid" + cb.ID);
            List<WSS.Model.Users> listdc = null;
            if (objType == null)
            {
                try
                {
                    listdc = new WSS.BLL.Users().GetModelList(where + " and F_IsUsed=1 order by F_DepartID asc");
                    SetCache("T_UsersUid" + cb.ID, listdc);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            else
            {
                listdc = (List<WSS.Model.Users>)objType;
            }

            for (int i = 0; i < listdc.Count; i++)
            {
                cb.Items.Add(new Coolite.Ext.Web.ListItem(listdc[i].F_UserName, listdc[i].F_UserID.ToString()));
            }
        }

        public static string GetWebServiceUrl(string GameZoneIDStr)//得到游戏大区相应的WEBSERVICE
        {
            if (GameZoneIDStr.Trim().Length<=0)
            {
                return "";
            }
            int GameZoneID = Convert.ToInt32(GameZoneIDStr);

            object objType = GetCache("WebServiceUrl" + GameZoneID);
              string WebServiceUrl = string.Empty;
            if (objType == null)
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 F_Value from T_GameConfig ");
                    strSql.Append(" where F_ID=@F_ID and F_IsUsed=1");
                    SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int)};
                    parameters[0].Value = GameZoneID;

                    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["F_Value"] != null)
                    {

                        WebServiceUrl = ConfigurationManager.AppSettings[ds.Tables[0].Rows[0]["F_Value"].ToString()];

                    }
                    SetCache("WebServiceUrl" + GameZoneID, WebServiceUrl);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            else
            {
                WebServiceUrl = (string)objType;
            }
            return WebServiceUrl;
        }

        public static void AddNotify(string title,string note)//添加即时消息
        {
            WSS.Model.Notfiy nf = new WSS.Model.Notfiy();
            nf.F_Title = title;
            nf.F_Note = note;
            nf.F_DateTime = DateTime.Now;
            new WSS.BLL.Notfiy().Add(nf);

        }
        public static void AddSysLog(string userid,string username,string note)//添加日志
        {
            string LogSwitch = ConfigurationManager.AppSettings["LogSwitch"];
            if (LogSwitch == "true")
            {
                string sql = "INSERT INTO WebServeDB.dbo.T_SysLog (F_UserID,F_UserName,F_Note,F_URL,F_DateTime)  VALUES ("+userid+",'"+username+"','"+note+"','',getdate())";                
                DbHelperSQL.Exists(sql);
            }

        }

        public static bool CheckUserPower(string roleid,string menuid)//检查用户权限
        {
            string sql = "select  top 1 F_RoleID,F_RoleName,F_IsUsed,F_Power  FROM [T_Roles] where F_RoleID="+roleid+" and ','+F_Power+',' like '%,"+menuid+",%'";
            bool isok=DbHelperSQL.Exists(sql);
            return isok;
        }
    }
}
