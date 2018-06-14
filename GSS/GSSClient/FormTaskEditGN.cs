using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;
using GSSUI;

namespace GSSClient
{
    public partial class FormTaskEditGN : GSSUI.AForm.FormMain
    {
        #region 私有变量
        /// <summary>
        /// 客户端处理类
        /// </summary>
        private ClientHandles _clihandle;
        /// <summary>
        /// 工单编号
        /// </summary>
        int _taskid = 0;
        /// <summary>
        /// 工单实体类
        /// </summary>
        GSSModel.Tasks model = new GSSModel.Tasks();
        /// <summary>
        /// 是否更改过数据
        /// </summary>
        bool _IsChange = false;
        /// <summary>
        /// 是否更改过工单状态
        /// </summary>
        bool _IsStateChange = false;
        /// <summary>
        /// 是否更改过工单状态(返回用)
        /// </summary>
        bool _IsChangeA = false;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormTaskEditGN(ClientHandles clihandle, int taskid)
        {
            InitializeComponent();
            _clihandle = clihandle;
            _taskid = taskid;
            InitElementText();
            SetGameUR();
        }
        void InitElementText()
        {
            label1.Text ="【"+ LanguageResource.Language.LblCountDown+"】";
            label16.Text = "【" + LanguageResource.Language.Tip_TimeoutTip+"】";
            label3.Text = "【" + LanguageResource.Language.LblVipLevel + "】";
            groupBox1.Text = LanguageResource.Language.GPWorkOrderInfo;
            groupBox3.Text = LanguageResource.Language.GBWorkOrderDealwith;
            panelMain.Text = LanguageResource.Language.GBWorkOrderDealwith;
            panelMain.ResetText();
            this.Text = LanguageResource.Language.GBWorkOrderDealwith + "--" + LanguageResource.Language.LblSystemName;
            this.Refresh();
            btnDosure.Text = LanguageResource.Language.BtnSure;
            lblPower.Text = LanguageResource.Language.Tip_ToDo;
            rbtnState1.Text = LanguageResource.Language.BtnStartDealwith;
            rbtnState2.Text = LanguageResource.Language.BtnTurnOut;
            rbtnState3.Text = LanguageResource.Language.BtnFinshAndFeedback;
            rbtnState4.Text = LanguageResource.Language.BtnFinshNeedScore;
            rbtnState5.Text = LanguageResource.Language.BtnFinshWorkOrder;
            rbtnState6.Text = LanguageResource.Language.BtnCloseWorkOrder;
            rbtnState8.Text = LanguageResource.Language.BtnWaitLeaderAudit;
            rbtnState7.Text = LanguageResource.Language.BtnRemark;
            groupBox8.Text = LanguageResource.Language.GBRemarkRecord;
            groupBoxTool1.Text = LanguageResource.Language.GBToolList;
            button16.Text = LanguageResource.Language.BtnQueryFDBILog;
            button17.Text = LanguageResource.Language.BtnFileQueryTool;
            button18.Text = LanguageResource.Language.BtnEquipGetbackTool;
            button20.Text = LanguageResource.Language.BtnRoleOperateRecord;
        }
        #region 事件
        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 工单修改提交
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //数据检验
            string strErr = "";
            if (lblNoteMust.Visible && this.rtxtF_Note.Text.Trim().Length == 0)
            {
                foreach (Control control in flowLayoutPanelF_State.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.RadioButton" && (control as System.Windows.Forms.RadioButton).Checked)
                    {
                        if ((control as System.Windows.Forms.RadioButton).Checked)
                        {

                            strErr += LanguageResource.Language.Tip_InputInWorkword + (control as System.Windows.Forms.RadioButton).Text + LanguageResource.Language.Tip_ReasonOrText + "!\n";
                            break;
                        }


                    }
                }
            }

            if (rbtnState2.Checked && cboxF_DutyMan.SelectedIndex == 0)
            {
                strErr += LanguageResource.Language.Tip_SelectTurnToResponsible+"!\n";
            }

            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //数据准备
            string F_Note = this.rtxtF_Note.Text.Trim();
            int? F_State = GetNextState();
            // string F_Telphone = this.tboxTelephone.Text.Trim() == TrimNull(model.F_Telphone).Trim() ? null : this.tboxTelephone.Text.Trim();
            int? dutyman = model.F_DutyMan;
            int? predutyman = model.F_PreDutyMan;
            if (cboxF_DutyMan.Visible && cboxF_DutyMan.Text != dutyman.ToString())
            {
                predutyman = dutyman;
                dutyman = int.Parse(cboxF_DutyMan.SelectedValue.ToString());
            }
            else if (rbtnState0.Visible && rbtnState0.Checked)
            {
                dutyman = int.Parse(ShareData.UserID);
            }
            else
            {
                predutyman = null;
                dutyman = null;
            }

            //数据保存
            GSSModel.Tasks modelN = new GSSModel.Tasks();
            modelN.F_ID = _taskid;
            modelN.F_Note = F_Note;
            modelN.F_State = F_State;
            // modelN.F_Telphone = F_Telphone;
            modelN.F_DutyMan = dutyman;
            modelN.F_PreDutyMan = predutyman;
            modelN.F_EditMan = int.Parse(ShareData.UserID);
            modelN.F_EditTime = DateTime.Now;
            // _clihandle.EditTask(modelN);

