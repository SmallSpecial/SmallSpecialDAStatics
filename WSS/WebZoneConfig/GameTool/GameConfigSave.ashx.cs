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
    public class GameConfigSave : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            var GameCode = string.Empty;
            var GameZoneID = string.Empty;

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

            Response sr = new Response(true);


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
                        string jsonsrequest = context.Request.Form[0].ToString();
                        xlj.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        xlj.SaveGameConfig(jsonsrequest);
                    }
                }
            }
            catch (System.Exception e)
            {
                sr.Success = false;
                sr.Msg = e.Message;
            }


            sr.Write();

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
