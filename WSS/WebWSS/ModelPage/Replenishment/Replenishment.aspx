<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Replenishment.aspx.cs" Inherits="WebWSS.ModelPage.Replenishment.Replenishment" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='../../img/Admin.Js'></script>
    <script type="text/javascript" src='../../img/GetDate.Js'></script>
     <script src="../../Script/jquery-1.12.3.min.js"></script>
    <style type="text/css">
        .switchLine {
            display:block;
            margin: 16px 0 0 16px;
        }
    </style>
    <script  type="text/javascript">
        var start= '<%= GetNowTimeFormat("yyyy-MM-dd")%>';
        function getNow() {
            var dt = new WdatePicker({
                skin: 'default',
                startDate:start,
                isShowClear: false,
                readOnly: false
            });
            return dr;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" visible="true">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Replenishment/Replenishment.aspx" Text="<%$Resources:Language,LblReplenishment%>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Replenishment/ReplenishmentLog.aspx" Text="<%$Resources:Language,LblReplenishmentLog%>"></asp:LinkButton>
            | &nbsp;
        </div>
        <asp:HiddenField ID="zoneID" runat="server" />
        <div class="search">
            <div class="switchLine">
                <span style="margin-left:20px;">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblUserNo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbUserID"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbRoleID"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblOrderNumber %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbTransactionID"></asp:TextBox>
                </span>
                <asp:Button runat="server" ID="btnSerach" CssClass="button" Text="<%$Resources:Language,LblReplenishment%>" OnClick="btnSerach_Click"/>
            </div>
        </div>
        <div class="titletip">
        </div>
        <div class="gridv">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
