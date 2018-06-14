using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using GSS.DBUtility;
using GSSUI;
using GSSCSFrameWork;
using GSSModel;
namespace GSSClient
{
    public partial class FormTaskAdd : GSSUI.AForm.FormMain
    {
        #region 私有变量
        /// <summary>
        /// 游戏帐号相关
        /// </summary>
        private DataGridViewRow _drguser;
        /// <summary>
        /// 游戏角色相关
        /// </summary>
        private DataGridViewRow _drrole;
        /// <summary>
        /// 客户端处理实例
        /// </summary>
        private ClientHandles _clienthandle;
        /// <summary>
        /// 工单编号
        /// </summary>
        private string _taskid = "";
        /// <summary>
        /// 工单类型
        /// </summary>
        private int? _tasktype = null;
        #endregion
        private string TaskTypeDesc;
        public int? UserID { get; private set; }//工单相关的用户ID
        public int? RoleID { get; private set; }//工单相关的角色id
        public int BigZoneID { get;private set; }
        public int? ZoneID { get; set; }
        object CreateTaskDetailLogicData { get; set; }
        string UserGaneName { get; set; }
        string RoleGameName { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clienthandle">客户端处理实例</param>
        /// <param name="drGuser">游戏帐号相关</param>
        /// <param name="drGrole">游戏角色相关</param>
        /// <param name="tasktype">游戏类型</param>
        /// <param name="taskTypeDesc">添加工单分类的描述【如果该值没有提供则查询数据库中title=tasktype的父节点内容+_+tasktype节点内容】</param>
        public FormTaskAdd(ClientHandles clienthandle, DataGridViewRow drGuser, DataGridViewRow drGrole, int tasktype,string taskTypeDesc=null)
        {
            //will call  service ? //recovery role
            InitializeComponent();
            InitLanguageText();
            _clienthandle = clienthandle;
            _drguser = drGuser;
            _drrole = drGrole;
            _tasktype = tasktype;
            if (_tasktype == 2213)
            {
                label2.Visible = false;
                cboxLimitTime.Visible = false;
            }
            TaskTypeDesc = taskTypeDesc;
            SetGameUR();
            SetControls();
            string.Format("work order type id={0}", tasktype).Logger();
            GetWorkOrderType(tasktype);
        }
        void GetWorkOrderType(int workOrderTypeID)
        {
            RoleOrAccountUnlock.Tag = "Account_btnUnlock|Role_btnUnlock";//此处值对应与 gssclient\formtask.cs 页面中工单button的name值
            string ele = ClientCache.GetDicName(workOrderTypeID.ToString());
            bool havaNowRun = false;
            foreach (Control item in pnlDonow.Controls)
            {
                item.Visible = false;
                object tag = item.Tag;
                string[] eleContainerWorkOrderTypes =new string[0];
                if (tag != null) 
                {
                    eleContainerWorkOrderTypes = tag.ToString().Split('|');//该元素适用于多种类型的工单
                }
                if (item.Name == ele||eleContainerWorkOrderTypes.Contains(ele))
                {
                    item.Visible = true;
                    havaNowRun = true;
                }
            }
            ckboxDonow.Visible = havaNowRun;
            if (havaNowRun) 
            {
                RoleOrAccountUnlock.Click += new EventHandler(Button_Click);
            }
        }
        void InitLanguageText() 
        {
            this.Account_BtnResetAntiIndulgence.Text = global::GSSClient.LanguageResource.Language.BtnResetAntiIndulgence;
            this.Account_btnPlayNo.Text = global::GSSClient.LanguageResource.Language.LblPlayNOTool;
            this.Role_btnGag.Text = global::GSSClient.LanguageResource.Language.BtnGagTool;
            this.Role_btnCloseDown.Text = global::GSSClient.LanguageResource.Language.BtnCloseDownRole;
            this.Account_btnCloseDown.Text = global::GSSClient.LanguageResource.Language.BtnCloseDownAccount;
            this.groupBoxInfo.Text = LanguageResource.Language.LblBaseInfo;
            this.lblTaskType.Text = LanguageResource.Language.LblWorkOrderType;
            this.lblURinfo.Text = LanguageResource.Language.LblBaseInfo;
            label1.Text = LanguageResource.Language.LblClipBoard;
            label2.Text = LanguageResource.Language.LblWorkOrderLimit;
            label3.Text = LanguageResource.Language.LblVipLevel;
            label6.Text = LanguageResource.Language.LblLastLoginTime;
            label7.Text = LanguageResource.Language.LblOftenGameLocation;
            label9.Text = LanguageResource.Language.LblContacterCall;
            label8.Text = LanguageResource.Language.LblTel;
            Account_btnCloseDown.Text = LanguageResource.Language.BtnCloseDownAccount;
            Role_btnCloseDown.Text = LanguageResource.Language.BtnCloseDownRole;
            Role_btnGag.Text = LanguageResource.Language.BtnGagTool;
            Account_btnPlayNo.Text = LanguageResource.Language.LblPlayNOTool;
            Account_BtnResetAntiIndulgence.Text = LanguageResource.Language.Account_BtnResetAntiIndulgence;
            btnDosure.Text = LanguageResource.Language.BtnSure;
            btnDoesc.Text = LanguageResource.Language.BtnCancel;
            ckboxCOther.Text = LanguageResource.Language.LblOther;
            label4.Text = LanguageResource.Language.LblOtherInfoWriteRemark;
            ckboxCUserName.Text = LanguageResource.Language.LblProvideFullName;
            ckboxCPersonID.Text = LanguageResource.Language.LblProviderFullDocumentNo;
            ckboxCPSWProtect.Text = LanguageResource.Language.LblSecurityVerify;
            groupBoxCheck.Text = LanguageResource.Language.GbVerifyItems;
            groupBoxKnow.Text = LanguageResource.Language.GbNeedKnowItems;
            ckboxOCanRestor.Text = LanguageResource.Language.LblRepairFailure;
            groupBox4.Text = LanguageResource.Language.LblWorkOrderRemark;
            ckboxDonow.Text = LanguageResource.Language.BtnSubmitWithExecute;
            toolStripStatusLabel1.Text = LanguageResource.Language.LblReady;
            this.groupBox4.Text = LanguageResource.Language.LblWorkOrderRemark;
        }
        #region 事件

        /// <summary>
        /// 按钮关闭窗体事件
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 提交工单
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ckboxDonow.Checked)
            {
                CommitTask();
            }
            else
            {
                MsgBox.Show(LanguageResource.Language.Tip_CheckSureSubmitWithExecute + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 复制工单基本信息
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            string text = lblURinfo.Tag as string;
            Clipboard.SetDataObject(text, false, 3, 500);
           // Clipboard.SetText(text);//error:Requested Clipboard operation did not succeed.
            MsgBox.Show(LanguageResource.Language.Tip_CopyWorkOrderToClipBoard + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        ///选择是否立即执行并提交工单
        /// </summary>
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (ckboxDonow.Checked)
            {
                pnlDonow.Enabled = true;
            }
            else
            {
                pnlDonow.Enabled = false;
            }
        }
        #endregion
        #region 私有方法

        /// <summary>
        /// 设置游戏用户角色信息
        /// </summary>
        private void SetGameUR()
        {
            if (SystemConfig.OpenGridCellNamePrint)
            {
                _drguser.Cells.GridViewCellNameLog();
                if(_drrole!=null)
                     _drrole.Cells.GridViewCellNameLog();
            }
            lblTaskType.Text = LanguageResource.Language.LblWorkOrderType + ":" + (!string.IsNullOrEmpty(TaskTypeDesc) ? TaskTypeDesc : ClientCache.GetDicPCName(_tasktype.ToString()));

            string userinfo = "";
            string uid = TrimNull(_drguser.Cells[0].Value); //该页面记录变量【用户ID，角色ID】
            int temp = 0;
            if (int.TryParse(uid, out temp)) 
            {
                UserID = temp;
            }
            

            userinfo += LanguageResource.Language.LblAccountNo + ":" + uid + " \t";
            userinfo += LanguageResource.Language.LblAccountName+":" + TrimNull(_drguser.Cells[1].Value) + "\n";
            userinfo += LanguageResource.Language.LblBelongBigZone + ":" + _drguser.Cells[2].Value + "\t";
            userinfo += LanguageResource.Language.LblOnlineStatue + ":" + TrimNull(_drguser.Cells[3].Value) + " \n";
            userinfo += LanguageResource.Language.LblCloseDownStatue+":" + TrimNull(_drguser.Cells[4].Value) + "\t";
            userinfo += LanguageResource.Language.LblIsSecurity + ":" + TrimNull(_drguser.Cells[5].Value) + " \n";
            userinfo += LanguageResource.Language.BtnAntiIndulgenceInfo + ":" + TrimNull(_drguser.Cells[6].Value) + "\t";
            userinfo += LanguageResource.Language.LblDocumentNumber + " :" + TrimNull(_drguser.Cells[7].Value) + "\n";
            userinfo += LanguageResource.Language.LblRegisterTime + ":" + TrimNull(_drguser.Cells[8].Value) + "\t";
            userinfo += LanguageResource.Language.LblLastOnlineTime + ":" + TrimNull(_drguser.Cells[9].Value) + " \n";
            userinfo +=LanguageResource.Language.LblLastOnlineIP+ "IP:" + TrimNull(_drguser.Cells[10].Value) + "\n";
            string bigZone = TrimNull(_drguser.Cells["F_BigZoneID"].Value);
            //F_GUserName
            UserGaneName = (string.Format("{0}", _drguser.Cells["F_UserName"].Value));
            if (int.TryParse(bigZone, out temp))
            {
                BigZoneID = temp;
            }
            if (_drrole != null)
            {
                string rid = TrimNull(_drrole.Cells[0].Value);
                int roleid=0;
                if (int.TryParse(rid, out roleid)) 
                {
                    RoleID = roleid; //该工单如果是解封角色则需要提供角色ID值【全局变量】
                }
                userinfo += LanguageResource.Language.LblRoleNo + ":" + rid + "\t";
                userinfo += LanguageResource.Language.LblRoleName+":" + TrimNull(_drrole.Cells[1].Value) + "\n";
                userinfo += LanguageResource.Language.LblBelongZone + ":" + TrimNull(_drrole.Cells[2].Value) + "\t";
                userinfo += LanguageResource.Language.LblOnlineStatue+":" + TrimNull(_drrole.Cells[3].Value) + "\n";
                userinfo += LanguageResource.Language.LblCloseDownStatue + ":" + TrimNull(_drrole.Cells[4].Value) + "\t";
                userinfo += LanguageResource.Language.LblLevel + ":" + TrimNull(_drrole.Cells[5].Value) + "\n";
                userinfo += LanguageResource.Language.LblCreateTime + ":" + TrimNull(_drrole.Cells[6].Value) + "\t";
                userinfo += LanguageResource.Language.LblLastOnlineTime+":" + TrimNull(_drrole.Cells[7].Value) + "\n";
                string zone = TrimNull(_drrole.Cells["F_ZoneID"].Value);
                if(int.TryParse(zone,out temp))
                {
                    ZoneID = temp;
                }
                // F_GRoleName
                RoleGameName = (string.Format("{0}", _drrole.Cells["F_RoleName"].Value));
            }
            lblURinfo.Tag = userinfo;
            rtbBaseInfo.Text = userinfo;
            string.Format("select inform :userId=[{0}],userGameName=[{1}], roleId=[{2}],roleGameName=[{3}]", UserID, UserGaneName, RoleID, RoleGameName);
        }

        /// <summary>
        /// 空对象转成空字符串
        /// </summary>
        private string TrimNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            return value.ToString();
        }

        /// <summary>
        /// 设置控件初始化
        /// </summary>
        private void SetControls()
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_Dictionary"].Clone();
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select("F_ParentID=100104");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cboxLimitTime.DataSource = dtdic;
                cboxLimitTime.DisplayMember = "F_Value";
                cboxLimitTime.ValueMember = "F_DicID";
                cboxLimitTime.SelectedIndex = 3;

                DataTable dtdic1 = ds.Tables["T_Dictionary"].Clone();
                DataRow[] drdic1 = ds.Tables["T_Dictionary"].Select("F_ParentID=100105");
                foreach (DataRow dr1 in drdic1)
                {
                    dtdic1.ImportRow(dr1);
                }
                cboxVIP.DataSource = dtdic1;
                cboxVIP.DisplayMember = "F_Value";
                cboxVIP.ValueMember = "F_DicID";
                cboxVIP.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// 窗口之间消息
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 601:
                    string.Format("class:{0}, Msg:{1},m.LParam:{2},HWnd:{3},WParam:{4}", typeof(FormTaskAdd).Name, m.Msg, m.LParam.ToString(), m.HWnd.ToString(),m.WParam.ToString()).Logger();
                    this.Activate();
                    string shareMsgPosition = m.LParam.ToString();
                    int index = Convert.ToInt32(shareMsgPosition) - 1;
                    if (index>=0)
                    {//存在异常信息 
                        //逻辑优化前
                        object obj = ShareData.Msg[index];
                        GSSModel.Request.ClientData data = obj as GSSModel.Request.ClientData;
                        string.Format("class:[{0}] new flow call return result= [{1}],message=[{2}]", typeof(FormTaskAdd).Name, data.Success, data.Message).Logger();
                        //目前这里只有解封账号和角色
                        if (data.Success)
                        {
                            MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            return;
                        }
                        else
                        {
                            string msg = data.Message;
                            msg.Logger();
                            if (msg == "2016:")
                            {//调用gamecoredb或者usercoredb只有这一项返回数据时的状态码 
                                msg = LanguageItems.BaseLanguageItem.Tip_OnlyStaueIsLockCanDo;
                            }
                            MsgBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (m.WParam.ToString() == "0")
                    {
                        _taskid = "0";
                        if (!ckboxDonow.Checked)
                        {
                            MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailure, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailureWithTool, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ComitDoControl(true);
                    }
                    else
                    {
                        _taskid = m.WParam.ToString();
                        if (!ckboxDonow.Checked)
                        {
                            MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            this.Close();
                        }
                        else
                        {
                            string Uname = TrimNull(_drguser.Cells[1].Value);
                            string Rname = _drrole == null ? "" : TrimNull(_drrole.Cells[1].Value);
                            if (Uname.Trim().Length == 0)
                            {
                                MsgBox.Show(LanguageResource.Language.Tip_WorkOrderLoseAccountWithInvalid+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            FormToolGuserLock form = new FormToolGuserLock(_clienthandle, 1, _taskid, Uname, Rname);
                            DialogResult dr = form.ShowDialog();
                            Application.DoEvents();
                            if (dr == DialogResult.OK)
                            {
                                MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSuccWithExecute+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                ComitDoControl(true);
                            }
                        }

                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        /// <summary>
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        /// <param name="isback"></param>
        private void ComitDoControl(bool isback)
        {
            if (isback)
            {
                if (ckboxDonow.Checked)
                {
                    pnlDonow.Enabled = true;
                }
                else
                {
                    pnlDonow.Enabled = false;
                }
                btnDosure.Enabled = true;
                btnDoesc.Enabled = true;
                ckboxDonow.Enabled = true;
            }
            else
            {
                pnlDonow.Enabled = false;
                btnDosure.Enabled = false;
                btnDoesc.Enabled = false;
                ckboxDonow.Enabled = false;
            }

        }

        /// <summary>
        /// 提交工单
        /// </summary>
        private void CommitTask()
        {
            Tasks model = getTaskModel();
            string strErr = "";
            if (ckboxCOther.Checked && model.F_COther.Length == 0)
            {
                strErr += LanguageResource.Language.GbVerifyItems + ">" + LanguageResource.Language.Tip_OtherInfoNoEmpty + "！\n";
            }
            if (model.F_Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty + "！\n";
            }

            if (model.F_URInfo.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_CreateWorkNeedUserAndRoleInfo + "！\\n";
            }
            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ComitDoControl(false);
            if (_tasktype.HasValue && SystemConfig.WillCallServicesWorkOrder.ContainsKey(_tasktype.Value))
            { //1 create work order 2 action
                msgCommand cmd = (msgCommand)Enum.Parse(typeof(msgCommand), SystemConfig.WillCallServicesWorkOrder[_tasktype.Value]);
                GSSModel.Request.RoleData role = new GSSModel.Request.RoleData()
                {
                    UserID = UserID.Value,
                    RoleID = RoleID.Value,
                    BigZoneId = BigZoneID,
                    ZoneId = ZoneID
                };
                _clienthandle.BindCommandWithSend(cmd, this.Handle.ToInt32(), model, role);//setup:create task 2:recovery role
            }
            else
            {
                model.F_GameBigZone = BigZoneID.ToString();
                model.F_GameZone = ZoneID.ToString();
                _clienthandle.AddTask(model);
            }

        }
        Tasks getTaskModel() 
        {
            string Title = "";
            string Note = rboxNote.Text;
            int From = SystemConfig.AppID;//客服中心
            int VipLevel = int.Parse(cboxVIP.SelectedValue.ToString());
            DateTime? LimitTime = GetLimitTime();
            int LimitType = int.Parse(cboxLimitTime.SelectedValue.ToString());
            int? Type = _tasktype;//角色异常
            int State = SystemConfig.DefaultWorkOrderStatue;//等待处理
            int GameName = SystemConfig.GameID;//寻龙记

            string GUserID = TrimNull(_drguser.Cells[0].Value);
            string GUserName = TrimNull(_drguser.Cells[1].Value);
            string GBigZoneName = TrimNull(_drguser.Cells[2].Value);
            string GameZone = "";
            string GRoleID = "";
            string GRoleName = "";
            if (_drrole != null)
            {
                GameZone = TrimNull(_drrole.Cells[2].Value);
                GRoleID = TrimNull(_drrole.Cells[0].Value);
                GRoleName = TrimNull(_drrole.Cells[1].Value);
            }

            string Telphone = tboxTelphone.Text;
            int? DutyMan = null;
            int? PreDutyMan = null;
            int CreatMan = int.Parse(ShareData.UserID);
            DateTime CreatTime = DateTime.Now;
            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = lblURinfo.Tag as string;
            string gpeoplename = tboxGPeopleName.Text;
            bool CUserName = ckboxCUserName.Checked;
            bool CPSWProtect = ckboxCPSWProtect.Checked;
            bool CPersonID = ckboxCPersonID.Checked;
            string COther = tboxCOther.Text;
            string OLastLoginTime = tboxOLastLoginTime.Text;
            bool OCanRestor = ckboxOCanRestor.Checked;
            string OAlwaysPlace = tboxOAlwaysPlace.Text;
            int Rowtype = 0;

           

            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_Title = Title;
            model.F_Note = Note;
            model.F_From = From;
            model.F_VipLevel = VipLevel;
            model.F_LimitType = LimitType;
            model.F_LimitTime = LimitTime;
            model.F_Type = Type;
            model.F_State = State;
            model.F_GameName = GameName;
            model.F_GameBigZone = GBigZoneName;
            model.F_GameZone = GameZone;
            model.F_GUserID = GUserID;
            model.F_GUserName = GUserName;
            model.F_GRoleID = GRoleID;
            model.F_GRoleName = GRoleName;
            model.F_Telphone = Telphone;
            model.F_GPeopleName = gpeoplename;
            model.F_DutyMan = DutyMan;
            model.F_PreDutyMan = PreDutyMan;
            model.F_CreatMan = CreatMan;
            model.F_CreatTime = CreatTime;
            model.F_EditMan = EditMan;
            model.F_EditTime = EditTime;
            model.F_URInfo = URInfo;
            model.F_Rowtype = Rowtype;
            model.F_CUserName = CUserName;
            model.F_CPSWProtect = CPSWProtect;
            model.F_CPersonID = CPersonID;
            model.F_COther = COther;
            model.F_OLastLoginTime = OLastLoginTime;
            model.F_OCanRestor = OCanRestor;
            model.F_OAlwaysPlace = OAlwaysPlace;
            return model;
        }
        /// <summary>
        /// 提交工单(同步)
        /// </summary>
        private bool CommitTaskSyn()
        {
            string Title = "";
            string Note = rboxNote.Text;
            int From = SystemConfig.AppID;//客服中心
            int VipLevel = int.Parse(cboxVIP.SelectedValue.ToString());
            DateTime? LimitTime = GetLimitTime();
            int LimitType = int.Parse(cboxLimitTime.SelectedValue.ToString());
            int? Type = _tasktype;//角色异常
            int State = SystemConfig.DefaultWorkOrderStatue;//等待处理
            int GameName = SystemConfig.GameID;//寻龙记
            
            string GUserID = TrimNull(_drguser.Cells[0].Value);
            string GUserName = TrimNull(_drguser.Cells[1].Value);
            string GBigZoneName = TrimNull(_drguser.Cells[2].Value);
            string GameZone = "";
            string GRoleID = "";
            string GRoleName = "";
            if (_drrole!=null)
            {
                 GameZone = TrimNull(_drrole.Cells[2].Value);
                 GRoleID = TrimNull(_drrole.Cells[0].Value);
                 GRoleName = TrimNull(_drrole.Cells[1].Value);
            }

            string Telphone = tboxTelphone.Text;
            string gpeoplename = tboxGPeopleName.Text;
            int? DutyMan = null;
            int? PreDutyMan = null;
            int CreatMan = int.Parse(ShareData.UserID);
            DateTime CreatTime = DateTime.Now;
            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = lblURinfo.Tag as string ;
            bool CUserName = ckboxCUserName.Checked;
            bool CPSWProtect = ckboxCPSWProtect.Checked;
            bool CPersonID = ckboxCPersonID.Checked;
            string COther = tboxCOther.Text;
            string OLastLoginTime = tboxOLastLoginTime.Text;
            bool OCanRestor = ckboxOCanRestor.Checked;
            string OAlwaysPlace = tboxOAlwaysPlace.Text;
            int Rowtype = 0;

            string strErr = "";
            if (ckboxCOther.Checked && COther.Length == 0)
            {
                strErr += LanguageResource.Language.GbVerifyItems + ">" + LanguageResource.Language.Tip_OtherInfoNoEmpty + "！\n";
            }
            //if (Telphone.Trim().Length == 0)
            //{
            //    strErr += "联系电话不能为空！\n";
            //}
            //else if (!WinUtil.IsTelphone(Telphone) && !WinUtil.IsMobile(Telphone))
            //{
            //    strErr += LanguageResource.Language.LblTelFormIsError+"！\n(格式:010-88886666或13912341234)\n";
            //}
            if (Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty+"！\n";
            }

            if (URInfo.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_CreateWorkNeedUserAndRoleInfo + "！\\n";
            }


            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            ComitDoControl(false);

            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_Title = Title;
            model.F_Note = Note;
            model.F_From = From;
            model.F_VipLevel = VipLevel;
            model.F_LimitType = LimitType;
            model.F_LimitTime = LimitTime;
            model.F_Type = Type;
            model.F_State = State;
            model.F_GameName = GameName;
            model.F_GameBigZone = GBigZoneName;
            model.F_GameZone = GameZone;
            model.F_GUserID = GUserID;
            model.F_GUserName = GUserName;
            model.F_GRoleID = GRoleID;
            model.F_GRoleName = GRoleName;
            model.F_Telphone = Telphone;
            model.F_GPeopleName = gpeoplename;
            model.F_DutyMan = DutyMan;
            model.F_PreDutyMan = PreDutyMan;
            model.F_CreatMan = CreatMan;
            model.F_CreatTime = CreatTime;
            model.F_EditMan = EditMan;
            model.F_EditTime = EditTime;
            model.F_URInfo = URInfo;
            model.F_Rowtype = Rowtype;
            model.F_CUserName = CUserName;
            model.F_CPSWProtect = CPSWProtect;
            model.F_CPersonID = CPersonID;
            model.F_COther = COther;
            model.F_OLastLoginTime = OLastLoginTime;
            model.F_OCanRestor = OCanRestor;
            model.F_OAlwaysPlace = OAlwaysPlace;

            string backStr = _clienthandle.AddTaskSyn(model);
            ComitDoControl(true);
            if (backStr == "0")
            {
                if (!ckboxDonow.Checked)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailure, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailureWithTool, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            else
            {
                if (!ckboxDonow.Checked)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _taskid = backStr;
                return true;
            }



        }

        /// <summary>
        /// 得到限制日期
        /// </summary>
        /// <returns></returns>
        private DateTime? GetLimitTime()
        {
            string limit = cboxLimitTime.SelectedValue.ToString();
            DateTime nowlimit = DateTime.Now;
            switch (limit)
            {
                case "10010400"://30分钟
                    nowlimit = nowlimit.AddMinutes(30);
                    break;
                case "10010401":
                    nowlimit = nowlimit.AddHours(2);
                    break;
                case "10010402":
                    nowlimit = nowlimit.AddHours(4);
                    break;
                case "10010403":
                    nowlimit = nowlimit.AddHours(8);
                    break;
                case "10010404":
                    nowlimit = nowlimit.AddHours(12);
                    break;
                case "10010405":
                    nowlimit = nowlimit.AddHours(16);
                    break;
                case "10010406":
                    nowlimit = nowlimit.AddHours(24);
                    break;
                case "10010407":
                    nowlimit = nowlimit.AddDays(7);
                    break;
                case "10010408":
                    return null;
                default:
                    return null;
            }
            return nowlimit;
        }

        #endregion

        #region 工具类事件
        private void button3_Click(object sender, EventArgs e)
        {
            // CommitTask();
            if (CommitTaskSyn())
            {
                string Uname = TrimNull(_drguser.Cells[1].Value);
                string Rname = _drrole==null?"":TrimNull(_drrole.Cells[1].Value);
                if (Uname.Trim().Length == 0)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderLoseAccountWithInvalid + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FormToolGuserLock form = new FormToolGuserLock(_clienthandle, 1, _taskid, Uname, Rname);
                form.ShowDialog();
                Application.DoEvents();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CommitTaskSyn())
            {
                string Uname = TrimNull(_drguser.Cells[1].Value);
                string Rname = _drrole == null ? "" : TrimNull(_drrole.Cells[1].Value);
                if (Rname.Trim().Length == 0 || Uname.Trim().Length == 0)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderLoseAccountOrRoleWithInvalid + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FormToolGuserLock form = new FormToolGuserLock(_clienthandle, 2, _taskid, Uname, Rname);
                form.ShowDialog();
                Application.DoEvents();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (CommitTaskSyn())
            {
                string Uname = TrimNull(_drguser.Cells[1].Value);
                if (Uname.Trim().Length == 0)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderLoseAccountWithInvalid+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FormToolGResetChildInfo form = new FormToolGResetChildInfo(_clienthandle, _taskid, Uname);
                form.ShowDialog();
                Application.DoEvents();
            }

        }
        #endregion

        private void Button_Click(object obj, EventArgs e)
        {//此处需要判断解封的是角色还是账户
            if (string.IsNullOrEmpty(rboxNote.Text))
            {
                MsgBox.Show(LanguageItems.BaseLanguageItem.Tip_RemarkNoEmpty, LanguageItems.BaseLanguageItem.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string.Format("class:{0},function:create work order,param:[userid={1},roleid={2}]",typeof(FormTaskAdd).Name,UserID,RoleID).Logger();
            if (!UserID.HasValue)
            {
                MsgBox.Show(LanguageItems.BaseLanguageItem.Tip_GameUseNotIsRequire,LanguageItems.BaseLanguageItem.Tip_Tip,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            int type = RoleID.HasValue ? 2 : 1;
            "input remark information ing".Logger();
             FormToolGuserUnLock form = new FormToolGuserUnLock(new CallBack(TaskDetailCall),UserGaneName, RoleGameName );
             form.ShowDialog();
        }
        private void TaskDetailCall(object data)
        {
            "Call back is into the parent form".Logger();
            CallBackEventParam p = data as CallBackEventParam;
            p.NowForm.Close();
            GSSModel.Request.Unlock ul = p.CallData as GSSModel.Request.Unlock;
            if (string.IsNullOrEmpty(ul.Remark))
            {
                "no input remark,refuse unlock".Logger();
                MsgBox.Show(LanguageItems.BaseLanguageItem.Tip_RemarkNoEmpty,LanguageItems.BaseLanguageItem.Tip_Tip,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            ul.UserId = UserID.Value;
            if (RoleID.HasValue)
            {
                ul.RoleId = RoleID.Value;
            }
            Tasks task = getTaskModel();
            GSSModel.Request.ClientData client = new GSSModel.Request.ClientData();
            client.FormID = this.Handle.ToInt32();
            TaskContainerLogicData tl = new TaskContainerLogicData()
            {
                WorkOrder = task,
                LogicData = ul
            };
            client.Data = tl;
            "do submit to service".Logger();
            _clienthandle.SendTaskContainerLogicData(client);
        }
    }
}
