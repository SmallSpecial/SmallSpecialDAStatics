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
    public partial class FormToolGUserNoUse : GSSUI.AForm.FormMain
    {
        private ClientHandles _clihandle;

        private string _taskid;
        private string _Uname;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        public FormToolGUserNoUse( ClientHandles clihandle,string taskid, string Uname)
        {
            InitializeComponent();
            _clihandle = clihandle;
            _taskid = taskid;
            _Uname = Uname;

        }

        private void FormToolGuserUnLock_Load(object sender, EventArgs e)
        {
            string URStr = "";
            if (_Uname.Trim().Length > 0)
            {
                URStr += LanguageResource.Language.LblAccount + _Uname;
            }
            lblUR.Text = URStr;
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

        private void btnDosure_Click(object sender, EventArgs e)
        {
            if (rtboxNote.Text.Trim().Length == 0)
            {
                MsgBox.Show("工具使用备注不能为空!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ComitDoControl(false);

            _clihandle.GameUserNoUse(this.Handle.ToInt32(), _taskid);

        }

        private void btnDoesc_Click(object sender, EventArgs e)
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
                    task.F_TUseData = "帐号归还工具-帐号归还" + "\n" + lblUR.Text + "\n";
                    task.F_Note = rtboxNote.Text;
                    _isToolUsed = true;
                    if (msg == "true")
                    {
                        task.F_TUseData += " 帐号归还成功!";
                        _clihandle.EditTaskNoReturn(task);
                        MsgBox.Show(LanguageResource.Language.LblAccountRetureSucc, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.Close();

                    }
                    else
                    {
                        task.F_TUseData += " 帐号归还失败!" + msg;
                        MsgBox.Show(LanguageResource.Language.LblAccountRetureFailure+"\r\n" + msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void FormToolGUserNoUse_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isToolUsed)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
