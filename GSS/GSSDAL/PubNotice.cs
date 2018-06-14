using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
	/// <summary>
	/// ���ݷ�����:PubNotice
	/// </summary>
	public partial class PubNotice
	{
		public PubNotice()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_ID", "T_PubNotice"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_PubNotice");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(GSSModel.PubNotice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_PubNotice(");
			strSql.Append("F_Title,F_Note,F_DateTime)");
			strSql.Append(" values (");
			strSql.Append("@F_Title,@F_Note,@F_DateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,500),
					new SqlParameter("@F_DateTime", SqlDbType.SmallDateTime)};
			parameters[0].Value = model.F_Title;
			parameters[1].Value = model.F_Note;
			parameters[2].Value = model.F_DateTime;

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
		public bool Update(GSSModel.PubNotice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_PubNotice set ");
			strSql.Append("F_Title=@F_Title,");
			strSql.Append("F_Note=@F_Note,");
			strSql.Append("F_DateTime=@F_DateTime");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,500),
					new SqlParameter("@F_DateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_Title;
			parameters[1].Value = model.F_Note;
			parameters[2].Value = model.F_DateTime;
			parameters[3].Value = model.F_ID;

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
		public bool Delete(int F_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_PubNotice ");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

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
		public bool DeleteList(string F_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_PubNotice ");
			strSql.Append(" where F_ID in ("+F_IDlist + ")  ");
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
		public GSSModel.PubNotice GetModel(int F_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_ID,F_Title,F_Note,F_DateTime from T_PubNotice ");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

			GSSModel.PubNotice model=new GSSModel.PubNotice();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_ID"].ToString()!="")
				{
					model.F_ID=int.Parse(ds.Tables[0].Rows[0]["F_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Title"]!=null)
				{
				model.F_Title=ds.Tables[0].Rows[0]["F_Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_Note"]!=null)
				{
				model.F_Note=ds.Tables[0].Rows[0]["F_Note"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_DateTime"].ToString()!="")
				{
					model.F_DateTime=DateTime.Parse(ds.Tables[0].Rows[0]["F_DateTime"].ToString());
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
			strSql.Append("select F_ID,F_Title,F_Note,F_DateTime ");
			strSql.Append(" FROM T_PubNotice ");
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
			strSql.Append(" F_ID,F_Title,F_Note,F_DateTime ");
			strSql.Append(" FROM T_PubNotice ");
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
			parameters[0].Value = "T_PubNotice";
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

