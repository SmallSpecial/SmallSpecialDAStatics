<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GssViewHisDisRoleChatAdd.aspx.cs" Inherits="WebWSS.ModelPage.GssTool.GssViewHisDisRoleChatAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='../../img/Admin.Js'></script>
    <script type="text/javascript" src='../../img/GetDate.Js'></script>
     <script src="../../Script/jquery-1.12.3.min.js"></script>
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
        $(function () {
            $('#sel_search_orderstatus').multipleSelect();
        })
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
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GssTool/GssQuery.aspx" Text="<%$Resources:Language,LblGssQuery %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GssTool/GssGiftAward.aspx" Text="<%$Resources:Language,LblAwardWorkOrder %>"></asp:LinkButton>
            | &nbsp;
           <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GssTool/GssFullServicesMail.aspx" Text="<%$Resources:Language,LblFullServicesMail %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GssTool/GssGameNotice.aspx" Text="<%$Resources:Language,BtnCallWorkOrder %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton5" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GssTool/GssViewHis.aspx" Text="<%$Resources:Language,LblViewHis %>"></asp:LinkButton>
            | &nbsp;
        </div>
        <div style="margin-top:20px;">
            <asp:Button ID="btnGiftAward" runat="server" Text="<%$Resources:Language,BtnAwardWorkOrderPoolHis %>" Width="240px" BorderColor="#135294" OnClick="btnGiftAward_Click"  />
            <asp:Button ID="btnDeleteGiftAward" runat="server" Text="<%$Resources:Language,LblDeleteGiftAward %>" Width="240px" BorderColor="#135294" OnClick="btnDeleteGiftAward_Click" />
            <asp:Button ID="btnFullServicesMail" runat="server" Text="<%$Resources:Language,BtnFullServerMailHis %>" Width="240px" BorderColor="#135294" OnClick="btnFullServicesMail_Click"  />
            <asp:Button ID="btnDeleteFullServicesMail" runat="server" Text="<%$Resources:Language,LblDeleteFullServicesMail %>" Width="240px" BorderColor="#135294" OnClick="btnDeleteFullServicesMail_Click"  />
            <asp:Button ID="btnLockUser" runat="server" Text="<%$Resources:Language,LblLockUser %>" Width="240px" BorderColor="#135294" OnClick="btnLockUser_Click" />
           <asp:Button ID="btnUnLockUser" runat="server" Text="<%$Resources:Language,LblUnLockUser %>" Width="240px" BorderColor="#135294" OnClick="btnUnLockUser_Click" />
           <br />
           <br />
           <asp:Button ID="btnLockRole" runat="server" Text="<%$Resources:Language,LblLockRole %>" Width="240px" BorderColor="#135294"  OnClick="btnLockRole_Click"/>
           <asp:Button ID="btnUnLockRole" runat="server" Text="<%$Resources:Language,LblUnLockRole %>" Width="240px" BorderColor="#135294"  OnClick="btnUnLockRole_Click"/>
           <asp:Button ID="btnDisRoleChatAdd" runat="server" Text="<%$Resources:Language,LblDisRoleChatAdd %>" Width="240px" BorderColor="#135294"  OnClick="btnDisRoleChatAdd_Click"/>
           <asp:Button ID="btnDisRoleChatDel" runat="server" Text="<%$Resources:Language,LblDisRoleChatDel %>" Width="240px" BorderColor="#135294"  OnClick="btnDisRoleChatDel_Click"/>
           <asp:Button ID="btnRoleRecovery" runat="server" Text="<%$Resources:Language,LblRoleRecovery %>" Width="240px" BorderColor="#135294"  OnClick="btnRoleRecovery_Click"/>
            <asp:Button ID="btnGameNoticeHis" runat="server" Text="<%$Resources:Language,BtnCallTankHis %>" Width="240" BorderColor="#135294" OnClick="btnGameNoticeHis_Click" />
        </div>
        <div style="margin-top:20px;margin-bottom:20px;">
            <asp:Label runat="server" Text="<%$Resources:Language,LblDisRoleChatAdd %>"></asp:Label>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" ShowFooter="false" AllowPaging="true">
                <Columns>
                    <asp:BoundField DataField="F_ID" HeaderText="<%$Resources:Language,Lbl_Order %>" />
                    <asp:BoundField DataField="F_OPID" HeaderText="<%$Resources:Language,LblOperate %>" />

                    <asp:BoundField DataField="F_LockID" HeaderText="<%$Resources:Language,LblRoleNo %>" />
                    <asp:BoundField DataField="F_LockName" HeaderText="<%$Resources:Language,LblRoleName %>" />
                    <asp:BoundField DataField="F_LockStartTime" HeaderText="<%$Resources:Language,LblStartTime %>" />
                    <asp:BoundField DataField="F_LockEndTime" HeaderText="<%$Resources:Language,LblEndTime %>" />
                    <asp:BoundField DataField="F_Bak" HeaderText="<%$Resources:Language,CDK_Remark %>" />

                    <asp:BoundField DataField="F_User" HeaderText="<%$Resources:Language,LblOpUser %>" />
                    <asp:BoundField DataField="F_CreateTime" HeaderText="<%$Resources:Language,LblOpTime %>"/>
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
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
