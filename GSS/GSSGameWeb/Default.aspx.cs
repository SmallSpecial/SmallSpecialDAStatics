using System;

namespace GSSGameWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        public double maxuploadlength = 1.5;
        public int overtime = 18000;
        public string paramrStr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Request.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            

            try
            {
                string p = GSSRequest.GetString("r");
                if (string.IsNullOrEmpty(p)) { return; }
                string[] paramrs = GSSRequest.GetString("r").Split('|');
                string p1 = Utils.MD5(paramrs[1]).Substring(27);
                if (p1 != paramrs[0])
                {
                    Response.Clear();
                    Response.End();
                    return;
                }
                maxuploadlength = Utils.GetMaxUpContentLength();
                overtime = Convert.ToInt32(Utils.GetMaxUpOverTime()*1000);


                paramrStr = Request["r"] == null ? "" : Request["r"].ToString();
                paramrStr += ";ClientIP:" + GSSRequest.GetIP4Address();

            }
            catch(Exception ex)
            {
                Response.Clear();
                Response.End();
                return;
            }
        }

    }
}
