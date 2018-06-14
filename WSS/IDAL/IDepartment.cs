using System;
using System.Data;
namespace WSS.IDAL
{
	/// <summary>
	/// �ӿڲ㲿�ű�
	/// </summary>
	public interface IDepartment
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int F_DepartID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(WSS.Model.Department model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(WSS.Model.Department model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int F_DepartID);
		bool DeleteList(string F_DepartIDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		WSS.Model.Department GetModel(int F_DepartID);
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
