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
    public partial class FormMenuAdd : Form
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        public FormMenuAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }


        private void FormMenuAdd_Load(object sender, EventArgs e)
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
            if (_id != 0)
            {
                this.Text = "菜单修改";
                f_MenuIDTextBox.Enabled = false;
                GSSBLL.Menus bll = new GSSBLL.Menus();
                GSSModel.Menus model = bll.GetModel(_id);
                f_MenuIDTextBox.Text = model.F_MenuID.ToString();
                f_ParentIDTextBox.Text = model.F_ParentID.ToString();
                f_NameTextBox.Text = model.F_Name;
                f_FormNameTextBox.Text = model.F_FormName;
                f_SortTextBox.Text = model.F_Sort.ToString();
                f_IsUsedCheckBox.Checked = model.F_IsUsed;
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

            //数据准备
            GSSModel.Menus model = new GSSModel.Menus();
            model.F_MenuID = Convert.ToInt32(f_MenuIDTextBox.Text);
            model.F_ParentID = Convert.ToInt32(f_ParentIDTextBox.Text);
            model.F_Name = f_NameTextBox.Text;
            model.F_FormName = f_FormNameTextBox.Text;
            model.F_Sort = Convert.ToInt32(f_SortTextBox.Text);
            model.F_IsUsed = f_IsUsedCheckBox.Checked;


            //数据提交
            GSSBLL.Menus bll = new GSSBLL.Menus();
            bool isok = false;
            if (_id != 0)
            {
                isok = bll.Update(model);
            }
            else
            {
                isok = bll.Add(model);
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
            if (!GSS.DBUtility.WinUtil.IsNumber(f_MenuIDTextBox.Text))
            {
                msg += "菜单编号应该为数字!\r\n";
            }
            if (!GSS.DBUtility.WinUtil.IsNumber(f_ParentIDTextBox.Text))
            {
                msg += "上级菜单应该为数字!\r\n";
            }
            if (f_NameTextBox.Text.Trim().Length == 0)
            {
                msg += "菜单名称不能为空!\r\n";
            }
            if (f_FormNameTextBox.Text.Trim().Length == 0)
            {
                msg += "窗体名称不能为空!\r\n";
            }
            if (!GSS.DBUtility.WinUtil.IsNumber(f_SortTextBox.Text))
            {
                msg += "排序应该为数字!\r\n";
            }
            return msg;
        }

    }
}
