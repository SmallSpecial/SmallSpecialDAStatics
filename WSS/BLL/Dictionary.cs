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
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_DicID)
        {
            return dal.Exists(F_DicID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(WSS.Model.Dictionary model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WSS.Model.Dictionary model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int F_DicID)
        {

            return dal.Delete(F_DicID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string F_DicIDlist)
        {
            return dal.DeleteList(F_DicIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WSS.Model.Dictionary GetModel(int F_DicID)
        {

            return dal.GetModel(F_DicID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WSS.Model.Dictionary> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}

