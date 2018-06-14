﻿using System;
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
    public partial class FormDictionaryAdd : Form
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        public FormDictionaryAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }

        private void FormDictionaryAdd_Load(object sender, EventArgs e)
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
            if (_id > 0)
            {
                this.Text = "字典修改";
                f_DicIDTextBox.Enabled = false;
                GSSBLL.Dictionary bll = new GSSBLL.Dictionary();
                GSSModel.Dictionary model = bll.GetModel(_id);
                f_DicIDTextBox.Text = model.F_DicID.ToString();
                f_ParentIDTextBox.Text = model.F_ParentID.ToString();
                f_ValueTextBox.Text = model.F_Value;
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
            GSSModel.Dictionary model = new GSSModel.Dictionary();
            model.F_DicID=Convert.ToInt32(f_DicIDTextBox.Text);
            model.F_ParentID = Convert.ToInt32(f_ParentIDTextBox.Text);
            model.F_Value = f_ValueTextBox.Text;
            model.F_Sort = Convert.ToInt32(f_SortTextBox.Text);
            model.F_IsUsed = f_IsUsedCheckBox.Checked;


            //数据提交
            GSSBLL.Dictionary bll = new GSSBLL.Dictionary();
            bool isok = false;
            if (_id > 0)
            {
                isok = bll.Update(model);
            }
            else
            {
                if (model.F_DicID == 0) 
                {
                    MessageBox.Show(LanguageResource.Language.Tip_IDShouldGatherThan0);
                    return;
                }
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
            if (!GSS.DBUtility.WinUtil.IsNumber(f_DicIDTextBox.Text))
            {
                msg += "字典编号应该为数字编号!\r\n";
            }
            if (!GSS.DBUtility.WinUtil.IsNumber(f_ParentIDTextBox.Text))
            {
                msg += "上级字典应该为数字编号!\r\n";
            }
            if (f_ValueTextBox.Text.Trim().Length==0)
            {
                msg += "字典值不能为空!\r\n";
            }
            if (!GSS.DBUtility.WinUtil.IsNumber(f_SortTextBox.Text))
            {
                msg += "排序应该为数字!\r\n";
            }
            return msg;
        }


    }
}
