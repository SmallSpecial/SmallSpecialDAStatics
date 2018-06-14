using System;
using System.Collections.Generic;
using System.Data;
using WSS.DALFactory;
using WSS.IDAL;
namespace WSS.BLL
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Tasks
    {
        private readonly ITasks dal = DataAccess.CreateTasks();
        public Tasks()
        { }
        #region  Method
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
        public int Add(WSS.Model.Tasks model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(WSS.Model.Tasks model)
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
        public bool DeleteList(string F_IDlist)
        {
            return dal.DeleteList(F_IDlist);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public WSS.Model.Tasks GetModel(int F_ID)
        {

            return dal.GetModel(F_ID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ�����
        /// </summary>
        public WSS.Model.Tasks GetModelByCache(int F_ID)
        {
            string CacheKey = "TasksModel-" + F_ID;
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
                catch { }
            }
            return (WSS.Model.Tasks)objModel;
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
        public List<WSS.Model.Tasks> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<WSS.Model.Tasks> DataTableToList(DataTable dt)
        {
            List<WSS.Model.Tasks> modelList = new List<WSS.Model.Tasks>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WSS.Model.Tasks model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new WSS.Model.Tasks();
                    if (dt.Rows[n]["F_ID"].ToString() != "")
                    {
                        model.F_ID = int.Parse(dt.Rows[n]["F_ID"].ToString());
                    }
                    model.F_Title = dt.Rows[n]["F_Title"].ToString();
                    model.F_Note = dt.Rows[n]["F_Note"].ToString();
                    if (dt.Rows[n]["F_From"].ToString() != "")
                    {
                        model.F_From = int.Parse(dt.Rows[n]["F_From"].ToString());
                    }
                    if (dt.Rows[n]["F_Type"].ToString() != "")
                    {
                        model.F_Type = int.Parse(dt.Rows[n]["F_Type"].ToString());
                    }
                    if (dt.Rows[n]["F_JinjiLevel"].ToString() != "")
                    {
                        model.F_JinjiLevel = int.Parse(dt.Rows[n]["F_JinjiLevel"].ToString());
                    }
                    if (dt.Rows[n]["F_GameName"].ToString() != "")
                    {
                        model.F_GameName = int.Parse(dt.Rows[n]["F_GameName"].ToString());
                    }
                    model.F_GameZone = dt.Rows[n]["F_GameZone"].ToString();
                    model.F_GUserID = dt.Rows[n]["F_GUserID"].ToString();
                    model.F_GRoleName = dt.Rows[n]["F_GRoleName"].ToString();
                    model.F_Tag = dt.Rows[n]["F_Tag"].ToString();
                    if (dt.Rows[n]["F_State"].ToString() != "")
                    {
                        model.F_State = int.Parse(dt.Rows[n]["F_State"].ToString());
                    }
                    model.F_Telphone = dt.Rows[n]["F_Telphone"].ToString();
                    if (dt.Rows[n]["F_DutyMan"].ToString() != "")
                    {
                        model.F_DutyMan = int.Parse(dt.Rows[n]["F_DutyMan"].ToString());
                    }
                    if (dt.Rows[n]["F_PreDutyMan"].ToString() != "")
                    {
                        model.F_PreDutyMan = int.Parse(dt.Rows[n]["F_PreDutyMan"].ToString());
                    }
                    if (dt.Rows[n]["F_DateTime"].ToString() != "")
                    {
                        model.F_DateTime = DateTime.Parse(dt.Rows[n]["F_DateTime"].ToString());
                    }
                    if (dt.Rows[n]["F_EditMan"].ToString() != "")
                    {
                        model.F_EditMan = int.Parse(dt.Rows[n]["F_EditMan"].ToString());
                    }
                    if (dt.Rows[n]["F_Rowtype"].ToString() != "")
                    {
                        model.F_Rowtype = int.Parse(dt.Rows[n]["F_Rowtype"].ToString());
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

