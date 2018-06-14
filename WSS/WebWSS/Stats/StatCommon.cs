using System;
using System.Collections.Generic;
using System.Web;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public class StatCommon
    {
        static string  ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
       // static string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
       // DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        //得到文本名称
        public static string GetTextName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT ( cast(F_ExcelID as varchar(9)) +' '+F_Name+' '+F_Type+' '+F_TypeP ) as F_Name  FROM T_BaseGameName WHERE (F_ExcelID = " + value + ") ";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }

        //得到角色名称
        public static string GetRoleName(string bigzone,string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT  F_RoleName FROM T_BaseRoleCreate WHERE (F_BigZoneID = 1) AND (F_RoleID = 2) " + value + ") ";
                return string.Format("({0}){1}",value,spg.GetSingle(sql) ) ;
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
    }
}
