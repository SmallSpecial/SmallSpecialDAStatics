<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositConfig.aspx.cs" Inherits="WebWSS.ModelPage.GiftConfig.DepositConfig" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='../../img/Admin.Js'></script>
    <script type="text/javascript" src='../../img/GetDate.Js'></script>
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <script src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .switchLine {
            display: block;
            margin: 16px 0 0 16px;
        }
        .titleSpan{
            width:120px;
            text-align:right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" visible="true">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/GiftConfig.aspx" Text="礼包配置"></asp:LinkButton>
            | &nbsp;
             <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/GiftList.aspx" Text="礼包信息列表"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/GiftConfigLog.aspx" Text="礼包配置操作日志"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/DepositConfig.aspx" Text="DepositConfig"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/DepositList.aspx" Text="DepositList"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton5" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/GiftConfig/DepositConfigLog.aspx" Text="DepositConfigLog"></asp:LinkButton>
            | &nbsp;
        </div>
        <div style="margin-top: 20px; margin-left: 16px;">
            <asp:Label ID="lblTitle" runat="server" Text="DepositConfig"></asp:Label>
        </div>
        <div class="search">
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblZone %>"></asp:Label>:
                </span>
                 <span style="margin-left:80px;">
                     <asp:Label runat="server" Text="<%$Resources:Language,LblSelectOpenBattleZone %>" ForeColor="Red"></asp:Label>
                 </span>
                <span style="margin-left:20px;">
                    <asp:CheckBoxList ID="ckbBattleZone" runat="server" RepeatColumns="15" RepeatDirection="Horizontal" >
                    </asp:CheckBoxList>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="产品ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="ProductID"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;
                    <asp:Label runat="server" Text="产品类型"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="F_Type" runat="server" Width="170" AutoPostBack="true" OnSelectedIndexChanged="F_Type_SelectedIndexChanged">
                        <asp:ListItem Value="0">钻石</asp:ListItem>
                        <asp:ListItem Value="1">月卡周卡</asp:ListItem>
                        <asp:ListItem Value="2">直购物品</asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="产品子类型"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="F_SubType" runat="server" Width="170">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="原价（韩元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="F_OldKRWCostMoney" runat="server"></asp:TextBox>
                </span>

                <span>
                    <asp:Label runat="server" Text="原价（美元）"></asp:Label>:
                </span>
                <span>
                   <asp:TextBox ID="F_OldUSDCostMoney" runat="server"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="现价（韩元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="F_CurKRWCostMoney" runat="server"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="现价（美元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="F_CurUSDCostMoney" runat="server"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine" runat="server" id="GetMoney">
                <span>
                    <asp:Label runat="server" Text="钻石数量"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GetMoney"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" ID="Para1" Text="首次购买获得钻石数量"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Para1"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" ID="Para2" Text="非首次购买获得钻石数量"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Para2"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" ID="Para3" Text="是否热卖"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Para3"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="充值点数"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Exp"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="生效时间"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_BeginGiveBindGoldTime" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="170"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="结束时间"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_EndGiveBindGoldTime" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="170"></asp:TextBox>
                </span>
            </div>
             <div class="switchLine">
                 <span>
                    <asp:Label runat="server" Text="首充给绑定钻石"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_FirstGiveBindGold"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="给绑定钻石"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiveBindGold"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_StartNoticeTitle %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbTitle" Width="120" Text="패키지 구매 성공"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblSendPeople %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbSnedUser" Width="120" Text="시스템"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -85px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailContent %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbMailContent"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200" Text="구매 성공 하셨습니다.패키지를 수령하세요." />
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Button ID="btnConfirm" runat="server" Text="<%$Resources:Language,LblConfirm %>" CssClass="button" OnClick="btnConfirm_Click" />
                </span>
                <span>
                    <asp:Button ID="btnClear" runat="server" Text="<%$Resources:Language,BtnClear %>" CssClass="button" OnClick="btnClear_Click" />
                </span>
            </div>
        </div>
        <div class="gridv">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
