using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WSS.DBUtility;
using WebWSS.Model;
namespace WebWSS.Stats
{
    public class UserRelatedDataStatis
    {//用户相关数据统计
        private string Connstring { get; set; }
        DbHelperSQLP db;
        public UserRelatedDataStatis(string connectionDB)
        {
            Connstring = connectionDB;
            db = new DbHelperSQLP(Connstring);
        }
        public JsonData TheDayActiveMount(RequestParam param, int userId = 0)
        {
            JsonData json = new JsonData();
            try
            {
                if (!param.Date.HasValue)
                {
                    string date = DateTime.Now.ToString("yyyyMMdd");
                    param.Date = Convert.ToInt32(date);
                }
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@bigZoneId", SqlDbType.Int) { Value = param.BigZoneId });
                ps.Add(new SqlParameter("@zoneId", SqlDbType.Int) { Value = param.ZoneId });
                if (userId > 0)
                {
                    ps.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = userId });
                }
                ps.Add(new SqlParameter("@dayInt", SqlDbType.Int) { Value = param.Date.Value });
               
                DataSet ds = db.RunProcedure("SP_UserActiveMount", ps.ToArray(), typeof(UserRelatedDataStatis).Name);
                List<OtherLog> logs = (new OtherLog()).GatherEntityDataWithMapColumn(ds); // string opbak = string.IsNullOrEmpty(l.opbak) ? l.opbak : (l.opbak.LastIndexOf("_") == l.opbak.Length - 1 ? l.opbak = l.opbak.Substring(0, l.opbak.Length - 2) : l.opbak);
                ds.Dispose();
                List<UserMountResponse> response = CalculateUserActiveMount(logs);
                json.Count = response.Count;
                json.Data = response;
                json.Result = true;
            }
            catch (Exception ex)
            {
                json.Message = ex.ToString();
            }
            return json;
        }
        List<UserMountResponse> CalculateUserActiveMount(List<OtherLog> logs) 
        {
            List<UserMountResponse> mount = new List<UserMountResponse>();
            int[] uids = logs.Select(l => l.userid).Distinct().ToArray();
            //calculate user active mount's information
            foreach (OtherLog item in logs)
            {
                string mountId = item.opbak.Substring(0, item.opbak.IndexOf("_"));
                UserMountResponse m = new UserMountResponse()
                {
                    UserId = item.userid
                };
                m.ActiveMountIds.Add(mountId);
                if (mount.Count == 0)
                {

                    mount.Add(m);
                    continue;
                }
                bool otherUser = true;
                for (int i = 0; i < mount.Count; i++)
                {
                    if (mount[i].UserId == item.userid)
                    {
                        mount[i].ActiveMountIds.Add(mountId);
                        otherUser = false;
                        continue;
                    }
                }
                if (!otherUser)
                {
                    continue;
                }
                mount.Add(m);
            }
            return mount;
        }
        /// <summary>
        /// 坐骑进阶
        /// Param1:坐骑ID
        /// Param2:进阶结果(1:成功 0:失败) Param3:进阶之前的等级 Param4:进阶之后的等级
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonData UserMountLevelStatic(RequestParam param)
        {//用户坐骑等级变化统计
            JsonData json = new JsonData() { Result = true };
            try
            {
                if (!param.Date.HasValue)
                {
                    string date = DateTime.Now.ToString("yyyyMMdd");
                    param.Date = Convert.ToInt32(date);
                }
                List<SqlParameter> sqlparam = new List<SqlParameter>();
                sqlparam.Add(new SqlParameter("@bigZoneId", SqlDbType.Int) { Value = param.BigZoneId });
                sqlparam.Add(new SqlParameter("@zoneId", SqlDbType.Int) { Value = param.ZoneId });
                sqlparam.Add(new SqlParameter("@dayInt", SqlDbType.Int) { Value = param.Date.Value });
                DataSet ds = db.RunProcedure("SP_MountLevel", sqlparam.ToArray(), typeof(UserRelatedDataStatis).Name);
                List<OtherLog> logs = (new OtherLog()).GatherEntityDataWithMapColumn(ds);
                List<UserMountLevel> levels = new List<UserMountLevel>();
                foreach (OtherLog item in logs)
                {
                    string mountId = item.opbak.Split('_')[MountUpLevelBakParam.MountId.GetHashCode()];
                    int g = int.Parse(item.opbak.Split('_')[MountUpLevelBakParam.NowLevel.GetHashCode()]);
                    UserMountLevel level = levels.Where(le => le.mountId == mountId && le.roleid == item.roleid).FirstOrDefault();
                    if (level == null)
                    {
                        levels.Add(new UserMountLevel() { roleid = item.roleid, mountId = mountId ,level=g});
                    }
                    else
                    {
                        foreach (UserMountLevel ml in levels)
                        {
                            if (ml.roleid == level.roleid && ml.mountId == level.mountId)
                            {
                                ml.level = ml.level < g ? g : ml.level;
                                break;
                            }
                        }
                    }
                }
                //不统计到用户：每一坐骑每一个等级的数目
                List<UserMountLevelStatisc> sample = new List<UserMountLevelStatisc>();
                foreach (UserMountLevel item in levels)
                {
                    UserMountLevelStatisc sa = sample.Where(s => s.mountId ==item.mountId && s.level == item.level).FirstOrDefault();
                    if (sa == null)
                    {
                        sample.Add(new UserMountLevelStatisc() { mountId=item.mountId,level=item.level});
                        continue;
                    }
                }
                foreach (UserMountLevel item in levels)
                {
                     foreach (UserMountLevelStatisc s in sample)
                    {
                        if (item.mountId == s.mountId && item.level == s.level)
                        {
                            s.mountNumber++;
                            break;
                        }
                    }
                }
                json.Data = sample;
                json.Count = sample.Count;
                return json;
            }
            catch (Exception ex)
            {
                json.Result = false;
                json.Message = ex.ToString();
            }
            return json;
        }

        /// <summary>
        /// 添加以及删除的记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonData GetUserFriendLogs(RequestParam param) 
        {
            return GetSocialContactLog(param, "SP_UserFriendLog");
        }
        DataSet SampleQueryDataset(RequestParam param,string procedureName)
        {
            if (!param.Date.HasValue)
            {
                param.Date = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            }
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(new SqlParameter("@bigZoneId", DbType.Int32) { Value = param.BigZoneId });
            ps.Add(new SqlParameter("@zoneId", DbType.Int32) { Value = param.ZoneId });
            ps.Add(new SqlParameter("@dayint", DbType.Int32) { Value = param.Date.Value });
            List<FriendLog> fs = new List<FriendLog>();
            DataSet ds = db.RunProcedure(procedureName, ps.ToArray(), typeof(FriendLog).Name);
            return ds;
        }
        JsonData GetSocialContactLog(RequestParam param,string procedureName) 
        {
            JsonData json = new JsonData();
            List<FriendLog> fs;
            try
            {
                DataSet ds = SampleQueryDataset(param, procedureName);
                fs = (new FriendLog()).GatherEntityDataWithMapColumn(ds).OrderBy(s => s.Level).ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                json.Message = msg;
                return json;
            }
            List<FriendLog> add = new List<FriendLog>();
            List<FriendLog> del = new List<FriendLog>();
            foreach (FriendLog item in fs)
            {
                string[] p = item.OpBak.Split('_');
                item.ActionType = int.Parse(p[0]);
                if (item.ActionType == 1)
                {
                    add.Add(item);
                }
                else
                {
                    del.Add(item);
                }
            }
            json.Data = new object[] { add, del };
            json.Result = true;
            return json;
        }
        public JsonData GetEmemy(RequestParam param) 
        {
            return GetSocialContactLog(param, "SP_QueryEmemyLog");
        }
        enum MountUpLevelBakParam 
        {
            MountId=0,
            UpLevelStaute=1,
            OriginLevel=2,
            NowLevel=3
        }
    }
}