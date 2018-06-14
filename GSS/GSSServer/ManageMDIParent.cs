using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSSServer
{
    public partial class ManageMDIParent : Form
    {
        private int childFormNumber = 0;

        public ManageMDIParent()
        {
            InitializeComponent();
        }
        private void ManageMDIParent_Load(object sender, EventArgs e)
        {
            GSSManage.FormUsersOnline form = new GSSManage.FormUsersOnline();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }





        private void 菜单管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormMenus")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormMenus form = new GSSManage.FormMenus();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 游戏配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormGameConfig")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormGameConfig form = new GSSManage.FormGameConfig();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 字典表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormDictionary")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormDictionary form = new GSSManage.FormDictionary();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 数据库备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormDBBackUp")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormDBBackUp form = new GSSManage.FormDBBackUp();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 在线用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormUsersOnline")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormUsersOnline form = new GSSManage.FormUsersOnline();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxGSS form = new AboutBoxGSS();
            form.ShowDialog();
        }



        private void 用户管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormUsers")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormUsers form = new GSSManage.FormUsers();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 部门管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormDepartment")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormDepartment form = new GSSManage.FormDepartment();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 角色管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormRoles")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormRoles form = new GSSManage.FormRoles();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 日志管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            bool newshow = true;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == "FormLog")
                {
                    newshow = false;
                    childForm.Show();
                    childForm.Activate();
                }
            }
            if (newshow)
            {
                GSSManage.FormLog form = new GSSManage.FormLog();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void 同步战区战线信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.同步战区战线信息ToolStripMenuItem.Enabled = false;
            string back=WebServiceLib.SynGameZoneLine();
            if (back=="true")
            {
                MessageBox.Show("同步战区战线信息完成");
            }
            else
            {
                MessageBox.Show(back);
            }
            this.同步战区战线信息ToolStripMenuItem.Enabled = true;
        }


    }
}
