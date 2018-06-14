using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
    /// <summary>
    /// 数据访问类:T_GameConfig
    /// </summary>
    public partial class GameConfig
    {
        public GameConfig()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("F_ID", "T_GameConfig");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_GameConfig");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = F_ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(GSSModel.GameConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_GameConfig(");
            strSql.Append("F_ID,F_ParentID,F_Name,F_Value,F_Value1,F_ValueGame,F_IsUsed,F_Sort)");
            strSql.Append(" values (");
            strSql.Append("@F_ID,@F_ParentID,@F_Name,@F_Value,@F_Value1,@F_ValueGame,@F_IsUsed,@F_Sort)");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_Name", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Value", SqlDbType.NVarChar,200),
					new SqlParameter("@F_Value1", SqlDbType.NVarChar,200),
					new SqlParameter("@F_ValueGame", SqlDbType.NVarChar,100),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.F_ID;
            parameters[1].Value = model.F_ParentID;
            parameters[2].Value = model.F_Name;
            parameters[3].Value = model.F_Value;
            parameters[4].Value = model.F_Value1;
            parameters[5].Value = model.F_ValueGame;
            parameters[6].Value = model.F_IsUsed;
            parameters[7].Value = model.F_Sort;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(GSSModel.GameConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_GameConfig set ");
            strSql.Append("F_ParentID=@F_ParentID,");
            strSql.Append("F_Name=@F_Name,");
            strSql.Append("F_Value=@F_Value,");
            strSql.Append("F_Value1=@F_Value1,");
            strSql.Append("F_ValueGame=@F_ValueGame,");
            strSql.Append("F_IsUsed=@F_IsUsed,");
            strSql.Append("F_Sort=@F_Sort");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_Name", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Value", SqlDbType.NVarChar,200),
					new SqlParameter("@F_Value1", SqlDbType.NVarChar,200),
					new SqlParameter("@F_ValueGame", SqlDbType.NVarChar,100),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Sort", SqlDbType.Int,4),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = model.F_ParentID;
            parameters[1].Value = model.F_Name;
            parameters[2].Value = model.F_Value;
            parameters[3].Value = model.F_Value1;
            parameters[4].Value = model.F_ValueGame;
            parameters[5].Value = model.F_IsUsed;
            parameters[6].Value = model.F_Sort;
            parameters[7].Value = model.F_ID;

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
            strSql.Append("delete from T_GameConfig ");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
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
            strSql.Append("delete from T_GameConfig ");
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
        public GSSModel.GameConfig GetModel(int F_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 F_ID,F_ParentID,F_Name,F_Value,F_Value1,F_ValueGame,F_IsUsed,F_Sort from T_GameConfig ");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = F_ID;

            GSSModel.GameConfig model = new GSSModel.GameConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["F_ID"].ToString() != "")
                {
                    model.F_ID = int.Parse(ds.Tables[0].Rows[0]["F_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_ParentID"].ToString() != "")
                {
                    model.F_ParentID = int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Name"] != null)
                {
                    model.F_Name = ds.Tables[0].Rows[0]["F_Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Value"] != null)
                {
                    model.F_Value = ds.Tables[0].Rows[0]["F_Value"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Value1"] != null)
                {
                    model.F_Value1 = ds.Tables[0].Rows[0]["F_Value1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_ValueGame"] != null)
                {
                    model.F_ValueGame = ds.Tables[0].Rows[0]["F_ValueGame"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_IsUsed"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["F_IsUsed"].ToString() == "1") || (ds.Tables[0].Rows[0]["F_IsUsed"].ToString().ToLower() == "true"))
                    {
                        model.F_IsUsed = true;
                    }
                    else
                    {
                        model.F_IsUsed = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["F_Sort"].ToString() != "")
                {
                    model.F_Sort = int.Parse(ds.Tables[0].Rows[0]["F_Sort"].ToString());
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
            strSql.Append("select F_ID,F_ParentID,F_Name,F_Value,F_Value1,F_ValueGame,F_IsUsed,F_Sort ");
            strSql.Append(" FROM T_GameConfig ");
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
            strSql.Append(" F_ID,F_ParentID,F_Name,F_Value,F_Value1,F_ValueGame,F_IsUsed,F_Sort ");
            strSql.Append(" FROM T_GameConfig ");
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
            parameters[0].Value = "T_GameConfig";
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

