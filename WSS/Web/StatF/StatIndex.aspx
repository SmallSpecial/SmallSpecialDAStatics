<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatIndex.aspx.cs" Inherits="WSS.Web.StatF.StatIndex" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>统计系统</title>
        <script type="text/javascript">
            var alignPanels = function() {
                //winstats.maximize();
            }</script>
</head>
<body>
    <form id="form1" runat="server">

<ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug"  CleanResourceUrl="False" >
        <Listeners>
            <DocumentReady Handler="alignPanels();" />
            <WindowResize Handler="alignPanels();" />
        </Listeners>
    </ext:ScriptManager>
    
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>

 <ext:FitLayout ID="FitLayout3" runat="server">
        <ext:Panel ID="Panel21" runat="Server" Border="False"   Icon="DateGo" Title="统计系统">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North>
                    <ext:Panel ID="Panel2" runat="Server" Border="False" Height="27">
                     <body>
                     
                 
                            <topbar>
                           <ext:Toolbar ID="Toolbar1" runat="server">
                           <Items>
                        <ext:ToolbarSeparator/>

                          <ext:ToolbarButton ID="ToolbarButton0" runat="server" Icon="User" Text="用户统计">
                            <Menu>
                              <ext:Menu ID="Menu1" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem8" runat="server" Icon="User" Text="用户状态走势" Href="../StatF/UserDay.aspx" >
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuSeparator ID="MenuSeparator2" runat="server" />
                                     <ext:MenuItem ID="MenuItem10" runat="server" Icon="User" Text="用户日统计" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem ID="MenuItem11" runat="server" Icon="User" Text="用户日统计" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem ID="MenuItem12" runat="server" Icon="User" Text="用户日统计" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem ID="MenuItem13" runat="server" Icon="User" Text="用户日统计" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem8);" />
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
     
                                    <ext:MenuItem ID="MenuItem19" runat="server" Icon="UserGreen" Text="平均在线走势" Href="../StatF/UserOnlineDay.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>

                                    <ext:MenuSeparator ID="MenuSeparator1" runat="server" />
                                    <ext:MenuItem ID="MenuItem14" runat="server" Icon="UserGreen" Text="在线用户分布" Href="../StatF/UserOnlineDayArea.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem14);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                   <ext:MenuItem ID="MenuItem22" runat="server" Icon="UserFemale" Text="用户活动走势" Href="../StatF/UserOnlineDayUser.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem22);" />
                                        </Listeners>
                                    </ext:MenuItem>  
                                    <ext:MenuItem ID="MenuItem15" runat="server" Icon="UserGreen" Text="每小时活动走势" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem15);" />
                                        </Listeners>
                                    </ext:MenuItem>                              
         
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        
                        
                         <ext:ToolbarButton ID="ToolbarButton2" runat="server" Icon="MoneyYen" Text="付费消费情况">
                            <Menu>
                              <ext:Menu ID="Menu2" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem16" runat="server" Icon="MoneyYen" Text="付费消费情况 2" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ToolbarButton3" runat="server" Icon="Lightbulb" Text="产品受关注度">
                            <Menu>
                              <ext:Menu ID="Menu3" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem17" runat="server" Icon="Lightbulb" Text="付费消费情况 2" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                        <ext:ToolbarButton ID="ToolbarButton4" runat="server" Icon="AwardStarGold2" Text="职业等级分布">
                            <Menu>
                              <ext:Menu ID="Menu5" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem18" runat="server" Icon="AwardStarGold2" Text="付费消费情况 2" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                                                <ext:ToolbarButton ID="ToolbarButton6" runat="server" Icon="Rainbow" Text="经济系统货币">
                            <Menu>
                              <ext:Menu ID="Menu7" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem21" runat="server" Icon="Rainbow" Text="付费消费情况 2" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>
 
                        </ext:ToolbarButton>
                        <ext:ToolbarSeparator/>
                        
                           <ext:ToolbarButton ID="ToolbarButton5" runat="server" Icon="BookOpenMark" Text="贵重道具产出&消耗">
                            <Menu>
                              <ext:Menu ID="Menu6" runat="server">
                                <Items>
     
                                    <ext:MenuItem ID="MenuItem20" runat="server" Icon="BookOpenMark" Text="付费消费情况 2" Href="../StatF/UserOnlineDayHour.aspx">
                                        <Listeners>
                                            <Click Handler="e.stopEvent();onItemClick(MenuItem19);" />
                                        </Listeners>
                                    </ext:MenuItem>                     
                                </Items>
                              </ext:Menu>
                            </Menu>
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
            </topbar>    </body>
                    
                    </ext:Panel>
                </North>
                <Center>
                    <ext:Panel ID="Panel1" runat="Server" Border="False">
                        <Body>
                            <ext:FitLayout ID="FitLayout2" runat="server">
                                <ext:TabPanel ID="TabPanelstats" runat="server" >
                                    <Tabs>
                                        <ext:Tab ID="Tab1" runat="server" Title="总体情况" BodyStyle="padding: 1px;" AutoScroll="True">
                                            <Body>
                                            
   

                                         <ext:FitLayout ID="FitLayout1" runat="server">
                                                      <ext:Panel ID="Panel5" runat="server" Frame="true" BodyBorder="False">
                                                        <Body>
                                                              <ext:Panel ID="Panel4" runat="server" Title="描述" BodyStyle="padding:3px;" Frame="true"
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
                            autoLoad: {
                                showMask: true,
                                url: menuItem.href,
                                mode: 'iframe',
                                maskMsg: '正在加载 ' + menuItem.text + '...'
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
                    TabPanelstats.setActiveTab(tab);
                }
            </script>

        </Body>
    </ext:Panel>
</ext:FitLayout></body></ext:ViewPort>
    </form>
</body>
</html>
