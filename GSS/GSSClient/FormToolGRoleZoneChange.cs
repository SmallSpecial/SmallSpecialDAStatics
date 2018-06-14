using System;
using System.Windows.Forms;
using GSSModel;
using GSSUI;
using System.Data;

namespace GSSClient
{
    public partial class FormToolGRoleZoneChange : GSSUI.AForm.FormMain
    {
        private Tasks _model;
        /// <summary>
        /// 是否提交过工具使用
        /// </summary>
        private bool _isToolUsed = false;

        public FormToolGRoleZoneChange(Tasks model)
        {
            InitializeComponent();
            _model = model;
            this.Text= LanguageResource.Language.BtnRoleChangeZone;
        }

        private void FormToolGRoleRecover_Load(object sender, EventArgs e)
        {
            lblUR.Text = string.Format("帐号:{0} 角色:{1}", _model.F_GUserName, _model.F_GRoleName);
            BindDicComb(cboxZone, _model.F_GameBigZone);
        }

        /// <summary>
        /// 绑定COMBOX字典控件
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="parentid"></param>
        private void BindDicComb(ComboBox cb, string bigZoneName)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_GameConfig"].Clone();
                //DataRow dra = dtdic.NewRow();
                //dra["F_Name"] = "全部类型";
                //dra["F_ValueGame"] = "0";
                //dtdic.Rows.Add(dra);

                DataRow[] drdic = ds.Tables["T_GameConfig"].Select("F_ParentID="+SystemConfig.BigZoneParentId+" and F_Name='"+bigZoneName+"'");
                if (drdic.Length>0)
                {
                    drdic = ds.Tables["T_GameConfig"].Select("F_ParentID=" + drdic[0]["F_ID"] + "");
                    foreach (DataRow dr in drdic)
                    {
                        dtdic.ImportRow(dr);
                    }
                    cb.DataSource = dtdic;
                    cb.DisplayMember = "F_Name";
                    cb.ValueMember = "F_ValueGame";
                }
                
                cb.SelectedIndex = 0;
            }
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
            MsgBox.Show("角色改服功能暂停开发!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;

            //if (rtboxNote.Text.Trim().Length == 0)
            //{
            //    MsgBox.Show("工具使用备注不能为空!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //try
            //{
            //    ComitDoControl(false);

            //    int zoneid = Convert.ToInt32(cboxZone.SelectedValue);
            //    int userid = Convert.ToInt32(_model.F_GUserID);
            //    int roleid = Convert.ToInt32(_model.F_GRoleID);
            //    GSSServerLibrary.ServerRemoteLib remote = ClientRemoting.ServerRemoteLib();
            //    string resultStr = remote.RoleZoneChange("寻龙记", _model.F_GameBigZone,userid, roleid, zoneid);

            //    GSSBLL.Tasks bll = ClientRemoting.Tasks();
            //    GSSModel.Tasks task = new GSSModel.Tasks();
            //    task.F_ID = _model.F_ID;
            //    task.F_EditMan = int.Parse(ShareData.UserID);
            //    task.F_EditTime = DateTime.Now;
            //    task.F_TToolUsed = true;
            //    task.F_TUseData = string.Format("角色改服工具 \n{0} \n新服:{1} \n{2}", lblUR.Text, cboxZone.Text, resultStr);
            //    task.F_Note = rtboxNote.Text;
            //    _isToolUsed = true;
            //    bll.Edit(task);

            //    MsgBox.Show(resultStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    if (resultStr.IndexOf("成功")!=-1)
            //    {
            //        this.Close();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    ShareData.Log.Warn(ex);
            //    MsgBox.Show(ex.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //finally
            //{
            //    ComitDoControl(true);
            //}
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
