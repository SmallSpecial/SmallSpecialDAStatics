<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Menus.aspx.cs" Inherits="WebWSS.Admin_Menus" %>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <meta http-equiv="Content-Language" content="utf-8">

    <script language='JavaScript' src='img/Admin.Js'></script>

    <link href="img/Style.css" rel="stylesheet" type="text/css">
    <style>
        body
        {
            background-color: #5ABEEB;
            background: url("img/left_bg.gif");
            background-attachment: fixed;
        }
        .LeftBG
        {
            /* background: #4DB4E2;*/
            filter: alpha(opacity=10);
        }
        .menubg
        {
            color: #ffffff;
            position: relative;
            filter: alpha(opacity=100);
        }
        .Titbg
        {
            /*background: none !important;*/
            color: #FFFFFF;
            position: relative;
            background: url("img/Menu_BG.gif") repeat-x 0px 0px;
            filter: Alpha(Opacity=95);
        }
        .Titbgc
        {
            /*background: none !important;*/
            color: #FFFFFF;
            position: relative;
            background: url("img/Menu_BGc.gif");
            filter: Alpha(Opacity=95);
        }
        A
        {
            color: #FFFFFF;
            text-decoration: None;
        }
        A:link
        {
            color: #FFFFFF;
            text-decoration: None;
        }
        A:visited
        {
            color: #FFFFFF;
            text-decoration: None;
        }
        A:hover
        {
            color: #FFFFFF;
            text-decoration: None;
        }
        A:active
        {
            text-decoration: none;
        }
    </style>
    <form id="form1" runat="server">
    <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" colspan="2" style='height: 78px; padding: 10px 10px; background: url("img/admin.gif")'>
                欢迎：<strong><asp:Label ID="lblUser" runat="server" Text="用户"></asp:Label>！</strong><br>
                等级：<asp:Label ID="lblRole" runat="server" Text="系统用户"></asp:Label>
                <br>
                操作：<a style='cursor: pointer' href="Admin_Main.Aspx" target="main" class="white"><font
                    color="#135294">管理首页</font></a>
                <asp:LinkButton runat="server" ID="lbquitsys" onclick="lbquitsys_Click">
                    <font color="#135294">退出系统</font></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td valign="top" width="25" style='padding: 0 0 5px 2px'>
                <table width='100%' border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel1"
                                id="tit" onclick="menudis(0)">
                                <tr>
                                    <td id="titfont" class="mLabeltit1">
                                        道具
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(1)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        金钱
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(2)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        角色
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(3)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        用户
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(4)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        聊天
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(5)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        任务
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(6)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        商城
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(7)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        推广
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(8)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        问卷
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width='100%' height='100%' border="0" cellspacing="0" cellpadding="0" class="mLabel"
                                id="tit" onclick="menudis(9)">
                                <tr>
                                    <td id="titfont" class="mLabeltit">
                                        服务器
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <div style="height: 100%; width: 100%; overflow-y: scroll; overflow-x: hidden; padding: 0 2px 5px 2px">
                    <table cellspacing="1" cellpadding="0" width='100%' align='center' border="0">
                        <tr id="ShowM">
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(d00,e00,f00)' id='f00' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;贵重物品掉落</strong>
                                                    </td>
                                                </tr>
                                                <tr id='d00'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/ItemDropDay.aspx'" target="main">装备历史掉落</a>
                                                        | <a href="Admin_IFrame.aspx?src='Stats/ItemDropDayNow.aspx?isnow=1'" target="main">
                                                            当天</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/StoneDropDay.aspx'" target="main">精炼石(宝石)</a><br />
                                                        <%--                             ────────<br>
                                                        <a href="Admin_IFrame.aspx?src=Admin_NoPage.Aspx" target="main">坐骑统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src=Admin_NoPage.Aspx" target="main">商城道具</a><br />--%>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/ItemDropQuery.aspx'" target="main">掉落日志查询</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ItemAttriAttack.aspx'" target="main">极品属性统计(攻击)</a><br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ItemAttriDefence.aspx'" target="main">极品属性统计(防御)</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/ItemQueryRank.aspx'" target="main">道具次数统计*</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/ItemQuery.aspx'" target="main">道具日志查询</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/TradeQuery.aspx'" target="main">交易日志查询</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/OtherQuery.aspx'" target="main">其它日志查询</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='e00'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(d3,e3,f3)' id='f3' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>拍卖行统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='d3'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/PublicSale.aspx'" target="main">拍卖行整体统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/PublicSaleStar.aspx'" target="main">道具星级统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/PublicSaleLevel.aspx'" target="main">道具等级统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/PublicSaleJinglian.aspx'" target="main">道具精炼统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/PublicSaleHuanhua.aspx'" target="main">道具幻化统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/PublicSaleRankFight.aspx'" target="main">道具战斗力排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">道具一口价排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">玩家售卖排行</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='e3'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG"
                                                style="display: none;">
                                                <tr>
                                                    <td onclick='show(d2,e2,f2)' id='f2' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;贵重物品消耗&冗余</strong>
                                                    </td>
                                                </tr>
                                                <tr id='d2'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">按职业统计(装备)</a>
                                                        <br />
                                                        <a href="Admin_IFrame.aspx'src='Admin_NoPage.Aspx'" target="main">按星级统计(装备)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx" target="main">精炼石(宝石)</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx" target="main">坐骑统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx" target="main">商城道具</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='e4'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(d4,e4,f4)' id='f4' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;物品统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='d4'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/TypeTotal.aspx?opid=10047&p=15&order=asc&t=NPC购买获取总计'"
                                                            target="main">NPC购买获取总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/TypeTotal.aspx?opid=10047&p=27&order=asc&t=主线任务获取总计'"
                                                            target="main">主线任务获取总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/TypeTotal.aspx?opid=10047&p=2&order=asc&t=拾取物品获取总计'"
                                                            target="main">拾取物品获取总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/TypeTotal.aspx?opid=10047&p=34&order=asc&t=邮件邮寄获取总计'"
                                                            target="main">邮件邮寄获取总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/TypeTotal.aspx?opid=10048&p=20051&order=asc&t=角色死亡掉落总计'"
                                                            target="main">角色死亡掉落总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=29&order=desc&t=商城购买获取排行'"
                                                            target="main">商城购买获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=3&order=desc&t=镇神关奖励获取排行'"
                                                            target="main">镇神关奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=6&order=desc&t=帮会领地奖励获取排行'"
                                                            target="main">帮会领地奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=7&order=desc&t=累计奖励获取排行'"
                                                            target="main">累计奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=10&order=desc&t=英雄榜奖励获取排行'"
                                                            target="main">英雄榜奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=16&order=desc&t=活跃值奖励获取排行'"
                                                            target="main">活跃值奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=17&order=desc&t=命魂盒奖励获取排行'"
                                                            target="main">命魂盒奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=23&order=desc&t=兑换奖励获取排行'"
                                                            target="main">兑换奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=26&order=desc&t=签到奖励获取排行'"
                                                            target="main">签到奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=27&order=desc&t=任务奖励获取排行'"
                                                            target="main">任务奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10047&p=28&order=desc&t=补偿奖励获取排行'"
                                                            target="main">补偿奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=36&order=desc&t=离线活动奖励获取排行'"
                                                            target="main">离线活动奖励获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank_Trade_InCount.aspx?opid=10047&p2=31&order=desc&t=角色道具交易获取排行'"
                                                            target="main">角色道具交易获取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank_In_TradeCount.aspx?opid=10047&p=34&order=desc&t=角色道具邮件获取排行'"
                                                            target="main">角色道具邮件获取排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001,433003,433005,433007,433009,433011,433013,433015&t=精炼石消耗排行'"
                                                            target="main">精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001&t=1品精炼石消耗排行'"
                                                            target="main">1品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433003&t=2品精炼石消耗排行'"
                                                            target="main">2品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433005&t=3品精炼石消耗排行'"
                                                            target="main">3品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433007&t=4品精炼石消耗排行'"
                                                            target="main">4品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433009&t=5品精炼石消耗排行'"
                                                            target="main">5品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433011&t=6品精炼石消耗排行'"
                                                            target="main">6品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433013&t=7品精炼石消耗排行'"
                                                            target="main">7品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433015&t=8品精炼石消耗排行'"
                                                            target="main">8品精炼石消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/RoleRank.aspx?opid=10048&p=20053&order=asc&t=卖给NPC物品排行'"
                                                            target="main">卖给NPC物品排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433001&t=1品精炼石拾取排行'"
                                                            target="main">1品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433003&t=2品精炼石拾取排行'"
                                                            target="main">2品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433005&t=3品精炼石拾取排行'"
                                                            target="main">3品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433007&t=4品精炼石拾取排行'"
                                                            target="main">4品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433009&t=5品精炼石拾取排行'"
                                                            target="main">5品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433011&t=6品精炼石拾取排行'"
                                                            target="main">6品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433013&t=7品精炼石拾取排行'"
                                                            target="main">7品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433015&t=8品精炼石拾取排行'"
                                                            target="main">8品精炼石拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581200&t=四元精金拾取排行'"
                                                            target="main">四元精金拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581054&t=佛骨菩提子拾取排行'"
                                                            target="main">佛骨菩提子拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src'=Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=580117&t=铭刻石拾取排行'"
                                                            target="main">铭刻石拾取排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Trade/RoleRank.aspx?opid=10002&order=desc&t=角色道具交易排行'"
                                                            target="main">角色道具交易排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Item/ItemBusinessRank.aspx'" target="main">道具交易排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Other/RoleRank_Proficiency.aspx?opid=10019&order=asc&t=装备精炼排行'"
                                                            target="main">装备精炼排行</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='e2'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a14,b14,c14)' id='c14' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;金钱统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a14'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyGoldOutIn.aspx'" target="main">银币产出消耗</a>
                                                        | <a href="Admin_IFrame.aspx?src='Stats/MoneySilverOutIn.aspx'" target="main">银票</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyGoldType.aspx'" target="main">银币分类消耗</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyGoldTypeOut.aspx'" target="main">银币分类产出</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneySilverType.aspx'" target="main">银票分类产消</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneySilverExist.aspx'" target="main">银币存有量</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyGoldRole.aspx'" target="main">玩家产消排行-银币</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyGoldRoleStream.aspx'" target="main">玩家流通排行-银币</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyRankTask.aspx'" target="main">任务银币排行</a>
                                                        | <a href="Admin_IFrame.aspx?src='Stats/MoneySilverRankTask.aspx'" target="main">银票</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/MoneyQuery.aspx'" target="main">金钱日志查询</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b14'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a142,b142,c142)' id='c142' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;银币统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a142'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=2&order=asc&t=邮件银币总数'"
                                                            target="main">邮件银币总数</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=4&order=asc&t=玩家拾取总数'"
                                                            target="main">玩家拾取总数</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=5&order=asc&t=任务奖励总计'"
                                                            target="main">任务奖励总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=29&order=asc&t=悬赏任务总计'"
                                                            target="main">悬赏任务总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=48&order=asc&t=灵数猎手总计'"
                                                            target="main">灵数猎手总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=6&order=asc&t=帮会创建总计'"
                                                            target="main">帮会创建总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=11&order=asc&t=修理装备总计'"
                                                            target="main">修理装备总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=14&order=asc&t=法宝升星总计'"
                                                            target="main">法宝升星总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=18&order=asc&t=装备精炼总计'"
                                                            target="main">装备精炼总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=22&order=asc&t=悬赏任务扣除总计'"
                                                            target="main">悬赏任务扣除总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=28&order=asc&t=玩家邮资总计'"
                                                            target="main">玩家邮资总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=31&order=asc&t=拍卖行手续费总计'"
                                                            target="main">拍卖行手续费总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=32&order=asc&t=直接完成洪荒任务总计'"
                                                            target="main">直接完成洪荒任务总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=33&order=asc&t=刷新洪荒任务总计'"
                                                            target="main">刷新洪荒任务总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=34&order=asc&t=刷新洪荒星级总计'"
                                                            target="main">刷新洪荒星级总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=41&order=asc&t=帮会捐献总计'"
                                                            target="main">帮会捐献总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=44&order=asc&t=精炼转移总计'"
                                                            target="main">精炼转移总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=45&order=asc&t=法宝技能刷新总计'"
                                                            target="main">法宝技能刷新总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=52&order=asc&t=装备铭刻总计'"
                                                            target="main">装备铭刻总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=57&order=asc&t=修改转换联盟总计'"
                                                            target="main">修改转换联盟总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=58&order=asc&t=坐骑繁殖消耗总计'"
                                                            target="main">坐骑繁殖消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=59&order=asc&t=坐骑刷星消耗总计'"
                                                            target="main">坐骑刷星消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=60&order=asc&t=坐骑刷新资质总计'"
                                                            target="main">坐骑刷新资质总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=61&order=asc&t=坐骑先天悟道总计'"
                                                            target="main">坐骑先天悟道总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=62&order=asc&t=坐骑后天悟道总计'"
                                                            target="main">坐骑后天悟道总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=49&order=asc&t=交易行卖银币总计'"
                                                            target="main">交易行卖银币总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10037&p=51&order=asc&t=元宝购买手续费总计'"
                                                            target="main">元宝购买手续费总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=4&order=desc&t=玩家拾取排行'"
                                                            target="main">玩家拾取排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=5&order=desc&t=任务奖励排行'"
                                                            target="main">任务奖励排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=29&order=desc&t=悬赏任务排行'"
                                                            target="main">悬赏任务排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=48&order=desc&t=灵数猎手排行'"
                                                            target="main">灵数猎手排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Other/RoleRank.aspx?opid=10041&p=7&order=desc&t=灵术猎手奖励排行'"
                                                            target="main">灵术猎手奖励排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=3&order=asc&t=拍卖行购物排行'"
                                                            target="main">拍卖行购物排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=11&order=asc&t=修理装备排行'"
                                                            target="main">修理装备排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=14&order=asc&t=法宝升星排行'"
                                                            target="main">法宝升星排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=18&order=asc&t=装备精炼排行'"
                                                            target="main">装备精炼排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=22&order=asc&t=悬赏任务扣除排行'"
                                                            target="main">悬赏任务扣除排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=28&order=asc&t=玩家邮资排行'"
                                                            target="main">玩家邮资排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=31&order=asc&t=拍卖行手续费排行'"
                                                            target="main">拍卖行手续费排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=32&order=asc&t=直接完成洪荒任务排行'"
                                                            target="main">直接完成洪荒任务排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=33&order=asc&t=刷新洪荒任务排行'"
                                                            target="main">刷新洪荒任务排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=34&order=asc&t=刷新洪荒星级排行'"
                                                            target="main">刷新洪荒星级排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=41&order=asc&t=帮会捐献排行'"
                                                            target="main">帮会捐献排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=44&order=asc&t=精炼转移排行'"
                                                            target="main">精炼转移排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=45&order=asc&t=法宝技能刷新排行'"
                                                            target="main">法宝技能刷新排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=52&order=asc&t=装备铭刻排行'"
                                                            target="main">装备铭刻排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=57&order=asc&t=修改转换联盟排行'"
                                                            target="main">修改转换联盟排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=58&order=asc&t=坐骑繁殖消耗排行'"
                                                            target="main">坐骑繁殖消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=59&order=asc&t=坐骑刷星消耗排行'"
                                                            target="main">坐骑刷星消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=60&order=asc&t=坐骑刷新资质排行'"
                                                            target="main">坐骑刷新资质排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=61&order=asc&t=坐骑先天悟道排行'"
                                                            target="main">坐骑先天悟道排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=62&order=asc&t=坐骑后天悟道排行'"
                                                            target="main">坐骑后天悟道排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=49&order=asc&t=交易行卖银币排行'"
                                                            target="main">交易行卖银币排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=51&order=asc&t=元宝购买手续费排行'"
                                                            target="main">元宝购买手续费排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_TradeCount.aspx?opid=10037&&p=1&order=desc&t=银币交易获取次数排行'"
                                                            target="main">银币交易获取次数排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&&p=1&order=asc&t=银币交易失去金额排行'"
                                                            target="main">银币交易失去金额排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_MoneyCount.aspx?opid=10037&p=1&order=desc&t=银币交易获取金额排行'"
                                                            target="main">银币交易获取金额排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_Email_TradeCount.aspx?opid=10037&p=2&order=desc&t=邮件收发银币次数排行'"
                                                            target="main">邮件收发银币次数排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_Email_In_TradeCount.aspx?opid=10037&p=2&order=desc&t=邮件获取银币次数排行'"
                                                            target="main">邮件获取银币次数排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_Email_MoneyCount.aspx?opid=10037&p=2&order=desc&t=邮件获取银币金额排行'"
                                                            target="main">邮件获取银币金额排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=2&order=desc&t=邮件发送银币金额排行'"
                                                            target="main">邮件发送银币金额排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_Depository_TradeCount.aspx?opid=10037&p=0&&order=desc&t=仓库存取银币次数排行'"
                                                            target="main">仓库存取银币次数排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10037&p=0&order=asc&t=仓库取出银币金额排行'"
                                                            target="main">仓库取出银币金额排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank_Depository_MoneyCount.aspx?opid=10037&p=0&order=desc&t=仓库存进银币金额排行'"
                                                            target="main">仓库存进银币金额排行</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b142'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a152,b152,c152)' id='c152' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;银票统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a152'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=3&order=asc&t=主线任务获取总计'"
                                                            target="main">主线任务获取总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=14&order=asc&t=活动奖励获取总计'"
                                                            target="main">活动奖励获取总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=12&order=asc&t=修理装备消耗总计'"
                                                            target="main">修理装备消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=6&order=asc&t=装备精炼消耗总计'"
                                                            target="main">装备精炼消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=19&order=asc&t=装备铭刻消耗总计'"
                                                            target="main">装备铭刻消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=10&order=asc&t=法宝升星消耗总计'"
                                                            target="main">法宝升星消耗总计</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/TypeTotal.aspx?opid=10038&p=15&order=asc&t=精炼转移消耗总计'"
                                                            target="main">精炼转移消耗总计</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=5&order=desc&t=NPC交易排行'"
                                                            target="main">NPC交易排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=0&order=desc&t=玩家拾取排行'"
                                                            target="main">玩家拾取排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=12&order=asc&t=装备修理消耗排行'"
                                                            target="main">装备修理消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=6&order=asc&t=装备精练消耗排行'"
                                                            target="main">装备精练消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=19&order=asc&t=装备铭刻消耗排行'"
                                                            target="main">装备铭刻消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=10&order=asc&t=法宝升星消耗排行'"
                                                            target="main">法宝升星消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/Money/RoleRank.aspx?opid=10038&p=15&order=asc&t=精练转移消耗排行'"
                                                            target="main">精练转移消耗排行</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b152'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a141,b141,c141)' id='c141' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;财富榜</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a141'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/HonorMoneySRoleRank.aspx'" target="main">银币角色排行</a><br>
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">金币周消耗排行</a><br>
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">金币总消耗排行</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b141'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a13,b13,c13)' id='c13' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;<% GetResource("LblOnlineStatic"); %></strong>
                                                    </td>
                                                </tr>
                                                <tr id='a13'>
                                                    <td class="menubg">
                                                        <b>角色在线 >></b>
                                                        <br />
                                                        服:<a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlow_Zone.Aspx'" target="main">每小时</a>|
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlowM_zone.aspx'" target="main">每15分钟</a><br />

                                                      <%--  线:<a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlow.Aspx'" target="main">每小时</a>|
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlowM.aspx'" target="main">每15分钟</a><br />--%>

                                                        总:<a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlow_ALL.Aspx'" target="main">每小时</a>|
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineFlowM_All.aspx'" target="main">每15分钟</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b13'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a11,b11,c11)' id='c11' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;角色统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a11'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocation.aspx'" target="main">角色总量统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationGrow.aspx'" target="main">角色增量统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleLoginZone.aspx'" target="main">角色登陆总数统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineLoginRank_Two.aspx'" target="main">角色登录次数排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineLoginOutRank_Two.aspx'" target="main">
                                                            角色登录出数排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationLevel.aspx'" target="main">职业等级统计</a>
                                                        | <a href="Admin_IFrame.aspx?src='Stats/RoleVocationLevelGrow.aspx'" target="main">增量</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationLevelPara.aspx'" target="main">等级分段统计</a>
                                                        | <a href="Admin_IFrame.aspx?src='Stats/RoleVocationLevelParaGrow.aspx'" target="main">
                                                            增量</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/RoleLevelTime.aspx'" target="main">等级耗时统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationLevelRank.aspx'" target="main">等级排行统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationAttackRank.aspx'" target="main">攻击力排行统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleVocationDefenceRank.aspx'" target="main">防御力排行统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/RoleVocationPKHonorRank.aspx'" target="main">PK荣誉排行统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/Fight/RoleRank.aspx?opid=10031&p=0&order=desc&t=角色死亡次数排行'"
                                                            target="main">角色死亡次数排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Fight/RoleRank.aspx?opid=10031&p=1&order=desc&t=被玩家杀死次数排行'"
                                                            target="main">被玩家杀死次数排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Fight/RoleRank.aspx?opid=10031&p=2&order=desc&t=被NPC杀死次数排行'"
                                                            target="main">被NPC杀死次数排行</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b11'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a111,b111,c111)' id='c111' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;流失统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a111'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/RoleLoseTimeFirst.aspx'" target="main">在线时长流失统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/RoleLoseTimeAll.aspx'" target="main">累计时长流失统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleLoseLevelPara.aspx'" target="main">等级段流失统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleLoseLevel.aspx'" target="main">每等级流失统计</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b111'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a112,b112,c112)' id='c112' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;公会统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a112'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/CorpsRankHonor.aspx'" target="main">公会繁荣度排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CorpsRankLevel.aspx'" target="main">公会等级排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CorpsRankUCount.aspx'" target="main">公会人数排行</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b112'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a21,b21,c21)' id='c21' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;用户统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a21'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserLoginZone.aspx'" target="main">用户登陆情况统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserLoginArea.aspx'" target="main">登陆区域分布统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserStateActive.aspx'" target="main">用户活跃统计</a>
                                                        <br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserStateLose.aspx'" target="main">用户流失统计</a>
                                                        <br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserStateBack.aspx'" target="main">用户回归统计</a>
                                                        <%--| <a href="Admin_IFrame.aspx?src=Admin_NoPage.Aspx"
                                                            target="main">明细</a>--%><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/UserLoginTimePara.aspx'" target="main">在线时长分布统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineLoginRank.aspx'" target="main">用户登录次数排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='Stats/RoleOnlineIPRank.aspx'" target="main">IP登录次数排行</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b21'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a22,b22,c22)' id='c22' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;GM统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a22'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">操作类型统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Stats/GMQuery.aspx'" target="main">GM日志查询</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b22'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a1,b1,c1)' id='c1' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;聊天相关</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a1'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/ChatTypeNumD.aspx'" target="main">聊天数量统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ChatRoleNumRank.aspx'" target="main">聊天数量排行(世界)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ChatRoleRpeatRank.aspx'" target="main">重复聊天统计(世界)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ChatQuery_Hours.aspx'" target="main">一小时聊天内容排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='Admin_NoPage.Aspx'" target="main">可疑聊天统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ChatQuery.Aspx'" target="main">聊天内容查询</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b1'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a41,b41,c41)' id='c41' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;任务统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a41'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskAcceptRank.aspx'" target="main">任务接受排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskAcceptRankRT.aspx'" target="main">重复接受任务排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskFinishRank.aspx'" target="main">任务完成排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskFinishRankRT.aspx'" target="main">重复完成任务排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskAcceptFinish.aspx'" target="main">任务完成比统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/Other/TypeTotal.aspx?opid=50066&order=desc&t=活动完成情况统计'"
                                                            target="main">活动完成情况统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskBugList.aspx'" target="main">问题任务列表</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/TaskQuery.aspx'" target="main">任务日志查询</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b41'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                            <tr>
                                                                <td onclick='show(a51,b51,c51)' id='c51' class='Titbg Font4' style='cursor: pointer'>
                                                                    <strong>&nbsp;充值统计</strong>
                                                                </td>
                                                            </tr>
                                                            <tr id='a51'>
                                                                <td class="menubg">
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountDepositRank_Money.aspx'" target="main">
                                                                        充值钱数排行*</a><br />
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountDepositRank_Time.aspx'" target="main">
                                                                        充值次数排行*</a><br />
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountDepositRank_All.aspx'" target="main">
                                                                        整体充值统计*</a><br />
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountInterzone.aspx'" target="main">充值区间统计</a><br />
                                                                    ────────<br>
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountQuery.aspx'" target="main">充值用户查询</a><br />
                                                                    <a href="Admin_IFrame.aspx?src='stats/UserAccountLogQuery.aspx'" target="main">充值记录查询</a><br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id='b51'>
                                                    <td height="6">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td onclick='show(a52,b52,c52)' id='c52' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;商城统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a52'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale.aspx'" target="main">商城销售统计(总)(服)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale_DayALL.aspx'" target="main">商城销售统计(总)(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale_Day.aspx'" target="main">商城销售统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale_Day_01.aspx'" target="main">商城交易笔数统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale_Day_02.aspx'" target="main">商城消费帐号数统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSale_Day_03.aspx'" target="main">商城消费元宝数统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_Goods.aspx'" target="main">商品销售统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_Goods_Day_GoodsCount.aspx'" target="main">
                                                            商品销售数量统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_Goods_Day_MoneyCount.aspx'" target="main">
                                                            商品销售元宝统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_Goods_Day_TradeCount.aspx'" target="main">
                                                            商品销售笔数统计(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_Items.aspx'" target="main">道具销售统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleRoleLevel.aspx'" target="main">玩家购买统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_ExcelLevel.aspx?shoptype=0&title=物品等级消耗统计'"
                                                            target="main">物品等级消耗统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_RoleIDCount.aspx?shoptype=0&title=商品购买排行'"
                                                            target="main">商品购买排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleCount.aspx?shoptype=0&title=购买次数排行'"
                                                            target="main">购买次数排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleB.aspx'" target="main">商城销售统计 绑.总.服</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleB_DayALL.aspx'" target="main">商城销售统计 绑.总.天</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleB_Day.aspx'" target="main">商城销售统计(绑)(天)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_GoodsB.aspx'" target="main">商品销售统计(绑)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_ItemsB.aspx'" target="main">道具销售统计(绑)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleRoleLevelB.aspx'" target="main">玩家购买统计(绑)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_ExcelLevel.aspx?shoptype=1&title=物品等级消耗统计(绑)'"
                                                            target="main">物品等级消耗统计(绑)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_RoleIDCount.aspx?shoptype=1&title=商品购买排行(绑)'"
                                                            target="main">商品购买排行(绑)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleCount.aspx?shoptype=1&title=购买次数排行(绑)'"
                                                            target="main">购买次数排行(绑)</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopQuery.aspx'" target="main">商城日志查询</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=0&title=商城物品销售日志'"
                                                            target="main">商城物品销售日志</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=1&title=商城物品销售日志(绑)'"
                                                            target="main">商城物品销售日志(绑)</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/AccountInterzoneShopSaleItems.aspx?shoptype=1&title=玩家消费习惯统计'"
                                                            target="main">玩家消费习惯统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/AccountInterzoneShopSaleItemsB.aspx?shoptype=1&title=玩家消费习惯统计'"
                                                            target="main">玩家消费习惯统计(绑定)</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b52'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a53,b53,c53)' id='c53' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;元宝统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a53'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/UserAccountTradeAll.aspx'" target="main">整体提取统计*</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10055&p=0&order=asc&t=角色提取排行'"
                                                            target="main">角色提取排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10055&p=fuzhi&order=asc&t=角色消耗排行'"
                                                            target="main">角色消耗排行</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10055&p=2&order=asc&t=角色购物排行'"
                                                            target="main">角色购物排行</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10056&p=zhengzhi&order=asc&t=角色获取排行(绑定)'"
                                                            target="main">角色获取排行(绑定)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10056&p=fuzhi&order=asc&t=角色消耗排行(绑定)'"
                                                            target="main">角色消耗排行(绑定)</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/Gold/RoleRank.aspx?opid=10056&p=2&order=asc&t=角色购物排行(绑定)'"
                                                            target="main">角色购物排行(绑定)</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/GoldQuery.aspx'" target="main">元宝日志查询</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b53'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a61,b61,c61)' id='c61' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;推广统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a61'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/Count.aspx'" target="main">安装\卸载统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/CountItem.aspx'" target="main">操作系统统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CountItem.aspx?item=cast(F_ScreenWidth as varchar(10))%2b'*'%2bcast(F_ScreenHeight as varchar(10))&title=系统分辨率统计'"
                                                            target="main">系统分辨率统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CountItem.aspx?item=F_Area&title=所在地区统计'" target="main">
                                                            所在地区统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CountItem.aspx?item=F_Language&title=系统语言统计'"
                                                            target="main">系统语言统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/CountItem.aspx?item=F_WinBit&title=系统架构统计'"
                                                            target="main">系统架构统计</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/MediaBaiduIndex.aspx'" target="main">百度指数</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/MediaWeiboZone.aspx'" target="main">微博转发统计</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/MediaWeiboQuery.aspx'" target="main">微博转发查询</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b61'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a71,b71,c71)' id='c71' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;问卷调查</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a71'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=1'" target="main">男女比例</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=2'" target="main">年龄分布</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=3'" target="main">用户来源</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=4'" target="main">游戏类型</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=5'" target="main">上网游戏时间</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=6'" target="main">累计游戏时间</a><br />
                                                        ────────<br>
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=7'" target="main">游戏地址</a><br />
                                                        <a href="Admin_IFrame.aspx?src='stats/QuestSex.aspx?itemid=8'" target="main">意愿消费</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b71'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="ShowM" style='display: none'>
                            <td>
                                <table width='100%' cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td onclick='show(a81,b81,c81)' id='c81' class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;服务器统计</strong>
                                                    </td>
                                                </tr>
                                                <tr id='a81'>
                                                    <td class="menubg">
                                                        <a href="Admin_IFrame.aspx?src='stats/ZoneInfo.aspx'" target="main">服务器基本状态统计</a><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id='b81'>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="5" width='100%' align='center' border="0" class="LeftBG">
                                                <tr>
                                                    <td class='Titbg Font4' style='cursor: pointer'>
                                                        <strong>&nbsp;版权信息</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="menubg">
                                                        <a target="_blank" href="http://www.shenlongyou.cn">版权：北京小小传奇网络信息有限公司</a><br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
