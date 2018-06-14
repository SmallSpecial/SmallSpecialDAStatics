using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI;

namespace GSSClient
{
    public partial class FormToolGuserLock : GSSUI.AForm.FormMain
    {
        private ClientHandles _clihandle;
        /// <summary>
        /// 封停用户还是角色 用户:1,角色:2
        /// </summary>
        private int _LockUorR;

        private string _taskid;
        private string _Uname;
        private string _Rname;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        void InitLanguageText() 
        {
            btnDosure.Text = LanguageResource.Language.BtnSure;
            btnDoesc.Text = LanguageResource.Language.BtnCancel;
            label2.Text = LanguageResource.Language.LblRemark;
            label1.Text = LanguageResource.Language.Tip_CloseDownTimeSpan;
            lblUR.Text = LanguageResource.Language.Tip_CloseDownAccountWithRole;
            this.Text = LanguageResource.Language.Tip_CloseDownTool;
        }
        public FormToolGuserLock(ClientHandles clihandle, int LockUorR, string taskid, string Uname, String Rname)
        {
            InitializeComponent();
            InitLanguageText();
            _clihandle = clihandle;
            _LockUorR = LockUorR;
            _taskid = taskid;
            _Uname = Uname;
            _Rname = Rname;
        }
        private void FormToolGuserLock_Load(object sender, EventArgs e)
        {
            string URStr = "";
            if (_Uname.Trim().Length > 0)
            {
                URStr += LanguageResource.Language.LblAccount + ":" + _Uname;
            }
            if (_Rname.Trim().Length > 0 && _LockUorR == 2)
            {
                URStr +="  "+ LanguageResource.Language.LblRole + " :" + _Rname;
            }
            lblUR.Text = URStr;
            BindLockTime();
        }

        /// <summary>
        /// 绑定时间
        /// </summary>
        private void BindLockTime()
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_GameConfig"].Clone();
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=101001");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cboxTime.DataSource = dtdic;
                cboxTime.DisplayMember = "F_Name";
                cboxTime.ValueMember = "F_ID";
                cboxTime.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rtboxNote.Text.Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_UserToolInputRemark + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComitDoControl(false);

            _clihandle.GameLockUR(this.Handle.ToInt32(), _LockUorR, _taskid, cboxTime.SelectedValue.ToString());


        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (_taskid == null || _taskid.Trim().Length == 0)
                    {
                        ComitDoControl(true);
                        return;
                    }
                    string msg = ShareData.Msg[m.WParam.ToInt32()].ToString();//分为两段,FORM编号+返回结果(字符串:true或错误结果)

                    GSSModel.Tasks task = new GSSModel.Tasks();
                    task.F_ID = int.Parse(_taskid);
                    task.F_EditMan = int.Parse(ShareData.UserID);
                    task.F_EditTime = DateTime.Now;
                    task.F_TToolUsed = true;
                    task.F_TUseData = LanguageResource.Language.Tip_CloseDownTool + "-" + (_LockUorR == 1 ? LanguageResource.Language.BtnCloseDownAccount : LanguageResource.Language.BtnCloseDownRole) + "\n" + lblUR.Text + " 封停时间:" + cboxTime.Text + "\n";
                    task.F_Note = rtboxNote.Text;
                    _isToolUsed = true;
                    if (msg == "true")
                    {
                        task.F_TUseData += LanguageResource.Language.Tip_CloseDownSucc + " !";
                        _clihandle.EditTaskNoReturn(task);
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_CloseDownSuccFormat, lblUR.Text+"!"), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.Close();

                    }
                    else if (msg == "2015")
                    { //改用户之前已被封停
                        task.F_TUseData += LanguageResource.Language.Tip_CloseDownFailure + " !";
                        _clihandle.EditTaskNoReturn(task);
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_CloseDownFailureFormat, lblUR.Text+"!")  + LanguageResource.Language.Tip_AccountHasBeenCloseDown, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _clihandle.EditTaskNoReturn(task);
                        ComitDoControl(true);
                    }
                    else
                    {
                        task.F_TUseData += LanguageResource.Language.Tip_CloseDownFailure + " !" + msg;
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_CloseDownFailureFormat, lblUR.Text+"!") + msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _clihandle.EditTaskNoReturn(task);
                        ComitDoControl(true);
                    }

                    
                    base.DefWndProc(ref m);
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

        private void FormToolGuserLock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isToolUsed)
            {
                this.DialogResult = DialogResult.OK;
            }
            
        }
    }
}
