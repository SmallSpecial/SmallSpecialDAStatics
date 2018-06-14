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
    public partial class ABaseForm : Form
    {
        #region 声明
        private Bitmap _BacklightImg;//窗体光泽背景图片
        private Rectangle _BacklightLTRB;//窗体光泽重绘边界
        private int _RgnRadius = 4;//设置窗口圆角
        private int Rgn;
        private Graphics g;
        private bool _IsResize = true;//是否允许改变窗口大小
        private bool _UseIcon = true;//是否使用ICON
        private bool _UseFadeStyle = false;//是否使用渐变


        private FormSystemBtn _FormSystemBtnSet = FormSystemBtn.SystemAll;
        private Bitmap btn_closeImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.btn_close.png");
        private Bitmap btn_maxImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.btn_max.png");
        private Bitmap btn_miniImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.btn_mini.png");
        private Bitmap btn_restoreImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.btn_restore.png");
        System.Windows.Forms.Timer timer;
        /// <summary>
        /// 正在使用的渐变量透明度
        /// </summary>
        private double targetOpacity;

        /// <summary>
        /// 渐变使用时间
        /// </summary>
        private double fadeTime = .35;

        /// <summary>
        /// 得到焦点后变成的透明度
        /// </summary>
        private double activeOpacity = 0.999;

        /// <summary>
        /// 失去焦点时变成的透明度
        /// </summary>
        private double inactiveOpacity = .85;

        /// <summary>
        /// 渐变最小量透明度
        /// </summary>
        private double minimizedOpacity = 0;

        /// <summary>
        /// 窗体消息,用来保存渐变过程变量.
        /// </summary>
        private Message heldMessage = new Message();

        /// <summary>
        /// 正在使用的渐变量透明度
        /// </summary>
        public double TargetOpacity
        {
            set
            {
                if (value > SharData.Opacity / 100)
                {
                    targetOpacity = SharData.Opacity / 100;
                }
                else { targetOpacity = value;}
                
                if (_UseFadeStyle && timer != null && !timer.Enabled) timer.Start();
            }
            get { return targetOpacity; }
        }

        bool MustOpacity = false;
        /// <summary>
        /// 正在使用的渐变量透明度(强制)
        /// </summary>
        public double TargetOpacityMust
        {
            set
            {
                MustOpacity = true;
                if (value > SharData.Opacity / 100)
                {
                    targetOpacity = SharData.Opacity / 100;
                }
                else { targetOpacity = value; }

                if (timer != null && !timer.Enabled) timer.Start();
            }
            get { return targetOpacity; }
        }
        /// <summary>
        /// 渐变时间
        /// </summary>
        public double FadeTime
        {
            set
            {
                fadeTime = value;
            }
            get { return fadeTime; }
        }
        //枚举系统按钮状态
        public enum FormSystemBtn
        {
            SystemAll = 0,
            SystemNo = 1,
            btn_close = 2,
            btn_miniAndbtn_close = 3,
            btn_maxAndbtn_close = 4
        }
        #endregion

        #region 构造函数
        public ABaseForm()
        {
            
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);//自绘
            this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.Opaque, true);//如果为真，控件将绘制为不透明，不绘制背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //透明效果



            this.BackColor = GSSUI.SharData.BackColor;
            this.Opacity = GSSUI.SharData.Opacity / 100;
            if (GSSUI.SharData.BackImage != null&&this.BackgroundImage==null)
            {
                this.BackgroundImage = GSSUI.SharData.BackImage;
            }
            if (SharData.TopMost)
            {
                this.TopMost = true;
            }

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            SystemBtnSet();
            timer = new System.Windows.Forms.Timer();
            //this.SuspendLayout();
            this.timer.Interval = 25;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            this.Load += new System.EventHandler(this.FadeForm_Load);
        }

        #endregion

        #region 属性

        [DefaultValue(4)]
        [CategoryAttribute("自定义窗口属性"), Description("设置窗口圆角半径")]
        public int RgnRadius
        {
            get { return this._RgnRadius; }
            set
            {
                _RgnRadius = value;
                this.Invalidate();
            }

        }

        [CategoryAttribute("自定义窗口属性"), Description("设置窗体光泽背景")]
        public Bitmap BacklightImg
        {

            get { return this._BacklightImg; }
            set
            {
                _BacklightImg = value;
                this.Invalidate();
            }
        }

        [CategoryAttribute("自定义窗口属性"), Description("设置窗体光泽背景重绘边界，例如 10,10,10,10")]
        public Rectangle BacklightLTRB
        {

            get { return this._BacklightLTRB; }
            set
            {
                _BacklightLTRB = value;
                if (_BacklightLTRB != Rectangle.Empty)
                {
                    this.Invalidate();
                }
            }
        }

        [DefaultValue(true)]
        [CategoryAttribute("自定义窗口属性"), Description("是否允许改变窗口大小")]
        public bool IsResize
        {
            get { return this._IsResize; }
            set { _IsResize = value; }
        }

        [CategoryAttribute("自定义窗口属性"), Description("系统按钮设置")]
        public FormSystemBtn FormSystemBtnSet
        {
            get
            {
                return _FormSystemBtnSet;
            }
            set
            {
                _FormSystemBtnSet = value;
                this.Invalidate();

            }
        }

        [DefaultValue(true)]
        [CategoryAttribute("自定义窗口属性"), Description("是否使用ICON图标")]
        public bool UseIcon
        {
            get { return this._UseIcon; }
            set { _UseIcon = value; }
        }

        [DefaultValue(false)]
        [CategoryAttribute("自定义窗口属性"), Description("是否使用FADE,设置使用后部分需要设置OCACITY为0.999才会在任务栏显示")]
        public bool UseFadeStyle
        {
            get { return this._UseFadeStyle; }
            set { _UseFadeStyle = value; }
        }

        #endregion

        #region 重写方法
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {

            SystemBtnSet();
            base.OnInvalidated(e);

        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetReion();
        }

        //重绘窗口
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality; //高质量
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                ImageDrawRect.DrawRect(g, _BacklightImg, ClientRectangle, Rectangle.FromLTRB(_BacklightLTRB.X, _BacklightLTRB.Y, _BacklightLTRB.Width, _BacklightLTRB.Height), 1, 1);
                if (this.Icon != null && _UseIcon)
                    g.DrawIcon(this.Icon, new Rectangle(12, 12, 16, 16));
                if (this.Text.Length > 0)
                    g.DrawString(this.Text, this.Font, new SolidBrush(Color.White), 32, 14);
            }
            catch
            { }
        }
        //重载WndProc方法
        protected override void WndProc(ref Message m)
        {
            try
            {
                //渐变相关
                if (_UseFadeStyle&&m.Msg!=Win32.WM_NCPAINT)
                {
                    if (m.Msg == Win32.WM_SYSCOMMAND || m.Msg == Win32.WM_COMMAND)
                    {
                        //渐变到0,当最小化时
                        if (m.WParam == (IntPtr)Win32.SC_MINIMIZE)
                        {
                            if (heldMessage.WParam != (IntPtr)Win32.SC_MINIMIZE)
                            {
                                heldMessage = m;
                                TargetOpacity = minimizedOpacity;
                            }
                            else
                            {
                                heldMessage = new Message();
                                TargetOpacity = activeOpacity;
                            }
                            return;
                        }
                        //当任务栏还原时渐变
                        else if (m.WParam == (IntPtr)Win32.SC_RESTORE
                            && this.WindowState == FormWindowState.Minimized)
                        {
                            base.WndProc(ref m);
                            TargetOpacity = activeOpacity;
                            return;
                        }

                        //当窗体关闭时.
                        else if (m.WParam == (IntPtr)Win32.SC_CLOSE)
                        {
                            heldMessage = m;
                            TargetOpacity = minimizedOpacity;
                            return;
                        }
                    }
                }



                switch (m.Msg)
                {
                    //窗体客户区以外的重绘消息,一般是由系统负责处理
                    case Win32.WM_NCPAINT:
                        break;
                    // 在需要计算窗口客户区的大小和位置时发送。通过处理这个消息，应用程序可以在窗口大小或位置改变时控制客户区的内容
                    //case Win32.WM_NCCALCSIZE:
                    //    break;
                    // 画窗体被激活或者没有被激活时的样子
                    case Win32.WM_NCACTIVATE:
                        if (m.WParam == (IntPtr)Win32.WM_FALSE)
                        {
                            m.Result = (IntPtr)Win32.WM_TRUE;
                        }
                        break;
                    case Win32.WM_ACTIVATE:
                        TaskMenu.AddSYSMENU(this);
                        base.WndProc(ref m);
                        break;
                    case Win32.WM_NCLBUTTONDBLCLK:
                        if (_IsResize)
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    //鼠标移动,按下或释放都会执行该消息
                    case Win32.WM_NCHITTEST:
                        WM_NCHITTEST(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch (Exception Exception)
            {
                
            }

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.Style = cp.Style | Win32.WS_CLIPCHILDREN | Win32.WS_SYSMENU | Win32.WS_MINIMIZEBOX;
                //cp.Style = cp.Style |Win32.WS_SYSMENU;
                cp.Style = cp.Style | Win32.WS_CLIPCHILDREN | Win32.WS_MINIMIZEBOX | Win32.WS_MAXIMIZEBOX | Win32.SC_SIZE;
                cp.ClassStyle = cp.ClassStyle | Win32.CS_DBLCLKS;
                return cp;
            }
        }


        #endregion

        #region 方法
        protected void SystemBtnSet()
        {
            switch ((int)_FormSystemBtnSet)
            {
                case 0:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_restore.BackImg = btn_restoreImg;
                    if (WindowState == FormWindowState.Normal)
                    {
                        btn_max.Location = new Point(this.Width - 68, 6);
                        btn_restore.Location = new Point(this.Width - 68, -20);
                    }
                    else
                    {
                        btn_max.Location = new Point(this.Width - 68, -20);
                        btn_restore.Location = new Point(this.Width - 68, 6);
                    }
                    break;
                case 1:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, -20);
                    btn_max.BackImg = btn_maxImg;
                    btn_max.Location = new Point(this.Width - 68, -20);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, -20);
                    btn_restore.BackImg = btn_restoreImg;
                    btn_restore.Location = new Point(this.Width - 68, -20);
                    break;
                case 2:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_max.Location = new Point(this.Width - 68, -20);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, -20);
                    btn_restore.BackImg = btn_restoreImg;
                    btn_restore.Location = new Point(this.Width - 68, -20);
                    break;
                case 3:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_max.Location = new Point(this.Width - 68, -20);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 68, 6);
                    btn_restore.BackImg = btn_restoreImg;
                    btn_restore.Location = new Point(this.Width - 68, -20);
                    break;
                case 4:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, -20);
                    btn_max.BackImg = btn_maxImg;
                    btn_restore.BackImg = btn_restoreImg;
                    if (WindowState == FormWindowState.Normal)
                    {
                        btn_max.Location = new Point(this.Width - 68, 6);
                        btn_restore.Location = new Point(this.Width - 68, -20);
                    }
                    else
                    {
                        btn_max.Location = new Point(this.Width - 68, -20);
                        btn_restore.Location = new Point(this.Width - 68, 6);
                    }
                    break;

            }

        }
        /// <summary>
        /// 给窗口圆角
        /// </summary>
        protected void SetReion()
        {
            Rgn = Win32.CreateRoundRectRgn(5, 5, ClientRectangle.Width - 4, ClientRectangle.Height - 4, _RgnRadius, _RgnRadius);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
        }
        private void WM_NCHITTEST(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point point = new Point(Win32.LOWORD(wparam), Win32.HIWORD(wparam));
            point = PointToClient(point);
            if (m.Msg != Win32.WM_NCHITTEST)
            {
                return;
            }


            if (_IsResize)
            {
                TaskMenu.DeleteSYSMENU(this);

                if (point.X <= 8)
                {
                    if (point.Y <= 8)
                        m.Result = (IntPtr)Win32.HTTOPLEFT;
                    else if (point.Y > Height - 8)
                        m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                    else
                        m.Result = (IntPtr)Win32.HTLEFT;
                }
                else if (point.X >= Width - 8)
                {
                    if (point.Y <= 8)
                        m.Result = (IntPtr)Win32.HTTOPRIGHT;
                    else if (point.Y >= Height - 8)
                        m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                    else
                        m.Result = (IntPtr)Win32.HTRIGHT;
                }
                else if (point.Y <= 8)
                {
                    m.Result = (IntPtr)Win32.HTTOP;
                }
                else if (point.Y >= Height - 8)
                    m.Result = (IntPtr)Win32.HTBOTTOM;
                else
                {
                    TaskMenu.AddSYSMENU(this);
                    m.Result = (IntPtr)Win32.HTCAPTION;
                }
                //this.Invalidate();
            }
            else
            {

                m.Result = (IntPtr)Win32.HTCAPTION;
            }

        }
        #endregion

        #region 事件
        private void btn_close_Click(object sender, EventArgs e)
        {
            if (_UseFadeStyle)
            {
                Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_CLOSE, 0);
            }
            else
            {
                this.Close();
            }
            
        }
        private void btn_mini_Click(object sender, EventArgs e)
        {
            if (_UseFadeStyle||MustOpacity)
            {
                Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            if (_UseFadeStyle)
            {
                Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!_UseFadeStyle&&!MustOpacity)
            {
                return;
            }
            double fadeChangePerTick = timer.Interval * 1.0 / 1000 / fadeTime;

            //检查是否需要停止TIMER
            if (Math.Abs(targetOpacity - this.Opacity) < fadeChangePerTick)
            {
                //如果透明度是1的话,会有黑背景
                if (targetOpacity == 1) this.Opacity = 0.999;
                else this.Opacity = targetOpacity;
                //进程用消息
                base.WndProc(ref heldMessage);
                heldMessage = new Message();
                //停止TIMER,保存状态
                if (MustOpacity)
                {
                    MustOpacity = false;
                    if (this.Opacity > 0.95)
                    {
                        this.Opacity = 1;
                        this.Invalidate();
                    }
                }

                timer.Stop();
            }
            else if (targetOpacity > this.Opacity) this.Opacity += fadeChangePerTick;
            else if (targetOpacity < this.Opacity) this.Opacity -= fadeChangePerTick;
        }
  
        private void FadeForm_Load(object sender, EventArgs e)
        {
            if (_UseFadeStyle)
            {
                this.Opacity = minimizedOpacity;
                TargetOpacity = activeOpacity;
            }
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }
        #endregion



    }
}
