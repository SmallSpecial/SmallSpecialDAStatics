using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.Replenishment
{
    public partial class Replenishment : Admin_Page
    {
        #region 属性
        string koreaDepositVerifyURL = System.Configuration.ConfigurationManager.AppSettings["KoreaDepositVerifyURL"];
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            GetUserCoreDBString();
            if (!IsPostBack)
            {
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerach_Click(object sender, EventArgs e)
        {
            string userID = this.tbUserID.Text.Trim();
            if(string.IsNullOrEmpty(userID))
            {
                Response.Write("<Script Language=JavaScript>alert('"+ App_GlobalResources.Language.Tip_WriteUserID+"');</Script>");
                return;
            }
            string roleID = this.tbRoleID.Text.Trim();
            if(string.IsNullOrEmpty(roleID))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteRoleID + "');</Script>");
                return;
            }
            string transactionID = this.tbTransactionID.Text.Trim();
            if(string.IsNullOrEmpty(transactionID))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WritetransactionID + "');</Script>");
                return;
            }

            string url = string.Format(koreaDepositVerifyURL, transactionID);

            string res = GetInfo(url);
            SerializeHelper sHelper = new SerializeHelper();
            RootObject rb = sHelper.JsonDeserialize<RootObject>(res);

            if (rb == null)
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_NoResule;
                return;
            }
            if (rb.code != "200")
            {
                lblinfo.Text = string.Format("{0}  {1}",App_GlobalResources.Language.Tip_InfoError, res);
                return;
            }

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@GlobalID", SqlDbType.Int) { Value = roleID });
            param.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userID });
            param.Add(new SqlParameter("@TransactionID", SqlDbType.Char) { Value = transactionID });
            param.Add(new SqlParameter("@WebRequestResult", SqlDbType.Int) { Value = 1 });
            param.Add(new SqlParameter("@Code", SqlDbType.Int) { Value = rb.code });
            param.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = rb.result_data.product.id });
            param.Add(new SqlParameter("@StrReturn", SqlDbType.NVarChar) { Value = res });
            param.Add(new SqlParameter("@StoreName", SqlDbType.Char) { Value = rb.result_data.store });
            param.Add(new SqlParameter("@price", SqlDbType.Int) { Value = rb.result_data.product.price });
            param.Add(new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output });

            DBHelperUserCoreDB.RunProcedure("_DBIS_Deposit_Verify_Result", param.ToArray());
            int result = (int)param[9].Value;
            lblinfo.Text = GetString(result.ToString());
            string sql = string.Format("INSERT INTO [dbo].[T_ReplenishmentLog] ([F_GlobalID], [F_UserID], [F_TransactionID],[F_ReturnStr], [F_DepositResult], [F_InsertTime]) VALUES ({0}, {1}, '{2}', '{3}',{4}, GETDATE())", roleID, userID, transactionID,res, result);
            DBHelperDigGameDB.ExecuteSql(sql);
        }
        public string GetInfo(string url)
        {
            string strBuff = "";
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换   
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容   
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理   
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以   
            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）   
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            strBuff = respStreamReader.ReadToEnd();
            return strBuff;
        }
        #endregion

        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        protected string GetString(string val)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (val)
                {
                    case "1":
                        return "성공";
                    case "-1001":
                        return "돌아오는 결과 값이 없습니다.";
                    case "-1002":
                        return "해당 결제 번호는 성공처리 되지 않았습니다.";
                    case "-1003":
                        return "이미 결제에 성공한 결제 번호입니다.";
                    case "-1004":
                        return "해당 유저가 존재하지 않습니다.";
                    case "-1005":
                        return "T_Deposit_UserAccount 입력 실패";
                    default:
                        return val;
                }
            }
            else
            {
                switch (val)
                {
                    case "1":
                        return "成功";
                    case "-1001":
                        return "网页没返回结果";
                    case "-1002":
                        return "网页认为这个单号没成功";
                    case "-1003":
                        return "已经存在充值成功的订单了";
                    case "-1004":
                        return "没有此玩家";
                    case "-1005":
                        return "插入T_Deposit_UserAccount失败";
                    default:
                        return val;
                }
            }
        }
    }

    public class Product
    {
        public string id { get; set; }
        public string price { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
    }

    public class Result_data
    {
        public string transaction_id { get; set; }
        public string state { get; set; }
        public Product product { get; set; }
        public string store { get; set; }
        public string verify_at { get; set; }
        public string mode { get; set; }
        public string store_response { get; set; }
    }

    public class RootObject
    {
        public string code { get; set; }
        public string message { get; set; }
        public Result_data result_data { get; set; }
    }
}
