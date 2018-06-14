<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CDKeyCreate.aspx.cs" Inherits="WebWSS.CDKey.CDKeyCreate" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>
    <style type="text/css">
        .switchLine {
            display:block;
            margin: 20px 0 0 20px;
        }
        .switchLinespan {
            margin-right: 100px;
        }
    </style>
    <script  type="text/javascript">
        var start= '<%= GetNowTimeFormat("yyyy-MM-dd HH:mm:ss")%>';
        function getNow() {
            var dt = new WdatePicker({
                skin: 'default',
                startDate:start,
                isShowClear: false,
                readOnly: false
            });
            return dr;
        }
        window.onload = function () {
            var disabledCreate = '<%= DisableCDKeyCreate()%>'.toLocaleLowerCase() == 'true';
            if (!disabledCreate) {
                document.getElementById('ButtonCreate').style.display = 'none';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" visible="true">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchList.aspx"  Text="<%$Resources:Language,CDK_BatchManage %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyList.aspx"  Text="<%$Resources:Language,CDK_CDKList %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyCreate.aspx" Enabled="False"  Text="<%$Resources:Language,CDK_CDKeyCreate %>"></asp:LinkButton>
                            | &nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyImport.aspx" Text="<%$Resources:Language,CDK_CDKImport %>"></asp:LinkButton>
        </div>
        <div class="search">
        <br />
            <div class="switchLine">
                <span class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_MsgGenerateCardType %>"></asp:Label>:
                </span>
                <span style=" margin-left: -45px;">
                     <asp:DropDownList ID="ddlKeyType" runat="server"   
                    onselectedindexchanged="ddlKeyType_SelectedIndexChanged" 
                    AutoPostBack="True">
                   <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                </span>
            </div>
            <div class="switchLine">
                <span class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblZone %>"></asp:Label>:
                </span>
                <span style="margin-left: 26px;">
                    <asp:DropDownList runat="server" ID="sltZone">
                          <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
           <div class="switchLine">
               <span class="switchLinespan"> <asp:Label runat="server" Text="<%$Resources:Language,CDK_AwardNo %>"></asp:Label>:</span>
                <asp:TextBox ID="tboxExcelID" runat="server" Width="60px" 
                    MaxLength="10" Enabled="true"></asp:TextBox>
           </div>
           <div class="switchLine">
              <span class="switchLinespan"> <asp:Label runat="server" Text="<%$Resources:Language,CDK_AwardNumber %>"></asp:Label>:</span>
               <asp:TextBox ID="tboxItemNum" runat="server" Width="60px" MaxLength="10" Enabled="False">1</asp:TextBox>
           </div>
            <div class="switchLine">
                <span class="switchLinespan"><asp:Label runat="server" Text="<%$Resources:Language,CDK_GenerateNumber %>"></asp:Label>:</span>
                <asp:TextBox ID="tboxKeyCount" runat="server" Width="60px" MaxLength="10"></asp:TextBox>
            </div>
            <div class="switchLine">
               <span class="switchLinespan"> <asp:Label runat="server" Text="<%$Resources:Language,CDK_Remark %>"></asp:Label>:</span>
                <span style="margin-left: 25px;">
                    <asp:TextBox ID="tboxNote" runat="server" Width="530px" MaxLength="100"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblStartTime %>"></asp:Label>:
                </span>
                 <span>
                     <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'>
                            <img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'>
                        </a>
                        <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"  onFocus="getNow()">
                        </asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblLimitRoleLevel %>"></asp:Label>
                </span>
                <span style="margin-left: -20px;">
                    <asp:TextBox runat="server" ID="txtRoleLevelLimit" Text="1"></asp:TextBox>
                </span>
            </div>
             <div class="switchLine">
                <span class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblEndTime %>"></asp:Label>:
                </span>
                <span>
                    <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("txtEndTime"), "yyyy-mm-dd", null, null, null, "11");'>
                        <img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'>
                    </a>
                    <asp:TextBox ID="txtEndTime" runat="server" Width="80px" MaxLength="10"  onFocus="getNow()">
                    </asp:TextBox>
                </span>
                </div>
            <div class="switchLine">
                <span  class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblEmailTitle %>"></asp:Label>
                </span>
                <span>
                    <asp:TextBox ID="mailTitle" Text="<%$Resources:Language,Txt_DefaultEmailTitle %>" runat="server" />
                </span>
            </div>
            <div class="switchLine">
                <span  class="switchLinespan">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailSendName %>"></asp:Label>
                </span>
                <span style="margin-left: 25px;">
                    <asp:TextBox ID="txtMailSendName" Text="<%$Resources:Language,LblMailSendName %>" runat="server" />
                </span>
            </div>
             <div class="switchLine">
                <span  class="switchLinespan" style="position: relative; top: -85px;">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailContent %>"></asp:Label>
                </span>
                <span>
                    <asp:TextBox ID="txtMailContent" Text="<%$Resources:Language,Txt_DefaultEmailContent %>"  TextMode="MultiLine" Height="150" Width="600" runat="server" MaxLength="200"  />
                </span>
            </div>
            <div class="switchLine">
                <asp:Button ID="ButtonCreate" runat="server" Text="<%$Resources:Language,CDK_Generate %>" CssClass="button" OnClick="ButtonSearch_Click" />
            </div>
        </div>
        <div class="titletip">
        </div>
        <div class="gridv">
             <br /> <asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label>
            <br />
            <br />
        </div>
    </div>
    </form>
   <div style="display:none;">
        <asp:Label runat="server" Text="<%$Resources:Language,Msg_OuterNetHide %>" > </asp:Label>
   </div>
</body>
</html>
