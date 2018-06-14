using System;
using System.Collections.Generic;
using System.Data;

namespace GSSBLL
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Tasks : MarshalByRefObject
    {
        private readonly GSSDAL.T_Tasks dal = new GSSDAL.T_Tasks();
        public Tasks()
        { }
        #region  Method

        /// <summary>
        /// ��ɫ�ָ�
        /// </summary>
        public int GSSTool_RoleRecover(GSSModel.Tasks model)
        {
            return dal.GSSTool_RoleRecover(model);
        }

        /// <summary>
        /// ��ն�������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GSSTool_CustomExec(int bigzone, int zone, int dbtype, string sql)
        {
            return dal.GSSTool_CustomExec(bigzone, zone, dbtype, sql);
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataSet GSSTool_GetGameUser(String bigzone, string sql)
        {
            return dal.GSSTool_GetGameUser(bigzone, sql);
        }

        /// <summary>
        /// ��Ϸ���� ��ͣ�û�
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GSSTool_SetUserLock(string userid, string username, string locktype, string locktime)
        {
            return dal.GSSTool_SetUserLock(userid, username, locktype, locktime);
        }


        /// <summary>
        /// ��Ϸ���� ����û�
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GSSTool_SetUserNoLock(string userid)
        {
            return dal.GSSTool_SetUserNoLock(userid);
        }


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
        public int Add(GSSModel.Tasks model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(GSSModel.Tasks model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ����һ������(�洢����)
        /// </summary>
        public int AddP(GSSModel.Tasks model)
        {
            return dal.AddP(model);
        }

        /// <summary>
        /// ��������(�洢���̷�ʽ)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Edit(GSSModel.Tasks model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// ��������(�洢���̷�ʽ)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditS(GSSModel.Tasks model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// �õ����������õ�DATASET
        /// </summary>
        /// <returns></returns>
        public DataSet GetAlertNum()
        {
            return dal.GetAlertNum();
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
        public GSSModel.Tasks GetModel(int F_ID)
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
        public List<GSSModel.Tasks> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<GSSModel.Tasks> DataTableToList(DataTable dt)
        {
            List<GSSModel.Tasks> modelList = new List<GSSModel.Tasks>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                GSSModel.Tasks model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new GSSModel.Tasks();
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
                    if (dt.Rows[n]["F_VipLevel"].ToString() != "")
                    {
                        model.F_VipLevel = int.Parse(dt.Rows[n]["F_VipLevel"].ToString());
                    }
                    if (dt.Rows[n]["F_LimitType"].ToString() != "")
                    {
                        model.F_LimitType = int.Parse(dt.Rows[n]["F_LimitType"].ToString());
                    }
                    if (dt.Rows[n]["F_LimitTime"].ToString() != "")
                    {
                        model.F_LimitTime = DateTime.Parse(dt.Rows[n]["F_LimitTime"].ToString());
                    }
                    if (dt.Rows[n]["F_Type"].ToString() != "")
                    {
                        model.F_Type = int.Parse(dt.Rows[n]["F_Type"].ToString());
                    }
                    if (dt.Rows[n]["F_State"].ToString() != "")
                    {
                        model.F_State = int.Parse(dt.Rows[n]["F_State"].ToString());
                    }
                    if (dt.Rows[n]["F_GameName"].ToString() != "")
                    {
                        model.F_GameName = int.Parse(dt.Rows[n]["F_GameName"].ToString());
                    }
                    model.F_GameBigZone = dt.Rows[n]["F_GameBigZone"].ToString();
                    model.F_GameZone = dt.Rows[n]["F_GameZone"].ToString();
                    model.F_GUserID = dt.Rows[n]["F_GUserID"].ToString();
                    model.F_GUserName = dt.Rows[n]["F_GUserName"].ToString();
                    model.F_GRoleID = dt.Rows[n]["F_GRoleID"].ToString();
                    model.F_GRoleName = dt.Rows[n]["F_GRoleName"].ToString();
                    model.F_Telphone = dt.Rows[n]["F_Telphone"].ToString();
                    model.F_GPeopleName = dt.Rows[n]["F_GPeopleName"].ToString();
                    if (dt.Rows[n]["F_DutyMan"].ToString() != "")
                    {
                        model.F_DutyMan = int.Parse(dt.Rows[n]["F_DutyMan"].ToString());
                    }
                    if (dt.Rows[n]["F_PreDutyMan"].ToString() != "")
                    {
                        model.F_PreDutyMan = int.Parse(dt.Rows[n]["F_PreDutyMan"].ToString());
                    }
                    if (dt.Rows[n]["F_CreatMan"].ToString() != "")
                    {
                        model.F_CreatMan = int.Parse(dt.Rows[n]["F_CreatMan"].ToString());
                    }
                    if (dt.Rows[n]["F_CreatTime"].ToString() != "")
                    {
                        model.F_CreatTime = DateTime.Parse(dt.Rows[n]["F_CreatTime"].ToString());
                    }
                    if (dt.Rows[n]["F_EditMan"].ToString() != "")
                    {
                        model.F_EditMan = int.Parse(dt.Rows[n]["F_EditMan"].ToString());
                    }
                    if (dt.Rows[n]["F_EditTime"].ToString() != "")
                    {
                        model.F_EditTime = DateTime.Parse(dt.Rows[n]["F_EditTime"].ToString());
                    }
                    model.F_URInfo = dt.Rows[n]["F_URInfo"].ToString();
                    if (dt.Rows[n]["F_Rowtype"].ToString() != "")
                    {
                        model.F_Rowtype = int.Parse(dt.Rows[n]["F_Rowtype"].ToString());
                    }
                    if (dt.Rows[n]["F_CUserName"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_CUserName"].ToString() == "1") || (dt.Rows[n]["F_CUserName"].ToString().ToLower() == "true"))
                        {
                            model.F_CUserName = true;
                        }
                        else
                        {
                            model.F_CUserName = false;
                        }
                    }
                    if (dt.Rows[n]["F_CPSWProtect"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_CPSWProtect"].ToString() == "1") || (dt.Rows[n]["F_CPSWProtect"].ToString().ToLower() == "true"))
                        {
                            model.F_CPSWProtect = true;
                        }
                        else
                        {
                            model.F_CPSWProtect = false;
                        }
                    }
                    if (dt.Rows[n]["F_CPersonID"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_CPersonID"].ToString() == "1") || (dt.Rows[n]["F_CPersonID"].ToString().ToLower() == "true"))
                        {
                            model.F_CPersonID = true;
                        }
                        else
                        {
                            model.F_CPersonID = false;
                        }
                    }
                    model.F_COther = dt.Rows[n]["F_COther"].ToString();
                    model.F_OLastLoginTime = dt.Rows[n]["F_OLastLoginTime"].ToString();
                    if (dt.Rows[n]["F_OCanRestor"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_OCanRestor"].ToString() == "1") || (dt.Rows[n]["F_OCanRestor"].ToString().ToLower() == "true"))
                        {
                            model.F_OCanRestor = true;
                        }
                        else
                        {
                            model.F_OCanRestor = false;
                        }
                    }
                    model.F_OAlwaysPlace = dt.Rows[n]["F_OAlwaysPlace"].ToString();
                    if (dt.Rows[n]["F_TToolUsed"].ToString() != "")
                    {
                        if ((dt.Rows[n]["F_TToolUsed"].ToString() == "1") || (dt.Rows[n]["F_TToolUsed"].ToString().ToLower() == "true"))
                        {
                            model.F_TToolUsed = true;
                        }
                        else
                        {
                            model.F_TToolUsed = false;
                        }
                    }
                    model.F_TUseData = dt.Rows[n]["F_TUseData"].ToString();
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

