using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI;
using GSSModel;
using GSSModel.Request;
using GSS.DBUtility;
namespace GSSClient
{
    public partial class FormToolGuserUnLock : GSSUI.AForm.FormMain
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
        bool isWorkOrderCreateAfter = true;//页面调用是在工单创建完毕之后
        CallBack dataCallBack;
        public FormToolGuserUnLock(ClientHandles clihandle, int LockUorR, string taskid, string Uname, String Rname)
        {
            InitializeComponent();
            _clihandle = clihandle;
            _LockUorR = LockUorR;
            _taskid = taskid;
            _Uname = Uname;
            _Rname = Rname;
            isWorkOrderCreateAfter = true;//审核流程调用
        }
        /// <summary>
        /// 该窗体调用是直接运行，此窗体此时只负责设定相关数据，不会将数据发送到服务端
        /// </summary>
        public FormToolGuserUnLock(CallBack parentCallEvent, string Uname, String Rname) 
        {
            InitializeComponent();
            isWorkOrderCreateAfter = false;
            _Uname = Uname;
            _Rname = Rname;
            dataCallBack = parentCallEvent;
        }
        private void FormToolGuserUnLock_Load(object sender, EventArgs e)
        {
            string URStr = "";
            if (_Uname.Trim().Length > 0)
            {
                URStr += "帐号:" + _Uname;
            }
            if (_Rname.Trim().Length > 0 && _LockUorR == 2)
            {
                URStr += " 的角色:" + _Rname;
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
            if (!isWorkOrderCreateAfter)
            {//该页面只是作为参数窗体的页面 
                Unlock ul = new Unlock()
                {
                    Remark = rtboxNote.Text,
                    UnLockTarget=_LockUorR,
                    UserName=_Uname,
                    RoleName=_Rname
                };
                CallBackEventParam p = new CallBackEventParam() 
                {
                    CallData=ul,
                    NowForm=this
                };
                dataCallBack(p);
                string.Format("{0} Call back is end",typeof(FormToolGuserUnLock).Name).Logger();
                
                return;
            }
            _clihandle.GameNoLockUR(this.Handle.ToInt32(), _LockUorR, _taskid);

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
                    task.F_TUseData = "解封工具-" + (_LockUorR == 1 ? "帐号解封" : "角色解封") + "\n" + lblUR.Text + "\n";
                    task.F_Note = rtboxNote.Text;
                    _isToolUsed = true;
                    if (msg == "true")
                    {
                        task.F_TUseData += " 解封成功!";
                        _clihandle.EditTaskNoReturn(task);
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_UnlockSuccFormat, lblUR.Text), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.Close();

                    }
                    else
                    {
                        task.F_TUseData += " 解封失败!" + msg;
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_UnlockFailureFormat, lblUR.Text) + msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void FormToolGuserUnLock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isToolUsed)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
