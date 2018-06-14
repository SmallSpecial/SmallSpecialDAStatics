using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;
namespace GSSServer.GSSManage
{
    public partial class FormRoleAdd : Form
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        public FormRoleAdd(int id)
        {
            _id = id;
            InitializeComponent();
        }


        private void FormRoleAdd_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void buttonESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn(ex);
                MessageBox.Show("信息:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            BindPower();
            if (_id != 0)
            {
                this.Text = "角色修改";
                GSSBLL.Roles bll = new GSSBLL.Roles();
                GSSModel.Roles model = bll.GetModel(_id);
                f_RoleIDTextBox.Text = model.F_RoleID.ToString();
                f_RoleNameTextBox.Text = model.F_RoleName;
                f_IsUsedCheckBox.Checked = model.F_IsUsed;

                SetTreeValue(null, model.F_Power);
            }

        }
        /// <summary>
        /// 设置树的值
        /// </summary>
        /// <returns></returns>
        private void SetTreeValue(TreeNode pnode, string power)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode node in (pnode == null ? TreeView1.Nodes : pnode.Nodes))
            {
                if (node.Name=="工单处理")
                {
                    int d=0;
                }
                if (power.IndexOf(","+node.Tag.ToString()+",") >= 0)
                {
                    TreeView1.SetTreeNodeCheckBoxChecked(node, true);
                }
                else
                {
                    TreeView1.SetTreeNodeCheckBoxChecked(node, false);
                }
                if (node.Nodes.Count > 0)
                {
                    SetTreeValue(node,power);
                }
            }
        }
        /// <summary>
        /// 得到树的值
        /// </summary>
        /// <returns></returns>
        private string GetTreeValue(TreeNode pnode)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode node in (pnode == null ? TreeView1.Nodes : pnode.Nodes))
            {
                if (node.Checked&&node.Parent!=null)
                {
                    sb.AppendFormat(",{0}", node.Tag);
                }
                if (node.Nodes.Count > 0)
                {
                    sb.Append(GetTreeValue(node)); ;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            //验证数据项
            string msg = "";
            if (f_RoleNameTextBox.Text.Trim().Length==0)
            {
                msg += "角色名不能为空!\r\n";
            }
            if (msg.Length>0)
            {
                MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            //数据准备
            GSSModel.Roles model = new GSSModel.Roles();
            model.F_RoleName = f_RoleNameTextBox.Text;
            model.F_IsUsed = f_IsUsedCheckBox.Checked;
            model.F_Power = GetTreeValue(null);
            model.F_Power += ",";


            //数据提交
            GSSBLL.Roles bll = new GSSBLL.Roles();
            bool isok = false;
            if (_id != 0)
            {
                model.F_RoleID = int.Parse(f_RoleIDTextBox.Text);
                isok = bll.Update(model);
            }
            else
            {
                int num = bll.Add(model);
                if (num > 0)
                {
                    isok = true;
                }
            }
            if (isok)
            {
                MessageBox.Show("数据保存成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("数据保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void BindPower()
        {
            InitTreeView(null, 1);
            for (int i = 0; i < TreeView1.Nodes.Count; i++)
            {
                TreeView1.Nodes[i].Expand();
            }

        }
        private void InitTreeView(TreeNode panode, int parenid)
        {
            GSSBLL.Menus bll = new GSSBLL.Menus();
            List<GSSModel.Menus> list = bll.GetModelList("F_ParentID=" + parenid + " and F_IsUsed=1 ");
            TreeNode node = null;
            foreach (GSSModel.Menus model in list)
            {
                if (panode==null)
                {
                    node = TreeView1.AddTreeNode(TreeView1.Nodes, model.F_Name, false);
                }
                else
                {
                    node = TreeView1.AddTreeNode(panode.Nodes, model.F_Name, false);
                }
               
                node.Tag = model.F_MenuID;
                InitTreeView(node, model.F_MenuID);
            }
        }
    }
}
