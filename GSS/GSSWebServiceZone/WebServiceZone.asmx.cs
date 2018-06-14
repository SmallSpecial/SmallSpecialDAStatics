using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Services;
using GSS.DBUtility;
using GSSCSFrameWork;

namespace GSSWebServiceZone
{
    /// <summary>
    /// WebServiceZone 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServiceZone : System.Web.Services.WebService
    {
        string GSSServerIP = ConfigurationManager.AppSettings["GSSServerIP"];//使用此WEBSERVICE的GSS系统服务端的IP地址
        string ConnStrSynGameGSLogDB = ConfigurationManager.AppSettings["ConnStrSynGameGSLogDB"];//SQL SERVER角色日志同步库(离线查询用)
        string ConnStrGsLogDB = ConfigurationManager.AppSettings["ConnStrGsLogDB"];//MYSQL的战区角色日志库(实时查询用)

        log4net.ILog Log = log4net.LogManager.GetLogger("GSSLog");

        [WebMethod]
        public string HelloWorld()
        {
            if (GetIP4Address() != GSSServerIP)
            {
                return "非法请求，将拒绝对此请求提供服务";
            }
            Log.Warn("执行了测试方法HelloWorld,请确认为合法访问,请求IP:" + GetIP4Address() + "");
            return "Hello World";
        }


        [WebMethod(Description = "SQLSERVER战区日志同步库查询(离线方式)")]
        public byte[] QuerySynGSLog(string querysql)
        {
            if (GetIP4Address() != GSSServerIP)
            {
                string erro = "非法请求，将拒绝对此请求提供服务,请求IP:" + GetIP4Address() + "";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            if (ConnStrSynGameGSLogDB.Trim() == string.Empty)
            {
                string erro = "非法请求，将拒绝对此离线请求提供服务";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            if (!CheckQuerySql(querysql))
            {
                string erro = "非法请求，将拒绝对此离线请求提供服务(SQL)";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            try
            {
                DbHelperSQLP sp = new DbHelperSQLP(ConnStrSynGameGSLogDB);
                DataSet ds = sp.Query(querysql);
                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                string erro = "SSQLSERVER战区日志同步库查询(离线方式)查询操作错误["+ex.Message+"]";
                Log.Error(erro, ex);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
        }

        [WebMethod(Description = "MYSQL战区日志实时库查询(实时方式)")]
        public byte[] QueryLiveGSLog(string querysql)
        {
            if (GetIP4Address() != GSSServerIP)
            {
                string erro = "非法请求，将拒绝对此请求提供服务,请求IP:" + GetIP4Address() + " ";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            if (ConnStrGsLogDB.Trim() == string.Empty)
            {
                string erro = "非法请求，将拒绝对此实时请求提供服务";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            if (!CheckQuerySql(querysql))
            {
                string erro = "非法请求，将拒绝对此实时请求提供服务(SQL)";
                Log.Warn(erro);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
            try
            {
                DbHelperMySQLP sp = new DbHelperMySQLP(ConnStrGsLogDB);
                DataSet ds = sp.Query(querysql);
                return DataSerialize.GetDataSetSurrogateZipBYtes(ds);
            }
            catch (System.Exception ex)
            {
                string erro = "MYSQL战区日志实时库查询(实时方式)查询操作错误[" + ex.Message + "]";
                Log.Error(erro, ex);
                return DataSerialize.GetDataSetSurrogateZipBYtes(GetErrorDS(erro));
            }
        }

        /// <summary>
        /// 判断请求SQL语句,保证数据安全
        /// </summary>
        private bool CheckQuerySql(string querysql)
        {
            string sql = querysql.ToLower();
            string[] CheckItems = new string[] { "insert", "update", "delete", "into", "create", "alter", "waitfor", "open", "truncate", "drop ", "exec", "holdlock" };//请输入小写特定字符串

            foreach (string CheckItem in CheckItems)
            {
                if (sql.IndexOf(CheckItem) >= 0)
                {
                    return false;
                }
            }
            string[] CheckHItems = new string[] { "drop_log", "fight_log", "gm_lg", "gold_log", "item_log", "money_log", "other_log", "task_log", "trade_log"};//请输入小写特定字符串
            bool HaveMust = false;
            foreach (string CheckHItem in CheckHItems)
            {
                if (sql.IndexOf(CheckHItem) >= 0)
                {
                    HaveMust=true;
                    break;
                }
            }
            return HaveMust;
        }

        /// <summary>
        /// 返回错误信息构成的DataSet
        /// </summary>
        private DataSet GetErrorDS(string erro)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("信息提示");
            ds.Tables[0].Rows.Add();
            ds.Tables[0].Rows[0][0] = erro;
            return ds;
        }

        /// <summary>
        /// 得到IPV4
        /// </summary>
        public string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }
            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
    }
}
