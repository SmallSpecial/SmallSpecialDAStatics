using System;
using System.Collections.Generic;
using System.Data;

namespace GSSBLL
{
    /// <summary>
    /// FDBISql
    /// </summary>
    public partial class FDBISql : MarshalByRefObject
    {
        private readonly GSSDAL.FDBISql dal = new GSSDAL.FDBISql();
        public FDBISql()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int F_ID)
        {
            return dal.Exists(F_ID);
        }

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(GSSModel.FDBISql model)
        //{
        //    return dal.Add(model);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(GSSModel.FDBISql model)
        {
            int isok = dal.Add(model);
            if (isok > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(GSSModel.FDBISql model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int F_ID)
        {

            return dal.Delete(F_ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string F_IDlist)
        {
            return dal.DeleteList(F_IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public GSSModel.FDBISql GetModel(int F_ID)
        {

            return dal.GetModel(F_ID);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int userid)
        {
            return dal.GetList("F_UserID=-1 or F_UserID=" + userid + "");
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
        public List<GSSModel.FDBISql> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<GSSModel.FDBISql> DataTableToList(DataTable dt)
        {
            List<GSSModel.FDBISql> modelList = new List<GSSModel.FDBISql>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                GSSModel.FDBISql model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new GSSModel.FDBISql();
                    if (dt.Rows[n]["F_ID"].ToString() != "")
                    {
                        model.F_ID = int.Parse(dt.Rows[n]["F_ID"].ToString());
                    }
                    model.F_Title = dt.Rows[n]["F_Title"].ToString();
                    model.F_Note = dt.Rows[n]["F_Note"].ToString();
                    model.F_Sql = dt.Rows[n]["F_Sql"].ToString();
                    if (dt.Rows[n]["F_UserID"].ToString() != "")
                    {
                        model.F_UserID = int.Parse(dt.Rows[n]["F_UserID"].ToString());
                    }
                    if (dt.Rows[n]["F_DaTeTime"].ToString() != "")
                    {
                        model.F_DaTeTime = DateTime.Parse(dt.Rows[n]["F_DaTeTime"].ToString());
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

