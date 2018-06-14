<%@ WebHandler Language="C#" Class="imgChar" %>

using System;
using System.Web;

using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

public class imgChar : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        CreateValidateImage(length);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private const double IMAGELENGTHBASE = 12.5;//图片基本长度
    private const int IMAGEHEIGTH = 22;//图像高
    private const int IMAGELINENUMBER = 25;//干扰线
    private const int IMAGEPOINTNUMBER = 100;//前景噪音点
    public static string VALIDATECODEKEY = "VALIDATECODEKEY";

    private int length = 4;//字符数量
    private string code = string.Empty;//获取验证码

    /// <summary>
    /// 获取或设置验证码长度，默认值为4。
    /// </summary>
    public int Length
    {
        get { return length; }
        set { length = value; }
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    public string Code
    {
        get { return Code; }
    }

    /// <summary>
    /// 创建随机验证码
    /// </summary>
    /// <param name="length">验证码长度</param>
    /// <returns></returns>
    public string CreateCode(int length)
    {
        if (length <= 0) { return string.Empty; }
        ///创建一组随机数，并构成验证码
        Random random = new Random();
        StringBuilder sbCode = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sbCode.Append(random.Next(0, 10));
        }
        ///保存验证码到Session对象中
        code = sbCode.ToString();

        HttpContext.Current.Session[VALIDATECODEKEY] = code;
        return code;
    }

    /// <summary>
    /// 创建验证码的图片和验证码
    /// </summary>
    /// <param name="length">验证码的长度</param>
    public void CreateValidateImage(int length)
    {   ///创建验证码
        code = CreateCode(length);
        ///创建验证码的图片
        CreateValidateImage(code);
    }

    /// <summary>
    /// 创建验证码的图片和验证码
    /// </summary>
    /// <param name="code">验证码</param>
    public void CreateValidateImage(string code)
    {
        if (string.IsNullOrEmpty(code) == true) { return; }
        ///保存验证码到Session对象中
        HttpContext.Current.Session[VALIDATECODEKEY] = code;
        ///创建一个图像
        Bitmap img = new Bitmap((int)Math.Ceiling((code.Length * IMAGELENGTHBASE)), IMAGEHEIGTH);
        Graphics g = Graphics.FromImage(img);

        ///随机数生成器
        Random random = new Random();

        try
        {
            ///清空图像，并指定填充颜色
            g.Clear(Color.White);

            ///绘制图片的干扰线
            int x1, x2, y1, y2;
            for (int i = 0; i < IMAGELINENUMBER; i++)
            {
                x1 = random.Next(img.Width);
                y1 = random.Next(img.Height);
                x2 = random.Next(img.Width);
                y2 = random.Next(img.Height);
                ///绘制干扰线
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            ///绘制验证码
            Font font = new Font("Tahoma", 12, FontStyle.Bold | FontStyle.Italic);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height),
                Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(code, font, brush, 2.0f, 2.0f);

            ///画图片的前景噪音点
            int x, y;
            for (int i = 0; i < IMAGEPOINTNUMBER; i++)
            {
                x = random.Next(img.Width);
                y = random.Next(img.Height);
                ///绘制点
                img.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            ///画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, img.Width - 1, img.Height - 1);
            ///保存图片的内容到流中

            img.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);

        }
        finally
        {   ///释放占有的资源
            g.Dispose();
            img.Dispose();
        }
    }

}