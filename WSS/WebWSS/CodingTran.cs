using System;
using System.Collections.Generic;
using System.Web;

namespace WebWSS
{
    public class CodingTran
    {

        public static string Tran(string type, string value)
        {
            switch (type)
            {
                case "1":
                    return CodingTran.TranI2G1(value);
                case "2":
                    return CodingTran.TranI2G2(value);
                case "3":
                    return CodingTran.TranI2G3(value);
                case "4":
                    return CodingTran.TranI2G4(value);
                case "5":
                    return CodingTran.TranI2G5(value);
                case "6":
                    return CodingTran.TranI2G6(value);
                case "7":
                    return CodingTran.TranI2G7(value);
                case "8":
                    return CodingTran.TranI2G8(value);
                case "9":
                    return CodingTran.TranI2G9(value);
                case "10":
                    return CodingTran.TranI2G10(value);
                case "11":
                    return CodingTran.TranI2G11(value);
                case "12":
                    return CodingTran.TranI2G12(value);
                case "13":
                    return CodingTran.TranI2G13(value);
                case "14":
                    return CodingTran.TranI2G14(value);
                case "15":
                    return CodingTran.TranI2G15(value);
                case "16":
                    return CodingTran.TranI2G16(value);
                case "17":
                    return CodingTran.TranI2G17(value);
                case "18":
                    return CodingTran.TranI2G18(value);
                case "19":
                    return CodingTran.TranI2G19(value);
                case "20":
                    return CodingTran.TranI2G20(value);
                case "21":
                    return CodingTran.TranI2G21(value);
                case "22":
                    return CodingTran.TranI2G22(value);
                case "23":
                    return CodingTran.TranI2G23(value);
                case "24":
                    return CodingTran.TranI2G24(value);
                case "25":
                    return CodingTran.TranI2G25(value);
                case "26":
                    return CodingTran.TranI2G26(value);
                case "27":
                    return CodingTran.TranI2G27(value);
                case "28":
                    return CodingTran.TranI2G28(value);
                case "29":
                    return CodingTran.TranI2G29(value);
                case "30":
                    return CodingTran.TranI2G30(value);
                case "31":
                    return CodingTran.TranI2G31(value);
                case "32":
                    return CodingTran.TranI2G32(value);
                case "33":
                    return CodingTran.TranI2G33(value);
                case "34":
                    return CodingTran.TranI2G34(value);
                case "35":
                    return CodingTran.TranI2G35(value);
                case "36":
                    return CodingTran.TranI2G36(value);
                case "37":
                    return CodingTran.TranI2G37(value);
                case "38":
                    return CodingTran.TranI2G38(value);
                case "39":
                    return CodingTran.TranI2G39(value);
                case "40":
                    return CodingTran.TranI2G40(value);
                case "41":
                    return CodingTran.TranI2G41(value);
                case "42":
                    return CodingTran.TranI2G42(value);
                case "43":
                    return CodingTran.TranI2G43(value);
                case "99":
                    return CodingTran.TranALL(value);
                case "100":
                    return CodingTran.TranALL1(value);
                case "200":
                    return CodingTran.TranALLALL(value);
                default:
                    return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(value)) + "+";
            }
        }
        //字符集转换   
        public static string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranALL(string value)
        {
            try
            {

                System.Text.Encoding iso8859, gb2312;
                string aBack = "";
                foreach (System.Text.EncodingInfo code in System.Text.Encoding.GetEncodings())
                {

                    iso8859 = System.Text.Encoding.GetEncoding(code.CodePage);
                    gb2312 = System.Text.Encoding.GetEncoding(936);
                    byte[] iso;
                    iso = iso8859.GetBytes(value);
                    aBack += " "+code.CodePage+"."+ gb2312.GetString(iso);
                }
                return aBack;
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
        public static string TranALL1(string value)
        {
            try
            {

                System.Text.Encoding iso8859, gb2312;
                string aBack = "";
                foreach (System.Text.EncodingInfo code in System.Text.Encoding.GetEncodings())
                {

                    iso8859 = System.Text.Encoding.GetEncoding(code.CodePage);
                    gb2312 = System.Text.Encoding.GetEncoding(65001);
                    byte[] iso;
                    iso = iso8859.GetBytes(value);
                    aBack += "|" + code.CodePage + "." + gb2312.GetString(iso);
                }
                return aBack;
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }

        public static string TranALLALL(string value)
        {
            try
            {

                System.Text.Encoding iso8859, gb2312;
                string aBack = "";
                foreach (System.Text.EncodingInfo code in System.Text.Encoding.GetEncodings())
                {

                    iso8859 = System.Text.Encoding.GetEncoding(code.CodePage);

                    foreach (System.Text.EncodingInfo code2 in System.Text.Encoding.GetEncodings())
                    {
                        gb2312 = System.Text.Encoding.GetEncoding(code2.CodePage);
                        byte[] iso;
                        iso = iso8859.GetBytes(value);
                        aBack += "|" + code.CodePage + "." + code2.CodePage + "." + gb2312.GetString(iso);
                    }
                    
                }
                return aBack;
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }


        public static string TranALLALLALL(string value)
        {
            try
            {

                System.Text.Encoding iso8859, gb2312;
                string aBack = "";
                foreach (System.Text.EncodingInfo code in System.Text.Encoding.GetEncodings())
                {

                    iso8859 = System.Text.Encoding.GetEncoding(code.CodePage);

                    
                    foreach (System.Text.EncodingInfo code2 in System.Text.Encoding.GetEncodings())
                    {
                        gb2312 = System.Text.Encoding.GetEncoding(code2.CodePage);
                        byte[] iso;
                        iso = iso8859.GetBytes(value);

                        aBack += TranALLALL(gb2312.GetString(iso));
                    }
                   

                }
                return aBack;
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }

        public static string TranI2G1(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }


        public static string TranI2G2(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.ASCII;
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G3(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.BigEndianUnicode;
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G4(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.Unicode;
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G5(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.UTF32;
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G6(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.UTF7;
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G7(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("ANSI");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G8(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("gb2312");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }


        public static string TranI2G9(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("big5");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G10(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("utf-16");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G11(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("unicodeFFFE");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G12(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("windows-1250");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G13(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("us-ascii");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G14(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-2");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G15(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-3");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G16(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("utf-32");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G17(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("utf-32BE");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G18(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("gbk");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G19(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM00858");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G20(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-3");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G21(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-15");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G22(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("windows-874 ");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G23(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("DOS-720");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G24(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("DOS-862");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G25(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("Windows-1252");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G26(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("x-cp20936");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G27(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-6");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G28(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("EUC-CN");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G29(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("GB18030");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G30(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM870");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G31(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM1026");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G32(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM01047");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G33(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM00924");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G34(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-3");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G35(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso-8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        public static string TranI2G36(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("IBM01140");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G37(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("x-cp20936");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G38(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("x-cp50227");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G39(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("hz-gb-2312");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G40(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("GB18030");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G41(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("ibm850");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G42(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("x-IA5");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        public static string TranI2G43(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("GB18030");
                gb2312 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

    }
}
