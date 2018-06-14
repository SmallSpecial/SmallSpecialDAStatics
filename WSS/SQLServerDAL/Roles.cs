using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.IDAL;
using WSS.DBUtility;//Please add references
namespace WSS.SQLServerDAL
{
	/// <summary>
	/// ���ݷ�����:Roles
	/// </summary>
	public partial class Roles:IRoles
	{
		public Roles()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_RoleID", "T_Roles"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
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
		/// ����һ������
		/// </summary>
		public int Add(WSS.Model.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Roles(");
			strSql.Append("F_IsUsed,F_Power)");
			strSql.Append(" values (");
			strSql.Append("@F_IsUsed,@F_Power)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Power", SqlDbType.NChar,300)};
			parameters[0].Value = model.F_IsUsed;
			parameters[1].Value = model.F_Power;

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
		/// ����һ������
		/// </summary>
		public bool Update(WSS.Model.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Roles set ");
			strSql.Append("F_IsUsed=@F_IsUsed,");
			strSql.Append("F_Power=@F_Power");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Power", SqlDbType.NChar,300),
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_IsUsed;
			parameters[1].Value = model.F_Power;
			parameters[2].Value = model.F_RoleID;

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
		/// ɾ��һ������
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
		/// ɾ��һ������
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
		/// �õ�һ������ʵ��
		/// </summary>
		public WSS.Model.Roles GetModel(int F_RoleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_RoleID,F_IsUsed,F_Power from T_Roles ");
			strSql.Append(" where F_RoleID=@F_RoleID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_RoleID", SqlDbType.Int,4)
};
			parameters[0].Value = F_RoleID;

			WSS.Model.Roles model=new WSS.Model.Roles();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_RoleID"].ToString()!="")
				{
					model.F_RoleID=int.Parse(ds.Tables[0].Rows[0]["F_RoleID"].ToString());
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_RoleID,F_IsUsed,F_Power ");
			strSql.Append(" FROM T_Roles ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" F_RoleID,F_IsUsed,F_Power ");
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
		/// ��ҳ��ȡ�����б�
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

