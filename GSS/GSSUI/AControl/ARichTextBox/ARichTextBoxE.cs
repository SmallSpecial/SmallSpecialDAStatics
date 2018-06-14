using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GSSUI.AControl.ARichTextBox
{
    public partial class ARichTextBoxE : UserControl
    {
        public ARichTextBoxE()
        {
            InitializeComponent();
            aRichTextBox1.SelectionIndent = 20;
        }


        #region 属性
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("自定义属性"), Description("RTF文本")]
        public string RTF
        {
            get { return this.aRichTextBox1.Rtf; }
            set
            {
                this.aRichTextBox1.Rtf = value;
                this.Invalidate();
            }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("自定义属性"), Description("文本")]
        public override string Text
        {
            get { return this.aRichTextBox1.Text; }
            set
            {
                this.aRichTextBox1.Text = value;
                this.Invalidate();
            }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("自定义属性"), Description("文本")]
        public string Texta
        {
            get { return this.aRichTextBox1.Text; }
            set
            {
                this.aRichTextBox1.Text = value;
                this.Invalidate();
            }
        }

        [CategoryAttribute("自定义属性"), Description("选中开始位置")]
        public int SelectionStart
        {
            get { return this.aRichTextBox1.SelectionStart; }
            set
            {
                this.aRichTextBox1.SelectionStart = value;
                this.Invalidate();
            }
        }

        [CategoryAttribute("自定义属性"), Description("数据行..")]
        public string[] Lines
        {
            get { return this.aRichTextBox1.Lines; }
            set
            {
                this.aRichTextBox1.Lines = value;
                this.Invalidate();
            }
        }

        #endregion

        private void showLineNo()
        {

            //获得当前坐标信息

            Point p = this.aRichTextBox1.Location;
            // Point p = new Point(0, 0); 

            int crntFirstIndex = this.aRichTextBox1.GetCharIndexFromPosition(p);

            int crntFirstLine = this.aRichTextBox1.GetLineFromCharIndex(crntFirstIndex);

            Point crntFirstPos = this.aRichTextBox1.GetPositionFromCharIndex(crntFirstIndex);

            //
            p.Y += this.aRichTextBox1.Height;
            //
            int crntLastIndex = this.aRichTextBox1.GetCharIndexFromPosition(p);

            int crntLastLine = this.aRichTextBox1.GetLineFromCharIndex(crntLastIndex);

            Point crntLastPos = this.aRichTextBox1.GetPositionFromCharIndex(crntLastIndex);
            //

            //准备画图

            Graphics g = this.panel1.CreateGraphics();

            Font font = new Font(this.aRichTextBox1.Font, this.aRichTextBox1.Font.Style);

            SolidBrush brush = new SolidBrush(Color.Green);

            //

            //画图开始

            //刷新画布

            Rectangle rect = this.panel1.ClientRectangle;

            brush.Color = this.panel1.BackColor;

            g.FillRectangle(brush, 0, 0, this.panel1.ClientRectangle.Width, this.panel1.ClientRectangle.Height);

            brush.Color = Color.Green;//重置画笔颜色

            //

            //绘制行号

            int lineSpace = 0;

            if (crntFirstLine != crntLastLine)
            {

                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);

            }
            else
            {
                lineSpace = Convert.ToInt32(this.aRichTextBox1.Font.Size);
            }

            int brushX = this.panel1.ClientRectangle.Width - Convert.ToInt32(font.Size * 2);

            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);


            string astr = aRichTextBox1.Text;
            int pi = astr.Substring(0, aRichTextBox1.GetFirstCharIndexFromLine(crntLastLine)).Replace("\\", "").Split('\n').Length + 1;


            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {
                int Indexa = aRichTextBox1.GetFirstCharIndexFromLine(i);
                int Indexb = aRichTextBox1.GetFirstCharIndexFromLine(i - 1 < 0 ? 0 : i - 1);
                string astr1 = "\n";
                if (Indexa - Indexb > 0)
                {
                    astr1 = astr.Substring(Indexb, Indexa - Indexb);
                }

                string DrawStr = "";
                if (astr1.IndexOf('\n') >= 0)
                {
                    pi--;
                    DrawStr = pi.ToString();
                }

                g.DrawString(DrawStr, font, brush, brushX, brushY);

                brushY -= lineSpace;
            }

            g.Dispose();
            font.Dispose();
            brush.Dispose();

        }
        public delegate void TextChangedkEventHandler(object sender, EventArgs e);
        public event TextChangedkEventHandler TextChangeds;
        private void aRichTextBox1_TextChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
            if (TextChangeds != null) TextChangeds(this, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            showLineNo();
        }

        private void aRichTextBox1_VScroll(object sender, EventArgs e)
        {
            panel1.Invalidate();
           
        }
        public void Undo()
        {
            aRichTextBox1.Undo();
        }

    }
}