            ComitDoControl(false);


            string backStr = _clihandle.EditTaskSyn(modelN);

            if (backStr == "0")
            {
                MsgBox.Show(LanguageResource.Language.BtnWorkorderDealwithNo, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                _IsChange = true;
                SetGameUR();
                MsgBox.Show(LanguageResource.Language.BtnWorkorderDealwithOK, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ComitDoControl(true);
        }

        /// <summary>
        /// 复制基本信息
        /// </summary>
        private void label6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(aRichTextBoxCode.Text + "\n");
            MsgBox.Show("公告内容已经复制到剪切板!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 工单操作选中事件
        /// </summary>
        private void rbtnState8_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton cboxactiv = (System.Windows.Forms.RadioButton)sender;
            string cboxname = cboxactiv.Name;

            if (!cboxactiv.Checked)
            {
                return;
            }
            int y = 0;
            if (cboxNowState.Text == "处理中")
            {
                y = 20;
            }
            switch (cboxname)
            {
                case "rbtnState0"://"接收":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = false;

                    panelTaskNote.Location = new Point(4, 86);
                    panelTaskNote.Height = 162;
                    break;
                case "rbtnState1"://"开始处理":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = false;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                case "rbtnState2"://"转向":
                    panelNextDutyMan.Visible = true;
                    panelNextDutyMan.Location = new Point(6, 105 - 20 + y);
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 114 + y);
                    panelTaskNote.Height = 134 - y;
                    break;
                case "rbtnState3"://"完成需反馈":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                case "rbtnState4"://"完成需评分":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                case "rbtnState5"://"完成工单":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = true;
                    panelNum.Location = new Point(6, 35 + 56 + y);
                    lblNoteMust.Visible = false;

                    panelTaskNote.Location = new Point(4, 114 + y);
                    panelTaskNote.Height = 134 - y;
                    break;
                case "rbtnState6"://"关闭工单":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                case "rbtnState8"://"请领导审核":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                case "rbtnState7"://"仅备注":
                    panelNextDutyMan.Visible = false;
                    panelNum.Visible = false;
                    lblNoteMust.Visible = true;

                    panelTaskNote.Location = new Point(4, 86 + y);
                    panelTaskNote.Height = 162 - y;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 评分鼠标悬于上面事件
        /// </summary>
        private void label25_MouseHover(object sender, EventArgs e)
        {

            System.Windows.Forms.Label llbactiv = (System.Windows.Forms.Label)sender;
            string nostr = llbactiv.Name.Substring(llbactiv.Name.Length - 1, 1);
            int noactiv = Convert.ToInt32(nostr);
            lblTotalNum.Text = (noactiv + 1).ToString();

            foreach (Control ctrl in panelNum.Controls)
            {
                if (ctrl.Name.IndexOf("lblNum") < 0)
                {
                    continue;
                }
                string ctrlname = ((System.Windows.Forms.Label)ctrl).Name;
                int no = Convert.ToInt32(ctrlname.Substring(ctrlname.Length - 1, 1));
                if (no <= noactiv)
                {
                    ((System.Windows.Forms.Label)ctrl).Text = "★";
                }
                else
                {
                    ((System.Windows.Forms.Label)ctrl).Text = "☆";
                }

            }
            panelNum.Refresh();
        }

        /// <summary>
        /// 选中部门事件
        /// </summary>
        private void cboxF_DutyManDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxF_DutyManDept.SelectedIndex > 0)
            {
                BindUser(cboxF_DutyMan, cboxF_DutyManDept.SelectedValue.ToString());
            }
            else
            {
                cboxF_DutyMan.SelectedValue = 0;
            }

        }

        /// <summary>
        /// 允许编辑下一步职责人
        /// </summary>
        private void ckboxEditNUser_CheckedChanged(object sender, EventArgs e)
        {
            if (ckboxEditNUser.Checked)
            {
                cboxF_DutyManDept.Enabled = true;
                cboxF_DutyMan.Enabled = true;
            }
            else
            {
                cboxF_DutyManDept.Enabled = false;
                cboxF_DutyMan.Enabled = false;
            }

        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        private void FormTaskEditGN_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            if (_IsStateChange)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (_IsChangeA)
            {
                //刷新列表数据
                _clihandle.GetAllTasks("", ShareData.TaskListRequstWhere);
            }
        }

        /// <summary>
        /// 定时器:刷新倒计时
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //工单限时,倒计时
            string timStr = lblLimitDate.Text;
            if (!WinUtil.IsDateTime(timStr))
            {
                return;
            }
            DateTime limtime = Convert.ToDateTime(timStr);
            TimeSpan ts = limtime.Subtract(DateTime.Now);
            lblLimitTime.Text = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
            if (ts.Seconds < 0)
            {
                lblLimitTime.BackColor = Color.Red;
                lblLimitTime.ForeColor = Color.White;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void SetGameUR()
        {

            string userinfo = "";
            BindDept(cboxF_DutyManDept, "0");
            BindUser(cboxF_DutyMan, "-1");

            if (_IsChange)
            {
                _IsChangeA = true;
                _clihandle.GetAllTasksSyn(" and F_ID=" + _taskid + "");
                _IsChange = false;
            }
            model = ClientCache.GetTaskModel(_taskid);

            if (model != null)
            {
                userinfo +=LanguageResource.Language.LblWorkOrderNo+":" + model.F_ID + " ";
                userinfo += LanguageResource.Language.LblWorkOrderType+":" + ClientCache.GetDicPCName(model.F_Type.ToString()) + "\n";
                // userinfo += "工单标题:" + model.F_Title+ "\n";

                userinfo += LanguageResource.Language.LblWorkOrderSatue+":" + ClientCache.GetDicName(model.F_State.ToString()) + " ";
                userinfo += LanguageResource.Language.LblWorkOrderSource+":" + ClientCache.GetDicName(model.F_From.ToString()) + " \n";
                userinfo += LanguageResource.Language.LblPreviousPersonInCharge+":" + ClientCache.GetUserNameT(model.F_PreDutyMan.ToString()) + "\n";
                userinfo += LanguageResource.Language.LblCurrentPersonInCharge+":" + ClientCache.GetUserNameT(model.F_DutyMan.ToString()) + "\n";

                lblLimitDate.Text = TrimNull(model.F_LimitTime);
                lblVip.Text = ClientCache.GetDicName(model.F_VipLevel.ToString());



                labelTitle.Text = model.F_Title;
                labelCreator.Text = model.F_GPeopleName;

                labelTelephone.Text = model.F_Telphone;


                cboxNowState.Text = ClientCache.GetDicName(TrimNull(model.F_State.ToString()));


                ComitDoControlGN(Convert.ToBoolean(model.F_OCanRestor));


                #region 状态显示

                rbtnState0.Visible = false;
                rbtnState1.Visible = false;
                rbtnState2.Visible = false;
                rbtnState3.Visible = false;
                rbtnState4.Visible = false;
                rbtnState5.Visible = false;
                rbtnState6.Visible = false;
                rbtnState7.Visible = false;
                rbtnState8.Visible = false;


                string taskstate = cboxNowState.Text;

                //权限检测
                if (model.F_DutyMan.ToString() != ShareData.UserID && taskstate != "等待处理")
                {
                    taskstate = "工单关闭";
                    lblPowerNull.Visible = true;
                }
                else
                {
                    lblPowerNull.Visible = false;
                }

                switch (taskstate)
                {
                    case "等待处理":
                        rbtnState0.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState0.Checked = true;
                        break;
                    case "已经接收":
                        rbtnState1.Visible = true;
                        rbtnState2.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState1.Checked = true;
                        break;
                    case "处理中":
                        rbtnState2.Visible = true;
                        rbtnState3.Visible = true;
                        rbtnState4.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState8.Visible = true;
                        rbtnState3.Checked = true;
                        break;
                    case "已经转向":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        rbtnState7.Text = "仅备注(要求或信息)";
                        break;
                    case "等待反馈":
                        rbtnState4.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState4.Checked = true;
                        rbtnState4.Text = "已反馈并需评分";
                        break;
                    case "等待评分":
                        rbtnState5.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState5.Checked = true;
                        rbtnState5.Text = "评分并完成工单";
                        break;
                    case "需领导审核":
                        rbtnState2.Visible = true;
                        rbtnState5.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState2.Checked = true;
                        rbtnState2.Text = "审核通过并转向";
                        SetNextUser(model.F_PreDutyMan);
                        cboxF_DutyManDept.Enabled = false;
                        cboxF_DutyMan.Enabled = false;
                        ckboxEditNUser.Visible = true;
                        break;
                    case "工单完成":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        break;
                    case "工单关闭":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        break;
                    default:
                        break;
                }

            }
                #endregion

            flowLayoutPanelF_State.Location = new Point(9, 55);

            lblTaskinfo.Text = userinfo;

            //权限
            //foreach (System.Windows.Forms.Control control in this.groupBoxTool0.Controls)
            //{
            //    if (control.GetType().ToString() == "System.Windows.Forms.Button" || control.GetType().ToString() == "GSSUI.AControl.AButton.AButton")
            //    {
            //        System.Windows.Forms.Button toolbtnActiv = (control as System.Windows.Forms.Button);
            //        toolbtnActiv.Enabled = false;
            //        if (ShareData.UserPower.IndexOf("," + toolbtnActiv.Name + ",") >= 0)
            //        {
            //            toolbtnActiv.Enabled = true;
            //        }
            //    }
            //}

            aRichTextBoxCode.Text = model.F_URInfo;
            SetTreeview();
            if (model.F_TUseData != null && model.F_TUseData.Trim().Length > 0)
            {
                string[] areas = model.F_TUseData.Split('|');
                foreach (string value in areas)
                {
                    string[] value1 = value.Split(',');
                    SetTreeValue(value1[0], value1[1], value1[2]);
                }
            }

            DataSet dsTaskLog = _clihandle.GetTaskLogSyn(" and F_ID=" + _taskid + "");
            SetTaskLogValue(dsTaskLog.Tables[0]);

        }

        /// <summary>
        /// 设置接收范围树形的值
        /// </summary>
        /// <returns></returns>
        private void SetTreeValue(string a, string b, string c)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode Root in treeViewArea.Nodes)
            {
                if (Root.Tag.ToString() == a || a == "-1")
                {
                    if (b != "-1")
                    {
                        treeViewArea.SetTreeNodeStateC(Root, GSSUI.AControl.AThirTreeView.CheckBoxState.Indeterminate);
                    }
                    else
                    {
                        treeViewArea.SetTreeNodeStateC(Root, GSSUI.AControl.AThirTreeView.CheckBoxState.Checked);
                    }
                    foreach (TreeNode item in Root.Nodes)
                    {
                        if (item.Tag.ToString() == b || b == "-1")
                        {
                            if (c != "-1")
                            {
                                treeViewArea.SetTreeNodeStateC(item, GSSUI.AControl.AThirTreeView.CheckBoxState.Indeterminate);
                            }
                            else
                            {
                                treeViewArea.SetTreeNodeStateC(item, GSSUI.AControl.AThirTreeView.CheckBoxState.Checked);
                            }
                            foreach (TreeNode item1 in item.Nodes)
                            {
                                if (item1.Tag.ToString() == c || c == "-1")
                                {
                                    treeViewArea.SetTreeNodeStateC(item1, GSSUI.AControl.AThirTreeView.CheckBoxState.Checked);
                                }

                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 得到接收范围树形的值
        /// </summary>
        /// <returns></returns>
        private string GetTreeValue()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode Root in treeViewArea.Nodes)
            {
                if (treeViewArea.GetTreeNodeCheckBoxState(Root) == GSSUI.AControl.AThirTreeView.CheckBoxState.Checked)
                {
                    sb.Append(string.Format("{0},{1},{2}|", Root.Tag.ToString(), "-1", "-1"));
                }
                else if (treeViewArea.GetTreeNodeCheckBoxState(Root) == GSSUI.AControl.AThirTreeView.CheckBoxState.Indeterminate)
                {
                    foreach (TreeNode item in Root.Nodes)
                    {
                        if (treeViewArea.GetTreeNodeCheckBoxState(item) == GSSUI.AControl.AThirTreeView.CheckBoxState.Checked)
                        {
                            sb.Append(string.Format("{0},{1},{2}|", Root.Tag.ToString(), item.Tag.ToString(), "-1"));
                        }
                        else if (treeViewArea.GetTreeNodeCheckBoxState(item) == GSSUI.AControl.AThirTreeView.CheckBoxState.Indeterminate)
                        {
                            foreach (TreeNode item1 in item.Nodes)
                            {
                                if (treeViewArea.GetTreeNodeCheckBoxState(item1) == GSSUI.AControl.AThirTreeView.CheckBoxState.Checked)
                                {
                                    sb.Append(string.Format("{0},{1},{2}|", Root.Tag.ToString(), item.Tag.ToString(), item1.Tag.ToString()));
                                }
                            }
                        }
                    }
                }
            }
            string backstr = sb.ToString();
            if (backstr.Length > 1)
            {
                backstr = backstr.Substring(0, backstr.Length - 1);
            }
            return backstr;
        }

        /// <summary>
        /// 得到单节值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetNodeValue(string a, string b, string c)
        {
            string backstr = "";
            foreach (TreeNode Root in treeViewArea.Nodes)
            {
                if (Root.Tag.ToString() == a)
                {
                    backstr = Root.Text;
                    foreach (TreeNode item in Root.Nodes)
                    {
                        if (item.Tag.ToString() == b)
                        {
                            backstr += "/" + item.Text;
                            foreach (TreeNode item1 in item.Nodes)
                            {
                                if (item1.Tag.ToString() == c)
                                {
                                    backstr += "/" + item1.Text;
                                }
                                if (c == "-1" && backstr.IndexOf(LanguageResource.Language.LblAllZoneLine) < 0)
                                {
                                    backstr += "/" + LanguageResource.Language.LblAllZoneLine;
                                }
                            }
                        }
                        else if (b == "-1" && backstr.IndexOf(LanguageResource.Language.LblAllZone) < 0)
                        {
                            backstr += "/" + LanguageResource.Language.LblAllZone;
                        }
                    }
                }
                else if (a == "-1" && backstr.IndexOf(LanguageResource.Language.LblAllBigZone) < 0)
                {
                    backstr = LanguageResource.Language.LblAllBigZone;
                }
            }
            if (backstr.Split('/').Length == 1)
            {
                backstr += "/" + LanguageResource.Language.LblAllZone + "/" + LanguageResource.Language.LblAllZoneLine;
            }
            if (backstr.Split('/').Length == 2)
            {
                backstr += "/" + LanguageResource.Language.LblAllZoneLine;
            }
            return backstr;
        }

        private void SetTreeview()
        {
            treeViewArea.Nodes.Clear();
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {

                DataRow[] drdic2 = ds.Tables["T_GameConfig"].Select("F_ParentID="+SystemConfig.BigZoneParentId);
                TreeNode node = null;
                foreach (DataRow dr2 in drdic2)
                {
                    node = treeViewArea.AddTreeNode(treeViewArea.Nodes, dr2["F_Name"].ToString(), false);
                    node.Tag = dr2["F_ValueGame"].ToString();
                    InitTreeView(ds, node, dr2["F_ID"].ToString());
                }
            }

            for (int i = 0; i < treeViewArea.Nodes.Count; i++)
            {
                treeViewArea.Nodes[i].Expand();
            }
        }

        private void InitTreeView(DataSet ds, TreeNode panode, string pnodestr)
        {
            DataRow[] drdic2 = ds.Tables["T_GameConfig"].Select("F_ParentID=" + pnodestr + "");
            TreeNode node = null;
            foreach (DataRow dr2 in drdic2)
            {
                node = treeViewArea.AddTreeNode(panode.Nodes, dr2["F_Name"].ToString(), false);
                node.Tag = dr2["F_ValueGame"].ToString();
                InitTreeView(ds, node, dr2["F_ID"].ToString());
            }
        }

        /// <summary>
        /// 设置下一步默认职责人
        /// </summary>
        private void SetNextUser(int? value)
        {
            if (value == null)
            {
                return;
            }
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataRow[] drUser = ds.Tables["T_Users"].Select("F_UserID=" + value.ToString() + "");
                if (drUser.Length > 0)
                {
                    cboxF_DutyManDept.SelectedValue = drUser[0]["F_DepartID"].ToString();
                    cboxF_DutyMan.SelectedValue = drUser[0]["F_UserID"].ToString(); ;
                }
            }
        }

        //工单历史控件的委托
        private delegate void SetRichtxtValue(DataTable dt);

        /// <summary>
        /// 设置工单历史
        /// </summary>
        /// <param name="dt"></param>
        private void SetTaskLogValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetRichtxtValue d = new SetRichtxtValue(SetTaskLogValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                string _taskNoteLog = "";
                string changStrColor = "";
                richtbTasklog.Clear();
                try
                {
                    StringBuilder changStr = new StringBuilder();
                    //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                    _taskNoteLog = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ";
                    string colorlistStr = "";

                    foreach (DataRow dr in dt.Rows)
                    {
                        changStr.Append(@"\pard{\b[" + LanguageResource.Language.LblTime + "]:}" + dr["F_EditTime"] + @"\par ");
                        changStr.Append(@"{\b ["+LanguageResource.Language.LblUser+"]:}" + dr["F_EditMan"] + @"\par ");

                        string dowhatStr =  LanguageResource.Language.LblDataChange;
                        if (dr["F_State"].ToString().Trim().Length > 0)
                        {
                            dowhatStr = @"\cf1\b" + LanguageResource.Language.LblWorkOrderStatueChange + @"\b0\cf0 ";
                        }
                        else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                        {
                            dowhatStr = @"\cf3\b" + LanguageResource.Language.LblToolUse + @"\b0\cf0 ";
                        }

                        changStr.Append(@"{\b ["+LanguageResource.Language.LblOperate+"]:}" + dowhatStr + @"\par ");
                        changStr.Append(@"{\b ["+LanguageResource.Language.LblData+@"]:}\par\pard\li57\ri57 ");


                        //用户名F_State
                        string[] colms = { LanguageResource.Language.LblWorkOrderSatue+"|F_State", LanguageResource.Language.LblCurrentPersonInCharge+"|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge+"|F_PreDutyMan",LanguageResource.Language.LblTitle+"|F_Title", LanguageResource.Language.LblSource+"|F_From",LanguageResource.Language.LblVipLevel+"|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun+"|F_LimitTime", LanguageResource.Language.LblType+"|F_Type", LanguageResource.Language.LblGame+"|F_GameName",  LanguageResource.Language.LblBigZone + "|F_GameBigZone",  LanguageResource.Language.LblZone+ "|F_GameZone",  LanguageResource.Language.LblAccount+ "|F_GUserName",  LanguageResource.Language.LblRole+ "|F_GRoleName", LanguageResource.Language.LblContacter+ "|F_GPeopleName",  LanguageResource.Language.LblTel+ "|F_Telphone",  LanguageResource.Language.LblSureAccount+ "|F_CUserName",  LanguageResource.Language.LblSureSecurity+ "|F_CPSWProtect", LanguageResource.Language.LblToolRecord+ "|F_COther",  LanguageResource.Language.LblLastLoginTime+ "|F_OLastLoginTime",  LanguageResource.Language.LblOftenGameLocation+ "|F_OAlwaysPlace",  LanguageResource.Language.LblRemark+ "|F_Note" };
                        foreach (string colm in colms)
                        {
                            if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                            {

                                changStr.Append(@"[" + colm.Split('|')[0] + @"]{\cf2  " + dr[colm.Split('|')[1]].ToString().Trim().Replace("\n", @"\par") + @" }\par ");

                            }

                        }

                        string F_userdata = dr["F_TUseData"].ToString();
                        if (F_userdata.Trim().Length > 0)
                        {
                            string[] uds = F_userdata.Split('|');
                            string gnerea = "";

                            foreach (string ud in uds)
                            {
                                string[] u = ud.Split(',');
                                if (u.Length == 3)
                                {
                                    gnerea += GetNodeValue(u[0], u[1], u[2]) + @"|\par";
                                }
                                else if (ud.Trim().Length > 0)
                                {
                                    gnerea += ud + @"|\par";
                                }
                            }
                            changStr.Append("[" + LanguageResource.Language.LblNoticeRange + @"]\par{\cf2" + gnerea + @"}");
                        }

                        string Gnotice = dr["F_URInfo"].ToString().Trim().Replace("\n", @"\par\highlight0################################\highlight0\par");

                        if (Gnotice.Trim().Length > 0)
                        {

                            //转换颜色
                            string codeBeginStr = "";
                            Gnotice = Gnotice.Replace("&199", "}");
                            System.Collections.ArrayList colorlist = new System.Collections.ArrayList();

                            for (int i = 0; i < Gnotice.Length - 10; i++)
                            {
                                if (Gnotice.Substring(i, 2) == "&2")
                                {
                                    codeBeginStr = Gnotice.Substring(i + 2, 9);
                                    if (WinUtil.IsNumber(codeBeginStr))
                                    {
                                        colorlist.Add(codeBeginStr);
                                    }
                                }
                            }
                            int colorindex = 4;
                            foreach (object colora in colorlist)
                            {
                                colorlistStr += "\\red" + colora.ToString().Substring(0, 3) + "\\green" + colora.ToString().Substring(3, 3) + "\\blue" + colora.ToString().Substring(6, 3) + ";";
                                Gnotice = Gnotice.Replace("&2" + colora.ToString(), @"{\cf" + colorindex.ToString());
                                colorindex++;
                            }

                            changStr.Append(LanguageResource.Language.LblNoticeText + @"]\par" + Gnotice + @" \par");
                        }
                        if (dr["F_OCanRestor"].ToString().Trim().Length > 0)
                        {
                            string istooluse = dr["F_OCanRestor"].ToString() == "True" ? LanguageResource.Language.LblStartRunning : LanguageResource.Language.LblEndRunning;
                            changStr.Append(@"\b[" + LanguageResource.Language.LblRunStatue + "]" + istooluse + @" \b0\par");
                        }


                        changStr.Append(@"\highlight0---------------------------------\highlight0\par ");


                        if (dr["F_Note"].ToString().Trim().Length > 0)
                        {

                            _taskNoteLog += @"\pard{\b[" + LanguageResource.Language.LblTime + "]:}" + dr["F_EditTime"] + @"\par ";
                            _taskNoteLog += @"{\b ["+LanguageResource.Language.LblUser+"]:}" + dr["F_EditMan"] + @"\par ";
                            _taskNoteLog += @"{\b[备注]:}\par{\cf2" + dr["F_Note"] + @"}\par ";
                            _taskNoteLog += @"\highlight0---------------------------------\highlight0\par ";
                        }

                    }
                    changStr.Append(@"}");
                    _taskNoteLog += @"}";

                    changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\lang2052\f0\fs18 " + changStr.ToString();
                    richtbTasklog.Rtf = changStrColor;
                    richtbnotelog.Rtf = _taskNoteLog;


                }
                catch (System.Exception ex)
                {
                    richtbTasklog.AppendText(LanguageResource.Language.Tip_Tip+":" + LanguageResource.Language.Tip_ErrorGetWorkOrderHistory + "!" + ex.Message);
                    //日志记录
                    ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                }

                richtbTasklog.SelectionStart = richtbTasklog.TextLength;
                richtbTasklog.ScrollToCaret();
                richtbnotelog.SelectionStart = richtbnotelog.TextLength;
                richtbnotelog.ScrollToCaret();

            }
        }

        /// <summary>
        /// 得到提交后的下一步工单状态
        /// </summary>
        /// <returns></returns>
        private int? GetNextState()
        {
            int? stateid = null;
            string ckbname = "";
            foreach (Control control in flowLayoutPanelF_State.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.RadioButton" && (control as System.Windows.Forms.RadioButton).Checked)
                {

                    ckbname = (control as System.Windows.Forms.RadioButton).Name;
                    break;
                }
            }
            switch (ckbname)
            {
                case "rbtnState0":
                    stateid = 100100101;
                    break;
                case "rbtnState1":
                    stateid = 100100102;
                    break;
                case "rbtnState2":
                    stateid = 100100103;
                    break;
                case "rbtnState3":
                    stateid = 100100104;
                    break;
                case "rbtnState4":
                    stateid = 100100105;
                    break;
                case "rbtnState5":
                    stateid = 100100107;
                    break;
                case "rbtnState6":
                    stateid = 100100108;
                    break;
                case "rbtnState7":
                    stateid = null;
                    break;
                case "rbtnState8":
                    stateid = 100100106;
                    break;
                default:
                    break;
            }
            return stateid;
        }

        /// <summary>
        /// 转空对象为空字符串
        /// </summary>
        private string TrimNull(object value)
        {
            if (value == null)
            {
                return " ";
            }
            return value.ToString();
        }

        /// <summary>
        /// 转字符串为布尔值
        /// </summary>
        private bool StringtoBool(string value)
        {
            if (value == "True" || value == "1")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        private void ComitDoControl(bool isback)
        {
            if (isback)
            {
                btnDosure.Enabled = true;
                btnDoesc.Enabled = true;
            }
            else
            {
                btnDosure.Enabled = false;
                btnDoesc.Enabled = false;
            }

        }

        /// <summary>
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        private void ComitDoControlGN(bool isback)
        {
            if (isback)
            {
                aButtonGNStop.Enabled = true;
                aButtonGNRun.Enabled = false;
                lblGNTips.Text = "注:正在运行的公告内容请查看工单记录!";
            }
            else
            {
                aButtonGNStop.Enabled = false;
                aButtonGNRun.Enabled = true;
                lblGNTips.Text = "注:将运行的公告内容为工单记录最后一条公告内容!";
            }

        }


        /// <summary>
        /// 绑定用户
        /// </summary>
        private void BindUser(ComboBox cb, string value)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtuser = ds.Tables["T_Users"].Clone();
                DataRow dru = dtuser.NewRow();
                dru["F_UserName"] = LanguageResource.Language.Tip_SelectResponsiblePerson;
                dru["F_UserID"] = "0";
                dtuser.Rows.Add(dru);
                DataRow[] druser = ds.Tables["T_Users"].Select(" F_DepartID=" + value + " ");
                foreach (DataRow dr in druser)
                {
                    if (dr["F_RealName"].ToString().Trim().Length > 0)
                    {
                        dr["F_UserName"] += "[" + dr["F_RealName"] + "]";
                    }
                    dtuser.ImportRow(dr);
                }
                cb.DataSource = dtuser;
                cb.DisplayMember = "F_UserName";
                cb.ValueMember = "F_UserID";
                cb.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindDept(ComboBox cb, string value)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdept = ds.Tables["T_Department"].Clone();
                DataRow dra = dtdept.NewRow();
                dra["F_DepartName"] = LanguageResource.Language.Tip_SelectDepart;
                dra["F_DepartID"] = "0";
                dtdept.Rows.Add(dra);
                DataRow[] drdept = ds.Tables["T_Department"].Select("F_ParentID=" + value + "");
                foreach (DataRow dr in drdept)
                {
                    dtdept.ImportRow(dr);
                }
                cb.DataSource = dtdept;
                cb.DisplayMember = "F_DepartName";
                cb.ValueMember = "F_DepartID";
                cb.SelectedIndex = 0;

            }
        }

        //窗口消息的委托
        private delegate void Setfrommsg(int handid, int msgid, int recv);
        private void SetFormsMSG(int handid, int msgid, int recv)
        {
            if (this.InvokeRequired)
            {
                Setfrommsg d = new Setfrommsg(SetFormsMSG);
                object hid = handid;
                object arg = msgid;
                object rec = recv;
                this.Invoke(d, hid, arg, rec);
            }
            else
            {
                GSSCSFrameWork.FormsMsg.PostMessage(handid, msgid, recv, 0);
            }
        }
        #endregion

        #region 工具类事件

        /// <summary>
        /// 工具类事件:帐号封停
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);
            string Rname = TrimNull(model.F_GRoleName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGuserLock form = new FormToolGuserLock(_clihandle, 1, model.F_ID.ToString(), Uname, Rname);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 工具类事件:角色封停
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);
            string Rname = TrimNull(model.F_GRoleName);
            if (Rname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkOrderLoseAccountOrRoleWithInvalid, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormToolGuserLock form = new FormToolGuserLock(_clihandle, 2, model.F_ID.ToString(), Uname, Rname);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }


