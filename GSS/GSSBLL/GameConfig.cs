using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// 字典表
	/// </summary>
    public partial class GameConfig : MarshalByRefObject 
	{
		private readonly GSSDAL.GameConfig dal=new GSSDAL.GameConfig();
		public GameConfig()
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
		public bool Add(GSSModel.GameConfig model)
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
		public bool Update(GSSModel.GameConfig model)
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
		public GSSModel.GameConfig GetModel(int F_ID)
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
		public List<GSSModel.GameConfig> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GSSModel.GameConfig> DataTableToList(DataTable dt)
		{
			List<GSSModel.GameConfig> modelList = new List<GSSModel.GameConfig>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.GameConfig model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.GameConfig();
					if(dt.Rows[n]["F_ID"].ToString()!="")
					{
						model.F_ID=int.Parse(dt.Rows[n]["F_ID"].ToString());
					}
					if(dt.Rows[n]["F_ParentID"].ToString()!="")
					{
						model.F_ParentID=int.Parse(dt.Rows[n]["F_ParentID"].ToString());
					}
					model.F_Name=dt.Rows[n]["F_Name"].ToString();
					model.F_Value=dt.Rows[n]["F_Value"].ToString();
                    model.F_Value1 = dt.Rows[n]["F_Value1"].ToString();
					model.F_ValueGame=dt.Rows[n]["F_ValueGame"].ToString();
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

