<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaBaiduIndex.aspx.cs"
    Inherits="WebWSS.Stats.MediaBaiduIndex" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:Label ID="LabelTitle" runat="server" Text=" 推广统计>>百度指数"></asp:Label>
        </div>

        <div class="gridv">
<iframe src="http://index.baidu.com/export/get_search_news_data.php?q=%E5%AF%BB%E9%BE%99%E8%AE%B0&len=m&rg=0&w=986&h=462&chk=25d0207901d33d2b09fd3c7fb57ee123" frameborder="0" width="1016" height="522" scrolling="no"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
