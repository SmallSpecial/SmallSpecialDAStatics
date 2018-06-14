using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GSSUI.Colors;
using System.Threading;

namespace GSSUI.AControl.ACorlorControl
{
    public partial class ColorAreaUserControl : UserControl
    {
        private double _h;
        private double _l;
        private static double _s=100;
        private Bitmap _colorBitmap;

        public ColorAreaUserControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer |
                    ControlStyles.ResizeRedraw|
                    ControlStyles.Selectable |
                    ControlStyles.AllPaintingInWmPaint,
                    true);
        }

        //public void SetHueSaturation(double h, double l)
        //{  
        //    _h = h;
        //    _l = l;
        //    Invalidate();

        //   // notifyHueSaturationChanged();
        //}


        public void SetHueSaturation(double s)
        {
            _s = s;
            Calculate();
            //ThreadStart threadStart = new ThreadStart(Calculate);
            //Thread thread = new Thread(threadStart);
            //thread.Start();

            drawCaret();
            notifyHueSaturationChanged();
        }
        public void Calculate()
        {
            _colorBitmap = drawColorBitmap();
        } 



        public void SetColor(double h,double s,double l)
        {
            _h = h;
            _l = l;
            _s = s;
            _colorBitmap = drawColorBitmap();
            drawCaret();
           // notifyValueChangedByUser();
            notifyHueSaturationChanged(); ;
        }

        public void GetHueSaturation(out double h,out double s, out double l)
        {
            h = _h;
            l = _l;
            s = _s;
        }


        /// <summary>
        /// Occurs when the user changed the hue and/or saturation.
        /// 当用户发生改变色调和/或饱和度 
        /// </summary>
        public event EventHandler HueSaturationChanged;

        /// <summary>
        /// Occurs when a value has been changed.
        /// 值更改时发生事件
        /// </summary>
        public event EventHandler ValueChangedByUser;

        private void notifyHueSaturationChanged()
        {
            if (HueSaturationChanged != null)
            {
                HueSaturationChanged(this, EventArgs.Empty);
            }
        }

        private void notifyValueChangedByUser()
        {
            if (ValueChangedByUser != null)
            {
                ValueChangedByUser(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="e"></param>
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
           drawCaret(e.Graphics);
        }
        
        /// <summary>
        /// 绘制位图的颜色
        /// </summary>
        /// <returns></returns>
        private static Bitmap drawColorBitmap()
        {
            const int width = 360;
            const int height = 100;

            var bmp = new Bitmap(width, height);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    double h = x;
                    double s = _s;//饱和度
                    double l = 100 - y;

                    Color color = new HslColor(h, s, l).ToColor();

                    bmp.SetPixel(x, y, color);
                }
            }

            return bmp;
        }

        /// <summary>
        /// 绘制移动圆点
        /// </summary>
        /// <param name="g">The g.</param>
        private void drawCaret(
            Graphics g)
        {

            Point p;
            translateHueSaturationToCaretPosition(out p, _h, _l);
            Image colorslider_dragbackground = (Image)GSSUI.AClass.ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.colorslider_dragbackground.png");
            g.DrawImage(colorslider_dragbackground, new Rectangle(new Point(p.X - 6, p.Y - 6), new Size(12, 12)));            
        }

        private void drawCaret()
        {
            Invalidate();
        }

        /// <summary>
        /// 返回指定的区域内,移动圆点的位置
        /// </summary>
        /// <param name="caretPosition"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        private void translateHueSaturationToCaretPosition( out Point caretPosition, double h, double l)
        {
            double facXBmpToScreen = 360.0 / ClientSize.Width;
            double facYBmpToScreen = 100.0 / ClientSize.Height;

            h = Math.Max(0.0, h);
            h = Math.Min(360.0, h);
            l = Math.Max(0.0, l);
            l = Math.Min(100.0, l);

            double pX = (h / facXBmpToScreen);
            double pY = (l / facYBmpToScreen);

            pX = Math.Max(0, pX);
            pX = Math.Min(ClientSize.Width - 1, pX);
            pY = Math.Max(0, pY);
            pY = Math.Min(ClientSize.Height - 1, pY);

            pY = ClientSize.Height - pY;

            caretPosition = new Point((int)pX, (int)pY);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="caretPosition"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        private void translateCaretPositionToHueSaturation( Point caretPosition,out double h, out double l)
        {
            double facXBmpToScreen = 360.0 / ClientSize.Width;
            double facYBmpToScreen = 100.0 / ClientSize.Height;

            Point p = caretPosition;

            p.X = Math.Max(0, p.X);
            p.X = Math.Min(ClientSize.Width - 1, p.X);
            p.Y = Math.Max(0, p.Y);
            p.Y = Math.Min(ClientSize.Height - 1, p.Y);

            p.Y = ClientSize.Height - p.Y;

            h = (p.X * facXBmpToScreen);
            l = (p.Y * facYBmpToScreen);

            h = Math.Max(0.0, h);
            h = Math.Min(360.0, h);
            l = Math.Max(0.0, l);
            l = Math.Min(100.0, l);
        }

        private void ColorAreaUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                translateCaretPositionToHueSaturation(e.Location, out _h, out _l);
                drawCaret();

                notifyValueChangedByUser();
                notifyHueSaturationChanged();
            }
        }

        private void ColorAreaUserControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                translateCaretPositionToHueSaturation(e.Location, out _h, out _l);
                drawCaret();

                notifyValueChangedByUser();
                notifyHueSaturationChanged();
            }
        }

        private void ColorAreaUserControl_MouseDown(object sender, MouseEventArgs e)
        {

			if ( e.Button == MouseButtons.Left )
			{
				translateCaretPositionToHueSaturation( e.Location, out _h, out _l );
				drawCaret();

				notifyValueChangedByUser();
				notifyHueSaturationChanged();
                //MessageBox.Show(_arvlue.ToString());
			}
        }

        private void ColorAreaUserControl_MouseLeave(object sender, EventArgs e)
        {
            drawCaret();
        }
      
    }
}
