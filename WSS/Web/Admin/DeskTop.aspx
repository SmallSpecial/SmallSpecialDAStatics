<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeskTop.aspx.cs" Inherits="Web.Admin.DeskTop" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>WSS统计系统</title>
    <link type="text/css" rel="Stylesheet" href="style/master.css" media="all" />
    <link type="text/css" rel="Stylesheet" href="../style/master.css" media="all" />

    <script type="text/javascript">
        var alignPanels = function() {
            // pnlSample.getEl().alignTo(Ext.getBody(), "tr", [-405, 5], false);
            // WinGongGao.show();
            //  Window1.show();

        }

        var template = '<span style="color:{0};">{1}</span>';

        var change = function(value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value);
        }

        var pctChange = function(value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value + '%');
        }
    </script>

    <script type="text/javascript">
        var loadPage = function(tabPanel, node) {
            var tab = tabPanel.getItem(node.id);
            if (!tab) {
                tab = tabPanel.add({
                    id: node.id,
                    title: node.text,
                    closable: true,
                    autoLoad: {
                        showMask: true,
                        url: node.attributes.href,
                        mode: 'iframe',
                        maskMsg: '正在加载 ' + node.text + '...'
                    },
                    listeners: {
                        update: {
                            fn: function(tab, cfg) {
                                cfg.iframe.setHeight(cfg.iframe.getSize().height - 20);
                            },
                            scope: this,
                            single: true
                        }
                    }
                });
            }
            tabPanel.setActiveTab(tab);
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug"  CleanResourceUrl="False" >
        <Listeners>
            <DocumentReady Handler="alignPanels();" />
            <WindowResize Handler="alignPanels();" />
        </Listeners>
    </ext:ScriptManager>
    <ext:Panel ID="DeskTopPanel" ContextMenuID="MyContextMenu" runat="server" Header="false"
        Border="false" AutoHeight="true" AutoWidth="true" BodyStyle="background:url(images/desktop.jpg);">
        <Body>
            <ext:Desktop ID="MyDesktop" runat="server" BackgroundColor="Black" ShortcutTextColor="White"
                Wallpaper="images/desktop.jpg">
                <StartButton Text="开始" IconCls="start-button" />
                <Body>
                    <ext:Panel ID="pnlSample" runat="server" Title="最新公告" Height="300" Width="400" Border="true"
                        Collapsible="true" Icon="NewRed" Visible="false">
                        <Body>
                        </Body>
                    </ext:Panel>
                </Body>
                <Modules>
                    <ext:DesktopModule ModuleID="DesktopModule1" WindowID="DesktopWindow1">
                        <Launcher ID="Launcher1" runat="server" Text="用户统计" Icon="Add" />
                    </ext:DesktopModule>
                    <ext:DesktopModule ModuleID="DesktopModule2" WindowID="winBrowser2">
                        <Launcher ID="Launcher2" runat="server" Text="在线统计" Icon="UserBrown" />
                    </ext:DesktopModule>
                    <ext:DesktopModule ModuleID="DesktopModule3" WindowID="winstats">
                    </ext:DesktopModule>
                </Modules>
                <%--桌面图标--%>
                <Shortcuts>
                 <ext:DesktopShortcut ModuleID="DesktopModule3" Text="<span style='font-size:10px;'>全部统计</span>"
                        IconCls="shortcut-icon icon-news48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="用户统计" IconCls="shortcut-icon icon-user48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="在线统计" IconCls="shortcut-icon icon-window48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="付费消费情况" IconCls="shortcut-icon icon-grid48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="产品受关注度" IconCls="shortcut-icon icon-shortcut-icon icon-wold48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="职业等级分布" IconCls="shortcut-icon icon-settings48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="经济系统货币" IconCls="shortcut-icon icon-grid48" />
                    <ext:DesktopShortcut ModuleID="DesktopModule3" Text="贵重道具产出&消耗" IconCls="shortcut-icon icon-grid48" />
                   
                </Shortcuts>
                <StartMenu Width="260" Height="250" ToolsWidth="120">
                    <ToolItems>
                        <ext:MenuItem Text="系统设置" Icon="Wrench">
                            <Listeners>
                                <Click Handler="#{winBrowser2}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem Text="退出系统" Icon="Disconnect">
                            <AjaxEvents>
                                <Click OnEvent="Logout_Click">
                                    <EventMask ShowMask="true" Msg="退出系统准备中..." MinDelay="1000" />
                                </Click>
                            </AjaxEvents>
                        </ext:MenuItem>
                    </ToolItems>
                </StartMenu>
            </ext:Desktop>
        </Body>
    </ext:Panel>
    <ext:Window ID="Window1" runat="server" Maximizable="false" Collapsible="True" Icon="Cup"
        Title="用户统计" ShowOnLoad="False" AutoShow="False" X="100" AutoHeight="True" Collapsed="True"
        Minimizable="False" Y="5" Closable="False" Resizable="False">
        <Body>
            <ext:MenuPanel ID="MenuPanel1" runat="server" Title="" SaveSelection="false">
                <Menu>
                    <Items>
                        <ext:MenuItem ID="MenuItem1" runat="server" Text="用户统计1" Icon="UserGreen">
                            <Listeners>
                                <Click Handler="#{winBrowser1}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem2" runat="server" Text="用户统计2" Icon="UserFemale">
                            <Listeners>
                                <Click Handler="#{winBrowser2}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                    </Items>
                </Menu>
            </ext:MenuPanel>
        </Body>
    </ext:Window>
    <ext:Window ID="Window2" runat="server" Maximizable="false" Collapsible="True" Icon="Database"
        Title="在线统计" ShowOnLoad="False" AutoShow="True" X="330" AutoHeight="True" Collapsed="True"
        Minimizable="False" Y="5" Closable="False" Resizable="False" AnimCollapse="True">
        <Body>
            <ext:MenuPanel ID="MenuPanel2" runat="server" Title="" SaveSelection="false">
                <Menu>
                    <Items>
                        <ext:MenuItem ID="MenuItem3" runat="server" Text="用户统计1" Icon="UserGreen">
                            <Listeners>
                                <Click Handler="#{winBrowser1}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem4" runat="server" Text="用户统计2" Icon="UserFemale">
                            <Listeners>
                                <Click Handler="#{winBrowser2}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem5" runat="server" Text="用户统计3" Icon="UserFemale">
                            <Listeners>
                                <Click Handler="#{winBrowser3}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem6" runat="server" Text="用户统计4" Icon="UserFemale">
                            <Listeners>
                                <Click Handler="#{winBrowser4}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem7" runat="server" Text="用户统计5" Icon="UserFemale">
                            <Listeners>
                                <Click Handler="#{winBrowser5}.show()" />
                            </Listeners>
                        </ext:MenuItem>
                    </Items>
                </Menu>
            </ext:MenuPanel>
        </Body>
    </ext:Window>
    <ext:DesktopWindow ID="DesktopWindow1" runat="server" Icon="DateGo" Title="用户统计"
        Width="1060" Height="760" Border="False" Collapsible="True" Plain="True" Maximizable="true"
        Resizable="True" ExpandOnShow="True">
        <Body>
            <ext:BorderLayout ID="BorderLayout2" runat="server">
                <North>
                    <ext:Panel ID="Panel6" runat="Server" Border="False">
                        <Body>
                            <topbar>
                           <ext:Toolbar ID="Toolbar11" runat="server">
                           <Items>
                        <ext:ToolbarSeparator/>
                         <ext:ToolbarButton  runat="server" Icon="User" Text="用户状态" >
                            <Listeners>
                                <Click Handler="onItemClick1('用户形态','../StatF/UserDay.aspx');" />                                
                            </Listeners>
                        </ext:ToolbarButton>

                        <ext:ToolbarSeparator/>
                                                 <ext:ToolbarButton  runat="server" Icon="User" Text="状态比例" >
                            <Listeners>
                                <Click Handler="onItemClick1('状态比例','../StatF/UserDay.aspx');" />                                
                            </Listeners>
                        </ext:ToolbarButton>

                        <ext:ToolbarFill />
 

                    </Items>
                </ext:Toolbar>
            </topbar>
                        </Body>
                    </ext:Panel>
                </North>
                <Center>
                    <ext:Panel ID="Panel7" runat="Server" Border="False">
                        <Body>
                            <ext:FitLayout ID="FitLayout3" runat="server">
                                <ext:TabPanel ID="TabPanel1" runat="server">
                                    <Tabs>
                                        <ext:Tab ID="Tab2" runat="server" Title="总体情况" BodyStyle="padding: 1px;" AutoScroll="True">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout4" runat="server">
                                                    <ext:Panel ID="Panel8" runat="server" Frame="true" BodyBorder="False">
                                                        <Body>
                                                            <ext:Panel ID="Panel9" runat="server" Title="描述" BodyStyle="padding:3px;" Frame="true"
                                                                Icon="CalendarViewDay">
                                                                <Body>
                                                                    最新统计日期:2012-1-1
<br />
                                            总注册量:111
<br />
                                            总激活量:
<br />
                                            昨天登录量
<br />
                                            昨天.....
<br />
                                            昨天平均在线
<br />
                                            昨天总在线时长:
                       </Body>
                                                            </ext:Panel>
                                                        </Body>
                                                    </ext:Panel>
                                                </ext:FitLayout>
                                            </Body>
                                        </ext:Tab>
                                    </Tabs>
                                </ext:TabPanel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </Center>
                <South MarginsSummary="5 5 2 5">
                    <ext:Panel ID="Panel10" runat="server" BodyStyle="padding: 3px;" Frame="true">
                        <Body>
                        </Body>
                    </ext:Panel>
                </South>
            </ext:BorderLayout>

            <script type="text/javascript">
                function onItemClick1(htext, hhref) {

                    var tab = TabPanel1.getItem(htext);
                    if (!tab) {
                        tab = TabPanel1.add({
                            id: htext,
                            title: htext,
                            closable: true,
                            autoLoad: {
                                showMask: true,
                                url: hhref,
                                mode: 'iframe',
                                maskMsg: '正在加载 ' + htext + '...'
                            },
                            listeners: {
                                update: {
                                    fn: function(tab, cfg) {
                                        cfg.iframe.setHeight(cfg.iframe.getSize().height - 1);
                                    },
                                    scope: this,
                                    single: true
                                }
                            }
                        });
                    }
                    TabPanel1.setActiveTab(tab);
                }
            </script>

        </Body>
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winstats" runat="server" Icon="DateGo" Title="统计系统" Width="800"
        Height="500" Border="False" Collapsible="True" Plain="True" Maximizable="true"
        Resizable="True" ExpandOnShow="True"  Y="10">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North>
                    <ext:Panel ID="Panel2" runat="Server" Border="False">
                        <Body>
                            <topbar>
                           <ext:Toolbar ID="Toolbar1" runat="server">
                           <Items>
                        <ext:ToolbarSeparator/>

                          <ext:ToolbarButton ID="ToolbarButton0" runat="server" Icon="User" Text="用户统计">
                            <Menu>
                              <ext:Menu ID="Menu1" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem8" runat="server" Icon="User" Text="用户状态" Href="../StatF/UserDay.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuSeparator ID="MenuSeparator2" runat="server" />
                                     <ext:MenuItem ID="MenuItem10" runat="server" Icon="User" Text="状态比例" Href="../StatF/UserDayScale.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem10);" />
                                        </Listeners>
                                    </ext:MenuItem>
      
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarSeparator/>
                        
                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" />
                        
                        <ext:ToolbarButton ID="ToolbarButton1" runat="server" Icon="UserGreen" Text="在线统计">
                            <Menu>
                              <ext:Menu ID="Menu4" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem19" runat="server" Icon="UserGreen" Text="平均在线" Href="../StatF/UserOnlineDay.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem12" runat="server" Icon="UserGreen" Text="最高在线" Href="../StatF/UserOnlineDayPCU.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem12);" />
                                        </Listeners>
                                    </ext:MenuItem>

                                    <ext:MenuSeparator ID="MenuSeparator1" runat="server" />
                                    <ext:MenuItem ID="MenuItem14" runat="server" Icon="UserGreen" Text="地区对比" Href="../StatF/UserOnlineDayAreaCol.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem14);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                                                  
                                    <ext:MenuItem ID="MenuItem11" runat="server" Icon="UserGreen" Text="分布地区" Href="../StatF/UserOnlineDayAreaMap.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem11);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuSeparator ID="MenuSeparator3" runat="server" />
                                   <ext:MenuItem ID="MenuItem22" runat="server" Icon="UserFemale" Text="用户活动" Href="../StatF/UserOnlineDayUser.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem22);" />
                                        </Listeners>
                                    </ext:MenuItem>  
                                    <ext:MenuItem ID="MenuItem15" runat="server" Icon="UserGreen" Text="每小时活动" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem15);" />
                                        </Listeners>
                                    </ext:MenuItem>                              
         
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        
                        
                         <ext:ToolbarButton ID="ToolbarButton2" runat="server" Icon="MoneyYen" Text="付费消费情况">
                            <%--<Menu>
                              <ext:Menu ID="Menu2" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem16" runat="server" Icon="MoneyYen" Text="付费消费情况" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>--%>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ToolbarButton3" runat="server" Icon="Lightbulb" Text="产品受关注度">
                            <%--<Menu>
                              <ext:Menu ID="Menu3" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem17" runat="server" Icon="Lightbulb" Text="产品受关注度" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>--%>
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                        <ext:ToolbarButton ID="ToolbarButton4" runat="server" Icon="AwardStarGold2" Text="职业等级分布">
                            <%--<Menu>
                              <ext:Menu ID="Menu5" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem18" runat="server" Icon="AwardStarGold2" Text="职业等级分布" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>--%>
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                                                <ext:ToolbarButton ID="ToolbarButton6" runat="server" Icon="Rainbow" Text="经济系统货币">
                            <%--<Menu>
                              <ext:Menu ID="Menu7" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem21" runat="server" Icon="Rainbow" Text="经济系统货币" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>--%>
 
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                        
                           <ext:ToolbarButton ID="ToolbarButton5" runat="server" Icon="BookOpenMark" Text="贵重道具产出&消耗">
                            <%--<Menu>
                              <ext:Menu ID="Menu6" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem20" runat="server" Icon="BookOpenMark" Text="贵重道具产出&消耗" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>--%>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarFill />
                        
                       
                         <ext:ToolbarButton runat="server" Icon="ComputerGo" Text="退出系统">
