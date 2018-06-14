using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GSSUI
{
    [DefaultEvent("Click")]
    public partial class ButtonUI : UserControl
    {
        #region 变量

        //三种不同状态下的图片
        Image _normalImage = null;
        Image _moveImage = null;
        Image _downImage = null;

        #endregion

        #region 属性

        [Category("QLFSkinDll")]
        public Image NormalImage
        {
            get
            {
                return _normalImage;

            }
            set
            {
                _normalImage = value;
            }
        }

        [Category("QLFSkinDll")]
        public Image DownImage
        {
            get { return _downImage; }
            set
            {
                _downImage = value;
            }
        }

        [Category("QLFSkinDll")]
        public Image MoveImage
        {
            get { return _moveImage; }
            set
            {
                _moveImage = value;
            }
        }

        [Category("QLFSkinDll")]
        public string Caption
        {
            get { return this.label1.Text; }   //控件运行时会自动运行get方法得到值
            set
            {
                this.label1.Text = value;
            }
        }

        #endregion

        #region 构造函数

        public ButtonUI()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            //默认的是自带的图片样式，如果使用该按钮则必须手工指定当前按钮你想要的背景图片
            _normalImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"GSSUI.GSSRes.button.btnnomal.bmp"));
            _moveImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"GSSUI.GSSRes.button.btnfore.bmp"));
            _downImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"GSSUI.GSSRes.button.btndown.bmp"));
            MakeTransparent(_normalImage);
            MakeTransparent(_moveImage);
            MakeTransparent(_downImage);
            InitializeComponent();
            this.BackgroundImage = _normalImage;
        }

        #endregion

        #region 辅助函数

        private void MakeTransparent(Image image)
        {
            Bitmap bitmap = image as Bitmap;
            bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
        }

        #endregion

        #region 事件

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = _moveImage;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = _downImage;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = _normalImage;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = _normalImage;
        }


        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        #endregion

    }
}
