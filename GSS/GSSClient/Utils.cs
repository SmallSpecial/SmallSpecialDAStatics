using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;

namespace GSSClient
{
    class Utils
    {

        public static Bitmap GetAttImg(string url)
        {
            try
            {
                if (url.IndexOf("http://")==-1)
                {
                    url = ClientCache.GetGameConfigValue("12") + url;
                }
               

                WebRequest webreq = WebRequest.Create(url);
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                Image image;
                image = Image.FromStream(stream);
                stream.Close();
                Bitmap bitm = new Bitmap(image, new Size(200, image.Height * 200 / image.Width));
                return bitm;
            }
            catch (System.Exception ex)
            {
                return new Bitmap(Image.FromFile(System.Windows.Forms.Application.StartupPath+"\\gssdata\\images\\picerror.png"),200,180);; 	
            }

        }
    }
}
