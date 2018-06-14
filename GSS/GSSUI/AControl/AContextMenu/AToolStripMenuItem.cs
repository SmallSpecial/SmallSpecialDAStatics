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
    public partial class AToolStripMenuItem : ToolStripMenuItem
    {
        public AToolStripMenuItem()
        {
            InitializeComponent();
        }

        public AToolStripMenuItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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
            //DrawBorder(e.Graphics, new Rectangle(Convert.ToInt16(e.Graphics.VisibleClipBounds.Location.X), Convert.ToInt16(e.Graphics.VisibleClipBounds.Location.Y), Convert.ToInt16(e.Graphics.VisibleClipBounds.Width), Convert.ToInt16(e.Graphics.VisibleClipBounds.Height)));

            

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            using (Pen p = new Pen(Color.FromArgb(149, 208, 249)))
            {
               this.Parent.CreateGraphics().DrawRectangle(p,
                    new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            }
        }
    }
}
