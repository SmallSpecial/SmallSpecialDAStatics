using System;
using System.Data;
namespace WSS.IDAL
{
    /// <summary>
    /// �ӿڲ�Dictionary
    /// </summary>
    public interface IDictionary
    {
        #region  ��Ա����
        /// <summary>
        /// �õ����ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        bool Exists(int F_DicID);
        /// <summary>
        /// ����һ������
        /// </summary>
        void Add(WSS.Model.Dictionary model);
        /// <summary>
        /// ����һ������
        /// </summary>
        bool Update(WSS.Model.Dictionary model);
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        bool Delete(int F_DicID);
        bool DeleteList(string F_DicIDlist);
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        WSS.Model.Dictionary GetModel(int F_DicID);
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// ���ݷ�ҳ��������б�
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  ��Ա����
    }
}
