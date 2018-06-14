using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GSSUI.AClass;

namespace GSSUI.AForm
{
    public partial class FormMainSkin : ABaseForm
    {
        Graphics g;
        Brush LineartBrush;
        private Bitmap _ShadeImage = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.1.png");
        private Bitmap _LogoImage = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.logoXlJ.png");
        private FormSkin _skinForm;
        /// <summary>
        /// skin按钮
        /// </summary>
        Bitmap s_bmp = null;

        public FormMainSkin()
            : base()
        {
            InitializeComponent();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            LineartBrush = new LinearGradientBrush(
                            new Rectangle(6, 37, ClientRectangle.Width - 12, 75),
                            Color.FromArgb(130, Color.White), Color.FromArgb(190, Color.White), 90);
            g.FillRectangle(LineartBrush, new Rectangle(6, 37, ClientRectangle.Width - 12, 75));

            LineartBrush = new LinearGradientBrush(
                            new Rectangle(6, 112, ClientRectangle.Width - 12, 25),
                            Color.FromArgb(190, Color.White), Color.FromArgb(230, Color.White), 90);
            g.FillRectangle(LineartBrush, new Rectangle(6, 112, ClientRectangle.Width - 12, 25));

            LineartBrush = new LinearGradientBrush(
                            new Rectangle(6, 137, ClientRectangle.Width - 12, ClientRectangle.Height - 174),
                            Color.FromArgb(230, Color.White), Color.FromArgb(240, Color.White), 90);
            g.FillRectangle(LineartBrush, new Rectangle(6, 137, ClientRectangle.Width - 12, ClientRectangle.Height - 174));

            //if (this.WindowState==FormWindowState.Maximized)
            //{
            //ImageDrawRect.DrawRect(g, _ShadeImage, new Rectangle(this.Width - _ShadeImage.Width - 26, 47, _ShadeImage.Width, _ShadeImage.Height), Rectangle.FromLTRB(0, 0, 0, 0), 1, 1);
            //}
            if (this.WindowState == FormWindowState.Maximized)
            {
                ImageDrawRect.DrawRect(g, _LogoImage, new Rectangle(this.Width - _LogoImage.Width + 40, 15, 80, 136), 1, 1);

            }
            else
            {
                g.DrawImage(_LogoImage, new Rectangle(this.Width - _LogoImage.Width + 55, 22, 60, 102), new Rectangle(0, 0, _LogoImage.Width, _LogoImage.Height), GraphicsUnit.Pixel);
            }
            LineartBrush.Dispose();
        }


        private void picBoxSkin_Click(object sender, EventArgs e)
        {
           // Win32.ReleaseCapture();
            if (_skinForm == null || _skinForm.IsDisposed)
            {
                _skinForm = new FormSkin();
                _skinForm.StartPosition = FormStartPosition.Manual;
                int s_x = this.Left + this.picBoxSkin.Left;
                int s_y = this.Top + SystemInformation.CaptionHeight;
                if (s_x + _skinForm.Width > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    s_x = Screen.PrimaryScreen.WorkingArea.Width - _skinForm.Width;
                }
                _skinForm.Location = new Point(s_x, s_y);
                _skinForm.Owner = this;
                _skinForm.Show();
                _skinForm.BringToFront();

            }
            else
            {
                _skinForm.Hide();
                _skinForm.Close();
            }
        }

        private void FormMain1_Load(object sender, EventArgs e)
        {

            picBoxSkin.Visible = true;
            picBoxSkin.Left = this.Width - 115;

        }



        private void picBoxSkin_MouseEnter(object sender, EventArgs e)
        {
            if (_skinForm == null)
                s_bmp = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.All_iconbutton_highlightBackground.png");
            s_bmp = (Bitmap)ImageObject.ProcImage(s_bmp, SharData.BackColor);
            picBoxSkin.Image = s_bmp;
        }

        private void picBoxSkin_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(Properties.Resources.colour, new Rectangle(0, 1, 18, 18), 0, 0, 18, 18, GraphicsUnit.Pixel);
        }

        //重载WndProc方法
        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {

                    //case Win32.WM_SYSCOMMAND:

                    //    base.WndProc(ref m);
                    //    if (m.WParam == (IntPtr)Win32.SC_RESTORE && this.WindowState != FormWindowState.Normal && this.Location.X<0)
                    //    {
                    //        this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
                    //    }
                        
                    //    break;

                    case Win32.WM_NCLBUTTONDOWN:
                        if (_skinForm != null)
                        {
                            _skinForm.Hide();
                            _skinForm.Close();
                        }
                        base.WndProc(ref m);
                        break;
                    case Win32.WM_LBUTTONDOWN:
                        if (_skinForm != null)
                        {
                            _skinForm.Hide();
                            _skinForm.Close();
                        }
                        base.WndProc(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch { }

        }

        private void picBoxSkin_MouseLeave(object sender, EventArgs e)
        {
            picBoxSkin.Image = null;
            picBoxSkin.BackColor = Color.Transparent;
        }
    }
}
