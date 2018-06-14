using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;
using System.Configuration;
using System.Data;
using WSS.DBUtility;

namespace WSS.Web.Admin.GameTool
{
    public partial class UserDisble : System.Web.UI.Page
    {
        protected WebServiceXLJ.WebServiceGame xlj = new WebServiceXLJ.WebServiceGame();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetGameCode();
            if (!Ext.IsAjaxRequest)
            {
                SetWebservice();
            }

        }

        protected void Check_btnSubmit(object sender, AjaxEventArgs e)//提交事件
        {
            int ShowIndex = Convert.ToInt32(rbgTool.CheckedItems[0].DataIndex);
            switch (ShowIndex)
            {
                case 0://基础信息
                    break;
                case 1://封停工具
                    SubmitUserLock();
                    break;
                case 2://查询/解封
                    SubmitUserNoLock();
                    break;
                case 3://封IP工具
                    SubmitUserLockIP();
                    break;
                case 4://清空防沉迷
                    SubmitClearChildInfo();
                    break;
                case 5://踢号工具
                    SubmitUserOffline();
                    break;
                case 6://改名工具
                    SubmitUserChangeName();
                    break;
                case 7://改服工具
                    SubmitUserChangeZone();
                    break;
                case 8://帐号借用
                    SubmitUserGMUse();
                    break;
                case 9://帐号归还
                    SubmitUserGMBack();
                    break;
                case 10://清空身份证
                    SubmitClearUserPersonID();
                    break;
                case 11://清空邮箱
                    SubmitClearUserEmail();
                    break;
                case 12://清空密保
                    SubmitClearUserPSWProtect();
                    break;
                case 13://清二级密码
                    SubmitClearUserSecondPSW();
                    break;
                case 14://密码初始化
                    SubmitSetUserPSWinit();
                    break;

            }
        }

        protected void Click_btnSubmit(object sender, AjaxEventArgs e)//重置事件
        {
            int ShowIndex = Convert.ToInt32(rbgTool.CheckedItems[0].DataIndex);
            txtaNote.Reset();
            switch (ShowIndex)
            {
                case 0://基础信息
                    break;
                case 1://封停工具

                    break;
                case 2://查询/解封
                    break;
                case 3://封IP工具
                    txtURlockipE.Reset();
                    txtURlockip.Reset();
                    break;
                case 4://清空防沉迷
                    break;
                case 5://踢号工具
                    break;
                case 6://改名工具
                    txtURchangename.Reset();
                    break;
                case 7://改服工具
                    cbURchangeserver.Reset();
                    break;
                case 8://帐号借用
                    txtPwd.Reset();
                    txtPwdR.Reset();
                    break;
                case 9://帐号归还
                    break;

            }
            //cbGameRole.Reset();
            //cbGameUser.Reset();
            cbGameRole.Focus();
            
        }

        protected void Select_cbGameCode(object sender, AjaxEventArgs e)//游戏名称选中事件
        {

            SetGameZoneID();
        }

        protected void Select_cbGameZoneID(object sender, AjaxEventArgs e)//游戏运营区域选中事件
        {
            SetWebservice();
        }
        protected void Select_cbGameUser(object sender, AjaxEventArgs e)//用户选中事件
        {

            string gameuserid = cbGameUser.SelectedItem.Value;

            if (gameuserid.Length > 0)
            {
                string jsons = xlj.GetGameRoles(gameuserid);
                cbGameRole.ClearValue();
                StoreGameRole.LoadData(jsons);
                SetToolInit();

            }

        }

        protected void Select_cbGameRole(object sender, AjaxEventArgs e)//角色选中事件
        {


            string gameuserid = cbGameUser.SelectedItem.Value;
            string gameroleid = cbGameRole.SelectedItem.Value;

            if (gameroleid.Length > 0)
            {
                SetToolInit();
            }

        }

        private void SetWebservice()//设置游戏的WEBSERVICE地址
        {
            try
            {
                string gamezoneid = cbGameZoneID.SelectedItem.Value;

                string WebSUrl = WSS.BLL.AllOther.GetWebServiceUrl(gamezoneid);

                if (WebSUrl.Length > 10)
                {
                    xlj.Url = WebSUrl;
                }
                xlj.Credentials = System.Net.CredentialCache.DefaultCredentials;
                xlj.HelloWorld();
                cbGameUser.DoQuery("1<>1", false);//为了清除自动下拉的缓存

                string jsons = xlj.GetUserlocktype();//更新封停时间类型
                cbURlocktime.ClearValue();
                StoreGameLockType.LoadData(jsons);

                jsons = xlj.GetGameZones();
                cbURchangeserver.ClearValue();
                StoreGameServer.LoadData(jsons);

                SetToolInit();
                cbGameUser.ClearValue();
                cbGameRole.ClearValue();
                SetToolInit();

                cbGameUser.Disabled = false;
                cbGameUser.ClearValue();
            }
            catch //(System.Exception ex)
            {
               // Ext.Msg.Alert("工具提示", cbGameZoneID.SelectedItem.Text + "WebService数据连接错误");
                if (cbGameZoneID.SelectedItem.Text.Length > 0)
                {
                    cbGameUser.Disabled = true;
                    cbGameUser.SetRawValue("【" + cbGameZoneID.SelectedItem.Text + "】连接失败,请重新选择");
                }
              
            }

        }

