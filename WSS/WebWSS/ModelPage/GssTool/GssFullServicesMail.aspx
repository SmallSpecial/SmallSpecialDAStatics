<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GssFullServicesMail.aspx.cs" Inherits="WebWSS.ModelPage.GssTool.GssFullServicesMail" %>

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
        <div style="margin-top:20px;margin-left:16px;">
            <asp:Label ID="lblTitle" runat="server" Text="<%$Resources:Language,LblFullServicesMail %>"></asp:Label>
        </div>
        <div class="search">
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblActiveName %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbTitle" Width="120"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblSendPeople %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbSnedUser" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMinLevel %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbMinLevel" Width="120"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMaxLevel %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbMaxLevel" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblStartTime %>"></asp:Label>:
                </span>
                <span>
                    
                    <asp:TextBox runat="server" ID="tbSTime" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="124"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblEndTime %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbETime" CssClass="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="124"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
                </span>
                <span>
                    <asp:DropDownList ID="ddlBigZone" runat="server" Width="120" AutoPostBack="True">
                        <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
             <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblZone %>"></asp:Label>:
                </span>
                 <span>
                     <asp:Button runat="server" ID="btnSelectAll" Text="<%$Resources:Language,LblAll %>" CssClass="button"  OnClick="btnSelectAll_Click"/>
                 </span>
                 <span>
                     <asp:Button runat="server" ID="btnCancle" Text="<%$Resources:Language,LblCancle %>" CssClass="button" OnClick="btnCancle_Click" />
                 </span>
                 <span>
                     <asp:Label runat="server" Text="<%$Resources:Language,LblSelectOpenBattleZone %>" ForeColor="Red"></asp:Label>
                 </span>
                <span style="margin-left:20px;">
                    <asp:CheckBoxList ID="ckbBattleZone" runat="server" RepeatColumns="15" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBlueDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbBindGold" Width="120"></asp:TextBox>
                </span>
                <span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRedDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbGold" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp1 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemID1" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp2 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemID2" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp3 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemID3" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp4 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemID4" Width="120"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardProp5 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemID5" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum1 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemNum1" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum2 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemNum2" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum3 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemNum3" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum4 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemNum4" Width="120"></asp:TextBox>
                </span>
                 <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_AwardPropNum5 %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbItemNum5" Width="120"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -85px;">
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailContent %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbMailContent"  TextMode="MultiLine" Height="150" Width="600" runat="server" MaxLength="200" />
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -85px;">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_Remark %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbBak"  TextMode="MultiLine" Height="150" Width="600" runat="server" MaxLength="200" />
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Button ID="btnConfirm" runat="server" Text="<%$Resources:Language,LblConfirm %>" CssClass="button" OnClick="btnConfirm_Click" />
                </span>
                <span>
                    <asp:Button ID="btnClear" runat="server" Text="<%$Resources:Language,BtnClear %>" CssClass="button" OnClick="btnClear_Click" />
                </span>
                <asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label>
            </div>
        </div>
        <div class="gridv">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            
        </div>
    </div>
    </form>
</body>
</html>
