using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GSS.DBUtility;

namespace GSSClient
{
    class DataConfig
    {
        public static string[] GetServerInfo()
        {
            string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
            DataSet ds = DbHelperSQLite.Query(sqlstr);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                string[] servinfo=new string[2];
                servinfo[0] = ds.Tables[0].Rows[0]["GSSIP"].ToString();
                servinfo[1] = ds.Tables[0].Rows[0]["GSSPORT"].ToString();
                return servinfo;
            }
            else
            {
                return null;
            }
        }

    }
}
