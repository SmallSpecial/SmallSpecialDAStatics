using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GSS.DBUtility
{
    public class WinUtil
    {

        /// <summary>
        /// 转换数据库连接密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string ConvertToPwd(string pwd)
        {
            string newPwd = "";
            char[] pwdArray = pwd.ToCharArray();
            int length = pwdArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i % 2 == 0 && i != 0 && i < length - 1)
                {
                    char temp = pwdArray[i];
                    pwdArray[i] = pwdArray[i + 1];
                    pwdArray[i + 1] = temp;
                }
                newPwd += pwdArray[i];
            }
            return newPwd;
        }

        /// <summary>
        /// 空对象转成空字符串
        /// </summary>
        public static string TrimNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            return value.ToString();
        }
        /// <summary>
        /// 是不是中国电话，格式010-88886666
        /// </summary>
        public static bool IsTelphone(string value)
        {
            return Regex.IsMatch(value, @"^[0]\d{2,3}-?\d{7,8}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public static bool IsMobile(string value)
        {
            return Regex.IsMatch(value, @"^[1]\d{10}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证该字符串是否是合法的ip地址
        /// </summary>
        /// <param name="ip">需要验证的字符串</param>
        /// <returns>合法为true,非法为false</returns>
        public static bool isIpaddres(string ip)
        {
            Regex reg = new Regex("(((2[0-4]\\d)|(25[0-5]))|(1\\d{2})|([1-9]\\d)|(\\d))[.](((2[0-4]\\d)|(25[0-5]))|(1\\d{2})|([1-9]\\d)|(\\d))[.](((2[0-4]\\d)|(25[0-5]))|(1\\d{2})|([1-9]\\d)|(\\d))[.](((2[0-4]\\d)|(25[0-5]))|(1\\d{2})|([1-9]\\d)|(\\d))");
            if (reg.IsMatch(ip, 0))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        /// <summary>
        /// 验证该字符串是否为数字
        /// </summary>
        /// <param name="Number">需要验证的字符串</param>
        /// <returns>验证结果：是为true，不是为false</returns>
        public static bool IsNumber(string Number)
        {
            //Regex reg = new Regex("[1-9]\\d*");
            //if (reg.IsMatch(Number, 0)&&Number.IndexOf(":")<0)
            //{
            //    return (true);
            //}
            //else
            //{
            //    return (false);
            //}
            try
            {
                Convert.ToInt32(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证该字符串是否为数字
        /// </summary>
        /// <param name="Number">需要验证的字符串</param>
        /// <returns>验证结果：是为true，不是为false</returns>
        public static bool IsDouble(string Number)
        {
            try
            {
                Convert.ToDouble(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 验证数据是否为空
        /// </summary>
        /// <param name="str">代验证数据</param>
        /// <returns></returns>
        public static bool isNull(string str)
        {
            if (str.Trim() == "" || str == "")
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        /// <summary>
        /// 验证是否时间格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;

            }
        }

        /// <summary>
        /// 转换成数字,则为NULL
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ConvertToInt(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);

            }
            catch
            {
                return null;

            }
        }

    }

    public class OperateAndValidate
    {
        #region  验证数字
        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="P_str_num"></param>
        /// <returns></returns>
        public bool validateNum(string P_str_num)
        {
            return Regex.IsMatch(P_str_num, "^[0-9]*$");
        }
        #endregion
        #region  验证传真
        /// <summary>
        /// 验证传真
        /// </summary>
        /// <param name="P_str_fax"></param>
        /// <returns></returns>
        public bool validateFax(string P_str_fax)
        {
            return Regex.IsMatch(P_str_fax, @"86-\d{3,4}-\d{7,8}");
        }
        #endregion
        #region  验证邮政编码
        /// <summary>
        /// 验证邮政编码
        /// </summary>
        /// <param name="P_str_postcode"></param>
        /// <returns></returns>
        public bool validatePostCode(string P_str_postcode)
        {
            return Regex.IsMatch(P_str_postcode, @"\d{6}");
        }
        #endregion
        #region  验证电子邮件
        /// <summary>
        /// 验证电子邮件
        /// </summary>
        /// <param name="P_str_email"></param>
        /// <returns></returns>
        public bool validateEmail(string P_str_email)
        {
            return Regex.IsMatch(P_str_email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        #endregion
        #region  验证网址
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="P_str_naddress"></param>
        /// <returns></returns>
        public bool validateNAddress(string P_str_naddress)
        {
            return Regex.IsMatch(P_str_naddress, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        #endregion
        #region  验证是否为空
        /// <summary>
        /// 验证是否为空
        /// </summary>
        /// <param name="P_str_null"></param>
        /// <returns></returns>
        public bool validateNull(string P_str_null)
        {
            bool falg = false;
            if (P_str_null == string.Empty)
            {
                falg = false;
            }
            else
            {
                falg = true;
            }
            return falg;
        }
        #endregion
    }
}
