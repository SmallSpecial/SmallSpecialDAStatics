using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;


namespace WebWSS
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbHelperMySQL.connectionString = "server=192.168.7.100;database=gsdata_db;uid=root;pwd=";
            string user = DbHelperMySQL.GetSingle("select szname from playerbaseinfo where nglobalid=2284 and dfPrestige=100").ToString();

           lblUser.Text = TranI2G(user)+user;
        }

        //字符集转换   
        public string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
    }
}
