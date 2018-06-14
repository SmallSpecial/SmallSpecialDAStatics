<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notic.aspx.cs" Inherits="Web.Admin.notic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>游戏客服管理系统--神龙游</title>
    <style type="text/css">
    body{ font-size:12px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    亲爱的<span style="color: #f00;"><asp:Literal ID="litName" runat="server" /></span>(<asp:Literal
        ID="litRole" runat="server" />):<br />
    &nbsp;&nbsp;&nbsp;&nbsp;欢迎登陆【<asp:Literal ID="litLocation" runat="server" />】
    </form>
</body>
</html>
