using System;
using System.Collections.Generic;
using System.Data;
using WSS.DALFactory;
using WSS.IDAL;
namespace WSS.BLL
{
	/// <summary>
	/// ��ɫ��
	/// </summary>
	public partial class Roles
	{
		private readonly IRoles dal=DataAccess.CreateRoles();
		public Roles()
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
		public bool Exists(int F_RoleID)
		{
			return dal.Exists(F_RoleID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(WSS.Model.Roles model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(WSS.Model.Roles model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int F_RoleID)
		{
			
			return dal.Delete(F_RoleID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string F_RoleIDlist )
		{
			return dal.DeleteList(F_RoleIDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public WSS.Model.Roles GetModel(int F_RoleID)
		{
			
			return dal.GetModel(F_RoleID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public WSS.Model.Roles GetModelByCache(int F_RoleID)
		{
			
			string CacheKey = "RolesModel-" + F_RoleID;
			object objModel = AllOther.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(F_RoleID);
					if (objModel != null)
					{
						int ModelCache = AllOther.GetConfigInt("ModelCache");
						AllOther.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (WSS.Model.Roles)objModel;
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
		public List<WSS.Model.Roles> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<WSS.Model.Roles> DataTableToList(DataTable dt)
		{
			List<WSS.Model.Roles> modelList = new List<WSS.Model.Roles>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				WSS.Model.Roles model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new WSS.Model.Roles();
					if(dt.Rows[n]["F_RoleID"].ToString()!="")
					{
						model.F_RoleID=int.Parse(dt.Rows[n]["F_RoleID"].ToString());
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
					model.F_Power=dt.Rows[n]["F_Power"].ToString();
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
