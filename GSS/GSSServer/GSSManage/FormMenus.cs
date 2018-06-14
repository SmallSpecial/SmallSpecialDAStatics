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
    public partial class FormMenus : Form
    {
        public FormMenus()
        {
            InitializeComponent();
        }

        private void FormMenus_Load(object sender, EventArgs e)
        {
            formInit();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormMenuAdd form = new FormMenuAdd(0);
            form.ShowDialog();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要修改的数据!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FormMenuAdd form = new FormMenuAdd(id);
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
                    GSSBLL.Menus bll = new GSSBLL.Menus();
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
            FormMenuAdd form = new FormMenuAdd(id);
            form.ShowDialog();
        }
        /// <summary>
        /// 窗体数据初始化
        /// </summary>
        private void formInit()
        {
            dataGridView1.AutoGenerateColumns = false;
            GSSBLL.Menus bll = new GSSBLL.Menus();
            DataSet ds = bll.GetAllList();
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
