<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDisble.aspx.cs" Inherits="WSS.Web.Admin.GameTool.UserDisble" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>帐户封停工具</title>
    <link type="text/css" rel="Stylesheet" href="../../style/master.css" media="all" />
    <style type="text/css">
        .search-item
        {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
        }
        .dot-label
        {
            font-weight: normal;
            font-size: 11px;
            color: Gray;
        }
        .ceshi
        {
            background-color: Red;
        }
        .list-itemgamezone
        {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
        }
        h3
        {
            display: block;
            font: inherit;
            font-weight: bold;
            color: #222;
        }

        .icon-exclamation {
            padding-left: 25px !important;
            background: url(/icons/exclamation-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .icon-accept {
            padding-left: 25px !important;
            background: url(/icons/accept-png/coolite.axd) no-repeat 3px 3px !important;
        }
 .icon-usermagnify {
            padding-left: 25px !important;
            background: url(/icons/user_magnify-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .icon-servergo {
            padding-left: 25px !important;
            background: url(/icons/server_go-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .icon-serverchart {
            padding-left: 25px !important;
            background: url(/icons/server_chart-png/coolite.axd) no-repeat 3px 3px !important;
        }
    </style>

    <script runat="server">




        protected void rbgTool_Click(object sender, EventArgs e)//工具选择事件
        {

            int ShowIndex = Convert.ToInt32(rbgTool.CheckedItems[0].DataIndex);
            string gameuserid = cbGameUser.SelectedItem.Value;
            string gameroleid = cbGameRole.SelectedItem.Value;

            //工具使用备注前面标明当前工具名称
            lblnote1.Text = rbgTool.CheckedItems[0].BoxLabel;

            for (int i = 0; i < 15; i++)//确定显示的功能模块
            {
                if (i != ShowIndex)
                {
                    WizardLayout.Items[i].Hide();
                }
                else
                {
                    WizardLayout.Items[i].Show();
                }
            }

            cbURlocktime.AllowBlank = true;
            cbURchangeserver.AllowBlank = true;
            txtPwd.AllowBlank = true;
            txtPwdR.AllowBlank = true;
            cbGameRole.AllowBlank = true;
            txtURlockip.AllowBlank = true;
            txtURlockipE.AllowBlank = true;
            txtURchangename.AllowBlank = true;
            txtaNote.AllowBlank = true;
            pnlNote.Show();
            pnlSubmit.Show();
            
            switch (ShowIndex)
            {
                case 0://基础信息

                    pnlNote.Hide();
                    pnlSubmit.Hide();
                    break;
                case 1://封停工具

                    if (cbURlocktime.Items.Count == 0 && Session["lblURnolock"]!=null && Session["lblURnolock"].ToString() == "false")
                    {

                    }
                    if (gameuserid.Trim() != string.Empty && Session["lblURnolock"].ToString() == "false")
                    {
                        lblURnolock.Html = xlj.GetLockInfo(gameuserid, gameroleid);
                        Session["lblURnolock"] = "true";//已经更新
                    }
                    int goint = lblURnolock.Html.IndexOf("暂无");
                    if (goint < 0)
                    {
                        rbgTool.Items[1].Checked = false;
                        rbgTool.Items[2].Checked = true;
                    }
                    cbURlocktime.SyncSize();
                    lbltooltips.Text = "工具使用提示：如果没有选择角色,则针对帐号操作";

                    cbURlocktime.AllowBlank = false;
                    txtaNote.AllowBlank = false;
                    break;
                case 2://查询/解封

                    if (gameuserid.Trim() != string.Empty && Session["lblURnolock"] != null && Session["lblURnolock"].ToString() == "false")
                    {
                        lblURnolock.Html = xlj.GetLockInfo(gameuserid, gameroleid);
                        Session["lblURnolock"] = "true";//已经更新
                    }
                    else if (gameuserid.Trim() == string.Empty)
                    {
                        lblURnolock.Html = "提示:暂无封停信息";
                    }
                    lbltooltips.Text = "工具使用提示：如果没有选择角色,则针对帐号操作";

                    txtaNote.AllowBlank = false;
                    break;
                case 3://封IP工具

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";

                    
                    txtURlockip.AllowBlank = false;
                    txtaNote.AllowBlank = false;
                    break;
                case 4://清空防沉迷

                    lbltooltips.Text = "工具使用提示：会清空防沉迷信息,只针对帐号操作";
                    if (gameuserid.Trim() != string.Empty)
                    {
                        lblURchildren.Html = xlj.GetChildDisInfo(gameuserid);
                    }
                    else
                    {
                        lblURchildren.Html = "提示:暂无防沉迷信息";
                    }

                    
                    txtaNote.AllowBlank = false;
                    break;
                case 5://踢号工具

                    if (gameuserid.Trim() != string.Empty)
                    {
                        lblURnoonline.Html = xlj.GetUserOnline(gameuserid);
                    }
                    else
                    {
                        lblURnoonline.Html = "提示:暂无在线信息";
                    }

                    lbltooltips.Text = "工具使用提示：暂时无法实时强制下线,主要用于在线信息造成的登录异常,针对帐号及所属角色";

                    
                    txtaNote.AllowBlank = false;
                    break;
                case 6://改名工具

                    lbltooltips.Text = "工具使用提示：如果没有选择角色,则针对帐号操作";

                    txtURchangename.AllowBlank = false;
                    txtaNote.AllowBlank = false;
                    break;
                case 7://改服工具
                    cbURchangeserver.SyncSize();

                    lbltooltips.Text = "工具使用提示：只针对角色操作";

                    cbURchangeserver.AllowBlank = false;
                    cbGameRole.AllowBlank = false;
                    txtaNote.AllowBlank = false;
                    break;
                case 8://帐号借用

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";

                    txtPwd.AllowBlank = false;
                    txtPwdR.AllowBlank = false;
                    txtaNote.AllowBlank = false;
                    break;
                case 9://帐号归还

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;
                case 10://清空身份证

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;
                case 11://清空邮箱

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;
                case 12://清空密保

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;
                case 13://清二级密码

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;
                case 14://密码初始化

                    lbltooltips.Text = "工具使用提示：只针对帐号操作";
                    txtaNote.AllowBlank = false;
                    break;

            }
            //重新计算组件宽度高度
            ToolsWindow.SyncSize();
        }
    </script>

    <script type="text/javascript">

        var getGameUdetail = function(value) {
            var r = StoreGameUser.getById(value);
            if (Ext.isEmpty(r)) {
                lblURdetail.setText("暂无基础信息", true)
            }
            lblURdetail.setText(r.data.F_UserDetail, false)
            lblURdetail2.setText("<br /><br /><br /><br />", false)
        }

        var getGameRdetail = function(value) {
            var r = StoreGameRole.getById(value);
            if (Ext.isEmpty(r)) {
                lblURdetail2.setText("暂无基础信息", true)
            }
            lblURdetail2.setText(r.data.F_RoleDetail, false)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server">
    </ext:ScriptManager>
    <ext:Store runat="server" ID="StoreGameUser">
        <Proxy>
            <ext:HttpProxy Method="POST" Url="gameusers.ashx" />
            <%--<ext:ScriptTagProxy  Timeout="50" Url="http://localhost/webservice/gameusers.ashx" />--%>
        </Proxy>
        <BaseParams>
            <ext:Parameter Name="GameCode" Value="#{cbGameCode}.getValue()" Mode="Raw" />
            <ext:Parameter Name="GameZoneID" Value="#{cbGameZoneID}.getValue()" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_UserID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_UserID" />
                    <ext:RecordField Name="F_UserName" />
                    <ext:RecordField Name="F_IsAdult" />
                    <ext:RecordField Name="F_IsLock" />
                    <ext:RecordField Name="F_Level" />
                    <ext:RecordField Name="F_ActiveTime" />
                    <ext:RecordField Name="F_IsProtect" />
                    <ext:RecordField Name="F_UserDetail" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameRole">
        <Reader>
            <ext:JsonReader ReaderID="F_RoleID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_RoleID" />
                    <ext:RecordField Name="F_RoleName" />
                    <ext:RecordField Name="F_RoleDetail" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameLockType">
        <Reader>
            <ext:JsonReader ReaderID="name" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="name" />
                    <ext:RecordField Name="value" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameServer">
        <Reader>
            <ext:JsonReader ReaderID="F_ZoneID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_ZoneID" />
                    <ext:RecordField Name="F_ZoneName" />
                    <ext:RecordField Name="F_ZoneDetail" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        <ext:Store runat="server" ID="StoreGameZoneID"   AutoLoad="true">
        <Reader>
            <ext:JsonReader ReaderID="F_ID">
                <Fields>
                    <ext:RecordField Name="F_ID" />
                    <ext:RecordField Name="F_Name" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Window ID="ToolsWindow" Border="false" Modal="true" Floating="false" ShowOnLoad="True"
        Width="650" runat="server" Collapsible="true" Icon="UserGo" Title="游戏工具" HideBorders="True"
        HideMode="Display" AutoHeight="True" Minimizable="True">
        <Body>
            <ext:FormPanel ID="FormPanelTools" MonitorValid="true" MonitorPoll="500" Shadow="Sides"
                runat="server" Header="true" BodyStyle="padding:15px;" ButtonAlign="Center">
                <Body>
                    <ext:FormLayout ID="FormLayout1" runat="server" LabelAlign="Top">
                        <ext:Anchor Horizontal="90%">
                            <ext:Panel ID="Panel2" Border="false" runat="server" AutoWidth="true" BodyStyle="padding:3px;">
                                <Body>
                                    <ext:TableLayout ID="TableLayout1" runat="server" Columns="4">
                                        <ext:Cell>
                                            <ext:Label ID="Label5" Text="游戏名称:" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbGameCode" runat="server" Width="230" EmptyText="请选择游戏名称" Editable="false"
                                                AllowBlank="False" LoadingText="正在加载..." Cls="icon-servergo">
                                                <AjaxEvents>
                                                    <Select OnEvent="Select_cbGameCode">
                                                    </Select>
                                                </AjaxEvents>
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label7" Text="&nbsp;&nbsp;大区:" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbGameZoneID" runat="server" Width="230" EmptyText="请选择大区" Editable="false" StoreID="StoreGameZoneID" DisplayField="F_Name"
                                                ValueField="F_ID" AllowBlank="False" Mode="Local" ForceSelection="true" LoadingText="正在加载..." Cls="icon-serverchart" >
                                                <AjaxEvents>
                                                    <Select OnEvent="Select_cbGameZoneID">
                                                        <EventMask Target="CustomTarget" CustomTarget="FormPanelTools" Msg="工具初始化中..." ShowMask="true" />
                                                    </Select>
                                                </AjaxEvents>
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell ColSpan="4">
                                            <ext:Label ID="Label3" Text="&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label8" Text="用户名:" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell ColSpan="3">
                                            <ext:ComboBox ID="cbGameUser" runat="server" StoreID="StoreGameUser" DisplayField="F_UserName"
                                                ValueField="F_UserID" TypeAhead="false" LoadingText="Searching..." Width="230"
                                                PageSize="5" HideTrigger="true" ItemSelector="div.search-item" MinChars="1" MsgTarget="Under"
                                                BlankText="请输入至少前两位;(测试输入10)" EmptyText="请输入用户名" AllowBlank="False" Cls="icon-usermagnify">
                                                <Template ID="Template1" runat="server">
                                                       <tpl for=".">
                                                          <div class="search-item">
                                                             <h3><span>编号:{F_UserID}</span>  用户名:{F_UserName}</h3>
                                                            成年:{F_IsAdult} 封停:{F_IsLock}等级:{F_Level}密保:{F_IsProtect}<br />活动时间:{F_ActiveTime}
                                                          </div>
                                                       </tpl>
                                                </Template>
                                                <Listeners>
                                                    <Select Handler="getGameUdetail(#{cbGameUser}.getValue());" />
                                                </Listeners>
                                                <AjaxEvents>
                                                    <Select OnEvent="Select_cbGameUser">
                                                        <EventMask Target="CustomTarget" CustomTarget="FormPanelTools" Msg="查询角色中..." ShowMask="true" />
                                                    </Select>
                                                </AjaxEvents>
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label6" Text="&nbsp;&nbsp;角 色:" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell ColSpan="3">
                                            <ext:ComboBox ID="cbGameRole" runat="server" StoreID="StoreGameRole" DisplayField="F_RoleName"
                                                ValueField="F_RoleID" Width="230" EmptyText="没有选择角色" Mode="Local" ForceSelection="true"
                                                LoadingText="正在加载..." >
                                                <%-- <Items><ext:ListItem Text="没有选择角色" Value="0" /></Items>--%>
                                                <Listeners>
                                                    <Select Handler="getGameRdetail(#{cbGameRole}.getValue());" />
                                                </Listeners>
                                                <AjaxEvents>
                                                    <Select OnEvent="Select_cbGameRole">
                                                    </Select>
                                                </AjaxEvents>
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell ColSpan="4">
                                            <ext:RadioGroup ID="rbgTool" runat="server" Width="550" ColumnsNumber="5" Height="80">
                                                <Items>
                                                    <ext:Radio ID="rdURdetail" runat="server" BoxLabel="【基础信息】" Checked="true" DataIndex="0"
                                                        Height="10" />
                                                    <ext:Radio ID="rdURlock" runat="server" BoxLabel="【封停工具】" DataIndex="1" Height="10" />
                                                    <ext:Radio ID="rdURnolock" runat="server" BoxLabel="【查询/解封】" DataIndex="2" Height="10" />
                                                    <ext:Radio ID="rdURlockip" runat="server" BoxLabel="【封IP工具】" DataIndex="3" Height="10" />
                                                    <ext:Radio ID="rdUchildclear" runat="server" BoxLabel="【清空防沉迷】" DataIndex="4" Height="10" />
                                                    <ext:Radio ID="rdURnoonline" runat="server" BoxLabel="【踢号工具】" DataIndex="5"  Height="10"/>
                                                    <ext:Radio ID="Radio2" runat="server" BoxLabel="【改名工具】" DataIndex="6" Height="10"/>
                                                    <ext:Radio ID="Radio3" runat="server" BoxLabel="【改服工具】" DataIndex="7" Height="10"/>
                                                    <ext:Radio ID="Radio4" runat="server" BoxLabel="【帐号借用】" DataIndex="8" Height="10"/>
                                                    <ext:Radio ID="Radio5" runat="server" BoxLabel="【帐号归还】" DataIndex="9" Height="10"/>
                                                    <ext:Radio ID="Radio1" runat="server" BoxLabel="【清空身份证】" DataIndex="10" />
                                                    <ext:Radio ID="Radio6" runat="server" BoxLabel="【清空邮箱】" DataIndex="11" />
                                                    <ext:Radio ID="Radio7" runat="server" BoxLabel="【清空密保】" DataIndex="12" />
                                                    <ext:Radio ID="Radio8" runat="server" BoxLabel="【清二级密码】" DataIndex="13" />
                                                    <ext:Radio ID="Radio9" runat="server" BoxLabel="【密码初始化】" DataIndex="14" />
                                                </Items>
                                                <AjaxEvents>
                                                    <Change OnEvent="rbgTool_Click" ViewStateMode="Include" />
                                                </AjaxEvents>
                                            </ext:RadioGroup>
                                        </ext:Cell>
                                        <ext:Cell ColSpan="4">
                                            <ext:Panel ID="WizardPanel" runat="server" BodyStyle="padding:1px" Border="false"
                                                AutoHeight="True" AutoShow="true" ActiveIndex="0" Width="590px">
                                                <Body>
                                                    <ext:CardLayout ID="WizardLayout" runat="server" AutoShow="true">
                                                        <%--基础信息--%>
                                                        <ext:Panel ID="pnlURdetail" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="lblURdetail" Height="30" Html="提示:暂无基础信息<br /><br /><br /><br />"
                                                                    runat="server" />
                                                                <br />
                                                                <br />
                                                                <ext:Label ID="lblURdetail2" Height="30" Html="<br /><br /><br />" runat="server" /><br />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--封停工具--%>
                                                        <ext:Panel ID="pnlURlock" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:TableLayout ID="TableLayout2" runat="server" Columns="2">
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label1" Height="30" Text="封停时间:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:ComboBox ID="cbURlocktime" runat="server" Editable="false" StoreID="StoreGameLockType"
                                                                            DisplayField="name" ValueField="value" LoadingText="正在加载..." EmptyText="请选择"
                                                                            Mode="Local" AllowBlank="False">
                                                                            <%--  <SelectedItem Value="0" />
                                                                            <Items>
                                                                                <ext:ListItem Text="15分钟" Value="0" />
                                                                                <ext:ListItem Text="30分钟" Value="1" />
                                                                                <ext:ListItem Text="1小时" Value="2" />
                                                                                <ext:ListItem Text="2小时" Value="3" />
                                                                                <ext:ListItem Text="6小时" Value="4" />
                                                                                <ext:ListItem Text="12小时" Value="5" />
                                                                                <ext:ListItem Text="一天" Value="6" />
                                                                                <ext:ListItem Text="二天" Value="7" />
                                                                                <ext:ListItem Text="三天" Value="8" />
                                                                                <ext:ListItem Text="四天" Value="9" />
                                                                                <ext:ListItem Text="五天" Value="10" />
                                                                                <ext:ListItem Text="6天" Value="11" />
                                                                                <ext:ListItem Text="7天" Value="12" />
                                                                                <ext:ListItem Text="15天" Value="13" />
                                                                                <ext:ListItem Text="30天" Value="14" />
                                                                                <ext:ListItem Text="半年" Value="15" />
                                                                                <ext:ListItem Text="一年" Value="16" />
                                                                                <ext:ListItem Text="永久" Value="17" />
                                                                            </Items>--%>
                                                                        </ext:ComboBox>
                                                                    </ext:Cell>
                                                                </ext:TableLayout>
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--解封工具--%>
                                                        <ext:Panel ID="pnlURnolock" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="lblURnolock" Height="30" Html="提示:暂无封停信息" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%-- 封IP工具--%>
                                                        <ext:Panel ID="PnlURlockip" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:TableLayout ID="TableLayout3" runat="server" Columns="4">
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label9" Height="30" Text="封停IP地址:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:TextField ID="txtURlockip" AllowBlank="false" runat="server" Width="180" MaxLength="15"
                                                                            MaxLengthText="IP地址最多15位" EmptyText="请输入开始IP地址" Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/"
                                                                            RegexText="请输入正确的IP地址">
                                                                        </ext:TextField>
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label13" Height="30" Html="&nbsp;-&nbsp;" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:TextField ID="txtURlockipE" AllowBlank="false" runat="server" Width="180" MaxLength="15"
                                                                            MaxLengthText="IP地址最多15位" EmptyText="请输入结束IP地址" Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/"
                                                                            RegexText="请输入正确的IP地址">
                                                                        </ext:TextField>
                                                                    </ext:Cell>
                                                                </ext:TableLayout>
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--清空防沉迷--%>
                                                        <ext:Panel ID="pnlURchildren" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="lblURchildren" Height="30" Text="提示:暂无防沉迷信息" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--踢号工具--%>
                                                        <ext:Panel ID="pnlURnoonline" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="lblURnoonline" Height="30" Text="提示:暂无在线信息" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--改名工具--%>
                                                        <ext:Panel ID="pnlURchangname" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:TableLayout ID="TableLayout4" runat="server" Columns="2">
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label4" Height="30" Text="新的名字:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:TextField ID="txtURchangename" AllowBlank="false" runat="server" Width="230"
                                                                            MaxLength="15" MaxLengthText="新名字最长为16位" EmptyText="请输入新的名字" MinLengthText="新名字最短为2位"
                                                                            MinLength="2" Cls="input username">
                                                                        </ext:TextField>
                                                                    </ext:Cell>
                                                                </ext:TableLayout>
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--改服工具--%>
                                                        <ext:Panel ID="pnlURchangeserver" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:TableLayout ID="TableLayout5" runat="server" Columns="2">
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label10" Text="新的服务器:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:ComboBox ID="cbURchangeserver" runat="server" Width="230" EmptyText="请选择" Editable="false"
                                                                            AllowBlank="False" LoadingText="正在加载..." StoreID="StoreGameServer"  Mode="Local" DisplayField="F_ZoneName" ValueField="F_ZoneID" ItemSelector="div.list-itemgamezone" SelectOnFocus="true">
                                                                            <Template ID="Template2" runat="server">
                                                                                    <tpl for=".">
                                                                                        <div  class="list-itemgamezone">
                                                                                             {F_ZoneDetail}
                                                                                        </div>
                                                                                    </tpl>
                                                                            </Template>
                                                                        </ext:ComboBox>
                                                                    </ext:Cell>
                                                                </ext:TableLayout>
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--帐号借用--%>
                                                        <ext:Panel ID="pnlUaccountout" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:TableLayout ID="TableLayout7" runat="server" Columns="2">
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label11" Text="新密码:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:TextField ID="txtPwd" AllowBlank="false" BlankText="密码还是填上吧！" MinLength="6"
                                                                            MaxLength="15" MaxLengthText="密码长度不能超过15位!" MinLengthText="密码长度介于6-15位!" InputType="Password"
                                                                            runat="server" Cls="input pwd" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:Label ID="Label12" Text="确认密码:" runat="server" />
                                                                    </ext:Cell>
                                                                    <ext:Cell>
                                                                        <ext:TextField ID="txtPwdR" AllowBlank="false" BlankText="确认密码必须输入哦" CausesValidation="true"
                                                                            InputType="Password" MinLength="6" MaxLength="15" MaxLengthText="密码长度不能超过15位!"
                                                                            MinLengthText="密码长度介于6-15位!" Validator="function ValidPwd(){if(#{txtPwdR}.getValue() != #{txtPwd}.getValue()){return '两次密码输入不一致！';}else{return true;}}"
                                                                            runat="server" Cls="input pwd" />
                                                                    </ext:Cell>
                                                                </ext:TableLayout>
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--帐号归还--%>
                                                        <ext:Panel ID="pnlUaccountback" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="lblUaccountback" Height="30" Text="提示:帐号归还工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--清空身份证--%>
                                                        <ext:Panel ID="pnlUpersonID" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="Label14" Height="30" Text="提示:清空身份证工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--清空邮箱--%>
                                                        <ext:Panel ID="pnlUemail" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="Label15" Height="30" Text="提示:清空邮箱工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--清空密保--%>
                                                        <ext:Panel ID="pnlUpswprotect" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="Label16" Height="30" Text="提示:清空密保工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--清二级密码--%>
                                                        <ext:Panel ID="pnlUseconpsw" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="Label17" Height="30" Text="提示:清二级密码工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                        <%--密码初始--%>
                                                        <ext:Panel ID="pnlpswinit" runat="server" Border="false" Header="true" AutoHeight="True">
                                                            <Body>
                                                                <ext:Label ID="Label18" Height="30" Text="提示:密码初始化工具" runat="server" />
                                                            </Body>
                                                        </ext:Panel>
                                                    </ext:CardLayout>
                                                </Body>
                                            </ext:Panel>
                                        </ext:Cell>
                                    </ext:TableLayout>
                                </Body>
                            </ext:Panel>
                        </ext:Anchor>
                        <ext:Anchor Horizontal="90%">
                            <ext:Panel ID="pnlNote" Border="false" runat="server" AutoWidth="true" Hidden="True">
                                <Body>
                                    <br />
                                    <ext:Label ID="lblnote1" Height="30" Text="【基础信息】" runat="server" />
                                    <ext:Label ID="lblnote2" Height="30" Text="使用备注:" runat="server" />
                                    <br />
                                    <ext:TextArea ID="txtaNote" runat="server" AllowBlank="true" Width="570px">
                                    </ext:TextArea>
                                    <br />
                                    <ext:Label ID="lbltooltips" runat="server" Text="工具使用提示：如果没有选择角色,则针对帐号操作" Icon="Note"
                                        Cls="dot-label" />
                                </Body>
                            </ext:Panel>
                        </ext:Anchor>
                        <ext:Anchor Horizontal="90%">
                            <ext:Panel ID="pnlSubmit" Border="false" runat="server" AutoWidth="true" BodyStyle="padding-left:100px;padding-top:10px;"
                                Hidden="True" Width="500">
                                <Body>
                                    <ext:TableLayout ID="TableLayout6" runat="server" Columns="3">
                                        <ext:Cell>
                                            <ext:Button ID="btnSubmit" Type="Submit" runat="server" Width="60" Icon="Disk" Text="提 交">
                                                <Listeners>
                                                    <Click Handler="if(#{FormPanelTools}.getForm().isValid() && #{txtaNote}.isValid()){return true;}else{#{txtaNote}.focus(true); return false;}" />
                                                </Listeners>
                                                <AjaxEvents>
                                                    <Click OnEvent="Check_btnSubmit">
                                                        <EventMask Target="CustomTarget" CustomTarget="FormPanelTools" Msg="正在提交,请稍候..."
                                                            ShowMask="true" />
                                                    </Click>
                                                </AjaxEvents>
                                            </ext:Button>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label2" runat="server" Html="&nbsp;<DIV style='width:30px;'></DIV>" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Button ID="btn" Type="Button" runat="server" Width="60" Icon="PencilGo" Text="重 填">
                                                <AjaxEvents>
                                                    <Click OnEvent="Click_btnSubmit">
                                                    </Click>
                                                </AjaxEvents>
                                            </ext:Button>
                                        </ext:Cell>
                                    </ext:TableLayout>
                                    <%--    <buttons>
                            <ext:Button ID="btnSubmit" Type="Submit" runat="server" width="60" Icon="Disk" Text="提 交">
                                <Listeners>
                                    <Click Handler="if(#{FormPanelTools}.getForm().isValid() && #{txtaNote}.isValid()){return true;}else{#{txtaNote}.focus(true); return false;}" />
                                </Listeners>
                                <AjaxEvents>
                                    <Click OnEvent="Check_btnSubmit">
                                     <EventMask Target="CustomTarget" CustomTarget="FormPanelTools" Msg="正在提交,请稍候..." ShowMask="true" />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                             <ext:Button ID="btnSubmita" Type="Submit" runat="server" width="60" Icon="Disk" Text="提 交">
                              <Listeners>
                                    <Click Handler="if(#{FormPanelTools}.getForm().isValid() && #{txtaNote}.isValid()){return true;}else{#{txtaNote}.focus(true); return false;}" />
                                </Listeners>
                                <AjaxEvents>
                                    <Click OnEvent="Check_btnSubmit">
                                     <EventMask Target="CustomTarget" CustomTarget="FormPanelTools" Msg="正在提交,请稍候..." ShowMask="true" />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                        </buttons>--%>
                                </Body>
                            </ext:Panel>
                        </ext:Anchor>
                    </ext:FormLayout>
                </Body>
                <BottomBar>
                    <ext:StatusBar ID="NewsStatus" runat="server" />
                </BottomBar>
                <Listeners>
                    <ClientValidation Handler="#{NewsStatus}.setStatus({text: valid && #{txtaNote}.isValid()? '该表单已经通过验证' : '表单未通过验证', iconCls: valid && #{txtaNote}.isValid() ? 'icon-accept' : 'icon-exclamation'});" />
                </Listeners>
            </ext:FormPanel>
        </Body>
        <Listeners>
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
