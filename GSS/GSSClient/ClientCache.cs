using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using GSSCSFrameWork;
using System.Threading;
using GSSModel.Response;
using GSS.DBUtility;
namespace GSSClient
{
    class ClientCache
    {
       static Mutex mtx = new Mutex();  
       
        /// <summary>
        /// 设置数据库在客户端的缓存
        /// </summary>
        /// <param name="cache"></param>
        public static bool SetCache(byte[] cache)
        {
            try
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\GSSData\\GSSCache");
                mtx.WaitOne();
                StreamWriter CacheInf = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\GSSData\\GSSCache\\DBCache.dat");

                if (cache == null)
                {
                    CacheInf.Close();
                    return false;
                }
                //从数据中能否提取出缓存数据表

                string Str = Convert.ToBase64String(cache);
                CacheInf.Write(Str);
                CacheInf.Close();
                mtx.ReleaseMutex();
                return (true);
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error(ex);
                return false;
            }
           
        }

        /// <summary>
        /// 从缓存文件中得到DATASET
        /// </summary>
        public static DataSet GetCacheDS()
        {
            string cachePath = System.Windows.Forms.Application.StartupPath;
            ("cache dir" + cachePath).Logger();
            string fileName = cachePath+ "\\GSSData\\GSSCache\\DBCache.dat";
            FileInfo file = new FileInfo(fileName);
            //if (!file.Exists)
            //{
            //    return null;
            //}
            try
            {
                mtx.WaitOne();
                StreamReader CacheInfo = new StreamReader(fileName);
                string cache = CacheInfo.ReadToEnd();
                byte[] cachebytes = Convert.FromBase64String(cache);
                DataSet ds = DataSerialize.GetDatasetFromByte(cachebytes);
                CacheInfo.Close();
                mtx.ReleaseMutex();
                return ds;
            }
            catch (System.Exception ex)
            {
                "ClientCache Exception ".ErrorLogger();
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 工单列表:设置工单列表在客户端的缓存
        /// </summary>
        /// <param name="cache"></param>
        public static bool SetTaskCache(byte[] cache, string cachename)
        {
            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\GSSData\\GSSCache");

            try
            {
                mtx.WaitOne();
                StreamWriter CacheInf = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\GSSData\\GSSCache\\TaskCache" + cachename + ".dat");

                if (cache == null)
                {
                    return false;
                }
                string Str = Convert.ToBase64String(cache);
                CacheInf.Write(Str);
                CacheInf.Close();
                mtx.ReleaseMutex();
                return (true);
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error(ex);
                return false;
            }

        }
        /// <summary>
        /// 工单列表:从缓存文件中得到DATASET
        /// </summary>
        public static DataSet GetTaskCache(string cachename)
        {
            string fileName = System.Windows.Forms.Application.StartupPath + "\\GSSData\\GSSCache\\TaskCache" + cachename + ".dat";
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                return null;
            }
            try
            {
                mtx.WaitOne();
                StreamReader CacheInfo = new StreamReader(fileName);
                string cache = CacheInfo.ReadToEnd();
                byte[] cachebytes = Convert.FromBase64String(cache);
                DataSet ds = DataSerialize.GetDatasetFromByte(cachebytes);
                CacheInfo.Close();
                mtx.ReleaseMutex();
                return ds;
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 得到用户表用户名
        /// </summary>
        public static string GetUserNameT(string id)
        {
            if (id.Trim().Length==0)
            {
                return "";
            }
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Users"].Select("F_UserID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    string username = drdic[0]["F_UserName"].ToString().Trim();
                    string realname = drdic[0]["F_RealName"].ToString().Trim();
                    string rolename = GetRoleName(drdic[0]["F_RoleID"].ToString().Trim());
                    string deptname = GetDeptName(drdic[0]["F_DepartID"].ToString().Trim());
                    if (realname.Length > 0)
                    {
                        username += "[" + realname + "]";
                    }
                    if (deptname.Length > 0)
                    {
                        username += "[" + deptname + "]";
                    }
                    if (rolename.Length > 0)
                    {
                        username += "[" + rolename + "]";
                    }
                    return username;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到用户表用户名+实际名称
        /// </summary>
        public static string GetUserNameR(string id)
        {
            if (id.Trim().Length == 0)
            {
                return "";
            }
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Users"].Select("F_UserID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    string username = drdic[0]["F_UserName"].ToString().Trim();
                    string realname = drdic[0]["F_RealName"].ToString().Trim();
                    if (realname.Length > 0)
                    {
                        username += "[" + realname + "]";
                    }
                    return username;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到部门名称
        /// </summary>
        public static string GetDeptName(string id)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Department"].Select("F_DepartID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_DepartName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到角色名字
        /// </summary>
        public static string GetRoleName(string id)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Roles"].Select("F_RoleID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_RoleName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到菜单控件名
        /// </summary>
        public static string GetMenuFormName(string id)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Menus"].Select("F_MenuID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_FormName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到帐号的权限标识
        /// </summary>
        public static string GetUserPower(string Uid)
        {
            try
            {
                DataSet ds = GetCacheDS();
                string pStr = "";
                if (ds != null)
                {
                    DataRow[] drdic = ds.Tables["T_Users"].Select("F_UserID=" + Uid + "");
                    if (drdic.Length > 0)
                    {
                        drdic = ds.Tables["T_Roles"].Select("F_RoleID=" + drdic[0]["F_RoleID"].ToString() + "");
                        if (drdic.Length > 0)
                        {
                            if (drdic[0]["F_Power"].ToString().Length>0)
                            {
                                string power= drdic[0]["F_Power"].ToString().Trim();
                                string[] p = power.Split(',');
                               
                                for (int i = 0; i < p.Length; i++)
                                {
                                    if (p[i].Length>0)
                                    {
                                        pStr +="," +GetMenuFormName(p[i]);
                                    }
                                    
                                }
                                pStr += ",";
                            }
                        }
                    }
                }
               
                return pStr;

            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error(ex);
                return "";
            }
        }

        /// <summary>
        /// 得到字典表名字
        /// </summary>
        public static string GetDicName(string dicid)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select("F_DicID=" + dicid + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_Value"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到字典表编号
        /// </summary>
        public static string GetDicID(string name, string parentid)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                string whereStr = "F_Value='" + name + "'";
                if (parentid.Length > 0)
                {
                    whereStr += " and F_ParentID=" + parentid + "";
                }
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select(whereStr);
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_DicID"].ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 得到字典表编号
        /// </summary>
        public static string GetDicID(string name)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                string whereStr = "F_Value='" + name + "'";
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select(whereStr);
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_DicID"].ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 得到字典表父编号
        /// </summary>
        public static string GetDicParentID(string dicid)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select("F_DicID=" + dicid + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_ParentID"].ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 得到字典表父+子名字
        /// </summary>
        public static string GetDicPCName(string dicid)
        {
            string parent = GetDicName(GetDicParentID(dicid));
            string node = GetDicName(dicid);
            System.Resources.ResourceManager rm=LanguageResource.Language.ResourceManager;
            string pd=rm.GetString(parent);
            pd=string.IsNullOrEmpty(pd)?parent:pd;
            string nd=rm.GetString(node);
            nd=string.IsNullOrEmpty(nd)?node:nd;
            return pd + "_" + nd;
        }

        /// <summary>
        /// 得到游戏大区名字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetZoneName(string value)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=" + SystemConfig.BigZoneParentId + " and F_ValueGame='" + value + "'");
                if (drdic != null && drdic.Length > 0)
                {

                    return drdic[0]["F_Name"].ToString();
                }

            }
            return "";
        }

        /// <summary>
        /// 得到游戏配置表编号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetGameConfigID(string value)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID="+SystemConfig.BigZoneParentId+" and F_Name='" + value + "'");
                if (drdic != null && drdic.Length > 0)
                {

                    return drdic[0]["F_ID"].ToString();
                }

            }
            return "";
        }
        /// <summary>
        /// 得到游戏配置表F_ValueGame
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetGameConfigByF_ID(string value)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ID=" + value + "");
                if (drdic != null && drdic.Length > 0)
                {

                    return drdic[0]["F_ValueGame"].ToString();
                }

            }
            return "";
        }

        /// <summary>
        /// 得到游戏大区配置表编号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetBigZoneGameID(string value)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID="+SystemConfig.BigZoneParentId+" and F_Name='" + value + "'");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_ValueGame"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到游戏大区配置表战区编号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetZoneGameID(string bigzoneid,string zonename)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=" + bigzoneid + " and F_Name='" + zonename + "'");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_ValueGame"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 得到游戏大区配置表编号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetGameConfigValue(string id)
        {
            DataSet ds = GetCacheDS();
            if (ds != null)
            {
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ID=" + id + "");
                if (drdic != null && drdic.Length > 0)
                {
                    return drdic[0]["F_Value"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 工单:得到工单实体
        /// </summary>
        public static GSSModel.Tasks GetTaskModel(int taskid)
        {
            GSSModel.Tasks model = new GSSModel.Tasks();
            DataSet ds = GetTaskCache("TaskList");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow[] drs = ds.Tables[1].Select("F_ID=" + taskid + "");
                if (drs.Length > 0)
                {
                    if (drs[0]["F_ID"].ToString() != "")
                    {
                        model.F_ID = int.Parse(drs[0]["F_ID"].ToString());
                    }
                    if (drs[0]["F_Title"] != null)
                    {
                        model.F_Title = drs[0]["F_Title"].ToString();
                    }
                    if (drs[0]["F_Note"] != null)
                    {
                        model.F_Note = drs[0]["F_Note"].ToString();
                    }
                    if (drs[0]["F_From"].ToString() != "")
                    {
                        model.F_From = int.Parse(drs[0]["F_From"].ToString());
                    }
                    if (drs[0]["F_VipLevel"].ToString() != "")
                    {
                        model.F_VipLevel = int.Parse(drs[0]["F_VipLevel"].ToString());
                    }
                    if (drs[0]["F_LimitType"].ToString() != "")
                    {
                        model.F_LimitType = int.Parse(drs[0]["F_LimitType"].ToString());
                    }
                    if (drs[0]["F_LimitTime"].ToString() != "")
                    {
                        model.F_LimitTime = DateTime.Parse(drs[0]["F_LimitTime"].ToString());
                    }
                    if (drs[0]["F_Type"].ToString() != "")
                    {
                        model.F_Type = int.Parse(drs[0]["F_Type"].ToString());
                    }
                    if (drs[0]["F_State"].ToString() != "")
                    {
                        model.F_State = int.Parse(drs[0]["F_State"].ToString());
                    }
                    if (drs[0]["F_GameName"].ToString() != "")
                    {
                        model.F_GameName = int.Parse(drs[0]["F_GameName"].ToString());
                    }
                    if (drs[0]["F_GameBigZone"] != null)
                    {
                        model.F_GameBigZone = drs[0]["F_GameBigZone"].ToString();
                    }
                    if (drs[0]["F_GameZone"] != null)
                    {
                        model.F_GameZone = drs[0]["F_GameZone"].ToString();
                    }
                    if (drs[0]["F_GUserID"] != null)
                    {
                        model.F_GUserID = drs[0]["F_GUserID"].ToString();
                    }
                    if (drs[0]["F_GUserName"] != null)
                    {
                        model.F_GUserName = drs[0]["F_GUserName"].ToString();
                    }
                    if (drs[0]["F_GRoleID"] != null)
                    {
                        model.F_GRoleID = drs[0]["F_GRoleID"].ToString();
                    }
                    if (drs[0]["F_GRoleName"] != null)
                    {
                        model.F_GRoleName = drs[0]["F_GRoleName"].ToString();
                    }
                    if (drs[0]["F_Telphone"] != null)
                    {
                        model.F_Telphone = drs[0]["F_Telphone"].ToString();
                    }
                    if (drs[0]["F_GPeopleName"] != null)
                    {
                        model.F_GPeopleName = drs[0]["F_GPeopleName"].ToString();
                    }
                    if (drs[0]["F_DutyMan"].ToString() != "")
                    {
                        model.F_DutyMan = int.Parse(drs[0]["F_DutyMan"].ToString());
                    }
                    if (drs[0]["F_PreDutyMan"].ToString() != "")
                    {
                        model.F_PreDutyMan = int.Parse(drs[0]["F_PreDutyMan"].ToString());
                    }
                    if (drs[0]["F_CreatMan"].ToString() != "")
                    {
                        model.F_CreatMan = int.Parse(drs[0]["F_CreatMan"].ToString());
                    }
                    if (drs[0]["F_CreatTime"].ToString() != "")
                    {
                        model.F_CreatTime = DateTime.Parse(drs[0]["F_CreatTime"].ToString());
                    }
                    if (drs[0]["F_EditMan"].ToString() != "")
                    {
                        model.F_EditMan = int.Parse(drs[0]["F_EditMan"].ToString());
                    }
                    if (drs[0]["F_EditTime"].ToString() != "")
                    {
                        model.F_EditTime = DateTime.Parse(drs[0]["F_EditTime"].ToString());
                    }
                    if (drs[0]["F_URInfo"] != null)
                    {
                        model.F_URInfo = drs[0]["F_URInfo"].ToString();
                    }
                    if (drs[0]["F_Rowtype"].ToString() != "")
                    {
                        model.F_Rowtype = int.Parse(drs[0]["F_Rowtype"].ToString());
                    }
                    if (drs[0]["F_CUserName"].ToString() != "")
                    {
                        if ((drs[0]["F_CUserName"].ToString() == "1") || (drs[0]["F_CUserName"].ToString().ToLower() == "true"))
                        {
                            model.F_CUserName = true;
                        }
                        else
                        {
                            model.F_CUserName = false;
                        }
                    }
                    if (drs[0]["F_CPSWProtect"].ToString() != "")
                    {
                        if ((drs[0]["F_CPSWProtect"].ToString() == "1") || (drs[0]["F_CPSWProtect"].ToString().ToLower() == "true"))
                        {
                            model.F_CPSWProtect = true;
                        }
                        else
                        {
                            model.F_CPSWProtect = false;
                        }
                    }
                    if (drs[0]["F_CPersonID"].ToString() != "")
                    {
                        if ((drs[0]["F_CPersonID"].ToString() == "1") || (drs[0]["F_CPersonID"].ToString().ToLower() == "true"))
                        {
                            model.F_CPersonID = true;
                        }
                        else
                        {
                            model.F_CPersonID = false;
                        }
                    }
                    if (drs[0]["F_COther"] != null)
                    {
                        model.F_COther = drs[0]["F_COther"].ToString();
                    }
                    if (drs[0]["F_OLastLoginTime"] != null)
                    {
                        model.F_OLastLoginTime = drs[0]["F_OLastLoginTime"].ToString();
                    }
                    if (drs[0]["F_OCanRestor"].ToString() != "")
                    {
                        if ((drs[0]["F_OCanRestor"].ToString() == "1") || (drs[0]["F_OCanRestor"].ToString().ToLower() == "true"))
                        {
                            model.F_OCanRestor = true;
                        }
                        else
                        {
                            model.F_OCanRestor = false;
                        }
                    }
                    if (drs[0]["F_OAlwaysPlace"] != null)
                    {
                        model.F_OAlwaysPlace = drs[0]["F_OAlwaysPlace"].ToString();
                    }
                    if (drs[0]["F_TToolUsed"].ToString() != "")
                    {
                        if ((drs[0]["F_TToolUsed"].ToString() == "1") || (drs[0]["F_TToolUsed"].ToString().ToLower() == "true"))
                        {
                            model.F_TToolUsed = true;
                        }
                        else
                        {
                            model.F_TToolUsed = false;
                        }
                    }
                    if (drs[0]["F_TUseData"] != null)
                    {
                        model.F_TUseData = drs[0]["F_TUseData"].ToString();
                    }

                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public static List<GameConfig> GetGameConfigCache() 
        {
            DataSet ds = GetCacheDS();
            if (ds == null) 
            {
                return new List<GameConfig>();
            }
            DataTable table= ds.Tables["T_GameConfig"];
            if (table == null) 
            {
                return new List<GameConfig>();
            }
            List<GameConfig> config= (new GameConfig()).TableConvertColumnAtrributeObject(table);
            config = config.Where(c => c.IsUsed == 1).ToList();
            return config;
        }
        public static List<GameConfig> GetGameZoneCache() 
        {
            DataSet ds = GetCacheDS();
            if (ds == null)
            {
                return new List<GameConfig>();
            }
            DataTable table = ds.Tables["T_GameConfig"];
            if (table == null)
            {
                return new List<GameConfig>();
            }
            List<GameConfig> config = (new GameConfig()).TableConvertColumnAtrributeObject(table);
            GameConfig[] big = config.Where(c => c.IsUsed == 1 && (c.ParentId == SystemConfig.BigZoneParentId)).ToArray();
            int[] bid=big.Select(b=>b.Id).ToArray();
            GameConfig[] zone = config.Where(z => bid.Contains(z.ParentId)).ToArray();
            List<GameConfig> bz = new List<GameConfig>();
            bz.AddRange(big);
            bz.AddRange(zone);
            return bz;
        }
        public static IEnumerable<GameConfig> GetGameConfigByName(string name) 
        {
            DataSet ds = GetCacheDS();
            if (ds == null)
            {
                return new List<GameConfig>();
            }
            DataTable table = ds.Tables["T_GameConfig"];
            if (table == null)
            {
                return new List<GameConfig>();
            }
            List<GameConfig> configs = (new GameConfig()).TableConvertColumnAtrributeObject(table);
            IEnumerable<GameConfig> selects= configs.Where(c => c.IsUsed == 1 && c.Name == name);
            return selects;
        }
        public static Dictionary<int, string> GetWorkOrderTypes() 
        {
            Dictionary<int, string> types = new Dictionary<int, string>();
            DataTable table = GetTargetTableFromCache("T_Dictionary");
            if (table == null) 
            {
                return types;
            }
            foreach (DataRow item in table.Rows)
            {
                int id = (int)item["F_dicid"];
                string name = (string)item["F_value"];
                types.Add(id, name);
            }
            return types;
        }
        static DataTable GetTargetTableFromCache(string tableName) 
        {
            DataTable table = null;
            DataSet ds = GetCacheDS();
            if (ds == null)
            {
                return table;
            }
            table = ds.Tables[tableName];
            if (table == null)
            {
                return table;
            }
            return table;
        }
    }
}
