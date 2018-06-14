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
    public partial class FormLog : Form
    {
        public FormLog()
        {
            InitializeComponent();
        }

        private void FormLog_Load(object sender, EventArgs e)
        {
            toolStripComboBoxType.SelectedIndex = 0;
            formInit("");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string filterStr = "";
            string whereStr = toolStripTextBoxSearchText.Text.Trim();
            if (whereStr.Length>0)
            {

                switch (toolStripComboBoxType.SelectedItem.ToString())
                {
                    case "用户名":
                        filterStr = "F_UserName=" + whereStr;
                        break;
                    case "操作记录":
                        filterStr = "F_Note like '%" + whereStr + "%'";
                        break;
                    case "相关数据":
                        filterStr = "F_Data like '%" + whereStr + "%'";
                        break;
                }
            }
            formInit(filterStr);
        }
        /// <summary>
        /// 窗体数据初始化
        /// </summary>
        private void formInit(string where)
        {
            dataGridView1.AutoGenerateColumns = false;
            GSSBLL.SysLog bll = new GSSBLL.SysLog();
            DataSet ds = bll.GetList(where);
            dataGridView1.DataSource = ds.Tables[0];
        }


    }
}
