using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSS.DBUtility;
using System.Data;
using System.Data.SqlClient;
using LanguageItems;
namespace GSSServerLibrary
{
    public class DBHandle
    {
        /// <summary>
        /// 字典表数据集,做数据库缓存
        /// </summary>
        public static DataSet dsDictionary;
        /// <summary>
        /// 游戏配置表
        /// </summary>
        public static DataSet dsGameConfig;
        /// <summary>
        /// 用户数据集
        /// </summary>
        public static DataSet dsUsers;
        /// <summary>
        /// 用户数据集
        /// </summary>
        public static DataSet dsDepartments;
        /// <summary>
        /// 角色数据集
        /// </summary>
        public static DataSet dsRoles;


        public DBHandle()
        {
            if (dsDictionary == null)
            {
                string sql = @"SELECT * FROM T_Dictionary with(nolock)";
                dsDictionary = DbHelperSQL.Query(sql);
                sql = @"SELECT * FROM T_GameConfig with(nolock)";
                dsGameConfig = DbHelperSQL.Query(sql);
                sql = @"SELECT * FROM T_Users with(nolock)";
                dsUsers = DbHelperSQL.Query(sql);
                sql = @"SELECT * FROM T_Department with(nolock)";
                dsDepartments = DbHelperSQL.Query(sql);
                sql = @"SELECT * FROM T_Roles with(nolock)";
                dsRoles = DbHelperSQL.Query(sql);
            }
        }

        public DBHandle(string conn)
        {
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <param name="upsw">密码</param>
        /// <param name="uip">密码</param>
        /// <returns></returns>
        public string Login(string uid, string upsw, string uip)
        {
            string sql = @"SELECT F_UserID  FROM T_Users with(nolock) WHERE (F_PassWord = '" + upsw + "') AND (F_UserName = N'" + uid + "')";
            DataSet ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sql = "INSERT INTO T_SysLog (F_UserID, F_UserName, F_Note,F_Data, F_DateTime) VALUES (" + ds.Tables[0].Rows[0]["F_UserID"].ToString() + ", N'" + uid + "', N'登录GSS系统',N'" + uip + "',getdate())";
                DbHelperSQL.ExecuteSql(sql);
                return ds.Tables[0].Rows[0]["F_UserID"].ToString();
            }
            return "false";

        }

        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="username">用户名</param>
        /// <param name="note">信息</param>
        /// <param name="data">数据</param>
        public void SysLog(string userid, string note, string data)
        {
            try
            {
                string sql = @"declare @uname varchar(30)  select top 1 @uname=F_UserName from T_Users where F_UserID=" + userid + @" ;INSERT INTO T_SysLog (F_UserID, F_UserName, F_Note,F_Data, F_DateTime) VALUES (" + userid + @",@uname, N'" + note + "',N'" + data + "',getdate())";
                DbHelperSQL.ExecuteSql(sql);
            }
            catch (System.Exception ex)
            {
            }
        }

        /// <summary>
        /// 得到数据库在客户端的缓存
        /// </summary>
        /// <returns></returns>
        public DataSet GetCache()
        {
            //字典表
            string sql = @"SELECT F_Sort, F_Value, F_ParentID, F_DicID FROM T_Dictionary with(nolock) WHERE (F_IsUsed = 1) ORDER BY F_DicID";
            DataSet ds = DbHelperSQL.Query(sql);
            //游戏配置表
            sql = @"SELECT F_ID, F_ParentID, F_Name,F_Value,F_Value1,F_ValueGame, F_Sort, F_IsUsed FROM T_GameConfig with(nolock) WHERE (F_IsUsed = 1) ORDER BY F_ID";
            DataSet dsGC = DbHelperSQL.Query(sql);
            //部门表
            sql = @"SELECT F_DepartID, F_ParentID, F_DepartName, F_Note FROM T_Department with(nolock)";
            DataSet dsdept = DbHelperSQL.Query(sql);
            //用户表
            sql = @"SELECT F_UserID, F_UserName, F_DepartID, F_RoleID, F_RealName FROM T_Users with(nolock) where F_IsUsed=1";
            DataSet dsuser = DbHelperSQL.Query(sql);
            //角色表
            sql = @"SELECT F_Power, F_IsUsed, F_RoleName, F_RoleID FROM T_Roles with(nolock)";
            DataSet dsrole = DbHelperSQL.Query(sql);
            //菜单表
            sql = @"SELECT F_MenuID, F_Name, F_FormName, F_ParentID, F_IsUsed, F_Sort FROM T_Menus with(nolock)";
            DataSet dsmenu = DbHelperSQL.Query(sql);
            sql = "select * from   dbo.T_Version where F_id in ( select MAX(F_id)  from dbo.T_Version )";//获取最新版本数据【new 2017-08-25】
            DataSet version = DbHelperSQL.Query(sql);
            ds.Tables[0].TableName = "T_Dictionary";

            DataTable dtGC = dsGC.Tables[0].Copy();
            dtGC.TableName = "T_GameConfig";
            ds.Tables.Add(dtGC);

            DataTable dtdept = dsdept.Tables[0].Copy();
            dtdept.TableName = "T_Department";
            ds.Tables.Add(dtdept);

            DataTable dtuser = dsuser.Tables[0].Copy();
            dtuser.TableName = "T_Users";
            ds.Tables.Add(dtuser);

            DataTable dtrole = dsrole.Tables[0].Copy();
            dtrole.TableName = "T_Roles";
            ds.Tables.Add(dtrole);

            DataTable dtmenu = dsmenu.Tables[0].Copy();
            dtmenu.TableName = "T_Menus";
            ds.Tables.Add(dtmenu);
            DataTable tv = version.Tables[0].Copy();
            tv.TableName = "T_Version";
            ds.Tables.Add(tv);
            return ds;
        }

