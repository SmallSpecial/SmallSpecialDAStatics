using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;
namespace GSSServer.GSSManage
{
    public partial class FormDepartmentAdd : Form
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        public FormDepartmentAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }

        private void FormDepartmentAdd_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void buttonESC_Click(object sender, EventArgs e)
        {
            this.Close();
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

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            GSSServerLibrary.ServerUtil.BindDropDLTDept(this.f_ParentIDComboBox);
            if (_id != 0)
            {
                this.Text = "部门修改";
                GSSBLL.Department bll = new GSSBLL.Department();
                GSSModel.Department model = bll.GetModel(_id);
                f_DepartIDTextBox.Text = model.F_DepartID.ToString();
                f_ParentIDComboBox.SelectedValue = model.F_ParentID;
                f_DepartNameTextBox.Text = model.F_DepartName;
                f_NoterichTextBox.Text = model.F_Note;
            }

        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            //验证数据项
            string msg = "";
            if (f_DepartNameTextBox.Text.Trim().Length == 0)
            {
                msg += "部门名不能为空!\r\n";
            }
            if (f_ParentIDComboBox.SelectedValue.ToString() == f_DepartIDTextBox.Text && f_DepartIDTextBox.Text!="0")
            {
                msg += "上级部门不能选择本部门!\r\n";
            }

            if (msg.Length > 0)
            {
                MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //数据准备
            GSSModel.Department model = new GSSModel.Department();
            model.F_ParentID = int.Parse(f_ParentIDComboBox.SelectedValue.ToString());
            model.F_DepartName = f_DepartNameTextBox.Text;
            model.F_Note = f_NoterichTextBox.Text;
            


            //数据提交
            GSSBLL.Department bll = new GSSBLL.Department();
            bool isok = false;
            if (_id != 0)
            {
                model.F_DepartID = int.Parse(f_DepartIDTextBox.Text);
                isok = bll.Update(model);
            }
            else
            {
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
    }
}
