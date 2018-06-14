<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftConfig.aspx.cs" Inherits="WebWSS.ModelPage.GiftConfig.GiftConfig" %>

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
            <asp:Label ID="lblTitle" runat="server" Text="礼包配置"></asp:Label>
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
                    <asp:CheckBoxList ID="ckbBattleZone" runat="server" RepeatColumns="15" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="ckbBattleZone_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </span>
            </div>

            <div class="switchLine">
                <span>
                    &nbsp;
                    <asp:Label runat="server" Text="礼包名称"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbPackageName" runat="server"></asp:TextBox>
                </span>

                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="是否推荐"></asp:Label>:
                </span>
                <span>
                    <asp:CheckBox runat="server" ID="ckbItemFlag"/>
                </span>
            </div>

            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="ProductID"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlProductID" runat="server" Width="170" AutoPostBack="true" OnSelectedIndexChanged="ddlProductID_SelectedIndexChanged">
                    </asp:DropDownList>
                </span>

                <span>
                    <asp:Label runat="server" Text="原价（韩元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="OldKRWMoney" runat="server"></asp:TextBox>
                </span>

                <span>
                    <asp:Label runat="server" Text="原价（美元）"></asp:Label>:
                </span>
                <span>
                   <asp:TextBox ID="OldUSDMoney" runat="server"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="现价（韩元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="CurKRWMoney" runat="server"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="现价（美元）"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="CurUSDMoney" runat="server"></asp:TextBox>
                </span>
            </div>

            <div class="switchLine">
                <span>&nbsp;
                    <asp:Label runat="server" Text="分页类型"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlItemType" runat="server" Width="170">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1" Text="装备"></asp:ListItem>
                        <asp:ListItem Value="2" Text="材料"></asp:ListItem>
                        <asp:ListItem Value="3" Text="宠物"></asp:ListItem>
                        <asp:ListItem Value="4" Text="坐骑"></asp:ListItem>
                        <asp:ListItem Value="5" Text="特殊"></asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="显示顺序"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Pos"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="礼包类型"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlPackageMoneyType" runat="server" Width="170" AutoPostBack="True">
                        <asp:ListItem Value="0">人民币礼包</asp:ListItem>
                        <asp:ListItem Value="1">游戏币礼包</asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="限制类型"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlLimitStell" runat="server" Width="170" AutoPostBack="True">
                         <asp:ListItem Value="1">日</asp:ListItem>
                        <asp:ListItem Value="2">周</asp:ListItem>
                        <asp:ListItem Value="3">月</asp:ListItem>
                        <asp:ListItem Value="4">永久</asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="限购个数"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="LimitNum"></asp:TextBox>
                </span>
                <span style="color:red;">-1为不限制购买个数</span>
            </div>
            <div class="switchLine">
                <span>&nbsp;&nbsp;
                    <asp:Label runat="server" Text="是否限时"></asp:Label>:
                </span>
                <span>
                    <asp:CheckBox runat="server" ID="ckbLimitTime" />
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="开始时间"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="TimeStart" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="170"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="结束时间"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="TimeEnd" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="170"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="礼包图片"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlPicID" runat="server" Width="170" AutoPostBack="True">
                        <asp:ListItem Value="9931" Text="9931"></asp:ListItem>
                        <asp:ListItem Value="9932" Text="9932"></asp:ListItem>
                        <asp:ListItem Value="9933" Text="9933"></asp:ListItem>
                        <asp:ListItem Value="9934" Text="9934"></asp:ListItem>
                        <asp:ListItem Value="9935" Text="9935"></asp:ListItem>
                        <asp:ListItem Value="9936" Text="9936"></asp:ListItem>
                        <asp:ListItem Value="9937" Text="9937"></asp:ListItem>
                        <asp:ListItem Value="9938" Text="9938"></asp:ListItem>
                        <asp:ListItem Value="9939" Text="9939"></asp:ListItem>
                        <asp:ListItem Value="9940" Text="9940"></asp:ListItem>
                        <asp:ListItem Value="9901" Text="9901"></asp:ListItem>
                        <asp:ListItem Value="9902" Text="9902"></asp:ListItem>
                        <asp:ListItem Value="9903" Text="9903"></asp:ListItem>
                        <asp:ListItem Value="9904" Text="9904"></asp:ListItem>
                        <asp:ListItem Value="9905" Text="9905"></asp:ListItem>
                        <asp:ListItem Value="9906" Text="9906"></asp:ListItem>
                        <asp:ListItem Value="9911" Text="9911"></asp:ListItem>
                        <asp:ListItem Value="9912" Text="9912"></asp:ListItem>
                        <asp:ListItem Value="9913" Text="9913"></asp:ListItem>
                        <asp:ListItem Value="9914" Text="9914"></asp:ListItem>
                        <asp:ListItem Value="9915" Text="9915"></asp:ListItem>
                        <asp:ListItem Value="9916" Text="9916"></asp:ListItem>
                        <asp:ListItem Value="9917" Text="9917"></asp:ListItem>
                        <asp:ListItem Value="9918" Text="9918"></asp:ListItem>
                        <asp:ListItem Value="9951" Text="9951"></asp:ListItem>
                        <asp:ListItem Value="9952" Text="9952"></asp:ListItem>
                        <asp:ListItem Value="9971" Text="9971"></asp:ListItem>
                        <asp:ListItem Value="9972" Text="9972"></asp:ListItem>
                        <asp:ListItem Value="9985" Text="9985"></asp:ListItem>
                        <asp:ListItem Value="9986" Text="9986"></asp:ListItem>
                        <asp:ListItem Value="9991" Text="9991"></asp:ListItem>
                        <asp:ListItem Value="9992" Text="9992"></asp:ListItem>
                        <asp:ListItem Value="9965" Text="9965"></asp:ListItem>
                        <asp:ListItem Value="9953" Text="9953"></asp:ListItem>
                        <asp:ListItem Value="9955" Text="9955"></asp:ListItem>
                        <asp:ListItem Value="9956" Text="9956"></asp:ListItem>
                        <asp:ListItem Value="9957" Text="9957"></asp:ListItem>
                        <asp:ListItem Value="9963" Text="9963"></asp:ListItem>
                        <asp:ListItem Value="9959" Text="9959"></asp:ListItem>
                        <asp:ListItem Value="9967" Text="9967"></asp:ListItem>
                        <asp:ListItem Value="9969" Text="9969"></asp:ListItem>
                        <asp:ListItem Value="9961" Text="9961"></asp:ListItem>
                        <asp:ListItem Value="9962" Text="9962"></asp:ListItem>
                        <asp:ListItem Value="9973" Text="9973"></asp:ListItem>
                        <asp:ListItem Value="15003" Text="15003"></asp:ListItem>
                        <asp:ListItem Value="15001" Text="15001"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp1 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftID_0" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp2 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftID_1" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp3 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftID_2" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp4 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftID_3" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp5 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftID_4" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum1 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftNUM_0" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum2 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftNUM_1" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum3 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftNUM_2" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum4 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftNUM_3" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum5 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_GiftNUM_4" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailContent %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbMailContent"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200" Text="구매 성공 하셨습니다.패키지를 수령하세요." />
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -85px;">
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="礼包描述"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="ItemInfo"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200"/>
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
