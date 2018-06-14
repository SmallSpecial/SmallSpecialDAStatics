using System;
using System.Collections.Generic;
using System.Data;
using WSS.DALFactory;
using WSS.IDAL;
namespace WSS.BLL
{
	/// <summary>
	/// ��ʱ����������Ϣ��
	/// </summary>
	public partial class Notfiy
	{
		private readonly INotfiy dal=DataAccess.CreateNotfiy();
		public Notfiy()
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
		public int  Add(WSS.Model.Notfiy model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(WSS.Model.Notfiy model)
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
		public WSS.Model.Notfiy GetModel(int F_ID)
		{
			
			return dal.GetModel(F_ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public WSS.Model.Notfiy GetModelByCache(int F_ID)
		{
			
			string CacheKey = "NotfiyModel-" + F_ID;
			object objModel = AllOther.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(F_ID);
					if (objModel != null)
					{
						int ModelCache = AllOther.GetConfigInt("ModelCache");
						AllOther.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (WSS.Model.Notfiy)objModel;
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
		public List<WSS.Model.Notfiy> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<WSS.Model.Notfiy> DataTableToList(DataTable dt)
		{
			List<WSS.Model.Notfiy> modelList = new List<WSS.Model.Notfiy>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				WSS.Model.Notfiy model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new WSS.Model.Notfiy();
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

