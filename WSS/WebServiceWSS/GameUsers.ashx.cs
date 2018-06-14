using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using Coolite.Ext.Web;
using WSS.DBUtility;


namespace WebServiceWSS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GameUsers : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            var start = 0;
            var limit = 10;
            var sort = string.Empty;
            var dir = string.Empty;
            var query = string.Empty;

            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }

            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }

            if (!string.IsNullOrEmpty(context.Request["sort"]))
            {
                sort = context.Request["sort"];
            }

            if (!string.IsNullOrEmpty(context.Request["dir"]))
            {
                dir = context.Request["dir"];
            }

            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                query = context.Request["query"];
            }
            if (query.Trim() != "")
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_User ");
          
                strSql.Append(" where F_UserName<>'" + query + "'");
            
            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = "server=.;database=UserCoreDB;uid=sa;pwd=123";
            DataSet ds = sp.Query(strSql.ToString());
            string jsons = "";
            JSONHelper json = new JSONHelper();
            json.success = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                json.AddItem("F_UserID", dr["F_UserID"].ToString());

                json.ItemOk();
            }
            json.totlalCount = ds.Tables[0].Rows.Count;
            jsons = json.ToString();
            context.Response.Write(jsons);}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
