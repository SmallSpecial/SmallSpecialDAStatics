<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GMJurisdictionSet.aspx.cs" Inherits="WebWSS.ModelPage.GMJurisdiction.GMJurisdictionSet" %>

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
</head>
<body>
    <form id="form1" runat="server" visible="true">
    <div class="main">
        <div class="itemtitle">
            <asp:Label runat="server" Text="<%$Resources:Language,LblGM %>"></asp:Label>
        </div>
        <div class="search">
            <div class="switchLine">
                <div class="switchLine">
                    <span style="margin-left: 25px;">
                        <asp:Label runat="server" ID="lblGMRoleID" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="tbGMContent"></asp:TextBox>
                    </span>
                    <asp:Button ID="btnGM" runat="server" CssClass="button"  Text="<%$Resources:Language,LblConfirm%>" OnClick="btnGM_Click"/>
                </div>
            </div>
        </div>

        <div class="itemtitle" style="margin-top:20px;">
            <asp:Label runat="server" Text="<%$Resources:Language,LblWhiteList %>"></asp:Label>
        </div>
        <div class="search">
            <div class="switchLine">
                <div class="switchLine">
                    <span style="margin-left: 25px;">
                        <asp:Label runat="server" ID="lblWhiteList" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="tbWhiteListContent"></asp:TextBox>
                    </span>
                    <asp:Button ID="btnWhiteList" runat="server" CssClass="button"  Text="<%$Resources:Language,LblConfirm%>" OnClick="btnWhiteList_Click"/>
                </div>
            </div>
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
