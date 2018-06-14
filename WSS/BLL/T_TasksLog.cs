using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.DBUtility;//Please add references

namespace WSS.BLL
{
    /// <summary>
    /// 类T_TasksLog。
    /// </summary>
    [Serializable]
    public partial class T_TasksLog
    {
        public T_TasksLog()
        { }
        #region Model
        private int _f_id;
        private string _f_title;
        private string _f_note;
        private int? _f_from;
        private int? _f_type;
        private int? _f_jinjilevel;
        private int? _f_gamename;
        private string _f_gamezone;
        private string _f_guserid;
        private string _f_grolename;
        private string _f_tag;
        private int? _f_state;
        private string _f_telphone;
        private int? _f_dutyman;
        private int? _f_predutyman;
        private DateTime _f_datetime;
        private int? _f_editman;
        private int? _f_rowtype = 0;
        /// <summary>
        /// 
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string F_Title
        {
            set { _f_title = value; }
            get { return _f_title; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        /// <summary>
        /// 工单来源
        /// </summary>
        public int? F_From
        {
            set { _f_from = value; }
            get { return _f_from; }
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
        /// 紧急程度
        /// </summary>
        public int? F_JinjiLevel
        {
            set { _f_jinjilevel = value; }
            get { return _f_jinjilevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_GameName
        {
            set { _f_gamename = value; }
            get { return _f_gamename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GameZone
        {
            set { _f_gamezone = value; }
            get { return _f_gamezone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GUserID
        {
            set { _f_guserid = value; }
            get { return _f_guserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GRoleName
        {
            set { _f_grolename = value; }
            get { return _f_grolename; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string F_Tag
        {
            set { _f_tag = value; }
            get { return _f_tag; }
        }
        /// <summary>
        /// 工单状态
        /// </summary>
        public int? F_State
        {
            set { _f_state = value; }
            get { return _f_state; }
        }
        /// <summary>
        /// 玩家电话
        /// </summary>
        public string F_Telphone
        {
            set { _f_telphone = value; }
            get { return _f_telphone; }
        }
        /// <summary>
        /// 当前职责人
        /// </summary>
        public int? F_DutyMan
        {
            set { _f_dutyman = value; }
            get { return _f_dutyman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_PreDutyMan
        {
            set { _f_predutyman = value; }
            get { return _f_predutyman; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime F_DateTime
        {
            set { _f_datetime = value; }
            get { return _f_datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_EditMan
        {
            set { _f_editman = value; }
            get { return _f_editman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_Rowtype
        {
            set { _f_rowtype = value; }
            get { return _f_rowtype; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_TasksLog(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype ");
            strSql.Append(" FROM [T_TasksLog] ");
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
                if (ds.Tables[0].Rows[0]["F_Title"] != null)
                {
                    this.F_Title = ds.Tables[0].Rows[0]["F_Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Note"] != null)
                {
                    this.F_Note = ds.Tables[0].Rows[0]["F_Note"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_From"].ToString() != "")
                {
                    this.F_From = int.Parse(ds.Tables[0].Rows[0]["F_From"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Type"].ToString() != "")
                {
                    this.F_Type = int.Parse(ds.Tables[0].Rows[0]["F_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString() != "")
                {
                    this.F_JinjiLevel = int.Parse(ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameName"].ToString() != "")
                {
                    this.F_GameName = int.Parse(ds.Tables[0].Rows[0]["F_GameName"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameZone"] != null)
                {
                    this.F_GameZone = ds.Tables[0].Rows[0]["F_GameZone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GUserID"] != null)
                {
                    this.F_GUserID = ds.Tables[0].Rows[0]["F_GUserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GRoleName"] != null)
                {
                    this.F_GRoleName = ds.Tables[0].Rows[0]["F_GRoleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Tag"] != null)
                {
                    this.F_Tag = ds.Tables[0].Rows[0]["F_Tag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_State"].ToString() != "")
                {
                    this.F_State = int.Parse(ds.Tables[0].Rows[0]["F_State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Telphone"] != null)
                {
                    this.F_Telphone = ds.Tables[0].Rows[0]["F_Telphone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_DutyMan"].ToString() != "")
                {
                    this.F_DutyMan = int.Parse(ds.Tables[0].Rows[0]["F_DutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString() != "")
                {
                    this.F_PreDutyMan = int.Parse(ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_DateTime"].ToString() != "")
                {
                    this.F_DateTime = DateTime.Parse(ds.Tables[0].Rows[0]["F_DateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_EditMan"].ToString() != "")
                {
                    this.F_EditMan = int.Parse(ds.Tables[0].Rows[0]["F_EditMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Rowtype"].ToString() != "")
                {
                    this.F_Rowtype = int.Parse(ds.Tables[0].Rows[0]["F_Rowtype"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [T_TasksLog]");
            strSql.Append(" where F_ID=@F_ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = F_ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_TasksLog] (");
            strSql.Append("F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype)");
            strSql.Append(" values (");
            strSql.Append("@F_ID,@F_Title,@F_Note,@F_From,@F_Type,@F_JinjiLevel,@F_GameName,@F_GameZone,@F_GUserID,@F_GRoleName,@F_Tag,@F_State,@F_Telphone,@F_DutyMan,@F_PreDutyMan,@F_DateTime,@F_EditMan,@F_Rowtype)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@F_ID", SqlDbType.Int,4),
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
            parameters[0].Value = F_ID;
            parameters[1].Value = F_Title;
            parameters[2].Value = F_Note;
            parameters[3].Value = F_From;
            parameters[4].Value = F_Type;
            parameters[5].Value = F_JinjiLevel;
            parameters[6].Value = F_GameName;
            parameters[7].Value = F_GameZone;
            parameters[8].Value = F_GUserID;
            parameters[9].Value = F_GRoleName;
            parameters[10].Value = F_Tag;
            parameters[11].Value = F_State;
            parameters[12].Value = F_Telphone;
            parameters[13].Value = F_DutyMan;
            parameters[14].Value = F_PreDutyMan;
            parameters[15].Value = F_DateTime;
            parameters[16].Value = F_EditMan;
            parameters[17].Value = F_Rowtype;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_TasksLog] set ");
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
            strSql.Append(" where F_ID=@F_ID ");
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
            parameters[0].Value = F_Title;
            parameters[1].Value = F_Note;
            parameters[2].Value = F_From;
            parameters[3].Value = F_Type;
            parameters[4].Value = F_JinjiLevel;
            parameters[5].Value = F_GameName;
            parameters[6].Value = F_GameZone;
            parameters[7].Value = F_GUserID;
            parameters[8].Value = F_GRoleName;
            parameters[9].Value = F_Tag;
            parameters[10].Value = F_State;
            parameters[11].Value = F_Telphone;
            parameters[12].Value = F_DutyMan;
            parameters[13].Value = F_PreDutyMan;
            parameters[14].Value = F_DateTime;
            parameters[15].Value = F_EditMan;
            parameters[16].Value = F_Rowtype;
            parameters[17].Value = F_ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [T_TasksLog] ");
            strSql.Append(" where F_ID=@F_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
            parameters[0].Value = F_ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int F_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 F_ID,F_Title,F_Note,F_From,F_Type,F_JinjiLevel,F_GameName,F_GameZone,F_GUserID,F_GRoleName,F_Tag,F_State,F_Telphone,F_DutyMan,F_PreDutyMan,F_DateTime,F_EditMan,F_Rowtype ");
            strSql.Append(" FROM [T_TasksLog] ");
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
                if (ds.Tables[0].Rows[0]["F_Title"] != null)
                {
                    this.F_Title = ds.Tables[0].Rows[0]["F_Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Note"] != null)
                {
                    this.F_Note = ds.Tables[0].Rows[0]["F_Note"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_From"].ToString() != "")
                {
                    this.F_From = int.Parse(ds.Tables[0].Rows[0]["F_From"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Type"].ToString() != "")
                {
                    this.F_Type = int.Parse(ds.Tables[0].Rows[0]["F_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString() != "")
                {
                    this.F_JinjiLevel = int.Parse(ds.Tables[0].Rows[0]["F_JinjiLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameName"].ToString() != "")
                {
                    this.F_GameName = int.Parse(ds.Tables[0].Rows[0]["F_GameName"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_GameZone"] != null)
                {
                    this.F_GameZone = ds.Tables[0].Rows[0]["F_GameZone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GUserID"] != null)
                {
                    this.F_GUserID = ds.Tables[0].Rows[0]["F_GUserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_GRoleName"] != null)
                {
                    this.F_GRoleName = ds.Tables[0].Rows[0]["F_GRoleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_Tag"] != null)
                {
                    this.F_Tag = ds.Tables[0].Rows[0]["F_Tag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_State"].ToString() != "")
                {
                    this.F_State = int.Parse(ds.Tables[0].Rows[0]["F_State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Telphone"] != null)
                {
                    this.F_Telphone = ds.Tables[0].Rows[0]["F_Telphone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["F_DutyMan"].ToString() != "")
                {
                    this.F_DutyMan = int.Parse(ds.Tables[0].Rows[0]["F_DutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString() != "")
                {
                    this.F_PreDutyMan = int.Parse(ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_DateTime"].ToString() != "")
                {
                    this.F_DateTime = DateTime.Parse(ds.Tables[0].Rows[0]["F_DateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_EditMan"].ToString() != "")
                {
                    this.F_EditMan = int.Parse(ds.Tables[0].Rows[0]["F_EditMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F_Rowtype"].ToString() != "")
                {
                    this.F_Rowtype = int.Parse(ds.Tables[0].Rows[0]["F_Rowtype"].ToString());
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
            strSql.Append(" FROM [T_TasksLog] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
