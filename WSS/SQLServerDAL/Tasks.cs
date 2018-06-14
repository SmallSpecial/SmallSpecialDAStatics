using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.IDAL;
using WSS.DBUtility;//Please add references
namespace WSS.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:Tasks
    /// </summary>
    public partial class Tasks : ITasks
    {
        public Tasks()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Tasks");
            strSql.Append(" where F_ID=@F_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
            parameters[0].Value = F_ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WSS.Model.Tasks model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Tasks(");
            strSql.Append("F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype)");
            strSql.Append(" values (");
            strSql.Append("@F_Title,@F_Note,@F_From,@F_Type,@F_JinjiLevel,@F_GameName,@F_GameZone,@F_GUserID,@F_GRoleName,@F_Tag,@F_State,@F_Telphone,@F_DutyMan,@F_PreDutyMan,@F_DateTime,@F_EditMan,@F_Rowtype)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,500),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_JinjiLevel", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Tag", SqlDbType.NVarChar,50),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_Telphone", SqlDbType.NChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_DateTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_Type;
            parameters[4].Value = model.F_JinjiLevel;
            parameters[5].Value = model.F_GameName;
            parameters[6].Value = model.F_GameZone;
            parameters[7].Value = model.F_GUserID;
            parameters[8].Value = model.F_GRoleName;
            parameters[9].Value = model.F_Tag;
            parameters[10].Value = model.F_State;
            parameters[11].Value = model.F_Telphone;
            parameters[12].Value = model.F_DutyMan;
            parameters[13].Value = model.F_PreDutyMan;
            parameters[14].Value = model.F_DateTime;
            parameters[15].Value = model.F_EditMan;
            parameters[16].Value = model.F_Rowtype;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WSS.Model.Tasks model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Tasks set ");
            strSql.Append("F_Title=@F_Title,");
            strSql.Append("F_Note=@F_Note,");
            strSql.Append("F_From=@F_From,");
            strSql.Append("F_Type=@F_Type,");
            strSql.Append("F_JinjiLevel=@F_JinjiLevel,");
            strSql.Append("F_GameName=@F_GameName,");
            strSql.Append("F_GameZone=@F_GameZone,");
            strSql.Append("F_GUserID=@F_GUserID,");
            strSql.Append("F_GRoleName=@F_GRoleName,");
            strSql.Append("F_Tag=@F_Tag,");
            strSql.Append("F_State=@F_State,");
            strSql.Append("F_Telphone=@F_Telphone,");
            strSql.Append("F_DutyMan=@F_DutyMan,");
            strSql.Append("F_PreDutyMan=@F_PreDutyMan,");
            strSql.Append("F_DateTime=@F_DateTime,");
            strSql.Append("F_EditMan=@F_EditMan,");
            strSql.Append("F_Rowtype=@F_Rowtype");
            strSql.Append(" where F_ID=@F_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,500),
					new SqlParameter("@F_From", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_JinjiLevel", SqlDbType.Int,4),
					new SqlParameter("@F_GameName", SqlDbType.Int,4),
					new SqlParameter("@F_GameZone", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GUserID", SqlDbType.NVarChar,16),
					new SqlParameter("@F_GRoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_Tag", SqlDbType.NVarChar,50),
					new SqlParameter("@F_State", SqlDbType.Int,4),
					new SqlParameter("@F_Telphone", SqlDbType.NChar,16),
					new SqlParameter("@F_DutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_PreDutyMan", SqlDbType.Int,4),
					new SqlParameter("@F_DateTime", SqlDbType.DateTime),
					new SqlParameter("@F_EditMan", SqlDbType.Int,4),
					new SqlParameter("@F_Rowtype", SqlDbType.TinyInt,1),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Title;
            parameters[1].Value = model.F_Note;
            parameters[2].Value = model.F_From;
            parameters[3].Value = model.F_Type;
            parameters[4].Value = model.F_JinjiLevel;
            parameters[5].Value = model.F_GameName;
            parameters[6].Value = model.F_GameZone;
            parameters[7].Value = model.F_GUserID;
            parameters[8].Value = model.F_GRoleName;
            parameters[9].Value = model.F_Tag;
            parameters[10].Value = model.F_State;
            parameters[11].Value = model.F_Telphone;
            parameters[12].Value = model.F_DutyMan;
            parameters[13].Value = model.F_PreDutyMan;
            parameters[14].Value = model.F_DateTime;
            parameters[15].Value = model.F_EditMan;
            parameters[16].Value = model.F_Rowtype;
            parameters[17].Value = model.F_ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int F_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Tasks ");
            strSql.Append(" where F_ID=@F_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
            parameters[0].Value = F_ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string F_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Tasks ");
            strSql.Append(" where F_ID in (" + F_IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WSS.Model.Tasks GetModel(int F_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype from T_Tasks ");
            strSql.Append(" where F_ID=@F_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
            parameters[0].Value = F_ID;

            WSS.Model.Tasks model = new WSS.Model.Tasks();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["F_ID"].ToString() != "")
                {
                    model.F_ID = int.Parse(ds.Tables[0].Rows[0]["F_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Title"] != null)
                {
                    model.F_Title = ds.Tables[0].Rows[0]["F_Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Note"] != null)
                {
                    model.F_Note = ds.Tables[0].Rows[0]["F_Note"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_From"].ToString() != "")
                {
                    model.F_From = int.Parse(ds.Tables[0].Rows[0]["F_From"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Type"].ToString() != "")
                {
                    model.F_Type = int.Parse(ds.Tables[0].Rows[0]["F_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString() != "")
                {
                    model.F_JinjiLevel = int.Parse(ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameName"].ToString() != "")
                {
                    model.F_GameName = int.Parse(ds.Tables[0].Rows[0]["F_GameName"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameZone"] != null)
                {
                    model.F_GameZone = ds.Tables[0].Rows[0]["F_GameZone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GUserID"] != null)
                {
                    model.F_GUserID = ds.Tables[0].Rows[0]["F_GUserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GRoleName"] != null)
                {
                    model.F_GRoleName = ds.Tables[0].Rows[0]["F_GRoleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Tag"] != null)
                {
                    model.F_Tag = ds.Tables[0].Rows[0]["F_Tag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_State"].ToString() != "")
                {
                    model.F_State = int.Parse(ds.Tables[0].Rows[0]["F_State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Telphone"] != null)
                {
                    model.F_Telphone = ds.Tables[0].Rows[0]["F_Telphone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_DutyMan"].ToString() != "")
                {
                    model.F_DutyMan = int.Parse(ds.Tables[0].Rows[0]["F_DutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString() != "")
                {
                    model.F_PreDutyMan = int.Parse(ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_DateTime"].ToString() != "")
                {
                    model.F_DateTime = DateTime.Parse(ds.Tables[0].Rows[0]["F_DateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_EditMan"].ToString() != "")
                {
                    model.F_EditMan = int.Parse(ds.Tables[0].Rows[0]["F_EditMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Rowtype"].ToString() != "")
                {
                    model.F_Rowtype = int.Parse(ds.Tables[0].Rows[0]["F_Rowtype"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype ");
            strSql.Append(" FROM T_Tasks ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype ");
            strSql.Append(" FROM T_Tasks ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_Tasks";
            parameters[1].Value = "F_ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

