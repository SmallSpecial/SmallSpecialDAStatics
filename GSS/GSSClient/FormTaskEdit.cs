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
using System.Net;
using System.IO;
using System.Diagnostics;

namespace GSSClient
{
    public partial class FormTaskEdit : GSSUI.AForm.FormMain
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
        public string msg { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormTaskEdit(ClientHandles clihandle, int taskid)
        {
            InitializeComponent();
            _clihandle = clihandle;
            _taskid = taskid;
            InitElementText();
        }
        void InitElementText()
        {
            label1.Text = "【" + LanguageResource.Language.LblCountDown + "】";
            label16.Text = "【" + LanguageResource.Language.Tip_TimeoutTip + "】";
            label3.Text = "【" + LanguageResource.Language.LblVipLevel + "】";
            label4.Text = "【" + LanguageResource.Language.Tip_CreateWorkOrderSnap + "】";
            label7.Text = LanguageResource.Language.LblTel;
            rtbSnapInfo.Text = LanguageResource.Language.Tip_InfoInGame;
            label6.Text = LanguageResource.Language.LblClipBoard;
            groupBox1.Text = LanguageResource.Language.GPWorkOrderInfo;
            groupBox3.Text = LanguageResource.Language.GBWorkOrderDealwith;
            panelMain.Text = LanguageResource.Language.GBWorkOrderDealwith;
            panelMain.ResetText();
            this.Text = LanguageResource.Language.GBWorkOrderDealwith + "--" + LanguageResource.Language.LblSystemName;
            this.Refresh();
            label2.Text = LanguageResource.Language.LblTimeLimit;
            this.label13.Text = LanguageResource.Language.LblLastLoginTime + ":";
            label12.Text = LanguageResource.Language.LblOftenGameLocation;
            groupBox2.Text = LanguageResource.Language.GBQuestionAswerInfo;
            cboxF_OCanRestor.Text = LanguageResource.Language.LblRepairFailure;
            label8.Text = LanguageResource.Language.LblWorkOrderSatue + ":";
            lblTaskState.Text = LanguageResource.Language.LblWorkOrderSatue;
            btnDosure.Text = LanguageResource.Language.BtnSure;
            lblPower.Text = LanguageResource.Language.Tip_ToDo;
            rbtnState0.Text = LanguageResource.Language.BtnReceive;
            rbtnState1.Text = LanguageResource.Language.BtnStartDealwith;
            rbtnState2.Text = LanguageResource.Language.BtnTurnOut;
            rbtnState3.Text = LanguageResource.Language.BtnFinshAndFeedback;
            rbtnState4.Text = LanguageResource.Language.BtnFinshNeedScore;
            rbtnState5.Text = LanguageResource.Language.BtnFinshWorkOrder;
            rbtnState6.Text = LanguageResource.Language.BtnCloseWorkOrder;
            rbtnState8.Text = LanguageResource.Language.BtnWaitLeaderAudit;
            rbtnState7.Text = LanguageResource.Language.BtnRemark;
            lblTaskFlow.Text = LanguageResource.Language.LblWorkOrderFlow;
            this.buttonTETool0.Text = LanguageResource.Language.BtnCloseDownAccount;
            this.buttonTETool1.Text = LanguageResource.Language.BtnUnlockAccount;
            this.buttonTETool2.Text = LanguageResource.Language.BtnCloseDownRole;
            this.buttonTETool3.Text = LanguageResource.Language.BtnUnlockRole;
            groupBox8.Text = LanguageResource.Language.GBRemarkRecord;
            groupBox4.Text = LanguageResource.Language.LblWorkOrderRecord;
            groupBoxTool1.Text = LanguageResource.Language.GBToolList;
            buttonTETool8.Text = LanguageResource.Language.Role_BtnRoleRecovery;
            buttonTETool9.Text = LanguageResource.Language.BtnRoleRename;
            buttonTETool10.Text = LanguageResource.Language.BtnRoleChangeZone;
            buttonTETool4.Text = LanguageResource.Language.BtnBorrowAccount;
            buttonTETool5.Text = LanguageResource.Language.BtnReturnAccount;
            buttonTETool11.Text = LanguageResource.Language.BtnClearOnlineRecord;
            buttonTETool6.Text = LanguageResource.Language.BtnResetAntiIndulgence;
            buttonTETool12.Text = LanguageResource.Language.BtnResetRoleLevelPsw;
            button13.Text = LanguageResource.Language.BtnQueryAwardTool;
            lblPowerNull.Text = LanguageResource.Language.Tip_RefuseDealwithWordorder;
            groupBoxTool.Text = LanguageResource.Language.Tip_GameTool;
            groupBoxTool0.Text = LanguageResource.Language.GBOperateList;
            label11.Text = LanguageResource.Language.LblWorkOrderRemark;
            label10.Text = LanguageResource.Language.Tip_OperateRemark;
            buttonTETool7.Text = LanguageResource.Language.BtnQueryFDBILog;
            button17.Text = LanguageResource.Language.BtnFileQueryTool;
            button18.Text = LanguageResource.Language.BtnEquipGetbackTool;
            button19.Text = LanguageResource.Language.BtnGameUserQuery;
            button20.Text = LanguageResource.Language.BtnRoleOperateRecord;
            label17.Text = LanguageResource.Language.Tip_RealtimeQueryGameUserInfoTool;
        }
        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTaskEdit_Load(object sender, EventArgs e)
        {
            SetGameUR();
        }

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
                strErr += LanguageResource.Language.Tip_SelectTurnToResponsible + "!\n";
            }

            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //数据准备
            string F_Note = this.rtxtF_Note.Text.Trim();
            int? F_State = GetNextState();
            string F_Telphone = this.tboxF_Telphone.Text.Trim() == TrimNull(model.F_Telphone).Trim() ? null : this.tboxF_Telphone.Text.Trim();
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

