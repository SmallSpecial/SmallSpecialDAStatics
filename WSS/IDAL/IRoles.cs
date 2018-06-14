using System;
using System.Data;
namespace WSS.IDAL
{
	/// <summary>
	/// �ӿڲ��ɫ��
	/// </summary>
	public interface IRoles
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int F_RoleID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(WSS.Model.Roles model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(WSS.Model.Roles model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int F_RoleID);
		bool DeleteList(string F_RoleIDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		WSS.Model.Roles GetModel(int F_RoleID);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  ��Ա����
	} 
}
