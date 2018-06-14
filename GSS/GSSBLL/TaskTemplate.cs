using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
	/// <summary>
	/// ��������ģ���
	/// </summary>
	public partial class TaskTemplate
	{
		private readonly GSSDAL.TaskTemplate dal=new GSSDAL.TaskTemplate();
		public TaskTemplate()
		{}
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
        public int Add(GSSModel.TaskTemplate model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(GSSModel.TaskTemplate model)
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
        public GSSModel.TaskTemplate GetModel(int F_ID)
        {

            return dal.GetModel(F_ID);
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
        public List<GSSModel.TaskTemplate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<GSSModel.TaskTemplate> DataTableToList(DataTable dt)
        {
            List<GSSModel.TaskTemplate> modelList = new List<GSSModel.TaskTemplate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                GSSModel.TaskTemplate model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new GSSModel.TaskTemplate();
                    if (dt.Rows[n]["F_ID"].ToString() != "")
                    {
                        model.F_ID = int.Parse(dt.Rows[n]["F_ID"].ToString());
                    }
                    if (dt.Rows[n]["F_Type"].ToString() != "")
                    {
                        model.F_Type = int.Parse(dt.Rows[n]["F_Type"].ToString());
                    }
                    model.F_Template = dt.Rows[n]["F_Template"].ToString();
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

