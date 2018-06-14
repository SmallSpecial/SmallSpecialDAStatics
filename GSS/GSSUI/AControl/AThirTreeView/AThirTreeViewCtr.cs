using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace GSSUI.AControl.AThirTreeView
{
    //public partial class AThirTreeViewCtr : Component
    //{
    //    //public AThirTreeViewCtr()
    //    //{
    //    //    InitializeComponent();
    //    //}

    //    //public AThirTreeViewCtr(IContainer container)
    //    //{
    //    //    container.Add(this);

    //    //    InitializeComponent();
    //    //}
        
    //}

    public partial class AThirTreeViewCtr : TriStateTreeView
    {
        #region Protected methods
        override protected void OnCheckBoxStateChanged(CheckBoxStateChangedEventArgs args)
        {
            EventHandler<CheckBoxStateChangedEventArgs> handler = CheckBoxStateChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// The imagelist for node state
        /// </summary>
        [
        Browsable(true),
        CategoryAttribute("Behavior"),
        Description("The ImageList control from which node checkbox state images are token")
        ]
        override public ImageList CheckBoxStateImageList
        {
            set
            {
                _ImageListSent = false;
                _CtrlStateImageList = value;
            }

            get
            {
                if (_CtrlStateImageList == null)
                {
                    _CtrlStateImageList = ctlStateImageList;
                }
                return _CtrlStateImageList;
            }
        }
        #endregion

        #region Events
        [
        Browsable(true),
        CategoryAttribute("Property Changed"),
        Description("Occurs when the checkbox state of node changed")
        ]
        public event EventHandler<CheckBoxStateChangedEventArgs> CheckBoxStateChanged;
        #endregion

        public AThirTreeViewCtr()
        {
            InitializeComponent();
            this.CheckBoxes = true;
        }

        public void InitNodes()
        {
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                InitDict(this.Nodes[i]);
            }
        }

        private void InitDict(TreeNode treeNode)
        {
            CheckBoxState ckstate = CheckBoxState.Unchecked;
            if (!_NodeStateDict.ContainsKey(treeNode))
            {
                ckstate = treeNode.Checked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
               // _NodeStateDict.Add(treeNode, ckstate);
                this.SetTreeNodeState(treeNode, ckstate);
                if (treeNode.Parent!=null)
                {
                    SetParentTreeNodeStateRecursively(treeNode.Parent);
                }
                
            }

            if (treeNode.Nodes.Count > 0)
            {
                for (int i = 0; i < treeNode.Nodes.Count; i++)
                {
                    InitDict(treeNode.Nodes[i]);
                }
            }    
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InitNodes();
        }

        #region Public Methods

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string text, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, text, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, TreeNode node, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, node, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageIndex, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, int selectedImageIndex, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageIndex, selectedImageIndex, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageKey, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, string selectedImageKey, bool checkboxChecked)
        {
            CheckBoxState checkboxState = checkboxChecked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageKey, selectedImageKey, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string text)
        {
            return AddTreeNode(nodes, text, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, TreeNode node)
        {
            return AddTreeNode(nodes, node, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex)
        {
            return AddTreeNode(nodes, key, text, imageIndex, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, int selectedImageIndex)
        {
            return AddTreeNode(nodes, key, text, imageIndex, selectedImageIndex, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey)
        {
            return AddTreeNode(nodes, key, text, imageKey, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, string selectedImageKey)
        {
            return AddTreeNode(nodes, key, text, imageKey, selectedImageKey, false);
        }

        public void SetTreeNodeStateC(TreeNode treeNode, CheckBoxState checkboxState)
        {
            SetTreeNodeState(treeNode, checkboxState);
            //SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
        }

        /// <summary>
        /// Get Tree Node CheckBox State
        /// </summary>
        /// <param name="treeNode">node</param>
        /// <returns>CheckBox State</returns>
        override public CheckBoxState GetTreeNodeCheckBoxState(TreeNode treeNode)
        {
            //if (!_NodeStateDict.ContainsKey(treeNode))
            //{
            //    CheckBoxState ckstate = treeNode.Checked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            //    _NodeStateDict.Add(treeNode, ckstate);
            //}
            return GetNodeState(treeNode);
        }

        /// <summary>
        /// Set Tree Node CheckBox State
        /// </summary>
        /// <param name="treeNode">node</param>
        /// <param name="checkboxChecked">CheckBox State</param>
        /// <returns>New CheckBox State</returns>
        public CheckBoxState SetTreeNodeCheckBoxChecked(TreeNode treeNode, bool checkboxChecked)
        {
            CheckBoxState checkboxState = GetTreeNodeCheckBoxState(treeNode);
            bool done = false;

            switch (checkboxState)
            {
                case CheckBoxState.Unchecked:
                    if (checkboxChecked)
                    {
                        done = true;
                    }
                    break;
                case CheckBoxState.Checked:
                case CheckBoxState.Indeterminate:
                    if (!checkboxChecked)
                    {
                        done = true;
                    }
                    break;
            }

            if (done)
            {
                ToggleTreeNodeState(treeNode);
            }

            return GetTreeNodeCheckBoxState(treeNode);
        }

        #endregion

    }

}
