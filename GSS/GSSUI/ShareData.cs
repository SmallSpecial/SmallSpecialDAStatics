using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using GSS.DBUtility;

namespace GSSUI
{
    public class SharData
    {

        private static Color _backcolor = Color.FromArgb(12, 130, 175);
        /// <summary>
        /// 窗体背景颜色
        /// </summary>
        public static Color BackColor
        {
            get
            {
                return (_backcolor);
            }
            set
            {
                _backcolor = value;

            }
        }
        private static Image _backimage = null;
        /// <summary>
        /// 窗体背景图片
        /// </summary>
        public static Image BackImage
        {
            get
            {
                return (_backimage);
            }
            set
            {
                _backimage = null;
                _backimage = value;
            }
        }
        private static double _opacity = 100;
        /// <summary>
        /// 窗体透明度
        /// </summary>
        public static double Opacity
        {
            get
            {
                return (_opacity);
            }
            set
            {
                _opacity = value;
            }
        }

        private static bool _topmost = false;
        /// <summary>
        /// 窗体最前
        /// </summary>
        public static bool TopMost
        {
            get
            {
                return (_topmost);
            }
            set
            {
                _topmost = value;
            }
        }

        /// <summary>
        /// 得到存储的窗体样式
        /// </summary>
        public static void GetUIInfo()
        {
            string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
            try
            {
                DataSet ds = DbHelperSQLite.Query(sqlstr);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    string[] servinfo = new string[2];
                    string backcolor = ds.Tables[0].Rows[0]["GSSUIBACKCOLOR"].ToString();
                    string[] cols = backcolor.Split(',');
                    _backcolor = Color.FromArgb(int.Parse(cols[0]), int.Parse(cols[1]), int.Parse(cols[2]));
                    _opacity = Convert.ToDouble(ds.Tables[0].Rows[0]["GSSUIOPACITY"].ToString());
                    _topmost = ds.Tables[0].Rows[0]["GSSUITOPMOST"].ToString() == "1" ? true : false;
                }
                _backimage = GetImage();
            }
            catch (System.Exception ex)
            {
                //不做处理
            }
        }
        /// <summary>
        /// 存储窗体样式
        /// </summary>
        public static void SetUIInfo()
        {
            try
            {
                string backcolor = _backcolor.R.ToString() + "," + _backcolor.G.ToString() + "," + _backcolor.B;
                string opacity = _opacity.ToString();
                string topmost = _topmost ? "1" : "0";

                string sql = "UPDATE GSSCONFIG SET GSSUIBACKCOLOR='" + backcolor + "',GSSUITOPMOST='" + topmost + "',GSSUIBACKIMAGE='',GSSUIOPACITY='" + opacity + "' WHERE ID=1";
                int row = DbHelperSQLite.ExecuteSql(sql);

                SaveImage(_backimage);
                Image dd = GetImage();
            }
            catch (System.Exception ex)
            {
                //不做处理
            }
        }

        #region 图片存储相关

        private static string GetStringFromImage(Image image)
        {
            if (image == null)
            {
                return "";
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
            bf.Serialize(ms, image);//将消息类转换为内存流
            ms.Position = 0;
            byte[] data = ms.GetBuffer();
            return Convert.ToBase64String(data);
        }
        private static Image GetImageFromString(string value)
        {
            if (value.Trim().Length == 0)
            {
                return null;
            }
            byte[] data = Convert.FromBase64String(value);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bfb = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            Image img = (Image)bfb.Deserialize(ms);
           // Image img = Image.FromStream(ms);
            return img;
        }

        private static bool SaveImage(Image img)
        {
            System.IO.Directory.CreateDirectory("GSSData\\GSSCache");

            System.IO.StreamWriter CacheInf = new System.IO.StreamWriter("GSSData\\GSSCache\\UIBackImgCache.dat");

            if (img == null)
            {
                return false;
            }
            string cahe = GetStringFromImage(img);
            CacheInf.Write(cahe);
            CacheInf.Close();
            return (true);
        }
        private static Image GetImage()
        {
            string fileName = "GSSData\\GSSCache\\UIBackImgCache.dat";
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            if (!file.Exists)
            {
                return null;
            }
            try
            {
                System.IO.StreamReader CacheInfo = new System.IO.StreamReader(fileName);
                string cache = CacheInfo.ReadToEnd();
                return GetImageFromString(cache);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
