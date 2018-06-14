<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CDKeyBatchList.aspx.cs"
    Inherits="WebWSS.CDKey.CDKeyBatchList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

</head>
<body>
    <form id="form1" runat="server" >
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchList.aspx" Enabled="False" Text="<%$Resources:Language,CDK_BatchManage %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyList.aspx" Enabled="True" Text="<%$Resources:Language,CDK_CDKList %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyCreate.aspx"  Text="<%$Resources:Language,CDK_CDKeyCreate %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyImport.aspx" Text="<%$Resources:Language,CDK_CDKImport %>"></asp:LinkButton>
        </div>
        <div class="search">
            <asp:Label Text="<%$Resources:Language,CDK_CardType %>" runat="server"></asp:Label>
            :<asp:DropDownList ID="ddlKeyType" runat="server">
                <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,CDK_AwardNo %>"></asp:Label>
            :<asp:TextBox ID="tboxExcelID" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
            &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,CDK_RemarkText %>"></asp:Label><asp:TextBox ID="tboxNote" runat="server" Width="180px" MaxLength="60"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
            &nbsp;
            <asp:Button ID="btnExcel" runat="server" Text="EXCEL" CssClass="button" OnClick="btnExcel_Click" />
        </div>
        <div class="titletip">
            <asp:Label ID="lblPageType" runat="server" Text="0" Visible="false"></asp:Label>
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 96%;">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                    AutoGenerateColumns="False" Font-Size="12px" Width="100%" OnSorting="GridView1_Sorting"
                    CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" 
                    onrowcommand="GridView1_RowCommand"  DataKeyField="F_BatchID">
                    <Columns>
                        <asp:BoundField DataField="F_BatchID" HeaderText="<%$Resources:Language,CDK_BatchNo %>"></asp:BoundField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,CDK_CardType %>">
                            <ItemTemplate>
                                <%#GetTypeName(ddlKeyType, Eval("F_KeyType").ToString())%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="F_KeyCount" HeaderText="<%$Resources:Language,CDK_GenerateNumber %>"></asp:BoundField>
                        <asp:BoundField DataField="F_ExcelID" HeaderText="<%$Resources:Language,CDK_AwardNo %>">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="F_ItemNum" HeaderText="<%$Resources:Language,CDK_AwardNumber %>">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="F_KeyCount" HeaderText="<%$Resources:Language,CDK_GenerateNumber %>"></asp:BoundField>
                        <asp:BoundField DataField="F_CreateTime" HeaderText="<%$Resources:Language,CDK_GenerateTime %>"></asp:BoundField>
                        <asp:BoundField DataField="F_Note" HeaderText="<%$Resources:Language,CDK_Remark %>"></asp:BoundField>
                        <asp:BoundField DataField="F_IsPublish" HeaderText="<%$Resources:Language,CDK_IsPublish %>"></asp:BoundField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,LblOperate %>">
                            <ItemTemplate>
                                 <asp:LinkButton ID="LinkButton1" runat="server" CommandName="List" CommandArgument ='<%# Eval("F_BatchID") %>'  Text="<%$Resources:Language,CDK_WatchCDKList %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                    <asp:LinkButton ID="lbtnF" runat="server" ForeColor="#FFFFFF" OnClick="lbtnF_Click"   Text="<%$Resources:Language,LblHomePage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnP" runat="server" ForeColor="#FFFFFF" OnClick="lbtnP_Click"   Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnN" runat="server" ForeColor="#FFFFFF" OnClick="lbtnN_Click" Text="<%$Resources:Language,LblNextPage %>"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbtnE" runat="server" ForeColor="#FFFFFF" OnClick="lbtnE_Click"     Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>&nbsp;
                    <asp:TextBox ID="tboxPageIndex" runat="server" Width="30px" MaxLength="6">1</asp:TextBox>
                    <asp:Button ID="btnPage" runat="server" Text="<%$Resources:Language,LblGoto %>" CssClass="button" OnClick="btnPage_Click" />
                </div>
            </div>
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
            <br />
            <asp:Label runat="server" Text="<%$Resources:Language,CDK_TipCDKNo %>"></asp:Label>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
