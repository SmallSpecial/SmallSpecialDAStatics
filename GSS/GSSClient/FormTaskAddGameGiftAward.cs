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
using System.IO;
namespace GSSClient
{
    public partial class FormTaskAddGameGiftAward : GSSUI.AForm.FormMain
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

        private DataSet ds = null;
        #endregion

        public FormTaskAddGameGiftAward(ClientHandles clienthandle, int tasktype)
        {

            InitializeComponent();
            InitLanguageText();
            _clienthandle = clienthandle;
            _tasktype = tasktype;
            //窗体位置居中
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //工单信息初始化
            SetGameUR();
            //设置控件初始化 限制输入数字
            SetControls();

        }
        void InitLanguageText()
        {
            string[] awardUserGridColumn = SystemConfig.GetAwardUserColumn;
            //DGVGameUser.Columns.Clear();
            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();
            foreach (var item in awardUserGridColumn)
            {
                DataGridViewTextBoxColumn column = new System.Windows.Forms.DataGridViewTextBoxColumn();
                column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                column.DataPropertyName = MapLanguageManage.GetString(item);
                column.FillWeight = 80F;
                column.HeaderText = MapLanguageManage.GetString(item);
                column.MinimumWidth = 84;
                column.Name = "Column1";
                column.ReadOnly = true;
                columns.Add(column);
            }
            //DGVGameUser.Columns.AddRange(columns.ToArray());
            this.button7.Text = global::GSSClient.LanguageResource.Language.BtnResetAntiIndulgence;
            this.button6.Text = global::GSSClient.LanguageResource.Language.LblPlayNOTool;
            this.button5.Text = global::GSSClient.LanguageResource.Language.BtnGagTool;
            this.button4.Text = global::GSSClient.LanguageResource.Language.BtnCloseDownRole;
            this.button3.Text = global::GSSClient.LanguageResource.Language.BtnCloseDownAccount;
            label12.Text = LanguageResource.Language.Tip_AwardUserListExcel;
            label9.Text = LanguageResource.Language.LblTel;
            this.label3.Text = LanguageResource.Language.LblVipLevel + "";
            this.label2.Text = LanguageResource.Language.LblWorkOrderLimit;
            this.label1.Text = LanguageResource.Language.LblUserList + ":";
            //this.aButton1.Text = LanguageResource.Language.BtnSelectUserListWithImport;
            this.label15.Text = LanguageResource.Language.LblEmailInfo;
            this.groupBoxInfo.Text = LanguageResource.Language.LblBaseInfo;
            this.lblURinfo.Text = LanguageResource.Language.LblBaseInfo;
            this.label8.Text = LanguageResource.Language.LblInitiatorName;
            this.radioButtonGiftType1.Text = LanguageResource.Language.LblPackage;
            this.radioButtonGiftType0.Text = LanguageResource.Language.LblProp;
            this.label7.Text = LanguageResource.Language.LblGiftType + ":";
            this.label6.Text = LanguageResource.Language.LblGiftName + ":";
            this.label7.Text = LanguageResource.Language.LblGiftType + ":";
            this.label13.Text = LanguageResource.Language.LblAwardBigZone + ":";
            this.groupBox1.Text = LanguageResource.Language.LblNoticeList;
            //this.label14.Text = LanguageResource.Language.LblImportUserList + ":";
            btnDosure.Text = LanguageResource.Language.BtnSure;
            btnDoesc.Text = LanguageResource.Language.BtnCancel;
            this.toolStripStatusLabel1.Text = LanguageResource.Language.LblReady;
            //this.labelUserCount.Text = LanguageResource.Language.LblUnit_Number;
            //邮件标题
            label5.Text = LanguageItems.BaseLanguageItem.LblTitle;

            lbItem1.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp1;
            lbItem2.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp2;
            lbItem3.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp3;
            lbItem4.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp4;
            lbItem5.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp5;
            lbItemNum1.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum1;
            lbItemNum2.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum2;
            lbItemNum3.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum3;
            lbItemNum4.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum4;
            lbItemNum5.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum5;
            lblBlueDiamond.Text = LanguageItems.BaseLanguageItem.LblBlueDiamond;
            lblMoney.Text = LanguageItems.BaseLanguageItem.LblRedDiamond;
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
            lblTaskType.Text = LanguageResource.Language.LblWorkOrderType + ":" + ClientCache.GetDicPCName(_tasktype.ToString());
            string userinfo = "";

            lblURinfo.Text = userinfo;

            BindDicComb(comboBoxGBigzone, SystemConfig.BigZoneParentId.ToString());
            BindDicComb(comboBoxBattlezone, SystemConfig.BattleZoneParentId.ToString());
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
                DataTable dtdic = ds.Tables["T_GameConfig"].Clone();
                //DataRow dra = dtdic.NewRow();
                //dra["F_Name"] = "全部类型";
                //dra["F_Name"] = "0";
                //dtdic.Rows.Add(dra);
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=" + parentid + "");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cb.DataSource = dtdic;
                cb.DisplayMember = "F_Name";
                cb.ValueMember = "F_ValueGame";
                cb.SelectedIndex = 0;
            }
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
        /// 设置控件初始化 限制输入数字
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


