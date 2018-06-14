<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GssGameNotice.aspx.cs" Inherits="WebWSS.ModelPage.GssTool.GssGameNotice" %>

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
            <asp:Label ID="lblTitle" runat="server" Text="<%$Resources:Language,BtnCallWorkOrder %>"></asp:Label>
        </div>
        <div class="search">
            <div class="switchLine">
                <span>
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblNoticeTitle %>"></asp:Label>:
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
                    <asp:Label runat="server" Text="<%$Resources:Language,LblGssNoticeTip %>"></asp:Label>
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -45px;">
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMessageInfo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbContentInfo"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200" />
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
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,LblTimeInterval %>"></asp:Label>:
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblInterval %>"></asp:Label>
                    <asp:TextBox ID="tbDay" runat="server" Width="30" Text="0"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblDay %>"></asp:Label>
                    <asp:TextBox ID="tbHourse" runat="server" Width="30" Text="0"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblHourse %>"></asp:Label>
                    <asp:TextBox ID="tbMinutes" runat="server" Width="30" Text="0"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMinutes %>"></asp:Label>
                    <asp:TextBox ID="tbsecond" runat="server" Width="30" Text="30"></asp:TextBox>
                   <asp:Label runat="server" Text="<%$Resources:Language,LblSecond %>"></asp:Label>
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
                <span style="margin-left:20px;">
                    <asp:CheckBoxList ID="ckbBattleZone" runat="server" RepeatColumns="15" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </span>
            </div>
           <div class="switchLine">
               <span>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnCreateCode" runat="server" Text="<%$Resources:Language,LblCreateCode %>" Width="120" OnClick="btnCreateCode_Click"/>
               </span>
           </div>
            <div class="switchLine">
                <span style="position: relative; top: -45px;">
                    &nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,Lbl_NoticeInfo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbMailContent"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200" Enabled="false"/>
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -45px;">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_Remark %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="tbBak"  TextMode="MultiLine" Height="100" Width="600" runat="server" MaxLength="200" />
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
