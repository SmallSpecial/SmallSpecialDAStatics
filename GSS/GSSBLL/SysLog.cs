using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// ϵͳ��־��
	/// </summary>
	public partial class SysLog
	{
		private readonly GSSDAL.SysLog dal=new GSSDAL.SysLog();
		public SysLog()
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
		public bool Exists(int F_ID)
		{
			return dal.Exists(F_ID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(GSSModel.SysLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(GSSModel.SysLog model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int F_ID)
		{
			
			return dal.Delete(F_ID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string F_IDlist )
		{
			return dal.DeleteList(F_IDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public GSSModel.SysLog GetModel(int F_ID)
		{
			
			return dal.GetModel(F_ID);
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
		public List<GSSModel.SysLog> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<GSSModel.SysLog> DataTableToList(DataTable dt)
		{
			List<GSSModel.SysLog> modelList = new List<GSSModel.SysLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GSSModel.SysLog model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GSSModel.SysLog();
					if(dt.Rows[n]["F_ID"].ToString()!="")
					{
						model.F_ID=int.Parse(dt.Rows[n]["F_ID"].ToString());
					}
					if(dt.Rows[n]["F_UserID"].ToString()!="")
					{
						model.F_UserID=int.Parse(dt.Rows[n]["F_UserID"].ToString());
					}
					model.F_UserName=dt.Rows[n]["F_UserName"].ToString();
					model.F_Note=dt.Rows[n]["F_Note"].ToString();
					model.F_Data=dt.Rows[n]["F_Data"].ToString();
					if(dt.Rows[n]["F_DateTime"].ToString()!="")
					{
						model.F_DateTime=DateTime.Parse(dt.Rows[n]["F_DateTime"].ToString());
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

