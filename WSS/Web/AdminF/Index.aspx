<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WSS.Web.AdminF.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>统计系统</title>
</head>

    <form id="form1" runat="server">
<frameset rows="70,*" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="head.ASPX" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
  <frameset cols="182,*" frameborder="no" border="0" framespacing="0">
    <frame src="left.htm" name="left" id="left" title="left"  scrolling="yes"  noresize="noresize" />
    <frame src="stats/ItemDropDay.aspx" name="main" frameborder="no" id="mainFrame" title="main" />
  </frameset>
</frameset>
<noframes><body></body>
</noframes>
    </form>

</html>
