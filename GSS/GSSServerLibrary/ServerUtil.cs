using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace GSSServerLibrary
{
    public class ServerUtil
    {
        public static void BindDropDLTDept(ComboBox cb)
        {
            DBHandle db = new DBHandle();
            DataSet ds = db.GetDepts();
            DataRow dr = ds.Tables[0].NewRow();
            dr["F_DepartID"] = "0";
            dr["F_ParentID"] = "0";
            dr["F_DepartName"] = "请选择...";
            ds.Tables[0].Rows.InsertAt(dr,0);
            BindDropDLT(cb, ds, "F_DepartName", "F_DepartID");
        }
        public static void BindDropDLTRoles(ComboBox cb)
        {
            DBHandle db = new DBHandle();
            DataSet ds = db.GetRoles();
            DataRow dr = ds.Tables[0].NewRow();
            dr["F_RoleID"] = "0";
            dr["F_RoleName"] = "请选择...";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            BindDropDLT(cb, ds, "F_RoleName", "F_RoleID");
        }



        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="ds"></param>
        /// <param name="dis"></param>
        /// <param name="value"></param>
        public static void BindDropDLT(ComboBox cb, DataSet ds, string dis, string value)
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.DataSource = ds.Tables[0];
            cb.DisplayMember = dis;
            cb.ValueMember = value;
        }
    }
}
