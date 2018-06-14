using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GSSUI;

namespace GSSClient
{
    public partial class FormChat : GSSUI.AForm.FormMain
    {
        /// <summary>
        /// 客户端处理实例
        /// </summary>
        private ClientHandles _clienthandle;
        public Thread ThStartServer = null;
        void InitLanguageText()
        {//由于在页面上拖动控件之后将导致设计文件（design）重新变动，如果在design中设置文本变动后无效
            this.btnRlstClear.Text = LanguageResource.Language.BtnRefreshConsumeList;// "刷新咨询列表";
            this.label6.Text = LanguageResource.Language.LblConsumeList;// "咨询列表";
            this.label1.Text = LanguageResource.Language.LblRole;// "角色";
            this.btnSearch.Text = LanguageResource.Language.BtnQuery;// "查 找";
            this.lblLoading.Text = LanguageResource.Language.LblLoding;// "数据加载中...";
            this.label5.Text = LanguageResource.Language.LblConsumeText;// "咨询内容";
            this.lblLoadingMSGList.Text = LanguageResource.Language.LblLoding;// "数据加载中...";
            this.buttonOK.Text = LanguageResource.Language.BtnSend;// "发 送";
            this.lboxExample.Items.AddRange(new object[] {
                LanguageResource.Language.Tip_SystemChatReplyPhrase.Split('|')
            //"您好",
            //"好的",
            //"还有什么需要帮助的吗？",
            //"谢谢您的宝贵意见",
            //"再见",
            //"我们会尽快处理",
            //"感谢您的支持！",
            //"非常抱歉",
            //"我们会尽快处理"
            });
            this.label4.Text = LanguageResource.Language.LblConsumeObject;// "咨询对象";
            this.lblLoadingRole.Text = LanguageResource.Language.LblLoding;// "数据加载中...";
            this.label2.Text = LanguageResource.Language.LblConsumeNo + ":";// "咨询编号:";
            this.label8.Text = LanguageResource.Language.BtnSendText;//"发送内容";
            this.aButton1.Text = LanguageResource.Language.BtnFinshChatWithSave;// "完成聊天并存档";
            this.aButton2.Text = LanguageResource.Language.BtnfinishChatWithGenerateWorkOrder;//  "完成聊天并生成工单";
            this.aButton3.Text = LanguageResource.Language.BtnHistoryRecord;//"历史记录";
            this.label3.Text ="Ctrl+Enter "+ LanguageResource.Language.BtnSend ;// "Ctrl+Enter 发送";
            this.label7.Text = LanguageResource.Language.BtnCommonSentences;// "常用语句";
            this.btnCloseForm.Text = LanguageResource.Language.BtnCloseForm;// "关闭窗口";
            this.ButtonClose.Text = LanguageResource.Language.BtnEndChat;// "结束对话";
            this.Text =LanguageResource.Language.BtnOnlineConsume ;//"在线咨询";
        }
        public FormChat(ClientHandles clienthandle)
        {
            InitializeComponent();
            InitLanguageText();
            //this.WindowState = FormWindowState.Maximized;
            // Control.CheckForIllegalCrossThreadCalls = false;
            _clienthandle = clienthandle;
            dgvRoleList.AutoGenerateColumns = false;
            contextMenuStrip1.Visible = false;
            Init();

            ThStartServer = new Thread(new ThreadStart(DoRunToRead));
            //   ThStartServer.IsBackground = true;
            ThStartServer.Start();
           
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        public void DoRunToRead()
        {
            while (true)
            {
                GetMsg(true);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 初始化窗口数据
        /// </summary>
        private void Init()
        {
            try
            {
                StreamReader sr = new StreamReader("CSSItems.txt", System.Text.Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine().Trim();
                    if (s.Length > 0)
                    {
                        lsvExample.Items.Add(new ListViewItem(s));
                    }

                }
                sr.Close();
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Warn(ex);
            }
        }

        /// <summary>
        /// 得到消息列表
        /// </summary>
        private void GetMsg(bool ispageload)
        {
            try
            {
                string sqlrlistWhere = " F_Type=20000216 and F_Rowtype=6 and F_TToolUsed is null  and F_DutyMan=" + GSSClient.ShareData.UserID + "";
                //  if (!lblLoading.Visible)
                //{
                //       sqlrlistWhere += " and F_TToolUsed is null";
                //  }
                // DataSet ds = _clienthandle.GetAllTasksSyn("", sqlrlistWhere, "F_ID", "ASC", 50, 1);
                GSSBLL.Tasks bll = ClientRemoting.Tasks();
                DataSet ds = bll.GetList(50, sqlrlistWhere, "F_ID ASC");
                if (ds != null && ds.Tables[0].Columns.Count > 15)
                {
                    bool isRoleAdd = true;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        isRoleAdd = true;
                        string imgTips = Application.StartupPath + "\\GSSData\\Images\\numerbgClear.png";
                        if (dr["F_TToolUsed"].ToString() == "")
                        {
                            imgTips = Application.StartupPath + "\\GSSData\\Images\\New.png";
                        }
                        Image img = GetImage(imgTips);

                        foreach (DataGridViewRow dgvr in dgvRoleList.Rows)
                        {
                            if (dgvr.Cells[0].Value.ToString() == dr["F_ID"].ToString())
                            {
                                isRoleAdd = false;
                                dgvr.Cells[2].Value = img;
                            }
                        }
                        if (isRoleAdd)
                        {
                            DGVInsert(dr, img);

                        }
                        Application.DoEvents();
                    }

                }
                lblLoading.Visible = false;
                //  dgvRoleList.DataSource = ds.Tables[0];

                if (dgvRoleList.SelectedRows.Count == 1)
                {
                    if (lblLoadingRole.Visible)
                    {


                        lblTaskID.Text = dgvRoleList.SelectedRows[0].Cells[0].Value.ToString();
                        //DataSet dsmsglist = _clienthandle.GetTaskLogSyn(" and F_ID=" + dgvRoleList.SelectedRows[0].Cells[0].Value + " ");
                        GSSBLL.TasksLog bllMsg = ClientRemoting.TasksLog();
                        DataSet dsmsglist = bllMsg.GetList(50, " F_ID=" + dgvRoleList.SelectedRows[0].Cells[0].Value + " and F_Note is not null ", "F_LogID ASC");
                        if (dsmsglist != null && dsmsglist.Tables[0].Rows.Count > 0)
                        {
                            lblLoadingRole.Visible = false;
                            lblRoleInfo.Text = dsmsglist.Tables[0].Rows[0]["F_URInfo"].ToString();

                            foreach (DataRow dr in dsmsglist.Tables[0].Rows)
                            {
                                string msg = "";
                                if (dr["F_EditMan"].ToString().Length != 0)
                                {
                                    msg = string.Format("【客服{2}说:{0}】\n{1}\n", dr["F_EditTime"], dr["F_Note"], dr["F_EditMan"]);
                                }
                                else
                                {
                                    string dd = dr["F_TToolUsed"].ToString();
                                    if (dr["F_TToolUsed"].ToString() == "")
                                    {
                                        GSSModel.Tasks model = new GSSModel.Tasks();
                                        model.F_ID = Convert.ToInt32(dr["F_ID"]);
                                        model.F_TToolUsed = true;
                                        //_clienthandle.EditTaskLogSyn(model);
                                        GSSBLL.TasksLog bllMsgT = ClientRemoting.TasksLog();
                                        bllMsgT.Edit(model);
                                    }
                                    msg = string.Format("【他说:{0}】\n{1}\n", dr["F_EditTime"], dr["F_Note"]);
                                }
                                lblLoadingMSGList.Visible = false;
                                MsgPrint(msg);
                            }

                        }
                    }
                    else
                    {
                        // DataSet dsmsglist = _clienthandle.GetTaskLogSyn(" and F_ID=" + dgvRoleList.SelectedRows[0].Cells[0].Value + " and F_EditMan is NULL and F_TToolUsed is null ");
                        GSSBLL.TasksLog bllMsg = ClientRemoting.TasksLog();
                        DataSet dsmsglist = bllMsg.GetList(50, " F_ID=" + dgvRoleList.SelectedRows[0].Cells[0].Value + " and F_EditMan is NULL and F_TToolUsed is null and F_Note is not null ", "F_LogID ASC");
                        if (dsmsglist != null)
                        {
                            foreach (DataRow dr in dsmsglist.Tables[0].Rows)
                            {
                                if (dr["F_TToolUsed"].ToString() == "")
                                {
                                    GSSModel.Tasks model = new GSSModel.Tasks();
                                    model.F_ID = Convert.ToInt32(dr["F_ID"]);
                                    model.F_TToolUsed = true;
                                    //_clienthandle.EditTaskLogSyn(model);
                                    GSSBLL.TasksLog bllMsgT = ClientRemoting.TasksLog();
                                    bllMsgT.Edit(model);
                                }
                                string msg = string.Format("【他说:{0}】\n{1}\n", dr["F_EditTime"], dr["F_Note"]);
                                MsgPrint(msg);
                            }
                        }
                    }
                    string imgTips = Application.StartupPath + "\\GSSData\\Images\\numerbgClear.png";
                    Image img = GetImage(imgTips);
                    dgvRoleList.SelectedRows[0].Cells[2].Value = img;
                }
                lblLoadingRole.Visible = false;
                lblLoadingMSGList.Visible = false;
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Warn(ex);
                //MsgPrint("Warn:"+ex.Message);
            }


        }




        private void buttonOK_Click(object sender, EventArgs e)
        {
            SendMSG();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private void SendMSG()
        {
            if (lblTaskID.Text.Trim().Length == 0 || tboxNote.Text.Trim().Length == 0)
            {
                return;
            }
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = Convert.ToInt32(lblTaskID.Text);
            model.F_Note = tboxNote.Text;
            model.F_EditMan = Convert.ToInt32(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;
            model.F_OCanRestor = null;
            model.F_Rowtype = 6;//聊天
            string msg = string.Format("【你说:{0}】\n{1}\n", model.F_EditTime, model.F_Note);
            tboxNote.Text = "";
            MsgPrint(msg);
            //string back = _clienthandle.EditTaskSyn(model);

            GSSBLL.Tasks bll = ClientRemoting.Tasks();
            int back = bll.Edit(model);

            if (back == 0)
            {
                MsgPrint("--发送失败--");
            }
            tboxNote.Focus();
        }
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            if (lblTaskID.Text.Trim().Length == 0)
            {
                return;
            }
            tboxNote.Text = "寻龙记感谢您的支持,谢谢!(暂不回复给玩家)";
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = Convert.ToInt32(lblTaskID.Text);
            model.F_Note = tboxNote.Text;
            model.F_EditMan = Convert.ToInt32(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;
            model.F_OCanRestor = true;
            model.F_Rowtype = 6;//聊天
            string msg = string.Format("【你说:{0}】\n{1}\n", model.F_EditTime, model.F_Note);
            tboxNote.Text = "";
            model.F_State = 100100100;
            model.F_DutyMan = -1;
            MsgPrint(msg);
            //string back = _clienthandle.EditTaskSyn(model);

            GSSBLL.Tasks bll = ClientRemoting.Tasks();
            int back = bll.Edit(model);

            if (back == 0)
            {
                MsgPrint("--发送失败--");
            }
            DGVDelete();
            tboxNote.Focus();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetMsg(true);
        }

        private void FormChat_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ThStartServer != null)
            {
                if (ThStartServer.IsAlive)
                {
                    ThStartServer.Abort();
                }
            }
            ShareData.FormChat = null;
        }

        private void dgvRoleList_SelectionChanged(object sender, EventArgs e)
        {
            tboxMSGList.Text = "";
            lblRoleInfo.Text = "";
            lblTaskID.Text = "";
            lblLoadingRole.Visible = true;
            lblLoadingMSGList.Visible = true;
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
        private delegate void delegate_MsgPrint(string msg);
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg"></param>
        private void MsgPrint(string msg)
        {
            if (dgvRoleList.InvokeRequired)
            {
                delegate_MsgPrint d = new delegate_MsgPrint(MsgPrint);
                object arg0 = msg;
                this.Invoke(d, arg0);
            }
            else
            {
                tboxMSGList.Text += msg.Replace("&nbsp;", " ") + "-----------------------------------------------\n";
                tboxMSGList.SelectionStart = tboxMSGList.TextLength;
                tboxMSGList.ScrollToCaret();
                Application.DoEvents();
            }
        }
        private delegate void delegate_DGVInsertF(int taskid, string rolename);
        /// <summary>
        /// 插入列表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="img"></param>
        public void DGVInsertF(int taskid, string rolename)
        {
            if (dgvRoleList.InvokeRequired)
            {
                delegate_DGVInsertF d = new delegate_DGVInsertF(DGVInsertF);
                object arg0 = taskid;
                object arg1 = rolename;
                this.Invoke(d, arg0, arg1);
            }
            else
            {
                string imgTips = Application.StartupPath + "\\GSSData\\Images\\numerbgClear.png";
                Image img = GetImage(imgTips);

                bool isRoleAdd = true;
                foreach (DataGridViewRow dgvr in dgvRoleList.Rows)
                {
                    if (Convert.ToInt32(dgvr.Cells[0].Value) == taskid)
                    {
                        isRoleAdd = false;
                        //dgvRoleList.Rows.Remove(dgvr);
                    }
                }
                if (isRoleAdd)
                {
                    try
                    {
                        GSSBLL.Tasks bll = ClientRemoting.Tasks();
                        GSSModel.Tasks model = new GSSModel.Tasks();
                        model.F_ID = taskid;
                        model.F_State = 100100101;
                        model.F_DutyMan = int.Parse(ShareData.UserID);
                        model.F_EditMan = int.Parse(ShareData.UserID);
                        model.F_EditTime = DateTime.Now;
                        model.F_TToolUsed = true;
                        model.F_OCanRestor = true;
                        model.F_Rowtype = 6;
                        model.F_Note = null;
                        bll.Edit(model);
                        Thread.Sleep(100);
                        dgvRoleList.Rows.Insert(0, taskid, rolename, img);
                        Application.DoEvents();
                        dgvRoleList.Rows[0].Selected = true;
                        Application.DoEvents();
                        dgvRoleList_SelectionChanged(null, null);
                        Application.DoEvents();
                    }
                    catch (System.Exception ex)
                    {
                        MsgBox.Show(LanguageResource.Language.Tip_ReceiverOnlineConsumeError, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }

        }

        private delegate void delegate_DGVInsert(DataRow dr, Image img);
        /// <summary>
        /// 插入列表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="img"></param>
        private void DGVInsert(DataRow dr, Image img)
        {
            if (dgvRoleList.InvokeRequired)
            {
                delegate_DGVInsert d = new delegate_DGVInsert(DGVInsert);
                object arg0 = dr;
                object arg1 = img;
                this.Invoke(d, arg0, arg1);
            }
            else
            {
                dgvRoleList.Rows.Add(dr["F_ID"], dr["F_GRoleName"], img);
            }

        }

        private delegate void delegate_DGVDelete();
        /// <summary>
        /// 删除列表行
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="img"></param>
        private void DGVDelete()
        {
            if (dgvRoleList.InvokeRequired)
            {
                delegate_DGVDelete d = new delegate_DGVDelete(DGVDelete);
                this.Invoke(d);
            }
            else
            {
                dgvRoleList.Rows.RemoveAt(dgvRoleList.SelectedRows[0].Index);
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
                    // this.tboxMSGList.Select(i, p.Length);
                    // this.tboxMSGList.SelectionColor = Color.SteelBlue;
                    this.tboxMSGList.SelectionBackColor = Color.AntiqueWhite;
                    cnt++;
                }
            }
            return cnt;
        }



        private void ToolStripMenuItemSend_Click(object sender, EventArgs e)
        {
            SendMSG();
        }

        private void lboxExample_SelectedIndexChanged(object sender, EventArgs e)
        {
            tboxNote.Text = lboxExample.Text;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlrlistWhere = " and F_Type=20000216 and F_Rowtype=6 and F_GRoleName='" + tboxRoleName.Text + "'";
            //  if (!lblLoading.Visible)
            //{
            //       sqlrlistWhere += " and F_TToolUsed is null";
            //  }
            DataSet ds = _clienthandle.GetAllTasksSyn("", sqlrlistWhere, "F_ID", "ASC", 50, 1);
            if (ds != null)
            {
                bool isRoleAdd = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    isRoleAdd = true;
                    string imgTips = Application.StartupPath + "\\GSSData\\Images\\numerbgClear.png";
                    if (dr["F_TToolUsed"].ToString() == "")
                    {
                        imgTips = Application.StartupPath + "\\GSSData\\Images\\New.png";
                    }
                    Image img = GetImage(imgTips);

                    foreach (DataGridViewRow dgvr in dgvRoleList.Rows)
                    {
                        if (dgvr.Cells[0].Value.ToString() == dr["F_ID"].ToString())
                        {
                            isRoleAdd = false;
                            dgvr.Cells[2].Value = img;
                            dgvRoleList.Rows[dgvr.Index].Selected = true;
                        }
                    }
                    if (isRoleAdd)
                    {
                        dgvRoleList.Rows.Add(dr["F_ID"], dr["F_GRoleName"], img);
                    }
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MsgBox.Show("没有这个角色的聊天记录!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }

        private void btnRlstClear_Click(object sender, EventArgs e)
        {
            dgvRoleList.Rows.Clear();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsvExample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvExample.SelectedItems.Count!=0)
            {
                tboxNote.Text = lsvExample.SelectedItems[0].Text;
            }
        }

    }
}
