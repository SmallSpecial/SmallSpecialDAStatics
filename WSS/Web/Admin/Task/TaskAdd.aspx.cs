using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;
using System.Configuration;
using System.Text;
using WSS.BLL;

namespace WSS.Web.Admin.Task
{
    public partial class TaskAdd : System.Web.UI.Page
    {
       string wherestr = "1=1";

        protected void Page_Load(object sender, EventArgs e)
        {
           // this.ScriptManager1.Theme = Coolite.Ext.Web.Theme.Slate;


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


        /// <summary>
        /// 绑定StoreTasks数据源
        /// </summary>

        private void LoadData(string where)
        {
            //List<WSS.Model.Tasks> list = new WSS.BLL.Tasks().GetModelList(where);

            
            //this.StoreTasks.DataSource = list;
            //this.StoreTasks.DataBind();


            //List<WSS.Model.Dictionary> lisd = new WSS.BLL.Dictionary().GetModelList("F_ParentID=100100 or F_ParentID=100101 ");
            //StoreDic.DataSource = lisd;
            //StoreDic.DataBind();

           

            AllOther.ComboBoxDic(cbFrom, "100103");
            AllOther.ComboBoxDic(cbJinjiLevel, "100104");
            AllOther.ComboBoxDic(cbGameName, "100102");
            AllOther.ComboBoxDic(cbType, "100101");
            AllOther.ComboBoxUser(cbDutyMan, "1=1");

        }
        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            LoadData(wherestr);
        }


        /// <summary>
        /// 提交操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Check_btnSubmit(object sender, AjaxEventArgs e)
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


                switch (GetAction.Text)
                {
                    case "add":
                        AddNewsInfo(n);
                        break;
                    case "edit":
                        string rowid = GetSelectRowID();
                        if (!string.IsNullOrEmpty(rowid))
                        {
                            n.F_ID = Convert.ToInt32(rowid);
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


        /// <summary>
        /// 修改操作
        /// </summary>
        private void UpdateNewsInfoByID(WSS.Model.Tasks n)
        {
            if (new WSS.BLL.Tasks().Update(n) == true)
            {
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
            if (new WSS.BLL.Tasks().Add(n) > 0)
            {
                GridPanelNewsList.Reload();
                Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "数据已经添加成功,是否继续添加?", new MessageBox.ButtonsConfig
                {
                    Yes = new MessageBox.ButtonConfig
                    {
                        Handler = "#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{txtTitle}.focus(true);",
                        Text = "确 定"
                    },
                    No = new MessageBox.ButtonConfig
                    {
                        Handler = "#{FormPanelNews}.getForm().reset();#{NewsWindow}.hide();",
                        Text = "取 消"
                    }
                }).Show();
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
                        WSS.Model.Tasks list = new WSS.BLL.Tasks().GetModel(Convert.ToInt32(rowsid[0]));
                        txtTitle.Text = list.F_Title;
                        cbType.Value = list.F_Type;

                        cbFrom.Value = list.F_From;
                        cbJinjiLevel.Value = list.F_JinjiLevel;
                        cbGameName.Value = list.F_GameName;
                        ////list.F_DateTime = DateTime.Now;
                        txtTelphone.Text = list.F_Telphone;
                        cbDutyMan.Value = list.F_DutyMan;
                        cbDutyMan.Value = 2;
                        return list.F_Note;
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

        /// <summary>
        /// 获取选中行并返回ID数组
        /// </summary>
        /// <returns></returns>
        private string GetSelectRowID()
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
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete_Click(object sender, AjaxEventArgs e)
        {
            string rowid = GetSelectRowID();

            if (!string.IsNullOrEmpty(rowid))
            {
                Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "您确定要删除选中的数据项吗?", new MessageBox.ButtonsConfig
                {
                    Yes = new MessageBox.ButtonConfig
                    {
                        Handler = "Coolite.AjaxMethods.DeleteNews(" + rowid + ");",
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
        public void DeleteNews(string IDList)
        {
            if (new WSS.BLL.Tasks().DeleteList(IDList) == true)
            {
                GridPanelNewsList.Reload();
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgTitle"], "数据删除成功!").Show();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], ConfigurationManager.AppSettings["ErrorMessage"]).Show();
            }
        }
        protected void Search_Click(object sender, AjaxEventArgs e)
        {
            if (!String.IsNullOrEmpty(tftitle.Text.Trim()))
            {
                wherestr += " and F_Title like '%"+tftitle.Text.Trim()+"%'";
            }
            if (!String.IsNullOrEmpty(tfnote.Text.Trim()))
            {
                
                wherestr += " and F_Title like '%" + tfnote.Text.Trim() + "%'";
            }
            LoadData(wherestr);

        }
        protected void SearchAll_Click(object sender, AjaxEventArgs e)
        {
            tftitle.Text = "";
            tfnote.Text = "";
            wherestr = "1=1";
            LoadData(wherestr);
        }

    }

}
