using System;
using System.Data;
using System.Collections.Generic;

using WSS.Model;
using WSS.DALFactory;
using WSS.IDAL;
namespace WSS.BLL
{
	/// <summary>
	/// 部门表
	/// </summary>
	public partial class Department
	{
		private readonly IDepartment dal=DataAccess.CreateDepartment();
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
		public int  Add(WSS.Model.Department model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(WSS.Model.Department model)
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
		public WSS.Model.Department GetModel(int F_DepartID)
		{
			
			return dal.GetModel(F_DepartID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public WSS.Model.Department GetModelByCache(int F_DepartID)
		{
			
			string CacheKey = "DepartmentModel-" + F_DepartID;
			object objModel = AllOther.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(F_DepartID);
					if (objModel != null)
					{
						int ModelCache = AllOther.GetConfigInt("ModelCache");
						AllOther.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (WSS.Model.Department)objModel;
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
		public List<WSS.Model.Department> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WSS.Model.Department> DataTableToList(DataTable dt)
		{
			List<WSS.Model.Department> modelList = new List<WSS.Model.Department>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				WSS.Model.Department model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new WSS.Model.Department();
					if(dt.Rows[n]["F_DepartID"].ToString()!="")
					{
						model.F_DepartID=int.Parse(dt.Rows[n]["F_DepartID"].ToString());
					}
					if(dt.Rows[n]["F_ParentID"].ToString()!="")
					{
						model.F_ParentID=int.Parse(dt.Rows[n]["F_ParentID"].ToString());
					}
					model.F_DepartName=dt.Rows[n]["F_DepartName"].ToString();
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

