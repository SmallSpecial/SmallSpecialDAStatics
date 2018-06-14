using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSSClient
{
    public partial class FormToolFDBISqlAdd : GSSUI.AForm.FormMain
    {
        GSSBLL.FDBISql bll = ClientRemoting.FDBISql();
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        public FormToolFDBISqlAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }

        private void FormToolFDBISqlAdd_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void btnDosure_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Warn(ex);
                MessageBox.Show("信息:" + ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDoesc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            if (_id != 0)
            {
                this.Text = "查询模板修改";
                GSSModel.FDBISql model = bll.GetModel(_id);
                tboxTitle.Text = model.F_Title;
                tboxSql.Text = model.F_Sql;
                tboxNote.Text = model.F_Note;
                if (model.F_UserID==-1)
                {
                    rbtnPublic.Checked = true;
                }
                else
                {
                    rbtnSelf.Checked = true;
                }
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
                MessageBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //数据准备
            GSSModel.FDBISql model = new GSSModel.FDBISql();
            model.F_ID = _id;
            model.F_Title = tboxTitle.Text.Trim();
            model.F_Sql = tboxSql.Text.Trim();
            model.F_Note = tboxNote.Text.Trim();
            model.F_UserID = rbtnPublic.Checked ? -1 : Convert.ToInt16(ShareData.UserID);
            model.F_DaTeTime = DateTime.Now;

            //数据提交
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
                GSSUI.MsgBox.Show("数据保存成功!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                GSSUI.MsgBox.Show("数据保存失败!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 数据检验
        /// </summary>
        /// <returns></returns>
        private string CheckData()
        {
            string msg = "";
            if (tboxTitle.Text.Trim().Length == 0)
            {
                msg += LanguageResource.Language.LblTitleCannotBlank + "!\r\n";
            }
            if (tboxSql.Text.Trim().Length==0)
            {
                msg += "命令不能为空!\r\n";
            }
            return msg;
        }

    }
}
