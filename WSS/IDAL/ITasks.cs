using System;
using System.Data;
namespace WSS.IDAL
{
	/// <summary>
	/// �ӿڲ�ϵͳ��־��
	/// </summary>
	public interface ITasks
	{
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int F_ID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(WSS.Model.Tasks model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(WSS.Model.Tasks model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int F_ID);
		bool DeleteList(string F_IDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		WSS.Model.Tasks GetModel(int F_ID);
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
