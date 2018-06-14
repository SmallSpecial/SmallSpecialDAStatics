using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using Coolite.Ext.Web;
using WSS.DBUtility;
using System.Data.SqlClient;
using System.Configuration;


namespace WSS.Web.Admin.GameTool
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

            string jsons = "{totalCount:0,success:true,data:[]}";
            if (query.Trim() != "" && query.Trim()!="1<>1")
            {


                //寻龙记
                if (GameCode == "100001")
                {

                    string WebSUrl = WSS.BLL.AllOther.GetWebServiceUrl(GameZoneID);
                    if (WebSUrl.Length > 10)
                    {
                        WebServiceXLJ.WebServiceGame xlj = new WebServiceXLJ.WebServiceGame();
                        xlj.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        xlj.Url = WebSUrl;
                        jsons = xlj.GetGameUsers(query);
                    }
                }


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
