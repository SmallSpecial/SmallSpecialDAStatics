﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;


namespace GSSGameWeb
{
    /// <summary>
    /// 验证码图片类
    /// </summary>
    public class VerifyImage
    {
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private static Matrix m = new Matrix();
        private static Bitmap charbmp = new Bitmap(40, 40);

        private static Font[] fonts = {
                                        new Font(new FontFamily("Times New Roman"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Georgia"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Arial"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Comic Sans MS"), 16 + Next(3), FontStyle.Regular)
                                     };
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0)
                value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }

        #region IVerifyImage 成员

        public VerifyImageInfo GenerateImage(string code, int width, int height, Color bgcolor, int textcolor)
        {
            VerifyImageInfo verifyimage = new VerifyImageInfo();
            verifyimage.ImageFormat = ImageFormat.Jpeg;
            verifyimage.ContentType = "image/pjpeg";

            // 直接指定尺寸, 而不使用外部参数中的建议尺寸
            width = 100;
            height = 32;

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.Clear(bgcolor);

            int fixedNumber = textcolor == 2 ? 60 : 0;
            Pen linePen = new Pen(Color.FromArgb(Next(50) + fixedNumber, Next(50) + fixedNumber, Next(50) + fixedNumber), 1);

            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(Next(100), Next(100), Next(100)));
            for (int i = 0; i < 3; i++)
            {
                g.DrawArc(linePen, Next(20) - 6, Next(20) - 6, Next(width) + 6, Next(height) + 6, Next(-100, 100), Next(-200, 200));
            }

            Graphics charg = Graphics.FromImage(charbmp);

            float charx = -18;
            for (int i = 0; i < code.Length; i++)
            {
                m.Reset();
                m.RotateAt(Next(50) - 25, new PointF(Next(3) + 7, Next(3) + 7));

                charg.Clear(Color.Transparent);
                charg.Transform = m;
                //定义前景色为黑色
                drawBrush.Color = Color.Black;

                charx = charx + 18 + Next(5);
                PointF drawPoint = new PointF(charx, 2.0F);
                charg.DrawString(code[i].ToString(), fonts[Next(fonts.Length - 1)], drawBrush, new PointF(0, 0));

                charg.ResetTransform();

                g.DrawImage(charbmp, drawPoint);
            }


            drawBrush.Dispose();
            g.Dispose();
            charg.Dispose();

            verifyimage.Image = bitmap;

            return verifyimage;
        }

        #endregion
    }



    /// <summary>
    /// 验证码图片信息
    /// </summary>
    public class VerifyImageInfo
    {
        private Bitmap image;
        private string contentType = "image/pjpeg";
        private ImageFormat imageFormat = ImageFormat.Jpeg;

        /// <summary>
        /// 生成出的图片
        /// </summary>
        public Bitmap Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// 输出的图片类型，如 image/pjpeg
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// 图片的格式
        /// </summary>
        public ImageFormat ImageFormat
        {
            get { return imageFormat; }
            set { imageFormat = value; }
        }
    }
}
