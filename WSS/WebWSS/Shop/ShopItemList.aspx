<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopItemList.aspx.cs" Inherits="WebWSS.Shop.ShopItemList" %>

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
        <div class="search" style="margin: 4px 0px 6px 0px">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;|&nbsp;
            <asp:Button ID="btnExcel" runat="server" Text="EXCEL" CssClass="buttonbl" OnClick="btnExcel_Click" />
            <asp:Button ID="btnAdd" runat="server" Text="新 增" CssClass="buttonbl" OnClick="btnAdd_Click" />
        </div>
        <div class="titletip">
            <span id="spanShopType" runat="Server">
                <asp:Button ID="btnShopTypeJB" runat="server" Text="元宝" CssClass="buttonblo" Enabled="False"
                    OnClick="btnShopType_Click" />
                <asp:Button ID="btnShopTypeJP" runat="server" Text="绑定元宝" CssClass="buttonbl" OnClick="btnShopType_Click" />
            </span>| <span id="spanItemType" runat="Server">
                <asp:Button ID="btnItemType00" runat="server" Text="促销" CssClass="buttonblo" Enabled="False"
                    OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType01" runat="server" Text="热销" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType0" runat="server" Text="宝石" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType1" runat="server" Text="法宝" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType2" runat="server" Text="坐骑" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType3" runat="server" Text="时装" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType4" runat="server" Text="奇珍" CssClass="buttonbl" OnClick="btnItemType_Click" />
                <asp:Button ID="btnItemType5" runat="server" Text="药水" CssClass="buttonbl" OnClick="btnItemType_Click" />
            </span>|
            <asp:Label ID="lblPageType" runat="server" Text="0" Visible="false"></asp:Label>
            <asp:Label ID="lblPageSize" runat="server" Text="12" Visible="false"></asp:Label>
            <asp:Label ID="lblEncoding" runat="server" Text="<%$Resources:Language,LblCodeOpen %>" Visible="true"></asp:Label>
            <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="true"></asp:Label>
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 96%;">
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" Width="100%" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="0" GridLines="Both"
                    RepeatDirection="Horizontal" OnEditCommand="DataList1_EditCommand" OnCancelCommand="DataList1_CancelCommand"
                    OnDeleteCommand="DataList1_DeleteCommand" OnUpdateCommand="DataList1_UpdateCommand"
                    OnItemCommand="DataList1_ItemCommand" DataKeyField="F_ID">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <AlternatingItemStyle BackColor="#D1DDF1" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000066" />
                    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>
                        <table style="margin: 10px 5px 10px 5px;">
                            <tr>
                                <td colspan="2" style="font-weight: bold;">
                                    <span style="color: #eb1291;">
                                        <%#Eval("F_ISHIDENITEM").ToString() == "1" ? "[隐藏] " : ""%></span>商品:<%#GetTextName(Eval("F_EXCELID").ToString())%>(<%#Eval("F_EXCELID")%>)
                                    <span style="color: #eb1291;">
                                        <%#Eval("F_ISNEW").ToString()=="1"?"新":""%>
                                        <%#Eval("F_ISHOTSALE").ToString() == "1" ? "热" : ""%>
                                        <%#Eval("F_ISPROMOTIONS").ToString() == "1" ? "促" : ""%>
                                        <%#Eval("F_ISTIMESALE").ToString() == "1" ? "限" : ""%>
                                        <%#Eval("F_USEGIFTS").ToString() == "1" ? "赠" : ""%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--  <div style="width: 100px; height: 80px; background-image: url(../img/Login.gif);">
                                        10</div> <img src="../img/Login.gif" width="100px" style="margin-right:-6px;" alt="主品" />10--%>
                                    <div style="position: relative; width: 100px;">
                                        <div style="position: absolute; right: 1; bottom: 1; color: white; font-weight: bold;">
                                            数量:<%#Eval("F_ITEMNUMBER")%>
                                        </div>
                                        <img src="../img/Login.gif" width="100px"> </img>
                                    </div>
                                </td>
                                <td align="left" width="200px" valign="top">
                                    描述:<%#TransDe(Eval("F_ItemInfo").ToString())%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%# Convert.ToInt32(Eval("F_GiftNUM_0")) > 0 ? "<img src=\"../img/Login.gif\" width=\"20px\" height=\"20\" alt=\"赠品1编号:" + Eval("F_GiftID_0") + " 数量:" + Eval("F_GiftNUM_0") + "\"  />" : ""%>
                                    <%# Convert.ToInt32(Eval("F_GiftNUM_1")) > 0 ? "<img src=\"../img/Login.gif\" width=\"20px\" height=\"20\" alt=\"赠品2编号:" + Eval("F_GiftID_1") + " 数量:" + Eval("F_GiftNUM_1") + "\"  />" : ""%>
                                    <%# Convert.ToInt32(Eval("F_GiftNUM_2")) > 0 ? "<img src=\"../img/Login.gif\" width=\"20px\" height=\"20\" alt=\"赠品3编号:" + Eval("F_GiftID_2") + " 数量:" + Eval("F_GiftNUM_2") + "\"  />" : ""%>
                                    <%# Convert.ToInt32(Eval("F_GiftNUM_3")) > 0 ? "<img src=\"../img/Login.gif\" width=\"20px\" height=\"20\" alt=\"赠品4编号:" + Eval("F_GiftID_3") + " 数量:" + Eval("F_GiftNUM_3") + "\"  />" : ""%>
                                    <%# Convert.ToInt32(Eval("F_GiftNUM_4")) > 0 ? "<img src=\"../img/Login.gif\" width=\"20px\" height=\"20\" alt=\"赠品5编号:" + Eval("F_GiftID_4") + " 数量:" + Eval("F_GiftNUM_4") + "\"  />" : ""%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%#Eval("F_ISTIMESALE").ToString() == "1" ? "<span style=\"color: #eb1291; font-size: 12px;\">限时:"+Eval("F_TimeStart")+"至"+Eval("F_TimeEnd")+"</span>" : ""%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-weight: bold;">
                                    <div style="float: left; width: 220px">
                                        原价: <span style="color: red; text-decoration: line-through;">
                                            <%#Eval("F_OLDPRICE")%></span> 现价: <span style="color: red;">
                                                <%#Eval("F_NEWPRICE")%></span> VIP价: <span style="color: red;">
                                                    <%#Eval("F_VIPRICE")%></span>
                                        <%# Convert.ToInt32(Eval("F_SHOPTYPE")) == 0 ? "<img src=\"../img/CoinJinBi.gif\" width=\"15px\" height=\"15px\" alt=\"元宝\"  />" : "<img src=\"../img/CoinJinPiao.gif\" width=\"15px\" height=\"15px\" alt=\"绑定元宝\"  />"%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-weight: bold;">
                                    <div style="float: left; width: 180px; margin-top: 3px;">
                                        <asp:Label runat="server" Text="<%$Resources:Language,LblOperate %>"></asp:Label>:
                                        <asp:LinkButton ID="btnUp" CommandName="Up" runat="server" Font-Overline="False"
                                            Font-Underline="True">上移</asp:LinkButton>
                                        <asp:LinkButton ID="btnDown" CommandName="Down" runat="server" Font-Underline="True">下移</asp:LinkButton>
                                        本页顺序:<%#Convert.ToInt32( ViewState["itemType"]) == -1 ? Eval("F_PROMOTIONPOS") : ""%><%#Convert.ToInt32(ViewState["itemType"]) == -2 ? Eval("F_HOTPOS") : ""%><%#Convert.ToInt32(ViewState["itemType"]) != -1 && Convert.ToInt32(ViewState["itemType"]) != -2 ? Eval("F_POSITION") : ""%>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Button ID="btnPEdit" runat="server" CommandName="PEdit" Text="编辑" CssClass="buttonblue" />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("确认要删除吗？");'
                                            Text="删除" CssClass="buttonblue" />
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="设置" CssClass="buttonblue"
                                            Visible="False" />
                                        <asp:Button ID="btnHidden" runat="server" CommandName="Edit" Text="隐藏" CssClass="buttonblue"
                                            Visible="False" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
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
            <br />
            <b><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label>:</b>1. 隐:隐藏 新:新品 热:热卖 促:促销 限:限时特卖 赠:含赠品 &nbsp;2. 商品名称为参考,游戏中直接按EXCEL编号读取商品名称
            <br />
            <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
        </div>
    </div>
    </form>
</body>
</html>
