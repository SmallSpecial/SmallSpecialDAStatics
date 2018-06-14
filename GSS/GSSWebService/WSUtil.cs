using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
namespace GSSWebService
{
    public abstract class WSUtil
    {
        public static string YesOrNO(string boolstr)
        {
            if (boolstr.Trim() == "1" || boolstr.Trim().ToLower() == "true")
            {
                return LanguageResource.Language.Tip_Yes;
            }
            else
            {
                return LanguageResource.Language.Tip_No;
            }
        }
        public static string IsOnLine(string boolstr)
        {
            if (boolstr.Trim() == "0" || boolstr.Trim().ToLower() == "false")
            {
                return LanguageResource.Language.Tip_Offline;
            }
            else
            {
                return LanguageResource.Language.Tip_Online;
            }
        }
        public static string ConvertNull(object value)
        {
            if (value==null)
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }


    }
}