<%--                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Accept');" />                                
                            </Listeners>--%>
                             <AjaxEvents>
                                <Click OnEvent="Logout_Click">
                                    <EventMask ShowMask="true" Msg="退出系统准备中..." MinDelay="1000" />
                                </Click>
                            </AjaxEvents>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="退出系统" />
                            </ToolTips>
                        </ext:ToolbarButton>

                    </Items>
                </ext:Toolbar>
            </topbar>
                        </Body>
                    </ext:Panel>
                </North>
                <Center>
                    <ext:Panel ID="Panel1" runat="Server" Border="False">
                        <Body>
                            <ext:FitLayout ID="FitLayout2" runat="server">
                                <ext:TabPanel ID="TabPanelstats" runat="server">
                                    <Tabs>
                                        <ext:Tab ID="Tab3" runat="server" Title="基本信息" BodyStyle="padding: 0px;" AutoScroll="true">
                                            <AutoLoad Url="../StatF/UserOnlineMain.aspx" Mode="IFrame" ShowMask="True" />
                                        </ext:Tab>
                                    </Tabs>
                                </ext:TabPanel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </Center>
                <South MarginsSummary="5 5 2 5">
                    <ext:Panel ID="Panel3" runat="server" BodyStyle="padding: 3px;" Frame="true">
                        <Body>
                        </Body>
                    </ext:Panel>
                </South>
            </ext:BorderLayout>

            <script type="text/javascript">
                function onItemClick(menuItem) {

                    var tab = TabPanelstats.getItem(menuItem.text);
                    if (!tab) {
                        tab = TabPanelstats.add({
                            id: menuItem.text,
                            title: menuItem.text,
                            closable: true,
                            autoscroll:true,
                            autoLoad: {
                                showMask: true,
                                url: menuItem.href,
                                mode: 'iframe',
                                maskMsg: '正在加载 ' + menuItem.text + '...'
                            },
                            listeners: {
                                update: {
                                    fn: function(tab, cfg) {
                                        cfg.iframe.setHeight(tab.iframe.getSize().height - 1);
                                    },
                                    scope: this,
                                    single: true
                                }
                            }
                        });
                    }
                    TabPanelstats.setActiveTab(tab);
                }
            </script>

        </Body>
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winBrowser1" Collapsible="true" runat="server" Title="用户统计"
        Icon="World" Width="1050" Height="700" CenterOnLoad="true" ConstrainHeader="true">
        <LoadMask ShowMask="true" />
        <AutoLoad Url="~/StatF/UserDay.aspx" Mode="IFrame" ShowMask="true" AutoDataBind="true" />
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winBrowser2" Collapsible="true" runat="server" Title="用户统计"
        Icon="World" Width="1050" Height="810" CenterOnLoad="true" ConstrainHeader="true">
        <LoadMask ShowMask="true" />
        <AutoLoad Url="~/StatF/UserOnlineDayArea.aspx" Mode="IFrame" ShowMask="true" AutoDataBind="true" />
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winBrowser3" Collapsible="true" runat="server" Title="用户统计"
        Icon="World" Width="1050" Height="700" CenterOnLoad="true" ConstrainHeader="true">
        <LoadMask ShowMask="true" />
        <AutoLoad Url="~/StatF/UserOnlineDayHour.aspx" Mode="IFrame" ShowMask="true" AutoDataBind="true" />
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winBrowser4" Collapsible="true" runat="server" Title="用户统计"
        Icon="World" Width="1050" Height="700" CenterOnLoad="true" ConstrainHeader="true">
        <LoadMask ShowMask="true" />
        <AutoLoad Url="~/StatF/UserOnlineDayUser.aspx" Mode="IFrame" ShowMask="true" AutoDataBind="true" />
    </ext:DesktopWindow>
    <ext:DesktopWindow ID="winBrowser5" Collapsible="true" runat="server" Title="用户统计"
        Icon="World" Width="1050" Height="700" CenterOnLoad="true" ConstrainHeader="true">
        <LoadMask ShowMask="true" />
        <AutoLoad Url="~/StatF/UserOnlineDay.aspx" Mode="IFrame" ShowMask="true" AutoDataBind="true" />
    </ext:DesktopWindow>
    <ext:Menu ID="MyContextMenu" runat="server" AllowOtherMenus="false">
        <Items>
            <ext:MenuItem ID="MenuItem9" runat="server" Text="刷新桌面" Icon="ArrowRefresh">
                <Listeners>
                    <Click Handler="location.reload()" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="ReFreshCache" runat="server" Text="更新系统缓存" Icon="Reload">
                <AjaxEvents>
                    <Click OnEvent="ReFreshCache_Click">
                        <EventMask ShowMask="true" Msg="缓存更新中..." MinDelay="2000" />
                    </Click>
                </AjaxEvents>
            </ext:MenuItem>
            <ext:MenuItem ID="MI_TechSport" runat="server" Text="技术支持" Icon="TelephoneLink">
                <%--<Listeners>
                    <Click Handler="MI_TechSport_Click" />
                </Listeners>--%>
            </ext:MenuItem>
            <ext:MenuItem ID="MI_SignOut" runat="server" Text="注销系统" Icon="Cancel">
                <AjaxEvents>
                    <Click OnEvent="Logout_Click">
                        <EventMask ShowMask="true" Msg="系统注销中..." MinDelay="1000" />
                    </Click>
                </AjaxEvents>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    </form>
</body>
</html>
