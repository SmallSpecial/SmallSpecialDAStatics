using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSSCSFrameWork;
using System.Media;
using GSS.DBUtility;
using System.IO;
using GSSUI;
using GSSUI.AForm;
using System.Threading;
using System.Reflection;
using System.Linq;
namespace GSSClient
{
    public partial class FormTask : GSSUI.AForm.FormMainSkin
    {
        string mapBigZoneID = "F_ValueGame";//GSSDB of value map the ID
        string queryAccount = LanguageResource.Language.BtnQueryAccount;
        #region 私有变量
        /// <summary>
        /// 客户端通讯实例
        /// </summary>
        public TcpCli clientNet = null;
        /// <summary>
        /// 客户端处理实例
        /// </summary>
        public ClientHandles _clihandle = null;
        #endregion

        /// <summary>
        /// 窗体任务栏闪动提示
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormTask()
        {
            InitializeComponent();
            "loading ShareData.UserPower.Split".Logger();
            if (ShareData.UserPower.Split(',').Contains(typeof(FormTask).Name + "." + tspNoWorkOrder.Name))
            {
                "hava author open no work order form".Logger();
                tspNoWorkOrder.Enabled = true;
                InitCreateMenus();//创建页面元素
            }
            this.Opacity = 0;
            "will set form text".Logger();
            InitLanguageText();
            "rely language set form text".Logger();
            InitConfig();
            "init config complate".Logger();
            InitElement();
            BindDisplayElement();
        }
        void InitElement() 
        {
            txtbCgameroleID.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtbCgameuserID.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
        }
        /// <summary>
        /// 页面显示名字韩文化
        /// </summary>
        void InitLanguageText() 
        {
            
            this.Text = LanguageResource.Language.LblSystemName;
            this.toolStripButtonT0.Text = LanguageResource.Language.BtnWorkOrderHomePage;
            this.toolStripButtonT10.Text = LanguageResource.Language.BtnMyWorkOrderPool;
            this.toolStripButtonT1.Text = LanguageResource.Language.LblReadyWorkOrderPool;
            this.toolStripButtonT13.Text = LanguageResource.Language.BtnConsuleWorkOrder;
            this.toolStripButtonT2.Text = LanguageResource.Language.BtnNewReceiverWorkOrder;
            this.toolStripButtonT3.Text = LanguageResource.Language.BtnDealWithingWorkOrder;
            this.toolStripButtonT4.Text = LanguageResource.Language.BtnTurnOutWorkOrderPool;
            this.toolStripButtonT7.Text = LanguageResource.Language.BtnLeaderAuditWorkOrderPool;
            this.toolStripButtonT5.Text = LanguageResource.Language.BtnWaitFeedBackWorkOrder;
            this.toolStripButtonT6.Text = LanguageResource.Language.BtnWaitScoreWorkOrder;
            this.toolStripButtonT8.Text = LanguageResource.Language.BtnHistoryWorkOrder;
            this.toolStripButtonT9.Text = LanguageResource.Language.BtnWorkOrderRecoveryTank;
            this.toolStripButtonT11.Text = LanguageResource.Language.BtnCallTank;
            this.toolStripButtonT12.Text = LanguageResource.Language.BtnAwardWorkOrderPool;
            this.tabPage1.Text = LanguageResource.Language.BtnWorkOrderHomePage;
            this.toolStripButtonT10.Text = LanguageResource.Language.BtnMyWorkOrderPool;
            this.toolStripButtonT1.Text = LanguageResource.Language.LblReadyWorkOrderPool;
            this.toolStripButtonT13.Text = LanguageResource.Language.BtnConsuleWorkOrder;
            this.toolStripButtonT2.Text = LanguageResource.Language.BtnNewReceiverWorkOrder;
            this.toolStripButtonT3.Text = LanguageResource.Language.BtnDealWithingWorkOrder;
            this.toolStripButtonT4.Text = LanguageResource.Language.BtnTurnOutWorkOrderPool;
            this.toolStripButtonT7.Text = LanguageResource.Language.BtnLeaderAuditWorkOrderPool;
            this.toolStripButtonT5.Text = LanguageResource.Language.BtnWaitFeedBackWorkOrder;
            this.toolStripButtonT6.Text = LanguageResource.Language.BtnWaitScoreWorkOrder;
            this.toolStripButtonT8.Text = LanguageResource.Language.BtnHistoryWorkOrder;
            this.toolStripButtonT9.Text = LanguageResource.Language.BtnWorkOrderRecoveryTank;
            this.toolStripButtonT11.Text = LanguageResource.Language.BtnCallTank;
            this.toolStripButtonT12.Text = LanguageResource.Language.BtnAwardWorkOrderPool;
            this.tabPage1.Text = LanguageResource.Language.BtnWorkOrderHomePage;
            this.buttonGameSearchRole.Text = LanguageResource.Language.BtnQueryRole;
            this.buttonGameRest.Text = LanguageResource.Language.LblReset;
            this.buttonGameSearch.Text = LanguageResource.Language.BtnQueryAccount;
            F_RoleID.HeaderText = LanguageResource.Language.LblRoleNo;
            this.Column28.SetDataGridViewTextBoxColumn("F_RoleName","F_RoleName", LanguageResource.Language.LblRoleName);
            this.ColumnF_ZoneName.HeaderText = LanguageResource.Language.LblBelongZone;
            this.Column30.SetDataGridViewTextBoxColumn("F_OnLine", "F_OnLine", LanguageResource.Language.LblOnlineStatue);
            this.Column31.SetDataGridViewTextBoxColumn("F_IsLock", "F_IsLock", LanguageResource.Language.LblCloseDownStatue);
            this.Column32.SetDataGridViewTextBoxColumn("F_Level", "F_Level", LanguageResource.Language.LblLevel);
            this.Column33.SetDataGridViewTextBoxColumn("F_CreateTime", "F_CreateTime", LanguageResource.Language.LblCreateTime);
            this.Column34.SetDataGridViewTextBoxColumn("F_UpdateTime", "F_UpdateTime", LanguageResource.Language.LblLastOnlineTime);
            this.F_ZoneID.HeaderText = LanguageResource.Language.LblBelongZoneWithBigZone;
            this.UserID.HeaderText = LanguageResource.Language.LblAccountNo;
            this.F_RowState.HeaderText = LanguageResource.Language.LblRoleStatue;
            this.F_UserID.HeaderText = LanguageResource.Language.LblAccountNo;
            Column2.SetDataGridViewTextBoxColumn("F_UserName", "F_UserName", LanguageResource.Language.LblAccountName);
            Column29.SetDataGridViewTextBoxColumn("F_BigZoneName", "F_BigZoneName", LanguageResource.Language.LblBelongBigZone);
            Column3.SetDataGridViewTextBoxColumn("F_OnLine", "F_OnLine", LanguageResource.Language.LblOnlineStatue);
            Column4.SetDataGridViewTextBoxColumn("F_IsLock", "F_IsLock", LanguageResource.Language.LblCloseDownStatue);
            this.Column5.SetDataGridViewTextBoxColumn("F_IsProtect", "F_IsProtect", LanguageResource.Language.LblIsSecurity);
            this.Column6.SetDataGridViewTextBoxColumn("F_IsAdult", "F_IsAdult", LanguageResource.Language.LblIsAdult);
            this.Column7.SetDataGridViewTextBoxColumn("F_PersonID", "F_PersonID", LanguageResource.Language.LblDocumentNumber);
            this.Column8.SetDataGridViewTextBoxColumn("F_CreatTime", "F_CreatTime", LanguageResource.Language.LblRegisterTime);
            this.Column9.SetDataGridViewTextBoxColumn("F_ActiveTime", "F_ActiveTime", LanguageResource.Language.LblLastOnlineTime);
            this.Column10.SetDataGridViewTextBoxColumn("F_LastLoginIP", "F_LastLoginIP", LanguageResource.Language.LblLastOnlineIP);
            this.tabPage2.Text = LanguageResource.Language.BtnWorkOrderExecTank;
            this.ColumnLimitT.HeaderText = LanguageResource.Language.LblWorkOrderLimit;
            this.Column11.SetDataGridViewTextBoxColumn("F_ID", "F_ID", LanguageResource.Language.LblWorkOrderNo);
            this.Column13.SetDataGridViewTextBoxColumn("F_Type", "F_Type", LanguageResource.Language.LblWorkOrderType);
            this.Column12.SetDataGridViewTextBoxColumn("F_Title", "F_Title", LanguageResource.Language.LblWorkOrderTitle);
            this.Column14.SetDataGridViewTextBoxColumn("F_GUserName", "F_GUserName", LanguageResource.Language.LblAccountName);
            this.Column15.SetDataGridViewTextBoxColumn("F_GRoleName", "F_GRoleName", LanguageResource.Language.LblRoleName);
            this.Column18.SetDataGridViewTextBoxColumn("F_GameName", "F_GameName", LanguageResource.Language.LblGameName);
            this.Column19.SetDataGridViewTextBoxColumn("F_GameZone", "F_GameZone", LanguageResource.Language.LblGameZone);
            this.ColumnF_State.SetDataGridViewTextBoxColumn("F_State", "F_State", LanguageResource.Language.LblWorkOrderSatue);
            this.Column16.SetDataGridViewTextBoxColumn("F_From", "F_From", LanguageResource.Language.LblWorkOrderSource);
            this.Column22.SetDataGridViewTextBoxColumn("F_DutyMan", "F_DutyMan", LanguageResource.Language.LblCurrentPersonInCharge);
            this.Column23.SetDataGridViewTextBoxColumn("F_PreDutyMan", "F_PreDutyMan", LanguageResource.Language.LblPreviousPersonInCharge);
            this.Column21.SetDataGridViewTextBoxColumn("F_CreatMan", "F_CreatMan", LanguageResource.Language.LblCreater);
            this.Column24.SetDataGridViewTextBoxColumn("F_CreatTime", "F_CreatTime", LanguageResource.Language.LblCreateTime);
            this.Column26.SetDataGridViewTextBoxColumn("F_EditMan", "F_EditMan", LanguageResource.Language.LblEditor);
            this.Column25.SetDataGridViewTextBoxColumn("F_EditTime", "F_EditTime", LanguageResource.Language.LblUpdateTime);
            this.Column17.SetDataGridViewTextBoxColumn("F_Note", "F_Note", LanguageResource.Language.LblWorkOrderRemark);
            this.ColumnLimitTime.SetDataGridViewTextBoxColumn("ColumnLimitTime", "F_LimitTime", LanguageResource.Language.LblTimeLimit);
            this.toolStripButtonTT0.Text = LanguageResource.Language.LblMyAllWorkOrder;
            this.toolStripButtonTT1.Text = LanguageResource.Language.LblMyAcceptWorkOrder;
            this.toolStripButtonTT2.Text = LanguageResource.Language.BtnMyDealWithWorkingOrder;
            this.toolStripButtonTT3.Text = LanguageResource.Language.LblMyTurnOutWorkOrder;
            this.toolStripButtonTT4.Text = LanguageResource.Language.LblWaitLeaderAuditOfWorkOrder;
            this.toolStripButtonTT5.Text = LanguageResource.Language.LblMyFinishWorkOrderForWaitFeedbook;
            this.toolStripButtonTT6.Text = LanguageResource.Language.LblMyWaitScoreWorkOrder;
            this.toolStripButtonTT7.Text = LanguageResource.Language.LblMyHistoryWorkOrder;
            this.toolStripButtonTT8.Text = LanguageResource.Language.LblMyCloseWorkOrder;
            this.buttonTaskRest.Text = LanguageResource.Language.LblReset;
            this.buttonTaskSearch.Text = LanguageResource.Language.BtnQuery;
            this.aButtonGNReset.Text = LanguageResource.Language.LblReset;
            this.ColumnLimitTGN.HeaderText = LanguageResource.Language.LblWorkOrderLimit;
            this.dataGridViewTextBoxColumn2.HeaderText = LanguageResource.Language.LblWorkOrderNo;
            this.ColumnGNF_OCanRestor.SetDataGridViewTextBoxColumn("F_OCanRestor","F_OCanRestor", LanguageResource.Language.LblNoticeState);
            this.dataGridViewTextBoxColumn3.HeaderText = LanguageResource.Language.LblWorkOrderType;
            this.dataGridViewTextBoxColumn4.HeaderText = LanguageResource.Language.LblWorkOrderTitle;
            this.dataGridViewTextBoxColumn17.HeaderText = LanguageResource.Language.LblWorkOrderRemark;
            this.dataGridViewTextBoxColumn5.HeaderText = LanguageResource.Language.LblAccountName;
            this.dataGridViewTextBoxColumn6.HeaderText = LanguageResource.Language.LblRoleName;
            this.dataGridViewTextBoxColumn7.HeaderText = LanguageResource.Language.LblGameName;
            this.dataGridViewTextBoxColumn8.HeaderText = LanguageResource.Language.LblGameBigZone;
            this.ColumnF_StateGN.HeaderText = LanguageResource.Language.LblWorkOrderSatue;
            this.dataGridViewTextBoxColumn10.HeaderText = LanguageResource.Language.LblWorkOrderSource;
            this.dataGridViewTextBoxColumn11.HeaderText = LanguageResource.Language.LblCurrentPersonInCharge;
            this.dataGridViewTextBoxColumn12.HeaderText = LanguageResource.Language.LblPreviousPersonInCharge;
            this.dataGridViewTextBoxColumn13.HeaderText = LanguageResource.Language.LblCreater;
            this.dataGridViewTextBoxColumn14.HeaderText = LanguageResource.Language.LblCreateTime;
            this.dataGridViewTextBoxColumn15.HeaderText = LanguageResource.Language.LblEditor;
            this.dataGridViewTextBoxColumn16.HeaderText = LanguageResource.Language.LblUpdateTime;
            this.ColumnLimitTimeGN.HeaderText = LanguageResource.Language.LblTimeLimit;
            this.btnReset.Text = LanguageResource.Language.LblReset;
            this.ColumnLimitTGA.HeaderText = LanguageResource.Language.LblWorkOrderLimit;
            this.dataGridViewTextBoxColumn47.HeaderText = LanguageResource.Language.LblWorkOrderNo;
            this.ColumnF_StateGA.HeaderText = LanguageResource.Language.LblWorkOrderSatue;
            this.dataGridViewTextBoxColumn49.HeaderText = LanguageResource.Language.LblWorkOrderType;
            this.dataGridViewTextBoxColumn50.HeaderText = LanguageResource.Language.LblWorkOrderTitle;
            this.dataGridViewTextBoxColumn51.HeaderText = LanguageResource.Language.LblWorkOrderRemark;
            this.dataGridViewTextBoxColumn52.HeaderText = LanguageResource.Language.LblAccountName;
            this.dataGridViewTextBoxColumn53.HeaderText = LanguageResource.Language.LblRoleName;
            this.dataGridViewTextBoxColumn54.HeaderText = LanguageResource.Language.LblGameName;
            this.dataGridViewTextBoxColumn55.HeaderText = LanguageResource.Language.LblGameBigZone;
            this.dataGridViewTextBoxColumn57.HeaderText = LanguageResource.Language.LblWorkOrderSource;
            this.dataGridViewTextBoxColumn58.HeaderText = LanguageResource.Language.LblCurrentPersonInCharge;
            this.dataGridViewTextBoxColumn59.HeaderText = LanguageResource.Language.LblPreviousPersonInCharge;
            this.dataGridViewTextBoxColumn60.HeaderText = LanguageResource.Language.LblCreater;
            this.dataGridViewTextBoxColumn61.HeaderText = LanguageResource.Language.LblCreateTime;
            this.dataGridViewTextBoxColumn62.HeaderText = LanguageResource.Language.LblEditor;
            this.dataGridViewTextBoxColumn63.HeaderText = LanguageResource.Language.LblUpdateTime;
            this.ColumnLimitTimeGA.HeaderText = LanguageResource.Language.LblTimeLimit;
            this.查询帐号相关工单ToolStripMenuItem.Image = Properties.Resources.GSSClient;
            this.查询证件号码ToolStripMenuItem.Image = Properties.Resources.GSSClient;
            this.查询在线状态ToolStripMenuItem.Image = Properties.Resources.GSSClient;
            this.查询角色相关工单ToolStripMenuItem.Image = Properties.Resources.GSSClient;
            this.查询在线状态ToolStripMenuItem1.Image = Properties.Resources.GSSClient;
            this.查询封停状态ToolStripMenuItem.Image = Properties.Resources.GSSClient;
            this.groupBox1.Text = LanguageResource.Language.LblQueryWhereArea;
            this.aButton2.Text = LanguageResource.Language.LblReset;
            this.dataGridViewTextBoxColumn1.HeaderText = LanguageResource.Language.LblWorkOrderLimit;
            this.dataGridViewTextBoxColumn9.HeaderText = LanguageResource.Language.LblWorkOrderNo;
            this.dataGridViewTextBoxColumn18.HeaderText = LanguageResource.Language.LblNoticeState;
            this.dataGridViewTextBoxColumn19.HeaderText = LanguageResource.Language.LblWorkOrderType;
            this.dataGridViewTextBoxColumn25.HeaderText = LanguageResource.Language.LblWorkOrderTitle;
            this.dataGridViewTextBoxColumn26.HeaderText = LanguageResource.Language.LblWorkOrderRemark;
            this.dataGridViewTextBoxColumn27.HeaderText = LanguageResource.Language.LblAccountName;
            this.dataGridViewTextBoxColumn28.HeaderText = LanguageResource.Language.LblRoleName;
            this.dataGridViewTextBoxColumn29.HeaderText = LanguageResource.Language.LblGameName;
            this.dataGridViewTextBoxColumn30.HeaderText = LanguageResource.Language.LblGameBigZone;
            this.dataGridViewTextBoxColumn31.HeaderText = LanguageResource.Language.LblWorkOrderSatue;
            this.dataGridViewTextBoxColumn32.HeaderText = LanguageResource.Language.LblWorkOrderSource;
            this.dataGridViewTextBoxColumn33.HeaderText = LanguageResource.Language.LblCurrentPersonInCharge;
            this.dataGridViewTextBoxColumn34.HeaderText = LanguageResource.Language.LblPreviousPersonInCharge;
            this.dataGridViewTextBoxColumn35.HeaderText = LanguageResource.Language.LblCreater;
            this.dataGridViewTextBoxColumn36.HeaderText = LanguageResource.Language.LblCreateTime;
            this.dataGridViewTextBoxColumn37.HeaderText = LanguageResource.Language.LblEditor;
            this.dataGridViewTextBoxColumn38.HeaderText = LanguageResource.Language.LblUpdateTime;
            this.dataGridViewTextBoxColumn39.HeaderText = LanguageResource.Language.LblTimeLimit;
            this.linkLabel20.Text = LanguageResource.Language.LblNextPage;
            this.linkLabel21.Text = LanguageResource.Language.LblPrevious;
            InitWorkOrderButtonText(gbAccountWorkOrder.Controls);//账号工单面板下的button按钮组文本初始化
            gbAccountWorkOrder.Text = LanguageResource.Language.gbAccountWorkOrder;
            gbRoleWorkOrder.Text = LanguageResource.Language.gbRoleWorkOrder;
            gbOtherWorkOrder.Text = LanguageResource.Language.gbOtherWorkOrder;
            InitWorkOrderButtonText(gbRoleWorkOrder.Controls);
            InitWorkOrderButtonText(gbOtherWorkOrder.Controls);
            this.label31.Text = LanguageResource.Language.LblAccountNo;
            this.label2.Text = LanguageResource.Language.LblAccountName;
            this.label3.Text = LanguageResource.Language.LblRoleName;
            this.label32.Text = LanguageResource.Language.LblRoleNo;
            this.label1.Text = LanguageResource.Language.LblTel;
            this.label4.Text = LanguageResource.Language.LblDocumentNumber;
            groupBoxG0.Text = LanguageResource.Language.LblQueryWhereArea;
            groupBoxG1.Text = LanguageResource.Language.GbAccountBaseInfoWithShortcutKey;
            groupBoxG2.Text = LanguageResource.Language.GbRoleBaseInfoWithShortcutKey;
            this.lblChat.Text = LanguageResource.Language.BtnOnlineConsume;
            this.Other_BtnReportFalseInfo.Text = LanguageResource.Language.BtnReportFalseInfo;
            Other_BtnLoginAward.Text = LanguageResource.Language.Other_BtnLoginAward;
            Other_BtnFullServiceEmail.Text = LanguageResource.Language.Other_BtnFullServiceEmail;
            DGVGameUser.Columns.Clear();
            this.DGVGameUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.F_UserID,
            this.Column2,
            this.Column29,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            new DataGridViewTextBoxColumn(){HeaderText=LanguageResource.Language.LblBigZoneNo,DataPropertyName=mapBigZoneID,Name="F_BigZoneID"}
            });
            DGVGameRole.Columns.Clear();
            this.DGVGameRole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.F_RoleID,
            this.Column28,
            this.ColumnF_ZoneName,
            this.Column30,
            this.Column31,
            this.Column32,
            this.Column33,
            this.Column34,
            this.F_ZoneID,
            this.UserID,
            this.F_RowState,
            new DataGridViewTextBoxColumn(){HeaderText=LanguageResource.Language.LblZoneNo,DataPropertyName="F_ZoneID",Name="F_ZoneID"}});
            ToolStripMenuItemTaskDeal.Text = LanguageResource.Language.BtnDealWithWorkOrder;
            ToolStripMenuItemTReceive.Text = LanguageResource.Language.BtnAcceptWorkOrder;
            ToolStripMenuItemGNStart.Text = LanguageResource.Language.BtnRunNotice;
            ToolStripMenuItemGNStop.Text = LanguageResource.Language.BtnStopNotice;
            ToolStripMenuItemTClose.Text = LanguageResource.Language.BtnCloseSelectWorkOrder;
            ToolStripMenuItemTSeachU.Text = LanguageResource.Language.BtnQueryWorkOrderInAccount;
            gbqueryArea.Text = gbqueryArea2.Text = LanguageResource.Language.LblQueryWhereArea;
            this.lblWorkOrderNo.Text=lblWorkOrderNo13.Text = LanguageResource.Language.LblWorkOrderNo;
            lblFounder.Text = lblFounder16 .Text= LanguageResource.Language.LblInitiatorName;
            lblTel.Text = lblTel15.Text = LanguageResource.Language.LblTel;
            lblWorkOrderStatue.Text = lblWorkOrderStatue9.Text = LanguageResource.Language.LblWorkOrderSatue;
            btnQuery.Text = aButtonGNSearch.Text = LanguageResource.Language.BtnQuery;
            btnReset.Text = LanguageResource.Language.LblReset;
            gbCallWorkOrderDeatil3.Text = gbCallWorkOrderDeatil5.Text = LanguageResource.Language.GbCallWorkOrderDetail;
            GpCallWorkOrderwithShortcutkey4.Text = LanguageResource.Language.GpCallWorkOrderwithShortcutkey;
            groupBoxTsearch.Text = LanguageResource.Language.LblQueryWhereArea;
            label6.Text = LanguageResource.Language.LblWorkOrderNo;
            label7.Text = LanguageResource.Language.LblAccountName;
            label12.Text = LanguageResource.Language.LblRoleName;
            label11.Text = LanguageResource.Language.LblTel;
            label10.Text = LanguageResource.Language.LblWorkOrderType;
            label30.Text = LanguageResource.Language.LblCreateTime;
            groupBoxTtasklist.Text = LanguageResource.Language.GbWorkOrderListShortcukey;
            groupBoxTtasklog.Text = LanguageResource.Language.LblWorkOrderRecord;
            groupBoxMyTask.Text = LanguageResource.Language.BtnMyWorkOrderPool;
            groupBox9.Text = LanguageResource.Language.Other_BtnAwardWorkOrder;
            groupBox8.Text = LanguageResource.Language.BtnAwardWorkOrderRecord;
            label5.Text = LanguageResource.Language.LblSystemName;
            btnSendEmail.Text = LanguageResource.Language.BtnSendEmail;
            this.btnHisView.Text = LanguageResource.Language.BtnViewHis;
            this.Role_BtnDisChatRecover.Text = LanguageResource.Language.BtnDisChatRecovery;
        }
        void InitConfig() 
        {
            string roleGrid=ClientCache.GetDicID(Role_BtnRoleRecovery.Parent.Name);
            ("[rolegrid ]" + roleGrid).Logger();
            string id = ClientCache.GetDicID(Role_BtnRoleRecovery.Name, roleGrid);
            ("[id ]" + id).Logger();
            if (!SystemConfig.WillCallServicesWorkOrder.ContainsKey(int.Parse(id)))
            {
                SystemConfig.WillCallServicesWorkOrder.Add(int.Parse(id), msgCommand.GameRoleRecovery.ToString());
            }
        }
        void InitWorkOrderButtonText(Control.ControlCollection buttons) 
        {
            foreach (Control item in buttons)
            {
                if (item is Button)
                {
                    item.Text = LanguageResource.Language.ResourceManager.GetString(item.Name);
                }
            } 
        }
        void BindDisplayElement() 
        {
            //配置 显示的按钮名称
            string[] validEles = new string[] { Account_btnCloseDown.Name, Other_BtnCallWorkOrder.Name, Role_BtnRoleRecovery.Name, Other_BtnLoginAward.Name, 
                Account_btnUnlock.Name,Role_btnCloseDown.Name, Role_btnUnlock.Name,Other_BtnAwardWorkOrder.Name,btnHisView.Name,Other_BtnFullServiceEmail.Name,Role_btnGag.Name,Role_BtnDisChatRecover.Name};
            foreach (Control item in gbAccountWorkOrder.Controls)
            {
                if (!validEles.Contains(item.Name))
                {
                    item.Visible = false;
                }
            }
            foreach (Control item in gbRoleWorkOrder.Controls)
            {
                if (!validEles.Contains(item.Name))
                {
                    item.Visible = false;
                }
            }
            foreach (Control item in gbOtherWorkOrder.Controls)
            {
                if (!validEles.Contains(item.Name))
                {
                    item.Visible = false;
                }
            }
        }
        #region 窗体事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //窗体位置居中
            this.TargetOpacityMust = 1;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            // this.WindowState=FormWindowState.Maximized;
            Control.CheckForIllegalCrossThreadCalls = false;
            //权限
            if (ShareData.UserPower.IndexOf(",toolStripButtonT0,") < 0)
            {
                toolStripButtonT0.Checked = false;
                toolStripButtonT10.Checked = true;
                tabControl1.SelectedIndex = 1;
            }
            //网络初始化
            NetInit();
            //窗体初始化
            ControlInit();
            PlayNotic(0);
            btnChat.ForeColor = System.Drawing.Color.White;

        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        private void FormTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                timerMain.Stop();
                FormClose form = new FormClose(this.Location, this.Size);
                DialogResult IsOK = form.ShowDialog();
                Application.DoEvents();
                if (IsOK == DialogResult.Yes)
                {
                    clientNet.Close();
                    // Win32.AnimateWindow(this.Handle, 1500, Win32.AW_BLEND|Win32.AW_HIDE);
                    //日志记录
                    ShareData.Log.Info("退出GSS系统");
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                    timerMain.Start();
                }
            }
        }

        /// <summary>
        /// 窗体位置变更事件
        /// </summary>
        private void FormTask_LocationChanged(object sender, EventArgs e)
        {
            //停止窗体停靠
            mStopAnthor();
        }

        #endregion

        #region 初始化相关

        /// <summary>
        /// 窗体控件初始化
        /// </summary>
        private void ControlInit()
        {
            timerMain.Start();
            toolStripStatusLabelCurrentUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripStatusLabelTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripStatusLabelTime.Text = string.Format("{0:yyyy"+LanguageResource.Language.LblUnitYear+"MM"+LanguageResource.Language.LblUnitMonth+"dd"+LanguageResource.Language.LblUnitDay+" dddd HH:mm:ss }", DateTime.Now);
            ShareData.FormhidMain = this.Handle.ToInt32();

            ToolSBtn_AllClickEventBind();
            ButtonTypeInit();
        }

        /// <summary>
        /// 工单建立按键事件绑定及初始化
        /// </summary>
        private void ButtonTypeInit()
        {
            this.Account_btnCloseDown.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_btnUnlock.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_btnPlayNo.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_btnGag.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnResetLevelPsw.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnResetIDCard.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnResetSecurity.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnRechargeException.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnResetAntiIndulgence.Click += new System.EventHandler(this.buttonType1_Click);
            this.Account_BtnResetBindEmail.Click += new System.EventHandler(this.buttonType1_Click);

            this.Role_btnCloseDown.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_btnUnlock.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_btnGag.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnDisChatRecover.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnRenamed.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnTurnService.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnResetWirehousePsw.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnCardNumber.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnRoleRecovery.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnRoleInfoException.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnPropException.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnPropLose.Click += new System.EventHandler(this.buttonType1_Click);
            this.Role_BtnReceivedException.Click += new System.EventHandler(this.buttonType1_Click);

            this.Other_BtnReportPW.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnReportPlugin.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnBugSubmit.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnSuggestion.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnReportOther.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnReportFalseInfo.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnDealWithTroublemakers.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnAwardOrQuery.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnGamerInforQuery.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnMergeServiceGangRenamed.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnHackedProcess.Click += new System.EventHandler(this.buttonType1_Click);
            this.Other_BtnPressReleases.Click += new System.EventHandler(this.buttonType1_Click);
            this.btnHisView.Click += btnHisView_Click;
        }

        void btnHisView_Click(object sender, EventArgs e)
        {
            FormHisView form = new FormHisView();
            form.ShowDialog();
            Application.DoEvents();
        }
        void InitCreateMenus() 
        {//该类元素按钮页面建立的对象不需要创建工单
            string[] noworkorder= Enum.GetNames(typeof(NoWorkOrderOfMenu));
            System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
            foreach (var item in noworkorder)
            {
                ToolStripMenuItem itemli = new ToolStripMenuItem()
                {
                    Name =item,
                    Text =rm.GetString(typeof(NoWorkOrderOfMenu).Name+"_"+item)
                };
                tspDDBMenu.DropDownItems.Add(itemli);
            }
            
            foreach (ToolStripMenuItem item in tspDDBMenu.DropDownItems)
            {
                item.Click += new EventHandler(StripMenuItem_Click);
            }
            
        }
        /// <summary>
        /// 绑定所有菜单按钮单击事件
        /// </summary>
        private void ToolSBtn_AllClickEventBind()
        {
            //权限
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (control as System.Windows.Forms.ToolStripButton);
                    if (ShareData.UserPower.IndexOf("," + toolbtnActiv.Name + ",") < 0)
                    {
                        toolbtnActiv.Enabled = false;

                    }

                    toolbtnActiv.Click += new System.EventHandler(this.toolStripButtonT0_Click);
                    toolbtnActiv.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonT0_Paint);
                }
            }
            string formname = this.GetType().Name;
            string[] eles=ShareData.UserPower.Split(',');
            if (eles.Contains(formname + "." + btnSendEmail.Name))
            {
                btnSendEmail.Enabled = true;
            }
            //我的工单菜单
            this.toolStripButtonTT0.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT1.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT2.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT3.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT4.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT5.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT6.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT7.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT8.Click += new System.EventHandler(this.toolStripButtonTT0_Click);

            this.toolStripButtonTT0.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT7.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT8.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);

        }

        /// <summary>
        /// 缓存数据控件初始化
        /// </summary>
        private void SetCacheControls()
        {
            try
            {
                BindDicComb(combTtasktypeP, "2000");
                BindDicComb(combTtasktype, "200000");
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Warn("系统实始化失败!" + ex.Message);
            }



            toolStripStatusLabelCurrentUser.Text = LanguageResource.Language.LblNowUser + ":" + ClientCache.GetUserNameT(ShareData.UserID) + " ";
        }

        /// <summary>
        /// 绑定COMBOX字典控件
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="parentid"></param>
        private void BindDicComb(ComboBox cb, string parentid)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_Dictionary"].Clone();
                DataRow dra = dtdic.NewRow();
                dra["F_Value"] = LanguageResource.Language.LblallType;
                dra["F_DicID"] = "0";
                dtdic.Rows.Add(dra);
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select("F_ParentID=" + parentid + "");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cb.DataSource = dtdic;
                cb.DisplayMember = "F_Value";
                cb.ValueMember = "F_DicID";
                cb.SelectedIndex = 0;
            }
        }

        #endregion

        #region 网络相关

        /// <summary>
        /// 网络通讯初始化
        /// </summary>
        private void NetInit()
        {
            try
            {
                clientNet = new TcpCli(new Coder(Coder.EncodingMothord.UTF8));
                clientNet.Resovlver = new DatagramResolver("]$}");
                clientNet.ReceivedDatagram += new NetEvent(this.RecvData);
                clientNet.DisConnectedServer += new NetEvent(this.ClientClose);
                clientNet.ConnectedServer += new NetEvent(this.ClientConn);
                clientNet.CannotConnectedServer += new NetEvent(this.CannotConnectedServer);
                clientNet.Connect(ShareData.LocalIp, ShareData.LocalPort);

                //实例化处理层
                _clihandle = new ClientHandles(clientNet);
            }
            catch (System.Exception ex)
            {
                MsgBox.Show(LanguageResource.Language.Tip_ConnectionServiceError+"\r\n"+LanguageResource.Language.Tip_PleaseInspectNetOrService+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //日志记录
                ShareData.Log.Warn(ex);
            }
        }

        /// <summary>
        /// 网络通讯-无法连接服务器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CannotConnectedServer(object sender, NetEventArgs e)
        {
            string info = LanguageResource.Language.Tip_Tip + ":" + LanguageResource.Language.Tip_ConnectionServiceError;
            toolStripStatusLabel1.Text = info;
            toolStripStatusLabel3.Visible = true;

            //日志记录
            ShareData.Log.Warn(info);
        }
        /// <summary>
        /// 网络通讯-客户端连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientConn(object sender, NetEventArgs e)
        {
            toolStripStatusLabel3.Visible = false;
            string info = string.Format(LanguageResource.Language.Tip_Client + ":{0}" + LanguageResource.Language.Tip_ConnectionService + ":{1}", e.Client, e.Client.ClientSocket.RemoteEndPoint.ToString());
            toolStripStatusLabel1.Text = info;
            clientNet.ClientSession.UserID = ShareData.UserID;

            _clihandle.GetCahce(ShareData.UserID);

            //  button1_Click(null, null);

            //权限
            if (ShareData.UserPower.IndexOf(",toolStripButtonT0,") < 0)
            {
                GetTaskList("");
            }

            //日志记录
            ShareData.Log.Info(info);
        }
        /// <summary>
        /// 网络通讯-客户端连接关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientClose(object sender, NetEventArgs e)
        {
           // toolStripStatusLabel3.Visible = true;
            string info;

            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format(LanguageResource.Language.Tip_Client + "Session:{0}状态:异外关闭.",
                 e.Client.ID);
            }
            else
            {
                info = string.Format(LanguageResource.Language.Tip_Client + "Session:{0}状态:正常关闭.",
                 e.Client.ID);
            }
            //日志记录
            ShareData.Log.Warn(info);

            FlashWindow(this.Handle, true);//闪烁

            toolStripStatusLabel1.Text = info;

        }
        /// <summary>
        /// 网络通讯-客户端接收数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecvData(object sender, NetEventArgs e)
        {

            string info = string.Format(LanguageResource.Language.LblReceiveData + ":{0}bytes " + LanguageResource.Language.LblFrom + ":{1}.", e.Client.MsgStrut.Data.Length, e.Client);
            info.Logger();
            toolStripStatusLabel1.Text = info;


            if (e.Client.MsgStrut == null)
            {
                return;
            }
            try
            {
                string.Format("class:{0},function:{1},command:{2},bytes:[3]", typeof(FormTask).Name, "RecvData", e.Client.MsgStrut.command, e.Client.MsgStrut.Data.Length).Logger();
                GSSModel.Request.ClientData fromServiceData;
                switch (e.Client.MsgStrut.command)
                {
                    case msgCommand.GetCache:
                        ClientCache.SetCache(e.Client.MsgStrut.Data);
                        SetCacheControls();
                        break;
                    case msgCommand.GetAlertNum:
                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskAlertNum");
                        //工单数量变化提醒
                        PlayNotic(1);
                        // FlashWindow(this.Handle, true);//闪烁
                        break;
                    case msgCommand.GetAllTasks:
                        DataSet dsTask = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        string.Format("query task type:{0} ,typeDesc:{1}",msgCommand.GetAllTasks, e.Client.MsgStrut.MsgParam.p0).Logger();
                        if (e.Client.MsgStrut.MsgParam.p0 == "toolStripButtonT11")
                        {
                            SetTaskValueGN(dsTask.Tables[0], e.Client.MsgStrut.MsgParam.p6);
                        }
                        else if (e.Client.MsgStrut.MsgParam.p0 == "toolStripButtonT12")
                        {
                            SetTaskValueGA(dsTask.Tables[0], e.Client.MsgStrut.MsgParam.p6);
                        }
                        else
                        {
                            SetTaskValue(dsTask.Tables[0], e.Client.MsgStrut.MsgParam.p6);
                        }

                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskList");
                        break;
                    case msgCommand.GetTaskLog:
                        DataSet dsTaskLog = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        // SetTaskLogValue(dsTaskLog.Tables[0]);
                        //this.backgroundWorker1.CancelAsync();

                        // if (this.backgroundWorker1.IsBusy)
                        // {
                        //    this.backgroundWorker1.CancelAsync();
                        // }
                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskLog");
                        this.backgroundWorker1.RunWorkerAsync(dsTaskLog.Tables[0]);

                        break;
                    case msgCommand.GetGameUsersC:
                        DataSet dsGameUsersC = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        if (dsGameUsersC == null)
                        {
                            MsgBox.Show(LanguageResource.Language.Tip_GameDataPullError + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        string.Format("command [{0}] query size :[{1}]", msgCommand.GetGameUsersC, dsGameUsersC.Tables[0].Rows.Count);
                        SetGameUserCValue(dsGameUsersC.Tables[0]);
                        if (DGVGameUser.SelectedRows.Count > 0)
                        {
                            string bigzonename = DGVGameUser.SelectedRows[0].Cells[2].Value.ToString();
                            string id = DGVGameUser.SelectedRows[0].Cells[0].Value.ToString();
                            _clihandle.GetGameRolesC(bigzonename + "|" + id);
                        }
                        break;
                    case msgCommand.GetGameRolesC:
                        DataSet dsGameRoleC = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        SetGameRoleCValue(dsGameRoleC.Tables[0]);
                        break;
                    case msgCommand.GetGameRolesCR:
                        dsGameRoleC = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        // SetGameRoleCValue(dsGameRoleC.Tables[0]);
                        if (dsGameRoleC != null)
                        {
                            if (dsGameRoleC.Tables[0].Rows.Count > 0)
                            {
                                // string uid = dsGameRoleC.Tables[0].Rows[0]["F_UserID"].ToString();
                                // SetDGVUser_R(uid);
                                string uids = "";
                                foreach (DataRow dr in dsGameRoleC.Tables[0].Rows)
                                {
                                    uids += string.Format(",{0}", dr["F_UserID"]);
                                }
                                if (uids.Length != 0)
                                {
                                    uids = uids.Remove(0, 1);
                                    SetDGVUser_Rs(uids);
                                }
                            }
                        }

                        break;
                    case msgCommand.AddTask:
                        string Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        SetFormsMSG(ShareData.FormhidAdd, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.EditTask:
                        Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        SetFormsMSG(ShareData.FormhidEdit, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.EditTaskNoReturn:
                        Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        // SetFormsMSG(_taskEdithandid, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.ExcSql:
                        string msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        string[] formsql = msgStr.Split('|');//分为两段,执行SQL语句的窗口ID和语句
                        int formid = Convert.ToInt32(formsql[0]);
                        break;
                    case msgCommand.GameLockUR:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        string[] msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        int backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameNoLockUR:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameUserUse:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameUserNoUse:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameResetChildInfo:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.AddLoginAward:
                        GSSModel.Request.ClientData client = DataSerialize.GetObjectFromByte(e.Client.MsgStrut.Data) as GSSModel.Request.ClientData;
                        formid = client.FormID;
                        client.ConvertJson().Logger();
                        backid = ShareData.Msg.Add(client);
                        SetFormsMSG(formid, 601, backid+1);
                        break;
                    case msgCommand.GameRoleRecovery:
                          //msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                          //msgs= msgStr.Split('|');//formid，msg
                          //formid = Convert.ToInt32(msgs[0]);
                          //int msgIndex = ShareData.Msg.Add(msgs[2]);
                          //SetFormsMSG(formid, 601, Convert.ToInt32(msgs[1]), msgIndex);
                           msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                          msgs= msgStr.Split('|');//formid，taskid，msg
                          fromServiceData = new GSSModel.Request.ClientData();
                          fromServiceData.FormID= Convert.ToInt32(msgs[0]);
                          fromServiceData.Message = msgs[2];
                          int msgIndex = ShareData.Msg.Add(fromServiceData);
                          SetFormsMSG(fromServiceData.FormID, 601,Convert.ToInt32(msgs[1]), msgIndex);//由于创建页中逻辑特殊，不能使用第三个参数作为消息传递的索引
                        break;
                    case msgCommand.SendEmailToRoles:
                        object response= DataSerialize.GetObjectFromByte(e.Client.MsgStrut.Data);
                        //msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        GSSModel.Request.ClientData data = response as GSSModel.Request.ClientData;
                        backid=ShareData.Msg.Add(data)+1;
                        SetFormsMSG(data.FormID, SystemConfig.BetweenFormChatMsgId, backid);
                        break;
                    case msgCommand.ActiveFallGoods:
                        object res = DataSerialize.GetObjectFromByte(e.Client.MsgStrut.Data);
                        GSSModel.Request.ClientData resClient = res as GSSModel.Request.ClientData;
                        backid = ShareData.Msg.Add(resClient) + 1;
                        SetFormsMSG(resClient.FormID, SystemConfig.BetweenFormChatMsgId, backid);
                        break;
                    case msgCommand.CreateTaskContainerLogic:
                        client = DataSerialize.GetObjectFromByte(e.Client.MsgStrut.Data) as GSSModel.Request.ClientData;
                        backid = ShareData.Msg.Add(client) ;
                        SetFormsMSG(client.FormID, SystemConfig.BetweenFormChatMsgId,client.TaskID, backid);//注意：在taskadd页中接收SetFormsMSG第三个参数WParam传递的是工单号
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.ErrorLogger();
                //日志记录
                ShareData.Log.Warn(LanguageResource.Language.Tip_ReceiveDataError + "!" + LanguageResource.Language.Tip_Position + e.Client.MsgStrut.command.ToString() + LanguageResource.Language.Tip_Info + ":" + ex.Message);
            }


        }

        /// <summary>
        /// 重连服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            //重连服务器
            NetInit();
        }
        #endregion

        #region 委托相关

        //窗口消息的委托
        private delegate void Setfrommsg(int handid, int msgid, int recv, int ShareDataMsgIndex=-1);
        private void SetFormsMSG(int handid, int msgid, int recv,  int ShareDataMsgIndex=-1)
        {
            if (this.InvokeRequired)
            {
                Setfrommsg d = new Setfrommsg(SetFormsMSG);
                object hid = handid;
                object arg = msgid;
                object rec = recv;
                object msgIndex = ShareDataMsgIndex;
                this.Invoke(d, hid, arg, rec, msgIndex);
            }
            else
            {
                FormsMsg.PostMessage(handid, msgid, recv, (ShareDataMsgIndex+1));
            }
        }

        //工单列表控件的委托
        private delegate void SetDGValue(DataTable dt, string count);
        private void SetTaskValue(DataTable dt, string count)
        {
            if (this.InvokeRequired)
            {
                SetDGValue d = new SetDGValue(SetTaskValue);
                object arg0 = dt;
                object arg1 = count;
                this.Invoke(d, arg0, arg1);
            }
            else
            {
                lock (this)
                {
                    DGVTask.AutoGenerateColumns = false;
                    DGVTask.DataSource = dt;
                    //int index = ShareData.DGVSelectIndex;
                    //if (index < DGVTask.Rows.Count && index != 0)
                    //{
                    //    DGVTask.Rows[index].Selected = true;
                    //    DGVTask.Rows[0].Selected = false;
                    //}
                    controlPagerT.DrawControl(Convert.ToInt16(count));
                }

                //SharData.DGVSelectIndex = 0;
                // dataGridView3.Columns[0].HeaderText = "(" + dt.Rows.Count + ")";
                //dataGridView3.Sort(dataGridViewZ.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private delegate void SetDGValueGN(DataTable dt, string count);
        private void SetTaskValueGN(DataTable dt, string count)
        {
            if (this.InvokeRequired)
            {
                SetDGValueGN d = new SetDGValueGN(SetTaskValueGN);
                object arg0 = dt;
                object arg1 = count;
                this.Invoke(d, arg0, arg1);
            }
            else
            {
                lock (DGVTaskGN)
                {
                    DGVTaskGN.AutoGenerateColumns = false;
                    DGVTaskGN.DataSource = dt;
                }
                controlPagerN.DrawControl(Convert.ToInt16(count));
                //int index = ShareData.DGVSelectIndex;
                //if (index < DGVTaskGN.Rows.Count && index != 0)
                //{
                //    DGVTaskGN.Rows[0].Selected = false;
                //    DGVTaskGN.Rows[index].Selected = true;

                //    string id = DGVTaskGN.Rows[index].Cells[2].Value.ToString();
                //    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                //    clihandle.GetTaskLog(sqlwhere);

                //}
            }
        }

        private delegate void SetDGValueGA(DataTable dt, string count);
        private void SetTaskValueGA(DataTable dt, string count)
        {
            if (this.InvokeRequired)
            {
                SetDGValueGA d = new SetDGValueGA(SetTaskValueGA);
                object arg0 = dt;
                object arg1 = count;
                this.Invoke(d, arg0, arg1);
            }
            else
            {
                lock (DGVTaskGA)
                {
                    DGVTaskGA.AutoGenerateColumns = false;
                    DGVTaskGA.DataSource = dt;
                }
                controlPagerA.DrawControl(Convert.ToInt16(count));
                //int index = ShareData.DGVSelectIndex;
                //if (index < DGVTaskGA.Rows.Count && index != 0)
                //{
                //    DGVTaskGA.Rows[0].Selected = false;
                //    DGVTaskGA.Rows[index].Selected = true;

                //    string id = DGVTaskGA.Rows[index].Cells[2].Value.ToString();
                //    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                //    clihandle.GetTaskLog(sqlwhere);

                //}
            }
        }

        //游戏玩家列表控件的委托
        private delegate void SetGameDGValue(DataTable dt);
        private void SetGameUserCValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetGameDGValue d = new SetGameDGValue(SetGameUserCValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVGameUser.AutoGenerateColumns = false;
                DGVGameUser.DataSource = dt;
            }
        }

        //游戏角色列表控件的
        private void SetGameRoleCValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetGameDGValue d = new SetGameDGValue(SetGameRoleCValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVGameRole.AutoGenerateColumns = false;
                DGVGameRole.DataSource = dt;
            }
        }


        string attfiles = "";
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
                if (dt == null)
                {
                    return;
                }
                if (richtbTasklog.Visible)
                {
                    attfiles = "";
                    richtbTasklog.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                        //中文使用f0 韩文使用f1
                        foreach (DataRow dr in dt.Rows)
                        {
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblTime + "]:" + dr["F_EditTime"] );
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblUser + "]:" + dr["F_EditMan"]);

                            string dowhatStr =  LanguageResource.Language.LblDataChange;
                            if (dr["F_State"].ToString().Trim().Length > 0)
                            {
                                dowhatStr =  LanguageResource.Language.LblWorkOrderStatueChange ;
                            }
                            else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                            {
                                dowhatStr = LanguageResource.Language.LblToolUse ;
                            }
                            if (toolStripButtonT13.Checked)
                            {
                                if (dr["F_TToolUsed"].ToString().Trim() == "True")
                                {
                                    dowhatStr = LanguageResource.Language.Tip_CustomerServiceCounter;
                                }
                                else
                                {
                                    dowhatStr =LanguageResource.Language.Tip_CustomerServiceNotRead;
                                }

                            }

                            changStr.AppendLine(@"[" + LanguageResource.Language.LblOperate + "]" + ":" + dowhatStr);
                            changStr.AppendLine("\r\n");
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblData+"]");
                            changStr.AppendLine();

                            //用户名F_State
                            string[] colms = { LanguageResource.Language.LblWorkOrderSatue + "|F_State", LanguageResource.Language.LblCurrentPersonInCharge + "|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge + "|F_PreDutyMan", LanguageResource.Language.LblTitle + "|F_Title", LanguageResource.Language.LblSource + "|F_From", LanguageResource.Language.LblVipLevel + "|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun + "|F_LimitTime", LanguageResource.Language.LblType + "|F_Type", LanguageResource.Language.LblGame + "|F_GameName", LanguageResource.Language.LblBigZone + "|F_GameBigZone", LanguageResource.Language.LblZone + "|F_GameZone", LanguageResource.Language.LblAccount + "|F_GUserName", LanguageResource.Language.LblRole + "|F_GRoleName", LanguageResource.Language.LblContacter + "|F_GPeopleName", LanguageResource.Language.LblTel + "|F_Telphone", LanguageResource.Language.LblSureAccount + "|F_CUserName", LanguageResource.Language.LblSureSecurity + "|F_CPSWProtect", LanguageResource.Language.LblSureOther + "|F_COther", LanguageResource.Language.LblLastLoginTime + "|F_OLastLoginTime", LanguageResource.Language.LblCanResetData + "|F_OCanRestor", LanguageResource.Language.LblOftenGameLocation + "|F_OAlwaysPlace", LanguageResource.Language.LblRemark + "|F_Note", LanguageResource.Language.LblBigZoneNo + "|" + mapBigZoneID };
                            foreach (string colm in colms)
                            {
                                string name = colm.Split('|')[0];
                                string value = colm.Split('|')[1];
                                if (!dr.Table.Columns.Contains(value))
                                {
                                    continue;
                                }
                                if (value == "F_Type")
                                {
                                    string type = dr[value].ToString();
                                    string[] workorder = type.Split('-');
                                    System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
                                    changStr.AppendLine("["+name+"]:"+ rm.GetString(workorder[0]) + "-" + rm.GetString(workorder[1]));
                                    continue;
                                }
                                if (dr[value].ToString().Trim().Replace("-", "").Length > 0)
                                {
                                    if (toolStripButtonT13.Checked)
                                    {
                                        name = name.Replace("备注", LanguageResource.Language.Tip_ChatDetail).Replace(LanguageResource.Language.LblCanResetData, LanguageResource.Language.Tip_USerRead);
                                    }
                                    changStr.AppendLine(@"[" + name + @"] :" + dr[value].ToString().Trim());
                                }

                            }

                            string fileurl = dr["F_TUseData"].ToString().Trim();
                            if (fileurl.IndexOf("GameUpfile") > -1)
                            {
                                attfiles += fileurl + "|";
                            }
                            else if (fileurl.Length > 0)
                            {
                                changStr.AppendLine("[" + LanguageResource.Language.LblToolUse + @"]:" + dr["F_TUseData"].ToString().Trim());
                            }
                        }
                        richtbTasklog.Text = changStr.ToString();

                    }
                    catch (System.Exception ex)
                    {
                        richtbTasklog.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklog.ScrollToCaret();
                    Application.DoEvents();

                    foreach (string file in attfiles.Split('|'))
                    {
                        if (file.Length > 0 && !backgroundWorker1.CancellationPending)
                        {
                            richtbTasklog.AppendText("\r\n"+LanguageResource.Language.BtnWorkOrderFileClickEvent);
                            Application.DoEvents();
                            Clipboard.SetDataObject(Utils.GetAttImg(file));
                            Application.DoEvents();
                            richtbTasklog.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                            Application.DoEvents();
                        }

                    }
                    Application.DoEvents();
                    richtbTasklog.AppendText("\r\n----------------------\r\n");
                    richtbTasklog.SelectionStart = richtbTasklog.TextLength;
                    richtbTasklog.ScrollToCaret();

                }
                else if (richtbTasklogGN.Visible)
                {
                    richtbTasklogGN.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                        string colorlistStr = "";
                        string changStrColor = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblTime + "]:" + dr["F_EditTime"] );
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblUser + "]:" + dr["F_EditMan"] );

                            string dowhatStr =  LanguageResource.Language.LblDataChange;
                            if (dr["F_State"].ToString().Trim().Length > 0)
                            {
                                dowhatStr = LanguageResource.Language.LblWorkOrderStatueChange ;
                            }
                            else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                            {
                                dowhatStr =  LanguageResource.Language.LblToolUse;
                            }

                            changStr.AppendLine(@"[" + LanguageResource.Language.LblOperate + "]:" + dowhatStr);
                            changStr.AppendLine(@"[" + LanguageResource.Language.LblData + @"]  ");
                            changStr.AppendLine();

                            //用户名F_State
                            string[] colms = { LanguageResource.Language.LblWorkOrderSatue+"|F_State", LanguageResource.Language.LblCurrentPersonInCharge+"|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge+"|F_PreDutyMan",LanguageResource.Language.LblTitle+"|F_Title", LanguageResource.Language.LblSource+"|F_From",LanguageResource.Language.LblVipLevel+"|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun+"|F_LimitTime", LanguageResource.Language.LblType+"|F_Type", LanguageResource.Language.LblGame+"|F_GameName",  LanguageResource.Language.LblBigZone + "|F_GameBigZone",  LanguageResource.Language.LblZone+ "|F_GameZone",  LanguageResource.Language.LblAccount+ "|F_GUserName",  LanguageResource.Language.LblRole+ "|F_GRoleName", LanguageResource.Language.LblContacter+ "|F_GPeopleName",  LanguageResource.Language.LblTel+ "|F_Telphone",  LanguageResource.Language.LblSureAccount+ "|F_CUserName",  LanguageResource.Language.LblSureSecurity+ "|F_CPSWProtect", LanguageResource.Language.LblToolRecord+ "|F_COther",  LanguageResource.Language.LblLastLoginTime+ "|F_OLastLoginTime",  LanguageResource.Language.LblOftenGameLocation+ "|F_OAlwaysPlace",  LanguageResource.Language.LblRemark+ "|F_Note" };
                            foreach (string colm in colms)
                            {
                                if (!dr.Table.Columns.Contains(colm.Split('|')[1]))
                                {
                                    continue;
                                }
                                string[] cs = colm.Split('|');
                                string name = cs[0];
                                string value = cs[1];
                                if (value == "F_Type")
                                {
                                    string type = dr[value].ToString();
                                    string typeDesc = string.Empty;
                                    if (type != "-")
                                    {
                                        System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
                                        string[] workorder = type.Split('-');
                                        typeDesc=rm.GetString(workorder[0]) + "-" + rm.GetString(workorder[1]);
                                    }
                                    changStr.AppendLine("[" + name + "]:" + typeDesc);
                                    continue;
                                }
                                if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                                {

                                    changStr.AppendLine(@"[" + colm.Split('|')[0] + @"] " + dr[colm.Split('|')[1]].ToString().Trim());

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
                                        gnerea += GetNodeValue(u[0], u[1], u[2]) + @"| \r\n";
                                    }
                                    else if (ud.Trim().Length > 0)
                                    {
                                        gnerea += ud + @"| \r\n";
                                    }
                                }
                                changStr.AppendLine("[" + LanguageResource.Language.LblNoticeRange + @"] ");
                            }


                            string Gnotice = dr["F_URInfo"].ToString().Trim();
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
                                changStr.AppendLine("["+LanguageResource.Language.LblNoticeText + @"] " + Gnotice );
                            }
                            if (dr["F_OCanRestor"].ToString().Trim().Length > 0)
                            {
                                string istooluse = dr["F_OCanRestor"].ToString() == "True" ? LanguageResource.Language.LblStartRunning : LanguageResource.Language.LblEndRunning;
                                changStr.AppendLine(@"[" + LanguageResource.Language.LblRunStatue + "]" + istooluse );
                            }

                          //  changStr.Append(@"\highlight0-----------------------------------\highlight0\par ");



                        }
                        //changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\lang2052\f0\fs18 " + changStr.ToString();
                        richtbTasklogGN.Text = changStr.ToString();

                    }
                    catch (System.Exception ex)
                    {
                        richtbTasklogGN.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklogGN.SelectionStart = richtbTasklogGN.TextLength;
                    richtbTasklogGN.ScrollToCaret();

                }
                else if (richtbTasklogGA.Visible)
                {
                    richtbTasklogGA.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                        string colorlistStr = "";
                        string changStrColor = "";

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
                                string[] cs = colm.Split('|');
                                string name = cs[0];
                                string value = cs[1];
                                if (!dr.Table.Columns.Contains(colm.Split('|')[1]))
                                {
                                    continue;
                                }
                                if (value == "F_Type")
                                {
                                    string type = dr[value].ToString();
                                    string[] workorder = type.Split('-');
                                    System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
                                    changStr.AppendLine("[" + name + "]:" + rm.GetString(workorder[0]) + "-" + rm.GetString(workorder[1]));
                                    continue;
                                }
                                if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                                {

                                    changStr.Append(@"[" + colm.Split('|')[0] + @"]{\cf2  " + dr[colm.Split('|')[1]].ToString().Trim().Replace("\n", @"\par") + @" }\par ");

                                }

                            }
                            string F_userdata = dr["F_TUseData"].ToString();
                            if (F_userdata.Trim().Length > 0)
                            {
                                string[] uds = F_userdata.Split('|');
                                if (uds.Length == 4)
                                {
                                    string gnerea = "";
                                    gnerea += LanguageResource.Language.LblAwardNo + ":" + uds[0] + @"\par";
                                    gnerea += "奖品名称:" + (uds[1] == "0" ? LanguageResource.Language.LblProp : LanguageResource.Language.LblPackage) + @"\par";
                                    gnerea += "奖品类型:" + uds[2] + @"\par";
                                    gnerea += "奖品数量:" + uds[3] + @"\par";

                                    changStr.Append(@"\b[奖品内容]\b0\par{\cf0" + gnerea + @"}\par");
                                }
                                else
                                {
                                    changStr.Append(@"\b[" + LanguageResource.Language.LblToolUse + @"]\b0\par{\cf0" + F_userdata + @"}\par");
                                }

                            }


                            string URInfo = dr["F_URInfo"].ToString().Trim();

                            if (URInfo.Length != 0)
                            {
                                DataSet ds = GSSCSFrameWork.DataSerialize.GetDatasetFromByte((byte[])GSSCSFrameWork.DataSerialize.GetObjectFromString(URInfo));
                                if (ds != null)
                                {
                                    int i = 0;
                                    changStr.Append(@"\b[中奖用户列表] \b0(" + ds.Tables[0].Rows.Count + @")\par");
                                    foreach (DataRow dru in ds.Tables[0].Rows)
                                    {
                                        if (i == 30)
                                        {
                                            changStr.Append(@">>>>>>>更多 \par");
                                            break;
                                        }
                                        changStr.Append(@"用户编号:" + dru[0] + LanguageResource.Language.LblRoleNo + dru[1] + @" \par");
                                        i++;
                                    }
                                }
                            }


                            changStr.Append(@"\highlight0-----------------------------------\highlight0\par ");

                        }
                        changStr.Append(@"}");
                        changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\lang2052\f0\fs18 " + changStr.ToString();
                        richtbTasklogGA.Rtf = changStrColor;

                    }
                    catch (System.Exception ex)
                    {
                        richtbTasklogGA.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklogGA.SelectionStart = richtbTasklogGA.TextLength;
                    richtbTasklogGA.ScrollToCaret();

                }

            }

        }




        #endregion

        #region 私有方法

        private void SetDGVUser_R(string uid)
        {
            "SetDGVUser_R".Logger();
            MsgParam msgparam = new MsgParam();
            if (uid != "")
            {
                string whereStr = " and a.F_UserID =" + uid + " ";
                msgparam.p1 = whereStr;
                _clihandle.GetGameUsersC(whereStr);
            }
        }
        private void SetDGVUser_Rs(string uids)
        {
            "SetDGVUser_Rs".Logger();
            MsgParam msgparam = new MsgParam();
            if (uids.Trim().Length > 0)
            {
                string whereStr = " and a.F_UserID in (" + uids + ") ";
                _clihandle.GetGameUsersC(whereStr);
            }
        }
        /// <summary>
        /// 得到IMAGE对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string path)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
                fs.Close();
                return result;
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Warn(ex);
                return new System.Drawing.Bitmap(1, 1);
            }
        }

        /// <summary>
        /// 给关键字上色
        /// </summary>
        public int getbrush(string p, string s)
        {
            int cnt = 0;
            int M = p.Length;
            int N = s.Length;
            char[] ss = s.ToCharArray(), pp = p.ToCharArray();
            if (M > N) return 0;
            for (int i = 0; i < N - M + 1; i++)
            {
                int j;
                for (j = 0; j < M; j++)
                {
                    if (ss[i + j] != pp[j]) break;
                }
                if (j == p.Length)
                {
                    this.richtbTasklog.Select(i, p.Length);
                    this.richtbTasklog.SelectionColor = Color.SteelBlue;
                    cnt++;
                }
            }
            return cnt;
        }

        /// <summary>
        /// 停靠方法
        /// </summary>
        internal AnchorStyles StopAanhor = AnchorStyles.None;
        private bool IsAnchorBack = true;
        private void mStopAnthor()
        {
            if (this.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (this.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }

        /// <summary>
        /// 窗体依靠
        /// </summary>
        private void SetAnchor()
        {

            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized || Location.Y > 3)
            {
                return;
            }
            bool istopmost = GSSUI.SharData.TopMost;
            if (this.Bounds.Contains(Cursor.Position))
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, 0);
                        break;
                    case AnchorStyles.None:
                        if (this.TopMost && !IsAnchorBack)
                        {
                            IsAnchorBack = true;
                            GSSUI.SharData.GetUIInfo();
                            this.TopMost = GSSUI.SharData.TopMost;
                        }
                        break;
                    //case AnchorStyles.Left:
                    //    this.Location = new Point(0, this.Location.Y);
                    //    break;
                    //case AnchorStyles.Right:
                    //    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                    //    break;
                }
            }
            else
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, (this.Height - 32) * (-1));
                        if (!this.TopMost)
                        {
                            this.TopMost = true;
                            GSSUI.SharData.TopMost = true;
                            IsAnchorBack = false;
                        }

                        break;
                    //case AnchorStyles.Left:
                    //    this.Location = new Point((-1) * (this.Width - 12), this.Location.Y);
                    //    break;
                    //case AnchorStyles.Right:
                    //    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 12, this.Location.Y);
                    //    break;
                }
            }
        }

        /// <summary>
        /// 播放提醒声音
        /// </summary>
        /// <param name="playid"></param>
        private void PlayNotic(int playid)
        {
            string sFileName;
            switch (playid)
            {
                case 0://功能切换
                    sFileName = "GSSData\\Sounds\\Start.wav";
                    break;
                case 1://收到消息
                    sFileName = "GSSData\\Sounds\\Msg.wav";
                    break;
                case 2://功能切换
                    sFileName = "GSSData\\Sounds\\Chimes.wav";
                    break;
                default:
                    sFileName = "GSSData\\Sounds\\Msg.wav";
                    break;
            }
            PlaySound.play(sFileName);
            //SoundPlayer sp = new SoundPlayer(Application.StartupPath+"\\System.wav");
            //sp.Play();
        }

        #endregion

        #region 工单主页相关

        /// <summary>
        /// 弹出工单建立窗口
        /// </summary>
        private void buttonType1_Click(object sender, EventArgs e)
        {
            //工单类型相关
            Button self = sender as System.Windows.Forms.Button;
            string dicname = self.Text;
            string parentname = self.Parent.Text;
            string parentid = ClientCache.GetDicID(self.Parent.Name);
            int tasktype = Convert.ToInt32(ClientCache.GetDicID(self.Name, parentid));
            if (parentid.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_InitErrorLoginAgain + "！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //不需要游戏信息的工单
            string NoUserInfo = ",20000201,20000202,20000203,20000204,20000205,20000206,20000207,";
            if (NoUserInfo.IndexOf("," + tasktype + ",") >= 0)
            {
                FormTaskAddNoUR formn = new FormTaskAddNoUR(_clihandle, tasktype);
                ShareData.FormhidAdd = formn.Handle.ToInt32();
                formn.ShowDialog();
                Application.DoEvents();
                return;
            }

            //需要游戏信息的工单
            DataGridViewRow drGuser = null;
            DataGridViewRow drGrole = null;

            if (DGVGameUser.SelectedRows.Count == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_TheWorkOrderNeedGameUserIfno + "！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            drGuser = DGVGameUser.SelectedRows[0];
            if (DGVGameRole.SelectedRows.Count > 0)
            {
                drGrole = DGVGameRole.SelectedRows[0];
            }
            string title = parentname + "_" + dicname;
            //根据按钮所属的工单类型判断是否需要传递角色信息
            DataGridViewRow role = drGrole;
            if (self.Parent.Name == gbAccountWorkOrder.Name)
            {
                role = null;
            }
            FormTaskAdd form = new FormTaskAdd(_clihandle, drGuser, role, tasktype, title);//此处数据传递实际上不合理？？列如在进行解封时如果操作的是账户>解封 此时不应该传递 角色数据
            ShareData.FormhidAdd = form.Handle.ToInt32();
            form.ShowDialog();
            Application.DoEvents();
        }

        /// <summary>
        /// 客服主页,查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGameSearch_Click(object sender, EventArgs e)
        {
            string telphoneStr = txtbCtelephone.Text.Trim();
            string gameuserid = txtbCgameuserID.Text.Trim();
            string gameuserStr = txtbCgameuser.Text.Trim();
            string gameroleid = txtbCgameroleID.Text.Trim();
            string gameroleStr = txtbCgamerole.Text.Trim();
            string personidStr = txtbCpersonid.Text.Trim();


            MsgParam msgparam = new MsgParam();
            string whereStr = " ";

            if ((telphoneStr + gameuserid + gameroleid + gameuserStr + gameroleStr + personidStr).Length == 0)
            {
                MsgBox.Show(Text = LanguageResource.Language.Tip_PleaseInputQueryWhere, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gameuserStr.Length > 0 && gameuserStr.Length < 3)
            {
                MsgBox.Show(LanguageResource.Language.Tip_AccountLeastInputTopThree + "！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gameuserid.Length > 0)
            {
                whereStr += " and a.F_UserID=" + gameuserid + "";
            }
            if (gameuserStr.Length > 0)
            {
                whereStr += " and a.F_UserName like '" + gameuserStr + "%'";
            }
            if (gameroleStr.Length > 0)
            {
                msgparam.p0 = gameroleStr;
            }
            if (personidStr.Length > 0 || telphoneStr.Length > 0)
            {
                whereStr += "  ";
                if (personidStr.Length > 0)
                {
                    whereStr += " and  F_PersonID='" + gameuserStr + "'";
                }
                if (telphoneStr.Length > 0)
                {
                    whereStr += " and  F_TelPhone='" + gameuserStr + "'";
                }
                whereStr += ")";
            }
            whereStr += " order by a.F_UserID asc";
            "Gss Client Query User Or Role".Logger();
            whereStr.Logger();
            msgparam.p1 = whereStr;
            DGVGameUser.DataSource = null;
            DGVGameRole.DataSource = null;
            _clihandle.GetGameUsersC(whereStr);
            "GetGameUsersC".Logger();
            buttonGameSearch.Text = queryAccount + " 5";
            buttonGameSearch.Enabled = false;
            buttonGameSearchRole.Enabled = false;
            buttonGameRest.Enabled = false;
            Application.DoEvents();
            timerGameSearch.Start();
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGameSearchRole_Click(object sender, EventArgs e)
        {
            string gameroleStr = txtbCgamerole.Text.Trim();
            string gameroleid = txtbCgameroleID.Text.Trim();

            txtbCtelephone.Text = "";
            txtbCgameuser.Text = "";
            txtbCpersonid.Text = "";

            if ((gameroleid + gameroleStr).Length == 0)
            {
                MsgBox.Show(Text = LanguageResource.Language.Tip_PleaseInputQueryWhere, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gameroleStr.Length > 0 && gameroleStr.Length < 2)
            {
                MsgBox.Show(LanguageResource.Language.Tip_QueryRoleNameInputLimit, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MsgParam msgparam = new MsgParam();
            string whereStr = " ";
            if (gameroleid.Length > 0)
            {
                whereStr += " and F_RoleID =" + gameroleid + "";
            }
            if (gameroleStr.Length > 0)
            {
                whereStr += " and  F_RoleName like N'" + gameroleStr + "%'";
            }
            msgparam.p1 = whereStr;
            DGVGameUser.DataSource = null;
            DGVGameRole.DataSource = null;
            string where = SystemConfig.BigZoneName + "|" + whereStr;
            ("button[GameSearchRole] ->GetGameRolesCR-> where \t:" + where).Logger();
            _clihandle.GetGameRolesCR(where);

            buttonGameSearch.Text = queryAccount + " 5";
            buttonGameSearch.Enabled = false;
            buttonGameSearchRole.Enabled = false;
            buttonGameRest.Enabled = false;
            Application.DoEvents();
            timerGameSearch.Start();
        }

        /// <summary>
        /// 客服主页,查询条件清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            txtbCtelephone.ResetText();
            txtbCgameuser.ResetText();
            txtbCgamerole.ResetText();
            txtbCpersonid.ResetText();
            txtbCgameuserID.ResetText();
            txtbCgameroleID.ResetText();
        }

        /// <summary>
        /// 游戏用户列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVGameUser_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVGameUser.SelectedRows.Count > 0)
            {
                string bigzonename = DGVGameUser.SelectedRows[0].Cells[2].Value.ToString();
                string uid = DGVGameUser.SelectedRows[0].Cells[0].Value.ToString();

                if (!(DGVGameRole.RowCount > 0 && DGVGameRole.SelectedRows[0].Cells[8].Value.ToString() == uid))
                {
                    Application.DoEvents();
                    _clihandle.GetGameRolesC(bigzonename + "|" + uid);
                }
            }
        }

        /// <summary>
        /// 游戏用户列表单击事件 右键
        /// </summary>
        private void DGVGameUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVGameUser.Rows[e.RowIndex].Selected)
                {
                    DGVGameUser.ClearSelection();
                    DGVGameUser.Rows[e.RowIndex].Selected = true;
                }
                if (DGVGameUser.SelectedRows.Count == 1)
                {
                    DGVGameUser.CurrentCell = DGVGameUser.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripGUser.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 游戏角色列表单击事件 右键
        /// </summary>
        private void DGVGameRole_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVGameRole.Rows[e.RowIndex].Selected)
                {
                    DGVGameRole.ClearSelection();
                    DGVGameRole.Rows[e.RowIndex].Selected = true;
                }
                if (DGVGameRole.SelectedRows.Count == 1)
                {
                    DGVGameRole.CurrentCell = DGVGameRole.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripGRole.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 角色列表格式化事件
        /// </summary>
        private void DGVGameRole_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DGVGameRole.Columns[e.ColumnIndex].Name.Equals("ColumnF_ZoneID") && DGVGameRole.Rows[e.RowIndex].Cells["ColumnF_ZoneID"].Value != null)
            {
                //VIP图标
                string value = DGVGameRole.Rows[e.RowIndex].Cells["ColumnF_ZoneID"].Value.ToString();
                if (value.Trim().Length > 0)
                {
                    e.Value = ClientCache.GetZoneName(value);
                }
            }
        }


        #endregion

        #region 工单相关

        /// <summary>
        /// 获取工单列表
        /// </summary>
        /// <param name="value"></param>
        private void GetTaskList(string sqlvalue)
        {
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton" && (control as System.Windows.Forms.ToolStripButton).Checked)
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)control;
                    if (toolbtnActiv.Name == "toolStripButtonT0")
                    {
                        tabControl1.SelectedIndex = 0;
                    }
                    else if (toolbtnActiv.Name == "toolStripButtonT11")
                    {
                        tabControl1.SelectedIndex = 2;

                        string whereStr = " and F_Type=20000213";
                        string orderStr = "order by F_ID desc";
                        richtbTasklogGN.Clear();
                        richtbTasklogGN.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklogGN.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            _clihandle.GetAllTasks("toolStripButtonT11", whereStr, "", orderStr, controlPagerN.PageSize, controlPagerN.PageIndex);
                        }
                        catch (System.Exception ex)
                        {
                            ex.Message.ErrorLogger();
                            //日志记录
                            ShareData.Log.Error(ex);
                        }

                    }
                    else if (toolbtnActiv.Name == "toolStripButtonT12")
                    {
                        tabControl1.SelectedIndex = 3;

                        string whereStr = " and F_Type=20000214";
                        string orderStr = "order by F_ID desc";

                        richtbTasklogGA.Clear();
                        richtbTasklogGA.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklogGA.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            _clihandle.GetAllTasks("toolStripButtonT12", whereStr, "", orderStr, controlPagerA.PageSize, controlPagerA.PageIndex);
                        }
                        catch (System.Exception ex)
                        {
                            ex.Message.ErrorLogger();
                            //日志记录
                            ShareData.Log.Error(ex);
                        }

                    }
                    else
                    {
                        tabControl1.SelectedIndex = 1;
                        string whereStr = " and F_Type<>20000213  and F_Type<>20000214 and  F_Type<>20000216 and F_Type<>20000217";
                        string orderStr = "order by F_ID desc";
                        switch (toolbtnActiv.Name)
                        {
                            case "toolStripButtonT1":
                                whereStr += sqlvalue + " and F_State=" + SystemConfig.DefaultWorkOrderStatue + "  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT2":
                                whereStr += sqlvalue + " and F_State=100100101  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT3":
                                whereStr += sqlvalue + " and F_State=100100102  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT4":
                                whereStr += sqlvalue + " and F_State=100100103  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT5":
                                whereStr += sqlvalue + " and F_State=100100104  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT6":
                                whereStr += sqlvalue + " and F_State=100100105  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT7":
                                whereStr += sqlvalue + " and F_State=100100106  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT8":
                                whereStr += sqlvalue + " and F_State=100100107  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT9":
                                whereStr += sqlvalue + " and F_State=100100108  order by F_CreatTime desc, F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT10":
                                whereStr = " and F_Type<>20000213  and F_Type<>20000214 " + sqlvalue + GetMyTaskSqlStr();
                                break;
                            case "toolStripButtonT13":
                                whereStr = sqlvalue + " and F_Type=20000216 ";
                                orderStr = "order by F_TToolUsed asc,F_DutyMan asc,F_EditTime DESC";
                                break;
                            default:
                                break;
                        }
                        richtbTasklog.Clear();
                        richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklog.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            string.Format("query task list where ->{0}", whereStr).Logger();
                            _clihandle.GetAllTasks(toolbtnActiv.Name, whereStr, "", orderStr, controlPagerT.PageSize, controlPagerT.PageIndex);
                            return;
                        }
                        catch (System.Exception ex)
                        {
                            ex.Message.ErrorLogger();
                            //日志记录
                            ShareData.Log.Error(ex);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 得到我的工单的SQL
        /// </summary>
        private string GetMyTaskSqlStr()
        {
            string sqlStr = "";
            if (ShareData.UserID.Trim().Length == 0)
            {
                return " and 1<>1";
            }
            foreach (ToolStripItem control in this.toolStripMyTask.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton" && (control as System.Windows.Forms.ToolStripButton).Checked)
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)control;
                    tabControl1.SelectedIndex = 1;
                    switch (toolbtnActiv.Name)
                    {
                        case "toolStripButtonTT0":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " ";
                            break;
                        case "toolStripButtonTT1":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100101 ";
                            break;
                        case "toolStripButtonTT2":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100102  ";
                            break;
                        case "toolStripButtonTT3":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100103  ";
                            break;
                        case "toolStripButtonTT4":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100106  ";
                            break;
                        case "toolStripButtonTT5":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100104  ";
                            break;
                        case "toolStripButtonTT6":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100105  ";
                            break;
                        case "toolStripButtonTT7":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100107  ";
                            break;
                        case "toolStripButtonTT8":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100108  ";
                            break;
                        default:
                            break;
                    }
                    //sqlStr += " order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                    sqlStr += " order by F_ID desc";
                }
            }
            return sqlStr;
        }

        /// <summary>
        /// 工单查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTaskSearch_Click(object sender, EventArgs e)
        {
            string sqlwhere = "";
            if (WinUtil.IsNumber(txtbTtaskid.Text))
            {
                sqlwhere += " and F_ID=" + txtbTtaskid.Text.Trim();
            }
            if (txtbTguserid.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_GUserName='" + txtbTguserid.Text.Trim() + "'";
            }
            if (txtbTgroleid.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_GRoleName='" + txtbTgroleid.Text.Trim() + "'";
            }
            if (txtbTtelephone.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_Telphone='" + txtbTtelephone.Text.Trim() + "'";
            }
            if (combTtasktype.SelectedIndex > 0)
            {
                sqlwhere += " and F_Type=" + combTtasktype.SelectedValue + "";
            }

            if (dtpTCreatTime.Checked)
            {
                sqlwhere += " and convert(varchar(10),F_CreatTime,20)='" + dtpTCreatTime.Text + "'";
            }

            GetTaskList(sqlwhere);

        }

        /// <summary>
        /// 工单列表选中变更事件
        /// </summary>
        private void DGVTask_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTask.SelectedRows.Count > 0)
            {
                int idTask = Convert.ToInt32(DGVTask.SelectedRows[DGVTask.SelectedRows.Count - 1].Cells[2].Value);
                if (ShareData.DGVSelectTaskID != idTask)
                {
                    ShareData.DGVSelectTaskID = idTask;
                    richtbTasklog.Clear();
                    Application.DoEvents();
                    string sqlwhere = "and F_ID=" + idTask + " order by F_LogID asc";
                    _clihandle.GetTaskLog(sqlwhere);
                }
            }
        }

        /// <summary>
        /// 工单列表格式化事件
        /// </summary>
        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTask.Columns[e.ColumnIndex].Name.Equals("ColumnVipPic") && DGVTask.Rows[e.RowIndex].Cells["ColumnVipLevel"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTask.Rows[e.RowIndex].Cells["ColumnVipLevel"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTask.Columns[e.ColumnIndex].Name.Equals("ColumnLimitT") && DGVTask.Rows[e.RowIndex].Cells["ColumnLimitTime"].Value != null)
            {
                //工单限时
                string timStr = DGVTask.Rows[e.RowIndex].Cells["ColumnLimitTime"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTask.Rows[e.RowIndex].Cells[8].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTask.Rows[e.RowIndex].Cells[8].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }


        }

        /// <summary>
        /// 工单列表:重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            txtbTtaskid.ResetText();
            txtbTguserid.ResetText();
            txtbTgroleid.ResetText();
            txtbTtelephone.ResetText();
            combTtasktype.SelectedIndex = 0;
            combTtasktypeP.SelectedIndex = 0;
            controlPagerT.PageIndex = 1;
        }


        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTask.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTask.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            String type = drtask.Cells[3].Value.ToString();
            if (type.IndexOf(LanguageResource.Language.BtnOnlineConsume) != -1)
            {
                if (ShareData.FormChat == null)
                {
                    ShareData.FormChat = new FormChat(_clihandle);
                }
                string rolename = drtask.Cells[6].Value.ToString();
                ShareData.FormChat.DGVInsertF(taskid, rolename);
                ShareData.FormChat.Show();
                ShareData.FormChat.BringToFront();
            }
            else
            {
                FormTaskEdit formtaskedit = new FormTaskEdit(_clihandle, taskid);
                ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
                ShareData.DGVSelectIndex = drtask.Index;
                DialogResult dresult = formtaskedit.ShowDialog();
                Application.DoEvents();
                if (dresult == DialogResult.OK)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }
            }

            Application.DoEvents();
        }

        /// <summary>
        /// 工单列表单击事件
        /// </summary>
        private void DGVTask_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVTask.Rows[e.RowIndex].Selected)
                {
                    DGVTask.ClearSelection();
                    DGVTask.Rows[e.RowIndex].Selected = true;
                }
                if (DGVTask.SelectedRows.Count == 1)
                {
                    DGVTask.CurrentCell = DGVTask.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripTask.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 选中父工单类型
        /// </summary>
        private void combTtasktypeP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combTtasktypeP.SelectedIndex > 0)
            {
                BindDicComb(combTtasktype, combTtasktypeP.SelectedValue.ToString());
            }
            else
            {
                combTtasktype.SelectedValue = 0;
            }

        }

        #endregion

        #region 菜单相关

        /// <summary>
        /// 菜单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonT0_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    (control as System.Windows.Forms.ToolStripButton).Checked = false;
                }
            }
            ToolStripMenuItemGNStart.Visible = false;
            ToolStripMenuItemGNStop.Visible = false;
            ToolStripMenuItemTReceive.Visible = false;
            ToolStripMenuItemTSeachU.Visible = true;
            ToolStripMenuItemTClose.Visible = false;
            ToolStripMenuItemTaskDeal.Visible = true;
            ToolStripItem ele = sender as ToolStripItem;
            ele.Name.Logger();
            if (ele.Name == "toolStripButtonT10")
            {
                groupBoxMyTask.Visible = true;
                groupBoxTsearch.Location = new Point(6, 59);
                groupBoxTtasklist.Location = new Point(6, 119);
                groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 119);
                groupBoxTtasklist.Height = tabPage2.Size.Height - 125;
                groupBoxTtasklog.Height = tabPage2.Size.Height - 125;
            }
            else if ((sender as ToolStripItem).Name == "toolStripButtonT11")
            {

                ToolStripMenuItemGNStart.Visible = true;
                ToolStripMenuItemGNStop.Visible = true;

            }
            else
            {
                groupBoxMyTask.Visible = false;
                groupBoxTsearch.Location = new Point(6, 6);
                groupBoxTtasklist.Location = new Point(6, 71);
                groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
                groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
                groupBoxTtasklog.Height = tabPage2.Size.Height - 77;

                if ((sender as ToolStripItem).Name == "toolStripButtonT1")
                {
                    ToolStripMenuItemTReceive.Visible = true;

                }
                ToolStripMenuItemTClose.Visible = true;
            }
            controlPagerT.PageIndex = 1;
            controlPagerA.PageIndex = 1;
            controlPagerN.PageIndex = 1;

            System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
            toolbtnActiv.Checked = true;
            GetTaskList("");
        }


        /// <summary>
        /// 我的工单菜单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTT0_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem control in this.toolStripMyTask.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    (control as System.Windows.Forms.ToolStripButton).Checked = false;
                }

            }
            controlPagerT.PageIndex = 1;

            System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
            toolbtnActiv.Checked = true;
            GetTaskList("");
        }

        /// <summary>
        /// 绘图:主菜单画新工单提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonT0_Paint(object sender, PaintEventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
            {
                System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
                if (toolbtnActiv.Name != "toolStripButtonT0")
                {
                    DrawMainTaskAlert(e.Graphics, toolbtnActiv);
                }

            }
        }

        /// <summary>
        /// 绘图:主菜单工单提醒图标
        /// </summary>
        private void DrawMainTaskAlert(Graphics g, ToolStripButton toolbtnActiv)
        {
            try
            {
                string drawStr = "0";
                float x = toolbtnActiv.Width - 34;
                float y = 5.0F;
                string filterStr = "";
                switch (toolbtnActiv.Name)
                {
                    case "toolStripButtonT10":
                        filterStr = "F_DutyMan=" + ShareData.UserID + "";
                        break;
                    case "toolStripButtonT1":
                        filterStr = "F_State=" + SystemConfig.DefaultWorkOrderStatue + " and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT2":
                        filterStr = "F_State=100100101 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT3":
                        filterStr = "F_State=100100102 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT4":
                        filterStr = "F_State=100100103 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT5":
                        filterStr = "F_State=100100104 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT6":
                        filterStr = "F_State=100100105 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT7":
                        filterStr = "F_State=100100106 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT8":
                        filterStr = "F_State=100100107 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT9":
                        filterStr = "F_State=100100108 and F_Type<>20000213 and F_Type<>20000214 and F_Type<>20000216 ";
                        break;
                    case "toolStripButtonT11":
                        filterStr = "F_Type=20000213";
                        break;
                    case "toolStripButtonT12":
                        filterStr = "F_Type=20000214";
                        break;
                    case "toolStripButtonT13":
                        filterStr = "F_Type=20000216 and  (F_DutyMan=-1 or F_DutyMan is null)";
                        break;
                    default:
                        filterStr = "1<>1";
                        break;
                }
                DataSet ds = ClientCache.GetTaskCache("TaskAlertNum");
                if (ds != null)
                {
                    DataRow[] drs = ds.Tables[0].Select(filterStr);
                    int num = 0;
                    foreach (DataRow dr in drs)
                    {
                        num += Convert.ToInt32(dr["F_Num"]);
                    }
                    drawStr = num.ToString();
                }

                DrawAlertImgNumM(g, drawStr, x, y);
            }
            catch (System.Exception ex)
            {
                //nothing
            }
        }

        /// <summary>
        /// 绘图:底图+字 (大图)
        /// </summary>
        private void DrawAlertImgNumM(Graphics g, string drawStr, float x, float y)
        {
            if (drawStr == "0")
            {
                return;
            }
            //定义变量
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; //高像素偏移质量
            Font drawFont = new Font("DotumChe", 10F, System.Drawing.FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            //if (DateTime.Now.Millisecond % 5 == 0)
            //{
            //    drawBrush = new SolidBrush(System.Drawing.SystemColors.ScrollBar);
            //}
            Image backimg = GetImage(Application.StartupPath + "\\GSSData\\Images\\numerbg.png");
            if (drawStr.Length >= 3)
            {
                g.DrawImage(backimg, x, y, 34, 15.8F);
            }
            else
            {
                g.DrawImage(backimg, x, y, 30, 15.8F);
            }


            float sx = x + 10;
            float sy = y + 1.9F;

            if (drawStr.Length == 2)
            {
                sx -= 3.3F;
            }
            else if (drawStr.Length == 3)
            {
                sx -= 5.0F;
            }
            else if (drawStr.Length > 3)
            {
                sx -= 5.0F;
                drawStr = "+++";
            }

            PointF drawPoint = new PointF(sx, sy);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoClip;

            g.DrawString(drawStr, drawFont, drawBrush, drawPoint, drawFormat);
        }


        /// <summary>
        /// 绘图:我的菜单画新工单提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTT0_Paint(object sender, PaintEventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
            {
                System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
                DrawMyTaskAlert(e.Graphics, toolbtnActiv);
            }
        }


        /// <summary>
        /// 绘图:我的工单提醒图标
        /// </summary>
        private void DrawMyTaskAlert(Graphics g, ToolStripButton toolbtnActiv)
        {
            try
            {
                string drawStr = "0";
                float x = toolbtnActiv.Width - 27;
                float y = 0;
                string filterStr = "F_Type<>20000213 and F_Type<>20000214 and";
                switch (toolbtnActiv.Name)
                {
                    case "toolStripButtonTT0":
                        filterStr = "F_DutyMan=" + ShareData.UserID + "";
                        break;
                    case "toolStripButtonTT1":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100101";
                        break;
                    case "toolStripButtonTT2":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100102";
                        break;
                    case "toolStripButtonTT3":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100103";
                        break;
                    case "toolStripButtonTT4":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100106";
                        break;
                    case "toolStripButtonTT5":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100104";
                        break;
                    case "toolStripButtonTT6":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100105";
                        break;
                    case "toolStripButtonTT7":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100107";
                        break;
                    case "toolStripButtonTT8":
                        filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100108";
                        break;
                    default:
                        filterStr = "1<>1";
                        break;
                }
                DataSet ds = ClientCache.GetTaskCache("TaskAlertNum");
                if (ds != null)
                {
                    DataRow[] drs = ds.Tables[0].Select(filterStr);
                    int num = 0;
                    foreach (DataRow dr in drs)
                    {
                        num += Convert.ToInt32(dr["F_Num"]);
                    }
                    drawStr = num.ToString();
                }

                DrawAlertImgNum(g, drawStr, x, y);
            }
            catch (System.Exception ex)
            {
                //nothing
            }

        }

        /// <summary>
        /// 绘图:底图+字
        /// </summary>
        private void DrawAlertImgNum(Graphics g, string drawStr, float x, float y)
        {
            if (drawStr == "0")
            {
                return;
            }
            //定义变量
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; //高像素偏移质量
            Font drawFont = new Font("DotumChe", 8, System.Drawing.FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            //if (DateTime.Now.Second % 2 == 0)
            //{
            //    //drawBrush = new SolidBrush(System.Drawing.SystemColors.ScrollBar);
            //    drawFont = new Font("DotumChe", 8F, System.Drawing.FontStyle.Bold);
            //}
            Image backimg = GetImage(Application.StartupPath + "\\GSSData\\Images\\numerbg.png");
            g.DrawImage(backimg, x, y, 28, 14.3F);

            float sx = x + 10;
            float sy = y + 1.9F;

            if (drawStr.Length == 2)
            {
                sx -= 2;
            }
            else if (drawStr.Length == 3)
            {
                sx -= 5;
            }
            else if (drawStr.Length > 3)
            {
                sx -= 5;
                drawStr = "+++";
            }

            PointF drawPoint = new PointF(sx, sy);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoClip;

            g.DrawString(drawStr, drawFont, drawBrush, drawPoint, drawFormat);
        }


        #endregion

        #region 右键快捷菜单事件

        private void 查询帐号相关工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            string whereStr = " and ( F_GUserID='" + DGVGameUser.SelectedRows[0].Cells[0].Value + "' or F_Note like '%" + DGVGameUser.SelectedRows[0].Cells[0].Value + "%')  ";
            string orderStr = "order by F_ID desc";
            richtbTasklog.ResetText();
            richtbTasklog.ForeColor = Color.DimGray;
            richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;

            groupBoxMyTask.Visible = false;
            groupBoxTsearch.Location = new Point(6, 6);
            groupBoxTtasklist.Location = new Point(6, 71);
            groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
            groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
            groupBoxTtasklog.Height = tabPage2.Size.Height - 77;
            ToolStripMenuItemTReceive.Visible = false;

            Application.DoEvents();
            _clihandle.GetAllTasks("", whereStr, "", orderStr, controlPagerT.PageSize, 1);
        }

        private void 查询角色相关工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            string whereStr = " and F_GRoleID='" + DGVGameRole.SelectedRows[0].Cells[0].Value + "' ";
            string orderStr = "order by F_ID desc";
            richtbTasklog.ResetText();
            richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
            richtbTasklog.ForeColor = Color.DimGray;

            groupBoxMyTask.Visible = false;
            groupBoxTsearch.Location = new Point(6, 6);
            groupBoxTtasklist.Location = new Point(6, 71);
            groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
            groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
            groupBoxTtasklog.Height = tabPage2.Size.Height - 77;
            ToolStripMenuItemTReceive.Visible = false;

            Application.DoEvents();
            _clihandle.GetAllTasks("", whereStr, "", orderStr, controlPagerT.PageSize, 1);
        }

        private void 处理工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVTask.Visible)
            {
                if (DGVTask.SelectedRows.Count == 0)
                {
                    return;
                }
                DataGridViewRow drtask = DGVTask.SelectedRows[0];
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                FormTaskEdit formtaskedit = new FormTaskEdit(_clihandle, taskid);
                ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
                ShareData.DGVSelectIndex = drtask.Index;
                DialogResult dresult = formtaskedit.ShowDialog();
                Application.DoEvents();
                if (dresult == DialogResult.OK)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }
            }
            else if (DGVTaskGN.Visible)
            {
                if (DGVTaskGN.SelectedRows.Count == 0)
                {
                    return;
                }
                DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                FormTaskEditGN formtaskedit = new FormTaskEditGN(_clihandle, taskid);
                ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
                ShareData.DGVSelectIndex = drtask.Index;
                DialogResult dresult = formtaskedit.ShowDialog();
                Application.DoEvents();
                if (dresult == DialogResult.OK)
                {
                    DGVTaskGN.Rows.RemoveAt(DGVTaskGN.SelectedRows[0].Index);
                }
            }

        }
        /// <summary>
        /// 接收工单
        /// </summary>
        private void ToolStripMenuItemTReceive_Click(object sender, EventArgs e)
        {
            if (DGVTask.SelectedRows.Count > 30)
            {
                MsgBox.Show("批量选择的工单,请不要超过30个", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_AcceptSelectWorkOrder, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.DoEvents();
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            GSSBLL.Tasks bll = ClientRemoting.Tasks();

            foreach (DataGridViewRow drtask in DGVTask.SelectedRows)
            {
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                GSSModel.Tasks model = new GSSModel.Tasks();
                model.F_ID = taskid;
                model.F_State = 100100101;
                model.F_DutyMan = int.Parse(ShareData.UserID);
                model.F_EditMan = int.Parse(ShareData.UserID);
                model.F_EditTime = DateTime.Now;
                model.F_Rowtype = null;


                //string back = clihandle.EditTaskSyn(model);
                int back = bll.Edit(model);
                if (back != 0)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }

                Application.DoEvents();
            }
            System.Threading.Thread.Sleep(1000);
            _clihandle.GetAlertNum();
            //ClientCache.SetTaskCache(DataSerialize.GetDataSetSurrogateZipBYtes(bll.GetAlertNum()) , "TaskAlertNum");
        }

        private void 停止公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_SureStopSelectNotcie, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.DoEvents();
            if (IsOK != DialogResult.Yes)
            {
                return;
            }

            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            ShareData.DGVSelectIndex = drtask.Index;

            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = taskid;
            model.F_EditMan = int.Parse(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;


            string backStr = _clihandle.GameNoticeStopSyn(taskid.ToString());
            Application.DoEvents();
            if (backStr == "true")
            {
                MsgBox.Show(LanguageResource.Language.LblSuccStopPlacard, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                model.F_COther = LanguageResource.Language.LblSuccStopPlacard;
                model.F_OCanRestor = false;
            }
            else
            {
                MsgBox.Show(LanguageResource.Language.LblFailureStopPlacard + " ！" + backStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                model.F_COther = LanguageResource.Language.LblFailureStopPlacard + " ！" + backStr;
            }
            Application.DoEvents();
            backStr = _clihandle.EditTaskSyn(model);
            Application.DoEvents();
            GetTaskList("");
        }

        private void 运行公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_SureRunSelectNotcie, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.DoEvents();
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            ShareData.DGVSelectIndex = drtask.Index;

            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = taskid;
            model.F_EditMan = int.Parse(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;


            string backStr = _clihandle.GameNoticeStartSyn(taskid.ToString());
            Application.DoEvents();
            if (backStr == "true")
            {
                MsgBox.Show(LanguageResource.Language.Tip_SuccRunPlacard, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                model.F_COther = LanguageResource.Language.Tip_SuccRunPlacard;
                model.F_OCanRestor = true;

            }
            else
            {
                MsgBox.Show( LanguageResource.Language.Tip_RunNoticeFailure + backStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                model.F_COther =  LanguageResource.Language.Tip_RunNoticeFailure + backStr;
            }
            Application.DoEvents();
            backStr = _clihandle.EditTaskSyn(model);
            GetTaskList("");
        }

        #endregion

        /// <summary>
        /// 定时操作器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMain_Tick(object sender, EventArgs e)
        {
            //刷新主菜单和我的工单菜单
            toolStripMain.Refresh();
            if (toolStripButtonT10.Checked)
            {
                toolStripMyTask.Refresh();
            }
            Application.DoEvents();

            //更新底部时间显示
            toolStripStatusLabelTime.Text = string.Format("{0:yyyy"+LanguageResource.Language.LblUnitYear+"MM"+LanguageResource.Language.LblUnitMonth+"dd"+LanguageResource.Language.LblUnitDay+" dddd HH:mm:ss }", DateTime.Now);
            //窗体停靠
            SetAnchor();
            Application.DoEvents();

            if (toolStripButtonT0.Checked)
            {
                return;
            }

            if (toolStripButtonT11.Checked)
            {

                //更新工单列表倒计时
                foreach (DataGridViewRow dr in DGVTaskGN.Rows)
                {
                    if (WinUtil.IsDateTime(dr.Cells["ColumnLimitTimeGN"].Value.ToString()) && Convert.ToDateTime(dr.Cells["ColumnLimitTimeGN"].Value).ToShortDateString() != Convert.ToDateTime("1900-1-1").ToShortDateString())
                    {
                        if (dr.Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || dr.Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                        {
                            return;
                        }

                        DateTime limtime = Convert.ToDateTime(dr.Cells["ColumnLimitTimeGN"].Value);
                        TimeSpan ts = limtime.Subtract(DateTime.Now);
                        string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                        dr.Cells[1].Value = limitStr;

                        double hours = ts.TotalHours;

                        if (hours < 0)
                        {
                            dr.Cells[1].Style.BackColor = Color.Red;
                        }
                        else if (hours < 4)
                        {
                            dr.Cells[1].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }
            else
            {
                //更新工单列表倒计时
                foreach (DataGridViewRow dr in DGVTask.Rows)
                {
                    if (WinUtil.IsDateTime(dr.Cells["ColumnLimitTime"].Value.ToString()) && Convert.ToDateTime(dr.Cells["ColumnLimitTime"].Value).ToShortDateString() != Convert.ToDateTime("1900-1-1").ToShortDateString())
                    {
                        if (dr.Cells[8].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || dr.Cells[8].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                        {
                            return;
                        }
                        DateTime limtime = Convert.ToDateTime(dr.Cells["ColumnLimitTime"].Value);
                        TimeSpan ts = limtime.Subtract(DateTime.Now);
                        string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                        dr.Cells[1].Value = limitStr;

                        double hours = ts.TotalHours;

                        if (hours < 0)
                        {
                            dr.Cells[1].Style.BackColor = Color.Red;
                        }
                        else if (hours < 4)
                        {
                            dr.Cells[1].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }
            Application.DoEvents();
        }

        private void timerGameSearch_Tick(object sender, EventArgs e)
        {
            string tag = buttonGameSearch.Tag==null?string.Empty:buttonGameSearch.Tag.ToString();
            int cur;
            int.TryParse(tag, out cur);
            if (!string.IsNullOrEmpty(tag)&&cur== 0)
            {
                timerGameSearch.Stop();
                buttonGameSearch.Enabled = true;
                buttonGameSearchRole.Enabled = true;
                buttonGameRest.Enabled = true;
                buttonGameSearch.Text = queryAccount;
                return;
            }
            try
            {
                string numStr = buttonGameSearch.Text.Substring(buttonGameSearch.Text.Length-1, 1);
                int num = Convert.ToInt32(numStr);
                num--;
                buttonGameSearch.Tag = num;
                buttonGameSearch.Text = queryAccount + (num).ToString();
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Error(ex);
            }
        }

        private void aButton1_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice form = new FormTaskAddGameNotice(_clihandle, 20000213);
            form.Show();
        }

        private void aButton2_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice1 form = new FormTaskAddGameNotice1(_clihandle, 20000213);
            form.Show();
        }

        private void aButton3_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice2 form = new FormTaskAddGameNotice2(_clihandle, 20000213);
            form.ShowDialog();
            Application.DoEvents();
        }

        /// <summary>
        /// 工单变更事件 喊话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGN_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTaskGN.SelectedRows.Count > 0)
            {
                int idTask = Convert.ToInt32(DGVTaskGN.SelectedRows[DGVTaskGN.SelectedRows.Count - 1].Cells[2].Value);
                if (ShareData.DGVSelectTaskID != idTask)
                {
                    ShareData.DGVSelectTaskID = idTask;
                    richtbTasklogGN.Clear();
                    Application.DoEvents();
                    string sqlwhere = "and F_ID=" + idTask + " order by F_LogID asc";
                    _clihandle.GetTaskLog(sqlwhere);
                }
            }
        }

        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口 喊话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTaskGN.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            FormTaskEditGN formtaskedit = new FormTaskEditGN(_clihandle, taskid);
            ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
            ShareData.DGVSelectIndex = drtask.Index;
            DialogResult dresult = formtaskedit.ShowDialog();
            Application.DoEvents();
            if (dresult == DialogResult.OK)
            {
                DGVTaskGN.Rows.RemoveAt(DGVTaskGN.SelectedRows[0].Index);
            }

        }

        /// <summary>
        /// 工单列表单击事件 喊话
        /// </summary>
        private void DGVTaskGN_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVTaskGN.Rows[e.RowIndex].Selected)
                {
                    DGVTaskGN.ClearSelection();
                    DGVTaskGN.Rows[e.RowIndex].Selected = true;
                }
                if (DGVTaskGN.SelectedRows.Count == 1)
                {
                    DGVTaskGN.CurrentCell = DGVTaskGN.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string gnstate = DGVTaskGN.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (gnstate == "True")
                    {
                        ToolStripMenuItemGNStart.Enabled = false;
                        ToolStripMenuItemGNStop.Enabled = true;
                    }
                    else
                    {
                        ToolStripMenuItemGNStart.Enabled = true;
                        ToolStripMenuItemGNStop.Enabled = false;
                    }
                }

                contextMenuStripTask.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 工单列表格式化事件
        /// </summary>
        private void DGVTaskGNGN_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnVipPicGN") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnVipLevelGN"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnVipLevelGN"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnLimitTGN") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnLimitTimeGN"].Value != null)
            {
                //工单限时
                string timStr = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnLimitTimeGN"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTaskGN.Rows[e.RowIndex].Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTaskGN.Rows[e.RowIndex].Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }
            else if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnGNF_OCanRestor") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnGNF_OCanRestor"].Value != null)
            {
                //公告状态
                string state = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnGNF_OCanRestor"].Value.ToString();
                if (state == "True")
                {
                    e.Value = LanguageResource.Language.LblRunning;
                    return;
                }
                else
                {
                    e.Value = LanguageResource.Language.LblEndRunning;
                    return;
                }

            }


        }

        /// <summary>
        /// 工单变更事件 发奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGA_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTaskGA.SelectedRows.Count > 0)
            {
                int idTask = Convert.ToInt32(DGVTaskGA.SelectedRows[DGVTaskGA.SelectedRows.Count - 1].Cells[2].Value);
                if (ShareData.DGVSelectTaskID != idTask)
                {
                    ShareData.DGVSelectTaskID = idTask;
                    richtbTasklogGA.Clear();
                    Application.DoEvents();
                    string sqlwhere = "and F_ID=" + idTask + " order by F_LogID asc";
                    _clihandle.GetTaskLog(sqlwhere);
                }
            }
        }

        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口 发奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTaskGA.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGA.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            FormTaskEditGA formtaskedit = new FormTaskEditGA(_clihandle, taskid);
            ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
            ShareData.DGVSelectIndex = drtask.Index;
            DialogResult dresult = formtaskedit.ShowDialog();
            Application.DoEvents();
            if (dresult == DialogResult.OK)
            {
                DGVTaskGA.Rows.RemoveAt(DGVTaskGA.SelectedRows[0].Index);
            }

        }

        /// <summary>
        /// 工单列表单击事件 发奖
        /// </summary>
        private void DGVTaskGA_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// 工单列表格式化事件 发奖
        /// </summary>
        private void DGVTaskGA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnVipPicGA") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnVipLevelGA"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnVipLevelGA"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnLimitTGA") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnLimitTimeGA"].Value != null)
            {
                //工单限时
                string timStr = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnLimitTimeGA"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTaskGA.Rows[e.RowIndex].Cells["ColumnF_StateGA"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTaskGA.Rows[e.RowIndex].Cells["ColumnF_StateGA"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }
            else if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnGAF_OCanRestor") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnGAF_OCanRestor"].Value != null)
            {
                //公告状态
                string state = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnGAF_OCanRestor"].Value.ToString();
                if (state == "True")
                {
                    e.Value = LanguageResource.Language.LblRunning;
                    return;
                }
                else
                {
                    e.Value = LanguageResource.Language.LblEndRunning;
                    return;
                }

            }


        }

        /// <summary>
        /// 得到单节值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetNodeValue(string a, string b, string c)
        {

            DataSet ds = ClientCache.GetCacheDS();
            DataRow[] dra = null;
            DataRow[] drb = null;
            DataRow[] drc = null;
            if (ds == null)
            {
                return LanguageResource.Language.Tip_GetError;
            }
            string backstr = "";
            if (a == "-1")
            {
                backstr += LanguageResource.Language.LblAllBigZone;
            }
            else
            {
                dra = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + a + "'");
                if (dra.Length > 0)
                {
                    backstr += dra[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowBigZone;
                }
            }
            if (b == "-1")
            {
                backstr += "/" + LanguageResource.Language.LblAllZone;
            }
            else
            {
                drb = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + b + "' and F_ParentID=" + dra[0]["F_ID"].ToString() + "");
                if (drb.Length > 0)
                {
                    backstr += "/" + drb[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowZone;
                }
            }
            if (c == "-1")
            {
                backstr += "/" + LanguageResource.Language.LblAllZoneLine;
            }
            else
            {
                drc = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + c + "' and F_ParentID=" + drb[0]["F_ID"].ToString() + "");
                if (drc.Length > 0)
                {
                    backstr += "/" + drc[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowZoneLine;
                }
            }

            return backstr;
        }

        private void aButton1_Click_1(object sender, EventArgs e)
        {
            FormTaskAddGameGiftAward form = new FormTaskAddGameGiftAward(_clihandle, 20000214);
            form.ShowDialog();
            Application.DoEvents();
        }

        private void richtbTasklog_DoubleClick(object sender, EventArgs e)
        {
            string url = attfiles.Replace("|", "");
            if (url.IndexOf("http://") == -1)
            {
                url = ClientCache.GetGameConfigValue("12") + url;
            }
            System.Diagnostics.Process.Start("iexplore", url);
        }

        //查询工单
        private void ToolStripMenuItemTSeachU_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            string whereStr = " and F_GUserID='" + DGVTask.SelectedRows[0].Cells[5].Value + "' ";
            string orderStr = "order by F_ID desc";
            richtbTasklog.ResetText();
            richtbTasklog.ForeColor = Color.DimGray;
            richtbTasklog.Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;

            groupBoxMyTask.Visible = false;
            groupBoxTsearch.Location = new Point(6, 6);
            groupBoxTtasklist.Location = new Point(6, 71);
            groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
            groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
            groupBoxTtasklog.Height = tabPage2.Size.Height - 77;
            ToolStripMenuItemTReceive.Visible = false;

            Application.DoEvents();
            _clihandle.GetAllTasks("", whereStr, "", orderStr, controlPagerT.PageSize, 1);
        }
        //关闭工单
        private void ToolStripMenuItemTClose_Click(object sender, EventArgs e)
        {
            if (DGVTask.SelectedRows.Count > 30)
            {
                MsgBox.Show("批量选择的工单,请不要超过30个", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult IsOK = MsgBox.Show("确定要关闭所选工单吗?", LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.DoEvents();
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            GSSBLL.Tasks bll = ClientRemoting.Tasks();

            foreach (DataGridViewRow drtask in DGVTask.SelectedRows)
            {
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                GSSModel.Tasks model = new GSSModel.Tasks();
                model.F_ID = taskid;
                model.F_State = 100100108;
                model.F_DutyMan = int.Parse(ShareData.UserID);
                model.F_EditMan = int.Parse(ShareData.UserID);
                model.F_EditTime = DateTime.Now;

                int back = bll.Edit(model);
                if (back != 0)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }
                Application.DoEvents();
            }
            System.Threading.Thread.Sleep(1000);
            _clihandle.GetAlertNum();
        }

        private void controlPagerT_OnPageChanged(object sender, EventArgs e)
        {
            buttonTaskSearch_Click(null, null);
        }
        private void controlPagerN_OnPageChanged(object sender, EventArgs e)
        {
            GetTaskList("");
        }

        private void controlPagerA_OnPageChanged(object sender, EventArgs e)
        {
            GetTaskList("");
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = (DataTable)e.Argument;
            SetTaskLogValue(dt);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            if (ShareData.FormChat == null)
            {
                ShareData.FormChat = new FormChat(_clihandle);
            }
            ShareData.FormChat.Show();
            ShareData.FormChat.BringToFront();
        }

        private void lblChat_Click(object sender, EventArgs e)
        {
            btnChat.Focus();
            btnChat_Click(null, null);
        }

        private void btnUsersLock_Click(object sender, EventArgs e)
        {
            FormTaskAddUserLock form = new FormTaskAddUserLock();
            form.Show();
        }

        private void btnUserUnLock_Click(object sender, EventArgs e)
        {
            FormTaskAddUserUnLock form = new FormTaskAddUserUnLock();
            form.Show();
        }

        private void Other_BtnLoginAward_Click(object sender, EventArgs e)
        {
            Button btn=sender as Button;
            string title = btn.Text + "-" + btn.Parent.Text;
            string parentid = ClientCache.GetDicID(btn.Parent.Name);
            int tasktype = Convert.ToInt32(ClientCache.GetDicID(btn.Name, parentid));
            FormTaskAddLoginAward login = new FormTaskAddLoginAward(_clihandle, title, tasktype);
            login.Show();
        }


        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            FormSendEmail email = new FormSendEmail(_clihandle,0);
            email.ShowDialog();
        }
        private void StripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            if (mi == null)
            {
                ShareData.Log.Info("element event bind error"+sender);
                return;
            }
            string name = mi.Name;
            NoWorkOrderOfMenu form;
            string noworkMenu=typeof(NoWorkOrderOfMenu).Name;
            form=(NoWorkOrderOfMenu) Enum.Parse(typeof(NoWorkOrderOfMenu), name);
            string taskTypeId = ClientCache.GetDicID(noworkMenu + "_" + mi.Name);

            switch (form)
            {
                case NoWorkOrderOfMenu.EquipFallForm:
                    FromTaskActiveFallEquip target = new FromTaskActiveFallEquip(_clihandle,int.Parse(taskTypeId));
                    target.ShowDialog();
                    break;
                case NoWorkOrderOfMenu.ExchangeForm:

                    break;
                default:
                    break;
            }
        }

        private void Other_BtnFullServiceEmail_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string title = btn.Text + "-" + btn.Parent.Text;
            string parentid = ClientCache.GetDicID(btn.Parent.Name);
            int tasktype = Convert.ToInt32(ClientCache.GetDicID(btn.Name, parentid));
            FormTaskAddFullServiceEmail login = new FormTaskAddFullServiceEmail(_clihandle, title, tasktype);
            login.Show();
        }
    }
    enum NoWorkOrderOfMenu 
    {//这是不建立工单的页面加载菜单项
        EquipFallForm=1,
        ExchangeForm=2,
        ExpMoneyActiveForm=3 
    }
}