            //只能输入数字
            tbItem1.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem2.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem3.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem4.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem5.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum1.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum2.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum3.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum4.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum5.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);


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
            string bigzonename = comboBoxGBigzone.Text.ToString();
            //  string giftStr = textBoxGiftID.Text + "|" + textBoxGiftName.Text + "|" + (radioButtonGiftType0.Checked ? "0" : "1") + "|" + textBoxGiftNum.Text;
            //string URInfo = aRichTextBoxCode.Text;
            int Rowtype = 0;
            //string ReceivArea = GetTreeValue();

            #region 道具赋值
            StringBuilder sbItem = new StringBuilder();
            int t = 0;
           
            if (int.TryParse(tbItem1.Text, out t))
            {

                sbItem.Append(t);

                if (int.TryParse(tbItemNum1.Text, out t))
                {
                    sbItem.Append("|" + t);
                }
                else
                {
                    sbItem.Append("|" + 0);
                }
            }
            else
            {
                sbItem.Append(0 + "|" + 0);
            }


            if (int.TryParse(tbItem2.Text, out t))
            {
                sbItem.Append("|" + t);
                if (int.TryParse(tbItemNum2.Text, out t))
                {

                    sbItem.Append("|" + t);
                }
                else
                {
                    sbItem.Append("|" + 0);
                }
            }
            else
            {
                sbItem.Append("|" + 0 + "|" + 0);
            }

            if (int.TryParse(tbItem3.Text, out t))
            {

                sbItem.Append("|" + t);
                if (int.TryParse(tbItemNum3.Text, out t))
                {

                    sbItem.Append("|" + t);
                }
                else
                {
                    sbItem.Append("|" + 0);
                }
            }
            else
            {
                sbItem.Append("|" + 0 + "|" + 0);
            }

            if (int.TryParse(tbItem4.Text, out t))
            {

                sbItem.Append("|" + t);
                if (int.TryParse(tbItemNum4.Text, out t))
                {

                    sbItem.Append("|" + t);
                }
                else
                {
                    sbItem.Append("|" + 0);
                }
            }
            else
            {
                sbItem.Append("|" + 0 + "|" + 0);
            }

            if (int.TryParse(tbItem5.Text, out t))
            {

                sbItem.Append("|" + t);
                if (int.TryParse(tbItemNum5.Text, out t))
                {

                    sbItem.Append("|" + t);
                }
                else
                {
                    sbItem.Append("|" + 0);
                }
            }
            else
            {
                sbItem.Append("|" + 0 + "|" + 0);
            }

            if (int.TryParse(txtBlueDiamond.Text, out t))
            {

                sbItem.Append("|" + t);
            }
            else
            {
                sbItem.Append("|" + 0);
            }
            if (int.TryParse(txtMoney.Text, out t))
            {

                sbItem.Append("|" + t);
            }
            else
            {
                sbItem.Append("|" + 0);
            }
            #endregion

            string giftStr = sbItem.ToString();




            string strErr = "";

            if (Title.Length == 0)
            {
                strErr += LanguageResource.Language.Tip_WorkOrderTiltleIsRequire + "!\n";
            }
            if (gpeoplename.Length == 0)
            {
                strErr += LanguageResource.Language.LblInitiatorNameIsRequire + "!\n";
            }
            if (telephone.Trim().Length < 6)
            {
                //取消电话号码判断
                //strErr += LanguageResource.Language.LblTelFormIsError + "!\n";
            }

            if (Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty + "!\n";
            }
            //if (DGVGameUser.Rows.Count == 0)
            //{
            //    strErr += LanguageResource.Language.Tip_GameUseNotIsRequire + "!\n";
            //}
            if (aRichTextBoxUserList.Text.Trim().Length==0)
            {
                strErr += LanguageResource.Language.Tip_GameUseNotIsRequire + "!\n";
            }
            GSSModel.Response.GameConfig big = GetComboBoxSelectValue(comboBoxBattlezone);
            DataTable dt = new DataTable("table_a");
            DataColumn dc = null;
            dc = dt.Columns.Add("角色名称", System.Type.GetType("System.String"));
            dc = dt.Columns.Add("战区编号", System.Type.GetType("System.String"));

            string[] strUserInfo = aRichTextBoxUserList.Text.Trim().Split(Environment.NewLine.ToCharArray());
            foreach (string userInfo in strUserInfo)
            {
                DataRow newRow;
                newRow = dt.NewRow();
                newRow["角色名称"] = userInfo;
                newRow["战区编号"] = "-1";
                dt.Rows.Add(newRow);
            }
            ds = new DataSet();
            ds.Tables.Add(dt);
            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(ds==null)
            {
                MsgBox.Show(LanguageResource.Language.Tip_Error);
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
            model.F_PreDutyMan = rBID.Checked ? 0 : 1;//存储输入类型:ID或Name//PreDutyMan;
            model.F_CreatMan = CreatMan;
            model.F_CreatTime = CreatTime;
            model.F_EditMan = EditMan;
            model.F_EditTime = EditTime;
            model.F_GameBigZone = bigzonename;
            model.F_COther = giftStr;
            model.F_Rowtype = Rowtype;

            string backStr = _clienthandle.AddTaskSynGA(model, ds);
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
        GSSModel.Response.GameConfig GetComboBoxSelectValue(ComboBox cmb)
        {
            GSSModel.Response.GameConfig config = cmb.SelectedItem as GSSModel.Response.GameConfig;
            return config;
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

        //private void aButton1_Click(object sender, EventArgs e)
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        Application.DoEvents();
        //        textBoxFile.Text = openFileDialog1.FileName;
        //        textBoxFile.SelectionStart = textBoxFile.Text.Length;

        //        ds = GetDataSet(textBoxFile.Text, MapLanguageManage.GetStringByMapLanguageConfig(MapLanguageConfig.Map_AwardUserSheetName), "1=1");
        //        if (ds != null)
        //        {
        //            DGVGameUser.AutoGenerateColumns = false;
        //            DGVGameUser.DataSource = ds.Tables[0];
        //            labelUserCount.Text = "(" + ds.Tables[0].Rows.Count.ToString() + ")";
        //        }
        //        else
        //        {
        //            DGVGameUser.Rows.Clear();
        //            labelUserCount.Text = "(0)";
        //        }

        //    }
        //}


        public DataSet GetDataSet(string filename, string tname, string wherestr)//返回excel中不把第一行当做标题看待的数据集
        {
            try
            {
                //OleDbDataAdapter read column donot support ko-kr ,first read the sheet
                string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=yes\"";
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(sConnectionString);
                string sql_select_commands = "Select * from [" + tname + "$] where len([" + MapLanguageManage.GetStringByMapLanguageConfig(MapLanguageConfig.Map_UserNo)
                    + "])>0 and len([" + MapLanguageManage.GetStringByMapLanguageConfig(MapLanguageConfig.Map_RoleNo)
                    + "])>0 and len([" + MapLanguageManage.GetStringByMapLanguageConfig(MapLanguageConfig.Map_ZoneNo) + "])>0";
                if (wherestr.Length != 0 && wherestr != "1=1")
                {
                    sql_select_commands += " where " + wherestr;
                }
                System.Data.OleDb.OleDbDataAdapter adp = new System.Data.OleDb.OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, "table_a");
                adp.Dispose();
                connection.Close();
                return ds;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message.ToString()); return null;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            //下载模本(从服务端下载)
            string lang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            string file = "Award.xlsx";
            if (folderDialog.ShowDialog() == DialogResult.OK)
            { //从服务器下载模本文件
                GSSModel.TemplateFile tem = new GSSModel.TemplateFile()
                {
                    TemplateName = file,
                    SystemLang = lang
                };
                GSSCSFrameWork.MsgStruts response = _clienthandle.DownloadTemplateFile(tem);
                if (response.msgsendstate != GSSCSFrameWork.msgSendState.None)
                {
                    FileStream fs = new FileStream(folderDialog.SelectedPath + "//" + file, FileMode.Create, FileAccess.Write);
                    fs.Write(response.Data, 0, response.Data.Length);
                    fs.Close();
                }
                else
                {
                    string msg = GSSCSFrameWork.DataSerialize.GetObjectFromByte(response.Data) as string;
                    MsgBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
