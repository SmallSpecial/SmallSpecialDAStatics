using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.Linq;
using Coolite.Ext.Web;
using WSS.DBUtility;
using WSS.Model;
using WebServiceZoneConfig.OtherUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
namespace WebServiceZoneConfig
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceZ : System.Web.Services.WebService
    {
        string BigZoneID = ConfigurationManager.AppSettings["BigZoneID"];//对应的大区编号
        string GSSServerIP = ConfigurationManager.AppSettings["GSSServerIP"];//对应的大区编号
        string ConnStrGameCoreDB = ConfigurationManager.ConnectionStrings["GameCoreDBConnectionString"].ConnectionString;//游戏数据库连接字条串

        log4net.ILog Log = log4net.LogManager.GetLogger("GSSLog");

        string msgNoSafe = Resources.Global.MSGNoSafe;
        string msgWSVError = Resources.Global.MSGWSVError;
        /// <summary>
        /// 验证该IP不允许访问返回true
        /// </summary>
        /// <param name="visitip"></param>
        /// <returns></returns>
        bool VerirySecurityIP(string visitip)
        {
            bool limit = ConfigurationManager.AppSettings["LimitRemoteServer"] == "true";
            if (!limit)
            {
                return false;
            }
            string[] ips = GSSServerIP.Split(',');
            if (ips.Contains(visitip))
            {
                return false;
            }
            return true;
        }
        [WebMethod]
        public string HelloWorld()
        {
            return msgNoSafe;
            if (VerirySecurityIP(GetIP4Address()))
            {
                return msgNoSafe;
            }
            Log.Warn("执行了测试方法HelloWorld,请确认为合法访问,请求IP:" + GetIP4Address() + "");
            return "Hello World";
        }


        [WebMethod]
        public string GetBattleZones(int start, int limit, string sort, string dir)//得到战区配置列表
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {

                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT F_ZoneID, F_ZoneName, F_ZoneState, F_ZoneLine, F_ZoneAttrib, F_ChargeType, F_BigZoneID,F_ServerType  ,F_FaVersions_Cur,F_ReVersions_Cur,F_FaVersionsLow_And,F_ReVersionsLow_And ,F_FaVersionsLow_Ios ,F_ReVersionsLow_Ios ");
                strSql.Append(" FROM T_BattleZone WITH(NOLOCK) ");                                                                           
                                                                                                                                           
                DbHelperSQLP sp = new DbHelperSQLP();                                                                                      
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());

                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                    json.AddItem("F_ZoneName", dr["F_ZoneName"].ToString().Trim());
                    //  json.AddItem("F_ZoneState", dr["F_ZoneState"].ToString());

                    int state = Convert.ToInt16(dr["F_ZoneState"]);
                    json.AddItem("F_ZoneState0", GetSubStr(state, 4, 3, 1));
                    json.AddItem("F_ZoneState1", GetSubStr(state, 4, 2, 1));
                    json.AddItem("F_ZoneState2", GetSubStr(state, 4, 0, 2));
                    json.AddItem("F_ZoneState", state.ToString());


                    json.AddItem("F_ZoneLine", dr["F_ZoneLine"].ToString());
                    //  json.AddItem("F_ZoneAttrib", dr["F_ZoneAttrib"].ToString());

                    int attrib = Convert.ToInt16(dr["F_ZoneAttrib"]);
                    json.AddItem("F_ZoneAttrib0", GetSubStr(attrib, 5, 4, 1));
                    json.AddItem("F_ZoneAttrib1", GetSubStr(attrib, 5, 3, 1));
                    json.AddItem("F_ZoneAttrib2", GetSubStr(attrib, 5, 1, 2));
                    json.AddItem("F_ZoneAttrib4", GetSubStr(attrib, 5, 0, 1));

                    json.AddItem("F_ChargeType", dr["F_ChargeType"].ToString());
                    
                    json.AddItem("F_BigZoneID", dr["F_BigZoneID"].ToString());

                    json.AddItem("F_ServerType", dr["F_ServerType"].ToString());
                    json.AddItem("F_FaVersions_Cur", dr["F_FaVersions_Cur"].ToString());
                    json.AddItem("F_ReVersions_Cur", dr["F_ReVersions_Cur"].ToString());
                    json.AddItem("F_FaVersionsLow_And", dr["F_FaVersionsLow_And"].ToString());
                    json.AddItem("F_ReVersionsLow_And", dr["F_ReVersionsLow_And"].ToString());
                    json.AddItem("F_FaVersionsLow_Ios", dr["F_FaVersionsLow_Ios"].ToString());
                    json.AddItem("F_ReVersionsLow_Ios", dr["F_ReVersionsLow_Ios"].ToString());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;
                return json.ToString();

            }
            catch (System.Exception ex)
            {
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                Log.Warn("GetBattleZones" + json.error, ex);
                return json.ToString();
            }
        }

        /// <summary>
        /// 得到二进制的字符串截取
        /// </summary>
        private string GetSubStr(int value,int total,int indexbegin,int length)
        {
            string valueStr = Convert.ToString(value, 2).PadLeft(total, '0');

            return valueStr.Substring(indexbegin, length);
        }
        private short Get10From2(int value)
        {
            try
            {
                return Convert.ToInt16(value.ToString(), 2);
            }
            catch
            {
                return 0;
            }
        }


        [WebMethod]
        public Coolite.Ext.Web.Response SaveBattleZones(string data)//保存战区信息
        {
              //首先进行数据处理：传递过来的数据情形可能如下：
                /* 
                 {
	                "Created":[{
		                "F_ZoneName":"内网联测","F_ZoneState2":"",
		                "F_ZoneState1":"","F_ZoneState0":"",
		                "F_ZoneState":"128","F_ZoneLine":"1",
		                "F_ZoneAttrib4":"1","F_ZoneAttrib2":"01",
		                "F_ZoneAttrib1":"1","F_ZoneAttrib0":"1",
		                "F_ChargeType":"0","F_CurVersion":"1",
		                "F_BigZoneID":"0"
		                }]
	                }*/
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

            try
            {
                data = data.Replace("\",\"F_ZoneState1\":\"", "").Replace("\",\"F_ZoneState0\":\"", "");
                data = data.Replace("\"F_ZoneAttrib4\":", "\"F_ZoneAttrib\":").Replace("\",\"F_ZoneAttrib2\":\"", "").Replace("\",\"F_ZoneAttrib1\":\"", "").Replace("\",\"F_ZoneAttrib0\":\"", "");
                //处理后的数据【在将json格式数据转换为对象时失败 string>short】
                /*
                 {
                      \"Created\":
                       [{
                            \"F_ZoneName\":\"Json\",\"F_ZoneState2\":\"\",
                            \"F_ZoneState\":\"128\",\"F_ZoneLine\":\"1\",
                            \"F_ZoneAttrib\":\"10011\",\"F_ChargeType\":\"0\",
                            \"F_CurVersion\":\"2\",\"F_BigZoneID\":\"0\"
                        }]
                 }
                */
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                RequestData request= JsonConvert.DeserializeObject<RequestData>(data);
                ChangeRecords<T_BattleZone> dataList = new ChangeRecords<T_BattleZone>();
                dataList.Created =request.Created==null?null: request.Created.Select(s =>
                    s.MapObject<BattleZoneRequest, T_BattleZone>()
                 ).ToList();
                dataList.Updated = request.Updated == null ? null : request.Updated.Select(s => s.MapObject<BattleZoneRequest, T_BattleZone>()).ToList();
                dataList.Deleted = request.Deleted == null ? null : request.Deleted.Select(s => s.MapObject<BattleZoneRequest, T_BattleZone>()).ToList();
                //StoreDataHandler dataHandler = new StoreDataHandler(data);
                
                //ChangeRecords<T_BattleZone> dataList = dataHandler.ObjectData<T_BattleZone>();

                foreach (T_BattleZone battlezone in dataList.Deleted)
                {
                    battlezone.F_ZoneState = battlezone.F_ZoneState;
                    battlezone.F_ZoneAttrib = Get10From2((int)battlezone.F_ZoneAttrib);
                    db.T_BattleZone.Attach(battlezone);
                    db.T_BattleZone.DeleteOnSubmit(battlezone);
                }

                foreach (T_BattleZone battlezone in dataList.Updated)
                {
                    battlezone.F_ZoneState = battlezone.F_ZoneState;
                    battlezone.F_ZoneAttrib = Get10From2((int)battlezone.F_ZoneAttrib);
                    db.T_BattleZone.Attach(battlezone);
                    db.Refresh(RefreshMode.KeepCurrentValues, battlezone);
                }

                foreach (T_BattleZone battlezone in dataList.Created)
                {
                    battlezone.F_ZoneState = battlezone.F_ZoneState;
                    battlezone.F_ZoneAttrib = Get10From2((int)battlezone.F_ZoneAttrib);
                    db.T_BattleZone.InsertOnSubmit(battlezone);
                }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Msg = ex.Message;
                Log.Warn("SaveBattleZones", ex);
            }

            return sr;
        }

        [WebMethod]
        public string GetBattleLines()//得到单条线列表
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {
                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_NGSID, F_Name, F_ZoneID, F_MaxUser, F_Ip, F_Port,F_Main_State, F_Sub_State, F_DBISID, F_PingPort");
                strSql.Append(" FROM T_BattleLine WITH(NOLOCK) ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_NGSID", dr["F_NGSID"].ToString());
                    json.AddItem("F_Name", dr["F_Name"].ToString().Trim());
                    json.AddItem("F_ZoneID", dr["F_ZoneID"].ToString());
                    json.AddItem("F_MaxUser", dr["F_MaxUser"].ToString());
                    json.AddItem("F_Ip", dr["F_Ip"].ToString().Trim());
                    json.AddItem("F_Port", dr["F_Port"].ToString());
                    json.AddItem("F_Main_State", dr["F_Main_State"].ToString());
                    json.AddItem("F_Sub_State", dr["F_Sub_State"].ToString());
                    json.AddItem("F_DBISID", dr["F_DBISID"].ToString());
                    json.AddItem("F_PingPort", dr["F_PingPort"].ToString());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                return json.ToString();
            }
            catch (System.Exception ex)
            {
                Log.Warn("GetBattleLines", ex);
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                return json.ToString();
            }
        }

        [WebMethod]
        public Coolite.Ext.Web.Response SaveBattleLines(string data)//保存单条线
        {
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

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
                Log.Warn("SaveBattleLines", e);
            }

            return sr;
        }

        [WebMethod]
        public string GetGameServers()//得到GS配置列表
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {
                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_GSID, F_IP, F_GSNAME, F_NGSID, F_ZONEID, F_CampID, F_GSSceneID ");
                strSql.Append(" FROM T_GameServer WITH(NOLOCK) ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
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
                return json.ToString();
            }
            catch (System.Exception ex)
            {
                Log.Warn("GetGameServers", ex);
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                return json.ToString();
            }
        }

        [WebMethod]
        public Coolite.Ext.Web.Response SaveGameServers(string data)//保存GS信息
        {
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

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
                Log.Warn("SaveGameServers", e);
            }

            return sr;
        }


        [WebMethod]
        public string GetGameConfig()//得到GameConfig
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {
                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT F_Key, F_Describe, F_Value ");
                strSql.Append(" FROM T_GameConfig WITH(NOLOCK) WHERE F_Key in ('F_GameBaseVersion','F_PatcherVersion','F_UpDateRootPath','F_UpDateSimplePackRootPath') ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_Key", dr["F_Key"].ToString());
                    json.AddItem("F_Describe", dr["F_Describe"].ToString().Trim());
                    json.AddItem("F_Value", dr["F_Value"].ToString().Trim());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;
                return json.ToString();
            }
            catch (System.Exception ex)
            {
                Log.Warn("GetGameConfig", ex);
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                return json.ToString();
            }
        }
        [WebMethod]
        public Coolite.Ext.Web.Response SaveGameConfig(string data)//保存GameConfig
        {
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_GameConfig> dataList = dataHandler.ObjectData<T_GameConfig>();

                //foreach (T_GameConfig GameConfig in dataList.Deleted)
                //{
                //    db.T_GameConfig.Attach(GameConfig);
                //    db.T_GameConfig.DeleteOnSubmit(GameConfig);
                //}

                foreach (T_GameConfig GameConfig in dataList.Updated)
                {
                    db.T_GameConfig.Attach(GameConfig);
                    db.Refresh(RefreshMode.KeepCurrentValues, GameConfig);
                }

                //foreach (T_GameConfig GameConfig in dataList.Created)
                //{
                //    db.T_GameConfig.InsertOnSubmit(GameConfig);
                //}

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
                Log.Warn("SaveGameConfig", e);
            }

            return sr;
        }
        [WebMethod]
        public string GetGameVersionList()//得到GameVersionList
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {
                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT  F_ID, F_CURVERSION, F_LOWVERSION, F_UPFILESIZE, F_DOWNFILESIZE, F_FILENAME, F_TIME ");
                strSql.Append(" FROM T_GameVersionList WITH(NOLOCK) ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_ID", dr["F_ID"].ToString());
                    json.AddItem("F_CURVERSION", dr["F_CURVERSION"].ToString().Trim());
                    json.AddItem("F_LOWVERSION", dr["F_LOWVERSION"].ToString().Trim());
                    json.AddItem("F_UPFILESIZE", dr["F_UPFILESIZE"].ToString().Trim());
                    json.AddItem("F_DOWNFILESIZE", dr["F_DOWNFILESIZE"].ToString().Trim());
                    json.AddItem("F_FILENAME", dr["F_FILENAME"].ToString().Trim());
                    json.AddItem("F_TIME", dr["F_TIME"].ToString().Trim());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;
                return json.ToString();
            }
            catch (System.Exception ex)
            {
                Log.Warn("GetGameVersionList", ex);
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                return json.ToString();
            }
        }
        [WebMethod]
        public Coolite.Ext.Web.Response SaveGameVersionList(string data)//保存GameVersionList
        {
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_GameVersionList> dataList = dataHandler.ObjectData<T_GameVersionList>();

                foreach (T_GameVersionList GameVersionList in dataList.Deleted)
                {
                    db.T_GameVersionList.Attach(GameVersionList);
                    db.T_GameVersionList.DeleteOnSubmit(GameVersionList);
                }

                foreach (T_GameVersionList GameVersionList in dataList.Updated)
                {
                    db.T_GameVersionList.Attach(GameVersionList);
                    db.Refresh(RefreshMode.KeepCurrentValues, GameVersionList);
                }

                foreach (T_GameVersionList GameVersionList in dataList.Created)
                {
                    db.T_GameVersionList.InsertOnSubmit(GameVersionList);
                }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Msg = ex.Message;
                Log.Warn("SaveGameVersionList", ex);
            }

            return sr;
        }

        [WebMethod]
        public string GetGameSimpleVersionList()//得到GameSimpleVersionList
        {
            JSONHelper json = new JSONHelper();
            if (VerirySecurityIP(GetIP4Address()))
            {
                json.success = false;
                json.error = msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "";
                Log.Warn(json.error);
                return json.ToString();
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT   F_ID, F_CURVERSION, F_LOWVERSION, F_UPFILESIZE, F_DOWNFILESIZE, F_FILENAME, F_TIME ");
                strSql.Append(" FROM T_GameSimpleVersionList WITH(NOLOCK) ");

                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStrGameCoreDB;
                DataSet ds = sp.Query(strSql.ToString());
                json.success = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    json.AddItem("F_ID", dr["F_ID"].ToString());
                    json.AddItem("F_CURVERSION", dr["F_CURVERSION"].ToString().Trim());
                    json.AddItem("F_LOWVERSION", dr["F_LOWVERSION"].ToString().Trim());
                    json.AddItem("F_UPFILESIZE", dr["F_UPFILESIZE"].ToString().Trim());
                    json.AddItem("F_DOWNFILESIZE", dr["F_DOWNFILESIZE"].ToString().Trim());
                    json.AddItem("F_FILENAME", dr["F_FILENAME"].ToString().Trim());
                    json.AddItem("F_TIME", dr["F_TIME"].ToString().Trim());
                    json.ItemOk();
                }
                json.totlalCount = ds.Tables[0].Rows.Count;

                return json.ToString();
            }
            catch (System.Exception ex)
            {
                Log.Warn("GetGameSimpleVersionList", ex);
                json.success = false;
                json.error = msgWSVError + ex.Message + "";
                return json.ToString();
            }
        }
        [WebMethod]
        public Coolite.Ext.Web.Response SaveGameSimpleVersionList(string data)//保存GameSimpleVersionList
        {
            Response sr = new Response(true);
            if (VerirySecurityIP(GetIP4Address()))
            {
                sr.Success = false;
                sr.Msg = msgNoSafe;
                Log.Warn(msgNoSafe + Context.Request.UserHostAddress + "<>" + GSSServerIP + "");
                return sr;
            }

            try
            {
                GameCoreDBDataContext db = new GameCoreDBDataContext();
                StoreDataHandler dataHandler = new StoreDataHandler(data);
                ChangeRecords<T_GameSimpleVersionList> dataList = dataHandler.ObjectData<T_GameSimpleVersionList>();

                foreach (T_GameSimpleVersionList GameSimpleVersionList in dataList.Deleted)
                {
                    db.T_GameSimpleVersionList.Attach(GameSimpleVersionList);
                    db.T_GameSimpleVersionList.DeleteOnSubmit(GameSimpleVersionList);
                }

                foreach (T_GameSimpleVersionList GameSimpleVersionList in dataList.Updated)
                {
                    db.T_GameSimpleVersionList.Attach(GameSimpleVersionList);
                    db.Refresh(RefreshMode.KeepCurrentValues, GameSimpleVersionList);
                }

                foreach (T_GameSimpleVersionList GameSimpleVersionList in dataList.Created)
                {
                    db.T_GameSimpleVersionList.InsertOnSubmit(GameSimpleVersionList);
                }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Msg = ex.Message;
                Log.Warn("SaveGameSimpleVersionList", ex);
            }

            return sr;
        }
        /// <summary>
        /// 得到IPV4
        /// </summary>
        /// <returns></returns>
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

    }
    class RequestData 
    {
        public List<BattleZoneRequest> Created { get; set; }
        public List<BattleZoneRequest> Deleted { get; set; }
        public List<BattleZoneRequest> Updated { get; set; }
    }
    public class BattleZoneRequest 
    {
        public string F_ZoneID { get; set; }

        public string F_ZoneName { get; set; }

        public string F_ZoneState { get; set; }

        public string F_ZoneLine { get; set; }

        public string F_ZoneAttrib { get; set; }

        public string F_ChargeType { get; set; }

        public string F_CurVersion { get; set; }

        public string F_BigZoneID { get; set; }
        #region 新增字段
        public string F_ServerType { get; set; }
        public string F_FaVersions_Cur { get; set; }
        public string F_ReVersions_Cur { get; set; }
        public string F_FaVersionsLow_And { get; set; }
        public string F_ReVersionsLow_And { get; set; }
        public string F_FaVersionsLow_Ios { get; set; }
        public string F_ReVersionsLow_Ios { get; set; }
        #endregion
    }
    public static class DataConvert 
    {
        public static R MapObject<T, R>(this T source)
            where T : class
            where R : class 
        {
            R result = System.Activator.CreateInstance<R>();
            List<string> commonProperty = new List<string>();
            Type st = source.GetType();
            PropertyInfo[] sp = st.GetProperties();
            Type rt=result.GetType();
            PropertyInfo[] rp = rt.GetProperties();
            foreach (PropertyInfo item in sp)
            {
                 PropertyInfo pi=rp.Where(p=>p.Name==item.Name).FirstOrDefault();
                if (pi==null) 
                {
                    continue;
                }
                object obj = item.GetValue(source, null);
                //如果是字符串，需要对空串进行过滤
                if (item.PropertyType.Name == typeof(string).Name&&(string.IsNullOrEmpty(obj as string))) 
                {
                    continue;
                }
                if (obj == null) { continue; }
                //如果类型不一致需要强制类型转换【如果类型不一致且可空】
                if (pi.PropertyType.Name == typeof(Nullable<>).Name)
                {
                    NullableConverter nullableConverter = new NullableConverter(pi.PropertyType);//如何获取可空类型属性非空时的数据类型
                    Type nt = nullableConverter.UnderlyingType;
                    pi.SetValue(result, Convert.ChangeType(obj,nt), null);
                    continue;

                }
                pi.SetValue(result, Convert.ChangeType(obj, pi.PropertyType), null);
            }
            return result;
        }
    }
}
