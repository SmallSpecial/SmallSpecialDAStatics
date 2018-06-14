<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemQuery.aspx.cs" Inherits="WebWSS.Stats.ItemQuery" %>

<%@ Register Src="../Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

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

    <style>
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
<body onLoad="remove_loading();">
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
    <div class="main">
        <div class="itemtitle">
            <asp:Label ID="LabelTitle" runat="server" Text="道具统计>>道具日志查询"></asp:Label>
        </div>
        <div class="search">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,LblQueryTime %>"></asp:Label>:<asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:Label runat="server" Text="<%$Resources:Language,LblEventType %>"></asp:Label>:<asp:DropDownList ID="ddlGoldType" runat="server">
                <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>"></asp:ListItem>
                <asp:ListItem Value="10047">获得道具</asp:ListItem>
                <asp:ListItem Value="10048">消毁或使用道具</asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" Text="<%$Resources:Language,LblOperateType %>"></asp:Label><asp:DropDownList ID="ddlPara2Type" runat="server">
                <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>"></asp:ListItem>
                <asp:ListItem Value="-1">无效类型(日志不记录，道具归包时使用)</asp:ListItem>
                <asp:ListItem Value="0">无效类型</asp:ListItem>
                <asp:ListItem Value="1">法宝技能刷新奖励</asp:ListItem>
                <asp:ListItem Value="2">从地上捡起</asp:ListItem>
                <asp:ListItem Value="3">镇神关奖励</asp:ListItem>
                <asp:ListItem Value="4">兑换道具</asp:ListItem>
                <asp:ListItem Value="5">坐骑分解</asp:ListItem>
                <asp:ListItem Value="6">帮会领地奖励</asp:ListItem>
                <asp:ListItem Value="7">累计奖励</asp:ListItem>
                <asp:ListItem Value="8">坐骑繁殖</asp:ListItem>
                <asp:ListItem Value="9">坐骑抓捕</asp:ListItem>
                <asp:ListItem Value="10">英雄榜奖励</asp:ListItem>
                <asp:ListItem Value="11">GM指令给道具</asp:ListItem>
                <asp:ListItem Value="12">杀怪任务掉落道具</asp:ListItem>
                <asp:ListItem Value="13">接受任务给任务道具</asp:ListItem>
                <asp:ListItem Value="14">采集</asp:ListItem>
                <asp:ListItem Value="15">NPC处购买</asp:ListItem>
                <asp:ListItem Value="16">活跃值奖励</asp:ListItem>
                <asp:ListItem Value="17">命魂盒奖励</asp:ListItem>
                <asp:ListItem Value="18">数据库礼品</asp:ListItem>
                <asp:ListItem Value="19">怪物掉落</asp:ListItem>
                <asp:ListItem Value="20">使用道具包</asp:ListItem>
                <asp:ListItem Value="21">精炼石合成</asp:ListItem>
                <asp:ListItem Value="22">装备分解</asp:ListItem>
                <asp:ListItem Value="23">兑换奖励</asp:ListItem>
                <asp:ListItem Value="24">城战报名失败返还道具</asp:ListItem>
                <asp:ListItem Value="25">脚本给道具</asp:ListItem>
                <asp:ListItem Value="26">签到奖励</asp:ListItem>
                <asp:ListItem Value="27">任务奖励</asp:ListItem>
                <asp:ListItem Value="28">补偿奖励</asp:ListItem>
                <asp:ListItem Value="29">商城购买</asp:ListItem>
                <asp:ListItem Value="30">GM刻道具</asp:ListItem>
                <asp:ListItem Value="31">玩家交易</asp:ListItem>
                <asp:ListItem Value="32">NPC处回购</asp:ListItem>
                <asp:ListItem Value="33">生活技能合成</asp:ListItem>
                <asp:ListItem Value="34">收取邮件附件</asp:ListItem>
                <asp:ListItem Value="35">每日奖励</asp:ListItem>
                <asp:ListItem Value="36">离线活动奖励</asp:ListItem>
                <asp:ListItem Value="20001">##脚本删除道具</asp:ListItem>
                <asp:ListItem Value="20002">##坐骑分解，删除坐骑</asp:ListItem>
                <asp:ListItem Value="20003">##坐骑繁殖，消耗道具</asp:ListItem>
                <asp:ListItem Value="20004">##坐骑喂食，消耗道具</asp:ListItem>
                <asp:ListItem Value="20005">##坐骑星级刷新，消耗道具</asp:ListItem>
                <asp:ListItem Value="20006">##坐骑资质刷新，消耗道具</asp:ListItem>
                <asp:ListItem Value="20007">##坐骑先天悟道，消耗道具</asp:ListItem>
                <asp:ListItem Value="20008">##坐骑后天悟道，消耗道具</asp:ListItem>
                <asp:ListItem Value="20009">##坐骑繁殖失败，删除子坐骑</asp:ListItem>
                <asp:ListItem Value="20010">##法宝升星</asp:ListItem>
                <asp:ListItem Value="20011">##法宝融合</asp:ListItem>
                <asp:ListItem Value="20012">##法宝技能刷新，消耗道具</asp:ListItem>
                <asp:ListItem Value="20013">##法宝资质提升，消耗道具</asp:ListItem>
                <asp:ListItem Value="20014">##GM指令清空面板</asp:ListItem>
                <asp:ListItem Value="20015">##GM指令删除道具</asp:ListItem>
                <asp:ListItem Value="20016">##GM指令完成任务，删除任务相关道具</asp:ListItem>
                <asp:ListItem Value="20017">##报名联盟城战</asp:ListItem>
                <asp:ListItem Value="20018">##创建帮会，消耗道具</asp:ListItem>
                <asp:ListItem Value="20019">##领取离线经验</asp:ListItem>
                <asp:ListItem Value="20020">##领取离线活动收益</asp:ListItem>
                <asp:ListItem Value="20021">##镖车升级，消耗道具</asp:ListItem>
                <asp:ListItem Value="20022">##兑换售卖，消耗道具</asp:ListItem>
                <asp:ListItem Value="20023">##发布金号角，消耗道具</asp:ListItem>
                <asp:ListItem Value="20024">##发布银号角，消耗道具</asp:ListItem>
                <asp:ListItem Value="20025">##发布超级宣言，消耗道具</asp:ListItem>
                <asp:ListItem Value="20026">##使用生活技能，消耗材料</asp:ListItem>
                <asp:ListItem Value="20027">##命魂盒换牌，消耗道具</asp:ListItem>
                <asp:ListItem Value="20028">##印记升级，消耗道具</asp:ListItem>
                <asp:ListItem Value="20029">##跨场景寻路，消耗道具</asp:ListItem>
                <asp:ListItem Value="20030">##洪荒任务刷新，消耗道具</asp:ListItem>
                <asp:ListItem Value="20031">##豪礼赠送活动，消耗道具</asp:ListItem>
                <asp:ListItem Value="20032">##装备精炼，消耗道具</asp:ListItem>
                <asp:ListItem Value="20033">##装备精炼转移，删除副装备</asp:ListItem>
                <asp:ListItem Value="20034">##装备幻化，消耗道具</asp:ListItem>
                <asp:ListItem Value="20035">##精炼石合成，删除精炼石</asp:ListItem>
                <asp:ListItem Value="20036">##装备分解，删除装备</asp:ListItem>
                <asp:ListItem Value="20037">##装备重铸，消耗道具</asp:ListItem>
                <asp:ListItem Value="20038">##装备铭刻，消耗道具</asp:ListItem>
                <asp:ListItem Value="20039">##兑换奖励，消耗道具</asp:ListItem>
                <asp:ListItem Value="20040">##上缴天晶，消耗道具</asp:ListItem>
                <asp:ListItem Value="20041">##任务完成，删除任务相关道具</asp:ListItem>
                <asp:ListItem Value="20042">##删除任务，删除任务相关道具</asp:ListItem>
                <asp:ListItem Value="20043">##耐久为0，删除道具</asp:ListItem>
                <asp:ListItem Value="20044">##使用道具</asp:ListItem>
                <asp:ListItem Value="20045">##传送到BOSS所在场景，消耗道具</asp:ListItem>
                <asp:ListItem Value="20046">##道具叠加，删除道具</asp:ListItem>
                <asp:ListItem Value="20047">##丢弃道具</asp:ListItem>
                <asp:ListItem Value="20048">##地上的道具被捡起后从房间删除（仅完全合并时）</asp:ListItem>
                <asp:ListItem Value="20049">##活动报名，消耗道具</asp:ListItem>
                <asp:ListItem Value="20050">##龙魂升阶，消耗道具</asp:ListItem>
                <asp:ListItem Value="20051">##死亡掉落，删除道具</asp:ListItem>
                <asp:ListItem Value="20052">##进入炼神台，消耗道具</asp:ListItem>
                <asp:ListItem Value="20053">##卖给NPC</asp:ListItem>
                <asp:ListItem Value="20054">##玩家交易</asp:ListItem>
                <asp:ListItem Value="20055">##放到拍卖行</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            &nbsp; 帐户编号:<asp:TextBox ID="tboxUID" runat="server" Width="40px" MaxLength="16"></asp:TextBox>
            &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,LblUserNo %>"></asp:Label>:<asp:TextBox ID="tboxCID" runat="server" Width="40px" MaxLength="16"></asp:TextBox>
           &nbsp; 道具编号:<asp:TextBox ID="tboxPARA_1" runat="server" Width="148px" MaxLength="160"></asp:TextBox>
         &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,CDK_Remark %>"></asp:Label>:
            <asp:TextBox ID="tboxBAK" runat="server" Width="148px" MaxLength="160"></asp:TextBox>
            &nbsp;
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7><div id="DateSelect" runat="Server"></div>
                <uc1:ControlOutFile ID="ControlOutFile1" runat="server" />
               <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:                <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>:<span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
            <asp:Label ID="lblPageType" runat="server" Text="1" Visible="false"></asp:Label>
            <asp:Label ID="lblEncoding" runat="server" Text="<%$Resources:Language,LblCodeOpen %>" Visible="true"></asp:Label>
            <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="false"></asp:Label>
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 96%;">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                    AutoGenerateColumns="False" Font-Size="12px" Width="100%" OnSorting="GridView1_Sorting"
                    CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="帐户编号">
                            <ItemStyle Width="80px" />
                            <ItemTemplate>
                                <%#Eval("UID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,LblRoleNo %>">
                            <ItemStyle Width="130px" />
                            <ItemTemplate>
                                <%#GetRoleName(DropDownListArea1.SelectedValue.Split(',')[1],Eval("CID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--       <asp:BoundField DataField="OPID" HeaderText="事件编号">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>--%>
                        <asp:TemplateField     HeaderText="<%$Resources:Language,LblEventType %>" >
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <%#GetTypeName(ddlGoldType, Eval("OPID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,LblOperateType %>">
                            <ItemTemplate>
                                <%#Eval("OPID").ToString() == "10047" || Eval("OPID").ToString() == "10048" ? GetTypeName(ddlPara2Type, Eval("PARA_2").ToString()) : Eval("PARA_2").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
