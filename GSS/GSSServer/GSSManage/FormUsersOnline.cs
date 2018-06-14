using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GSSCSFrameWork;

namespace GSSServer.GSSManage
{
    public partial class FormUsersOnline : Form
    {
        public FormUsersOnline()
        {
            InitializeComponent();
        }



        private void FormUsersOnline_Load(object sender, EventArgs e)
        {
            formInit();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            formInit();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value==null)
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

        /// <summary>
        /// 窗体数据初始化
        /// </summary>
        private void formInit()
        {
            string online = "0";
            if (ServerHandle.TcpSvr.SessionTable != null)
            {
                //foreach (Session Client in ServerHandle.TcpSvr.SessionTable.Values)
                //{
                //    if (Client != null && Client.UserID != null)
                //    {
                //        online += "," + Client.UserID.ToString();
                //    }
                //}

                List<object> clients = new List<object>(ServerHandle.TcpSvr.dic.Values);
                foreach (object client in clients)
                {
                    Session Client = (Session)client;
                    if (Client != null && Client.UserID != null)
                    {
                        online += "," + Client.UserID.ToString();
                    }
                }
            }
            dataGridView1.AutoGenerateColumns = false;
            GSSBLL.Users bll = new GSSBLL.Users();
            DataSet ds = bll.GetList("F_UserID in (" + online + ")");
            dataGridView1.DataSource = ds.Tables[0];
            groupBox2.Text = "在线用户(" + dataGridView1.Rows.Count + ")";
        }

    }
}
