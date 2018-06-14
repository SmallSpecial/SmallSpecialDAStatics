using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using WSS.DBUtility;
using System.Data;

namespace WebWSS.ModelPage.GameOverView
{
    /// <summary>
    /// Ajax 的摘要说明
    /// </summary>
    public class Ajax : IHttpHandler
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        //DigGameDB
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        //UserCoreDB
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();
        //GameCoreDB
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();

        DataSet ds;
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            string action = context.Request.QueryString["action"];
            string sTime = context.Request.QueryString["STime"];
            string eTime = context.Request.QueryString["ETime"];
            switch (action)
            {
                case "ShowChartRegister":
                    GetUserCoreDBString();
                    ShowChartRegister(sTime, eTime);
                    break;
                case "ShowChartCreate":
                    GetGameCoreDBString();
                    ShowChartCreate(sTime, eTime);
                    break;
                case "ShowChartRechargePeopleOfNum":
                    GetUserCoreDBString();
                    ShowChartRechargePeopleOfNum(sTime, eTime);
                    break;
                case "ShowChartRechargeAmount":
                    GetUserCoreDBString();
                    ShowChartRechargeAmount(sTime, eTime);
                    break;
            }
        }
        #region 图表数据获取
        private void ShowChartRegister(string sTime, string eTime)
        {
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (!string.IsNullOrEmpty(sTime))
            {
                searchdateB = Convert.ToDateTime(sTime);
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                searchdateE = Convert.ToDateTime(eTime);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreatTime,23) 时间,COUNT(F_UserID) 玩家数 FROM T_User WHERE CONVERT(VARCHAR(100),F_CreatTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreatTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_CreatTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));

            ds = DBHelperUserCoreDB.Query(sql);
            int sp = searchdateE.Subtract(searchdateB).Days;
            List<int> list = new List<int>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (searchdateB.AddDays(i).ToString("yyyy-MM-dd") == ds.Tables[0].Rows[j]["时间"].ToString())
                        {
                            flag = 1;
                            list.Add(Convert.ToInt32(ds.Tables[0].Rows[j]["玩家数"]));
                        }
                    }
                    if (flag == 0)
                        list.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    list.Add(0);
                }
            }

            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            Series series1 = new Series();
            series1.name = "玩家数";
            series1.type = "line";
            series1.data = list;//new List<double>() { 26061649.1, 26161649.41, 21782199.14, 27749708.51, 8819500.47, 27711342.26, 0.00, 0.00 };

            seriesList.Add(series1);
            var newObj = new
            {
                series = seriesList
            };

            string strJson = ToJson(newObj);

            WriteJson(strJson);
        }
        private void ShowChartCreate(string sTime,string eTime)
        {
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (!string.IsNullOrEmpty(sTime))
            {
                searchdateB = Convert.ToDateTime(sTime);
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                searchdateE = Convert.ToDateTime(eTime);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreateTime,23) 时间,COUNT(DISTINCT F_UserID) 玩家数 FROM (SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}')TEMP GROUP BY CONVERT(VARCHAR(100),F_CreateTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));

            ds = DBHelperGameCoreDB.Query(sql);
            int sp = searchdateE.Subtract(searchdateB).Days;
            List<int> list=new List<int>();
            if(ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i <= sp + 1;i++ )
                {
                    int flag = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (searchdateB.AddDays(i).ToString("yyyy-MM-dd") == ds.Tables[0].Rows[j]["时间"].ToString())
                        {
                            flag = 1;
                            list.Add(Convert.ToInt32(ds.Tables[0].Rows[j]["玩家数"]));
                        }
                    }
                    if(flag==0)
                        list.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    list.Add(0);
                }
            }

            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            Series series1 = new Series();
            series1.name = "玩家数";
            series1.type = "line";
            series1.data = list;//new List<double>() { 26061649.1, 26161649.41, 21782199.14, 27749708.51, 8819500.47, 27711342.26, 0.00, 0.00 };

            seriesList.Add(series1);
            var newObj = new
            {
                series = seriesList
            };

            string strJson = ToJson(newObj);

            WriteJson(strJson);
        }
        private void ShowChartRechargePeopleOfNum(string sTime, string eTime)
        {
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (!string.IsNullOrEmpty(sTime))
            {
                searchdateB = Convert.ToDateTime(sTime);
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                searchdateE = Convert.ToDateTime(eTime);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) 时间,COUNT(DISTINCT F_UserID) 人数 FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));

            ds = DBHelperUserCoreDB.Query(sql);
            int sp = searchdateE.Subtract(searchdateB).Days;
            List<int> list = new List<int>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (searchdateB.AddDays(i).ToString("yyyy-MM-dd") == ds.Tables[0].Rows[j]["时间"].ToString())
                        {
                            flag = 1;
                            list.Add(Convert.ToInt32(ds.Tables[0].Rows[j]["人数"]));
                        }
                    }
                    if (flag == 0)
                        list.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    list.Add(0);
                }
            }

            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            Series series1 = new Series();
            series1.name = "充值人数";
            series1.type = "line";
            series1.data = list;//new List<double>() { 26061649.1, 26161649.41, 21782199.14, 27749708.51, 8819500.47, 27711342.26, 0.00, 0.00 };

            seriesList.Add(series1);
            var newObj = new
            {
                series = seriesList
            };

            string strJson = ToJson(newObj);

            WriteJson(strJson);
        }
        private void ShowChartRechargeAmount(string sTime, string eTime)
        {
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (!string.IsNullOrEmpty(sTime))
            {
                searchdateB = Convert.ToDateTime(sTime);
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                searchdateE = Convert.ToDateTime(eTime);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) 时间,SUM(F_Price) 金额 FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));

            ds = DBHelperUserCoreDB.Query(sql);
            int sp = searchdateE.Subtract(searchdateB).Days;
            List<int> list = new List<int>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (searchdateB.AddDays(i).ToString("yyyy-MM-dd") == ds.Tables[0].Rows[j]["时间"].ToString())
                        {
                            flag = 1;
                            list.Add(Convert.ToInt32(ds.Tables[0].Rows[j]["金额"]));
                        }
                    }
                    if (flag == 0)
                        list.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= sp + 1; i++)
                {
                    list.Add(0);
                }
            }

            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            Series series1 = new Series();
            series1.name = "充值金额";
            series1.type = "line";
            series1.data = list;//new List<double>() { 26061649.1, 26161649.41, 21782199.14, 27749708.51, 8819500.47, 27711342.26, 0.00, 0.00 };

            seriesList.Add(series1);
            var newObj = new
            {
                series = seriesList
            };

            string strJson = ToJson(newObj);

            WriteJson(strJson);
        }
        #endregion

        #region JSON方法
        public static string ToJson(object obj)
        {
            return NewtonsoftJson(obj);
        }

        public static string NewtonsoftJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
        }

        private static void WriteJson(string str)
        {
            HttpContext.Current.Response.Write(str);
            //HttpContext.Current.Response.ContentType = "text/plain"; //设置MIME格式
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 获取JSON字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string GetJSON(string key, string val)
        {
            return string.Format("{{\"{0}\":\"{1}\"}}", key, val);
        }
        #endregion

        #region Series实体类
        /// <summary>
        /// 定义一个Series类 设置其每一组sereis的一些基本属性
        /// </summary>
        public class Series
        {
            /// <summary>
            /// sereis序列组id
            /// </summary>
            //public int id
            //{
            //    get;
            //    set;
            //}
            /// <summary>
            /// series序列组名称
            /// </summary>
            public string name
            {
                get;
                set;
            }
            /// <summary>
            /// series序列组呈现图表类型(line、column、bar等)
            /// </summary>
            public string type
            {
                get;
                set;
            }
            /// <summary>
            /// series序列组的数据为数据类型数组
            /// </summary>
            public List<int> data
            {
                get;
                set;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取GameCoreDB连接字符串
        /// </summary>
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 获取UsercoreDB连接字符串
        /// </summary>
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        #endregion
    }
}