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
    public partial class FormTaskAddGameNotice2 : GSSUI.AForm.FormMain
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

        public FormTaskAddGameNotice2(ClientHandles clienthandle, int tasktype)
        {

            InitializeComponent();
            InitLanguageText();
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
            this.label8.Text = LanguageResource.Language.LblInitiatorName;
            this.groupBox1.Text = LanguageResource.Language.LblNoticeList;
            this.groupBoxInfo.Text = LanguageResource.Language.LblBaseInfo;
            this.lblTaskType.Text = LanguageResource.Language.LblWorkOrderType;
            this.lblURinfo.Text = LanguageResource.Language.LblBaseInfo;
            this.label3.Text = LanguageResource.Language.LblVipLevel + "";
            this.label2.Text = LanguageResource.Language.LblWorkOrderLimit;
            this.button7.Text = LanguageResource.Language.BtnResetAntiIndulgence;
            this.button6.Text = LanguageResource.Language.LblPlayNOTool;
            this.button5.Text = LanguageResource.Language.BtnGagTool;
            this.button4.Text = LanguageResource.Language.BtnCloseDownRole;
            this.button3.Text = LanguageResource.Language.BtnCloseDownAccount;
            this.groupBox4.Text = LanguageResource.Language.LblWorkOrderRemark;
            this.groupBox1.Text = LanguageResource.Language.LblNoticeList;
            this.btnDosure.Text = LanguageResource.Language.BtnSure;
            this.ckboxDonow.Text = LanguageResource.Language.BtnSubmitAndRun;
            this.btnDoesc.Text = LanguageResource.Language.BtnCancel;
            this.toolStripStatusLabel1.Text = LanguageResource.Language.LblReady;
            this.label1.Text = LanguageResource.Language.LblReceiveRange + ":";
            this.tabPage3.Text = LanguageResource.Language.LblNoticeText;
            this.object_2ade7b09_8e2f_47ba_b9b6_121e27e8f2d1.Text = LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText +
               "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50";
            this.aRichTextBoxE2.Lines = new string[] {
         LanguageResource.Language.LblNoticeText+"|1,2,3|1,3|1,3|50",
        LanguageResource.Language.LblNoticeText+"|1,2,3|1,3|1,3|50",
         LanguageResource.Language.LblNoticeText+"|1,2,3|1,3|1,3|50",
         LanguageResource.Language.LblNoticeText+"|1,2,3|1,3|1,3|50"};
            this.aRichTextBoxE2.Texta = LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText +
               "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50";
            this.object_df8ec814_c165_467c_a4d1_f00fc0813f9d.Text = LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText +
               "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50\n" + LanguageResource.Language.LblNoticeText + "|1,2,3|1,3|1,3|50";

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
            lblTaskType.Text = LanguageResource.Language.LblWorkOrderType + ":" + ClientCache.GetDicPCName(_tasktype.ToString());//20000213 喊话工单
            string userinfo = "";

            lblURinfo.Text = userinfo;

            dateTimePickerBegin.Text = DateTime.Now.ToString(SystemConfig.DateHourMinute);
            dateTimePickerEnd.Text = DateTime.Now.AddMinutes(15).ToString(SystemConfig.DateHourMinute);

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
        void CallMsgPosotion()
        {
            radioButtonRL0.Visible = false;
            radioButtonRL1.Visible = false;
            radioButtonRL2.Visible = false;
            radioButtonRL3.Visible = false;
            radioButtonRL4.Visible = false;
            radioButtonRL5.Location = new Point(radioButtonRL5.Location.X - 150, radioButtonRL5.Location.Y - 20);
            radioButtonRL5.Checked = true;//目前游戏客户端不设置显示公告的颜色【2017-07-26】
            label6.Visible = false;
            panelObject.Visible = false;
        }
        /// <summary>
        /// 设置控件初始化
        /// </summary>
        private void SetControls()
        {
            //不修改文本的颜色
            CallMsgPosotion();//修改公告显示位置目前不设置
            label25.Visible = false;
            pictureBoxColor.Visible = false;
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

                //DataRow[] drdic2 = ds.Tables["T_GameConfig"].Select("F_ParentID=1000");
                //TreeNode node = null;
                //foreach (DataRow dr2 in drdic2)
                //{
                //    node = treeViewArea.AddTreeNode(treeViewArea.Nodes, dr2["F_Name"].ToString(), false);
                //    node.Tag = dr2["F_ValueGame"].ToString();
                //    InitTreeView(ds, node, dr2["F_ID"].ToString());

                //}
                InitTreeView(ds, null, "1000");
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
                if (panode == null)
                {
                    node = treeViewArea.AddTreeNode(treeViewArea.Nodes, dr2["F_Name"].ToString(), false);
                }
                else
                {
                    node = treeViewArea.AddTreeNode(panode.Nodes, dr2["F_Name"].ToString(), false);
                }

                node.Tag = dr2["F_ValueGame"].ToString();
                InitTreeView(ds, node, dr2["F_ID"].ToString());
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
            GSSModel.Tasks model = GetTaskData();
            string strErr = "";

            if (model.F_Title.Length == 0)
            {
                strErr += "公告工单标题不能为空!\n";
            }
            if (model.F_GPeopleName.Length == 0)
            {//在公告数据中是将发起人的数据存储到玩家信息中
                strErr += LanguageResource.Language.LblInitiatorNameIsRequire + "!\n";
            }
            //edit hexw 2017-9-18 取消电话号码必须
            //if (model.F_Telphone.Trim().Length == 0)
            //{
            //    // strErr += LanguageResource.Language.LblTelNotAllowBlank + "！\n";
            //}
            //else if (!WinUtil.IsTelphone(model.F_Telphone) && !WinUtil.IsMobile(model.F_Telphone))
            //{
            //    strErr += LanguageResource.Language.LblTelFormIsError + "！\n(格式:010-88886666或13912341234)\n";
            //}

            if (string.IsNullOrEmpty(model.F_URInfo))
            {
                strErr += LanguageResource.Language.Tip_NoticeMessgaeIsRequired + "！\n";
            }
            else if (model.F_URInfo.Length > 300)
            {
                strErr += LanguageResource.Language.Tip_LimitNoticeMsgLength + "\r\n";
            }
            if (model.F_TUseData.Trim().Length == 0)
            {
                strErr += "请选择接收范围!\n";
            }
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

            if (model.F_Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty + "!\n";
            }
            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string backStr = _clienthandle.AddTaskSyn(model);
            ComitDoControl(true);
            if (backStr == "0")
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateFailure, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _taskid = backStr;
                this.Close();
            }

        }
        GSSModel.Tasks GetTaskData()
        {
            string Title = tboxTitle.Text.Trim();
            string gpeoplename = tboxCreator.Text.Trim();
            string telephone = tboxTelephone.Text.Trim();
            string Note = rboxNote.Text.Trim();
            int From = SystemConfig.AppID;//客服中心
            int VipLevel = int.Parse(cboxVIP.SelectedValue.ToString());
            DateTime? LimitTime = GetLimitTime();
            int LimitType = int.Parse(cboxLimitTime.SelectedValue.ToString());
            int? Type = _tasktype;//喊话工单
            int State = SystemConfig.DefaultWorkOrderStatue;//等待处理
            int GameName = SystemConfig.GameID;//寻龙记
            int? DutyMan = null;
            int? PreDutyMan = null;
            int CreatMan = int.Parse(ShareData.UserID);
            DateTime CreatTime = DateTime.Now;
            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string URInfo = aRichTextBoxCode.Text;
            int Rowtype = 0;
            bool isview = false;
            if (buttonCode.Text == LanguageResource.Language.LblEdit)
            {
                isview = true;
                buttonCode_Click(null, null);
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
            if (isview)
            {
                buttonCode_Click(null, null);
            }

            ComitDoControl(false);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_Title = Title;
            model.F_GPeopleName = gpeoplename;
            model.F_Telphone = telephone;
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
            string ReceivArea = GetTreeValue();
            model.F_TUseData = ReceivArea;
            model.F_Rowtype = Rowtype;
            model.F_URInfo = URInfo.Replace("\n", "www");
            return model;
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



        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxColor.Size = new Size(20, 20);
            this.pictureBoxColor.Invalidate();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxColor.Size = new Size(19, 19);
            this.pictureBoxColor.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    dlg.Color = aRichTextBoxMessage.SelectionColor;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Application.DoEvents();
                        //aRichTextBoxMessage.Select(0, aRichTextBoxMessage.Text.Length);
                        aRichTextBoxMessage.SelectionColor = dlg.Color;
                        // aRichTextBoxMessage.SelectionStart = aRichTextBoxMessage.Text.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        private void aButtonCreatCode_Click(object sender, EventArgs e)
        {
            if (aRichTextBoxMessage.Text.Replace("\n", "").Replace("|", "").Trim().Length == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_NotcieIsRequire + "!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool isview = false;
            if (buttonCode.Text == LanguageResource.Language.LblEdit)
            {
                isview = true;
                buttonCode_Click(null, null);
            }

            string codeStr = "";

            string msg = aRichTextBoxMessage.Text;
            string msgcode = "";
            if (radioButtonRL5.Checked)
            {//屏幕最上方
                msgcode = msg;
            }
            else
            {
                Color msgcolor = new Color();
                for (int i = 0; i < msg.Length; i++)
                {
                    aRichTextBoxMessage.Select(i, 1);
                    if (i == 0 || msgcolor != aRichTextBoxMessage.SelectionColor)
                    {
                        msgcolor = aRichTextBoxMessage.SelectionColor;

                        if (i != 0)
                        {
                            msgcode += "&199";
                        }

                        msgcode += "&2" + msgcolor.R.ToString().PadLeft(3, '0') + msgcolor.G.ToString().PadLeft(3, '0') + msgcolor.B.ToString().PadLeft(3, '0');
                    }
                    msgcode += aRichTextBoxMessage.SelectedText;
                }
                msgcode += "&199";
            }


            codeStr += msgcode.Replace("\n", "").Replace("|", "").Trim() + "|";


            codeStr += textBoxROlevelB.Text + "," + textBoxROlevelE.Text + "|";

            codeStr += textBoxRL0.Text + "|";

            codeStr += dateTimePickerBegin.Text + "|";
            codeStr += dateTimePickerEnd.Text + "|";

            int secsum = Convert.ToInt32(textBoxTd.Text) * 24 * 60 * 60 + Convert.ToInt32(textBoxTh.Text) * 60 * 60 + Convert.ToInt32(textBoxTm.Text) * 60 + Convert.ToInt32(textBoxTs.Text);
            codeStr += secsum.ToString();

            tabControl1.SelectTab(0);

            if (aRichTextBoxCode.Text.Trim().Length > 1 && aRichTextBoxCode.Text.Trim().Substring(aRichTextBoxCode.Text.Trim().Length - 2, 2) != "\n")
            {
                //edit hexw 2017-9-9 生成代码前把预览框清空,一次只生成一条公告设置
                aRichTextBoxCode.Text = "";
                // codeStr = "\n" + codeStr;
            }

            aRichTextBoxCode.Text += codeStr;
            aRichTextBoxCode.Focus();
            aRichTextBoxCode.SelectionStart = aRichTextBoxCode.Text.Length;
            if (isview)
            {
                buttonCode_Click(null, null);
            }

        }

        #region 输入验证事件
        private void radioButtonRO0_CheckedChanged(object sender, EventArgs e)
        {
            panelROLevel.Visible = false;
            textBoxROlevelB.Text = "0";
            textBoxROlevelE.Text = "500";
        }

        private void radioButtonRO1_CheckedChanged(object sender, EventArgs e)
        {
            panelROLevel.Visible = false;
            textBoxROlevelB.Text = "0";
            textBoxROlevelE.Text = "10";
        }

        private void radioButtonR2_CheckedChanged(object sender, EventArgs e)
        {
            panelROLevel.Visible = false;
            textBoxROlevelB.Text = "60";
            textBoxROlevelE.Text = "500";
        }

        private void radioButtonRO3_CheckedChanged(object sender, EventArgs e)
        {
            panelROLevel.Visible = true;
            textBoxROlevelB.Text = "0";
            textBoxROlevelE.Text = "60";
        }

        private void radioButtonRL0_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "0";
            pictureBoxColor.Enabled = true;
        }

        private void radioButtonRL1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "1";
            pictureBoxColor.Enabled = true;
        }

        private void radioButtonRL2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "2";
            pictureBoxColor.Enabled = true;
        }

        private void radioButtonRL3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "3";
            pictureBoxColor.Enabled = true;
        }
        private void radioButtonRL4_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "4";
            pictureBoxColor.Enabled = true;
        }
        private void radioButtonRL5_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRL0.Text = "5";
            pictureBoxColor.Enabled = false;
        }

        private void textBoxROlevelB_TextChanged(object sender, EventArgs e)
        {
            textBoxROlevelB.Text = textBoxROlevelB.Text.Trim();
            if (textBoxROlevelB.Text.IndexOf("0") == 0 && textBoxROlevelB.Text.Length > 1)
            {
                textBoxROlevelB.Text = textBoxROlevelB.Text.Substring(1);
            }
            if (textBoxROlevelB.Text.Length > 0 && !WinUtil.IsNumber(textBoxROlevelB.Text))
            {
                textBoxROlevelB.Text = "0";
            }
        }

        private void textBoxROlevelE_TextChanged(object sender, EventArgs e)
        {
            textBoxROlevelE.Text = textBoxROlevelE.Text.Trim();
            if (textBoxROlevelE.Text.IndexOf("0") == 0 && textBoxROlevelE.Text.Length > 1)
            {
                textBoxROlevelE.Text = textBoxROlevelE.Text.Substring(1);
            }
            if (textBoxROlevelE.Text.Length > 0 && !WinUtil.IsNumber(textBoxROlevelE.Text))
            {
                textBoxROlevelE.Text = "60";
            }
        }

        private void textBoxTd_TextChanged(object sender, EventArgs e)
        {
            textBoxTd.Text = textBoxTd.Text.Trim();
            if (textBoxTd.Text.IndexOf("0") == 0 && textBoxTd.Text.Length > 1)
            {
                textBoxTd.Text = textBoxTd.Text.Substring(1);
            }
            if (textBoxTd.Text.Length > 0 && !WinUtil.IsNumber(textBoxTd.Text))
            {
                textBoxTd.Text = "0";
            }
        }

        private void textBoxTh_TextChanged(object sender, EventArgs e)
        {
            textBoxTh.Text = textBoxTh.Text.Trim();
            if (textBoxTh.Text.IndexOf("0") == 0 && textBoxTh.Text.Length > 1)
            {
                textBoxTh.Text = textBoxTh.Text.Substring(1);
            }
            if (textBoxTh.Text.Length > 0 && !WinUtil.IsNumber(textBoxTh.Text))
            {
                textBoxTh.Text = "0";
            }
        }

        private void textBoxTm_TextChanged(object sender, EventArgs e)
        {
            textBoxTm.Text = textBoxTm.Text.Trim();
            if (textBoxTm.Text.IndexOf("0") == 0 && textBoxTm.Text.Length > 1)
            {
                textBoxTm.Text = textBoxTm.Text.Substring(1);
            }
            if (textBoxTm.Text.Length > 0 && !WinUtil.IsNumber(textBoxTm.Text))
            {
                textBoxTm.Text = "0";
            }
        }

        private void textBoxTs_TextChanged(object sender, EventArgs e)
        {
            textBoxTs.Text = textBoxTs.Text.Trim();
            if (textBoxTs.Text.IndexOf("0") == 0 && textBoxTs.Text.Length > 1)
            {
                textBoxTs.Text = textBoxTs.Text.Substring(1);
            }
            if (textBoxTs.Text.Length > 0 && !WinUtil.IsNumber(textBoxTs.Text))
            {
                textBoxTs.Text = "0";
            }
        }

        private void textBoxROlevelE_Leave(object sender, EventArgs e)
        {
            if (textBoxROlevelE.Text.Trim().Length == 0)
            {
                textBoxROlevelE.Text = "0";
            }
        }

        private void textBoxROlevelB_Leave(object sender, EventArgs e)
        {
            if (textBoxROlevelB.Text.Trim().Length == 0)
            {
                textBoxROlevelB.Text = "0";
            }
        }

        private void textBoxTd_Leave(object sender, EventArgs e)
        {
            if (textBoxTd.Text.Trim().Length == 0)
            {
                textBoxTd.Text = "0";
            }
        }

        private void textBoxTh_Leave(object sender, EventArgs e)
        {
            if (textBoxTh.Text.Trim().Length == 0)
            {
                textBoxTh.Text = "0";
            }
        }

        private void textBoxTm_Leave(object sender, EventArgs e)
        {
            if (textBoxTm.Text.Trim().Length == 0)
            {
                textBoxTm.Text = "0";
            }
        }

        private void textBoxTs_Leave(object sender, EventArgs e)
        {
            if (textBoxTs.Text.Trim().Length == 0)
            {
                textBoxTs.Text = "0";
            }
        }
        #endregion

        private string CodeStr = "";
        private string changStrColor;
        private void buttonCode_Click(object sender, EventArgs e)
        {
            if (buttonCode.Text == LanguageResource.Language.LblPreview)
            {
                buttonCode.Text = LanguageResource.Language.LblEdit;
                CodeStr = aRichTextBoxCode.Text;
                //转换颜色
                string codeBeginStr = "";
                string colorlistStr = "";
                string Gnotice = aRichTextBoxCode.Text.Replace("\n", "\\par").Replace("&199", "}");
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


                changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\li300\lang2052\f0\fs18 " + Gnotice;

                changStrColor += @"}";
                aRichTextBoxCode.RTF = changStrColor;
            }
            else
            {
                buttonCode.Text = LanguageResource.Language.LblPreview;
                aRichTextBoxCode.Text = CodeStr;
            }
            aRichTextBoxCode.TextChangeds += new GSSUI.AControl.ARichTextBox.ARichTextBoxE.TextChangedkEventHandler(this.aRichTextBoxCode_TextChanged);

        }
        private void aRichTextBoxCode_TextChanged(object sender, EventArgs e)
        {
            if (buttonCode.Text == LanguageResource.Language.LblEdit)
            {
                //MsgBox.Show("请在编辑状态进行修改!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                aRichTextBoxCode.Undo();
            }
        }

        //private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    if (e.Action == TreeViewAction.ByMouse)
        //    {

        //        //取消节点选中状态之后，取消所有父节点的选中状态
        //        setChildNodeCheckedState(e.Node, e.Node.Checked);
        //        //如果节点存在父节点，取消父节点的选中状态
        //        if (e.Node.Parent != null)
        //        {
        //            setParentNodeCheckedState(e.Node, e.Node.Checked);
        //        }

        //        //if (e.Node.Checked)
        //        //{
        //        //    //取消节点选中状态之后，取消所有父节点的选中状态
        //        //    setChildNodeCheckedState(e.Node, true);
        //        //}
        //        //else
        //        //{
        //        //    //取消节点选中状态之后，取消所有父节点的选中状态
        //        //    setChildNodeCheckedState(e.Node, false);
        //        //    //如果节点存在父节点，取消父节点的选中状态
        //        //    if (e.Node.Parent != null)
        //        //    {
        //        //        setParentNodeCheckedState(e.Node, false);
        //        //    }
        //        //}
        //    }
        //}
        ////取消节点选中状态之后，取消所有父节点的选中状态
        //void setParentNodeCheckedState(TreeNode currNode, bool state)
        //{
        //    TreeNode parentNode = currNode.Parent;
        //    parentNode.Checked = state;

        //    int num=0;
        //    for (int i=0;i<parentNode.Nodes.Count;i++)
        //    {
        //        if (parentNode.Nodes[i].Checked)
        //        {
        //            num++;
        //        }
        //    }

        //    if (num>0&&!state)
        //    {
        //        parentNode.StateImageIndex = 2;
        //    }

        //    if (currNode.Parent.Parent != null)
        //    {
        //        setParentNodeCheckedState(currNode.Parent, state);
        //    }
        //}
        ////选中节点之后，选中节点的所有子节点
        //void setChildNodeCheckedState(TreeNode currNode, bool state)
        //{
        //    TreeNodeCollection nodes = currNode.Nodes;
        //    if (nodes.Count > 0)
        //        foreach (TreeNode tn in nodes)
        //        {
        //            tn.Checked = state;
        //            setChildNodeCheckedState(tn, state);
        //        }
        //}

        //        // 调用 BuildTree 方法来创建 TreeView
        //CreateTreeViewFromDataTable.BuildTree(
        //    this._dtEmployees, this.treeView1,
        //    true, "名字", "员工标识", "上级");

        private void Button_Click(object sender, EventArgs e)
        {//创建并立即执行
            GSSModel.Tasks task = GetTaskData();
            GSSModel.TaskContainerLogicData wl = new GSSModel.TaskContainerLogicData()
            {
                WorkOrder = task,
                LogicData = new GSSModel.Request.RunTask() { Command = GSSCSFrameWork.msgCommand.GameNoticeStart.ToString() }
            };
            GSSModel.Request.ClientData cd = new GSSModel.Request.ClientData()
            {
                Data = wl,
                FormID = this.Handle.ToInt32()
            };
            _clienthandle.SendTaskContainerLogicData(cd);
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {//save data response
                case 601:
                    this.Activate();
                    string.Format("class:{0}, Msg:{1},m.LParam:{2},HWnd:{3},WParam:{4}", typeof(FormTaskAddGameNotice2).Name, m.Msg, m.LParam.ToString(), m.HWnd.ToString(), m.WParam.ToString()).Logger();
                    string index = m.LParam.ToString();
                    if (Convert.ToInt32(index) == 0)
                    {
                        index = m.WParam.ToString();
                    }
                    int ind = int.Parse(index) - 1;
                    object obj = ShareData.Msg[ind];
                    ShareData.Msg.RemoveAt(ind);
                    GSSModel.Request.ClientData data = obj as GSSModel.Request.ClientData;
                    if (data.Success)
                    {
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_WorkOrderCreateSucc), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MsgBox.Show(data.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
    }
}
