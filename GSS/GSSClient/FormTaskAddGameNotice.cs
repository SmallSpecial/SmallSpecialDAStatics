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

namespace GSSClient
{
    public partial class FormTaskAddGameNotice : GSSUI.AForm.FormMain
    {
        #region 私有变量
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

        public FormTaskAddGameNotice(ClientHandles clienthandle, int tasktype)
        {

            InitializeComponent();
            _clienthandle = clienthandle;
            _tasktype = tasktype;
            //窗体位置居中
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //工单信息初始化
            SetGameUR();
            //控件初始化
            SetControls();
        }
        void InitLanguageText() 
        {
            this.label3.Text = LanguageResource.Language.LblVipLevel + "";

        }
        #region 事件
        /// <summary>
        /// 关闭事件
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
            CommitTask();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置游戏用户角色信息
        /// </summary>
        private void SetGameUR()
        {
            lblTaskType.Text = LanguageResource.Language.LblWorkOrderType+":" + ClientCache.GetDicPCName(_tasktype.ToString());
            string userinfo = "";

            lblURinfo.Text = userinfo;

        }

        /// <summary>
        /// 空对象转成空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
                    this.Activate();
                    if (m.WParam.ToString() == "0")
                    {
                        _taskid = "0";

                        MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailure, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ComitDoControl(true);
                    }
                    else
                    {
                        _taskid = m.WParam.ToString();

                        MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

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
        /// 提交工单
        /// </summary>
        private void CommitTask()
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
            int? DutyMan = null;
            int? PreDutyMan = null;
            int CreatMan = int.Parse(ShareData.UserID);
            DateTime CreatTime = DateTime.Now;
            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = lblURinfo.Text;

            int Rowtype = 0;

            string strErr = "";


            if (Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty+"！\n";
            }


            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
            model.F_DutyMan = DutyMan;
            model.F_PreDutyMan = PreDutyMan;
            model.F_CreatMan = CreatMan;
            model.F_CreatTime = CreatTime;
            model.F_EditMan = EditMan;
            model.F_EditTime = EditTime;
            model.F_URInfo = URInfo;
            model.F_Rowtype = Rowtype;


            _clienthandle.AddTask(model);


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
                case "100104100"://30分钟
                    nowlimit = nowlimit.AddMinutes(30);
                    break;
                case "100104101":
                    nowlimit = nowlimit.AddHours(2);
                    break;
                case "100104102":
                    nowlimit = nowlimit.AddHours(4);
                    break;
                case "100104103":
                    nowlimit = nowlimit.AddHours(8);
                    break;
                case "100104104":
                    nowlimit = nowlimit.AddHours(12);
                    break;
                case "100104105":
                    nowlimit = nowlimit.AddHours(16);
                    break;
                case "100104106":
                    nowlimit = nowlimit.AddHours(24);
                    break;
                case "100104107":
                    nowlimit = nowlimit.AddDays(7);
                    break;
                case "100104108":
                    return null;
                default:
                    return null;
            }
            return nowlimit;
        }

        #endregion

        private void checkBoxIsTiming_CheckedChanged(object sender, EventArgs e)
        {
            panelTiming.Enabled = checkBoxIsTiming.Checked;
        }

        private void checkBoxIsReAgain_CheckedChanged(object sender, EventArgs e)
        {
            panelInterval.Enabled = checkBoxIsReAgain.Checked;
        }


    }
}
