using System;
using System.Collections.Generic;
using System.Web;

namespace WebWSS.Common
{
    public class Validate
    {
        public static bool IsDateTime(object value)
        {
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool IsDouble(object value)
        {
            try
            {
                Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsInt(object value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
