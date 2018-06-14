using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// 即时窗口提醒信息表
	/// </summary>
	public partial class Notfiy
	{
		private readonly GSSDAL.Notfiy dal=new GSSDAL.Notfiy();
		public Notfiy()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int F_ID)
		{
			return dal.Exists(F_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(GSSModel.Notfiy model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(GSSModel.Notfiy model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int F_ID)
		{
			
			return dal.Delete(F_ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string F_IDlist )
		{
			return dal.DeleteList(F_IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public GSSModel.Notfiy GetModel(int F_ID)
		{
			
			return dal.GetModel(F_ID);
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GSSModel.Notfiy> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GSSModel.Notfiy> DataTableToList(DataTable dt)
		{
			List<GSSModel.Notfiy> modelList = new List<GSSModel.Notfiy>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.Notfiy model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.Notfiy();
					if(dt.Rows[n]["F_ID"].ToString()!="")
					{
						model.F_ID=int.Parse(dt.Rows[n]["F_ID"].ToString());
					}
					model.F_Title=dt.Rows[n]["F_Title"].ToString();
					model.F_Note=dt.Rows[n]["F_Note"].ToString();
					model.F_URL=dt.Rows[n]["F_URL"].ToString();
					if(dt.Rows[n]["F_DateTime"].ToString()!="")
					{
						model.F_DateTime=DateTime.Parse(dt.Rows[n]["F_DateTime"].ToString());
					}
					if(dt.Rows[n]["F_SeeTime"].ToString()!="")
					{
						model.F_SeeTime=DateTime.Parse(dt.Rows[n]["F_SeeTime"].ToString());
					}
					if(dt.Rows[n]["F_IsSeed"].ToString()!="")
					{
						if((dt.Rows[n]["F_IsSeed"].ToString()=="1")||(dt.Rows[n]["F_IsSeed"].ToString().ToLower()=="true"))
						{
						model.F_IsSeed=true;
						}
						else
						{
							model.F_IsSeed=false;
						}
					}
					if(dt.Rows[n]["F_UserID"].ToString()!="")
					{
						model.F_UserID=int.Parse(dt.Rows[n]["F_UserID"].ToString());
					}
					if(dt.Rows[n]["F_Type"].ToString()!="")
					{
						model.F_Type=int.Parse(dt.Rows[n]["F_Type"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

