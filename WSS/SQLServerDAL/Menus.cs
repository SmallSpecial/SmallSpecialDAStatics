using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.IDAL;
using WSS.DBUtility;//Please add references
namespace WSS.SQLServerDAL
{
	/// <summary>
	/// ���ݷ�����:Menus
	/// </summary>
	public partial class Menus:IMenus
	{
		public Menus()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_MenuID", "T_Menus"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int F_MenuID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Menus");
			strSql.Append(" where F_MenuID=@F_MenuID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)
};
			parameters[0].Value = F_MenuID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(WSS.Model.Menus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Menus(");
			strSql.Append("F_Name,F_ParentID,F_IsUsed)");
			strSql.Append(" values (");
			strSql.Append("@F_Name,@F_ParentID,@F_IsUsed)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Name", SqlDbType.NVarChar,50),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1)};
			parameters[0].Value = model.F_Name;
			parameters[1].Value = model.F_ParentID;
			parameters[2].Value = model.F_IsUsed;

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
		public bool Update(WSS.Model.Menus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Menus set ");
			strSql.Append("F_Name=@F_Name,");
			strSql.Append("F_ParentID=@F_ParentID,");
			strSql.Append("F_IsUsed=@F_IsUsed");
			strSql.Append(" where F_MenuID=@F_MenuID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Name", SqlDbType.NVarChar,50),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_Name;
			parameters[1].Value = model.F_ParentID;
			parameters[2].Value = model.F_IsUsed;
			parameters[3].Value = model.F_MenuID;

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
		public bool Delete(int F_MenuID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Menus ");
			strSql.Append(" where F_MenuID=@F_MenuID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)
};
			parameters[0].Value = F_MenuID;

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
		public bool DeleteList(string F_MenuIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Menus ");
			strSql.Append(" where F_MenuID in ("+F_MenuIDlist + ")  ");
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
		public WSS.Model.Menus GetModel(int F_MenuID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_MenuID,F_Name,F_ParentID,F_IsUsed from T_Menus ");
			strSql.Append(" where F_MenuID=@F_MenuID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_MenuID", SqlDbType.Int,4)
};
			parameters[0].Value = F_MenuID;

			WSS.Model.Menus model=new WSS.Model.Menus();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_MenuID"].ToString()!="")
				{
					model.F_MenuID=int.Parse(ds.Tables[0].Rows[0]["F_MenuID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Name"]!=null)
				{
				model.F_Name=ds.Tables[0].Rows[0]["F_Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_ParentID"].ToString()!="")
				{
					model.F_ParentID=int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_MenuID,F_Name,F_ParentID,F_IsUsed ");
			strSql.Append(" FROM T_Menus ");
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
			strSql.Append(" F_MenuID,F_Name,F_ParentID,F_IsUsed ");
			strSql.Append(" FROM T_Menus ");
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
			parameters[0].Value = "T_Menus";
			parameters[1].Value = "F_MenuID";
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

