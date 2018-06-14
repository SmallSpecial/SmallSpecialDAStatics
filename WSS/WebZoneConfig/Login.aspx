<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebZoneConfig.Login" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <%--  <link type="text/css" rel="Stylesheet" href="../style/master.css" media="all" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManagerLogin" runat="server" CleanResourceUrl="False" />
    <ext:Window ID="LoginWindow" runat="server" CenterOnLoad="true" Closable="false"
        Resizable="false" Modal="true" Height="150" Icon="HouseKey" Title="<%$Resources:Global,StringUserLogin %>" Draggable="false"
        Width="330" BodyStyle="padding:5px;">
        <Body>
            <ext:FormPanel ID="LoginForm" MonitorPoll="450" MonitorValid="true" Header="false"
                Border="false" runat="server">
                <Defaults>
                    <ext:Parameter Name="AllowBlank" Value="true" Mode="Raw" />
                    <ext:Parameter Name="MsgTarget" Value="side" />
                </Defaults>
                <Body>
                    <ext:FormLayout ID="FormLayout1" runat="server" LabelWidth="60"  style="height:78px;" LabelStyle="padding-left:10px;">
                        <ext:Anchor Horizontal="85%">
                            <ext:TextField ID="txtUsername" runat="server" FieldLabel="<%$Resources:Global,StringUserName %>" AllowBlank="false"
                                BlankText="<%$Resources:Global,StringUserNameN %>" Text="" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="85%">
                            <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="<%$Resources:Global,StringUserPSW %>"
                                AllowBlank="false" BlankText="<%$Resources:Global,StringUserPSWN %>" Text="" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="85%">
                            <ext:TextField ID="txtCard" Hidden="true" EmptyText="<%$Resources:Global,StringUserAuthCodeTip %>" runat="server" MaxLength="4" FieldLabel="<%$Resources:Global,StringUserAuthCode %>"
                                AllowBlank="true" BlankText="<%$Resources:Global,StringUserAuthCodeN %>" Text="">
                                <Listeners>
                                    <Focus Handler="#{tip}.show()" />
                                </Listeners>
                            </ext:TextField>
                        </ext:Anchor>
                    </ext:FormLayout>
                </Body>
                <Buttons>
                    <ext:Button ID="btnLogin" Type="Submit" runat="server" Text="<%$Resources:Global,StringLogin %>" Icon="Accept">
                        <Listeners>
                            <Click Handler="if(#{LoginForm}.getForm().isValid()){return true;}else{ #{txtUsername}.focus(true);return false;}" />
                        </Listeners>
                        <AjaxEvents>
                            <Click OnEvent="btnLogin_Click">
                                <EventMask ShowMask="true" Msg="<%$Resources:Global,StringLoadingNow %>" MinDelay="1" />
                            </Click>
                        </AjaxEvents>
                    </ext:Button>
                    <ext:Button Type="Button" Text="<%$Resources:Global,StringReset %>" Icon="UserAdd" runat="server" ID="btnRegist">
                        <Listeners>
                            <Click Handler="#{LoginForm}.getForm().reset();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Body>
    </ext:Window>
    <ext:ToolTip ID="tip" AutoDestroy="false" AutoShow="true" Html="<img src='GameTool/imgChar.ashx' id='img' onclick='javascript:reimg();' style='cursor:pointer' alt='' width='60' height='20' />"
        Target="txtCard" Collapsed="false" Frame="true" AutoHide="false" Draggable="false"
        runat="server" >
    </ext:ToolTip>
    <script type="text/javascript" language="javascript">
        function reimg() {
            var num = Math.random();
            var img = document.getElementById("img");
            img.src = "GameTool/imgChar.ashx?" + num;
   }
    </script>
    </form>
</body>
</html>
