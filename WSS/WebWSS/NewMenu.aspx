<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMenu.aspx.cs" Inherits="WebWSS.NewMenu" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <meta http-equiv="Content-Language" content="utf-8">

    <script language='JavaScript' src='img/Admin.Js'></script>

    <link href="img/Style.css" rel="stylesheet" type="text/css">
   <%-- 新布局菜单效果引用文件--%>
    <script src="Script/jquery-1.12.3.min.js"></script>
    <script src="Script/bootstrap-3.3.7/js/bootstrap.js"></script>
    <link href="Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <link href="Script/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="Script/ace/css/ace-rtl.min.css" rel="stylesheet" />
    <link href="Script/ace/css/ace-skins.min.css" rel="stylesheet" />
   <%-- @*sidebar-menu/sidebar-menu.css*@--%>
    <script src="Script/ace/js/ace-extra.min.js"></script>
    <script src="Script/ace/js/ace.min.js"></script>
    <script src="Script/sidebar-menu/sidebar-menu.js"></script>
    <script src="Script/bootstrap-3.3.7/js/bootstrap-tab.js"></script>
    <script src="Script/Ui/MoneyStatic<%="."+ PageLanguage %>.js"></script>
    <style  type="text/css">
        .leftCon {
            float:left;
            width:13%;
            height:86%;
            overflow-y:auto;
            border-right: solid 3px #ccc;
            padding: 0 10px;
        }
        .centerCon {
            float:left;
            width:85%;
            height:100%;
            margin-left:30px;
        }
        .tab-content {
             width:100%;height:100%;
        }
        .tab-pane {
            width:100%;height:100%;
        }
        .menuItemClick span{
            color:#f51111;
        }
        .nav > li {
            margin-top: 20px;
            border: solid 1px #576fa5;
        }
        .nav-tabs li {
            border:none;
        }
        .nav-show li {
            margin-left:-30px;
        }
        .submenu li  {
            padding: 5px;
            border-bottom: solid 2px #ccc;
            list-style: none;
        }
        .submenu li > a {
            text-decoration:none;
        }
    </style>
   <%-- ie8- 不能调用json--%>
    <!--[if IE]>
        <script src="Script/Browser.js" type="text/javascript"></script>
        <style type="text/css">
            .nav-tabs li{
              float:left;
            }
        </style>
    <![endif]-->
    <script type="text/javascript">
        function initScreen() {
            //计算屏幕分辨率
            var mh = window.screen.height;
            var mw =window.screen.width;
            $('body').css({
                "width": mw-10+"px",
                "height": mh - 90 + "px"
            });
        }
        var node = {
            '1': { hidden: false, menus: tool },
            '2': { hidden: false, menus: money },
            '3': { hidden: false, menus: roleOnline },
            '4': { hidden: false, menus: user },
            '5': { hidden: false, menus: chat },
            '6': { hidden: false, menus: task },
            '7': { hidden: false, menus: mark },
            '8': { hidden: false, menus: guess },
            '9': { hidden: false, menus: questionService },
            '10': { hidden: false, menus: service },
            '11': { hidden: false, menus: tool0menus },
            '14': { hidden: false, menus: detailedInfo },
            '16': { hidden: false, menus: gameOverViewMenus },
            '17': { hidden: false, menus: gameInComeMenus },
            '18': { hidden: false, menus: gameMoneyMenus },

            '11': { hidden: false, menus: tool0menus },
            '12': { hidden: false, menus: tool1menus },
            '13': { hidden: false, menus: tool2menus },
            '15': { hidden: false, menus: tool3menus },

            '210': { hidden: false, menus: money0menus },
            '220': { hidden: false },//银币统计
            '230': { hidden: false, menus: money2menus },
            '240': { hidden: false, menus: money3menus },
            '30': { hidden: false, menus: roleOnline0menus },
            '31': { hidden: false, menus: roleOnline1menus },
            '32': { hidden: false, menus: roleOnline2menus },
            '33': { hidden: false, menus: roleOnline3menus },
            '41': { hidden: false, menus: user0menus },
            '42': { hidden: false, menus: user1menus },
            '43': { hidden: false, menus: userEmail },
            '51': { hidden: false, menus: chat0menus },
            '61': { hidden: false, menus: task0menus },
            '71': { hidden: false, menus: mark0menus },
            '72': { hidden: false, menus: mark1menus },
            '73': { hidden: false, menus: mark2menus },
            '81': { hidden: false, menus: guess0menus },
            '91': { hidden: false, menus: questionService0menus },
            '101': { hidden: false, menus: service0menus },
            '141': { hidden: false, menus: detailedInfo0menus },
            '16102': { hidden: false, menus: realTimeOnlineMenus },
        };
        var menuNode = [];
        function getDispayMenus(id) {
            var item = node[id];
            if (item == undefined) {
                return;
            }
            if (item.hidden==true) {
                return;
            }
            var items = [];
            $(item.menus).each(function (i, ele) {
                if (ele == undefined) {
                    return;
                }
                if (ele.hidden == true) {
                    return;
                }
                if (ele.param != undefined) {
                    ele.param = $.format(ele.param, { gold: moneyCategory.gold.key, t: ele.text });
                    ele.url = '/Admin_IFrame.aspx?src=' + ele.param;
                    ele.url = ele.url.replace('&t={t}', '&t=' + ele.text).replace('&title={t}', '&title=' + ele.text);
                    ele.icon = 'icon-glass';
                }
                items.push(ele);
            });
            return items;
        }
        function initMoneySilverMenu() {
            var silverRank = [];//金钱统计模块单独加载
            $(moneyStatics.soliverSum).each(function (i, ele) {
                if (ele.hidden == true) {
                    return;
                }
                var silverTmpl = { id: '221{param}', icon: 'icon-glass', url: "/Admin_IFrame.aspx?src=Stats/Money/RoleRank.aspx?opid={silver}&p={param}&order=desc&t={text}", text: '{text}' };
                silverTmpl.id =$.format( silverTmpl.id,{ param: ele.param });
                silverTmpl.url =$.format( silverTmpl.url,{ param: ele.param, silver: moneyCategory.silver.key, text: ele.text });
                silverTmpl.text =$.format( silverTmpl.text,{ text: ele.text });
                silverRank.push(silverTmpl);
            });

            $(moneyStatics.soliverRank).each(function (i, ele) {
                if (ele.hidden == true) {
                    return;
                }
                var silverTmpl = { id: '222{param}', icon: 'icon-glass', url: "/Admin_IFrame.aspx?src=Stats/Money/RoleRank.aspx?opid={silver}&&p={param}&order=asc&t={text}", text: '{text}' }
                silverTmpl.id =$.format( silverTmpl.id,{ param: ele.param });
                silverTmpl.url =$.format( silverTmpl.url,{ param: ele.param, silver: moneyCategory.silver.key, text: ele.text });
                silverTmpl.text =$.format( silverTmpl.text,{ text: ele.text });
                silverRank.push(silverTmpl);

            });
            return silverRank;
        }
        $(menu).each(function (i, ele) {
            if (ele.hidden == true) {
                return;
            }
            var sec = getDispayMenus(ele.id);//查找二级菜单
            $(sec).each(function (i, e) {//具体的菜单项
                if (e.id == '220') {//金钱模块下银币统计单独加载
                    e.menus = initMoneySilverMenu();
                    return;
                } 
                var ms = getDispayMenus(e.id);
                if (ms == undefined) {
                    return;
                } else if (e.id == '230') {
                    $(ms).each(function (i, ele) {
                        ele.param = $.format(ele.param, { gold: moneyCategory.gold.key })
                    });
                }
                e.menus = ms;
            });
            ele.menus = sec;
            menuNode.push(ele);
        });
        $(function () {
            var onlyOneTab = false;
            var cache = window.localStorage;
            if (window.localStorage["Browser"] != undefined) {
                var browser = JSON.parse(cache["Browser"]);
                if (browser.IsIE && parseInt(browser.Ver)<8) {
                    onlyOneTab = true;
                }
            } else {
                var browser = navigator.userAgent;
                var version = (browser.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [])[1]
                if (browser.indexOf("MSIE") > -1) {//这是IE浏览器[IE8-]
                    onlyOneTab = true
                }
            }
            initScreen();
            $('#menu').sidebarMenu({
                data: menuNode,
                onlyOneTab: onlyOneTab
            });
            $('.submenu li').click(function (event) {
                var obj = $(this);
                $('.menuItemClick').removeClass('menuItemClick');
                $(obj).children('a').addClass('menuItemClick');//只作用于当前选择的元素
               
                if (obj.find('li').length == 0) {
                    event.stopPropagation();
                }
            });
            initMenuItem();
        });
        function initMenuItem() {
            var menus = $('.dropdown-toggle');
            $('.submenu').css("display", "none");
        }
        window.onerror = function () {
            var msg = arguments;
            if (Reporter)
                Reporter.sender(msg);
            return false;
        }
    </script>
