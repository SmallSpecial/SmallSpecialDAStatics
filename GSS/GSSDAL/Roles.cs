using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
	/// <summary>
	/// 数据访问类:Roles
	/// </summary>
	public partial class Roles
	{
		public Roles()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_RoleID", "T_Roles"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_RoleID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Roles");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)
};
			parameters[0].Value = F_RoleID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(GSSModel.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Roles(");
			strSql.Append("F_RoleName,F_IsUsed,F_Power)");
			strSql.Append(" values (");
			strSql.Append("@F_RoleName,@F_IsUsed,@F_Power)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Power", SqlDbType.NVarChar)};
			parameters[0].Value = model.F_RoleName;
			parameters[1].Value = model.F_IsUsed;
			parameters[2].Value = model.F_Power;

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
		public bool Update(GSSModel.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Roles set ");
			strSql.Append("F_RoleName=@F_RoleName,");
			strSql.Append("F_IsUsed=@F_IsUsed,");
			strSql.Append("F_Power=@F_Power");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleName", SqlDbType.NVarChar,16),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Power", SqlDbType.NVarChar),//2017-08-29  存储的权限ID列表不做长度限制
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_RoleName;
			parameters[1].Value = model.F_IsUsed;
			parameters[2].Value = model.F_Power;
			parameters[3].Value = model.F_RoleID;

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
		public bool Delete(int F_RoleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Roles ");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)
};
			parameters[0].Value = F_RoleID;

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
		public bool DeleteList(string F_RoleIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Roles ");
			strSql.Append(" where F_RoleID in ("+F_RoleIDlist + ")  ");
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
		public GSSModel.Roles GetModel(int F_RoleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_RoleID,F_RoleName,F_IsUsed,F_Power from T_Roles ");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)
};
			parameters[0].Value = F_RoleID;

			GSSModel.Roles model=new GSSModel.Roles();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_RoleID"].ToString()!="")
				{
					model.F_RoleID=int.Parse(ds.Tables[0].Rows[0]["F_RoleID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_RoleName"]!=null)
				{
				model.F_RoleName=ds.Tables[0].Rows[0]["F_RoleName"].ToString();
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
				if(ds.Tables[0].Rows[0]["F_Power"]!=null)
				{
				model.F_Power=ds.Tables[0].Rows[0]["F_Power"].ToString();
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
			strSql.Append("select F_RoleID,F_RoleName,F_IsUsed,F_Power ");
			strSql.Append(" FROM T_Roles ");
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
			strSql.Append(" F_RoleID,F_RoleName,F_IsUsed,F_Power ");
			strSql.Append(" FROM T_Roles ");
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
			parameters[0].Value = "T_Roles";
			parameters[1].Value = "F_RoleID";
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