        /// <summary>
        /// 得到提醒数量用的DATASET
        /// </summary>
        /// <returns></returns>
        public DataSet GetAlertNum()
        {
            string sql = @"SELECT F_State,F_Type, F_DutyMan, F_PreDutyMan,count(1) as F_Num FROM T_Tasks with(nolock) where F_State<>100100107 and F_State<>100100108 group by F_State,F_Type, F_DutyMan, F_PreDutyMan";
            DataSet ds = DbHelperSQL.Query(sql);
            return ds;
        }

        public int GetNewTaskCount()
        {
            string sql = @"SELECT count(1) FROM T_Tasks with(nolock) where F_State=100100100 and F_CreatTime>dateadd(n,-5,getdate())";
            try
            {
                return Convert.ToInt32(DbHelperSQL.GetSingle(sql));

            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTask(string whereStr)
        {
            string sql = @"SELECT top 100 * FROM T_Tasks with(nolock) where 1=1 " + whereStr;

            DataSet ds = DbHelperSQL.Query(sql);


            DataTable dtold = ds.Tables[0].Copy();
            DataTable dtnew = ds.Tables[0].Clone();
            dtnew.TableName = "dtnew";

            dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
            int colCount = dtnew.Columns.Count;
            for (int i = 1; i < colCount; i++)
            {
                dtnew.Columns[i].DataType = System.Type.GetType("System.String");
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                DataRow drnew = dtnew.NewRow();

                for (int i = 0; i < colCount; i++)
                {
                    drnew[i] = dr[i].ToString();
                }
                //字典表
                string[] drdics = { "F_From", "F_VipLevel", "F_State" };
                foreach (string dic in drdics)
                {
                    drnew[dic] = GetDicValue(drnew[dic].ToString());
                }
                string[] drdicsp = { "F_Type" };
                foreach (string dic in drdicsp)
                {//此处需要将查询到的关键字转换为文本
                    drnew[dic] = GetDicPCValue(drnew[dic].ToString());
                }
                //游戏配置表
                string[] drgcs = { "F_GameName" };
                foreach (string gc in drgcs)
                {
                    drnew[gc] = GetGconfigName(drnew[gc].ToString());
                }
                //用户名
                string[] drusers = { "F_DutyMan", "F_PreDutyMan", "F_CreatMan", "F_EditMan" };
                foreach (string user in drusers)
                {
                    drnew[user] = GetUserName(drnew[user].ToString());
                }

                //时间
                string[] drdates = { "F_LimitTime", "F_CreatTime", "F_EditTime" };
                foreach (string date in drdates)
                {
                    try
                    {
                        drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
                    }
                    catch (System.Exception ex)
                    {
                        drnew[date] = "";
                    }

                }

                dtnew.Rows.Add(drnew);

            }
            ds.Tables.Clear();
            ds.Tables.Add(dtnew);
            ds.Tables.Add(dtold);

            return ds;
        }

        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTask(string whereStr, DbHelperSQLP sp)
        {
            string sql = @"SELECT top 100 * FROM T_Tasks with(nolock) where 1=1 " + whereStr;

            DataSet ds = sp.Query(sql);


            //DataTable dtold = ds.Tables[0].Copy();
            //DataTable dtnew = ds.Tables[0].Clone();
            //dtnew.TableName = "dtnew";

            //dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
            //int colCount = dtnew.Columns.Count;
            //for (int i = 1; i < colCount; i++)
            //{
            //    dtnew.Columns[i].DataType = System.Type.GetType("System.String");
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //    DataRow drnew = dtnew.NewRow();

            //    for (int i = 0; i < colCount; i++)
            //    {
            //        drnew[i] = dr[i].ToString();
            //    }
            //    //字典表
            //    string[] drdics = { "F_From", "F_VipLevel", "F_State" };
            //    foreach (string dic in drdics)
            //    {
            //        drnew[dic] = GetDicValue(drnew[dic].ToString());
            //    }
            //    string[] drdicsp = { "F_Type" };
            //    foreach (string dic in drdicsp)
            //    {
            //        drnew[dic] = GetDicPCValue(drnew[dic].ToString());
            //    }
            //    //游戏配置表
            //    string[] drgcs = { "F_GameName" };
            //    foreach (string gc in drgcs)
            //    {
            //        drnew[gc] = GetGconfigName(drnew[gc].ToString());
            //    }
            //    //用户名
            //    string[] drusers = { "F_DutyMan", "F_PreDutyMan", "F_CreatMan", "F_EditMan" };
            //    foreach (string user in drusers)
            //    {
            //        drnew[user] = GetUserName(drnew[user].ToString());
            //    }

            //    //时间
            //    string[] drdates = { "F_LimitTime", "F_CreatTime", "F_EditTime" };
            //    foreach (string date in drdates)
            //    {
            //        try
            //        {
            //            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
            //        }
            //        catch (System.Exception ex)
            //        {
            //            drnew[date] = "";
            //        }

            //    }

            //    dtnew.Rows.Add(drnew);

            //}
            //ds.Tables.Clear();
            //ds.Tables.Add(dtnew);
            //ds.Tables.Add(dtold);

            return ds;
        }

        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public int GetTaskCount(string whereStr)
        {
            if (whereStr.IndexOf("order") >= 0)
            {
                whereStr = whereStr.Substring(0, whereStr.ToLower().IndexOf("order"));
            }
            string sql = @"SELECT count(1) FROM T_Tasks with(nolock) where 1=1 " + whereStr;
            return Convert.ToInt16(DbHelperSQL.GetSingle(sql));

        }

        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTask(string whereStr, string fieldName, string orderStr, int PageSize, int PageIndex)
        {
            if (whereStr.IndexOf("order") >= 0)
            {
                whereStr = whereStr.Substring(0, whereStr.ToLower().IndexOf("order"));
            }
            //临时使用条件查询分页数据,后续在服务端使用存储过程分页
            int pe = PageSize * PageIndex;
            int pb = pe - PageSize + 1;

            string PwhereStr = " and F_ID in (select F_ID from ( SELECT top 10000000 ROW_NUMBER() OVER(" + orderStr + ") AS rownum,F_ID FROM T_Tasks with(nolock) where 1=1   " + whereStr + ") a where rownum between " + pb + " and " + pe + ") " + orderStr + "";

            string sql = @"SELECT top 100 * FROM T_Tasks with(nolock) where 1=1 " + PwhereStr;

            DataSet ds = DbHelperSQL.Query(sql);


            DataTable dtold = ds.Tables[0].Copy();
            DataTable dtnew = ds.Tables[0].Clone();
            dtnew.TableName = "dtnew";

            dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
            int colCount = dtnew.Columns.Count;
            for (int i = 1; i < colCount; i++)
            {
                dtnew.Columns[i].DataType = System.Type.GetType("System.String");
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                DataRow drnew = dtnew.NewRow();

                for (int i = 0; i < colCount; i++)
                {
                    object obj = dr[i];
                    if (obj != null)
                        drnew[i] = dr[i].ToString();
                    else drnew[i] = string.Empty;
                }
                if (drnew["F_Type"].ToString() == "20000214")
                {
                    drnew["F_URInfo"] = "";
                }

                //字典表
                string[] drdics = { "F_From", "F_VipLevel", "F_State" };
                foreach (string dic in drdics)
                {
                    drnew[dic] = GetDicValue(drnew[dic].ToString());
                }
                string[] drdicsp = { "F_Type" };
                foreach (string dic in drdicsp)
                {
                    drnew[dic] = GetDicPCValue(drnew[dic].ToString());
                }
                //游戏配置表
                string[] drgcs = { "F_GameName" };
                foreach (string gc in drgcs)
                {
                    drnew[gc] = GetGconfigName(drnew[gc].ToString());
                }
                //用户名
                string[] drusers = { "F_DutyMan", "F_PreDutyMan", "F_CreatMan", "F_EditMan" };
                foreach (string user in drusers)
                {
                    drnew[user] = GetUserName(drnew[user].ToString());
                }

                //时间
                string[] drdates = { "F_LimitTime", "F_CreatTime", "F_EditTime" };
                foreach (string date in drdates)
                {
                    try
                    {
                        string d = dr[date].ToString();
                        if (!string.IsNullOrEmpty(d))
                            drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(d));
                    }
                    catch (System.Exception ex)
                    {
                        ex.Message.Logger();
                        drnew[date] = "";
                    }

                }

                dtnew.Rows.Add(drnew);

            }
            ds.Tables.Clear();
            ds.Tables.Add(dtnew);
            ds.Tables.Add(dtold);
            DataSetRowTotalLog(ds, typeof(DBHandle).Name + ".GetTask");
            return ds;
        }


        /// <summary>
        /// 得到工单历史报表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTaskLog(string whereStr)
        {
            string sql = @"SELECT TOP 100  * FROM T_TasksLog with(nolock) where 1=1 " + whereStr;

            DataSet ds = DbHelperSQL.Query(sql);


            DataTable dtnew = ds.Tables[0].Clone();
            dtnew.TableName = "dtnew";

            dtnew.Columns[0].DataType = System.Type.GetType("System.Int32");
            dtnew.Columns[1].DataType = System.Type.GetType("System.Int32");
            int colCount = dtnew.Columns.Count;
            for (int i = 2; i < colCount; i++)
            {
                dtnew.Columns[i].DataType = System.Type.GetType("System.String");
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                DataRow drnew = dtnew.NewRow();

                for (int i = 0; i < colCount; i++)
                {
                    drnew[i] = dr[i].ToString();
                }
                //字典表
                string[] drdics = { "F_From", "F_VipLevel", "F_State" };
                foreach (string dic in drdics)
                {
                    drnew[dic] = GetDicValue(drnew[dic].ToString());
                }
                string[] drdicsp = { "F_Type" };
                foreach (string dic in drdicsp)
                {
                    drnew[dic] = GetDicPCValue(drnew[dic].ToString());
                }
                //游戏配置表
                string[] drgcs = { "F_GameName" };
                foreach (string gc in drgcs)
                {
                    drnew[gc] = GetGconfigName(drnew[gc].ToString());
                }
                //用户名
                string[] drusers = { "F_DutyMan", "F_PreDutyMan", "F_CreatMan", "F_EditMan" };
                foreach (string user in drusers)
                {
                    drnew[user] = GetUserName(drnew[user].ToString());
                }

                //时间
                string[] drdates = { "F_LimitTime", "F_CreatTime", "F_EditTime" };
                foreach (string date in drdates)
                {
                    try
                    {
                        drnew[date] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr[date]));
                    }
                    catch (System.Exception ex)
                    {
                        drnew[date] = "";
                    }

                }

                dtnew.Rows.Add(drnew);

            }
            ds.Tables.Clear();
            ds.Tables.Add(dtnew);


