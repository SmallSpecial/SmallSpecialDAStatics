using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// �ֵ��
	/// </summary>
	public partial class Dictionary
	{
		private readonly GSSDAL.Dictionary dal=new GSSDAL.Dictionary();
		public Dictionary()
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
		public bool Exists(int F_DicID)
		{
			return dal.Exists(F_DicID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Add(GSSModel.Dictionary model)
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
		public bool Update(GSSModel.Dictionary model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int F_DicID)
		{
			
			return dal.Delete(F_DicID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string F_DicIDlist )
		{
			return dal.DeleteList(F_DicIDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public GSSModel.Dictionary GetModel(int F_DicID)
		{
			
			return dal.GetModel(F_DicID);
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
		public List<GSSModel.Dictionary> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<GSSModel.Dictionary> DataTableToList(DataTable dt)
		{
			List<GSSModel.Dictionary> modelList = new List<GSSModel.Dictionary>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.Dictionary model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.Dictionary();
					if(dt.Rows[n]["F_DicID"].ToString()!="")
					{
						model.F_DicID=int.Parse(dt.Rows[n]["F_DicID"].ToString());
					}
					if(dt.Rows[n]["F_ParentID"].ToString()!="")
					{
						model.F_ParentID=int.Parse(dt.Rows[n]["F_ParentID"].ToString());
					}
					model.F_Value=dt.Rows[n]["F_Value"].ToString();
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

