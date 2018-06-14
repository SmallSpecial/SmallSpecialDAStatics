using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// 部门表
	/// </summary>
	public partial class Department
	{
		private readonly GSSDAL.Department dal=new GSSDAL.Department();
		public Department()
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
		public bool Exists(int F_DepartID)
		{
			return dal.Exists(F_DepartID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(GSSModel.Department model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(GSSModel.Department model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int F_DepartID)
		{
			
			return dal.Delete(F_DepartID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string F_DepartIDlist )
		{
			return dal.DeleteList(F_DepartIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public GSSModel.Department GetModel(int F_DepartID)
		{
			
			return dal.GetModel(F_DepartID);
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
		public List<GSSModel.Department> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GSSModel.Department> DataTableToList(DataTable dt)
		{
			List<GSSModel.Department> modelList = new List<GSSModel.Department>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.Department model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.Department();
					if(dt.Rows[n]["F_DepartID"].ToString()!="")
					{
						model.F_DepartID=int.Parse(dt.Rows[n]["F_DepartID"].ToString());
					}
					if(dt.Rows[n]["F_ParentID"].ToString()!="")
					{
						model.F_ParentID=int.Parse(dt.Rows[n]["F_ParentID"].ToString());
					}
					model.F_DepartName=dt.Rows[n]["F_DepartName"].ToString();
					model.F_Note=dt.Rows[n]["F_Note"].ToString();
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

