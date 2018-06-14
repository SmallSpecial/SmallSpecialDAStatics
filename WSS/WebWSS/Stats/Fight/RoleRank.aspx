﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleRank.aspx.cs" Inherits="WebWSS.Stats.Fight.RoleRank" %>

<%@ Register Src="../../Common/ControlDateSelect.ascx" TagName="ControlDateSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../../Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../../img/Admin.Js'></script>

    <script type="text/javascript" src='../../img/GetDate.Js'></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:Label ID="LabelTitle" runat="server" Text="<%$Resources:Language,LblRoleStatic %> "></asp:Label>
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </div>
        <div class="search">
            <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <%-- 线路:--%>&nbsp;<asp:Literal runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Literal>:
            <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <uc1:ControlDateSelect ID="ControlDateSelect" runat="server" />
        <div class="titletip">
            <h7>
                <uc2:ControlOutFile ID="ControlOutFile1" runat="server" />
                <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>: 
                 <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>
                &nbsp; &nbsp;
                <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>: 
                <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100">
                <Columns>
                    <asp:BoundField HeaderText="<%$Resources:Language,LblRank %>"  DataField="RowNum" DataFormatString="<%$Resources:Language,Msg_RankFormat %>"/>
                    <asp:TemplateField HeaderText="<%$Resources:Language,LblZone %>">
                        <ItemTemplate>
                            <%#GetZoneName(Eval("F_BigZone"), Eval("F_ZoneID"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="<%$Resources:Language,LblUserNo %>" DataField="F_UserID" />
                    <asp:BoundField HeaderText="<%$Resources:Language,LblRoleNo %>" DataField="F_RoleID" />
                    <asp:TemplateField HeaderText="<%$Resources:Language,LblRoleName %>">
                        <ItemTemplate>
                            <%# GetRoleName(Eval("F_RoleID").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="<%$Resources:Language,LblDieTimes %>" DataField="F_TradeCount" DataFormatString="<%$Resources:Language,Msg_TimesFormat %>" />
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
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
