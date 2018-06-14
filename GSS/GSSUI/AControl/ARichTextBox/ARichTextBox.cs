using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GSSUI.AControl.ARichTextBox
{
    public partial class ARichTextBox : RichTextBox
    {
        #region 变量
        //属性
        /// <summary>
        /// 是否使用右键菜单
        /// </summary>
        private bool _UseRightItem = true;
        private bool _UseCutPaste = true;



        //控件
        private AContextMenu.AContextMenu contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        //private System.Windows.Forms.Panel panel1;
        #endregion

        #region 构造

        public ARichTextBox()
        {
            InitializeComponent();
        }

        public ARichTextBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        #endregion

        #region 属性
        [CategoryAttribute("自定义属性"), DefaultValueAttribute(true), Description("是否使用右键菜单")]
        public bool UseRightItem
        {
            get { return this._UseRightItem; }
            set
            {
                _UseRightItem = value;
                this.Invalidate();
            }
        }
        [CategoryAttribute("自定义属性"), DefaultValueAttribute(true), Description("是否使用剪切,粘贴")]
        public bool UseCutPaste
        {
            get { return this._UseCutPaste; }
            set
            {
                _UseCutPaste = value;
                this.Invalidate();
            }
        }
        #endregion

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //if (BackImg == null)
            //{
                base.OnPaint(e);
            //    return;
            //}


            //base.OnPaint(e);
            //int i = (int)state;
            //if (this.Focused && state != State.MouseDown && _IsTabFocus == true) i = 5;
            //if (!this.Enabled) i = 4;
           
            
            //Rectangle rc = this.ClientRectangle;
            //Graphics g = e.Graphics;

            //using (System.Drawing.SolidBrush backbrush = new System.Drawing.SolidBrush(Color.Red))
            //{

            //    this.CreateGraphics().FillRectangle(backbrush, 0, 0, this.Width - 1, this.Height - 1);
            //}

            //base.InvokePaintBackground(this, new PaintEventArgs(e.Graphics, base.ClientRectangle));
                try
                {

            //    Color backimgcorlor = Color.Red;
            //    this.BackColor = backimgcorlor;
            //    //绘制背景色      
            //    if (this.FindForm().BackColor != null)
            //    {
            //        backimgcorlor = this.FindForm().BackColor;
            //    }

            //    if (this.FindForm().BackgroundImage != null)
            //    {
            //        backimgcorlor = ((Bitmap)this.FindForm().BackgroundImage).GetPixel(10, 10);
            //    }
            //    backimgcorlor = Color.FromArgb(240, backimgcorlor);
            //    using (System.Drawing.SolidBrush backbrush = new System.Drawing.SolidBrush(backimgcorlor))
            //    {
            //        Rectangle border = e.ClipRectangle;
            //        border.Width -= 2;
            //        border.Height -= 2;
            //        Rectangle rt = new Rectangle(border.Location.X + 1, border.Location.Y + 1, border.Width, border.Height);

            //        // 填充绘制效果        
            //        e.Graphics.FillRectangle(backbrush, rt);

            //    }


            //    using (System.Drawing.SolidBrush backbrush = new System.Drawing.SolidBrush(Color.Red))
            //    {

            //        this.CreateGraphics().FillRectangle(backbrush, 0, 0, this.Width - 1, this.Height - 1);
            //    }
            //    this.Invalidate();


                //if (BackImg != null)
                //{
                //    if (_BacklightLTRB != Rectangle.Empty)
                //    {

                //        ImageDrawRect.DrawRect(g, BackImg, rc, Rectangle.FromLTRB(_BacklightLTRB.X, _BacklightLTRB.Y, _BacklightLTRB.Width, _BacklightLTRB.Height), i, 5);
                //    }
                //    else
                //    {
                //        ImageDrawRect.DrawRect(g, BackImg, rc, Rectangle.FromLTRB(10, 10, 10, 10), i, 5);
                //    }

                //}
            }
            catch
            { }

        }
        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    Color backimgcorlor = Color.Red;
        //    // 绘制背景色      
        //    if (this.FindForm().BackColor != null)
        //    {
        //        backimgcorlor = this.FindForm().BackColor;
        //    }

        //    if (this.FindForm().BackgroundImage != null)
        //    {
        //        backimgcorlor = ((Bitmap)this.FindForm().BackgroundImage).GetPixel(10, 10);
        //    }
        //    backimgcorlor = Color.FromArgb(240, backimgcorlor);
        //    using (System.Drawing.SolidBrush backbrush = new System.Drawing.SolidBrush(backimgcorlor))
        //    {
        //        Rectangle border = pevent.ClipRectangle;
        //        border.Width -= 2;
        //        border.Height -= 2;
        //        Rectangle rt = new Rectangle(border.Location.X + 1, border.Location.Y + 1, border.Width, border.Height);

        //        //填充绘制效果        
        //        pevent.Graphics.FillRectangle(backbrush, rt);

        //    }
        //   // base.OnPaintBackground(pevent);

        //    this.Invalidate();

        //}
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new AContextMenu.AContextMenu(this.components);
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            //this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.剪切ToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.粘贴toolStripMenuItem,
            //this.删除ToolStripMenuItem,
            this.toolStripSeparator1,
            this.全选ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 136);
            this.contextMenuStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.contextMenuStrip1_Paint);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.剪切ToolStripMenuItem.Text = LanguageResource.Language.BtnCut;
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.复制ToolStripMenuItem.Text = LanguageResource.Language.BtnCopy;
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text =LanguageResource.Language.BtnDelete;
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 粘贴toolStripMenuItem
            // 
            this.粘贴toolStripMenuItem.Name = "粘贴toolStripMenuItem";
            this.粘贴toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.粘贴toolStripMenuItem.Text = LanguageResource.Language.BtnPaste;
            this.粘贴toolStripMenuItem.Click += new System.EventHandler(this.粘贴toolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);

            //// 
            //// panel1
            //// 
            //this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            //this.panel1.Location = new System.Drawing.Point(17, 3);
            //this.panel1.Name = "panel1";
            //this.panel1.Size = new System.Drawing.Size(200, 100);
            //this.panel1.TabIndex = 2;



            this.粘贴toolStripMenuItem.Visible = _UseCutPaste;
            this.剪切ToolStripMenuItem.Visible = _UseCutPaste;

            this.contextMenuStrip1.Visible = _UseRightItem;

           //this.Parent.Controls.Add(this.panel1);

            this.contextMenuStrip1.ResumeLayout(false);
            this.Invalidate();

        }
        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    this.Invalidate();
        //    base.OnMouseEnter(e);
        //}
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _UseRightItem)
            {

                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);

            }
            base.OnMouseClick(e);
        }

        #region 右键菜单

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clipboard.SetText(this.SelectedText);
            this.Copy();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void 粘贴toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeselectAll();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }

        private void contextMenuStrip1_Paint(object sender, PaintEventArgs e)
        {

            if (Clipboard.GetText().Length == 0)
            {
                this.粘贴toolStripMenuItem.Enabled = false;
            }
            else
            {
                this.粘贴toolStripMenuItem.Enabled = true;
            }
            if (this.SelectedText.Length == 0)
            {
                this.复制ToolStripMenuItem.Enabled = false;
                this.剪切ToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.复制ToolStripMenuItem.Enabled = true;
                this.剪切ToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

 

        ////重载WndProc方法
        //protected override void WndProc(ref Message m)
        //{
            
        //    //base.WndProc(ref m);
        //    try
        //    {
        //        switch (m.Msg)
        //        {
        //            case Win32.WM_NCPAINT:
        //                break;
        //            case Win32.WM_PAINT:

        //                PaintEventArgs mepaintevent = new PaintEventArgs(base.CreateGraphics(), base.ClientRectangle);
        //                this.OnPaint(mepaintevent);
        //                break;
        //            default:
        //                base.WndProc(ref m);
        //                break;
        //        }
        //    }
        //    catch { }
        //}

        //protected override void WndProc(ref Message m)
        //{
            
        //    if (m.Msg == Win32.WM_PAINT)
        //    {
        //        WmPaint(ref m);
        //    }
        //    else
        //    {
        //        base.WndProc(ref m);
        //    }
        //}

        //private void WmPaint(ref Message m)
        //{
        //    using (Graphics graphics = Graphics.FromHwnd(base.Handle))
        //    {
        //        if (Text.Length == 0
        //            &&  !Focused)
        //        {
        //            TextFormatFlags format =
        //                TextFormatFlags.EndEllipsis |
        //                TextFormatFlags.VerticalCenter;

        //            if (RightToLeft == RightToLeft.Yes)
        //            {
        //                format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
        //            }

        //            TextRenderer.DrawText(
        //                graphics,
        //                "dddd",
        //                Font,
        //                base.ClientRectangle,
        //               Color.Black,
        //                format);
        //        }
        //        PaintEventArgs pevent = new PaintEventArgs(graphics, this.ClientRectangle);
        //        OnPaintBackground(pevent);

        //    }
        //}


        //private void InsertImage(string name, Bitmap bmp)
        //{
        //    BeginInvoke((MethodInvoker)delegate
        //    {
        //        richTextBox1.AppendText(string.Format("{0} {1}：\r\n", name, DateTime.Now));
        //        bool isread = richTextBox1.ReadOnly;
        //        Clipboard.SetDataObject(bmp, false);
        //        richTextBox1.ReadOnly = false;
        //        if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
        //        {
        //            richTextBox1.Paste();
        //            richTextBox1.AppendText("\r\n");
        //            richTextBox1.ScrollToCaret();
        //        }
        //        richTextBox1.ReadOnly = isread;
        //    });
        //}
    }

}
