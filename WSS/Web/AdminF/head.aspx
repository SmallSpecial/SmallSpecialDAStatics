<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="head.aspx.cs" Inherits="WSS.Web.AdminF.head" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/style.css" type="text/css"  />
<title>Management System</title>
<script language ="javascript" type ="text/javascript" src ="javascript/base.js"></script>
<script type="text/javascript" src="javascript/CheckedAll.js"></script>
<style>
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="head">
  <tr>
    <td class="logo">&nbsp;</td>
   <td class="head_right topmenu"> 
   <div class="version"><em><a href="#" >当前登录用户:<%=Session["LoginUser"]==null?"":Session["LoginUser"] %></a></em>&nbsp;&nbsp; <a href="../login.aspx" target ="_parent">退出系统</a></div>
      <ul>
       <li id="basic1"  style="width: 1px; height: 1px"><!--<a href="Stats/UserOnLine.aspx" onclick="menuchange(1);" target="main">在线人数</a> --><a href="#" ></a></li>
        <li id="basic2"  class="on" ><a href="Stats/ItemDropDay.aspx" onclick="menuchange(2);"  target="main">道具统计</a></li>
        <li id="basic3" ><a href="Stats/MoneyGoldOutIn.aspx" onclick="menuchange(3);"  target="main">金钱统计</a></li>
        <li id="basic5" ><a href="Stats/RoleOnlineFlow.aspx" onclick="menuchange(5);"  target="main">角色统计</a></li>
        <li id="basic6" ><a href="NoPage.htm" onclick="menuchange(5);"  target="main">聊天相关</a></li>
        <li id="basic4" style="display:none"><a href="NoPage.htm" onclick="menuchange(4);" target="main">其他统计</a></li>
      </ul>
	</td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