<%--                        <asp:BoundField DataField="PARA_1" HeaderText="道具编号">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="道具名称">
                            <ItemTemplate>
                                <%#GetTextName(Eval("PARA_1").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                   <%--     <asp:BoundField DataField="OP_BAK" HeaderText="备注"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="备注格式化">
                            <ItemTemplate>
                                <%#GetOPBakFormate(Eval("OP_BAK").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OP_TIME" HeaderText="<%$Resources:Language,LblTime %>">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                        ForeColor="White" />
                    <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                    <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                    <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="#D1DDF1" />
                </asp:GridView>
                <div style="margin: 5px 36px; text-align: right;">
                    <asp:Label ID="lblPageIndex" runat="server" Text="1" ForeColor="#FFFFFF"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="/" ForeColor="#FFFFFF"></asp:Label>
                    <asp:Label ID="lblPageCount" runat="server" Text="1" ForeColor="#FFFFFF"></asp:Label>&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="<%$Resources:Language,LblSum %>" ForeColor="#FFFFFF"></asp:Label><asp:Label
                        ID="lblCount" runat="server" Text="1" ForeColor="#FFFFFF"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnF" runat="server" ForeColor="#FFFFFF" OnClick="lbtnF_Click"  Text="<%$Resources:Language,LblHomePage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnP" runat="server" ForeColor="#FFFFFF" OnClick="lbtnP_Click"   Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnN" runat="server" ForeColor="#FFFFFF" OnClick="lbtnN_Click"   Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnE" runat="server" ForeColor="#FFFFFF" OnClick="lbtnE_Click"     Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>&nbsp;
                    <asp:TextBox ID="tboxPageIndex" runat="server" Width="30px" MaxLength="6">1</asp:TextBox>
                    <asp:Button ID="btnPage" runat="server" Text="<%$Resources:Language,LblGoto %>" CssClass="button" OnClick="btnPage_Click" />
                </div>
            </div>
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
