<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopBaseItemList.aspx.cs"
    Inherits="WebWSS.Shop.ShopBaseItemList" %>

<%@ Register Src="../Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopItemList.aspx">商城商品</asp:LinkButton>
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopItemImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopBaseItemList.aspx">基础道具</asp:LinkButton>
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopBaseItemImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
        </div>
        <div class="search" style="margin: 5px 0px;">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            商城类型:<asp:DropDownList ID="ddlShopType" runat="server" Width="80px">
                <asp:ListItem Value="-1"  Text="<%$Resources:Language,LblAllType %>"></asp:ListItem>
                <asp:ListItem Value="0">元宝</asp:ListItem>
                <asp:ListItem Value="1">绑定元宝</asp:ListItem>
            </asp:DropDownList>
            商品类型:<asp:DropDownList ID="ddlItemType" runat="server" Width="80px">
                <asp:ListItem Value="-1"  Text="<%$Resources:Language,LblAllType %>"></asp:ListItem>
                <asp:ListItem Value="0">宝石</asp:ListItem>
                <asp:ListItem Value="1">法宝</asp:ListItem>
                <asp:ListItem Value="2">坐骑</asp:ListItem>
                <asp:ListItem Value="3">时装</asp:ListItem>
                <asp:ListItem Value="4">奇珍</asp:ListItem>
                <asp:ListItem Value="5">药水</asp:ListItem>
            </asp:DropDownList>
            &nbsp; EXCEL编号:<asp:TextBox ID="tboxEXCELID" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
            &nbsp; 商品价格:<asp:TextBox ID="tboxPrice" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="buttonbl" OnClick="ButtonSearch_Click" />
            &nbsp;<asp:Button ID="btnExcel" runat="server" Text="EXCEL" CssClass="buttonbl" OnClick="btnExcel_Click" />
        </div>
        <div class="titletip">
            <asp:Label ID="lblPageType" runat="server" Text="0" Visible="false"></asp:Label><%--//0普通分页 1连续ID分页--%>
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 98%;">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                    AutoGenerateColumns="False" Font-Size="12px" Width="100%" CellPadding="3" BorderWidth="1px"
                    BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging"
                    ShowFooter="True" DataKeyNames="F_ID" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing"
                    OnRowDeleting="GridView1_RowDeleting" 
                    OnRowUpdating="GridView1_RowUpdating" PageSize="15" 
                    onrowcancelingedit="GridView1_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("F_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <span style="color: Red">*</span>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <span style="color: Red">*</span>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品名称">
                            <ItemTemplate>
                                <%#GetTextName(Eval("F_EXCELID").ToString())%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%#GetTextName(Eval("F_EXCELID").ToString())%>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblItemName" runat="server" ForeColor="red" Text="*"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EXCEL编号">
                            <ControlStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblF_EXCELID" runat="server" Text='<%# Eval("F_EXCELID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:TextBox ID="tboxF_EXCELID" runat="server" Width="80px" MaxLength="10" Text='<%#Eval("F_EXCELID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tboxF_EXCELID" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商城类型">
                            <ItemTemplate>
                                <%#GetTypeName(ddlShopType, Eval("F_SHOPTYPE").ToString())%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlF_SHOPTYPE" runat="server" Width="90px">
                                    <asp:ListItem Value="0">元宝</asp:ListItem>
                                    <asp:ListItem Value="1">绑定元宝</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblF_SHOPTYPE" runat="server" Text='<%# Eval("F_SHOPTYPE") %>' Visible=false></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlF_SHOPTYPE" runat="server" Width="90px">
                                    <asp:ListItem Value="0">元宝</asp:ListItem>
                                    <asp:ListItem Value="1">绑定元宝</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品类型">
                            <ItemTemplate>
                                <%#GetTypeName(ddlItemType,Eval("F_ITEMTYPE").ToString())%>
                                <%--<%# Convert.ToInt32(Eval("F_ITEMTYPE")) == 0 ? "True" : "False"%>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlF_ITEMTYPE" runat="server" Width="130px">
                                    <asp:ListItem Value="0">宝石</asp:ListItem>
                                    <asp:ListItem Value="1">法宝</asp:ListItem>
                                    <asp:ListItem Value="2">坐骑</asp:ListItem>
                                    <asp:ListItem Value="3">时装</asp:ListItem>
                                    <asp:ListItem Value="4">奇珍</asp:ListItem>
                                    <asp:ListItem Value="5">药水</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblF_ITEMTYPE" runat="server" Text='<%# Eval("F_ITEMTYPE") %>' Visible=false></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <<asp:DropDownList ID="ddlF_ITEMTYPE" runat="server" Width="130px">
                                    <asp:ListItem Value="0">宝石</asp:ListItem>
                                    <asp:ListItem Value="1">法宝</asp:ListItem>
                                    <asp:ListItem Value="2">坐骑</asp:ListItem>
                                    <asp:ListItem Value="3">时装</asp:ListItem>
                                    <asp:ListItem Value="4">奇珍</asp:ListItem>
                                    <asp:ListItem Value="5">药水</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品价格">
                            <ItemTemplate>
                                <%#Eval("F_ITEMPRICE").ToString()%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tboxF_ITEMPRICE" runat="server" Width="80px" MaxLength="10" Text=<%#Eval("F_ITEMPRICE").ToString()%>></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tboxF_ITEMPRICE" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,LblOperate %>">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="编辑" CssClass="buttonblue" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("确认要删除吗？");'
                                    Text="删除" CssClass="buttonblue" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新" CssClass="buttonblue" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" CssClass="buttonblue" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="新 增" CssClass="buttonbl" />
                                <asp:Button ID="btnCancel0" runat="server" OnClick="btnCancel_Click" Text="取消" CssClass="buttonblue"
                                    Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                        ForeColor="White" />
                    <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                    <FooterStyle HorizontalAlign="Center" BackColor="#e6e8ea" ForeColor="#FFFFFF" Font-Size="Medium" />
                    <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                    <EditRowStyle BackColor="#e6e8ea" />
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
                    <asp:LinkButton ID="lbtnF" runat="server" ForeColor="#FFFFFF" OnClick="lbtnF_Click"  Text="<%$Resources:Language,LblHomePage %>"></asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnP" runat="server" ForeColor="#FFFFFF" OnClick="lbtnP_Click"   Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnN" runat="server" ForeColor="#FFFFFF" OnClick="lbtnN_Click"   Text="<%$Resources:Language,LblPrviousPage %>"></asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnE" runat="server" ForeColor="#FFFFFF" OnClick="lbtnE_Click"     Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>
                    &nbsp;
                    <asp:TextBox ID="tboxPageIndex" runat="server" Width="30px" MaxLength="6">1</asp:TextBox>
                    <asp:Button ID="btnPage" runat="server" Text="<%$Resources:Language,LblGoto %>" CssClass="button" OnClick="btnPage_Click" />
                </div>
            </div>
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
