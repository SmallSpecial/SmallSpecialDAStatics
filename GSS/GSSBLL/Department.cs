using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// ���ű�
	/// </summary>
	public partial class Department
	{
		private readonly GSSDAL.Department dal=new GSSDAL.Department();
		public Department()
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
		public bool Exists(int F_DepartID)
		{
			return dal.Exists(F_DepartID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(GSSModel.Department model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(GSSModel.Department model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int F_DepartID)
		{
			
			return dal.Delete(F_DepartID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string F_DepartIDlist )
		{
			return dal.DeleteList(F_DepartIDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public GSSModel.Department GetModel(int F_DepartID)
		{
			
			return dal.GetModel(F_DepartID);
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
		public List<GSSModel.Department> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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

