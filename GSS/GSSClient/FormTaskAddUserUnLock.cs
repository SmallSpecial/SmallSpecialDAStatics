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
using GSSModel;

namespace GSSClient
{
    public partial class FormTaskAddUserUnLock : GSSUI.AForm.FormMain
    {
        #region 私有变量

        /// <summary>
        /// 工单类型
        /// </summary>
        private int _tasktype = 20000002;

        private DataSet ds = null;
        #endregion

        public FormTaskAddUserUnLock()
        {

            InitializeComponent();
            InitLanguageText();
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
            BindDicComb(ddlGBigzone, SystemConfig.BigZoneParentId.ToString());
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
                cb.ValueMember = "F_Name";
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
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        /// <param name="isback"></param>
        private void ComitDoControl(bool isback)
        {
            if (isback)
            {
                panel1.Enabled = true;
                btnDosure.Enabled = true;
                btnDoesc.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
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
            int? Type = _tasktype;//帐号封停工单
            int State = 100100102;//处理中
            int GameName = SystemConfig.GameID;//寻龙记
            int? DutyMan = Convert.ToInt32(ShareData.UserID);
            int? PreDutyMan = null;
            int CreatMan = int.Parse(ShareData.UserID);
            DateTime CreatTime = DateTime.Now;
            int EditMan = int.Parse(ShareData.UserID);
            DateTime EditTime = DateTime.Now;
            string bigzonename = ddlGBigzone.SelectedValue.ToString();


            int Rowtype = 0;
            //string ReceivArea = GetTreeValue();


            string strErr = "";

            if (Title.Length == 0)
            {
                strErr += "工单标题不能为空!\n";
            }
            if (gpeoplename.Length == 0)
            {
                strErr += LanguageResource.Language.LblInitiatorNameIsRequire+"!\n";
            }
            if (telephone.Trim().Length < 6)
            {
                strErr += LanguageResource.Language.LblTelFormIsError+"!\n";
            }

            if (Note.Trim().Length == 0)
            {
                strErr += LanguageResource.Language.Tip_RemarkNoEmpty+"!\n";
            }
            if (DGVGameUser.Rows.Count == 0)
            {
                strErr +=  LanguageResource.Language.Tip_GameUseNotIsRequire+"!\n";
            }


            if (strErr != "")
            {
                MsgBox.Show(strErr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ComitDoControl(false);



            try
            {

                GSSBLL.Tasks bll = ClientRemoting.Tasks();

                if (tboxInfo.Text.Trim().Length == 0)
                {
                    foreach (DataGridViewRow dr in DGVGameUser.Rows)
                    {
                        string userid = dr.Cells[0].Value.ToString();
                        string username = dr.Cells[1].Value.ToString();

                        string result = bll.GSSTool_SetUserNoLock(userid);
                        tboxInfo.Text += string.Format(" {0},{1},{2}\n", userid, username, result);
                        tboxInfo.ScrollToCaret();
                    }
                }


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
                model.F_GameBigZone = bigzonename;
                model.F_COther = "";
                model.F_Rowtype = 0;
                model.F_GUserID = "0";
                model.F_GUserName = "批量用户";
                // model.F_URInfo = tboxInfo.Text;
                model.F_Note = Note + "\n用户列表:\n" + tboxInfo.Text;


                if (bll.AddP(model) > 0)
                {
                    MsgBox.Show("工单执行并保存成功", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MsgBox.Show("工单保存失败", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {

                MsgBox.Show("错误:" + ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ComitDoControl(true);
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

        private void btnSearchGameUser_Click(object sender, EventArgs e)
        {
            btnSearchGameUser.Enabled = false;
            try
            {
                if (tboxUserIDs.Text.Trim().Length >= 0)
                {
                    string[] userids = tboxUserIDs.Text.Split(',');

                    foreach (string userid in userids)
                    {
                        DGVInsert(Convert.ToInt32(userid), "NoName");
                    }

                }
                else if (tboxGUserName.Text.Trim().Length > 0)
                {
                    GSSBLL.Tasks bll = ClientRemoting.Tasks();
                    DataSet ds = bll.GSSTool_GetGameUser(ddlGBigzone.SelectedValue.ToString(), " and a.F_UserName like '%" + tboxGUserName.Text + "%'");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DGVInsert((int)dr["F_UserID"], (string)dr["F_UserName"]);
                    }

                }
            }
            catch (System.Exception ex)
            {
                MsgBox.Show("错误:" + ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnSearchGameUser.Enabled = true;
        }

        private delegate void delegate_DGVInsert(int userid, string username);
        /// <summary>
        /// 插入列表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="img"></param>
        public void DGVInsert(int userid, string username)
        {
            if (DGVGameUser.InvokeRequired)
            {
                delegate_DGVInsert d = new delegate_DGVInsert(DGVInsert);
                object arg0 = userid;
                object arg1 = username;
                this.Invoke(d, arg0, arg1);
            }
            else
            {

                bool isRoleAdd = true;
                foreach (DataGridViewRow dgvr in DGVGameUser.Rows)
                {
                    if ((int)dgvr.Cells[0].Value == userid)
                    {
                        isRoleAdd = false;
                        //dgvRoleList.Rows.Remove(dgvr);
                    }
                }
                if (isRoleAdd)
                {
                    DGVGameUser.Rows.Insert(0, userid, username, "移除");
                    labelUserCount.Text = DGVGameUser.Rows.Count.ToString();
                }

            }

        }


        private void DGVGameUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DGVGameUser.Rows.RemoveAt(e.RowIndex);
                labelUserCount.Text = DGVGameUser.Rows.Count.ToString();
            }
        }


    }
}
