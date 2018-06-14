<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopBaseItemQuery.aspx.cs"
    Inherits="WebWSS.Shop.ShopBaseItemQuery" %>

<%@ Register Src="../Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_parent" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

    <script language="javascript" type="text/javascript">
        function SetValue(value) {
            parent.window.returnValue = value;
            this.window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:Label ID="LabelTitle" runat="server" Text="商城管理>>基础商品查询"></asp:Label>
        </div>
        <div class="search" style="margin: 5px 0px;">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged" Enabled="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged" Enabled="False">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            商城类型:<asp:DropDownList ID="ddlShopType" runat="server" Width="80px">
                <asp:ListItem Value="-1"  Text="<%$Resources:Language,LblAllType %>"></asp:ListItem>
                <asp:ListItem Value="0">元宝</asp:ListItem>
                <asp:ListItem Value="1">绑定元宝</asp:ListItem>
            </asp:DropDownList>
            <br />
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
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <asp:Label ID="lblPageType" runat="server" Text="0" Visible="false"></asp:Label><%--//0普通分页 1连续ID分页--%>
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 98%;">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                    AutoGenerateColumns="False" Font-Size="12px" Width="100%" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="F_ID" HeaderText="编号" DataFormatString="{0}"></asp:BoundField>
                        <asp:BoundField DataField="F_EXCELID" HeaderText="EXCEL编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="商品名称">
                            <ItemTemplate>
                                <%#GetTextName(Eval("F_EXCELID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商城类型">
                            <ItemTemplate>
                                <%#GetTypeName(ddlShopType, Eval("F_SHOPTYPE").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品类型">
                            <ItemTemplate>
                                <%#GetTypeName(ddlItemType,Eval("F_ITEMTYPE").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="F_ITEMPRICE" HeaderText="商品价格"></asp:BoundField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,LblOperate %>">
                            <ItemTemplate>
                               <input id="Button1" type="button" value="选择" class="buttonblue" onclick="return SetValue('<%#(Eval("F_EXCELID")+"|"+GetTextName(Eval("F_EXCELID").ToString())+"|"+Eval("F_SHOPTYPE")+"|"+Eval("F_ITEMTYPE")+"|"+Eval("F_ITEMPRICE"))%>');" />
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
                <div style="margin: 5px 36px; text-align: right;" id="divPage" runat="server">
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