            if (panelNum.Visible)
            {
                F_Note += string.Format("\r\n【" + LanguageResource.Language.Tip_WorkOrderMarkFormat + "】", GetStarNum());
            }

            //数据保存
            GSSModel.Tasks modelN = new GSSModel.Tasks();
            modelN.F_ID = _taskid;
            modelN.F_Note = F_Note;
            modelN.F_State = F_State;
            modelN.F_Telphone = F_Telphone;
            modelN.F_DutyMan = dutyman;
            modelN.F_PreDutyMan = predutyman;
            modelN.F_EditMan = int.Parse(ShareData.UserID);
            modelN.F_EditTime = DateTime.Now;
            _clihandle.EditTask(modelN);

            ComitDoControl(false);

        }

        /// <summary>
        /// 复制基本信息
        /// </summary>
        private void label6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblTaskinfo.Text + "\n" + rtbSnapInfo.Text);
            MsgBox.Show(LanguageResource.Language.Tip_CopyWorkOrderToClipBoard + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (cboxNowState.Text == LanguageResource.Language.ResourceManager.GetString(typeof(WorkOrderStatueEnum).Name + "_" + WorkOrderStatueEnum.Dealwithing))
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
        /// 获取得分
        /// </summary>
        private int GetStarNum()
        {
            int starnum = 0;
            foreach (Control ctrl in panelNum.Controls)
            {
                if (ctrl.Name.IndexOf("lblNum") < 0)
                {
                    continue;
                }
                if (((System.Windows.Forms.Label)ctrl).Text == "★")
                {
                    starnum++;
                }

            }
            return starnum;
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
        private void FormTaskEdit_FormClosing(object sender, FormClosingEventArgs e)
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
            Application.DoEvents();
            model = ClientCache.GetTaskModel(_taskid);

            DataSet dsTaskLog = _clihandle.GetTaskLogSyn(" and F_ID=" + _taskid + "");
            if (dsTaskLog == null || dsTaskLog.Tables.Count == 0)
            {

                msg = LanguageResource.Language.Tip_TCPError;
                // MsgBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg.Logger();
                return;
            }
            SetTaskLogValue(dsTaskLog.Tables[0]);

            Application.DoEvents();
            if (model != null)
            {
                userinfo += LanguageResource.Language.LblWorkOrderNo + ":" + model.F_ID + " ";
                userinfo += LanguageResource.Language.LblWorkOrderType + ":" + ClientCache.GetDicPCName(model.F_Type.ToString()) + "\n";
                // userinfo += "工单标题:" + model.F_Title+ "\n";

                userinfo += LanguageResource.Language.LblWorkOrderSatue + ":" + ClientCache.GetDicName(model.F_State.ToString()) + " ";
                userinfo += LanguageResource.Language.LblWorkOrderSource + ":" + ClientCache.GetDicName(model.F_From.ToString()) + " \n";
                userinfo += LanguageResource.Language.LblPreviousPersonInCharge + ":" + ClientCache.GetUserNameT(model.F_PreDutyMan.ToString()) + "\n";
                userinfo += LanguageResource.Language.LblCurrentPersonInCharge + ":" + ClientCache.GetUserNameT(model.F_DutyMan.ToString()) + "\n";

                lblLimitDate.Text = TrimNull(model.F_LimitTime);
                lblVip.Text = ClientCache.GetDicName(model.F_VipLevel.ToString());

                rtbSnapInfo.Text = model.F_URInfo;

                tboxF_Telphone.Text = model.F_Telphone;
                tboxF_OLastLoginTime.Text = model.F_OLastLoginTime.ToString();
                tboxF_OAlwaysPlace.Text = model.F_OAlwaysPlace;
                cboxF_OCanRestor.Checked = Convert.ToBoolean(model.F_OCanRestor);
                WorkOrderStatueEnum state = (WorkOrderStatueEnum)Enum.Parse(typeof(WorkOrderStatueEnum), model.F_State.ToString());//using the hashcode query the enum of field
                System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
                string statueDesc = QueryLanguageResource.GetWorkorderStatueDesc(rm, state);//work order statue desription
                if (string.IsNullOrEmpty(statueDesc))
                { //
                    ShareData.Log.Info("please check the GSSDB.dbo.T_Dictionary exists the item id =" + model.F_State + ",or not. Maybe the resx loss the id of item.");
                    MsgBox.Show(string.Format(LanguageResource.Language.Tip_LoseWorkOrderStatueFormat, model.F_State.ToString()),
                        LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cboxNowState.Text = statueDesc; //ClientCache.GetDicName(TrimNull(model.F_State.ToString()));
                Application.DoEvents();

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
                Application.DoEvents();

                string taskstate = cboxNowState.Text;

                //权限检测
                if (model.F_DutyMan.ToString() != ShareData.UserID && taskstate != QueryLanguageResource.GetWorkorderStatueDesc(rm, WorkOrderStatueEnum.Prepare))
                {
                    taskstate = QueryLanguageResource.GetWorkorderStatueDesc(rm, WorkOrderStatueEnum.Recovery);// "工单关闭";
                    lblPowerNull.Visible = true;
                    groupBoxTool.Enabled = false;
                }
                else
                {
                    lblPowerNull.Visible = false;
                    groupBoxTool.Enabled = true;
                }

                switch (state)
                {
                    case WorkOrderStatueEnum.Prepare:
                        rbtnState0.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState0.Checked = true;
                        groupBoxTool.Enabled = false;
                        break;
                    case WorkOrderStatueEnum.NewReceive://"已经接收":
                        rbtnState1.Visible = true;
                        rbtnState2.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState1.Checked = true;
                        break;
                    case WorkOrderStatueEnum.Dealwithing://"处理中":
                        rbtnState2.Visible = true;
                        rbtnState3.Visible = true;
                        rbtnState4.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState8.Visible = true;
                        rbtnState3.Checked = true;
                        break;
                    case WorkOrderStatueEnum.TurnOut://"已经转向":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        rbtnState7.Text = LanguageResource.Language.Tip_OnlyMark_SetRequireOrInfo;
                        break;
                    case WorkOrderStatueEnum.WaitFeedbook://"等待反馈":
                        rbtnState4.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState4.Checked = true;
                        rbtnState4.Text = LanguageResource.Language.Tip_FeedbackNeedScore;
                        break;
                    case WorkOrderStatueEnum.WaitScore://"等待评分":
                        rbtnState5.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState5.Checked = true;
                        rbtnState5.Text = LanguageResource.Language.Tip_ScoreAndFinshWorkorder;
                        break;
                    case WorkOrderStatueEnum.LeaderAudit://"需领导审核":
                        rbtnState2.Visible = true;
                        rbtnState5.Visible = true;
                        rbtnState6.Visible = true;
                        rbtnState7.Visible = true;
                        rbtnState2.Checked = true;
                        rbtnState2.Text = LanguageResource.Language.Tip_AutidPassAndTurnout;
                        SetNextUser(model.F_PreDutyMan);
                        cboxF_DutyManDept.Enabled = false;
                        cboxF_DutyMan.Enabled = false;
                        ckboxEditNUser.Visible = true;
                        break;
                    case WorkOrderStatueEnum.History://"工单完成":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        break;
                    case WorkOrderStatueEnum.Recovery://"工单关闭":
                        rbtnState7.Visible = true;
                        rbtnState7.Checked = true;
                        break;
                    default:
                        break;
                }

            }
                #endregion
            Application.DoEvents();
            flowLayoutPanelF_State.Location = new Point(9, 55);

            lblTaskinfo.Text = userinfo;
            string[] authors = ShareData.UserPower.Split(',');
            //权限
            foreach (System.Windows.Forms.Control control in this.groupBoxTool0.Controls)
            {
                if (new string[] { buttonTETool8.Name }.Contains(control.Name))
                {//功能存在问题的操作不显示
                    control.Visible = false;
                    continue;
                }
                if (control.GetType().ToString() == "System.Windows.Forms.Button" || control.GetType().ToString() == "GSSUI.AControl.AButton.AButton")
                {
                    System.Windows.Forms.Button toolbtnActiv = (control as System.Windows.Forms.Button);
                    toolbtnActiv.Visible = false;
                    if (authors.Contains(typeof(FormTaskEdit).Name + "." + toolbtnActiv.Name))
                    {
                        toolbtnActiv.Visible = true;
                        control.Enabled = true;
                    }
                    Application.DoEvents();
                }
            }
            foreach (System.Windows.Forms.Control control in this.groupBoxTool1.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.Button" || control.GetType().ToString() == "GSSUI.AControl.AButton.AButton")
                {
                    System.Windows.Forms.Button toolbtnActiv = (control as System.Windows.Forms.Button);
                    toolbtnActiv.Visible = false;
                    if (authors.Contains(typeof(FormTaskEdit).Name + "." + toolbtnActiv.Name))
                    {
                        control.Enabled = true;
                        toolbtnActiv.Visible = true;
                    }
                    Application.DoEvents();
                }
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

        string attfiles = "";
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
                if (dt == null)
                {
                    return;
                }
                StringBuilder _taskNoteLog = new StringBuilder();
                attfiles = "";
                richtbTasklog.Clear();
                richtbnotelog.Clear();

                try
                {
                    StringBuilder changStr = new StringBuilder();
                    //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                    //_taskNoteLog = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ";

                    foreach (DataRow dr in dt.Rows)
                    {
                        changStr.AppendLine(@"[" + LanguageResource.Language.LblTime + "]:" + WinUtil.TrimNull(GetColumnValue(dr, "F_EditTime")));
                        changStr.AppendLine(@"[" + LanguageResource.Language.LblUser + "]:" + GetColumnValue(dr, "F_EditMan"));

                        string dowhatStr = LanguageResource.Language.LblDataChange;
                        if (dr["F_State"].ToString().Trim().Length > 0)
                        {
                            dowhatStr = LanguageResource.Language.LblWorkOrderStatueChange;
                        }
                        else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                        {
                            dowhatStr = LanguageResource.Language.LblToolUse;
                        }

                        changStr.AppendLine(@"[" + LanguageResource.Language.LblOperate + "]" + dowhatStr);
                        changStr.AppendLine(@"[" + LanguageResource.Language.LblData + @"]: ");


                        //用户名F_State
                        string[] colms = { LanguageResource.Language.LblWorkOrderSatue + "|F_State", LanguageResource.Language.LblCurrentPersonInCharge + "|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge + "|F_PreDutyMan", LanguageResource.Language.LblTitle + "|F_Title", LanguageResource.Language.LblSource + "|F_From", LanguageResource.Language.LblVipLevel + "|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun + "|F_LimitTime", LanguageResource.Language.LblType + "|F_Type", LanguageResource.Language.LblGame + "|F_GameName", LanguageResource.Language.LblBigZone + "|F_GameBigZone", LanguageResource.Language.LblZone + "|F_GameZone", LanguageResource.Language.LblAccount + "|F_GUserName", LanguageResource.Language.LblRole + "|F_GRoleName", LanguageResource.Language.LblContacter + "|F_GPeopleName", LanguageResource.Language.LblTel + "|F_Telphone", LanguageResource.Language.LblSureAccount + "|F_CUserName", LanguageResource.Language.LblSureSecurity + "|F_CPSWProtect", LanguageResource.Language.LblSureOther + "|F_COther", LanguageResource.Language.LblLastLoginTime + "|F_OLastLoginTime", LanguageResource.Language.LblCanResetData + "|F_OCanRestor", LanguageResource.Language.LblOftenGameLocation + "|F_OAlwaysPlace", LanguageResource.Language.LblRemark + "|F_Note" };
                        foreach (string colm in colms)
                        {
                            if (!dt.Columns.Contains(colm))
                            {
                                continue;
                            }
                            if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                            {
                                changStr.AppendLine(@"[" + colm.Split('|')[0] + @"] " + GetColumnValue(dr, colm.Split('|')[1]));

                            }

                        }

                        string fileurl = GetColumnValue(dr, "F_TUseData");
                        if (fileurl.IndexOf("GameUpfile") > -1)
                        {
                            attfiles += fileurl + "|";
                        }
                        else if (fileurl.Length > 0)
                        {
                            changStr.AppendLine("[" + LanguageResource.Language.LblToolUse + @"] :" + GetColumnValue(dr, "F_TUseData"));
                        }
                        if (GetColumnValue(dr, "F_Note").Length > 0)
                        {

                            _taskNoteLog.AppendLine(@"[" + LanguageResource.Language.LblTime + "]:" + WinUtil.TrimNull(GetColumnValue(dr, "F_EditTime")));
                            _taskNoteLog.AppendLine(@" [" + LanguageResource.Language.LblUser + "]:" + GetColumnValue(dr, "F_EditMan"));
                            _taskNoteLog.AppendLine(@"[" + LanguageResource.Language.LblRemark + "]:" + GetColumnValue(dr, "F_Note"));
                        }

                    }
                    richtbTasklog.Text = changStr.ToString();
                    richtbnotelog.Text = _taskNoteLog.ToString();

                }
                catch (System.Exception ex)
                {
                    richtbTasklog.AppendText(LanguageResource.Language.Tip_Tip + ":" + LanguageResource.Language.Tip_ErrorGetWorkOrderHistory + "!" + ex.Message);
                    //日志记录
                    ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                }


                richtbTasklog.ScrollToCaret();
                richtbnotelog.SelectionStart = richtbnotelog.TextLength;
                richtbnotelog.ScrollToCaret();
                Application.DoEvents();


                foreach (string file in attfiles.Split('|'))
                {
                    if (file.Length > 0)
                    {
                        richtbTasklog.AppendText("\r\n" + LanguageResource.Language.BtnWorkOrderFileClickEvent);
                        Application.DoEvents();
                        Clipboard.SetDataObject(Utils.GetAttImg(file));
                        richtbTasklog.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                    }

                }
                Application.DoEvents();
                richtbTasklog.AppendText("\r\n----------------------\r\n");
                richtbTasklog.SelectionStart = richtbTasklog.TextLength;
                richtbTasklog.ScrollToCaret();

            }
        }
        string GetColumnValue(DataRow row, string column)
        {
            if (row.Table.Columns.Contains(column))
            {
                string val = row[column] + "";
                return val.Trim();
            }
            return string.Empty;
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
        /// 窗口之间消息
        /// </summary>
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 601:
                    if (m.WParam.ToString() == "0")
                    {
                        this.DialogResult = DialogResult.Cancel;
                        ComitDoControl(true);
                        MsgBox.Show(LanguageResource.Language.BtnWorkorderDealwithNo, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        _IsChange = true;
                        SetGameUR();
                        ComitDoControl(true);
                        MsgBox.Show(LanguageResource.Language.BtnWorkorderDealwithOK, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    this.Activate();
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
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
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
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
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 帐号解封
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);
            string Rname = TrimNull(model.F_GRoleName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGuserUnLock form = new FormToolGuserUnLock(_clihandle, 1, model.F_ID.ToString(), Uname, Rname);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 角色解封
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);
            string Rname = TrimNull(model.F_GRoleName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGuserUnLock form = new FormToolGuserUnLock(_clihandle, 2, model.F_ID.ToString(), Uname, Rname);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 借用帐号
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGUserUse form = new FormToolGUserUse(_clihandle, model.F_ID.ToString(), Uname);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 帐号归还
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGUserNoUse form = new FormToolGUserNoUse(_clihandle, model.F_ID.ToString(), Uname);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        /// <summary>
        /// 清空防沉迷
        /// </summary>
        private void button11_Click(object sender, EventArgs e)
        {
            string Uname = TrimNull(model.F_GUserName);
            string Rname = TrimNull(model.F_GRoleName);

            if (Uname.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkorderLoseGameAccount, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormToolGResetChildInfo form = new FormToolGResetChildInfo(_clihandle, model.F_ID.ToString(), Uname);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }
        /// <summary>
        /// FDBI工具
        /// </summary>
        private void button16_Click(object sender, EventArgs e)
        {

            FormToolFDBI form = new FormToolFDBI(_clihandle, 1, model.F_ID.ToString(), "", "");
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }
        #endregion

        private void richtbTasklog_DoubleClick(object sender, EventArgs e)
        {
            string url = attfiles.Replace("|", "");
            if (url.IndexOf("http://") == -1)
            {
                url = ClientCache.GetGameConfigValue("12") + url;
            }
            Process.Start("iexplore", url);
        }

        private void buttonTETool8_Click(object sender, EventArgs e)
        {
            FormToolGRoleRecover form = new FormToolGRoleRecover(model);
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                Application.DoEvents();
                _IsChange = true;
                SetGameUR();
            }
        }

        private void lblTaskFlow_Click(object sender, EventArgs e)
        {
            FormTaskFlow form = new FormTaskFlow();
            form.Show();
        }

        private void lblTaskState_Click(object sender, EventArgs e)
        {
            FormTaskState form = new FormTaskState();
            form.Show();
        }

        private void buttonTETool9_Click(object sender, EventArgs e)
        {
            FormToolGRoleNameChange form = new FormToolGRoleNameChange(model);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }

        private void buttonTETool10_Click(object sender, EventArgs e)
        {
            FormToolGRoleZoneChange form = new FormToolGRoleZoneChange(model);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }

        private void buttonTETool11_Click(object sender, EventArgs e)
        {
            FormToolGUROnlineClear form = new FormToolGUROnlineClear(model);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }

        private void buttonTETool12_Click(object sender, EventArgs e)
        {
            FormToolGRoleDepotPSW form = new FormToolGRoleDepotPSW(model);
            DialogResult dialogresult = form.ShowDialog();
            Application.DoEvents();
            if (dialogresult == DialogResult.OK)
            {
                _IsChange = true;
                SetGameUR();
            }
        }


    }
}
