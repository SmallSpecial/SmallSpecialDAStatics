﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bar3d.aspx.cs" Inherits="WSS.Web.Stat.Bar3D" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="swfobject.js"></script>

    <script type="text/javascript">
swfobject.embedSWF("open-flash-chart.swf", "my_chart", "550", "200",
  "9.0.0", "expressInstall.swf",
  {"data-file":"datafile/bar3d.aspx"}
  );
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="my_chart">
        </div>
    </form>
</body>
</html>