using System;
using System.Data;
namespace WSS.IDAL
{
	/// <summary>
	/// �ӿڲ�˵���
	/// </summary>
	public interface IMenus
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int F_MenuID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(WSS.Model.Menus model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(WSS.Model.Menus model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int F_MenuID);
		bool DeleteList(string F_MenuIDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		WSS.Model.Menus GetModel(int F_MenuID);
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
