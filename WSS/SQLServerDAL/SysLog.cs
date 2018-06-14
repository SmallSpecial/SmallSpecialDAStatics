using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WSS.IDAL;
using WSS.DBUtility;//Please add references
namespace WSS.SQLServerDAL
{
	/// <summary>
	/// 数据访问类:SysLog
	/// </summary>
	public partial class SysLog:ISysLog
	{
		public SysLog()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_ID", "T_SysLog"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SysLog");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WSS.Model.SysLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SysLog(");
			strSql.Append("F_UserID,F_UserName,F_Note,F_URL,F_DateTime)");
			strSql.Append(" values (");
			strSql.Append("@F_UserID,@F_UserName,@F_Note,@F_URL,@F_DateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,4),
					new SqlParameter("@F_UserName", SqlDbType.NChar,16),
					new SqlParameter("@F_Note", SqlDbType.NChar,10),
					new SqlParameter("@F_URL", SqlDbType.NChar,10),
					new SqlParameter("@F_DateTime", SqlDbType.NChar,10)};
			parameters[0].Value = model.F_UserID;
			parameters[1].Value = model.F_UserName;
			parameters[2].Value = model.F_Note;
			parameters[3].Value = model.F_URL;
			parameters[4].Value = model.F_DateTime;

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
		public bool Update(WSS.Model.SysLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SysLog set ");
			strSql.Append("F_UserID=@F_UserID,");
			strSql.Append("F_UserName=@F_UserName,");
			strSql.Append("F_Note=@F_Note,");
			strSql.Append("F_URL=@F_URL,");
			strSql.Append("F_DateTime=@F_DateTime");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_UserID", SqlDbType.Int,4),
					new SqlParameter("@F_UserName", SqlDbType.NChar,16),
					new SqlParameter("@F_Note", SqlDbType.NChar,10),
					new SqlParameter("@F_URL", SqlDbType.NChar,10),
					new SqlParameter("@F_DateTime", SqlDbType.NChar,10),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_UserID;
			parameters[1].Value = model.F_UserName;
			parameters[2].Value = model.F_Note;
			parameters[3].Value = model.F_URL;
			parameters[4].Value = model.F_DateTime;
			parameters[5].Value = model.F_ID;

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
		public bool Delete(int F_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SysLog ");
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string F_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SysLog ");
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
		/// 得到一个对象实体
		/// </summary>
		public WSS.Model.SysLog GetModel(int F_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_ID,F_UserID,F_UserName,F_Note,F_URL,F_DateTime from T_SysLog ");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

			WSS.Model.SysLog model=new WSS.Model.SysLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["F_ID"].ToString()!="")
				{
					model.F_ID=int.Parse(ds.Tables[0].Rows[0]["F_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_UserID"].ToString()!="")
				{
					model.F_UserID=int.Parse(ds.Tables[0].Rows[0]["F_UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_UserName"]!=null)
				{
				model.F_UserName=ds.Tables[0].Rows[0]["F_UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_Note"]!=null)
				{
				model.F_Note=ds.Tables[0].Rows[0]["F_Note"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_URL"]!=null)
				{
				model.F_URL=ds.Tables[0].Rows[0]["F_URL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_DateTime"]!=null)
				{
				model.F_DateTime=ds.Tables[0].Rows[0]["F_DateTime"].ToString();
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
			strSql.Append("select F_ID,F_UserID,F_UserName,F_Note,F_URL,F_DateTime ");
			strSql.Append(" FROM T_SysLog ");
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
			strSql.Append(" F_ID,F_UserID,F_UserName,F_Note,F_URL,F_DateTime ");
			strSql.Append(" FROM T_SysLog ");
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
			parameters[0].Value = "T_SysLog";
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

