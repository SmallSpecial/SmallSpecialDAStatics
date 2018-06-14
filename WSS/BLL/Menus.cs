using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WSS.DBUtility;//Please add references
namespace WSS.BLL
{
    /// <summary>
    /// 类T_Menus。
    /// </summary>
    [Serializable]
    public partial class T_Menus
    {
        public T_Menus()
        { }
        #region Model
        private int _f_menuid;
        private string _f_name;
        private string _f_url;
        private int _f_parentid = 0;
        private bool _f_isused = true;
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int F_MenuID
        {
            set { _f_menuid = value; }
            get { return _f_menuid; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string F_Name
        {
            set { _f_name = value; }
            get { return _f_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_URL
        {
            set { _f_url = value; }
            get { return _f_url; }
        }
        /// <summary>
        /// 父编号
        /// </summary>
        public int F_ParentID
        {
            set { _f_parentid = value; }
            get { return _f_parentid; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool F_IsUsed
        {
            set { _f_isused = value; }
            get { return _f_isused; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_Menus(int F_MenuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select F_MenuID,F_Name,F_URL,F_ParentID,F_IsUsed ");
            strSql.Append(" FROM [T_Menus] ");
            strSql.Append(" where F_MenuID=@F_MenuID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
            parameters[0].Value = F_MenuID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["F_MenuID"].ToString() != "")
                {
                    this.F_MenuID = int.Parse(ds.Tables[0].Rows[0]["F_MenuID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Name"] != null)
                {
                    this.F_Name = ds.Tables[0].Rows[0]["F_Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_URL"] != null)
                {
                    this.F_URL = ds.Tables[0].Rows[0]["F_URL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_ParentID"].ToString() != "")
                {
                    this.F_ParentID = int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_IsUsed"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["F_IsUsed"].ToString() == "1") || (ds.Tables[0].Rows[0]["F_IsUsed"].ToString().ToLower() == "true"))
                    {
                        this.F_IsUsed = true;
                    }
                    else
                    {
                        this.F_IsUsed = false;
                    }
                }

            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("F_MenuID", "T_Menus");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_MenuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [T_Menus]");
            strSql.Append(" where F_MenuID=@F_MenuID ");

            SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
            parameters[0].Value = F_MenuID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Menus] (");
            strSql.Append("F_MenuID,F_Name,F_URL,F_ParentID,F_IsUsed)");
            strSql.Append(" values (");
            strSql.Append("@F_MenuID,@F_Name,@F_URL,@F_ParentID,@F_IsUsed)");
            SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4),
					new SqlParameter("@F_Name", SqlDbType.NVarChar,50),
					new SqlParameter("@F_URL", SqlDbType.NVarChar,150),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1)};
            parameters[0].Value = F_MenuID;
            parameters[1].Value = F_Name;
            parameters[2].Value = F_URL;
            parameters[3].Value = F_ParentID;
            parameters[4].Value = F_IsUsed;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Menus] set ");
            strSql.Append("F_Name=@F_Name,");
            strSql.Append("F_URL=@F_URL,");
            strSql.Append("F_ParentID=@F_ParentID,");
            strSql.Append("F_IsUsed=@F_IsUsed");
            strSql.Append(" where F_MenuID=@F_MenuID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_Name", SqlDbType.NVarChar,50),
					new SqlParameter("@F_URL", SqlDbType.NVarChar,150),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
            parameters[0].Value = F_Name;
            parameters[1].Value = F_URL;
            parameters[2].Value = F_ParentID;
            parameters[3].Value = F_IsUsed;
            parameters[4].Value = F_MenuID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int F_MenuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [T_Menus] ");
            strSql.Append(" where F_MenuID=@F_MenuID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
            parameters[0].Value = F_MenuID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int F_MenuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 F_MenuID,F_Name,F_URL,F_ParentID,F_IsUsed ");
            strSql.Append(" FROM [T_Menus] ");
            strSql.Append(" where F_MenuID=@F_MenuID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
            parameters[0].Value = F_MenuID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["F_MenuID"].ToString() != "")
                {
                    this.F_MenuID = int.Parse(ds.Tables[0].Rows[0]["F_MenuID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Name"] != null)
                {
                    this.F_Name = ds.Tables[0].Rows[0]["F_Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_URL"] != null)
                {
                    this.F_URL = ds.Tables[0].Rows[0]["F_URL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_ParentID"].ToString() != "")
                {
                    this.F_ParentID = int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_IsUsed"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["F_IsUsed"].ToString() == "1") || (ds.Tables[0].Rows[0]["F_IsUsed"].ToString().ToLower() == "true"))
                    {
                        this.F_IsUsed = true;
                    }
                    else
                    {
                        this.F_IsUsed = false;
                    }
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [T_Menus] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

