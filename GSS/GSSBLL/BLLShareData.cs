using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace GSSBLL
{
    public class BLLShareData : MarshalByRefObject
    {
        public BLLShareData()
            : base()
        {
        }

        public string GSSClientVersion_C()
        {
            try
            {
                StreamReader sr = new StreamReader("GSSDATA/Version.DAT", System.Text.Encoding.Default);
                //while (!sr.EndOfStream)
                //{
                string s = sr.ReadLine();
                return s.Trim();
                // }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public string GSSClientVersion_S()
        {
            try
            {
                StreamReader sr = new StreamReader("GSSClient/GSSDATA/Version.DAT", System.Text.Encoding.Default);
                //while (!sr.EndOfStream)
                //{
                string s = sr.ReadLine();
                return s.Trim();
                // }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
 
    }
}
