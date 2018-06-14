using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using GSSUI.AClass;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GSSUI.AControl.APictrueBox
{
    public partial class APictrueBox : PictureBox
    {
        private State state = State.Normal;//设置按钮初始状态为默认状态
        private Bitmap _NormalImg = null;//做为按钮图像
        private Bitmap _OverImg = null;//做为按钮图像
        private Bitmap _NowImg = null;
        Graphics g;
        Brush LineartBrush;

        //枚举按钮的状态
        public enum State
        {
            Normal = 1,//按钮默认时
            MouseOver = 4,//鼠标移上按钮时
            MouseDown = 2,//鼠标按下按钮时
            Disable = 3,//当不启用按钮时（也就是按钮属性Enabled==Ture时）
            Default = 5//控件得到Tab焦点时
        }

        [CategoryAttribute("自定义属性"), Description("按钮正常图片")]
        public Bitmap NormalImg
        {
            get { return this._NormalImg; }
            set
            {
                _NormalImg = value;
                this.Invalidate();
            }
        }
        [CategoryAttribute("自定义属性"), Description("按钮正常图片")]
        public Bitmap OverImg
        {
            get { return this._OverImg; }
            set
            {
                _OverImg = value;
                this.Invalidate();
            }
        }

        public APictrueBox()
           
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
            this.SetStyle(ControlStyles.Selectable, true);
            InitializeComponent();
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.BackgroundImage = _NormalImg;

        }


        public APictrueBox(IContainer container)
        {
        }


        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

           
            try
            {

                if ( state == State.MouseOver&&_OverImg == null)
                {
                    base.InvokePaintBackground(this, new PaintEventArgs(e.Graphics, base.ClientRectangle));
                    ImageDrawRect.DrawRect(e.Graphics,NormalImg, e.ClipRectangle,  1, 1);
                    LineartBrush = new LinearGradientBrush(
                            new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height),
                            Color.FromArgb(230, Color.White), Color.FromArgb(250, Color.White), 90);
                    g.FillRectangle(LineartBrush, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
                    ImageDrawRect.DrawRect(g, ImageObject.GetResBitmap("GSSUI.ASkinImg.ButtonImg.Botton2.png"), e.ClipRectangle, Rectangle.FromLTRB(10, 10, 10, 10), 1, 5);

                }
            }
            catch
            { }
        }
        protected override void OnCreateControl()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = _NormalImg;
            base.OnCreateControl();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            state = State.MouseOver;
            this.Image = _OverImg;
            this.Width += 2;
            this.Height += 2;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            state = State.Normal;
            this.Image = _NormalImg;
            this.Width -= 2;
            this.Height -= 2;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

    }
}