        protected void SubmitUserLock()//提交用户或角色封停
        {
            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string roleid = cbGameRole.SelectedItem.Value;
            string rolename = cbGameRole.SelectedItem.Text;
            string locktype = cbURlocktime.SelectedItem.Value;
            string jsons = "";
            if (roleid.Length > 0)
            {
                jsons = xlj.SetRoleLock(roleid, rolename, locktype);
            }
            else
            {
                jsons = xlj.SetUserLock(userid, username, locktype);
            }

            string msg = "用户:" + username + "";
            if (rolename.Length > 0)
            {
                msg += "下的角色:" + rolename + "";
            }
            if (jsons == "true")
            {
                Session["lblURnolock"] = "false";
                Ext.Msg.Alert("工具提示", "" + msg + "封停成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "封停失败!" + jsons).Show();
            }

        }

        protected void SubmitUserNoLock()//提交用户或角色解封
        {
            if (lblURnolock.Html.IndexOf("暂无") >= 0)
            {
                Ext.Msg.Alert("工具提示", "没有被封停,不需要解封").Show();
                return;
            }
            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string roleid = cbGameRole.SelectedItem.Value;
            string rolename = cbGameRole.SelectedItem.Text;

            string jsons = "";
            if (roleid.Length > 0)
            {
                jsons = xlj.SetRoleNoLock(roleid);
            }
            else
            {
                jsons = xlj.SetUserNoLock(userid);
            }

            string msg = "用户:" + username + "";
            if (rolename.Length > 0)
            {
                msg += "下的角色:" + rolename + "";
            }
            if (jsons == "true")
            {
                Session["lblURnolock"] = "false";
                lblURnolock.Html = "提示:暂无封停信息";
                Ext.Msg.Alert("工具提示", "" + msg + "解封成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "解封失败!" + jsons).Show();
            }

        }

        protected void SubmitClearChildInfo()//提交清除防沉迷信息
        {
            if (lblURchildren.Html.IndexOf("暂无") >= 0)
            {
                Ext.Msg.Alert("工具提示", "没有防沉迷信息,不需要清除").Show();
                return;
            }
            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.ClearChildDisInfo(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Session["lblURchildren"] = "false";//表示没有查询过信息,需要更新信息
                lblURchildren.Html = "提示:暂无防沉迷信息";
                Ext.Msg.Alert("工具提示", "" + msg + "清除防沉迷信息成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清除防沉迷信息失败!" + jsons).Show();
            }

        }

        protected void SubmitUserLockIP()//提交用户封IP
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string ipbegin = txtURlockip.Text;
            string ipend = txtURlockipE.Text;

            string jsons = "";
            if (userid.Length > 0)
            {
                jsons = xlj.SetUserLockIP(userid, username, ipbegin, ipend);
            }

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {

                Ext.Msg.Alert("工具提示", "" + msg + "封停IP成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "封住IP失败!" + jsons).Show();
            }

        }

        protected void SubmitUserOffline()//提交踢号操作
        {
            if (lblURnoonline.Html.IndexOf("暂无") >= 0)
            {
                Ext.Msg.Alert("工具提示", "没有在线信息,不需要踢号").Show();
                return;
            }
            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.SetUserOffline(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Session["lblURnoonline"] = "false";//表示没有查询过信息,需要更新信息
                lblURnoonline.Html = "提示:暂无在线信息";
                Ext.Msg.Alert("工具提示", "" + msg + "踢号成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "踢号失败!" + jsons).Show();
            }

        }

        protected void SubmitUserChangeName()//提交改名操作
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string roleid = cbGameRole.SelectedItem.Value;
            string rolename = cbGameRole.SelectedItem.Text;
            string newname = txtURchangename.Text;

            string jsons = "";
            if (roleid.Length > 0)
            {
                jsons = xlj.SetRoleNameChange(userid, roleid, rolename, newname);
            }
            else
            {
                jsons = xlj.SetUserNameChange(userid, username, newname);
            }

            string msg = "用户:" + username + "";
            if (rolename.Length > 0)
            {
                msg += "下的角色:" + rolename + "";
            }
            if (jsons == "true")
            {

                Ext.Msg.Alert("工具提示", "" + msg + "改名成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "改名失败!" + jsons).Show();
            }

        }

        protected void SubmitUserChangeZone()//角色改服
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string roleid = cbGameRole.SelectedItem.Value;
            string rolename = cbGameRole.SelectedItem.Text;
            string zoneid = cbURchangeserver.SelectedItem.Value;

            string jsons = "";
            if (roleid.Length > 0)
            {
                jsons = xlj.SetRoleZoneChange(userid, roleid, zoneid);
            }
            else
            {
                Ext.Msg.Alert("工具提示", "请选择角色!").Show();
                return;
            }

            string msg = "用户:" + username + "";
            if (rolename.Length > 0)
            {
                msg += "下的角色:" + rolename + "";
            }
            if (jsons == "true")
            {

                Ext.Msg.Alert("工具提示", "" + msg + "改服成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "改服失败!" + jsons).Show();
            }

        }

        protected void SubmitUserGMUse()//帐号借用
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;
            string password = txtPwd.Text;
            password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password.Replace("'", ""), "MD5").ToLower();

            string jsons = "";
            if (userid.Length > 0)
            {
                jsons = xlj.SetUserGMUse(userid, password, "GM");
            }

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {

                Ext.Msg.Alert("工具提示", "" + msg + "帐号借用成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "帐号借用失败!" + jsons).Show();
            }

        }

        protected void SubmitUserGMBack()//帐号归还
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;

            string jsons = "";
            if (userid.Length > 0)
            {
                jsons = xlj.SetUserGMBack(userid);
            }

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {

                Ext.Msg.Alert("工具提示", "" + msg + "帐号归还成功!").Show();

            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "帐号归还失败!" + jsons).Show();
            }

        }

