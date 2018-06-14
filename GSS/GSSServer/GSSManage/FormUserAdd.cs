using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSServerLibrary;
using GSS.DBUtility;
namespace GSSServer.GSSManage
{
    public partial class FormUserAdd : Form
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        public FormUserAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }


        private void FormUserAdd_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn(ex);
                MessageBox.Show("信息:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            ServerUtil.BindDropDLTDept(this.f_DepartIDComboBox);
            ServerUtil.BindDropDLTRoles(this.f_RoleIDComboBox);
            if (_id != 0)
            {
                this.Text = "用户修改";
                label1.Visible = true;
                label2.Visible = true;
                f_RegTimeLabel1.Visible = true;
                f_LastInTimeLabel1.Visible = true;

                GSSBLL.Users bll = new GSSBLL.Users();
                GSSModel.Users model = bll.GetModel(_id);
                f_UserIDTextBox.Text = model.F_UserID.ToString();
                f_UserNameTextBox.Text = model.F_UserName;
                //f_PassWordTextBox.Text=model.F_PassWord ;
                textBoxPSWH.Text = model.F_PassWord;
                f_DepartIDComboBox.SelectedValue = model.F_DepartID;
                f_RoleIDComboBox.SelectedValue = model.F_RoleID;
                f_RealNameTextBox.Text = model.F_RealName;
                SexradioButton1.Checked = model.F_Sex == false ? false : true;
                SexradioButton2.Checked = !SexradioButton1.Checked;
                f_BirthdayDateTimePicker.Text = model.F_Birthday.ToString();
                f_EmailTextBox.Text = model.F_Email;
                f_MobilePhoneTextBox.Text = model.F_MobilePhone;
                f_TelphoneTextBox.Text = model.F_Telphone;
                f_NoteTextBox.Text = model.F_Note;
                f_IsUsedCheckBox.Checked = model.F_IsUsed;

                f_RegTimeLabel1.Text = model.F_RegTime.ToString();
                f_LastInTimeLabel1.Text = model.F_LastInTime.ToString();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            //验证数据项
            string msg = CheckData();
            if (msg.Length > 0)
            {
                MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBoxPSWH.Text.Trim().Length == 0 && f_PassWordTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入密码!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                textBoxPSWH.Text = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(f_PassWordTextBox.Text.Trim(), "MD5").ToLower();
            }

            //数据准备
            GSSModel.Users model = new GSSModel.Users();
            model.F_UserName = f_UserNameTextBox.Text;
            model.F_PassWord = textBoxPSWH.Text;
            model.F_DepartID = Convert.ToInt32(f_DepartIDComboBox.SelectedValue);
            model.F_RoleID = Convert.ToInt32(f_RoleIDComboBox.SelectedValue);
            model.F_RealName = f_RealNameTextBox.Text;
            model.F_Sex = SexradioButton1.Checked;
            model.F_Birthday = Convert.ToDateTime(f_BirthdayDateTimePicker.Text);
            model.F_Email = f_EmailTextBox.Text;
            model.F_MobilePhone = f_MobilePhoneTextBox.Text;
            model.F_Telphone = f_TelphoneTextBox.Text;
            model.F_Note = f_NoteTextBox.Text;
            model.F_IsUsed = f_IsUsedCheckBox.Checked;



            //数据提交
            GSSBLL.Users bll = new GSSBLL.Users();
            bool isok = false;
            if (_id != 0)
            {
                model.F_UserID = int.Parse(f_UserIDTextBox.Text);
                model.F_LastInTime = DateTime.Now;
                isok = bll.Update(model);
            }
            else
            {
                model.F_RegTime = DateTime.Now;
                int num = bll.Add(model);
                if (num > 0)
                {
                    isok = true;
                }
            }
            if (isok)
            {
                MessageBox.Show("数据保存成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("数据保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 数据检验
        /// </summary>
        /// <returns></returns>
        private string CheckData()
        {
            string msg = "";
            if (this.f_DepartIDComboBox.SelectedValue.ToString() == "0")
            {
                msg += "请选择部门\n\r";
            }
            if (this.f_RoleIDComboBox.SelectedValue.ToString() == "0")
            {
                msg += "请选择角色\n\r";
            }
            return msg;
        }

    }
}
