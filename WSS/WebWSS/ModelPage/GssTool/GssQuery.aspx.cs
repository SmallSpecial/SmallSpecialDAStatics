using MySql.Data.MySqlClient;
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

namespace WebWSS.ModelPage.GssTool
{
    public partial class GssQuery : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();
        DataSet ds;
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
            GetGameCoreDBString();
            GetUserCoreDBString();
            if (!IsPostBack)
            {
                //初始化用户基本信息
                InitUserInfo();
                //初始化角色基本信息
                InitRoleInfo();
                //绑定封停时间
                BindLockTime();
            }
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUserSearch_Click(object sender, EventArgs e)
        {
            UserSearch();
        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRoleSearch_Click(object sender, EventArgs e)
        {
            RoleSearch();
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbRoleID.Text = "";
            tbRoleName.Text = "";
            tbUserID.Text = "";
            tbUserName.Text = "";
            InitUserInfo();
            InitRoleInfo();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定封停时间
        /// </summary>
        public void BindLockTime()
        {
            try
            {
                string sql = "SELECT * FROM T_GameConfig WHERE F_ParentID=101001";
                ds = DBHelperGSSDB.Query(sql);
                this.ddlLockTime.DataSource = ds;
                this.ddlLockTime.DataTextField = "F_Name";
                this.ddlLockTime.DataValueField = "F_ID";
                this.ddlLockTime.DataBind();
            }
            catch (System.Exception ex)
            {
            }
        }
        /// <summary>
        /// 初始化用户信息
        /// </summary>
        protected void InitUserInfo()
        {
            StringBuilder sbUserInfo = new StringBuilder("");
            sbUserInfo.Append("<table border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbUserInfo.Append("<tr><th>" + App_GlobalResources.Language.LblUserNo + "</th><th>" + App_GlobalResources.Language.UserName + "</th><th>" + App_GlobalResources.Language.Tip_BigZoneNo + "</th><th>" + App_GlobalResources.Language.Tip_BigZone + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.Tip_IsAdult + "</th><th>" + App_GlobalResources.Language.LblRegisterTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            sbUserInfo.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbUserInfo.Append("</table>");
            this.divUserInfo.InnerHtml = sbUserInfo.ToString();  
        }
        /// <summary>
        /// 初始化角色信息
        /// </summary>
        protected void InitRoleInfo()
        {
            StringBuilder sbRoleInfo = new StringBuilder("");
            sbRoleInfo.Append("<table border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbRoleInfo.Append("<tr><th>" + App_GlobalResources.Language.LblRoleNo + "</th><th>" + App_GlobalResources.Language.LblRoleName + "</th><th>" + App_GlobalResources.Language.LblRoleJob + "</th><th>" + App_GlobalResources.Language.Tip_BattleNo + "</th><th>" + App_GlobalResources.Language.Tip_Battle + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.LblRoleStatue + "</th><th>" + App_GlobalResources.Language.LblRoleLevel + "</th><th>" + App_GlobalResources.Language.Tip_Corps + "</th><th>" + App_GlobalResources.Language.Tip_CorpsPro + "</th><th>" + App_GlobalResources.Language.Lbl_CreateTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            sbRoleInfo.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbRoleInfo.Append("</table>");
            this.divRoleInfo.InnerHtml = sbRoleInfo.ToString();
        }
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        protected string GetUserOnLineState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "로그오프";
                    case "1":
                        return "로그온";
                    case "2":
                        return "로그오프";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "下线";
                    case "1":
                        return "在线";
                    case "2":
                        return "下线";
                    default:
                        return value;
                }
            }
            
        }
        protected string GetUserLockState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "True":
                        return "확인";
                    case "False":
                        return "취소";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "True":
                        return "确认";
                    case "False":
                        return "取消";
                    default:
                        return value;
                }
            }
            
        }
        protected string GetUserProtectState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "No";
                    case "1":
                        return "Yes";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "否";
                    case "1":
                        return "是";
                    default:
                        return value;
                }
            }
           
        }
        protected string GetUserAdultState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "True":
                        return "확인";
                    case "False":
                        return "취소";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "True":
                        return "确定";
                    case "False":
                        return "取消";
                    default:
                        return value;
                }
            }
            
        }
        protected string GetRoleOnLineState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "로그오프";
                    case "1":
                        return "로그온";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "下线";
                    case "1":
                        return "在线";
                    default:
                        return value;
                }
            }
        }
        protected string GetRoleLockState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "취소";
                    case "1":
                        return "확인";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "取消";
                    case "1":
                        return "确认";
                    default:
                        return value;
                }
            }
            
        }
        protected string GetRoleState(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "일반";
                    case "1":
                        return "삭제";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "正常";
                    case "1":
                        return "删除";
                    default:
                        return value;
                }
            }
        }
        public string GetPro(string value)
        {
            try
            {
                if (PageLanguage == "ko-kr")
                {
                    switch (value)
                    {
                        case "1":
                            return "암살자";
                        case "2":
                            return "마법사";
                        case "3":
                            return "용전사";
                        case "4":
                            return "소환사";
                        default:
                            return value;
                    }
                }
                else
                {
                    switch (value)
                    {
                        case "1":
                            return "刺客";
                        case "2":
                            return "魔法师";
                        case "3":
                            return "战士";
                        case "4":
                            return "萝莉";
                        default:
                            return value;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public string GetRankLevel(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "0":
                        return "길드장";
                    case "1":
                        return "부길드장";
                    case "2":
                        return "단장";
                    case "9":
                        return "길드원";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "0":
                        return "会长";
                    case "1":
                        return "副会长";
                    case "2":
                        return "团长";
                    case "9":
                        return "普通成员";
                    default:
                        return value;
                }
            }
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        protected void UserSearch()
        {
            InitUserInfo();
            InitRoleInfo();

            #region 判断搜索条件
            string userID = tbUserID.Text.Trim();
            string userName = tbUserName.Text.Trim();
            if (string.IsNullOrEmpty(userID) && string.IsNullOrEmpty(userName))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteSearch + "');</Script>");
                return;
            }
            #endregion

            #region 获取搜索用户ID
            string sql = "SELECT F_UserID FROM T_User";
            string sqlWhere = " WHERE 1=1";
            if (!string.IsNullOrEmpty(userID))
            {
                sqlWhere += string.Format(" AND F_UserID={0}", userID);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sqlWhere += string.Format(" AND F_UserName=N'{0}'", userName);
            }
            sql += sqlWhere;
            ds = DBHelperUserCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUser + "');</Script>");
                return;
            }
            string searchUserID = ds.Tables[0].Rows[0][0].ToString();
            #endregion

            #region 获取用户信息
            sql = string.Format("SELECT  A.F_UserID, A.F_UserName,F_CreatTime,F_ActiveTime,F_IsProtect,F_IsAdult,F_IsLock,F_Level,F_LastLoginIP,(SELECT COUNT(1) FROM T_UserLoged WITH(NOLOCK) WHERE F_UserID=A.F_UserID ) as F_OnLine, F_PersonID FROM T_User as A WITH(NOLOCK) LEFT JOIN (SELECT * FROM T_UserState0 WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState1  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState2  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState3  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState4  WITH(NOLOCK)) B ON A.F_UserID=B.F_UserID LEFT JOIN T_UserExInfo C ON A.F_UserID=C.F_UserID WHERE 1=1   AND A.F_UserID={0}", searchUserID);
            ds = DBHelperUserCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUser + "');</Script>");
                return;
            }
            StringBuilder sbUserInfo = new StringBuilder("");
            sbUserInfo.Clear();
            sbUserInfo.Append("<table id=\"tableUser\" border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbUserInfo.Append("<tr><th>" + App_GlobalResources.Language.LblUserNo + "</th><th>" + App_GlobalResources.Language.UserName + "</th><th>" + App_GlobalResources.Language.Tip_BigZoneNo + "</th><th>" + App_GlobalResources.Language.Tip_BigZone + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.Tip_IsAdult + "</th><th>" + App_GlobalResources.Language.LblRegisterTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            sbUserInfo.Append(string.Format("<tr onclick=\"tagscheckUser(this);\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>", ds.Tables[0].Rows[0]["F_UserID"].ToString(), ds.Tables[0].Rows[0]["F_UserName"].ToString(), "0", "BigZone", GetUserOnLineState(ds.Tables[0].Rows[0]["F_OnLine"].ToString()), GetUserLockState(ds.Tables[0].Rows[0]["F_IsLock"].ToString()), GetUserAdultState(ds.Tables[0].Rows[0]["F_IsAdult"].ToString()), ds.Tables[0].Rows[0]["F_CreatTime"].ToString(), ds.Tables[0].Rows[0]["F_ActiveTime"].ToString()));
            sbUserInfo.Append("</table>");
            this.divUserInfo.InnerHtml = sbUserInfo.ToString();
            #endregion

            #region 获取角色信息

            #region sql
            sql = string.Format(@"SELECT F_UserID,F_RoleID,F_RoleName,F_ZoneID,(SELECT TOP 1 F_ZoneName FROM T_BattleZone WITH (nolock) WHERE F_ZoneID = a.F_ZoneID) AS F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(SELECT COUNT (1) FROM T_LogInRole_0 WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_OnLine,(SELECT COUNT (1) FROM T_LockRole WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_IsLock,0 AS F_RowState FROM T_RoleBaseData_0 AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={0} UNION ALL
	SELECT
		 F_UserID,
		F_RoleID,
		F_RoleName,
		F_ZoneID,
		(
			SELECT
				TOP 1 F_ZoneName
			FROM
				T_BattleZone WITH (nolock)
			WHERE
				F_ZoneID = a.F_ZoneID
		) AS F_ZoneName,
		F_Level,
		F_CreateTime,
		F_UpdateTime,
		(
			SELECT
				COUNT (1)
			FROM
				T_LogInRole_1 WITH (nolock)
			WHERE
				F_RoleID = a.F_RoleID
		) AS F_OnLine,
		(
			SELECT
				COUNT (1)
			FROM
				T_LockRole WITH (nolock)
			WHERE
				F_RoleID = a.F_RoleID
		) AS F_IsLock,
		0 AS F_RowState
	FROM
		T_RoleBaseData_1 AS a WITH (nolock)
	WHERE
		1 = 1
	AND F_UserID ={1}
	UNION ALL
		SELECT
			 F_UserID,
			F_RoleID,
			F_RoleName,
			F_ZoneID,
			(
				SELECT
					TOP 1 F_ZoneName
				FROM
					T_BattleZone WITH (nolock)
				WHERE
					F_ZoneID = a.F_ZoneID
			) AS F_ZoneName,
			F_Level,
			F_CreateTime,
			F_UpdateTime,
			(
				SELECT
					COUNT (1)
				FROM
					T_LogInRole_2 WITH (nolock)
				WHERE
					F_RoleID = a.F_RoleID
			) AS F_OnLine,
			(
				SELECT
					COUNT (1)
				FROM
					T_LockRole WITH (nolock)
				WHERE
					F_RoleID = a.F_RoleID
			) AS F_IsLock,
			0 AS F_RowState
		FROM
			T_RoleBaseData_2 AS a WITH (nolock)
		WHERE
			1 = 1
		AND F_UserID ={2}
		UNION ALL
			SELECT
				 F_UserID,
				F_RoleID,
				F_RoleName,
				F_ZoneID,
				(
					SELECT
						TOP 1 F_ZoneName
					FROM
						T_BattleZone WITH (nolock)
					WHERE
						F_ZoneID = a.F_ZoneID
				) AS F_ZoneName,
				F_Level,
				F_CreateTime,
				F_UpdateTime,
				(
					SELECT
						COUNT (1)
					FROM
						T_LogInRole_3 WITH (nolock)
					WHERE
						F_RoleID = a.F_RoleID
				) AS F_OnLine,
				(
					SELECT
						COUNT (1)
					FROM
						T_LockRole WITH (nolock)
					WHERE
						F_RoleID = a.F_RoleID
				) AS F_IsLock,
				0 AS F_RowState
			FROM
				T_RoleBaseData_3 AS a WITH (nolock)
			WHERE
				1 = 1
			AND F_UserID ={3}
			UNION ALL
				SELECT
					 F_UserID,
					F_RoleID,
					F_RoleName,
					F_ZoneID,
					(
						SELECT
							TOP 1 F_ZoneName
						FROM
							T_BattleZone WITH (nolock)
						WHERE
							F_ZoneID = a.F_ZoneID
					) AS F_ZoneName,
					F_Level,
					F_CreateTime,
					F_UpdateTime,
					(
						SELECT
							COUNT (1)
						FROM
							T_LogInRole_4 WITH (nolock)
						WHERE
							F_RoleID = a.F_RoleID
					) AS F_OnLine,
					(
						SELECT
							COUNT (1)
						FROM
							T_LockRole WITH (nolock)
						WHERE
							F_RoleID = a.F_RoleID) AS F_IsLock,0 AS F_RowState FROM T_RoleBaseData_4 AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={4} UNION ALL SELECT F_UserID,F_RoleID,F_RoleName,F_ZoneID,(SELECT TOP 1 F_ZoneName FROM T_BattleZone WITH (nolock) WHERE F_ZoneID = a.F_ZoneID) AS F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(SELECT COUNT (1) FROM T_LogInRole_0 WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_OnLine,(SELECT COUNT (1) FROM T_LockRole WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_IsLock,1 AS F_RowState FROM T_RoleBaseDataDeleted AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={5}", searchUserID, searchUserID, searchUserID, searchUserID, searchUserID, searchUserID);
            #endregion

            ds = DBHelperGameCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoRole + "');</Script>");
                return;
            }
            StringBuilder sbRoleInfo = new StringBuilder("");
            sbRoleInfo.Append("<table border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbRoleInfo.Append("<tr><th>" + App_GlobalResources.Language.LblRoleNo + "</th><th>" + App_GlobalResources.Language.LblRoleName + "</th><th>" + App_GlobalResources.Language.LblRoleJob + "</th><th>" + App_GlobalResources.Language.Tip_BattleNo + "</th><th>" + App_GlobalResources.Language.Tip_Battle + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.LblRoleStatue + "</th><th>" + App_GlobalResources.Language.LblRoleLevel + "</th><th>" + App_GlobalResources.Language.Tip_Corps + "</th><th>" + App_GlobalResources.Language.Tip_CorpsPro + "</th><th>" + App_GlobalResources.Language.Lbl_CreateTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "5" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "10" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "15" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "20")//CBT服关闭，不查询CBT服角色信息
                { 
                    //continue;
                    //获取角色职业，所属军团，军团职业
                    string strSql = string.Format("SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_0_" + ds.Tables[0].Rows[i]["F_ZoneID"].ToString() + "], 'SELECT nGlobalID,nPro,A.CorpsId,CorpsName,RankLevel FROM playerbaseinfo A LEFT JOIN corps_baseinfotable B ON A.CorpsId=B.CorpsID LEFT JOIN corps_membertable C ON A.CorpsId=C.CorpsID and C.GlobalID=A.nGlobalID WHERE nGlobalID={0}')", ds.Tables[0].Rows[i]["F_RoleID"].ToString());
                    DataSet dsRoleInfo = DBHelperDigGameDB.Query(strSql);
                    string rolePro = "";
                    string roleCorpsName = "";
                    string roleRankLevel = "";
                    if (dsRoleInfo == null || dsRoleInfo.Tables.Count == 0 || dsRoleInfo.Tables[0].Rows.Count == 0) { }
                    else
                    {
                        rolePro = dsRoleInfo.Tables[0].Rows[0]["nPro"].ToString();
                        if (dsRoleInfo.Tables[0].Rows[0]["CorpsName"].ToString() != "-1")
                        {
                            roleCorpsName = dsRoleInfo.Tables[0].Rows[0]["CorpsName"].ToString();
                            roleRankLevel = dsRoleInfo.Tables[0].Rows[0]["RankLevel"].ToString();
                        }
                    }
                    sbRoleInfo.Append(string.Format("<tr onclick=\"tagscheckRole(this);\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>", ds.Tables[0].Rows[i]["F_RoleID"].ToString(), ds.Tables[0].Rows[i]["F_RoleName"].ToString(), GetPro(rolePro), ds.Tables[0].Rows[i]["F_ZoneID"].ToString(), ds.Tables[0].Rows[i]["F_ZoneName"].ToString(), GetRoleOnLineState(ds.Tables[0].Rows[i]["F_OnLine"].ToString()), GetRoleLockState(ds.Tables[0].Rows[i]["F_IsLock"].ToString()), GetRoleState(ds.Tables[0].Rows[i]["F_RowState"].ToString()), ds.Tables[0].Rows[i]["F_Level"].ToString(), roleCorpsName, GetRankLevel(roleRankLevel), ds.Tables[0].Rows[i]["F_CreateTime"].ToString(), ds.Tables[0].Rows[i]["F_UpdateTime"].ToString()));
                }
            }
            sbRoleInfo.Append("</table>");
            this.divRoleInfo.InnerHtml = sbRoleInfo.ToString();
            #endregion
        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        protected void RoleSearch()
        {
            InitUserInfo();
            InitRoleInfo();

            #region 判断搜索条件
            string roleID = tbRoleID.Text.Trim();
            string roleName = tbRoleName.Text.Trim();
            if (string.IsNullOrEmpty(roleID) && string.IsNullOrEmpty(roleName))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteSearch + "');</Script>");
                return;
            }
            #endregion

            #region 获取搜索用户ID
            string sql = "SELECT F_UserID FROM dbo.T_RoleCreate";
            string sqlWhere = " WHERE 1=1";
            if (!string.IsNullOrEmpty(roleID))
            {
                sqlWhere += string.Format(" AND F_RoleID={0}", roleID);
            }
            if (!string.IsNullOrEmpty(roleName))
            {
                sqlWhere += string.Format(" AND F_RoleName=N'{0}'", roleName);
            }
            sql += sqlWhere;
            ds = DBHelperGameCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUser + "');</Script>");
                return;
            }
            string searchUserID = ds.Tables[0].Rows[0]["F_UserID"].ToString();
            #endregion

            #region 获取用户信息
            sql = string.Format("SELECT  A.F_UserID, A.F_UserName,F_CreatTime,F_ActiveTime,F_IsProtect,F_IsAdult,F_IsLock,F_Level,F_LastLoginIP,(SELECT COUNT(1) FROM T_UserLoged WITH(NOLOCK) WHERE F_UserID=A.F_UserID ) as F_OnLine, F_PersonID FROM T_User as A WITH(NOLOCK) LEFT JOIN (SELECT * FROM T_UserState0 WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState1  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState2  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState3  WITH(NOLOCK) UNION ALL SELECT * FROM T_UserState4  WITH(NOLOCK)) B ON A.F_UserID=B.F_UserID LEFT JOIN T_UserExInfo C ON A.F_UserID=C.F_UserID WHERE 1=1   AND A.F_UserID={0}", searchUserID);
            ds = DBHelperUserCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUser + "');</Script>");
                return;
            }
            StringBuilder sbUserInfo = new StringBuilder("");
            sbUserInfo.Clear();
            sbUserInfo.Append("<table border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbUserInfo.Append("<tr><th>" + App_GlobalResources.Language.LblUserNo + "</th><th>" + App_GlobalResources.Language.UserName + "</th><th>" + App_GlobalResources.Language.Tip_BigZoneNo + "</th><th>" + App_GlobalResources.Language.Tip_BigZone + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.Tip_IsAdult + "</th><th>" + App_GlobalResources.Language.LblRegisterTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            sbUserInfo.Append(string.Format("<tr onclick=\"tagscheckUser(this);\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>", ds.Tables[0].Rows[0]["F_UserID"].ToString(), ds.Tables[0].Rows[0]["F_UserName"].ToString(), "0", "BigZone", GetUserOnLineState(ds.Tables[0].Rows[0]["F_OnLine"].ToString()), GetUserLockState(ds.Tables[0].Rows[0]["F_IsLock"].ToString()), GetUserAdultState(ds.Tables[0].Rows[0]["F_IsAdult"].ToString()), ds.Tables[0].Rows[0]["F_CreatTime"].ToString(), ds.Tables[0].Rows[0]["F_ActiveTime"].ToString()));
            sbUserInfo.Append("</table>");
            this.divUserInfo.InnerHtml = sbUserInfo.ToString();
            #endregion

            #region 获取角色信息

            #region sql
            sql = string.Format(@"SELECT F_UserID,F_RoleID,F_RoleName,F_ZoneID,(SELECT TOP 1 F_ZoneName FROM T_BattleZone WITH (nolock) WHERE F_ZoneID = a.F_ZoneID) AS F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(SELECT COUNT (1) FROM T_LogInRole_0 WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_OnLine,(SELECT COUNT (1) FROM T_LockRole WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_IsLock,0 AS F_RowState FROM T_RoleBaseData_0 AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={0} UNION ALL
	SELECT
		 F_UserID,
		F_RoleID,
		F_RoleName,
		F_ZoneID,
		(
			SELECT
				TOP 1 F_ZoneName
			FROM
				T_BattleZone WITH (nolock)
			WHERE
				F_ZoneID = a.F_ZoneID
		) AS F_ZoneName,
		F_Level,
		F_CreateTime,
		F_UpdateTime,
		(
			SELECT
				COUNT (1)
			FROM
				T_LogInRole_1 WITH (nolock)
			WHERE
				F_RoleID = a.F_RoleID
		) AS F_OnLine,
		(
			SELECT
				COUNT (1)
			FROM
				T_LockRole WITH (nolock)
			WHERE
				F_RoleID = a.F_RoleID
		) AS F_IsLock,
		0 AS F_RowState
	FROM
		T_RoleBaseData_1 AS a WITH (nolock)
	WHERE
		1 = 1
	AND F_UserID ={1}
	UNION ALL
		SELECT
			 F_UserID,
			F_RoleID,
			F_RoleName,
			F_ZoneID,
			(
				SELECT
					TOP 1 F_ZoneName
				FROM
					T_BattleZone WITH (nolock)
				WHERE
					F_ZoneID = a.F_ZoneID
			) AS F_ZoneName,
			F_Level,
			F_CreateTime,
			F_UpdateTime,
			(
				SELECT
					COUNT (1)
				FROM
					T_LogInRole_2 WITH (nolock)
				WHERE
					F_RoleID = a.F_RoleID
			) AS F_OnLine,
			(
				SELECT
					COUNT (1)
				FROM
					T_LockRole WITH (nolock)
				WHERE
					F_RoleID = a.F_RoleID
			) AS F_IsLock,
			0 AS F_RowState
		FROM
			T_RoleBaseData_2 AS a WITH (nolock)
		WHERE
			1 = 1
		AND F_UserID ={2}
		UNION ALL
			SELECT
				 F_UserID,
				F_RoleID,
				F_RoleName,
				F_ZoneID,
				(
					SELECT
						TOP 1 F_ZoneName
					FROM
						T_BattleZone WITH (nolock)
					WHERE
						F_ZoneID = a.F_ZoneID
				) AS F_ZoneName,
				F_Level,
				F_CreateTime,
				F_UpdateTime,
				(
					SELECT
						COUNT (1)
					FROM
						T_LogInRole_3 WITH (nolock)
					WHERE
						F_RoleID = a.F_RoleID
				) AS F_OnLine,
				(
					SELECT
						COUNT (1)
					FROM
						T_LockRole WITH (nolock)
					WHERE
						F_RoleID = a.F_RoleID
				) AS F_IsLock,
				0 AS F_RowState
			FROM
				T_RoleBaseData_3 AS a WITH (nolock)
			WHERE
				1 = 1
			AND F_UserID ={3}
			UNION ALL
				SELECT
					 F_UserID,
					F_RoleID,
					F_RoleName,
					F_ZoneID,
					(
						SELECT
							TOP 1 F_ZoneName
						FROM
							T_BattleZone WITH (nolock)
						WHERE
							F_ZoneID = a.F_ZoneID
					) AS F_ZoneName,
					F_Level,
					F_CreateTime,
					F_UpdateTime,
					(
						SELECT
							COUNT (1)
						FROM
							T_LogInRole_4 WITH (nolock)
						WHERE
							F_RoleID = a.F_RoleID
					) AS F_OnLine,
					(
						SELECT
							COUNT (1)
						FROM
							T_LockRole WITH (nolock)
						WHERE
							F_RoleID = a.F_RoleID) AS F_IsLock,0 AS F_RowState FROM T_RoleBaseData_4 AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={4} UNION ALL SELECT F_UserID,F_RoleID,F_RoleName,F_ZoneID,(SELECT TOP 1 F_ZoneName FROM T_BattleZone WITH (nolock) WHERE F_ZoneID = a.F_ZoneID) AS F_ZoneName,F_Level,F_CreateTime,F_UpdateTime,(SELECT COUNT (1) FROM T_LogInRole_0 WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_OnLine,(SELECT COUNT (1) FROM T_LockRole WITH (nolock) WHERE F_RoleID = a.F_RoleID) AS F_IsLock,1 AS F_RowState FROM T_RoleBaseDataDeleted AS a WITH (nolock) WHERE 1 = 1 AND F_UserID ={5}", searchUserID, searchUserID, searchUserID, searchUserID, searchUserID, searchUserID);
            #endregion

            ds = DBHelperGameCoreDB.Query(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoRole + "');</Script>");
                return;
            }
            StringBuilder sbRoleInfo = new StringBuilder("");
            sbRoleInfo.Append("<table border=\"1\" bordercolor=\"#a0c6e5\" style=\"border-collapse:collapse;width:96%\">");
            sbRoleInfo.Append("<tr><th>" + App_GlobalResources.Language.LblRoleNo + "</th><th>" + App_GlobalResources.Language.LblRoleName + "</th><th>" + App_GlobalResources.Language.LblRoleJob + "</th><th>" + App_GlobalResources.Language.Tip_BattleNo + "</th><th>" + App_GlobalResources.Language.Tip_Battle + "</th><th>" + App_GlobalResources.Language.LblOnlineStatue + "</th><th>" + App_GlobalResources.Language.LblCloseDownStatue + "</th><th>" + App_GlobalResources.Language.LblRoleStatue + "</th><th>" + App_GlobalResources.Language.LblRoleLevel + "</th><th>" + App_GlobalResources.Language.Tip_Corps + "</th><th>" + App_GlobalResources.Language.Tip_CorpsPro + "</th><th>" + App_GlobalResources.Language.Lbl_CreateTime + "</th><th>" + App_GlobalResources.Language.LblLastOnlineTime + "</th></tr>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "5" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "10" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "15" || ds.Tables[0].Rows[i]["F_ZoneID"].ToString() == "20")//CBT服关闭，不查询CBT服角色信息
                {
                    //continue;
                    //获取角色职业，所属军团，军团职业
                    string strSql = string.Format("SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_0_" + ds.Tables[0].Rows[i]["F_ZoneID"].ToString() + "], 'SELECT nGlobalID,nPro,A.CorpsId,CorpsName,RankLevel FROM playerbaseinfo A LEFT JOIN corps_baseinfotable B ON A.CorpsId=B.CorpsID LEFT JOIN corps_membertable C ON A.CorpsId=C.CorpsID and C.GlobalID=A.nGlobalID WHERE nGlobalID={0}')", ds.Tables[0].Rows[i]["F_RoleID"].ToString());
                    DataSet dsRoleInfo = DBHelperDigGameDB.Query(strSql);
                    string rolePro = "";
                    string roleCorpsName = "";
                    string roleRankLevel = "";
                    if (dsRoleInfo == null || dsRoleInfo.Tables.Count == 0 || dsRoleInfo.Tables[0].Rows.Count == 0) { }
                    else
                    {
                        rolePro = dsRoleInfo.Tables[0].Rows[0]["nPro"].ToString();
                        if (dsRoleInfo.Tables[0].Rows[0]["CorpsName"].ToString() != "-1")
                        {
                            roleCorpsName = dsRoleInfo.Tables[0].Rows[0]["CorpsName"].ToString();
                            roleRankLevel = dsRoleInfo.Tables[0].Rows[0]["RankLevel"].ToString();
                        }
                    }
                    sbRoleInfo.Append(string.Format("<tr onclick=\"tagscheckRole(this);\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>", ds.Tables[0].Rows[i]["F_RoleID"].ToString(), ds.Tables[0].Rows[i]["F_RoleName"].ToString(), GetPro(rolePro), ds.Tables[0].Rows[i]["F_ZoneID"].ToString(), ds.Tables[0].Rows[i]["F_ZoneName"].ToString(), GetRoleOnLineState(ds.Tables[0].Rows[i]["F_OnLine"].ToString()), GetRoleLockState(ds.Tables[0].Rows[i]["F_IsLock"].ToString()), GetRoleState(ds.Tables[0].Rows[i]["F_RowState"].ToString()), ds.Tables[0].Rows[i]["F_Level"].ToString(), roleCorpsName, GetRankLevel(roleRankLevel), ds.Tables[0].Rows[i]["F_CreateTime"].ToString(), ds.Tables[0].Rows[i]["F_UpdateTime"].ToString()));
                }
            }
            sbRoleInfo.Append("</table>");
            this.divRoleInfo.InnerHtml = sbRoleInfo.ToString();
            #endregion
        }
        protected void BtnOperation_Click(object sender, EventArgs e)
        {
            string type = hidType.Value;
            switch(type)
            {
                case "1101":
                    LockUser();//封停帐户
                    break;
                case "1102":
                    UnLockUser();//解封账户
                    break;
                case "2201":
                    LockRole();//封停角色
                    break;
                case "2202":
                    UnLockRole();//解封角色
                    break;
                case "2203":
                    DisRoleChatAdd();//禁言角色
                    break;
                case "2213":
                    DisRoleChatDel();//禁言恢复
                    break;
                case "2208":
                    RoleRecovery();//角色恢复
                    break;
            }
        }
        /// <summary>
        /// 封停用户
        /// </summary>
        protected void LockUser()
        {
            string strUserID = hidUserID.Value.Trim();//账号编号
            string strUserName = hidUserName.Value.Trim();//账号名称
            string strLockType = ddlLockTime.SelectedValue;//封停时间类型
            string strBak=tbBak.Text.Trim();

            string sql = string.Format("SELECT F_ID,F_ParentID,F_Name,F_Value,F_IsUsed,F_Sort FROM T_GameConfig where F_ID={0}", strLockType);
            ds = DBHelperGSSDB.Query(sql);
            string strLockTimeValue = ds.Tables[0].Rows[0]["F_Value"].ToString();//封停时间值

            try
            {
                DateTime lockStartTime = DateTime.Now;//定义封停开始时间
                DateTime lockEndTime = lockStartTime;//定义封停结束时间
                lockEndTime = lockEndTime.AddHours(Convert.ToInt32(strLockTimeValue));//计算封停结束时间

                int result = 0;

                SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int),
					new SqlParameter("@F_UserName", SqlDbType.NChar),
					new SqlParameter("@F_LockType", SqlDbType.Int),
					new SqlParameter("@F_LockStartTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_LockEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Result", SqlDbType.Char,26)};
                parameters[0].Value = Convert.ToInt32(strUserID);
                parameters[1].Value = strUserName;
                parameters[2].Value = LockTypeIntFromStr(strLockType);
                parameters[3].Value = lockStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                parameters[4].Value = lockEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                parameters[5].Direction = ParameterDirection.Output;
                DBHelperUserCoreDB.RunProcedure("_WSS_User_LockTime", parameters, "tableName");

                result = Convert.ToInt32(parameters[5].Value);

                if (result == 0)
                {
                    sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_LockStartTime],[F_LockEndTime],[F_Bak], [F_User], [F_CreateTime]) VALUES (N'账号封停', N'封停', {0}, N'{1}',N'{2}','{3}',N'{4}',N'{5}', GETDATE())", strUserID, strUserName, lockStartTime.ToString("yyyy-MM-dd HH:mm:ss"), lockEndTime.ToString("yyyy-MM-dd HH:mm:ss"), strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                    //UserSearch();
                }
                else if (result == 2015)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_UserIsLock + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解封用户
        /// </summary>
        protected void UnLockUser()
        {
            try
            {
                string strUserID = hidUserID.Value.Trim();//账号编号
                string strUserName = hidUserName.Value.Trim();//账号名称
                string strBak = tbBak.Text.Trim();

                SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                parameters[0].Value = strUserID;
                parameters[1].Direction = ParameterDirection.Output;
                DBHelperUserCoreDB.RunProcedure("_WSS_User_LockDel", parameters, "tableName");

                int result = Convert.ToInt32(parameters[1].Value);
                if (result == 0)
                {
                    string sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_Bak] ,[F_User], [F_CreateTime]) VALUES (N'账号解封', N'解封', {0}, N'{1}',N'{2}',N'{3}', GETDATE())", strUserID, strUserName, strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 封停角色
        /// </summary>
        protected void LockRole()
        {
            string strRoleID = hidRoleID.Value.Trim();//角色编号
            string strRoleName = hidRoleName.Value.Trim();//角色名称
            string strLockType = ddlLockTime.SelectedValue;//封停时间类型
            string strBak = tbBak.Text.Trim();

            string sql = string.Format("SELECT F_ID,F_ParentID,F_Name,F_Value,F_IsUsed,F_Sort FROM T_GameConfig where F_ID={0}", strLockType);
            ds = DBHelperGSSDB.Query(sql);
            string strLockTimeValue = ds.Tables[0].Rows[0]["F_Value"].ToString();//封停时间值

            try
            {
                DateTime lockStartTime = DateTime.Now;//定义封停开始时间
                DateTime lockEndTime = lockStartTime;//定义封停结束时间
                lockEndTime = lockEndTime.AddHours(Convert.ToInt32(strLockTimeValue));//计算封停结束时间

                int result = 0;
                TimeSpan ts = lockEndTime - lockStartTime;
                int LockMinute = Convert.ToInt32(ts.TotalMinutes);

                SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,10),
					new SqlParameter("@F_RoleName", SqlDbType.NChar,20),
					new SqlParameter("@F_LockType", SqlDbType.Int),
					new SqlParameter("@LockMinute", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                parameters[0].Value = strRoleID;
                parameters[1].Value = strRoleName;
                parameters[2].Value = LockTypeIntFromStr(strLockType);
                parameters[3].Value = LockMinute;
                parameters[4].Direction = ParameterDirection.Output;
                DBHelperGameCoreDB.RunProcedure("_DBIS_Role_LockAdd", parameters, "tableName");

                result = Convert.ToInt32(parameters[4].Value);
                if (result == 0)
                {
                    sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_LockStartTime],[F_LockEndTime],[F_Bak] ,[F_User], [F_CreateTime]) VALUES (N'角色封停', N'封停', {0}, N'{1}',N'{2}','{3}',N'{4}',N'{5}', GETDATE())", strRoleID, strRoleName, lockStartTime.ToString("yyyy-MM-dd HH:mm:ss"), lockEndTime.ToString("yyyy-MM-dd HH:mm:ss"), strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);

                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else if (result == 1013)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_RoleIsLock + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 角色解封
        /// </summary>
        protected void UnLockRole()
        {
            try
            {
                string strRoleID = hidRoleID.Value.Trim();//角色编号
                string strRoleName = hidRoleName.Value.Trim();//角色名称
                string strBak = tbBak.Text.Trim();

                SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,10),
					new SqlParameter("@Result", SqlDbType.SmallInt)};
                parameters[0].Value = strRoleID;
                parameters[1].Direction = ParameterDirection.Output;
                DBHelperGameCoreDB.RunProcedure("_DBIS_Role_LockDel", parameters, "tableName");

                int result = Convert.ToInt32(parameters[1].Value);
                if (result == 0)
                {
                    string sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_Bak] ,[F_User], [F_CreateTime]) VALUES (N'角色解封', N'解封', {0}, N'{1}',N'{2}',N'{3}', GETDATE())", strRoleID, strRoleName,strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);

                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 禁言角色
        /// </summary>
        protected void DisRoleChatAdd()
        {
            try
            {
                string strRoleID = hidRoleID.Value.Trim();//角色编号
                string strRoleName = hidRoleName.Value.Trim();//角色名称
                string strLockType = ddlLockTime.SelectedValue;//封停时间类型
                string strBak = tbBak.Text.Trim();

                string sql = string.Format("SELECT [F_RoleID],[F_RoleName],[F_UserID] FROM [dbo].[T_RoleCreate] WHERE [F_RoleID]={0}", strRoleID);
                ds = DBHelperGameCoreDB.Query(sql);
                string strUserID = ds.Tables[0].Rows[0]["F_UserID"].ToString();

                sql = string.Format("SELECT F_ID,F_ParentID,F_Name,F_Value,F_IsUsed,F_Sort FROM T_GameConfig where F_ID={0}", strLockType);
                ds = DBHelperGSSDB.Query(sql);
                string strLockTimeValue = ds.Tables[0].Rows[0]["F_Value"].ToString();//封停时间值

                DateTime lockStartTime = DateTime.Now;//定义封停开始时间
                DateTime lockEndTime = lockStartTime;//定义封停结束时间
                lockEndTime = lockEndTime.AddHours(Convert.ToInt32(strLockTimeValue));//计算封停结束时间

                int result = 0;
                TimeSpan ts = lockEndTime - lockStartTime;
                int LockMinute = Convert.ToInt32(ts.TotalMinutes);

                SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Role_ForbidChatBit",SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@LockMinute", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.SmallInt,4)};
                parameters[0].Value = strUserID;
                parameters[1].Value = 4;
                parameters[2].Value = strRoleID;
                parameters[3].Value = LockMinute;
                parameters[4].Direction = ParameterDirection.Output;
                DBHelperGameCoreDB.RunProcedure("_DBIS_Role_DisChatAdd", parameters, "tableName");

                result = Convert.ToInt32(parameters[4].Value);
                if (result == 0)
                {
                    sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_LockStartTime],[F_LockEndTime],[F_Bak], [F_User], [F_CreateTime]) VALUES (N'角色禁言', N'禁言', {0}, N'{1}',N'{2}','{3}',N'{4}',N'{5}', GETDATE())", strRoleID, strRoleName, lockStartTime.ToString("yyyy-MM-dd HH:mm:ss"), lockEndTime.ToString("yyyy-MM-dd HH:mm:ss"), strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);

                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 禁言恢复
        /// </summary>
        protected void DisRoleChatDel()
        {
            try
            {
                string strRoleID = hidRoleID.Value.Trim();//角色编号
                string strRoleName = hidRoleName.Value.Trim();//角色名称
                string strBak = tbBak.Text.Trim();

                string sql = string.Format("SELECT [F_RoleID],[F_RoleName],[F_UserID] FROM [dbo].[T_RoleCreate] WHERE [F_RoleID]={0}", strRoleID);
                ds = DBHelperGameCoreDB.Query(sql);
                string strUserID = ds.Tables[0].Rows[0]["F_UserID"].ToString();

                int result = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.SmallInt,4)};
                parameters[0].Value = strUserID;
                parameters[1].Value = strRoleID;
                parameters[2].Direction = ParameterDirection.Output;
                DBHelperGameCoreDB.RunProcedure("_DBIS_Role_DisChatDel", parameters, "tableName");

                result = Convert.ToInt32(parameters[2].Value);
                if (result == 0)
                {
                    sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_Bak] ,[F_User], [F_CreateTime]) VALUES (N'禁言恢复', N'禁言恢复', {0}, N'{1}',N'{2}',N'{3}', GETDATE())", strRoleID, strRoleName, strBak, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);

                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 角色恢复
        /// </summary>
        protected void RoleRecovery()
        {
            string strRoleID = hidRoleID.Value.Trim();//角色编号
            string strRoleName = hidRoleName.Value.Trim();//角色名称
            string strBak = tbBak.Text.Trim();

            string sql = string.Format("SELECT F_RoleID,F_UserID,F_ZoneID FROM [dbo].[T_RoleBaseDataDeleted] WHERE F_RoleID={0}", strRoleID);
            ds = DBHelperGameCoreDB.Query(sql);
            if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_RoleBattleZoneError + "');</Script>");
                return;
            }
            string strUserID = ds.Tables[0].Rows[0]["F_UserID"].ToString();
            string strBattleZone = ds.Tables[0].Rows[0]["F_ZoneID"].ToString();

            SqlParameter[] parameters = {
					new SqlParameter("@ZoneID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.BigInt,4),
                    new SqlParameter("@Result", SqlDbType.SmallInt,4)};
            parameters[0].Value = strBattleZone;
            parameters[1].Value = strUserID;
            parameters[2].Value = strRoleID;
            parameters[3].Direction = ParameterDirection.Output;

            DBHelperGameCoreDB.RunProcedure("_WSS_Role_Recover", parameters, "tableName");
            int result = Convert.ToInt32(parameters[3].Value);
            if(result==1)
            {
                Response.Write("<Script Language=JavaScript>alert('当前账号下已经存在4个角色！');</Script>");
                return;
            }
            else if(result==1801)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                return;
            }
            else if(result==1800)
            {
                Response.Write("<Script Language=JavaScript>alert('删除信息中无此角色！');</Script>");
                return;
            }
            //读取MySQL匹配的连接串
            string linkName = string.Format("LKSV_6_ZoneRoleDataDB_0_{0}", strBattleZone);
            string query = string.Format("select provider_string from sys.servers  where name='{0}'", linkName);
            
            ds = DBHelperDigGameDB.Query(query);
            if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
            {
                Response.Write("<Script Language=JavaScript>alert('"+ App_GlobalResources.Language.Tip_ConnectionDBError+"');</Script>");
                return;
            }
            string connString = ds.Tables[0].Rows[0]["provider_string"].ToString();
            
            //需要进行特殊项过滤： DRIVER,OPTION,STMT
            string filter = FilterDBConnString(connString);
            DbHelperSQLP dbHelperMySQL = new DbHelperSQLP();
            dbHelperMySQL.connectionString = filter;
            MySqlParameter[] param = new MySqlParameter[]{
                new MySqlParameter("ROLEID",MySqlDbType.Int32){Value=strRoleID},
                new MySqlParameter("USERID",MySqlDbType.Int32){Value=strUserID},
                new MySqlParameter("OverRoleID",MySqlDbType.Int32){Value=strRoleID},
                new MySqlParameter("OverUserID",MySqlDbType.Int32){Value=strUserID},
                new MySqlParameter("CODE",MySqlDbType.Int32)
            };
            param[param.Length - 1].Direction = ParameterDirection.Output;
            dbHelperMySQL.RunProcedureMysql("TakeRoleData", param, "tableName");
            object outd = param[param.Length - 1].Value;
            if (outd == null)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                return;
            }

            sql = string.Format("INSERT INTO [DigGameDB].[dbo].[T_OpLog] ([F_Module], [F_OPID], [F_LockID],[F_LockName],[F_Bak] ,[F_User], [F_CreateTime]) VALUES (N'角色恢复', N'角色恢复', {0}, N'{1}',N'{2}',N'{3}', GETDATE())", strRoleID, strRoleName, strBak, Session["LoginUser"].ToString());
            DBHelperDigGameDB.ExecuteSql(sql);
            Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");

            //Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
         
        }
        private int LockTypeIntFromStr(string value)
        {
            try
            {
                value = value.Substring(value.Length - 3, 3);
                return Convert.ToInt32(value);
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        protected static string FilterDBConnString(string dbConstring)
        {
            string[] filter = new string[] { "driver", "option", "stmt","port" };
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
        protected string GetContent(string str)
        {
            switch(str)
            {
                case "userID":
                    return App_GlobalResources.Language.LblUserNo;
                case "userName":
                    return App_GlobalResources.Language.UserName;
                case "roleID":
                    return App_GlobalResources.Language.LblRoleNo;
                case "roleName":
                    return App_GlobalResources.Language.LblRoleName;
                case "lockTool":
                    return App_GlobalResources.Language.LblLockTool;
                case "unLockTool":
                    return App_GlobalResources.Language.LblUnLockTool;
                case "selectLockUser":
                    return App_GlobalResources.Language.LblSelectLockUser;
                case "selectUnLockUser":
                    return App_GlobalResources.Language.LblSelectUnLockUser;
                case "selectLockRole":
                    return App_GlobalResources.Language.LblSelectLockRole;
                case "selectUnLockRole":
                    return App_GlobalResources.Language.LblSelectUnLockRole;
                case "selectDisRoleChatAdd":
                    return App_GlobalResources.Language.LblSelectDisRoleChatAdd;
                case "selectDisRoleChatDel":
                    return App_GlobalResources.Language.LblSelectDisRoleChatDel;
                case "selectRoleRecovery":
                    return App_GlobalResources.Language.LblSelectRoleRecovery;
                default:
                    return str;
            }
         
        }
        #endregion
    }
}
