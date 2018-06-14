using System;
using System.Data;

namespace GSSGameWeb
{
    public partial class Ajax : System.Web.UI.Page
    {
        string result = "0";
        public Ajax()
        {
            if (Utils.StrIsNullOrEmpty(GSSRequest.GetUrlReferrer()))
                return;
            else if (GSSRequest.IsCrossSitePost(GSSRequest.GetUrlReferrer(), GSSRequest.GetHost())) //如果是跨站提交...
                return;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string type = GSSRequest.GetString("t");
            switch (type)
            {
                case "addtask":
                    AddTask();    //提交工单
                    break;
                case "addanswer":
                    AddAnswer();    //问卷调查
                    break;
                case "msgadd":
                    MSGAdd();    //提交工单
                    break;
                case "msglist":
                    MSGList();    //列取工单
                    break;
            }
        }

        private bool SessionAllow(string name, int time)
        {
            if (Session[name] != null)
            {
                TimeSpan ts = DateTime.Now -
                Convert.ToDateTime(Session[name]);
                if (ts.TotalSeconds > time)
                {
                    Session[name] = DateTime.Now; return true;
                }
                else
                    return false;
            }
            else
            {
                Session[name] = DateTime.Now; return true;
            }
        }

        WebServiceGSS.WebServiceGSS ws = new WebServiceGSS.WebServiceGSS();


        private void AddTask()
        {
            string vercode = GSSRequest.GetString("vercode");

            if (Session["vercode"] == null || vercode != Session["vercode"].ToString())
            {
                result = "验证码不正确或失效,请重新输入!";

            }
            else
            {

                if (!SessionAllow("addtask", 30))
                {
                    result = "30秒内只允许提交一次,请稍候再试!";
                }
                else
                {
                    try
                    {
                        string type = GSSRequest.GetString("type");
                        string note = GSSRequest.GetString("note");
                        string qq = GSSRequest.GetString("qq");
                        string mobile = GSSRequest.GetString("mobile");
                        string gamedata = GSSRequest.GetString("r");
                        string upfile = GSSRequest.GetString("f");

                        ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        result = ws.GSSTaskAdd(type, note, qq, mobile, gamedata, upfile);
                    }
                    catch (System.Exception ex)
                    {
                        result = "LINK GSSSERVER ERROR";
                    }
                }
            }

            Session["vercode"] = null;

            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            ResponseXML(xmlnode.AppendFormat("<result>{0}</result>", result));
        }

        private void AddAnswer()
        {
            if (!SessionAllow("addanswer", 30))
            {
                result = "30秒内只允许提交一次,请稍候再试!";
            }
            else
            {
                try
                {
                    string gdata = GSSRequest.GetString("r");
                    string answer = GSSRequest.GetString("as");

                    ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    result = ws.GSSTaskAddRequst(gdata, answer);
                }
                catch (System.Exception ex)
                {
                    result = "LINK GSSSERVER ERROR";
                }
            }

            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            ResponseXML(xmlnode.AppendFormat("<result>{0}</result>", result));
        }

        private void MSGAdd()
        {
            result = "0";

            if (!SessionAllow("addmsg", 3))
            {
                result = "消息发送太快!";
            }
            else
            {
                try
                {
                    string note = GSSRequest.GetString("note");
                    string gamedata = GSSRequest.GetString("r");

                    ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    result = ws.GSSMSGAdd(note, gamedata);
                }
                catch (System.Exception ex)
                {
                    result = "LINK GSSSERVER ERROR";
                }
            }

            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            ResponseXML(xmlnode.AppendFormat("<result>{0}</result>", result));
        }

        private void MSGList()
        {
            result = "0";
            if (!SessionAllow("listmsg", 1))
            {
                result = "0";//返回正常
            }
            else
            {

                try
                {
                    string roleid = GSSRequest.GetString("roleid");

                    ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    DataSet ds = ws.GSSMSGList(roleid);

                    if (ds != null)
                    {
                        result = "";
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string note = dr["F_Note"].ToString().Replace("\n","<br />");
                            string tuser = dr["F_EditMan"].ToString().PadLeft(4, '0');
                            string time = dr["F_EditTime"].ToString();

                            if (note.IndexOf("【游戏数据】") == -1)
                            {
                                string info = "";
                                if (tuser == "0000")
                                {
                                    tuser = dr["F_GRoleName"].ToString();
                                    info = string.Format("<font color=#0078c9>{0}</font> <font color=gray>{1}</font><br/>{2}<br/>", tuser, time, note);
                                }
                                else
                                {
                                    tuser = "客服" + tuser + " ";
                                     info = string.Format("<font color=#e81e75>{0}</font> <font color=gray>{1}</font><br/>{2}<br/>", tuser, time, note);
                                }
                                
                                result += Server.HtmlEncode(info);
                            }
                        }
                        if (result == "")
                        {
                            result = "0";
                        }
                    }
                    else
                    {
                        result = "0";//返回正常
                    }
                }
                catch (System.Exception ex)
                {
                    result = "0";
                }

                System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
                ResponseXML(xmlnode.AppendFormat("<result>{0}</result>", result));
            }
        }

        #region Helper
        /// <summary>
        /// 向页面输出xml内容
        /// </summary>
        /// <param name="xmlnode">xml内容</param>
        private void ResponseXML(System.Text.StringBuilder xmlnode)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出json内容
        /// </summary>
        /// <param name="json"></param>
        private void ResponseJSON(string json)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/json";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(json);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //System.Web.HttpContext.Current.Response.End();
        }

        private void ResponseJSON<T>(T jsonobj)
        {
            // ResponseJSON(JavaScriptConvert.SerializeObject(jsonobj));
        }
        #endregion
    }

}
