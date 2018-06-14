using System;
using System.Windows.Forms;
using GSSModel;
using GSSUI;

namespace GSSClient
{
    public partial class FormToolGUROnlineClear : GSSUI.AForm.FormMain
    {
        private Tasks _model;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        public FormToolGUROnlineClear(Tasks model)
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


                int userid = Convert.ToInt32(_model.F_GUserID);


                if (userid.ToString().Length==0)
                {
                    MsgBox.Show("用户都不能为空!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                GSSServerLibrary.ServerRemoteLib remote =  ClientRemoting.ServerRemoteLib();
                string resultStr = remote.UserRoleClearOnline("寻龙记", _model.F_GameBigZone, userid);

                GSSBLL.Tasks bll = ClientRemoting.Tasks();
                GSSModel.Tasks task = new GSSModel.Tasks();
                task.F_ID = _model.F_ID;
                task.F_EditMan = int.Parse(ShareData.UserID);
                task.F_EditTime = DateTime.Now;
                task.F_TToolUsed = true;
                task.F_TUseData = string.Format("帐号/角色清除在线状态工具 \n{0} \n{1}", lblUR.Text, resultStr);
                task.F_Note = rtboxNote.Text;
                _isToolUsed = true;
                bll.Edit(task);

                MsgBox.Show(resultStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultStr.IndexOf("成功")!=-1)
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
