using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace WebWSS.CDKey
{
    public partial class CDKeyCreate : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DataSet ds;
        DataTable dtctype;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            InitKeyType();
            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                txtEndTime.Text = DateTime.Now.AddDays(1).ToString(SmallDateTimeFormat);
                BindDropDownList(sltZone);
                BindDdlCtype();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void CreateCDKey()
        {

            DateTime searchdateB = DateTime.Now.AddDays(-1);
            DateTime searchdateE = DateTime.Now;


            string sqlwhere = " where 1=1 ";

            if (ddlKeyType.SelectedIndex > 0)
            {
                sqlwhere += @" and F_KeyType = " + ddlKeyType.SelectedValue + "";
            }

            if (Common.Validate.IsInt(tboxExcelID.Text))
            {
                sqlwhere += @" and F_UserID=" + tboxExcelID.Text + "";
            }

            if (ddlKeyType.SelectedIndex == 0)
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_PleaseCDKCardType;
                return;
            }
            if (!Common.Validate.IsInt(tboxExcelID.Text))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_AwardNoIsDigital;
                return;
            }
            if (!Common.Validate.IsInt(tboxItemNum.Text))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_AwardNumberIsDigital;
                return;
            }
            if (!Common.Validate.IsInt(tboxKeyCount.Text))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_CDKNumberIsDigital;
                return;
            }

            if (tboxNote.Text.Trim().Length == 0)
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_CDKRemarkNonEmpty;
                return;
            }
            else if (tboxNote.Text.Trim().Length > 100)
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_CDKRemarkLengthLimit;
                return;
            }
            int roleLevelLimit;
            if (!int.TryParse(txtRoleLevelLimit.Text, out roleLevelLimit))
            {
                lblinfo.Text = string.Format(App_GlobalResources.Language.Tip_ItemIsDigitalFormat, App_GlobalResources.Language.LblLimitRoleLevel);
                return;
            }
            string startstr = tboxTimeB.Text;
            DateTime start;
            if (!DateTime.TryParse(startstr, out start))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_TimeError;
                return;
            }
            //此处修改为通过gss控制
            ButtonCreate.Enabled = CreateCdkSwitchIsOn();
            string endstr = txtEndTime.Text;
            DateTime end;
            if (!DateTime.TryParse(endstr, out end))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_TimeError;
                return;
            }
            lblinfo.Text = string.Empty;
            try
            {
                int result = 0;
                string batchid = "";

                List<SqlParameter> parameters = new List<SqlParameter>() {
					new SqlParameter("@KeyType", SqlDbType.Int),
					new SqlParameter("@KeyCount", SqlDbType.Int),
                    new SqlParameter("@ExcelID", SqlDbType.Int),
                    new SqlParameter("@ItemNum",SqlDbType.Int),
                    new SqlParameter("@BeginTime",SqlDbType.SmallDateTime),
                    new SqlParameter("@EndTime",SqlDbType.SmallDateTime),
                    new SqlParameter("@BigZoneID",SqlDbType.SmallInt),
                    new SqlParameter("@ZoneID",SqlDbType.SmallInt),
                    new SqlParameter("@NeedPlayerLevel",SqlDbType.SmallInt),
                    new SqlParameter("@Note",SqlDbType.NVarChar,200),
                    new SqlParameter("@MailTitle",SqlDbType.NVarChar,40),
                    new SqlParameter("@MailSendName",SqlDbType.NVarChar,20),
                    new SqlParameter("@MailContent",SqlDbType.NVarChar,200),
                    new SqlParameter("@BatchID",SqlDbType.NVarChar,4),
					new SqlParameter("@Result", SqlDbType.Int)
                };
                parameters[0].Value = ddlKeyType.SelectedValue;
                parameters[1].Value = tboxKeyCount.Text;
                parameters[2].Value = tboxExcelID.Text;
                parameters[3].Value = tboxItemNum.Text;
                parameters[4].Value = start;
                parameters[5].Value =end;
                parameters[6].Value = BigZoneID;
                parameters[7].Value = sltZone.SelectedItem.Value;
                parameters[8].Value = roleLevelLimit <= 1 ? 1 : roleLevelLimit;//等级
                parameters[9].Value = tboxNote.Text;
                parameters[10].Value = string.IsNullOrEmpty(mailTitle.Text) ? "MailTitle" : mailTitle.Text;
                parameters[11].Value = string.IsNullOrEmpty(txtMailSendName.Text) ? "MailSendName" : txtMailSendName.Text;
                parameters[12].Value = string.IsNullOrEmpty(txtMailContent.Text) ? "MailContent" : txtMailContent.Text;
                parameters[13].Direction = ParameterDirection.Output;
                parameters[14].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("LKSV_8_MainDB_"+BigZoneID+".MainDB.[dbo].[_SQL_CDKey_Create]", parameters.ToArray(), "ds", 180);
                batchid = parameters[13].Value.ToString().ToUpper();
                result = (int)parameters[14].Value;

                if (result==0)
                {
                    lblinfo.Text = string.Format(App_GlobalResources.Language.Tip_CDKExecuteSuccNoInfoFormat, batchid);
                }
                else
                {
                    lblinfo.Text = App_GlobalResources.Language.Tip_Failure;
                }

            }
            catch (System.Exception ex)
            {
                lblinfo.Text = ex.Message;
            }
            ButtonCreate.Enabled = true;

        }




        private void InitKeyType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID", Type.GetType("System.String"));
            dt.Columns.Add("CValue", Type.GetType("System.String"));
            string[] ctypes = GetCDKeyCategory();// new string[] { "1|官方新手礼包", "2|YY贵族特权卡", "3|精英公会卡", "4|17173专属卡", "5|QQ特权卡", "6|新浪特权卡", "7|QQ会员特权卡", "8|YY皇室特权卡", "9|公会元宝卡(特殊 200张)", "10|17173爱心礼包", "11|QQ每天10次卡" };
            foreach (String ctype in ctypes)
            {
                DataRow dr = dt.NewRow();
                dr["CID"] = ctype.Split('|')[0];
                dr["CValue"] = ctype.Split('|')[1];
                dt.Rows.Add(dr);
            }
            dtctype = dt;
        }



        private void BindDdlCtype()
        {
            try
            {
                DataRow newdr = dtctype.NewRow();
                newdr["CID"] = App_GlobalResources.Language.LblAllSelect;
                newdr["CValue"] = App_GlobalResources.Language.LblAllSelect;
                dtctype.Rows.InsertAt(newdr, 0);
                this.ddlKeyType.DataSource = dtctype;
                this.ddlKeyType.DataTextField = "CValue";
                this.ddlKeyType.DataValueField = "CID";
                this.ddlKeyType.DataBind();

            }
            catch (System.Exception ex)
            {
                ddlKeyType.Items.Clear();
                ddlKeyType.Items.Add(new ListItem(App_GlobalResources.Language.LblAllSelect, ""));
            }
        }

        protected void ddlKeyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tboxExcelID.Enabled&& ddlKeyType.SelectedIndex > 0)
            {
                switch (ddlKeyType.SelectedValue)
                {
                    case "1":
                        tboxExcelID.Text = "530140";
                        break;
                    case "2":
                        tboxExcelID.Text = "530141";
                        break;
                    case "3":
                        tboxExcelID.Text = "530142";
                        break;
                    case "4":
                        tboxExcelID.Text = "530143";
                        break;
                    case "5":
                        tboxExcelID.Text = "530144";
                        break;
                    case "6":
                        tboxExcelID.Text = "530145";
                        break;
                    case "7":
                        tboxExcelID.Text = "530146";
                        break;
                    case "8":
                        tboxExcelID.Text = "530147";
                        break;
                    case "9":
                        tboxExcelID.Text = "570384";
                        break;
                    case "10":
                        tboxExcelID.Text = "530148";
                        break;
                    case "11":
                        tboxExcelID.Text = "533104";
                        break;
                    default:
                        tboxExcelID.Text = "";
                        break;
                }
            }
        }
        public void BindDropDownList(DropDownList drop)
        {
            Dictionary<string, string> zones = GetZoneDataDict();
          
            drop.Items.Clear();
            drop.Items.Add(new ListItem(App_GlobalResources.Language.LblAllSelect, "-1"));
            foreach (KeyValuePair<string,string> item in zones)
            {
                drop.Items.Add(new ListItem(item.Value,item.Key));
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            CreateCDKey();
        }

    }
}
