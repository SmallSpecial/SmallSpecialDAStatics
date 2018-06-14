using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI;
using GSS.DBUtility;
using System.Threading;

namespace GSSClient
{
    public partial class FormToolFDBI : GSSUI.AForm.FormMain
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


        public FormToolFDBI(ClientHandles clihandle, int LockUorR, string taskid, string Uname, String Rname)
        {
            InitializeComponent();
            _clihandle = clihandle;
            _LockUorR = LockUorR;
            _taskid = taskid;
            _Uname = Uname;
            _Rname = Rname;
            groupBoxQuerySearch.Text =  LanguageResource.Language.BtnQueryFDBILog;
        }
        private void FormToolGuserLock_Load(object sender, EventArgs e)
        {
            FormInit();
        }
        private void FormInit()
        {
            BindDicComb(comboBoxBigZoneList, SystemConfig.BigZoneParentId.ToString());
            BindDicComb(comboBoxZoneList, "-1");
            BindDicComb(comboBoxType0, "11");
            BindDicCombV(comboBoxType1, "-1");
            rboxOP_TIME.Text = DateTime.Now.AddDays(-1).ToShortDateString();

            if (comboBoxBigZoneList.Items.Count>1)
            {
                comboBoxBigZoneList.SelectedIndex = 1;
            }
            if (comboBoxZoneList.Items.Count > 1)
            {
                comboBoxZoneList.SelectedIndex = 1;
            }
            if (comboBoxType0.Items.Count > 1)
            {
                comboBoxType0.SelectedIndex = 1;
            }
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
                DataRow dra = dtdic.NewRow();
                dra["F_Name"] = "请选择..";
                dra["F_ID"] = "0";
                dtdic.Rows.Add(dra);
                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=" + parentid + "");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cb.DataSource = dtdic;
                cb.DisplayMember = "F_Name";
                cb.ValueMember = "F_ID";
                cb.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定COMBOX字典控件
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="parentid"></param>
        private void BindDicCombV(ComboBox cb, string parentid)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_GameConfig"].Clone();
                DataRow dra = dtdic.NewRow();
                dra["F_Name"] = "请选择..";
                dra["F_ValueGame"] = "0";
                dtdic.Rows.Add(dra);
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


        private void comboBoxBigZoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBigZoneList.SelectedIndex > 0)
            {
                BindDicComb(comboBoxZoneList, comboBoxBigZoneList.SelectedValue.ToString());
            }
            else
            {
                BindDicComb(comboBoxZoneList, "-1");
            }
        }

        private void comboBoxType0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType0.SelectedIndex > 0)
            {
                BindDicCombV(comboBoxType1, comboBoxType0.SelectedValue.ToString());
            }
            else
            {
                BindDicCombV(comboBoxType1, "-1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (rtboxNote.Text.Trim().Length == 0)
            //{
            //    MsgBox.Show("工具使用备注不能为空!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //ComitDoControl(false);

            //_clihandle.GameLockUR(this.Handle.ToInt32(), _LockUorR, _taskid, cboxTime.SelectedValue.ToString());


        }
        private void button2_Click(object sender, EventArgs e)
        {
            tboxUID.Text = "";
            tboxCID.Text = "";
            tboxPARA_1.Text = "";
            tboxPARA_2.Text = "";
            tboxOP_BAK.Text = "";
        }


        /// <summary>
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        /// <param name="isback"></param>
        private void ComitDoControl(bool isback)
        {
            if (isback)
            {

            }
            else
            {

            }
        }

        private void FormToolGuserLock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isToolUsed)
            {
                this.DialogResult = DialogResult.OK;
            }

        }
        DataSet dsss = null;
        private void ButtonRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                {
                    return;
                }
                string table = ClientCache.GetGameConfigByF_ID(comboBoxType0.SelectedValue.ToString());
                table = rboxOP_TIME.Text.Replace("-","_") + "_" + table;

                string sql = @"SELECT ID as 日志编号, UID as 用户编号, CID as 角色编号, PARA_1 as 参数1, PARA_2 as 参数2, OPID as 事件, OP_BAK as 对象方描述, OP_TIME as 处理时间 FROM ["+table+"] where 1=1";
                if (tboxUID.Text.Length>0)
                {
                    sql += " and UID=" + tboxUID.Text + "";
                }
                if (tboxCID.Text.Length>0)
                {
                    sql += " and CID=" + tboxCID.Text + "";
                }
                if (tboxPARA_1.Text.Length > 0)
                {
                    sql += " and PARA_1=" + tboxPARA_1.Text + "";
                }
                if (tboxPARA_2.Text.Length > 0)
                {
                    sql += " and PARA_2=" + tboxPARA_2.Text + "";
                }
                if (comboBoxType1.SelectedIndex>0&&comboBoxType1.SelectedValue.ToString().Trim().Length>0)
                {
                    sql += " and OPID=" + comboBoxType1.SelectedValue + "";
                }
                if (tboxOP_BAK.Text.Length > 0)
                {
                    sql += " and OP_BAK='" + tboxOP_BAK.Text + "'";
                }
                if (!CheckQuerySql(sql))
                {
                    MsgBox.Show("查询语句错误!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RichTextBoxSql.Text = "";
                    return;
                }

                timer1.Enabled = true;//启动按钮禁用计时
                DataSet ds = null;
                if (radioButtonOnLine.Checked)
                {
                    sql = sql.Replace("[", "").Replace("]", "");
                    ds = _clihandle.QueryLiveGSLog(comboBoxZoneList.SelectedValue.ToString(), sql);
                }
                else
                {
                    ds = _clihandle.QuerySynLog(comboBoxZoneList.SelectedValue.ToString(), sql);
                }
                RichTextBoxSql.Text = sql;
                if (ds != null)
                {
                    dsss = ds;
                    dataGridViewUIRequestList.DataSource = ds.Tables[0];
                    int colCount = dataGridViewUIRequestList.Columns.Count - 1;
                    dataGridViewUIRequestList.Columns[colCount].MinimumWidth = 30;
                    dataGridViewUIRequestList.Columns[colCount].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
                else
                {
                    dataGridViewUIRequestList.Rows.Clear();
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Error("FDBI工具", ex);
                MsgBox.Show("操作失败!" + ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ButtonDoSqlQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                {
                    return;
                }
                DataSet ds = null;
                string sql = RichTextBoxSql.Text.Trim();
                if (!CheckQuerySql(sql))
                {
                    MsgBox.Show("查询语句错误!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                timer1.Enabled = true;//启动按钮禁用计时
                if (radioButtonOnLine.Checked)
                {
                    sql = sql.Replace("[", "").Replace("]", "");
                    ds = _clihandle.QueryLiveGSLog(comboBoxZoneList.SelectedValue.ToString(), sql);
                }
                else
                {
                    ds = _clihandle.QuerySynLog(comboBoxZoneList.SelectedValue.ToString(), sql);
                }
                if (ds != null)
                {
                    dataGridViewUIRequestList.DataSource = ds.Tables[0];
                    int colCount = dataGridViewUIRequestList.Columns.Count - 1;
                    dataGridViewUIRequestList.Columns[colCount].MinimumWidth = 30;
                    dataGridViewUIRequestList.Columns[colCount].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
                else
                {
                    dataGridViewUIRequestList.Rows.Clear();
                }
            }
            catch (System.Exception ex)
            {

                ShareData.Log.Error("FDBI工具", ex);
                MsgBox.Show("操作失败!" + ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //SQL查询
        private void labelTool0_Click(object sender, EventArgs e)
        {
            if (LabelStyleDown(((Label)sender)))
            {
                dataGridViewUIRequestList.DataSource = null;
                GroupBoxQuerySql.Location = new Point(12,76);
                groupBoxQuerySearch.Location = new Point(12, -622);
            }
        }
        //模板查询
        private void labelTool1_Click(object sender, EventArgs e)
        {
            //LabelStyleDown(labelTool0);

            FormToolFDBISqlList form = new FormToolFDBISqlList();

            if (form.ShowDialog() == DialogResult.OK)
            {
                RichTextBoxSql.Text = form.sql;

            }

        }
        //FDBI查询
        private void labelTool3_Click(object sender, EventArgs e)
        {
            if (LabelStyleDown(((Label)sender)))
            {
                dataGridViewUIRequestList.DataSource = null;
                GroupBoxQuerySql.Location = new Point(12, -622);
                groupBoxQuerySearch.Location = new Point(12, 76);
            }
        }

        #region 选项LABEL样式更改
        private void labelTool1_MouseEnter(object sender, EventArgs e)
        {
            LabelStyle(((Label)sender), true);
        }
        private void labelTool1_MouseLeave(object sender, EventArgs e)
        {
            LabelStyle(((Label)sender), false);
        }

        private void LabelStyle(Label label, bool isdown)
        {
            if (label.BorderStyle == BorderStyle.Fixed3D)
            {
                return;
            }
            if (isdown)
            {
                label.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                label.BorderStyle = BorderStyle.None;
            }
        }
        private bool LabelStyleDown(Label label)
        {
            if (label.BorderStyle == BorderStyle.Fixed3D)
            {
                return false;
            }
            labelTool0.BorderStyle = BorderStyle.None;
            labelTool3.BorderStyle = BorderStyle.None;
            labelTool0.ForeColor = System.Drawing.SystemColors.ControlText;
            labelTool3.ForeColor = System.Drawing.SystemColors.ControlText;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.ForeColor = System.Drawing.SystemColors.ControlDark;
            return true;
        }
        #endregion

        /// <summary>
        /// 判断请求SQL语句,保证数据安全
        /// </summary>
        private bool CheckQuerySql(string querysql)
        {
            string sql = querysql.ToLower();
            string[] CheckItems = new string[] { "insert", "update", "delete", "into", "create", "alter", "waitfor", "open", "truncate", "drop ", "exec", "holdlock" };//请输入小写特定字符串

            foreach (string CheckItem in CheckItems)
            {
                if (sql.IndexOf(CheckItem) >= 0)
                {
                    return false;
                }
            }
            string[] CheckHItems = new string[] { "drop_log", "fight_log", "gm_lg", "gold_log", "item_log", "money_log", "other_log", "task_log", "trade_log" };//请输入小写特定字符串
            bool HaveMust = false;
            foreach (string CheckHItem in CheckHItems)
            {
                if (sql.IndexOf(CheckHItem) >= 0)
                {
                    HaveMust = true;
                    break;
                }
            }
            return HaveMust;
        }

        /// <summary>
        /// 检查提交的数据
        /// </summary>
        private bool CheckData()
        {
            string msg = "";
            if (comboBoxZoneList.SelectedIndex==0)
            {
                msg += "请选择要查询的战区\n";
            }
            //SQL语句查询
            if (labelTool0.BorderStyle==BorderStyle.Fixed3D)
            {
               
                if (RichTextBoxSql.Text.Trim().Length == 0 || !CheckQuerySql(RichTextBoxSql.Text.Trim()))
                {
                    msg += "SQL查询命令不正确\n";
                    
                }
            }
            //FDBI查询
            if (labelTool3.BorderStyle == BorderStyle.Fixed3D)
            {
                if (comboBoxType0.SelectedIndex==0)
                {
                    msg += "请选择查询类型\n";
                }
               

                if (tboxUID.Text.Length > 0&&!WinUtil.IsNumber(tboxUID.Text))
                {
                    msg += "帐号UID应该为数字\n";
                }
                if (tboxCID.Text.Length > 0 && !WinUtil.IsNumber(tboxCID.Text))
                {
                    msg += "角色CID应该为数字\n";
                }
                if (tboxPARA_1.Text.Length > 0 && !WinUtil.IsNumber(tboxPARA_1.Text))
                {
                    msg += "参数1应该为数字\n";
                }
                if (tboxPARA_2.Text.Length > 0 && !WinUtil.IsNumber(tboxPARA_2.Text))
                {
                    msg += "参数2应该为数字\n";
                }

            }

            if (msg.Length>0)
            {
                MsgBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Tag==null)
            {
                timer1.Tag = DateTime.Now;
                ButtonRequest.Enabled = false;
                ButtonReset.Enabled = false;
                ButtonSqlQuery.Enabled = false;
            }
            else if ((DateTime.Now - Convert.ToDateTime(timer1.Tag)).TotalSeconds > 2)
            {
                ButtonRequest.Enabled = true;
                ButtonReset.Enabled = true;
                ButtonSqlQuery.Enabled = true;
                timer1.Enabled = false;
                timer1.Tag = null;
            }
            else
            {

            }
           
        }
        static Mutex mtx = new Mutex(false,"pro");

        public void statt()
        {

        }
        //导出EXCEL
        private void toolStripButtonToExcel_Click(object sender, EventArgs e)
        {
             Form.CheckForIllegalCrossThreadCalls = false;
            if (dataGridViewUIRequestList.Rows.Count==0)
            {
                MsgBox.Show("没有数据可导出!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormToExcel form = new FormToExcel(dataGridViewUIRequestList, "FDBI查询结果");
            form.ShowDialog();

        }
 
    }
}
