using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;
using System.Configuration;
using System.Text;
using System.Data;
using WSS.BLL;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WSS.Web.Admin.Task
{
    public partial class TaskList : System.Web.UI.Page
    {
        string wherestr = "1=1";
        WSS.Model.Tasks TList;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "工单管理";

            //if (!string.IsNullOrEmpty(Request.QueryString["typeid"]))
            //{
            if (!Ext.IsAjaxRequest)
            {
                LoadData(wherestr);
                GetAction.Text = "add";

            }

            //}
            //else
            //{
            //    Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], ConfigurationManager.AppSettings["AlertMessage"]).Show();
            //}
           
        }


        private void LoadData(string where)// 绑定StoreTasks数据源
        {
            where += " order by f_datetime desc";
            List<WSS.Model.Tasks> list = new WSS.BLL.Tasks().GetModelList(where);

            foreach (WSS.Model.Tasks ts in list)
            {
                if (ts.F_Note.Length>25)
                {
                    ts.F_Note = Server.HtmlDecode(ts.F_Note.Substring(0, 25));
                }
                

            }

            this.StoreTasks.DataSource = list;
            this.StoreTasks.DataBind();


            List<WSS.Model.Dictionary> lisd = new WSS.BLL.Dictionary().GetModelList("F_ParentID=100100 or F_ParentID=100101 ");
            StoreDic.DataSource = lisd;
            StoreDic.DataBind();




            AllOther.ComboBoxDic(cbFrom, "100103");
            AllOther.ComboBoxDic(cbJinjiLevel, "100104");
            AllOther.ComboBoxDic(cbGameName, "100102");
            AllOther.ComboBoxDic(cbType, "100101");
            AllOther.ComboBoxDic(cbstate, "100100");
            AllOther.ComboBoxUser(cbDutyMan, "1=1");

            cbFrom.SelectedIndex = 0;
            cbJinjiLevel.SelectedIndex = 0;
            cbGameName.SelectedIndex = 0;
            cbstate.SelectedIndex = 0;
            GridPanelNewsList.SelectionMemory = SelectionMemoryMode.Auto;
            RowSelectionModel sm = this.GridPanelNewsList.SelectionModel.Primary as RowSelectionModel;
            sm.ClearSelections();
        }
        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            LoadData(wherestr);
        }


        protected void Check_btnSubmit(object sender, AjaxEventArgs e)//提交操作
        {
            //try
            //{

            WSS.Model.Tasks n = new WSS.Model.Tasks();
            n.F_Title = txtTitle.Text;
            n.F_Type = int.Parse(cbType.SelectedItem.Value.ToString());
            n.F_Note = e.ExtraParams["fckParameter"].ToString(); ;
            n.F_From = int.Parse(cbFrom.SelectedItem.Value.ToString());
            n.F_JinjiLevel = int.Parse(cbJinjiLevel.SelectedItem.Value.ToString());
            n.F_GameName = int.Parse(cbGameName.SelectedItem.Value.ToString());
            n.F_DateTime = DateTime.Now;
            n.F_Telphone = txtTelphone.Text;
            n.F_DutyMan = int.Parse(cbDutyMan.SelectedItem.Value.ToString());
            // n.F_EditMan = int.Parse(Session["FID"]);
            n.F_EditMan = 3;
            n.F_Rowtype = 0;
            n.F_State = int.Parse(cbstate.SelectedItem.Value.ToString());


            switch (GetAction.Text)
            {
                case "add":
                    if (n.F_Note.Trim().Length < 1)
                    {
                        Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgTitle"], "内容不能为空").Show();
                        break;
                    }

                    AddNewsInfo(n);
                    break;
                case "edit":
                    string rowid = HF_id.Text;
                    if (!string.IsNullOrEmpty(rowid))
                    {
                        n.F_ID = Convert.ToInt32(rowid);
                        TList = new WSS.BLL.Tasks().GetModel(n.F_ID);
                        if (TList != null)
                        {
                            n.F_PreDutyMan = TList.F_DutyMan;
                        }

                        UpdateNewsInfoByID(n);
                    }
                    break;
                default:
                    break;
            }

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        } 

        private void UpdateNewsInfoByID(WSS.Model.Tasks n)//修改操作
        {
            if (new WSS.BLL.Tasks().Update(n) == true)
            {
                WSS.BLL.T_TasksLog TLoglist = new WSS.BLL.T_TasksLog();

                TLoglist.F_ID = TList.F_ID;

                if (n.F_Note.Trim().Length != 0)
                {
                    TLoglist.F_Note = n.F_Note;
                }
                if (n.F_Title != TList.F_Title)
                {
                    TLoglist.F_Title = n.F_Title;
                }
                if (n.F_DateTime != TList.F_DateTime)
                {
                    TLoglist.F_DateTime = n.F_DateTime;
                }
                if (n.F_DutyMan != TList.F_DutyMan)
                {
                    TLoglist.F_DutyMan = n.F_DutyMan;
                }

                TLoglist.F_EditMan = TList.F_EditMan;
                if (n.F_From != TList.F_From)
                {
                    TLoglist.F_From = n.F_From;
                }
                if (n.F_GameName != TList.F_GameName)
                {
                    TLoglist.F_GameName = n.F_GameName;
                }
                if (n.F_JinjiLevel != TList.F_JinjiLevel)
                {
                    TLoglist.F_JinjiLevel = n.F_JinjiLevel;
                }
                if (n.F_PreDutyMan != TList.F_PreDutyMan)
                {
                    TLoglist.F_PreDutyMan = n.F_PreDutyMan;
                }
                if (n.F_Rowtype != TList.F_Rowtype)
                {
                    TLoglist.F_Rowtype = n.F_Rowtype;
                }
                if (n.F_State != TList.F_State)
                {
                    TLoglist.F_State = n.F_State;
                }
                if (n.F_Telphone != TList.F_Telphone)
                {
                    TLoglist.F_Telphone = n.F_Telphone;
                }
                if (n.F_Type != TList.F_Type)
                {
                    TLoglist.F_Type = n.F_Type;
                }

                TLoglist.Add();
                GridPanelNewsList.Reload();
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgTitle"], "数据已经修改成功！").Show();
                NewsWindow.Hide();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], ConfigurationManager.AppSettings["ErrorMessage"]).Show();
            }
        }

        private void AddNewsInfo(WSS.Model.Tasks n)
        {
            int tid = new WSS.BLL.Tasks().Add(n);

            if (tid > 0)
            {
                WSS.BLL.T_TasksLog a = new WSS.BLL.T_TasksLog();
                a.F_ID = tid;
                a.F_Title = n.F_Title;
                a.F_Note = n.F_Note;
                a.F_From = n.F_From;
                a.F_Type = n.F_Type;
                a.F_JinjiLevel = n.F_JinjiLevel;
                a.F_GameName = n.F_GameName;
                a.F_GameZone = n.F_GameZone;
                a.F_GUserID = n.F_GUserID;
                a.F_GRoleName = n.F_GRoleName;
                a.F_Tag = n.F_Tag;
                a.F_State = n.F_State;
                a.F_Telphone = n.F_Telphone;
                a.F_DutyMan = n.F_DutyMan;
                a.F_PreDutyMan = n.F_PreDutyMan;
                a.F_DateTime = n.F_DateTime;
                a.F_EditMan = n.F_EditMan;
                a.F_Rowtype = n.F_Rowtype;
                a.Add();



                GridPanelNewsList.Reload();
                //Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "数据已经添加成功,是否继续添加?", new MessageBox.ButtonsConfig
                //{
                //    Yes = new MessageBox.ButtonConfig
                //    {
                //        Handler = "#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{txtTitle}.focus(true);",
                //        Text = "确 定"
                //    },
                //    No = new MessageBox.ButtonConfig
                //    {
                //        Handler = "#{FormPanelNews}.getForm().reset();#{NewsWindow}.hide();",
                //        Text = "取 消"
                //    }
                //}).Show();
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "数据已经添加成功").Show();
                WSS.BLL.AllOther.AddNotify("qq","qqqqqq");
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], ConfigurationManager.AppSettings["ErrorMessage"]).Show();
            }
        }

        [AjaxMethod]
        public string Update_Click()
        {
            string rowid = GetSelectRowID();
            if (!string.IsNullOrEmpty(rowid))
            {
                string[] rowsid = rowid.Split(new char[] { ',' });
                switch (rowsid.Length)
                {

                    case 1:
                        GetAction.Text = "edit";
                        HF_id.Text = rowid;
                        WSS.Model.Tasks list = new WSS.BLL.Tasks().GetModel(Convert.ToInt32(rowsid[0]));

                        txtTitle.Text = list.F_Title;
                        cbType.Value = list.F_Type;

                        cbFrom.Value = list.F_From;
                        cbJinjiLevel.Value = list.F_JinjiLevel;
                        cbGameName.Value = list.F_GameName;
                        ////list.F_DateTime = DateTime.Now;
                        txtTelphone.Text = list.F_Telphone;
                        cbDutyMan.Value = list.F_DutyMan;
                        cbstate.Value = list.F_State;

                        LabelHistory.Html = GetHistory(list.F_ID);


                        //return list.F_Note;
                        return "";
                    default:
                        Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,您在<span style=\"color:#F00;\">多行选中状态</span>下不能进行此操作,请选中一行!!").Show();
                        return null;
                }
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,请选中数据行后执行此操作.").Show();
                return null;
            }
        }

        [AjaxMethod]
        public string Update_Click1(string rowid)
        {
            //string rowid = GetSelectRowID();
            if (!string.IsNullOrEmpty(rowid))
            {
                string[] rowsid = rowid.Split(new char[] { ',' });
                switch (rowsid.Length)
                {

                    case 1:
                        GetAction.Text = "edit";
                        HF_id.Text = rowid;
                        WSS.Model.Tasks list = new WSS.BLL.Tasks().GetModel(Convert.ToInt32(rowsid[0]));

                        txtTitle.Text = list.F_Title;
                        cbType.Value = list.F_Type;

                        cbFrom.Value = list.F_From;
                        cbJinjiLevel.Value = list.F_JinjiLevel;
                        cbGameName.Value = list.F_GameName;
                        ////list.F_DateTime = DateTime.Now;
                        txtTelphone.Text = list.F_Telphone;
                        cbDutyMan.Value = list.F_DutyMan;
                        cbstate.Value = list.F_State;

                        LabelHistory.Html = GetHistory(list.F_ID);
                        this.NewsWindow.SetTitle("修改工单");
                        this.NewsWindow.Show();
                        return "";
                    default:
                        Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,您在<span style=\"color:#F00;\">多行选中状态</span>下不能进行此操作,请选中一行!!").Show();
                        return "";
                }
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,请选中数据行后执行此操作.").Show();
                return "";
            }
        }
        [AjaxMethod]
        public string GetTemplate()
        {
            DataSet dsTempalte = new WSS.BLL.T_TaskTemplate().GetList("f_Type=" +

cbType.SelectedItem.Value + "");
            if (dsTempalte == null || dsTempalte.Tables.Count == 0 || dsTempalte.Tables

[0].Rows.Count == 0)
            {
                return "";
            }
            string tStr = dsTempalte.Tables[0].Rows[0]["F_Template"].ToString();
            if (tStr.Length > 0)
            {
                return tStr;
            }
            else
            {
                return "";
            }
        }


        private string GetSelectRowID()//获取选中行并返回ID数组
        {
            try
            {
                RowSelectionModel sm = this.GridPanelNewsList.SelectionModel.Primary as RowSelectionModel;
                if (sm.SelectedRows.Count == 0)
                {
                    return "";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (SelectedRow row in sm.SelectedRows)
                    {
                        sb.Append(row.RecordID + ",");
                    }
                    return sb.ToString().Substring(0, (sb.Length - 1));

                }
            }
            catch (Exception)
            {
                return null;
            }
        } 

        protected void Delete_Click(object sender, AjaxEventArgs e)//删除操作
        {
            string rowid = GetSelectRowID();

            if (!string.IsNullOrEmpty(rowid))
            {
                Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "您确定要删除选中的数据项吗?", new MessageBox.ButtonsConfig
                {
                    Yes = new MessageBox.ButtonConfig
                    {
                        Handler = "Coolite.AjaxMethods.DeleteTasks('" + rowid + "');",
                        Text = "确 定"
                    },
                    No = new MessageBox.ButtonConfig
                    {
                        Text = "取 消"
                    }
                }).Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,请选中数据行后执行此操作.").Show();
            }

        }
        [AjaxMethod]
        public void DeleteCommd(string rowid)
        {
            if (!string.IsNullOrEmpty(rowid))
            {
                Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "您确定要删除选中的数据项吗?", new MessageBox.ButtonsConfig
                {
                    Yes = new MessageBox.ButtonConfig
                    {
                        Handler = "Coolite.AjaxMethods.DeleteTasks('" + rowid + "');",
                        Text = "确 定"
                    },
                    No = new MessageBox.ButtonConfig
                    {
                        Text = "取 消"
                    }
                }).Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,请选中数据行后执行此操作.").Show();
            }

        }


        protected void History_Click(object sender, AjaxEventArgs e)//转为历史工单
        {
            string rowid = GetSelectRowID();

            if (!string.IsNullOrEmpty(rowid))
            {
                Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "您确定要转存选中的数据项吗?", new MessageBox.ButtonsConfig
                {
                    Yes = new MessageBox.ButtonConfig
                    {
                        Handler = "Coolite.AjaxMethods.HistoryTasks('" + rowid + "');",
                        Text = "确 定"
                    },
                    No = new MessageBox.ButtonConfig
                    {
                        Text = "取 消"
                    }
                }).Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "对不起,请选中数据行后执行此操作.").Show();
            }

        }
        [AjaxMethod]
        public void HistoryTasks(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Tasks] set f_rowtype=2 ");
            strSql.Append(" where F_ID in (" + IDList + ") ");
            int isok = DbHelperSQL.ExecuteSql(strSql.ToString());

            if (isok > 0)
            {
                GridPanelNewsList.Reload();
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgTitle"], "数据操作成功!").Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], ConfigurationManager.AppSettings["ErrorMessage"]).Show();
            }
        }


        [AjaxMethod]
        public void DeleteTasks(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Tasks] set f_rowtype=1 ");
            strSql.Append(" where F_ID in (" + IDList + ") ");
            int isok = DbHelperSQL.ExecuteSql(strSql.ToString());

            if (isok > 0)
            {
                GridPanelNewsList.Reload();
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgTitle"], "数据删除成功!").Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], ConfigurationManager.AppSettings["ErrorMessage"]).Show();
            }
        }
        protected void Search_Click(object sender, AjaxEventArgs e)//按条件查找
        {
            if (!String.IsNullOrEmpty(tftitle.Text.Trim()))
            {
                wherestr += " and F_Title like '%" + tftitle.Text.Trim() + "%'";
            }
            if (!String.IsNullOrEmpty(tfnote.Text.Trim()))
            {

                wherestr += " and F_Note like '%" + tfnote.Text.Trim() + "%'";
            }
            LoadData(wherestr);

        }
        protected void SearchAll_Click(object sender, AjaxEventArgs e)//清空查找条件
        {
            tftitle.Text = "";
            tfnote.Text = "";
            wherestr = "1=1";
            LoadData(wherestr);
        }

        private string GetHistory(int f_id)//得到工单历史
        {
            WSS.BLL.T_TasksLog t = new WSS.BLL.T_TasksLog();
            DataSet ds = t.GetList(" F_ID=" + f_id + "");
            if (ds != null && ds.Tables.Count > 0)
            {
                string Rstr = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Rstr += "<hr><br />";
                    if (dr["F_DateTime"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<font color=red><b>---编辑时间：" + dr["F_DateTime"].ToString() + "---&nbsp;&nbsp</b><br />";
                    }
                    if (dr["F_EditMan"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>---编辑用户</b>：" + dr["F_EditMan"].ToString() + "&nbsp;---</font><br />";
                    }

                    if (dr["F_Title"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>标题：</b>" + dr["F_Title"].ToString() + "<br />";
                    }
                    if (dr["F_Note"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>内容：</b><br />" + dr["F_Note"].ToString() + " <br />";
                    }
                    if (dr["F_From"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>工单来源：</b>" + dr["F_From"].ToString() + "<br />";
                    }
                    if (dr["F_Type"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>工单类型：</b>" + dr["F_Type"].ToString() + "<br />";
                    }
                    if (dr["F_JinjiLevel"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>紧急程度：</b>" + dr["F_JinjiLevel"].ToString() + "<br />";
                    }
                    if (dr["F_GameName"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>游戏名：</b>" + dr["F_GameName"].ToString() + "<br />";
                    }
                    //if (dr["F_Tag"].ToString().Trim().Length > 0)
                    //{
                    //    Rstr += "标签：" + dr["F_Title"].ToString() + "；";
                    //}
                    if (dr["F_State"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>工单状态：</b>" + dr["F_State"].ToString() + "<br />";
                    }
                    if (dr["F_Telphone"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>玩家电话：</b>" + dr["F_Telphone"].ToString() + "<br />";
                    }
                    if (dr["F_DutyMan"].ToString().Trim().Length > 0)
                    {
                        Rstr += "<b>当前职责人</b>：" + dr["F_DutyMan"].ToString() + "<br />";
                    }
                    //if (dr["F_PreDutyMan.Text"].ToString().Trim().Length > 0)
                    //{
                    //    Rstr += "F_PreDutyMan：" + dr["F_Title"].ToString() + "；";
                    //}

                    //if (dr["F_Rowtype.Text"].ToString().Trim().Length > 0)
                    //{
                    //    Rstr += "行状态：" + dr["F_Title"].ToString() + "；";
                    //}
                }
                if (Rstr.Trim().Length == 0)
                {
                    Rstr = "暂无";
                }
                return Rstr;
            }
            else
            {
                return "暂无";
            }

        }


    }
}
