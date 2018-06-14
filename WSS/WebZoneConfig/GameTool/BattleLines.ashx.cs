using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using Coolite.Ext.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebZoneConfig.GameTool
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BattleLines : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            var start = 0;
            var limit = 15;
            var sort = string.Empty;
            var dir = string.Empty;
            var query = string.Empty;
            var GameCode = string.Empty;
            var GameZoneID = string.Empty;

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

            if (!string.IsNullOrEmpty(context.Request["GameCode"]))
            {
                GameCode = context.Request["GameCode"];//游戏编号
            }
            if (!string.IsNullOrEmpty(context.Request["GameZoneID"]))
            {
                GameZoneID = context.Request["GameZoneID"];//游戏编号
            }

            GameCode = "100001";
            GameZoneID = "100001001";

            string jsons = "{totalCount:0,success:true,data:[]}";

            try
            {

                //寻龙记
                if (GameCode == "100001")
                {

                    string WebSUrl = System.Configuration.ConfigurationManager.AppSettings["WebServiceZoneConfigURL"];
                    if (WebSUrl.Length > 10)
                    {
                        WebServiceZoneConfig.ServiceZ xlj = new WebServiceZoneConfig.ServiceZ();
                        xlj.Url = WebSUrl;
                        xlj.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        jsons = xlj.GetBattleLines();
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

            context.Response.Write(jsons);
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
