using System;
using System.Data;
using System.Collections.Generic;
using GSSModel;
namespace GSSBLL
{
    /// <summary>
    /// �û���
    /// </summary>
    public partial class Users
    {
        private readonly GSSDAL.Users dal = new GSSDAL.Users();
        public Users()
        { }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int F_UserID)
        {
            return dal.Exists(F_UserID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(GSSModel.Users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(GSSModel.Users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int F_UserID)
        {

            return dal.Delete(F_UserID);
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteList(string F_UserIDlist)
        {
            return dal.DeleteList(F_UserIDlist);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public GSSModel.Users GetModel(int F_UserID)
        {

            return dal.GetModel(F_UserID);
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
        public List<GSSModel.Users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<GSSModel.Users> DataTableToList(DataTable dt)
        {
            List<GSSModel.Users> modelList = new List<GSSModel.Users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                GSSModel.Users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new GSSModel.Users();
                    if (dt.Rows[n]["F_UserID"].ToString() != "")
                    {
                        model.F_UserID = int.Parse(dt.Rows[n]["F_UserID"].ToString());
                    }
                    model.F_UserName = dt.Rows[n]["F_UserName"].ToString();
                    model.F_PassWord = dt.Rows[n]["F_PassWord"].ToString();
                    if (dt.Rows[n]["F_DepartID"].ToString() != "")
                    {
                        model.F_DepartID = int.Parse(dt.Rows[n]["F_DepartID"].ToString());
                    }
                    if (dt.Rows[n]["F_RoleID"].ToString() != "")
                    {
                        model.F_RoleID = int.Parse(dt.Rows[n]["F_RoleID"].ToString());
                    }
                    model.F_RealName = dt.Rows[n]["F_RealName"].ToString();
                    if (dt.Rows[n]["F_Sex"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_Sex"].ToString() == "1") || (dt.Rows[n]["F_Sex"].ToString().ToLower() == "true"))
                        {
                            model.F_Sex = true;
                        }
                        else
                        {
                            model.F_Sex = false;
                        }
                    }
                    if (dt.Rows[n]["F_Birthday"].ToString() != "")
                    {
                        model.F_Birthday = DateTime.Parse(dt.Rows[n]["F_Birthday"].ToString());
                    }
                    model.F_Email = dt.Rows[n]["F_Email"].ToString();
                    model.F_MobilePhone = dt.Rows[n]["F_MobilePhone"].ToString();
                    model.F_Telphone = dt.Rows[n]["F_Telphone"].ToString();
                    model.F_Note = dt.Rows[n]["F_Note"].ToString();
                    if (dt.Rows[n]["F_RegTime"].ToString() != "")
                    {
                        model.F_RegTime = DateTime.Parse(dt.Rows[n]["F_RegTime"].ToString());
                    }
                    if (dt.Rows[n]["F_LastInTime"].ToString() != "")
                    {
                        model.F_LastInTime = DateTime.Parse(dt.Rows[n]["F_LastInTime"].ToString());
                    }
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

