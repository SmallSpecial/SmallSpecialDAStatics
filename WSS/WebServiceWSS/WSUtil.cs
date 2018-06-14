using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceWSS
{
    public abstract class WSUtil
    {
        public static string YesOrNO(string boolstr)
        {
            if (boolstr.Trim()=="1")
            {
                return "是";
            }
            else
            {
                return "否";
            }
        }
        
    }
}
