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
    public partial class FormToolGRoleDepotPSW : GSSUI.AForm.FormMain
    {
        private Tasks _model;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        public FormToolGRoleDepotPSW(Tasks model)
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


                int bigzoneid = Convert.ToInt32(ClientCache.GetBigZoneGameID(_model.F_GameBigZone));
                string bigzoneCFID = ClientCache.GetGameConfigID(_model.F_GameBigZone);
                int zoneid = Convert.ToInt32(ClientCache.GetZoneGameID(bigzoneCFID, _model.F_GameZone));
                string sql = string.Format("update OPENQUERY ([LKSV] ,'select * from T_Role_Base where F_ID={0} and F_UserID={1}')  set F_DepotPass='0'", _model.F_GRoleID, _model.F_GUserID);
                GSSBLL.Tasks bll = ClientRemoting.Tasks();
                int result = bll.GSSTool_CustomExec(bigzoneid, zoneid, 6, sql);

                string info = "";
                if (result != 0)
                    info = "操作执行成功";
                else
                    info = "此角色已经不存在";


                GSSModel.Tasks task = new GSSModel.Tasks();
                task.F_ID = _model.F_ID;
                task.F_EditMan = int.Parse(ShareData.UserID);
                task.F_EditTime = DateTime.Now;
                task.F_TToolUsed = true;
                task.F_TUseData = string.Format("角色二级密码清空工具 \n{0} \n{1}", lblUR.Text,info);
                task.F_Note = rtboxNote.Text;
                _isToolUsed = true;
                bll.Edit(task);

                MsgBox.Show(info, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (result != 0)
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
