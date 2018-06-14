<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Manage.aspx.cs" Inherits="WebWSS.Admin_Manage" %>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <meta http-equiv="Content-Language" content="utf-8">

    <script language='JavaScript' src='img/Admin.Js'></script>

    <link href="img/Style.css" rel="stylesheet" type="text/css">
    <style>
        body {
            overflow: hidden;
        }
    </style>
    <title>WSS Administrator's Control Panel</title>
    <base onmouseover="window.status='Powered By WSS &copy; 2011-2012 Shenlongyou Inc.';return true">
    <script src="Script/jquery-1.12.3.min.js"></script>
    <script type="text/javascript">
        var t_id = setInterval(animate, 20);
        var pos = 0; var dir = 2;
        var len = 0;
        function animate() {
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
        #loader_container {
            text-align: center;
            position: absolute;
            top: 40%;
            width: 100%;
            left: 0;
        }

        #loader {
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

        #progress {
            height: 5px;
            font-size: 1px;
            width: 1px;
            position: relative;
            top: 1px;
            left: 0px;
            background-color: #8894a8;
        }

        #loader_bg {
            background-color: #e4e7eb;
            position: relative;
            top: 8px;
            left: 8px;
            height: 7px;
            width: 113px;
            font-size: 1px;
        }

        .splitA {
            border-left: solid 2px #544d4d;
            margin-left: 10px;
        }

        .hideMenu {
            border-right: solid 2px #544d4d;
            padding-right: 10px;
        }
    </style>
    <%--兼容IE--%>
    <!--[if IE]> 
     <style  type="text/css">
            .IEheight {
                height:900px;
            }
            .centerCon{
                height:800px;
            }
        </style>
    <![endif]-->