        #endregion

        private void aButton3_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show("确定要运行当前工单的公告吗?", LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            this.Invalidate();
            Application.DoEvents();
            CommitTask(true);
        }
        private void aButton2_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show("确定要停止当前工单的公告吗?", LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            Application.DoEvents();
            CommitTask(false);
        }

        /// <summary>
        /// 提交工单
        /// </summary>
        private void CommitTask(bool gnstate)
        {
            //string Title = tboxTitle.Text.Trim();
            //string gpeoplename = tboxCreator.Text.Trim();
            //string telephone = tboxTelephone.Text.Trim();
            string Note = rtxtF_Note.Text.Trim();
            //int From = 100103100;//客服中心

            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = aRichTextBoxCode.Text;
            string ReceivArea = GetTreeValue();


            string strErr = "";

            //if (Title.Length == 0)
            //{
            //    strErr += "公告工单标题不能为空!\n";
            //}
            //if (gpeoplename.Length == 0)
            //{
            //    strErr += LanguageResource.Language.LblInitiatorNameIsRequire+"!\n";
            //}
            //if (telephone.Trim().Length < 6)
            //{
            //    strErr += LanguageResource.Language.LblTelFormIsError+"!\n";
            //}

            if (ReceivArea.Trim().Length == 0)
            {
                strErr += "请选择接收范围!\n";
            }


            int row1 = 0;
            int row2 = 0;
            string[] slines = aRichTextBoxCode.Lines;
            string[] snewlines = new string[slines.Length];


            foreach (string sstr in aRichTextBoxCode.Lines)
            {
                if (sstr.Replace("\n", "").Trim().Length > 0)
                {
                    snewlines[row1] = slines[row2];
                    row1++;
                }
                row2++;
            }
            string[] snewliness = new string[row1];
            Array.Copy(snewlines, 0, snewliness, 0, snewliness.Length);

            aRichTextBoxCode.Lines = snewliness;



            int row = 1;
            foreach (string sstr in aRichTextBoxCode.Lines)
            {
                string[] param = sstr.Split('|');
                if (param.Length != 6)
                {
                    strErr += "公告内容:行" + row + "需要6个参数!\n";
                }
                else
                {
                    if (param[1].Split(',').Length != 2)
                    {
                        strErr += "公告内容:行" + row + "参数2格式不正确!\n";
                    }
                    else
                    {
                        if (!WinUtil.IsNumber(param[1].Split(',')[0]) || !WinUtil.IsNumber(param[1].Split(',')[1]))
                        {
                            strErr += "公告内容:行" + row + "参数2格式不正确!\n";
                        }
                    }
                    if (!WinUtil.IsNumber(param[2]))
                    {
                        strErr += "公告内容:行" + row + "参数3格式不正确!\n";
                    }
                    if (!WinUtil.IsDateTime(param[3]))
                    {
                        strErr += "公告内容:行" + row + "参数4格式不正确!\n";
                    }
                    if (!WinUtil.IsDateTime(param[4]))
                    {
                        strErr += "公告内容:行" + row + "参数5格式不正确!\n";
                    }
                    if (!WinUtil.IsNumber(param[5]))
                    {
                        strErr += "公告内容:行" + row + "参数6格式不正确!\n";
                    }
                }
                row++;
            }


            //if (Note.Trim().Length == 0)
            //{
            //    strErr += LanguageResource.Language.Tip_RemarkNoEmpty+"!\n";
            //}


            //if (strErr != "")
            //{
            //    MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            ComitDoControlGN(gnstate);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = _taskid;
            model.F_EditMan = EditMan;
            model.F_EditTime = EditTime;
            //model.F_URInfo = URInfo;
            //model.F_TUseData = ReceivArea;
            //model.F_Rowtype = Rowtype;
            model.F_TToolUsed = true;
            model.F_OCanRestor = gnstate;

            string backStr = _clihandle.GameNoticeStartSyn(_taskid.ToString());

            if (gnstate)
            {
                backStr = _clihandle.GameNoticeStartSyn(_taskid.ToString());
            }
            else
            {
                backStr = _clihandle.GameNoticeStopSyn(_taskid.ToString());
            }


            string operatStr = gnstate == true ? "运行公告操作" : "停止公告操作";

            if (backStr == "true")
            {
                model.F_COther = operatStr + "成功!";
                _IsChange = true;
                backStr = _clihandle.EditTaskSyn(model);
                SetGameUR();
                MsgBox.Show(operatStr + "成功!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                model.F_COther = operatStr + "失败!" + backStr;
                ComitDoControlGN(!gnstate);
                model.F_OCanRestor = null;
                backStr = _clihandle.EditTaskSyn(model);
                SetGameUR();
                MsgBox.Show(operatStr + "失败!" + backStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void aButtonGNUpdate_Click(object sender, EventArgs e)
        {

            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = aRichTextBoxCode.Text;
            int? Rowtype = 0;
            string ReceivArea = GetTreeValue();

            string strErr = "";


            if (ReceivArea.Trim().Length == 0)
            {
                strErr += "请选择接收范围!\n";
            }


            int row1 = 0;
            int row2 = 0;
            string[] slines = aRichTextBoxCode.Lines;
            string[] snewlines = new string[slines.Length];


            foreach (string sstr in aRichTextBoxCode.Lines)
            {
                if (sstr.Replace("\n", "").Trim().Length > 0)
                {
                    snewlines[row1] = slines[row2];
                    row1++;
                }
                row2++;
            }
            string[] snewliness = new string[row1];
            Array.Copy(snewlines, 0, snewliness, 0, snewliness.Length);

            aRichTextBoxCode.Lines = snewliness;



            int row = 1;
            foreach (string sstr in aRichTextBoxCode.Lines)
            {
                string[] param = sstr.Split('|');
                if (param.Length != 6)
                {
                    strErr += "公告内容:行" + row + "需要6个参数!\n";
                }
                else
                {
                    if (param[1].Split(',').Length != 2)
                    {
                        strErr += "公告内容:行" + row + "参数2格式不正确!\n";
                    }
                    else
                    {
                        if (!WinUtil.IsNumber(param[1].Split(',')[0]) || !WinUtil.IsNumber(param[1].Split(',')[1]))
                        {
                            strErr += "公告内容:行" + row + "参数2格式不正确!\n";
                        }
                    }
                    if (!WinUtil.IsNumber(param[2]))
                    {
                        strErr += "公告内容:行" + row + "参数3格式不正确!\n";
                    }
                    if (!WinUtil.IsDateTime(param[3]))
                    {
                        strErr += "公告内容:行" + row + "参数4格式不正确!\n";
                    }
                    if (!WinUtil.IsDateTime(param[4]))
                    {
                        strErr += "公告内容:行" + row + "参数5格式不正确!\n";
                    }
                    if (!WinUtil.IsNumber(param[5]))
                    {
                        strErr += "公告内容:行" + row + "参数6格式不正确!\n";
                    }
                }
                row++;
            }
            if (model.F_URInfo == URInfo && model.F_TUseData == ReceivArea)
            {
                strErr += "公告内容没有更改,不需要更新!\n";
            }

            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GSSModel.Tasks model0 = new GSSModel.Tasks();
            model0.F_ID = _taskid;
            model0.F_EditMan = EditMan;
            model0.F_EditTime = EditTime;
            model0.F_URInfo = URInfo;
            model0.F_TUseData = ReceivArea;
            model0.F_Rowtype = Rowtype;


            string backStr = _clihandle.EditTaskSyn(model0);

            if (backStr == "0")
            {
                MsgBox.Show("已经更新公告到工单失败!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MsgBox.Show("已经更新公告到工单成功!\n需要重新运行公告才会更新到游戏服务器", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            _IsChange = true;
            SetGameUR();
        }


    }
}
