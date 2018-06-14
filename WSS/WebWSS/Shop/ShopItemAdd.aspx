<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopItemAdd.aspx.cs" Inherits="WebWSS.Shop.ShopItemAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

    <script type="text/javascript">
        function OpenDialog(type) {
            var returnValue = window.showModalDialog("ShopBaseItemQuery.aspx?bigzone=<%=DropDownListArea1.SelectedValue%>&zone=<%=DropDownListArea2.SelectedValue%>", window, "dialogWidth=600px;dialogHeight=600px;status=no;help=no;scrollbars=no");
            if (returnValue) {
                if (type == 0) {
                    document.form1.tboxValue.value = returnValue;
                }
                else if (type == 1) {
                    document.form1.tboxGift1ID.value = returnValue;
                }
                else if (type == 2) {
                    document.form1.tboxGift2ID.value = returnValue;
                }
                else if (type == 3) {
                    document.form1.tboxGift3ID.value = returnValue;
                }
                else if (type == 4) {
                    document.form1.tboxGift4ID.value = returnValue;
                }
                else if (type == 5) {
                    document.form1.tboxGift5ID.value = returnValue;
                }

                __doPostBack("", "");
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
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
        </div>
        <div class="search" style="margin: 4px 0px 6px 0px">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged" Enabled="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" Enabled="False">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblEncoding" runat="server" Text="<%$Resources:Language,LblCodeOpen %>" Visible="True"></asp:Label>
            <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="true"></asp:Label>
        </div>
        <div style="border: 1px solid #0099CC; background-color: white; width: 99%;">
            <table style="margin: 30px 5px 40px 60px;">
                <tr height="36px">
                    <td style="font-weight: bold;">
                        EXCEL编号:<asp:TextBox ID="tboxExcelID" runat="server" Width="85px" MaxLength="10"
                            Enabled="False"></asp:TextBox>
                        &nbsp;商品名称:<asp:TextBox ID="tboxItemName" runat="server" Width="230px" MaxLength="10"
                            Enabled="False"></asp:TextBox>
                        <input id="Button1" type="button" value="选择" class="buttonblue" onclick="OpenDialog(0)" />
                        <asp:Label ID="Label20" runat="server" Text="*" ForeColor="#FF0066"></asp:Label>
                        <span style="display: none;">
                            <asp:TextBox ID="tboxValue" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                            <asp:TextBox ID="tboxGift1ID" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                            <asp:TextBox ID="tboxGift2ID" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                            <asp:TextBox ID="tboxGift3ID" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                            <asp:TextBox ID="tboxGift4ID" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                            <asp:TextBox ID="tboxGift5ID" runat="server" Width="89px" MaxLength="200"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr height="36px">
                    <td>
                        <span style="color: #eb1291;">
                            <asp:CheckBox ID="cbxIsNew" runat="server" Text="是否新品:" TextAlign="Left" AutoPostBack="True"
                                OnCheckedChanged="cbxIsNew_CheckedChanged" />&nbsp;新品顺序:<asp:TextBox ID="tboxNewPos"
                                    runat="server" Width="33px" MaxLength="3" Enabled="False">0</asp:TextBox>
                            &nbsp;<asp:CheckBox ID="cbxIsHot" runat="server" Text="是否热卖:" TextAlign="Left" AutoPostBack="True"
                                OnCheckedChanged="cbxIsHot_CheckedChanged" />&nbsp;热卖顺序:<asp:TextBox ID="tboxHotPos"
                                    runat="server" Width="33px" MaxLength="3" Enabled="False">0</asp:TextBox>
                            &nbsp;<asp:CheckBox ID="cbxIsOffSale" runat="server" Text="是否促销:" TextAlign="Left"
                                AutoPostBack="True" OnCheckedChanged="cbxIsOffSale_CheckedChanged" />&nbsp;促销顺序:<asp:TextBox
                                    ID="tboxOffSalePos" runat="server" Width="33px" MaxLength="3" Enabled="False">0</asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr height="36px">
                    <td>
                        <asp:CheckBox ID="cbxIsLimitTimeS" runat="server" Text="限时特卖" TextAlign="Left" Font-Bold="True"
                            ForeColor="#EB1291" AutoPostBack="True" OnCheckedChanged="cbxIsLimitTimeS_CheckedChanged" />
                        <span style="color: #eb1291;">
                            <asp:TextBox ID="tboxLimitTimePos" runat="server" Width="33px" MaxLength="3" Enabled="False">0</asp:TextBox>
                            &nbsp; </span>限时时段<asp:TextBox ID="tboxLTBegin" runat="server" Width="150px" Enabled="False">2013-01-01 00:00</asp:TextBox>-<asp:TextBox
                                ID="tboxLTEnd" runat="server" Width="169px" Enabled="False">2013-01-01 00:00</asp:TextBox><br />
                    </td>
                </tr>
                <tr height="36px" style="font-weight: bold;">
                    <td>
                        商品数量:<asp:TextBox ID="tboxNum" runat="server" Width="51px" MaxLength="3" AutoPostBack="True"
                            OnTextChanged="tboxNum_TextChanged">1</asp:TextBox>
                        &nbsp;商品顺序:<span style="color: #eb1291;"><asp:TextBox ID="tboxPos" runat="server"
                            Width="33px" MaxLength="3">0</asp:TextBox>
                        </span>&nbsp;商品类型:<asp:DropDownList ID="ddlItemType" runat="server" Width="230px"
                            Enabled="False">
                            <asp:ListItem Value="0">宝石</asp:ListItem>
                            <asp:ListItem Value="1">法宝</asp:ListItem>
                            <asp:ListItem Value="2">坐骑</asp:ListItem>
                            <asp:ListItem Value="3">时装</asp:ListItem>
                            <asp:ListItem Value="4">奇珍</asp:ListItem>
                            <asp:ListItem Value="5">药水</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr height="30px">
                    <td>
                        <asp:CheckBox ID="cbxGift" runat="server" Text="是否包含赠品" TextAlign="Left" Font-Bold="True"
                            ForeColor="#EB1291" AutoPostBack="True" OnCheckedChanged="cbxGift_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Panel ID="pnlGift" runat="server" Visible="False">
                            <asp:Label ID="Label4" runat="server" Text="赠品1"></asp:Label>
                            (<asp:Label ID="lblGift1" runat="server" Text="-1"></asp:Label>) &nbsp;数量: <span
                                style="color: #eb1291;">
                                <asp:TextBox ID="tboxGift1Num" runat="server" Width="33px" MaxLength="3">0</asp:TextBox>
                            </span>
                            <input id="Button18" class="buttonblue" onclick="OpenDialog(1)" type="button" value="选择" />
                            <asp:Label ID="Label5" runat="server" Text="赠品2"></asp:Label>
                            (<asp:Label ID="lblGift2" runat="server" Text="-1"></asp:Label>
                            ) &nbsp;数量: <span style="color: #eb1291;">
                                <asp:TextBox ID="tboxGift2Num" runat="server" MaxLength="3" Width="33px">0</asp:TextBox>
                            </span>
                            <input id="Button19" class="buttonblue" onclick="OpenDialog(2)" type="button" value="选择" />
                            <asp:Label ID="Label15" runat="server" Text="赠品3"></asp:Label>
                            (<asp:Label ID="lblGift3" runat="server" Text="-1"></asp:Label>
                            ) &nbsp;数量: <span style="color: #eb1291;">
                                <asp:TextBox ID="tboxGift3Num" runat="server" Width="33px" MaxLength="3">0</asp:TextBox>
                            </span>
                            <input id="Button20" class="buttonblue" onclick="OpenDialog(3)" type="button" value="选择" /><div
                                style="margin: 3px 0px 0px 0px">
                                <asp:Label ID="Label17" runat="server" Text="赠品4"></asp:Label>
                                (<asp:Label ID="lblGift4" runat="server" Text="-1"></asp:Label>) &nbsp;数量: <span
                                    style="color: #eb1291;">
                                    <asp:TextBox ID="tboxGift4Num" runat="server" Width="33px" MaxLength="3">0</asp:TextBox>
                                </span>
                                <input id="Button21" class="buttonblue" onclick="OpenDialog(4)" type="button" value="选择" />
                                <asp:Label ID="Label19" runat="server" Text="赠品5"></asp:Label>
                                (<asp:Label ID="lblGift5" runat="server" Text="-1"></asp:Label>
                                ) &nbsp;数量: <span style="color: #eb1291;">
                                    <asp:TextBox ID="tboxGift5Num" runat="server" MaxLength="3" Width="33px">0</asp:TextBox>
                                </span>
                                <input id="Button22" class="buttonblue" onclick="OpenDialog(5)" type="button" value="选择" /></div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr height="36px">
                    <td style="font-weight: bold;">
                        参考价:<asp:Label ID="lblTempPrice" runat="server" Text="---" ForeColor="Red"></asp:Label>
                        &nbsp; 原价:
                        <asp:TextBox ID="tboxOldPrice" runat="server" Width="55px" Style="color: red;"></asp:TextBox>
                        &nbsp;现价:
                        <asp:TextBox ID="tboxNowPrice" runat="server" Width="55px" Style="color: red;"></asp:TextBox>
                        &nbsp;VIP价:
                        <asp:TextBox ID="tboxVIPPrice" runat="server" Width="55px" Style="color: red;"></asp:TextBox>
                        <asp:DropDownList ID="ddlShopType" runat="server" Width="86px" Enabled="False">
                            <asp:ListItem Value="0">元宝</asp:ListItem>
                            <asp:ListItem Value="1">绑定元宝</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr height="36px" style="font-weight: bold;">
                    <td>
                        描述:<asp:TextBox ID="tboxNote" runat="server" Height="73px" TextMode="MultiLine" Width="475px"
                            Enabled="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">
                        <div style="float: right; margin-right: 2px; margin-top: 16px;">
                            <span runat="Server" id="spanTips" style="font-weight: bold; color: #0c9bba; margin-right: 10px;
                                width: 240px">
                                <img src="../img/tipnorm.png" width="20px" style="margin-bottom: -5px;" />
                                <asp:Label ID="lblTips" runat="server" Text="点击<选择>可以进行商品选择"></asp:Label></span>
                            <asp:CheckBox ID="cbxIsHidden" runat="server" Text="隐藏" TextAlign="Left" />
                            &nbsp;
                            <asp:Button ID="btnCommit" runat="server" Text="保 存" CssClass="button" OnClick="btnCommit_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="返 回" CssClass="button" OnClick="btnCancel_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
