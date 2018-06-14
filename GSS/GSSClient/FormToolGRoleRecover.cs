using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI;
using GSSModel;

namespace GSSClient
{
    public partial class FormToolGRoleRecover : GSSUI.AForm.FormMain
    {
        private Tasks _model;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        public FormToolGRoleRecover(Tasks model)
        {
            InitializeComponent();
            _model = model;
        }

        private void FormToolGRoleRecover_Load(object sender, EventArgs e)
        {
            lblUR.Text = string.Format("帐号:{0} 角色:{1}", _model.F_GUserName, _model.F_GRoleName);
        }

        /// <summary>
        ///  提交时禁用按键,返回结果后启用
        /// </summary>
        /// <param name="isback"></param>
        private void ComitDoControl(bool value)
        {

            btnDosure.Enabled = value;
            btnDoesc.Enabled = value;
        }

        private void btnDosure_Click(object sender, EventArgs e)
        {
            if (rtboxNote.Text.Trim().Length == 0)
            {
                MsgBox.Show("工具使用备注不能为空!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ComitDoControl(false);
                GSSModel.Tasks model = new GSSModel.Tasks();
                model.F_GRoleID = _model.F_GRoleID;
                model.F_GUserID = _model.F_GUserID;
                model.F_GameBigZone = ClientCache.GetBigZoneGameID(_model.F_GameBigZone);
                string bigzoneCFID = ClientCache.GetGameConfigID(_model.F_GameBigZone);
                model.F_GameZone = ClientCache.GetZoneGameID(bigzoneCFID, _model.F_GameZone);

                GSSBLL.Tasks bll = ClientRemoting.Tasks();
                int codeResult = bll.GSSTool_RoleRecover(model);

                string info = "";
                if (codeResult == 0)
                    info = "操作执行成功";
                else if (codeResult == 1801)
                    info = "用户在该战区下已经有3个角色";
                else if (codeResult == 1800)
                    info = "删除表中无此角色";
                else info = "操作执行失败";

                GSSModel.Tasks task = new GSSModel.Tasks();
                task.F_ID = _model.F_ID;
                task.F_EditMan = int.Parse(ShareData.UserID);
                task.F_EditTime = DateTime.Now;
                task.F_TToolUsed = true;
                task.F_TUseData = string.Format("角色恢复工具 \n{0} \n{1}", lblUR.Text,info);
                task.F_Note = rtboxNote.Text;
                _isToolUsed = true;
                bll.Edit(task);

                MsgBox.Show(info, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (codeResult==0)
                {
                    this.Close();
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Warn(ex);
                MsgBox.Show(ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ComitDoControl(true);
            }
        }

        private void btnDoesc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormToolGUserNoUse_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isToolUsed)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
