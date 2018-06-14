using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.IDAL;
using WSS.DBUtility;//Please add references
namespace WSS.SQLServerDAL
{
	/// <summary>
	/// 数据访问类:Users
	/// </summary>
	public partial class Users:IUsers
	{
		public Users()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_UserID", "T_Users"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Users");
			strSql.Append(" where F_UserID=@F_UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,4)
};
			parameters[0].Value = F_UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WSS.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Users(");
			strSql.Append("F_UserName,F_PassWord,F_DepartID,F_RoleID,F_Sex,F_Birthday,F_Email,F_MobilePhone,F_RegTime,F_LastInTime,F_IsUsed)");
			strSql.Append(" values (");
			strSql.Append("@F_UserName,@F_PassWord,@F_DepartID,@F_RoleID,@F_Sex,@F_Birthday,@F_Email,@F_MobilePhone,@F_RegTime,@F_LastInTime,@F_IsUsed)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserName", SqlDbType.NChar,16),
					new SqlParameter("@F_PassWord", SqlDbType.Char,32),
					new SqlParameter("@F_DepartID", SqlDbType.Int,4),
					new SqlParameter("@F_RoleID", SqlDbType.Int,4),
					new SqlParameter("@F_Sex", SqlDbType.Bit,1),
					new SqlParameter("@F_Birthday", SqlDbType.SmallDateTime),
					new SqlParameter("@F_Email", SqlDbType.NChar,50),
					new SqlParameter("@F_MobilePhone", SqlDbType.Char,13),
					new SqlParameter("@F_RegTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_LastInTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1)};
			parameters[0].Value = model.F_UserName;
			parameters[1].Value = model.F_PassWord;
			parameters[2].Value = model.F_DepartID;
			parameters[3].Value = model.F_RoleID;
			parameters[4].Value = model.F_Sex;
			parameters[5].Value = model.F_Birthday;
			parameters[6].Value = model.F_Email;
			parameters[7].Value = model.F_MobilePhone;
			parameters[8].Value = model.F_RegTime;
			parameters[9].Value = model.F_LastInTime;
			parameters[10].Value = model.F_IsUsed;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(WSS.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Users set ");
			strSql.Append("F_UserName=@F_UserName,");
			strSql.Append("F_PassWord=@F_PassWord,");
			strSql.Append("F_DepartID=@F_DepartID,");
			strSql.Append("F_RoleID=@F_RoleID,");
			strSql.Append("F_Sex=@F_Sex,");
			strSql.Append("F_Birthday=@F_Birthday,");
			strSql.Append("F_Email=@F_Email,");
			strSql.Append("F_MobilePhone=@F_MobilePhone,");
			strSql.Append("F_RegTime=@F_RegTime,");
			strSql.Append("F_LastInTime=@F_LastInTime,");
			strSql.Append("F_IsUsed=@F_IsUsed");
			strSql.Append(" where F_UserID=@F_UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserName", SqlDbType.NChar,16),
					new SqlParameter("@F_PassWord", SqlDbType.Char,32),
					new SqlParameter("@F_DepartID", SqlDbType.Int,4),
					new SqlParameter("@F_RoleID", SqlDbType.Int,4),
					new SqlParameter("@F_Sex", SqlDbType.Bit,1),
					new SqlParameter("@F_Birthday", SqlDbType.SmallDateTime),
					new SqlParameter("@F_Email", SqlDbType.NChar,50),
					new SqlParameter("@F_MobilePhone", SqlDbType.Char,13),
					new SqlParameter("@F_RegTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_LastInTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_UserID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_UserName;
			parameters[1].Value = model.F_PassWord;
			parameters[2].Value = model.F_DepartID;
			parameters[3].Value = model.F_RoleID;
			parameters[4].Value = model.F_Sex;
			parameters[5].Value = model.F_Birthday;
			parameters[6].Value = model.F_Email;
			parameters[7].Value = model.F_MobilePhone;
			parameters[8].Value = model.F_RegTime;
			parameters[9].Value = model.F_LastInTime;
			parameters[10].Value = model.F_IsUsed;
			parameters[11].Value = model.F_UserID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int F_UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Users ");
			strSql.Append(" where F_UserID=@F_UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,4)
};
			parameters[0].Value = F_UserID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string F_UserIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Users ");
			strSql.Append(" where F_UserID in ("+F_UserIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public WSS.Model.Users GetModel(int F_UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_UserID,F_UserName,F_PassWord,F_DepartID,F_RoleID,F_Sex,F_Birthday,F_Email,F_MobilePhone,F_RegTime,F_LastInTime,F_IsUsed from T_Users ");
			strSql.Append(" where F_UserID=@F_UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,4)
};
			parameters[0].Value = F_UserID;

			WSS.Model.Users model=new WSS.Model.Users();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_UserID"].ToString()!="")
				{
					model.F_UserID=int.Parse(ds.Tables[0].Rows[0]["F_UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_UserName"]!=null)
				{
				model.F_UserName=ds.Tables[0].Rows[0]["F_UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_PassWord"]!=null)
				{
				model.F_PassWord=ds.Tables[0].Rows[0]["F_PassWord"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_DepartID"].ToString()!="")
				{
					model.F_DepartID=int.Parse(ds.Tables[0].Rows[0]["F_DepartID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_RoleID"].ToString()!="")
				{
					model.F_RoleID=int.Parse(ds.Tables[0].Rows[0]["F_RoleID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Sex"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["F_Sex"].ToString()=="1")||(ds.Tables[0].Rows[0]["F_Sex"].ToString().ToLower()=="true"))
					{
						model.F_Sex=true;
					}
					else
					{
						model.F_Sex=false;
					}
				}
				if(ds.Tables[0].Rows[0]["F_Birthday"].ToString()!="")
				{
					model.F_Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["F_Birthday"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Email"]!=null)
				{
				model.F_Email=ds.Tables[0].Rows[0]["F_Email"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_MobilePhone"]!=null)
				{
				model.F_MobilePhone=ds.Tables[0].Rows[0]["F_MobilePhone"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_RegTime"].ToString()!="")
				{
					model.F_RegTime=DateTime.Parse(ds.Tables[0].Rows[0]["F_RegTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_LastInTime"].ToString()!="")
				{
					model.F_LastInTime=DateTime.Parse(ds.Tables[0].Rows[0]["F_LastInTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_IsUsed"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["F_IsUsed"].ToString()=="1")||(ds.Tables[0].Rows[0]["F_IsUsed"].ToString().ToLower()=="true"))
					{
						model.F_IsUsed=true;
					}
					else
					{
						model.F_IsUsed=false;
					}
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_UserID,F_UserName,F_PassWord,F_DepartID,F_RoleID,F_Sex,F_Birthday,F_Email,F_MobilePhone,F_RegTime,F_LastInTime,F_IsUsed ");
			strSql.Append(" FROM T_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" F_UserID,F_UserName,F_PassWord,F_DepartID,F_RoleID,F_Sex,F_Birthday,F_Email,F_MobilePhone,F_RegTime,F_LastInTime,F_IsUsed ");
			strSql.Append(" FROM T_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "T_Users";
			parameters[1].Value = "F_UserID";
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

