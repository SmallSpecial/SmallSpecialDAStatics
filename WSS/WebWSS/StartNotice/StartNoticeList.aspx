<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartNoticeList.aspx.cs" Inherits="WebWSS.StartNotice.StartNoticeList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>
    <style type="text/css">
        .queryItem {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="search" style="display: none">
                <span class="queryItem">
                    <asp:Label runat="server" Text="<%$ Resources:Language,LblRoleName %>"></asp:Label>:
                <asp:TextBox ID="txtRoleName" runat="server" Width="60px" MaxLength="10"></asp:TextBox>
                </span>
                <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
            </div>
            <div class="titletip">
            </div>
            <div>
                <asp:Button runat="server" Width="220" ID="btnAdd" Text="addNewOne" ToolTip="after add a empty notice,you can update it " OnClick="btnAdd_Click" />
            </div>
            <div class="gridv">
                <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 96%;">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="False" Font-Size="12px" Width="100%" OnSorting="GridView1_Sorting"
                        CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="GridView1_RowDataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowUpdating="GridView1_RowUpdating" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" PageSize="20" DataKeyNames="F_ID">
                        <Columns>
                            <asp:BoundField DataField="F_ID" HeaderText="<%$Resources:Language,LblNo%>" ReadOnly="true"></asp:BoundField>
                            <asp:BoundField DataField="F_TITLE" HeaderText="<%$Resources:Language,LblNo%>"></asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,CDK_RemarkText%>">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.F_NOTICEINFO") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtbox"></asp:TextBox>
                                    <asp:HiddenField ID="hfTasksToRole" runat="server" Value='<%# Eval("F_NOTICEINFO") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="F_NOTICEINFO" HeaderText="内容" ReadOnly="true"></asp:BoundField>--%>
                            <asp:BoundField DataField="F_STARTTIME" HeaderText="<%$Resources:Language,LblStartTime%>"></asp:BoundField>
                            <asp:BoundField DataField="F_ENDTIME" HeaderText="<%$Resources:Language,LblEndTime%>"></asp:BoundField>
                            <asp:BoundField DataField="F_CREATETIME" HeaderText="<%$Resources:Language,Lbl_CreateTime%>" ReadOnly="true"></asp:BoundField>
                            <asp:CommandField HeaderText="UPDATE" ShowEditButton="True" />
                            <asp:CommandField HeaderText="DELETE" ShowDeleteButton="True" />
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
                        <asp:Label ID="lblPageCount" runat="server" Text="1" ForeColor="#FFFFFF"></asp:Label>
                        &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="<%$Resources:Language,LblSum %>" ForeColor="#FFFFFF"></asp:Label><asp:Label
                        ID="lblCount" runat="server" Text="1" ForeColor="#FFFFFF"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnF" runat="server" ForeColor="#FFFFFF" OnClick="lbtnF_Click" Text="<%$Resources:Language,LblHomePage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:LinkButton ID="lbtnP" runat="server" ForeColor="#FFFFFF" OnClick="lbtnP_Click" Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:LinkButton ID="lbtnN" runat="server" ForeColor="#FFFFFF" OnClick="lbtnN_Click" Text="<%$Resources:Language,LblNextPage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:LinkButton ID="lbtnE" runat="server" ForeColor="#FFFFFF" OnClick="lbtnE_Click" Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:TextBox ID="tboxPageIndex" runat="server" Width="30px" MaxLength="6">1</asp:TextBox>
                        <asp:Button ID="btnPage" runat="server" Text="<%$Resources:Language,LblGoto %>" CssClass="button" OnClick="btnPage_Click" />
                    </div>
                </div>
                <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