</head>
<body scroll="no" bgcolor='#3A6592' onload="remove_loading();">
    <div id="loader_container">
        <div id="loader">
            <div align="center">
                <asp:Literal runat="server" Text="<%$Resources:Language,Tip_PageLoading %>"></asp:Literal>
            </div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <table width='100%' height='99%' border="0" cellspacing="0" cellpadding="0">
            <tr id="headtool">
                <td height='41' background='img/top_bg.gif' colspan="2">
                    <div id="toolBar" style='float: right; width: auto; padding: 20px 0 0 0;' class="Font4">
                       <%-- <a style='cursor: pointer' href="stats/wssquerytextname.aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$ Resources:Language,Btn_TextQuery %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">
                        <a style='cursor: pointer' href="stats/wssquerytask.aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_TaskQuery %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">
                        <a style='cursor: pointer' href="stats/userquery.aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_AccountQuery %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">
                        <a style='cursor: pointer' href="stats/wssqueryrole.aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_RoleQuery %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">--%>
                        <a style='cursor: pointer' href="stats/sqlquery.aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_CustormQuery %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">
                        <%--<a style='cursor: pointer' href="Admin_Main_Data.Aspx" target="main" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_RunStatue %>"></asp:Literal>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">--%>
                        <%--<a style='cursor:pointer' href=Admin_Main.Aspx target=main class=white>&#31649;&#29702;&#39318;&#39029;</a> <img src=img/Menu_Line.gif align=absmiddle>--%>
                        <a style='cursor: pointer' onclick="main.location.reload();" class="white">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_PageRefresh%>"></asp:Literal>
                        </a><%--&#21047;&#26032;--%>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">
                        <asp:LinkButton runat="server" ID="lbquitsys" OnClick="lbquitsys_Click">
                        <font color="#ffffff">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_ExitSystem%>" ></asp:Literal>     
                        </font>

                        </asp:LinkButton>&nbsp;&nbsp;
                    </div>
                    <div style='float: left; width: 150px'>
                        <img src='img/Logo.gif' valign="bottom">
                    </div>
                    <div style='float: left; width: 120px; padding: 20px 0 0 0'>
                        <%--<a style='cursor: pointer' onclick='MenuType()' class="white"><span id="menukey"><<
                        <asp:Literal runat="server" Text="<%$Resources:Language,Btn_HideMenu%>"></asp:Literal>
                        </span></a>
                        <img class="hideMenu" src="img/Menu_Line.gif" align="absmiddle" />--%>
                    </div>
                    <div style='float: left; width: 600px; padding: 20px 0 0 0'>
                        <%--<a style='cursor: pointer' href="shop/ShopItemList.aspx" target="main" class="white">
                            <span id="Span1">
                                <asp:Literal Text="<%$Resources:Language,Btn_Mark%>" runat="server"></asp:Literal>
                            </span></a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; <a style='cursor: pointer'
                            href="CDKey/CDKeyBatchList.aspx" target="main" class="white"><span id="Span2">CDKey</span></a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="StartNotice/StartNoticeList.aspx" target="main" class="white"><span id="Span4">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_StartNotice%>"></asp:Literal>

                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="ModelPage/Reimburse/Reimburse.aspx" target="main" class="white">
                            <span id="Span4">
                             <asp:Literal runat="server" Text="<%$Resources:Language,LblReimburse%>"></asp:Literal>
                            </span>
                        </a>
                         <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="ModelPage/Replenishment/Replenishment.aspx" target="main" class="white">
                            <span id="Span4">
                             <asp:Literal runat="server" Text="<%$Resources:Language,LblReplenishment%>"></asp:Literal>
                            </span>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="ModelPage/ServiceState/QueryServiceState.aspx" target="main" class="white">
                            <span id="Span4">
                             <asp:Literal runat="server" Text="<%$Resources:Language,LblServidesSet%>"></asp:Literal>
                            </span>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="ModelPage/GMJurisdiction/GMJurisdictionSet.aspx" target="main" class="white">
                            <span id="Span4">
                             <asp:Literal runat="server" Text="<%$Resources:Language,LblSet%>"></asp:Literal>
                            </span>
                        </a>
                        <img class="splitA" src="img/Menu_Line.gif" align="absmiddle">&nbsp; 
                        <a style='cursor: pointer' href="ModelPage/GssTool/GssQuery.aspx" target="main" class="white">
                            <span id="Span4">
                             <asp:Literal runat="server" Text="GSSTool"></asp:Literal>
                            </span>
                        </a>--%>
                     <%--  <a style='cursor: pointer'
                        href="Admin_IFrame.aspx?src=stats/GSS_CustServ_Day.aspx" target="main" class="white"><span id="Span3">
                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_CRM%>"></asp:Literal>                                                        
                      </span>
                     </a>--%>
                    </div>
                    <div style="float: left; width: 230px; padding: 10px 0 0 0; font-size: 20px; color: white;">
                        <%--<a style="cursor: pointer" href="Resources/GSSClient/setup.exe">Gss
                        </a>--%>
                    </div>
                    <div style='float: left; width: auto; padding: 20px 20px 0 20px' class="Font4" id="AjaxShow">
                    </div>
                    <%--<script>    ToAjax('Admin_Top_Ajax.aspx', 'Action=ChackDate'); setInterval("ToAjax('Admin_Top_Ajax.aspx','Action=ChackDate')", 300000)</script>--%>
                </td>
            </tr>
            <tr id="menutool" style="height:100%">
                <td id="menutd" rowspan="1" width="100%" id="menu">
                    <iframe id="IEIframe" name="left" src='NewMenu.aspx' width='100%' height='100%' scrolling="no" class="IEheight"
                        frameborder="0"></iframe>

                </td>
                <td id="Top" height='98%' hidden="hidden" style="height:80%;">
                    <iframe id="toolIframe" frameborder="0" width='100%' height='100%' scrolling="no"></iframe>
                </td>
            </tr>
            <tr>
                <td height="9" bgcolor='#3A6592' align="center">
                    <%--<img src='img/p_5.gif' id=TopPic align=absmiddle style='cursor:pointer' onClick='TopType()'>--%>
                </td>
            </tr>
            <tr style="display: none;">
                <td height='100%'>
                    <div style='position: absolute; width: 100%; text-align: right; padding: 0 16px 0 0'>
                        <img src='img/R_T.gif'>
                    </div>
                    <div style='position: absolute'>
                        <img src='img/L_T.gif'>
                    </div>
                    <div style='position: absolute; bottom: 0px; width: 100%; text-align: right; padding: 0 16px 25px 0'>
                        <img src='img/R_B.gif'>
                    </div>
                    <div style='position: absolute; bottom: 0px; padding: 0 0 25px 0'>
                        <img src='img/L_B.gif'>
                    </div>
                    <iframe name="main" src='Admin_Main.aspx' width='100%' height='100%' scrolling="yes"
                        frameborder="0"></iframe>
                </td>
            </tr>
            <tr>
                <td height="25" colspan="2" bgcolor="#3A6592">
                    <div style='float: right' class="Font4">
                        Powered By WSS &copy; 2011-2012 .&nbsp;
                    </div>
                    <div style='float: left' class="Font4">
                        <span id="Ver"></span>&nbsp;快速链接=> <a target="main" href="stats/sqlquery.Aspx" class="white">自定义查询</a> <a target="main" href="Admin_Main_Data.Aspx" class="white">运行状态</a>
                        <a target="main" href="Admin_Main.Aspx" class="white">管理首页</a> <a target="main" href="#"
                            class="white">北京小小传奇网络信息有限公司</a>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $('#headtool a').bind('click', function () {
            var obj = $(this);
            var url = obj.attr('href');
            //判断当前控件是否隐藏
            var menutd = $("#menutd");
            var toptd = $('#Top');
            if (url == undefined) {
                if (menutd.attr('hidden') == "hidden") {
                    menutd.show();
                    toptd.hide();
                    toptd.attr("hidden", "hidden");
                }

                return;
            }
            if (toptd.attr("hidden") == "hidden") {
                menutd.hide();
                toptd.show();
                menutd.attr("hidden", "hidden");
            }
            $('#toolIframe')[0].src = url;
            //ie模式下特殊设置  IEIframe 高度不能用百分比
            var browser = navigator.userAgent;
            if (browser.indexOf("MSIE") > -1) {//这是IE浏览器
                //$('#IEIframe')[0].height = 800;
            }
        });
    })
</script>
