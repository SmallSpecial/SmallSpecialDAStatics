using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GSSUI.Colors;
using System.Drawing.Drawing2D;

namespace GSSUI.AControl.ACorlorControl
{
    public partial class ColorSliderUserControl : UserControl
    {
        private Bitmap _colorBitmap;
        private double _h;
        private double _l;
        public ColorSliderUserControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.Selectable |
                    ControlStyles.AllPaintingInWmPaint,
                    true);
        }

        public void SetHueSaturation(double h, double s,double l)
        {
            _h = h;
            _l = l;
            _colorBitmap = drawColorBitmap();
            Invalidate();

          //  notifyValueChanged();
        }

        public void SetColorSaturation(double s)
        {
            double lw = (100 - s) / 100 * this.Width;
            int wp = (int)lw;
            arrowControl.Location = new Point(wp, -1);
            _colorBitmap = drawColorBitmap();
            Invalidate();

            //  notifyValueChanged();
        }

        public void GetHueSaturation(out double s)
        {
            s =100.0-(double)(arrowControl.Location.X ) / (double)this.Width * 100;
        }

        /// <summary>
        /// Occurs when a value has been changed.
        /// 发生在一个值已更改
        /// </summary>
        public event EventHandler ValueChangedByUser;

        /// <summary>
        /// Occurs when a value has been changed.
        /// </summary>
        public event EventHandler ValueChanged;

        private void NotifyValueChangedByUser()
        {
            if (ValueChangedByUser != null)
            {
                ValueChangedByUser(this, EventArgs.Empty);
            }
        }

        private void notifyValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Draws the color bitmap.
        /// </summary>
        /// <returns></returns>
        private Bitmap drawColorBitmap()
        {
            const int width = 100;
            const int height = 5;
            double h = _h;
            double l = _l;

            var bmp = new Bitmap(width, height);

            for (int x = 0; x < width; ++x)
            {
                double s =width-x;
                Color color = new HslColor(h, s, l).ToColor();
                for (int y = 0; y < height; ++y)
                {
                    bmp.SetPixel(x, y, color);
                }               
               
            }
            return bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_colorBitmap == null)
            {
                _colorBitmap = drawColorBitmap();
            }

            double facXBmpToScreen = (double)_colorBitmap.Width / ClientSize.Width;
            double facYBmpToScreen = (double)_colorBitmap.Height / ClientSize.Height;

            var sourceRect = new Rectangle(
                (int)(facXBmpToScreen * ClientRectangle.Left),
                (int)(facYBmpToScreen * ClientRectangle.Top),
                (int)(facXBmpToScreen * ClientRectangle.Width),
                (int)(facYBmpToScreen * ClientRectangle.Height));

            //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(
                _colorBitmap,
                ClientRectangle,
                sourceRect,
                GraphicsUnit.Pixel);

        }
        /// <summary>
        /// 重新定位的箭头。
        /// </summary>
        /// <param name="offsetY">The offset X.</param>
        private void repositionArrow(int offsetX)
        {
            offsetX = Math.Max(0, offsetX);
            offsetX = Math.Min(ClientSize.Width - 1, offsetX);

            arrowControl.Location = new Point(
                offsetX - (arrowControl.Width / 2), arrowControl.Location.Y);
        }

        private void arrowControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                repositionArrow(PointToClient(arrowControl.PointToScreen(e.Location)).X);
                notifyValueChanged();
                NotifyValueChangedByUser();
            }

        }

        private void arrowControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                repositionArrow(PointToClient(arrowControl.PointToScreen(e.Location)).X);
                notifyValueChanged();
                NotifyValueChangedByUser();
            }
        }

        private void ColorSliderUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                repositionArrow(PointToClient(this.PointToScreen(e.Location)).X);
                notifyValueChanged();
                NotifyValueChangedByUser();
            }
        }

        private void ColorSliderUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                repositionArrow(PointToClient(this.PointToScreen(e.Location)).X);
                notifyValueChanged();
                NotifyValueChangedByUser();
                Invalidate();
            }
        }

    }
}
