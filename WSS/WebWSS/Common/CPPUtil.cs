using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.InteropServices;

namespace WebWSS.Common
{
    public class CPPUtil
    {
        [DllImport("CLibForWSS.dll")]
        public static extern int Add(int x, int y);
    }
}
