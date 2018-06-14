using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// �˵���
	/// </summary>
	public partial class Menus
	{
		private readonly GSSDAL.Menus dal=new GSSDAL.Menus();
		public Menus()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int F_MenuID)
		{
			return dal.Exists(F_MenuID);
		}

		/// <summary>
		/// ����һ������
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
		/// ����һ������
		/// </summary>
		public bool Update(GSSModel.Menus model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int F_MenuID)
		{
			
			return dal.Delete(F_MenuID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string F_MenuIDlist )
		{
			return dal.DeleteList(F_MenuIDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public GSSModel.Menus GetModel(int F_MenuID)
		{
			
			return dal.GetModel(F_MenuID);
		}


		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<GSSModel.Menus> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// ��������б�
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
		/// ��������б�
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

