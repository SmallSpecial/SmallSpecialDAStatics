<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Login.aspx.cs" Inherits="WebWSS.Admin_Login" %>

<html>
<head id="Head1" runat="server">
    <title>WSS Administrator Login</title>
    <link href='img/Style.css' rel='stylesheet' type='text/css'>
    <script src="Script/jquery-1.12.3.min.js"></script>
    <script src="Script/Browser.js"></script>
    <script type="text/javascript">
        var langBtn = '<asp:Literal runat="server" Text="<%$Resources:Language,LblSwitchLanguage%>" />'
        $(function () {
            InitLanguage();
            setButtonText();
            $('#btnSwitchLanguage').bind('click', function () {
                var obj = $(this);
                var cur = obj.attr('lang').split('|');
                var index = obj.attr('lannow');
                var language = cur[0];
                if (index < cur.length - 1)
                {
                    index++;
                    language = cur[index];
                }
                setLanguage(language);
            });
            function setLanguage(language) {
                $.ajax({
                    url: 'Admin_Login.aspx/SwitchLanguage',
                    data: "{language:'" + language + "'}",
                    type: 'post',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response, statue) {//在asp.net中返回数据没有使用json进行处理返回的数据为[d：data]
                        if ($.trim(response.d).length > 0) {
                            $('#tip').text(response.d);
                            return;
                        }
                        location.reload();
                    }, error: function (response, statue) {
                        var json = JSON.stringify(response);
                      
                    },
                    complete: function () {
                        
                    }
                });
            }
            function InitLanguage() {
                $.ajax({
                    url: 'Admin_Login.aspx/GetLanguage',
                    type: 'post',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response,statue) {
                        var obj = $('#btnSwitchLanguage');
                        var lan = obj.attr('lang');
                        var index = $.inArray(response.d,lan.split('|'));
                        if (index> -1)
                        {
                            obj.attr('lannow', index);
                        }
                    }
                });
            }
            function setButtonText() {
                document.getElementById('btnSwitchLanguage').value= langBtn;
            }
        })
    </script>
</head>
<body style="overflow: hidden; background-image: url('img/login_bg.gif'); background-repeat: repeat-x;
    text-align: center;">
       
    <form id="form1" runat="server">
    <div>
        <div id="login">
            <div class="logo">
                <img src="img/Login.gif" width="246" height="94" alt="" />
                <p><asp:Literal runat="server" Text="<%$Resources:Language,SystemName %>"></asp:Literal> </p>
            </div>
            <div class="Regist">
                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="30" colspan="3" style="font-size: 14px; color: #626262">
                            <strong><asp:Literal Text="<%$Resources:Language,SystemLoginTitle %>" runat="server"></asp:Literal></strong>
                        </td>
                    </tr>
                    <tr>
                        <td width="50" height="30" class="luer">
                            <asp:Literal runat="server" Text="<%$Resources:Language,UserName %>"></asp:Literal>
                        </td>
                        <td height="25" colspan="3">
                            <asp:TextBox ID="UserName" Text="" runat="server" Style="width: 176px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="luer">
                           <asp:Literal runat="server" Text="<%$Resources:Language,Password %>"></asp:Literal>
                        </td>
                        <td height="25" colspan="3">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Style="width: 176px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-top: 15px;">
                            <span>
                                <asp:Button ID="ButtonSub" runat="server" Text=" <%$Resources:Language,Login %> " CssClass="button" OnClick="ButtonSub_Click" />
                            </span>
                             <span>
                               <input id="btnSwitchLanguage" type="button"  lang="zh-CN|ko-kr" lannow="0" class="button" />
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clr">
            </div>
            <div id="tip" style="clear:both;color:red;"></div>
             <div id="IEVersion" style="clear:both;color:red;position:absolute;bottom:20px;"></div>
            <div id="Copyright">
                <a href="http://www.shenlongyou.cn/" target="_blank">
                    <span style="color: #555;">
                         <asp:Literal runat="server" Text="<%$Resources:Language,SystemBelongCompany %>"></asp:Literal>
                    </span>
                </a>
                &copy; 2011-2012
            </div>
        </div>

        <script type="text/javascript">

            if (document.getElementById("UserName").value != "") {
                document.getElementById("Password").focus();
            }
            else { document.getElementById("UserName").focus(); }

            function CheckBrowser() {
                var app = navigator.appName;
                var verStr = navigator.appVersion;
                var browserTip = '';//'<asp:Literal runat="server" Text="<%$Resources:Language,Tip_BrowserLower%>"></asp:Literal>';
                var br = window.localStorage.getItem('Browser');
                var isIE = browserData.IsIE;
                if (isIE) {
                    document.getElementById('IEVersion').innerHTML = browserData.NameDesc;
                }
                if (!isIE ) {
                    document.getElementById("tip").innerHTML = browserTip;
                } else if (isIE&&app.indexOf('Microsoft') != -1) {
                    if (verStr.indexOf('MSIE 3.0') != -1 || verStr.indexOf('MSIE 4.0') != -1 || verStr.indexOf('MSIE 5.0') != -1 || verStr.indexOf('MSIE 5.1') != -1)
                        document.getElementById("tip").innerHTML = browserTip;
                }
            }
            function get_Code() {
                document.getElementById("imgurl").src = '../inc/CodeNum.aspx?' + Math.random();
            }
            
        </script>

        <script type="text/javascript">
            if (top.location != self.location) {
                top.location.href = "Admin_Login.aspx";
            }
            else {
                CheckBrowser();
            }
        </script>

    </div>
    </form>
</body>
</html>
