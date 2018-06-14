<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_IFrame.aspx.cs" Inherits="WebWSS.Stats.Admin_IFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript">
        var t_id = setInterval(animate_liu, 20);
        var pos = 0; var dir = 2;
        var len = 0;
        function animate_liu() {
            var elem = document.getElementById('progress');
            if (elem != null) {
                if (pos == 0)
                    len += dir;
                if (len > 32 || pos > 79)
                    pos += dir;
                if (pos > 79)
                    len -= dir;
                if (pos > 79 && len == 0)
                    pos = 0;
                elem.style.left = pos; elem.style.width = len;
            }
        }
        function remove_loading() {
            this.clearInterval(t_id);
            var targelem = document.getElementById('loader_container');
            targelem.style.display = 'none';
            targelem.style.visibility = 'hidden';
        }
    </script>

    <style type="text/css">
        #loader_container
        {
            text-align: center;
            position: absolute;
            top: 40%;
            width: 100%;
            left: 0;
        }
        #loader
        {
            font-family: Tahoma, Helvetica, sans;
            font-size: 11.5px;
            color: #000000;
            background-color: #FFFFFF;
            padding: 10px 0 16px 0;
            margin: 0 auto;
            display: block;
            width: 130px;
            border: 1px solid #5a667b;
            text-align: left;
            z-index: 2;
        }
        #progress
        {
            height: 5px;
            font-size: 1px;
            width: 1px;
            position: relative;
            top: 1px;
            left: 0px;
            background-color: #8894a8;
        }
        #loader_bg
        {
            background-color: #e4e7eb;
            position: relative;
            top: 8px;
            left: 8px;
            height: 7px;
            width: 113px;
            font-size: 1px;
        }
    </style>
</head>
<body onload="remove_loading();">
    <div id="loader_container">
        <div id="loader">
            <div align="center">
                <asp:Label runat="server" Text="<%$ Resources:Language,Tip_PageLoading %>"></asp:Label></div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <iframe name="page_iframe" src='' width='100%' height='86%' scrolling="yes" frameborder="0">
    </iframe>

    <script type="text/javascript">
        var para = location.search.toString();
        var browser = navigator.userAgent;
        var sign = '';
        if (browser.indexOf("MSIE") > -1) {//这是IE浏览器
            sign = '\'';
        } else if (browser.indexOf("Chrome")>-1) {//这是Chrome浏览器
            sign = '%27';
        }
        //var sign = '%27';//此处需要判断浏览器【如果是IE浏览器则 ' 符号不会变化，如果是Chrome 则会变为 %27】
        while (para.indexOf(sign) > -1)
        {
            para = para.substring(0, para.indexOf(sign)) + para.substring(para.indexOf(sign) + sign.length)
        }
        var param = 'src=';
        var startIndex = para.indexOf(param);
        var srcValue = para.substring(startIndex + param.length);
        document.getElementsByName("page_iframe")[0].src = srcValue;
    </script>

    </form>
</body>
</html>