        protected void SubmitClearUserPersonID()//提交清空身份证
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.ClearUserPersonID(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空身份证成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空身份证失败!" + jsons).Show();
            }

        }

        protected void SubmitClearUserEmail()//提交清空邮箱
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.ClearUserEmail(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空邮箱成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空邮箱失败!" + jsons).Show();
            }

        }
        protected void SubmitClearUserPSWProtect()//提交清空密保
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.ClearUserPSWProtect(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空密保成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空密保失败!" + jsons).Show();
            }

        }
        protected void SubmitClearUserSecondPSW()//提交清二级密码
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.ClearUserSecondPSW(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空二级密码成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "清空二级密码失败!" + jsons).Show();
            }

        }
        protected void SubmitSetUserPSWinit()//提交密码初始化
        {

            string userid = cbGameUser.SelectedItem.Value;
            string username = cbGameUser.SelectedItem.Text;


            string jsons = "";

            jsons = xlj.SetUserPSWinit(userid);

            string msg = "用户:" + username + "";

            if (jsons == "true")
            {
                Ext.Msg.Alert("工具提示", "" + msg + "密码初始化成功!").Show();
            }
            else
            {
                Ext.Msg.Alert("工具提示", "" + msg + "密码初始化失败!" + jsons).Show();
            }

        }

        protected void SetToolInit()//游戏工具初始化
        {

            rbgTool.Items[0].Checked = true;
            for (int i = 1; i < 15; i++)
            {
                rbgTool.Items[i].Checked = false;
            }
            //用SESSION来记录是否已经更新
            Session["lblURnolock"] = "false";
            Session["lblURchildren"] = "false";
            Session["lblURnoonline"] = "false";
            lblURnolock.Html = "";
            lblURchildren.Html = "";
            lblURnoonline.Html = "";
            txtPwd.Text = "";
            txtPwdR.Text = "";
            txtURchangename.Text = "";
            txtURlockip.Text = "";

        }

        private void SetGameCode()//设置游戏名称列表
        {
            string sql = "SELECT F_ID, F_Name FROM T_GameConfig WHERE (F_IsUsed = 1) and F_ParentID=100 ORDER BY F_Sort";
            DataSet ds = DbHelperSQL.Query(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string txtstr = dr["F_Name"].ToString();
                string valuestr = dr["F_ID"].ToString();
                cbGameCode.Items.Add(new Coolite.Ext.Web.ListItem(txtstr, valuestr));
            }

        }
        private void SetGameZoneID()//设置游戏大区(以WEBSERVICE为区分,目前设计为一个大区数据库对应一个WEBSEVICE)
        {
            string gameid = cbGameCode.SelectedItem.Value;
            if (gameid.Length > 0)
            {
                cbGameZoneID.ClearValue();
                string sql = "SELECT F_ID, F_Name, F_Value FROM T_GameConfig WHERE (F_IsUsed = 1) and F_ParentID=" + gameid + " ORDER BY F_Sort";
                DataSet ds = DbHelperSQL.Query(sql);

                StoreGameZoneID.DataSource = ds.Tables[0];
                StoreGameZoneID.DataBind();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    cbGameZoneID.SelectedItem.Value = ds.Tables[0].Rows[0]["F_ID"].ToString();
                //}

            }
        }

    }
}
