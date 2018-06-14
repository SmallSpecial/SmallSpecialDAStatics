using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using GSSUI.AClass;
using System.Drawing.Drawing2D;

namespace GSSUI.AControl.AContextMenu
{
    public partial class AContextMenu : ContextMenuStrip
    {
        private Graphics g = null;
        private Bitmap Bmp = null;

        public AContextMenu()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public AContextMenu(IContainer container)
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            container.Add(this);
            this.BackColor = Color.White;

            InitializeComponent();
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            g = e.Graphics;


            Color backimgcorlor = Color.Red;
            
            //// 绘制背景色      
            //if (this.Parent.BackColor != null)
            //{
            //    backimgcorlor = this.FindForm().BackColor;
            //}

            //if (this.Parent.BackgroundImage != null)
            //{
            //    backimgcorlor = ((Bitmap)this.FindForm().BackgroundImage).GetPixel(10, 10);
            //}
            //backimgcorlor = Color.FromArgb(240, backimgcorlor);


            Bmp = AClass.ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.menuEx_background.bmp");
            g.DrawImage(Bmp, new Rectangle(0, 0, 28, 5), 0, 0, 28, 5, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(28, 0, this.Width - 33, 5), 29, 0, Bmp.Width - 33, 5, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 5), Bmp.Width - 5, 0, 5, 5, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(0, 5, 28, this.Height - 10), 0, 5, 28, Bmp.Height - 10, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(28, 5, this.Width - 33, this.Height - 10), 29, 5, Bmp.Width - 33, Bmp.Height - 10, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 5, 5, this.Height - 10), Bmp.Width - 5, 5, 5, Bmp.Height - 10, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(0, this.Height - 5, 28, 5), 0, Bmp.Height - 5, 28, 5, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(28, this.Height - 5, this.Width - 33, 5), 29, Bmp.Height - 5, Bmp.Width - 33, 5, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 5, 5, 5), Bmp.Width - 5, Bmp.Height - 5, 5, 5, GraphicsUnit.Pixel);


            //using (System.Drawing.SolidBrush backbrush = new System.Drawing.SolidBrush(backimgcorlor))
            //{
            //    Rectangle border = e.ClipRectangle;
            //    border.Width -= 2;
            //    border.Height -= 2;
            //    Rectangle rt = new Rectangle(border.Location.X, border.Location.Y, border.Width, border.Height);

            //    //填充绘制效果        
            //    g.FillRectangle(backbrush, rt);
            //}
            //            StringFormat drawFormat = new StringFormat();
            //drawFormat.FormatFlags = StringFormatFlags.NoClip;
            //e.Graphics.DrawString("北京", new Font("DotumChe", 16F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Red), new PointF(-5, -5),drawFormat);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            //Bmp = AClass.ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.menuEx_background.bmp");
            //g.DrawImage(Bmp, new Rectangle(0, 0, 28, 5), 0, 0, 28, 5, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(28, 0, this.Width - 33, 5), 29, 0, Bmp.Width - 33, 5, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 5), Bmp.Width - 5, 0, 5, 5, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(0, 5, 28, this.Height - 10), 0, 5, 28, Bmp.Height - 10, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(28, 5, this.Width - 33, this.Height - 10), 29, 5, Bmp.Width - 33, Bmp.Height - 10, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.Width - 5, 5, 5, this.Height - 10), Bmp.Width - 5, 5, 5, Bmp.Height - 10, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(0, this.Height - 5, 28, 5), 0, Bmp.Height - 5, 28, 5, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(28, this.Height - 5, this.Width - 33, 5), 29, Bmp.Height - 5, Bmp.Width - 33, 5, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 5, 5, 5), Bmp.Width - 5, Bmp.Height - 5, 5, 5, GraphicsUnit.Pixel);
            DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height));
            //DrawBorder(e.Graphics, new Rectangle(Convert.ToInt16(e.Graphics.VisibleClipBounds.Location.X), Convert.ToInt16(e.Graphics.VisibleClipBounds.Location.Y), Convert.ToInt16(e.Graphics.VisibleClipBounds.Width), Convert.ToInt16(e.Graphics.VisibleClipBounds.Height)));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
           // DrawBorder(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
            //using (System.Drawing.SolidBrush backbrush=new System.Drawing.SolidBrush(Color.Red))
            //{
                
            //    this.CreateGraphics().FillRectangle(backbrush, 0, 0, this.Width - 1, this.Height - 1);
            //}
        }


        //protected override void    OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        //{

        //    if (e.ToolStrip is ToolStripDropDownMenu)
        //    {
        //        #region Draw Rectangled Border

        //        DrawVistaMenuBorder(e.Graphics,
        //            new Rectangle(Point.Empty, e.ToolStrip.Size));

        //        #endregion
        //    }
        //    else
        //    {
        //        #region Draw Rounded Border
        //        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        //        using (GraphicsPath path = GetToolStripRectangle(e.ToolStrip))
        //        {
        //            using (Pen p = new Pen(Color.Red))
        //            {
        //                e.Graphics.DrawPath(p, path);
        //            }
        //        }
        //        #endregion
        //    }


        //}

        /// <summary>
        /// Draws the border of the vista menu window
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        private void DrawBorder(Graphics g, Rectangle r)
        {
            using (Pen p = new Pen(Color.FromArgb(149,208,249)))
            {
                g.DrawRectangle(p,
                    new Rectangle(r.Left, r.Top, r.Width-1, r.Height-1));
            }
        }
        /// <summary>
        /// Gets a rounded rectangle representing the hole area of the toolstrip
        /// </summary>
        /// <param name="toolStrip"></param>
        /// <returns></returns>
        private GraphicsPath GetToolStripRectangle(ToolStrip toolStrip)
        {
            return CreateRoundRectangle(
                new Rectangle(0, 0, toolStrip.Width - 1, toolStrip.Height - 1), 3);
        }

        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;

            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.CloseFigure();

            return path;
        }
    }
}
