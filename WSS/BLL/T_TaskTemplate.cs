using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.DBUtility;//Please add references
namespace WSS.BLL
{
    /// <summary>
    /// 类T_TaskTemplate。
    /// </summary>
    [Serializable]
    public partial class T_TaskTemplate
    {
        public T_TaskTemplate()
        { }
        #region Model
        private int _f_id;
        private int? _f_type;
        private string _f_template;
        /// <summary>
        /// 主键ID
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// 工单类型
        /// </summary>
        public int? F_Type
        {
            set { _f_type = value; }
            get { return _f_type; }
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string F_Template
        {
            set { _f_template = value; }
            get { return _f_template; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_TaskTemplate(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select F_Type,F_Template ");
            strSql.Append(" FROM [T_TaskTemplate] ");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = F_ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["F_ID"].ToString() != "")
                {
                    this.F_ID = int.Parse(ds.Tables[0].Rows[0]["F_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Type"].ToString() != "")
                {
                    this.F_Type = int.Parse(ds.Tables[0].Rows[0]["F_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Template"] != null)
                {
                    this.F_Template = ds.Tables[0].Rows[0]["F_Template"].ToString();
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
            strSql.Append(" FROM [T_TaskTemplate] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

