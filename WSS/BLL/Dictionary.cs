using System;
using System.Collections.Generic;
using System.Data;
using WSS.DALFactory;
using WSS.IDAL;
namespace WSS.BLL
{
    /// <summary>
    /// Dictionary
    /// </summary>
    public partial class Dictionary
    {
        private readonly IDictionary dal = DataAccess.CreateDictionary();
        public Dictionary()
        { }
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
        public bool Exists(int F_DicID)
        {
            return dal.Exists(F_DicID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(WSS.Model.Dictionary model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(WSS.Model.Dictionary model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int F_DicID)
        {

            return dal.Delete(F_DicID);
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteList(string F_DicIDlist)
        {
            return dal.DeleteList(F_DicIDlist);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public WSS.Model.Dictionary GetModel(int F_DicID)
        {

            return dal.GetModel(F_DicID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ�����
        /// </summary>
        public WSS.Model.Dictionary GetModelByCache(int F_DicID)
        {

            string CacheKey = "DictionaryModel-" + F_DicID;
            object objModel = AllOther.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(F_DicID);
                    if (objModel != null)
                    {
                        int ModelCache = AllOther.GetConfigInt("ModelCache");
                        AllOther.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (WSS.Model.Dictionary)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<WSS.Model.Dictionary> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<WSS.Model.Dictionary> DataTableToList(DataTable dt)
        {
            List<WSS.Model.Dictionary> modelList = new List<WSS.Model.Dictionary>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WSS.Model.Dictionary model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new WSS.Model.Dictionary();
                    if (dt.Rows[n]["F_DicID"].ToString() != "")
                    {
                        model.F_DicID = int.Parse(dt.Rows[n]["F_DicID"].ToString());
                    }
                    if (dt.Rows[n]["F_ParentID"].ToString() != "")
                    {
                        model.F_ParentID = int.Parse(dt.Rows[n]["F_ParentID"].ToString());
                    }
                    model.F_Value = dt.Rows[n]["F_Value"].ToString();
                    if (dt.Rows[n]["F_IsUsed"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_IsUsed"].ToString() == "1") || (dt.Rows[n]["F_IsUsed"].ToString().ToLower() == "true"))
                        {
                            model.F_IsUsed = true;
                        }
                        else
                        {
                            model.F_IsUsed = false;
                        }
                    }
                    if (dt.Rows[n]["F_Sort"].ToString() != "")
                    {
                        model.F_Sort = int.Parse(dt.Rows[n]["F_Sort"].ToString());
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

