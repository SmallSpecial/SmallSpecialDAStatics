<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReimburseLog.aspx.cs" Inherits="WebWSS.ModelPage.Reimburse.ReimburseLog" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css"></link>
    <script language='JavaScript' src='../../img/Admin.Js'></script>
    <script type="text/javascript" src='../../img/GetDate.Js'></script>
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
<body onload="remove_loading();">
    <div id="loader_container">
        <div id="loader">
            <div align="center">
                <asp:Label runat="server" Text="<%$ Resources:Language,Tip_PageLoading %>"></asp:Label>
            </div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Reimburse/Reimburse.aspx" Text="<%$Resources:Language,LblReimburse%>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Reimburse/ReimburseLog.aspx" Text="<%$Resources:Language,LblOper%>"></asp:LinkButton>
            | &nbsp;
        </div>
        <div class="search">
            <asp:Label runat="server" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>:
            <asp:TextBox runat="server" ID="tbRoleID"></asp:TextBox>
            &nbsp;
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" ShowFooter="false" AllowPaging="true">
                <Columns>
                    <asp:BoundField DataField="GlobalID" HeaderText="<%$Resources:Language,LblRoleNo %>" />
                    <asp:BoundField DataField="CostGold" HeaderText="<%$Resources:Language,LblRedDiamond %>" />
                    <asp:BoundField DataField="CostBindGold" HeaderText="<%$Resources:Language,LblBlueDiamond %>" />
                    <asp:BoundField DataField="CostMoney" HeaderText="<%$Resources:Language,LblMoney %>" />
                    <asp:BoundField DataField="CostBindMoney" HeaderText="<%$Resources:Language,LblBindMoney %>" />
                    <asp:BoundField DataField="ItemID1" HeaderText="ItemID1" />
                    <asp:BoundField DataField="ItemNum1" HeaderText="ItemNum1" />
                    <asp:BoundField DataField="ItemID2" HeaderText="ItemID2" />
                    <asp:BoundField DataField="ItemNum2" HeaderText="ItemNum2" />
                    <asp:BoundField DataField="ItemID3" HeaderText="ItemID3" />
                    <asp:BoundField DataField="ItemNum3" HeaderText="ItemNum3" />
                    <asp:BoundField DataField="ItemID4" HeaderText="ItemID4" />
                    <asp:BoundField DataField="ItemNum4" HeaderText="ItemNum4" />
                    <asp:BoundField DataField="ItemID5" HeaderText="ItemID5" />
                    <asp:BoundField DataField="ItemNum5" HeaderText="ItemNum5" />
                    <asp:BoundField DataField="F_IS_Use_Mail" HeaderText="<%$Resources:Language,LblIsUseEmail%>" />
                    <asp:BoundField DataField="F_Mail_Title" HeaderText="<%$Resources:Language,LblEmailTitle %>" />
                    <asp:BoundField DataField="F_Sender_Name" HeaderText="<%$Resources:Language,LblSendPeople %>" />
                    <asp:BoundField DataField="F_Mail_Content"  HeaderText="<%$Resources:Language,LblMailContent %>" />
                    <asp:BoundField DataField="AddTime" HeaderText="<%$Resources:Language,LblOpTime%>" />
                </Columns>
                <PagerTemplate>
                    <div style="text-align: right; color: Blue">
                        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" Text="<%$Resources:Language,LblHomePage %>">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" Text="<%$Resources:Language,LblPrviousPage %>">
                        </asp:LinkButton>
                        &nbsp;
                        第
                        <asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'>
                        </asp:Label>
                        页/共
                        &nbsp;
                        <asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'>
                        </asp:Label>
                        &nbsp;
                        页
                        <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:LAnguage,LblNextPage %>">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini"></asp:TextBox>页<asp:Button ID="Button3" runat="server" OnClick="Go_Click" Text="<%$Resources:Language,LblGoto %>" />&nbsp;
                    </div>
                </PagerTemplate>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#D1DDF1" />
            </asp:GridView>
            <asp:Label ID="lblTxt" runat="server" Text="<%$Resources:Language,LblIsUseEmail%>"></asp:Label>0:No;1:Yes
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
