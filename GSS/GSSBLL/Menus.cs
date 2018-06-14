using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// 菜单表
	/// </summary>
	public partial class Menus
	{
		private readonly GSSDAL.Menus dal=new GSSDAL.Menus();
		public Menus()
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
		public bool Exists(int F_MenuID)
		{
			return dal.Exists(F_MenuID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(GSSModel.Menus model)
		{
            try
            {
                dal.Add(model);
                return true;
            }
            catch //(System.Exception ex)
            {
                return false;
            }
			
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(GSSModel.Menus model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int F_MenuID)
		{
			
			return dal.Delete(F_MenuID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string F_MenuIDlist )
		{
			return dal.DeleteList(F_MenuIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public GSSModel.Menus GetModel(int F_MenuID)
		{
			
			return dal.GetModel(F_MenuID);
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
		public List<GSSModel.Menus> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public string GetMenuID(string menuname)
        {
            DataSet ds = dal.GetMenuID(menuname);
            if (ds.Tables[0].Rows.Count==0)
            {
                return "";
            }
            return ds.Tables[0].Rows[0][0].ToString();
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GSSModel.Menus> DataTableToList(DataTable dt)
		{
			List<GSSModel.Menus> modelList = new List<GSSModel.Menus>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.Menus model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.Menus();
					if(dt.Rows[n]["F_MenuID"].ToString()!="")
					{
						model.F_MenuID=int.Parse(dt.Rows[n]["F_MenuID"].ToString());
					}
					model.F_Name=dt.Rows[n]["F_Name"].ToString();
					model.F_FormName=dt.Rows[n]["F_FormName"].ToString();
					if(dt.Rows[n]["F_ParentID"].ToString()!="")
					{
						model.F_ParentID=int.Parse(dt.Rows[n]["F_ParentID"].ToString());
					}
					if(dt.Rows[n]["F_IsUsed"].ToString()!="")
					{
						if((dt.Rows[n]["F_IsUsed"].ToString()=="1")||(dt.Rows[n]["F_IsUsed"].ToString().ToLower()=="true"))
						{
						model.F_IsUsed=true;
						}
						else
						{
							model.F_IsUsed=false;
						}
					}
					if(dt.Rows[n]["F_Sort"].ToString()!="")
					{
						model.F_Sort=int.Parse(dt.Rows[n]["F_Sort"].ToString());
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

