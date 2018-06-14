using System;
using System.Data;
namespace WSS.IDAL
{
	/// <summary>
	/// �ӿڲ��û���
	/// </summary>
	public interface IUsers
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int F_UserID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(WSS.Model.Users model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(WSS.Model.Users model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int F_UserID);
		bool DeleteList(string F_UserIDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		WSS.Model.Users GetModel(int F_UserID);
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
