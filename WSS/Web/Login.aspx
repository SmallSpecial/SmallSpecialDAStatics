<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link type="text/css" rel="Stylesheet" href="../style/master.css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManagerLogin" runat="server" CleanResourceUrl="False" />
    <ext:Window ID="LoginWindow" runat="server" CenterOnLoad="true" Closable="false"
        Resizable="false" Modal="true" Height="180" Icon="HouseKey" Title="用户登录" Draggable="false"
        Width="350" BodyStyle="padding:5px;">
        <Body>
            <ext:FormPanel ID="LoginForm" MonitorPoll="500" MonitorValid="true" Header="false"
                Border="false" runat="server">
                <Defaults>
                    <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                    <ext:Parameter Name="MsgTarget" Value="side" />
                </Defaults>
                <Body>
                    <ext:FormLayout ID="FormLayout1" runat="server" LabelWidth="60" >
                        <ext:Anchor Horizontal="80%">
                            <ext:TextField ID="txtUsername" runat="server" FieldLabel="&nbsp;&nbsp;用户名" AllowBlank="false"
                                BlankText="您未输入用户名！" Text="" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="80%">
                            <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="&nbsp;&nbsp;密&nbsp;&nbsp;&nbsp;码"
                                AllowBlank="false" BlankText="您未输入密码！" Text="" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="80%">
                            <ext:TextField ID="txtCard" EmptyText="请输入下面的验证码" runat="server" MaxLength="6" FieldLabel="&nbsp;&nbsp;验证码"
                                AllowBlank="false" BlankText="验证码不能为空哦！" Text="">
                            </ext:TextField>
                            
                        </ext:Anchor>
                         <ext:Anchor Horizontal="80%">
  
                            <ext:Label ID="Label2" FieldLabel="&nbsp;&nbsp;验证码" runat="server" Html="<img src='ashx/imgChar.ashx' id='img' onclick='javascript:reimg();' style='cursor:pointer' alt='点击更换验证码' width='100' height='20' />" />
                            
                        </ext:Anchor>
                    </ext:FormLayout>
                </Body>
                <Buttons>
                    <ext:Button ID="btnLogin" Type="Submit" runat="server" Text="现在登陆" Icon="Accept">
                        <Listeners>
                            <Click Handler="if(#{LoginForm}.getForm().isValid()){return true;}else{ #{txtUsername}.focus(true);return false;}" />
                        </Listeners>
                        <AjaxEvents>
                            <Click OnEvent="btnLogin_Click">
                                <EventMask ShowMask="true" Msg="正在验证,请稍候..." />
                            </Click>
                        </AjaxEvents>
                    </ext:Button>
                    <ext:Button Type="Button" Text="重填" Icon="UserAdd" runat="server" ID="btnRegist">
                        <Listeners>
                            <Click Handler="#{LoginForm}.getForm().reset();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Body>
    </ext:Window>
    </form>
        <script type="text/javascript" language=javascript>
        function reimg() {
            var num = Math.random();
            var img = document.getElementById("img");
            img.src = "ashx/imgChar.ashx?" + num;
   }
    </script>
</body>
</html>
