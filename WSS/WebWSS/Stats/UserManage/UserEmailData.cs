using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WSS.DBUtility;
using WebWSS.Model;
using System.Data.Linq.Mapping;
namespace WebWSS.Stats
{
    public class BaseRequestParam 
    {
        public int Begin { get; set; }
        public int End { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByDesc { get; set; }
        public int Total { get; set; }
    }
    public class RequestParam : BaseRequestParam
    {
        public int RoleID { get; set; }
        public int BigZoneId { get; set; }
        public int ZoneId { get; set; }
        public int? Date { get; set; }//年月日 如：20170818
    }
    public class UserEmailData
    {
        [ColumnAttribute(Name = "RoleId")]
        public int RoleId { get; set; }
        [ColumnAttribute(Name = "title")]
        public string Title { get; set; }
        [ColumnAttribute(Name = "text")]
        public string Text { get; set; }
        [ColumnAttribute(Name = "state")]
        public int IsRead { get; set; }
        [ColumnAttribute(Name = "prop")]
        public bool HavaProp { get; set; }
        [ColumnAttribute(Name = "SendTime")]
        public string SendTime { get; set; }
    }
    public class UserEmailRequestParam : RequestParam
    {
        public int? RoleId { get; set; }
       
    }
    public class UserEmailDataManage 
    {
        public string connString { get; private set; }
        public UserEmailDataManage(string connString)
        {
            this.connString = connString;
        }
        public JsonData QueryData(UserEmailRequestParam request)
        {
            JsonData json = new JsonData();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@bigzone", SqlDbType.Int) { Value = request.BigZoneId });
                param.Add(new SqlParameter("@zoneid", SqlDbType.Int) { Value = request.ZoneId });
                param.Add(new SqlParameter("@start", SqlDbType.Int) { Value = request.Begin });
                param.Add(new SqlParameter("@end", SqlDbType.Int) { Value = request.End });
                param.Add(new SqlParameter("@total", SqlDbType.Int) { Direction = ParameterDirection.Output });
                if (request.RoleId.HasValue)
                {
                    param.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = request.RoleId.Value });
                }
                DbHelperSQLP db = new DbHelperSQLP(connString);
                int total = 0;
                DataSet ds = db.RunProcedure("SP_UserEmail", param.ToArray(), typeof(UserEmailData).Name);
                SqlParameter output = param.Where(p => p.ParameterName == "@total").FirstOrDefault();
                if (output != null && (int)output.Value == -1)
                {
                    json.Count = 0;
                    json.Message = App_GlobalResources.Language.Tip_LoseLinkServerName;
                    return json;
                }
                List<UserEmailData> emails = (new UserEmailData()).GatherEntityDataWithMapColumn(ds);
                if (output != null)
                {
                    json.Count = (int)output.Value;
                    json.Data = emails;
                }
            }
            catch (Exception ex) 
            {
                json.Message = ex.Message;
            }
            return json;
        }
    }
}