            return ds;
        }
        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddTask(GSSModel.Tasks model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,500),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),//该字段
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
                    new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Direction = ParameterDirection.Output;

            try
            {
                DbHelperSQL.RunProcedure("GSS_Tasks_ADD", parameters, out rowsAffected);
                int id = (int)parameters[34].Value;
                return id;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddTask(GSSModel.Tasks model, DbHelperSQLP sp)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
                    new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Direction = ParameterDirection.Output;

            try
            {
                sp.RunProcedure("GSS_Tasks_ADD", parameters, out rowsAffected);
                int id = (int)parameters[34].Value;
                return id;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 得到工单历史报表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataSet GetTaskLog(string whereStr, DbHelperSQLP sp)
        {
            string sql = @"SELECT TOP 100  * FROM T_TasksLog with(nolock) where 1=1 " + whereStr;
            DataSet ds = sp.Query(sql);
            return ds;
        }

        /// <summary>
        /// 编辑工单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int EditTask(GSSModel.Tasks model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Value = model.F_ID;


            try
            {
                DbHelperSQL.RunProcedure("GSS_Tasks_Update", parameters, out rowsAffected);
                return rowsAffected;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }


        /// <summary>
        /// 编辑工单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int EditTask(GSSModel.Tasks model, DbHelperSQLP sp)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Value = model.F_ID;


            try
            {
                sp.RunProcedure("GSS_Tasks_Update", parameters, out rowsAffected);
                return rowsAffected;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }


        /// <summary>
        /// 编辑工单历史
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int EditTaskLog(GSSModel.Tasks model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Value = model.F_ID;


            try
            {
                DbHelperSQL.RunProcedure("GSS_TasksLog_Update", parameters, out rowsAffected);
                return rowsAffected;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 编辑工单历史
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int EditTaskLog(GSSModel.Tasks model, DbHelperSQLP sp)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_VipLevel", SqlDbType.Int,4),
					new SqlParameter("@F_LimitType", SqlDbType.Int,4),
					new SqlParameter("@F_LimitTime", SqlDbType.DateTime),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Telphone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GPeopleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatMan", SqlDbType.Int,4),
					new SqlParameter("@F_CreatTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_EditTime", SqlDbType.DateTime),
					new SqlParameter("@F_URInfo", SqlDbType.NVarChar),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_CUserName", SqlDbType.Bit,1),
					new SqlParameter("@F_CPSWProtect", SqlDbType.Bit,1),
					new SqlParameter("@F_CPersonID", SqlDbType.Bit,1),
					new SqlParameter("@F_COther", SqlDbType.NVarChar,500),
					new SqlParameter("@F_OLastLoginTime", SqlDbType.NVarChar,50),
					new SqlParameter("@F_OCanRestor", SqlDbType.Bit,1),
					new SqlParameter("@F_OAlwaysPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@F_TToolUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_TUseData", SqlDbType.NVarChar),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_VipLevel;
            parameters[4].Value = model.F_LimitType;
            parameters[5].Value = model.F_LimitTime;
            parameters[6].Value = model.F_Type;
            parameters[7].Value = model.F_State;
            parameters[8].Value = model.F_GameName;
            parameters[9].Value = model.F_GameBigZone;
            parameters[10].Value = model.F_GameZone;
            parameters[11].Value = model.F_GUserID;
            parameters[12].Value = model.F_GUserName;
            parameters[13].Value = model.F_GRoleID;
            parameters[14].Value = model.F_GRoleName;
            parameters[15].Value = model.F_Telphone;
            parameters[16].Value = model.F_GPeopleName;
            parameters[17].Value = model.F_DutyMan;
            parameters[18].Value = model.F_PreDutyMan;
            parameters[19].Value = model.F_CreatMan;
            parameters[20].Value = model.F_CreatTime;
            parameters[21].Value = model.F_EditMan;
            parameters[22].Value = model.F_EditTime;
            parameters[23].Value = model.F_URInfo;
            parameters[24].Value = model.F_Rowtype;
            parameters[25].Value = model.F_CUserName;
            parameters[26].Value = model.F_CPSWProtect;
            parameters[27].Value = model.F_CPersonID;
            parameters[28].Value = model.F_COther;
            parameters[29].Value = model.F_OLastLoginTime;
            parameters[30].Value = model.F_OCanRestor;
            parameters[31].Value = model.F_OAlwaysPlace;
            parameters[32].Value = model.F_TToolUsed;
            parameters[33].Value = model.F_TUseData;
            parameters[34].Value = model.F_ID;


            try
            {
                sp.RunProcedure("GSS_TasksLog_Update", parameters, out rowsAffected);
                return rowsAffected;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 得到部门名称
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        public string GetDeptName(string id)
        {
            if (!WinUtil.IsNumber(id))
            {
                return "";
            }
            DataRow[] drs = dsDepartments.Tables[0].Select("F_DepartID=" + id + "");
            if (drs.Length > 0)
            {
                return drs[0]["F_DepartName"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到角色名称
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        public string GetRoleName(string id)
        {
            if (!WinUtil.IsNumber(id))
            {
                return "";
            }
            DataRow[] drs = dsRoles.Tables[0].Select("F_RoleID=" + id + "");
            if (drs.Length > 0)
            {
                return drs[0]["F_RoleName"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到字典表值
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        private string GetDicValue(string dicid)
        {
            if (!WinUtil.IsNumber(dicid))
            {
                return "";
            }
            DataRow[] drs = dsDictionary.Tables[0].Select("F_DicID=" + dicid + "");
            if (drs.Length > 0)
            {
                string dic = drs[0]["F_Value"].ToString();
                System.Resources.ResourceManager rm = BaseLanguageItem.ResourceManager;
                string value = rm.GetString(dic);
                value = string.IsNullOrEmpty(value) ? dic : value;
                return value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 得到字典表父值
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        private string GetDicPID(string dicid)
        {
            if (!WinUtil.IsNumber(dicid))
            {
                return "";
            }
            DataRow[] drs = dsDictionary.Tables[0].Select("F_DicID=" + dicid + "");
            if (drs.Length > 0)
            {
                return drs[0]["F_ParentID"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 得到字典表父+子值
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        private string GetDicPCValue(string dicid)
        {//如果这里是工单数据检索查询到的是对应的关键字，而不是相关类型文本
            System.Resources.ResourceManager rm = BaseLanguageItem.ResourceManager;
            string category = GetDicValue(GetDicPID(dicid));
            string workOrderCategory = rm.GetString(category);//工单所属分类
            string type = GetDicValue(dicid);
            string workOrderType = rm.GetString(type);
            workOrderCategory = string.IsNullOrEmpty(workOrderCategory) ? category : workOrderCategory;
            workOrderType = string.IsNullOrEmpty(workOrderType) ? type : workOrderType;
            return workOrderCategory + "-" + workOrderType;
        }
        /// <summary>
        /// 得到游戏配置表的值
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        private string GetGconfigValue(string id)
        {
            if (!WinUtil.IsNumber(id))
            {
                return "";
            }
            DataRow[] drs = dsGameConfig.Tables[0].Select("F_ID=" + id + "");
            if (drs.Length > 0)
            {
                return drs[0]["F_Value"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到游戏配置表里的名字
        /// </summary>
        /// <param name="dicid"></param>
        /// <returns></returns>
        private string GetGconfigName(string id)
        {
            if (!WinUtil.IsNumber(id))
            {
                return id;
            }
            DataRow[] drs = dsGameConfig.Tables[0].Select("F_ID=" + id + "");
            if (drs.Length > 0)
            {
                return drs[0]["F_Name"].ToString();
            }
            else
            {
                return id;
            }
        }
        /// <summary>
        /// 得到用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetUserName(string id)
        {
            if (!WinUtil.IsNumber(id))
            {
                return "";
            }
            DataRow[] drs = dsUsers.Tables[0].Select("F_UserID=" + id + "");
            if (drs.Length > 0)
            {
                return string.Format("{0}[{1}]", drs[0]["F_UserName"].ToString(), drs[0]["F_RealName"].ToString());
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 执行SQL语句,返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExeSql(string sql)
        {
            try
            {
                return DbHelperSQL.ExecuteSql(sql);
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }


        //        StringBuilder strSql=new StringBuilder();
        //strSql.Append("update T_Roles set ");
        //strSql.Append("F_IsUsed=@F_IsUsed,");
        //strSql.Append("F_Power=@F_Power");
        //strSql.Append(" where F_RoleID=@F_RoleID");
        //SqlParameter[] parameters = {
        //        new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
        //        new SqlParameter("@F_Power", SqlDbType.NChar,300),
        //        new SqlParameter("@F_RoleID", SqlDbType.Int,4)};
        //parameters[0].Value = model.F_IsUsed;
        //parameters[1].Value = model.F_Power;
        //parameters[2].Value = model.F_RoleID;

        //int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        //if (rows > 0)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}


        /// <summary>
        /// 得到部门表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepts()
        {
            string sql = @"SELECT * FROM T_Department with(nolock)";
            DataSet dsdept = DbHelperSQL.Query(sql);
            return dsdept;
        }

        /// <summary>
        /// 得到角色表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRoles()
        {
            string sql = @"SELECT * FROM T_Roles with(nolock)";
            DataSet dsrole = DbHelperSQL.Query(sql);
            return dsrole;
        }

        public string AddLoginAward(GSSModel.Request.LoginAwardTask at)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                int taskid = AddTask(at.Task);//先添加工单【注：此处后期可以处理成一个存储过程直接进行，将工单和奖励数据一次进行存储】
                //将工单奖励数据写入到gss信息中
                GSSModel.Request.LoginAward award = at.Award;
                string json = award.ConvertJson();
                "create login award".Logger();
                json.Logger();

                param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = award.BigZoneID });
                param.Add(new SqlParameter("@ZoneID", SqlDbType.Int) { Value = award.ZoneID });
                param.Add(new SqlParameter("@logicJson", SqlDbType.NVarChar) { Value = json });
                param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskid });

                DataSet ds = DbHelperSQL.RunProcedure("SP_AddAwardToMysql", param.ToArray(), typeof(DBHandle).Name);// error msg write log
                if (ds == null || ds.Tables.Count == 0)
                {
                    "login award:the service lack link server".Logger();
                    return BaseLanguageItem.Tip_SyncGameDbError;
                }
                string timeFormat = "yyyy-MM-dd HH:mm:ss";
                string mysqlCmd = @"insert into playerloadgameawardinfo (F_min_level,F_max_level,
F_Item1,F_ItemNum1,F_Item2,F_ItemNum2,F_Item3,F_ItemNum3,F_Item4,F_ItemNum4,F_Item5,F_ItemNum5,
F_begintime,F_invalidtime,F_note,F_Order,F_Mail_Title,F_Mail_Content,F_Sender_Name,F_Bind_GOLD,F_GOLD ) values(" + award.MinLevel + "," + award.MaxLevel + ","
+ award.Item1 + "," + award.ItemNum1 + "," + award.Item2 + "," + award.ItemNum2 + "," + award.Item3 + "," + award.ItemNum3 + "," + award.Item4 + "," + award.ItemNum4 + "," + award.Item5 + "," + award.ItemNum5 + ","
+ "'" + award.StartTime.ToString(timeFormat) + "','" + award.EndTime.ToString(timeFormat) + "','" + award.Remark + "','" + taskid + "','" + award.ActiveName + "','" + award.EmailBody + "','" + award.SendBy + "'," + award.BlueDiamond + "," + award.Money + ")";
                GSS.DBUtility.DbHelperMySQLP sync = new DbHelperMySQLP();
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string link = item["name"] as string;
                    string conn = item["provider_string"] as string;
                    (link + "\t" + conn).Logger();
                    if (string.IsNullOrEmpty(conn))
                    {
                        (link + " :lose db connString").Logger();
                        sb.AppendLine(string.Format(BaseLanguageItem.Tip_LinkServerStatue_4043, link));
                        continue;
                    }
                    string filter = FilterMySqlDBConnString(conn);
                    DbHelperMySQL.connectionString = filter;
                    int succ = DbHelperMySQL.ExecuteSql(mysqlCmd);
                    if (succ == 0)
                    {
                        sb.AppendLine(link + " :sync error");
                    }
                }
                string error = sb.ToString();
                if (!string.IsNullOrEmpty(error))
                {
                    error.Logger();
                    return error;
                }
                return true.ToString();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                msg.Logger();
                return msg;
            }
        }
        public string AddFullServiceEmail(GSSModel.Request.LoginAwardTask at)
        {
            try
            {
                int taskid = AddTask(at.Task);//先添加工单【注：此处后期可以处理成一个存储过程直接进行，将工单和奖励数据一次进行存储】
                //将工单奖励数据写入到gss信息中
                GSSModel.Request.LoginAward award = at.Award;
                string json = award.ConvertJson();
                "create login award".Logger();
                json.Logger();
                string[] battleZone = at.Task.F_GameZone.Split(';');
                for (int i = 0; i < battleZone.Count(); i++)
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = award.BigZoneID });
                    param.Add(new SqlParameter("@ZoneID", SqlDbType.Int) { Value = Convert.ToInt32(battleZone[i]) });
                    param.Add(new SqlParameter("@logicJson", SqlDbType.NVarChar) { Value = json });
                    param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskid });

                    DataSet ds = DbHelperSQL.RunProcedure("SP_AddAwardToMysql", param.ToArray(), typeof(DBHandle).Name);// error msg write log

                    if (ds == null || ds.Tables.Count == 0)
                    {
                        "login award:the service lack link server".Logger();
                        return BaseLanguageItem.Tip_SyncGameDbError;
                    }
                    string timeFormat = "yyyy-MM-dd HH:mm:ss";
                    string mysqlCmd = @"insert into sys_loss_award_table (DBID,LevelMin,LevelMax,
ItemID1,ItemNum1,ItemID2,ItemNum2,ItemID3,ItemNum3,ItemID4,ItemNum4,ItemID5,ItemNum5,
BeginTime,InvalidTime,ItemContent,F_Mail_Title,F_Mail_Content,F_Sender_Name,F_BIND_GOLD,F_GOLD ) 
values(" + taskid + "," + award.MinLevel + "," + award.MaxLevel + ","
    + award.Item1 + "," + award.ItemNum1 + "," + award.Item2 + "," + award.ItemNum2 + "," + award.Item3 + "," + award.ItemNum3 + "," + award.Item4 + "," + award.ItemNum4 + "," + award.Item5 + "," + award.ItemNum5 + ","
    + "'" + award.StartTime.ToString(timeFormat) + "','" + award.EndTime.ToString(timeFormat) + "','" + award.Remark + "','" + award.ActiveName + "','" + award.EmailBody + "','" + award.SendBy + "'," + award.BlueDiamond + "," + award.Money + ")";
                    GSS.DBUtility.DbHelperMySQLP sync = new DbHelperMySQLP();
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        string link = item["name"] as string;
                        string conn = item["provider_string"] as string;
                        (link + "\t" + conn).Logger();
                        if (string.IsNullOrEmpty(conn))
                        {
                            (link + " :lose db connString").Logger();
                            sb.AppendLine(string.Format(BaseLanguageItem.Tip_LinkServerStatue_4043, link));
                            continue;
                        }
                        string filter = FilterMySqlDBConnString(conn);
                        DbHelperMySQL.connectionString = filter;
                        int succ = DbHelperMySQL.ExecuteSql(mysqlCmd);
                        if (succ == 0)
                        {
                            sb.AppendLine(link + " :sync error");
                        }
                    }
                    string error = sb.ToString();
                    if (!string.IsNullOrEmpty(error))
                    {
                        error.Logger();
                        return error;
                    }
                }
                return true.ToString();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                msg.Logger();
                return msg;
            }
        }
        /// <summary>
        /// only recovery GameCoreDB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public string RecoveryRole(GSSModel.Request.RoleData role)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                //-- 4041 sql server 中没有数据库连接实例名，4042 没有MySQL连接实例名
                //-- 1 改角色数据使用的UI位置被其他角色占用 2 客户端界面没有被刷新，实际上改角色已恢复
                //- 200操作成功

                param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = role.BigZoneId });
                param.Add(new SqlParameter("@zoneID", SqlDbType.Int) { Value = role.ZoneId.HasValue ? role.ZoneId.Value : -1 });
                param.Add(new SqlParameter("@uid", SqlDbType.Int) { Value = role.UserID });
                param.Add(new SqlParameter("@delRid", SqlDbType.Int) { Value = role.RoleID });
                param.Add(new SqlParameter("@code", SqlDbType.Int));
                param[param.Count - 1].Direction = ParameterDirection.Output;
                int row;
                DbHelperSQL.RunProcedure("SP_VerifyRoleRecovery", param.ToArray(), out row);
                int code = (int)param[param.Count - 1].Value;
                if (code == 200)
                {
                    return true.ToString();
                }
                return code.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public int AddSampleTaskWithLogicData(GSSModel.SampleWorkOrder order)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            string outputField = "@taskId";
            int taskId = 0;
            param.Add(new SqlParameter("@logicData", SqlDbType.NVarChar) { Value = order.LogicData });
            param.Add(new SqlParameter("@createBy", SqlDbType.Int) { Value = order.CreateBy });
            param.Add(new SqlParameter("@F_GameBigZone", SqlDbType.NVarChar, 16) { Value = order.BigZoneName });
            param.Add(new SqlParameter("@F_GameZone", SqlDbType.NVarChar, 16) { Value = order.ZoneName });
            param.Add(new SqlParameter("@TaskTypeID", SqlDbType.Int) { Value = order.TypeId });
            param.Add(new SqlParameter("@title", SqlDbType.NVarChar, 30) { Value = order.Title });
            param.Add(new SqlParameter("@F_Note", SqlDbType.NVarChar, 500) { Value = order.Note });
            param.Add(new SqlParameter("@AppId", SqlDbType.Int) { Value = order.AppId });
            param.Add(new SqlParameter(outputField, SqlDbType.Int) { Direction = ParameterDirection.Output });
            int row;
            DbHelperSQL.RunProcedure("SP_AddTaskLogicDataWithTask", param.ToArray(), out row);
            if (row > 0)
            {//操作成功 
                object obj = param.Where(p => p.ParameterName == outputField).Select(p => p.Value).FirstOrDefault();
                if (obj != null)
                {
                    taskId = (int)obj;
                }
            }
            return taskId;
        }
        public void FinshSyncLogicData(int taskId)
        {//成功同步了逻辑数据到MySQL库中之后设置逻辑数据的标志
            SqlParameter param = new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId };
            int row;
            DbHelperSQL.RunProcedure("SP_TaskLogicSyncComplate", new SqlParameter[] { param }, out row);
        }
        static string FilterMySqlDBConnString(string dbConstring)
        {
            string[] filter = new string[] { "driver", "option", "stmt" };
            string[] mysqlConfigs = dbConstring.Split(';');
            List<string> connConfig = new List<string>();
            foreach (string item in mysqlConfigs)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (!filter.Contains(item.Split('=')[0].ToLower()))
                {
                    connConfig.Add(item);
                }
            }
            return string.Join(";", connConfig.ToArray());
        }
        static void DataSetRowTotalLog(DataSet ds, string cmd)
        {

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                string.Format("{0}-> query data rows:[null]", cmd).Logger();
            }
            else
            {
                string.Format("{0}-> query data rows:[{1}]", cmd, ds.Tables[0].Rows.Count).Logger();
            }
        }
        /// <summary>
        /// 在创建工单完毕之后添加工单的逻辑数据
        /// </summary>
        /// <param name="taskId">工单ID</param>
        /// <param name="logicData">逻辑数据的json串</param>
        /// <returns>执行时的返回状态码 200表示执行成功</returns>
        public static int AddLogicDataAfterTask(int taskId, string logicData)
        {
            string.Format("Logic Json:->\r\n{0}", logicData).Logger();
            List<SqlParameter> param = new List<SqlParameter>();
            string outputField = "@code";
            param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId });
            param.Add(new SqlParameter("@logicData", SqlDbType.NVarChar) { Value = logicData });
            param.Add(new SqlParameter(outputField, SqlDbType.Int) { Direction = ParameterDirection.Output });
            int row;
            try
            {
                DbHelperSQL.RunProcedure("SP_AddLogicDataAfterTask", param.ToArray(), out row);
                SqlParameter p = param.Where(s => s.ParameterName == outputField).FirstOrDefault();
                return (int)p.Value;
            }
            catch (Exception ex)
            {
                ex.Message.Logger();
                return GSSModel.Request.StatueCode.ServiceUnavaliable;
            }
        }
        public int InsertLogicJsonAfterTask(int taskId, string logicData)
        {
            return AddLogicDataAfterTask(taskId, logicData);
        }
    }
}