</head>
<body>
      <form id="form1" runat="server" style="height:100%;width:100%;">
           <div style="height:100%;width:100%;">
                <div class="sidebar leftCon" id="sidebar">
                    <div id="loginUser">
                        <table  width='180' height='70' border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="top" colspan="2" style='height: 78px; padding: 10px 10px; background: url("img/admin.gif")'>
                                    <asp:Literal runat="server" Text="<%$Resources:Language,LblWelcome %>"></asp:Literal>：
                                    <strong><asp:Label ID="lblUser" runat="server" Text="<%$Resources:Language,UserName %>"></asp:Label>！</strong><br>
                                    <asp:Literal runat="server" Text="<%$Resources:Language,LblLevel %>"></asp:Literal>：
                                    <asp:Label ID="lblRole" runat="server" Text="<%$Resources:Language,LblSystemUser %>"></asp:Label>
                                    <br>
                                    <asp:Literal runat="server" Text="<%$Resources:Language,LblOperate %>"></asp:Literal>：
                                    <a style='cursor: pointer' href="Admin_Main.Aspx" target="main" class="white">
                                    <font color="#135294">
                                         <asp:Literal runat="server" Text="<%$Resources:Language,LblManageHomePage %>"></asp:Literal>
                                    </font></a>
                                    <asp:LinkButton runat="server" ID="lbquitsys" onclick="lbquitsys_Click">
                                        <font color="#135294">
                                            <asp:Literal runat="server" Text="<%$Resources:Language,Btn_ExitSystem %>"></asp:Literal>
                                        </font></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div  style="position:static;">
                        <ul class="nav nav-list" id="menu"></ul>
                        <div class="sidebar-collapse" id="sidebar-collapse">
                            <i class="icon-double-angle-left" data-icon1="icon-double-angle-left" data-icon2="icon-double-angle-right"></i>
                        </div>
                    </div>
                </div>
                <div class="main-content centerCon">
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12" style="padding-left:5px;">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="active">
                                       <%-- <a href="#Index" role="tab" data-toggle="tab" >
                                          <asp:Literal runat="server" Text="<%$Resources:Language,LblHomePage %>"></asp:Literal>
                                         </a>--%>
                                    </li>
                                </ul>
                                <div class="tab-content" style="margin-top:20px;">
                                   <%-- <div  class="tab-pane active" id="Index" role="tabpanel">
                                        <div style="display:none">IE hack</div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           </div>
      </form>
</body>
</html>
