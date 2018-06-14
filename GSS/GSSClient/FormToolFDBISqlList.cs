using System;
using System.Data;
using System.Windows.Forms;

namespace GSSClient
{
    public partial class FormToolFDBISqlList : GSSUI.AForm.FormMain
    {
        //变量
        GSSBLL.FDBISql bll = ClientRemoting.FDBISql();
        string _sql = "";
        public string sql
        {
            get { return this._sql; }
            set { this._sql = value; }
        }

        public FormToolFDBISqlList()
        {
            InitializeComponent();
        }

        private void FormToolFDBISqlList_Load(object sender, EventArgs e)
        {
            formInit();
        }
        /// <summary>
        /// 窗体数据初始化
        /// </summary>
        private void formInit()
        {
            dataGridViewUISql.AutoGenerateColumns = false;
            DataSet ds = bll.GetList(Convert.ToInt16(ShareData.UserID));
            dataGridViewUISql.DataSource = ds.Tables[0];
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormToolFDBISqlAdd form = new FormToolFDBISqlAdd(0);
            DialogResult dresult = form.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                formInit();
            }
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewUISql.SelectedRows.Count == 0)
            {
                GSSUI.MsgBox.Show("请选择要修改的数据!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dataGridViewUISql.SelectedRows[0].Cells[0].Value);
            FormToolFDBISqlAdd form = new FormToolFDBISqlAdd(id);
            DialogResult dresult = form.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                formInit();
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (GSSUI.MsgBox.Show("确定要删除选中的数据吗?", LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            int iSelectRowCount = dataGridViewUISql.SelectedRows.Count;

            //判断是否选择了行
            if (iSelectRowCount > 0)
            {
                //循环删除行
                foreach (DataGridViewRow dgvRow in dataGridViewUISql.SelectedRows)
                {

                    if (bll.Delete(Convert.ToInt32(dgvRow.Cells[0].Value)))
                    {
                        dataGridViewUISql.Rows.Remove(dgvRow);
                    }
                }


            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            formInit();
        }

        private void dataGridViewUISql_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUISql.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            _sql = dataGridViewUISql.SelectedRows[0].Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridViewUISql_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string colname = dataGridViewUISql.Columns[e.ColumnIndex].HeaderText;
            switch (colname)
            {
                case "属性":
                    e.Value = e.Value.ToString() == "-1" ? "公开" : "私人";
                    break;
            }
        }


    }
}
