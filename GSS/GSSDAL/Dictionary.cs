using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
	/// <summary>
	/// ���ݷ�����:Dictionary
	/// </summary>
	public partial class Dictionary
	{
		public Dictionary()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_DicID", "T_Dictionary"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int F_DicID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Dictionary");
			strSql.Append(" where F_DicID=@F_DicID ");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DicID", SqlDbType.Int,4)};
			parameters[0].Value = F_DicID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(GSSModel.Dictionary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Dictionary(");
			strSql.Append("F_DicID,F_ParentID,F_Value,F_IsUsed,F_Sort)");
			strSql.Append(" values (");
			strSql.Append("@F_DicID,@F_ParentID,@F_Value,@F_IsUsed,@F_Sort)");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DicID", SqlDbType.Int,4),
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_Value", SqlDbType.NVarChar,50),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.F_DicID;
			parameters[1].Value = model.F_ParentID;
			parameters[2].Value = model.F_Value;
			parameters[3].Value = model.F_IsUsed;
			parameters[4].Value = model.F_Sort;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(GSSModel.Dictionary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Dictionary set ");
			strSql.Append("F_ParentID=@F_ParentID,");
			strSql.Append("F_Value=@F_Value,");
			strSql.Append("F_IsUsed=@F_IsUsed,");
			strSql.Append("F_Sort=@F_Sort");
			strSql.Append(" where F_DicID=@F_DicID ");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_Value", SqlDbType.NVarChar,50),
					new SqlParameter("@F_IsUsed", SqlDbType.Bit,1),
					new SqlParameter("@F_Sort", SqlDbType.Int,4),
					new SqlParameter("@F_DicID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_ParentID;
			parameters[1].Value = model.F_Value;
			parameters[2].Value = model.F_IsUsed;
			parameters[3].Value = model.F_Sort;
			parameters[4].Value = model.F_DicID;

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
		public bool Delete(int F_DicID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Dictionary ");
			strSql.Append(" where F_DicID=@F_DicID ");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DicID", SqlDbType.Int,4)};
			parameters[0].Value = F_DicID;

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
		public bool DeleteList(string F_DicIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Dictionary ");
			strSql.Append(" where F_DicID in ("+F_DicIDlist + ")  ");
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
		public GSSModel.Dictionary GetModel(int F_DicID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_DicID,F_ParentID,F_Value,F_IsUsed,F_Sort from T_Dictionary ");
			strSql.Append(" where F_DicID=@F_DicID ");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DicID", SqlDbType.Int,4)};
			parameters[0].Value = F_DicID;

			GSSModel.Dictionary model=new GSSModel.Dictionary();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_DicID"].ToString()!="")
				{
					model.F_DicID=int.Parse(ds.Tables[0].Rows[0]["F_DicID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_ParentID"].ToString()!="")
				{
					model.F_ParentID=int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Value"]!=null)
				{
				model.F_Value=ds.Tables[0].Rows[0]["F_Value"].ToString();
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
				if(ds.Tables[0].Rows[0]["F_Sort"].ToString()!="")
				{
					model.F_Sort=int.Parse(ds.Tables[0].Rows[0]["F_Sort"].ToString());
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
			strSql.Append("select F_DicID,F_ParentID,F_Value,F_IsUsed,F_Sort ");
			strSql.Append(" FROM T_Dictionary ");
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
			strSql.Append(" F_DicID,F_ParentID,F_Value,F_IsUsed,F_Sort ");
			strSql.Append(" FROM T_Dictionary ");
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
			parameters[0].Value = "T_Dictionary";
			parameters[1].Value = "F_DicID";
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

