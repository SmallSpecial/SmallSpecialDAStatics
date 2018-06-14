using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
	/// <summary>
	/// 数据访问类:Department
	/// </summary>
	public partial class Department
	{
		public Department()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_DepartID", "T_Department"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_DepartID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Department");
			strSql.Append(" where F_DepartID=@F_DepartID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DepartID", SqlDbType.Int,4)
};
			parameters[0].Value = F_DepartID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(GSSModel.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Department(");
			strSql.Append("F_ParentID,F_DepartName,F_Note)");
			strSql.Append(" values (");
			strSql.Append("@F_ParentID,@F_DepartName,@F_Note)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_DepartName", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.F_ParentID;
			parameters[1].Value = model.F_DepartName;
			parameters[2].Value = model.F_Note;

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
		public bool Update(GSSModel.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Department set ");
			strSql.Append("F_ParentID=@F_ParentID,");
			strSql.Append("F_DepartName=@F_DepartName,");
			strSql.Append("F_Note=@F_Note");
			strSql.Append(" where F_DepartID=@F_DepartID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ParentID", SqlDbType.Int,4),
					new SqlParameter("@F_DepartName", SqlDbType.NVarChar,30),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,200),
					new SqlParameter("@F_DepartID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_ParentID;
			parameters[1].Value = model.F_DepartName;
			parameters[2].Value = model.F_Note;
			parameters[3].Value = model.F_DepartID;

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
		public bool Delete(int F_DepartID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Department ");
			strSql.Append(" where F_DepartID=@F_DepartID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DepartID", SqlDbType.Int,4)
};
			parameters[0].Value = F_DepartID;

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
		public bool DeleteList(string F_DepartIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Department ");
			strSql.Append(" where F_DepartID in ("+F_DepartIDlist + ")  ");
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
		public GSSModel.Department GetModel(int F_DepartID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_DepartID,F_ParentID,F_DepartName,F_Note from T_Department ");
			strSql.Append(" where F_DepartID=@F_DepartID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_DepartID", SqlDbType.Int,4)
};
			parameters[0].Value = F_DepartID;

			GSSModel.Department model=new GSSModel.Department();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_DepartID"].ToString()!="")
				{
					model.F_DepartID=int.Parse(ds.Tables[0].Rows[0]["F_DepartID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_ParentID"].ToString()!="")
				{
					model.F_ParentID=int.Parse(ds.Tables[0].Rows[0]["F_ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_DepartName"]!=null)
				{
				model.F_DepartName=ds.Tables[0].Rows[0]["F_DepartName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_Note"]!=null)
				{
				model.F_Note=ds.Tables[0].Rows[0]["F_Note"].ToString();
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
			strSql.Append("select F_DepartID,F_ParentID,F_DepartName,F_Note ");
			strSql.Append(" FROM T_Department ");
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
			strSql.Append(" F_DepartID,F_ParentID,F_DepartName,F_Note ");
			strSql.Append(" FROM T_Department ");
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
			parameters[0].Value = "T_Department";
			parameters[1].Value = "F_DepartID";
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

