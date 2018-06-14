using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using GSS.DBUtility;
namespace GSSDAL
{
	/// <summary>
	/// 数据访问类:Notfiy
	/// </summary>
	public partial class Notfiy
	{
		public Notfiy()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("F_ID", "T_Notfiy"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Notfiy");
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
		public int Add(GSSModel.Notfiy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Notfiy(");
			strSql.Append("F_Title,F_Note,F_URL,F_DateTime,F_SeeTime,F_IsSeed,F_UserID,F_Type)");
			strSql.Append(" values (");
			strSql.Append("@F_Title,@F_Note,@F_URL,@F_DateTime,@F_SeeTime,@F_IsSeed,@F_UserID,@F_Type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,100),
					new SqlParameter("@F_URL", SqlDbType.NVarChar,150),
					new SqlParameter("@F_DateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_SeeTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_IsSeed", SqlDbType.Bit,1),
					new SqlParameter("@F_UserID", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4)};
			parameters[0].Value = model.F_Title;
			parameters[1].Value = model.F_Note;
			parameters[2].Value = model.F_URL;
			parameters[3].Value = model.F_DateTime;
			parameters[4].Value = model.F_SeeTime;
			parameters[5].Value = model.F_IsSeed;
			parameters[6].Value = model.F_UserID;
			parameters[7].Value = model.F_Type;

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
		public bool Update(GSSModel.Notfiy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Notfiy set ");
			strSql.Append("F_Title=@F_Title,");
			strSql.Append("F_Note=@F_Note,");
			strSql.Append("F_URL=@F_URL,");
			strSql.Append("F_DateTime=@F_DateTime,");
			strSql.Append("F_SeeTime=@F_SeeTime,");
			strSql.Append("F_IsSeed=@F_IsSeed,");
			strSql.Append("F_UserID=@F_UserID,");
			strSql.Append("F_Type=@F_Type");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Note", SqlDbType.NVarChar,100),
					new SqlParameter("@F_URL", SqlDbType.NVarChar,150),
					new SqlParameter("@F_DateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_SeeTime", SqlDbType.SmallDateTime),
					new SqlParameter("@F_IsSeed", SqlDbType.Bit,1),
					new SqlParameter("@F_UserID", SqlDbType.Int,4),
					new SqlParameter("@F_Type", SqlDbType.Int,4),
					new SqlParameter("@F_ID", SqlDbType.Int,4)};
			parameters[0].Value = model.F_Title;
			parameters[1].Value = model.F_Note;
			parameters[2].Value = model.F_URL;
			parameters[3].Value = model.F_DateTime;
			parameters[4].Value = model.F_SeeTime;
			parameters[5].Value = model.F_IsSeed;
			parameters[6].Value = model.F_UserID;
			parameters[7].Value = model.F_Type;
			parameters[8].Value = model.F_ID;

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
			strSql.Append("delete from T_Notfiy ");
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
			strSql.Append("delete from T_Notfiy ");
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
		public GSSModel.Notfiy GetModel(int F_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 F_ID,F_Title,F_Note,F_URL,F_DateTime,F_SeeTime,F_IsSeed,F_UserID,F_Type from T_Notfiy ");
			strSql.Append(" where F_ID=@F_ID");
			SqlParameter[] parameters = {
					new SqlParameter("@F_ID", SqlDbType.Int,4)
};
			parameters[0].Value = F_ID;

			GSSModel.Notfiy model=new GSSModel.Notfiy();
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
				if(ds.Tables[0].Rows[0]["F_URL"]!=null)
				{
				model.F_URL=ds.Tables[0].Rows[0]["F_URL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_DateTime"].ToString()!="")
				{
					model.F_DateTime=DateTime.Parse(ds.Tables[0].Rows[0]["F_DateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_SeeTime"].ToString()!="")
				{
					model.F_SeeTime=DateTime.Parse(ds.Tables[0].Rows[0]["F_SeeTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_IsSeed"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["F_IsSeed"].ToString()=="1")||(ds.Tables[0].Rows[0]["F_IsSeed"].ToString().ToLower()=="true"))
					{
						model.F_IsSeed=true;
					}
					else
					{
						model.F_IsSeed=false;
					}
				}
				if(ds.Tables[0].Rows[0]["F_UserID"].ToString()!="")
				{
					model.F_UserID=int.Parse(ds.Tables[0].Rows[0]["F_UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F_Type"].ToString()!="")
				{
					model.F_Type=int.Parse(ds.Tables[0].Rows[0]["F_Type"].ToString());
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
			strSql.Append("select F_ID,F_Title,F_Note,F_URL,F_DateTime,F_SeeTime,F_IsSeed,F_UserID,F_Type ");
			strSql.Append(" FROM T_Notfiy ");
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
			strSql.Append(" F_ID,F_Title,F_Note,F_URL,F_DateTime,F_SeeTime,F_IsSeed,F_UserID,F_Type ");
			strSql.Append(" FROM T_Notfiy ");
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
			parameters[0].Value = "T_Notfiy";
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

