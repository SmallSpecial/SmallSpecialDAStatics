using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSSServer.GSSManage
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            formInit();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormUserAdd form = new FormUserAdd(0);
            form.ShowDialog();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count==0)
            {
                MessageBox.Show("请选择要修改的数据!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FormUserAdd form = new FormUserAdd(id);
            form.ShowDialog();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除选中的数据吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            int iSelectRowCount = dataGridView1.SelectedRows.Count;

                //判断是否选择了行
                if (iSelectRowCount > 0)
                {
                    //循环删除行
                    foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
                    {
                        GSSBLL.Users bll = new GSSBLL.Users();
                        if (bll.Delete(Convert.ToInt32(dgvRow.Cells[0].Value)))
                        {
                            dataGridView1.Rows.Remove(dgvRow);
                        }
                         
                        
                    }
                }

        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            formInit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FormUserAdd form = new FormUserAdd(id);
            form.ShowDialog();
        }

        /// <summary>
        /// 窗体数据初始化
        /// </summary>
        private void formInit()
        {
            dataGridView1.AutoGenerateColumns = false;
            GSSBLL.Users bll = new GSSBLL.Users();
            DataSet ds = bll.GetAllList();
            dataGridView1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 格式化显示
        /// </summary>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            GSSServerLibrary.DBHandle gs = new GSSServerLibrary.DBHandle();
            string colname = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            switch (colname)
            {
                case "部门":
                    e.Value = gs.GetDeptName(e.Value.ToString());
                    break;
                case "角色":
                    e.Value = gs.GetRoleName(e.Value.ToString());
                    break;
            }
        }




        //private void t_UsersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    if (e.Exception != null &&e.Context == DataGridViewDataErrorContexts.Commit)
        //    {
        //        e.ThrowException = false;

        //        MessageBox.Show(e.Exception.Message, "提示消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        e.Cancel = true;
        //    }


        //    ////t_UsersDataGridView.SelectedRows[0].Selected = false;
        //    ////t_UsersDataGridView.Rows[t_UsersDataGridView.Rows.Count - 1].Selected = true;


        //    //e.Cancel = true;
        //    //this.dataSetGSS.AcceptChanges();

        //    //dataSetGSS.T_Users.Rows[t_UsersDataGridView.CurrentCell.RowIndex].SetColumnError(dataSetGSS.T_Users.Columns[t_UsersDataGridView.CurrentCell.ColumnIndex], e.Exception.Message);
        //    //this.BindingContext[dataSetGSS.T_Users].Position = e.RowIndex;
        //}

        //private void t_UsersDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    // Validate the CompanyName entry by disallowing empty strings.
        //    //if (t_UsersDataGridView.Columns[e.ColumnIndex].Name == "用户名DataGridViewTextBoxColumn")
        //    //{
        //    //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
        //    //    {
        //    //        t_UsersDataGridView.Rows[e.RowIndex].ErrorText =
        //    //            "Company Name must not be empty";
        //    //        e.Cancel = true;
        //    //    }
        //    //}

        //}
    }
}
