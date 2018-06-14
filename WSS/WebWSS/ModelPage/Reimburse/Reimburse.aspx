<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reimburse.aspx.cs" Inherits="WebWSS.ModelPage.Reimburse.Reimburse" %>

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
        window.onload = function () {
                document.getElementById('divSend').style.display = 'none';
        }
        function isSend() {
            if ($('#ckbSendEmail').is(':checked')) {
                document.getElementById('divSend').style.display = '';
            }
            else {
                document.getElementById('divSend').style.display = 'none';
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" visible="true">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Reimburse/Reimburse.aspx" Text="<%$Resources:Language,LblReimburse%>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/ModelPage/Reimburse/ReimburseLog.aspx" Text="<%$Resources:Language,LblOper%>"></asp:LinkButton>
            | &nbsp;
        </div>
        <asp:HiddenField ID="zoneID" runat="server" />
        <div class="search">
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Options"></asp:Label>:
                </span>
                <span style="margin-left: 25px;">
                    <asp:RadioButton runat="server" ID="rbtRoleID" Text="<%$Resources:Language,LblRoleNo %>" GroupName="rbtTeam" />
                    <asp:RadioButton runat="server" ID="rbtRoleName" Text="<%$Resources:Language,LblRoleName %>" GroupName="rbtTeam" Checked="true" />
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbContent"></asp:TextBox>
                </span>
                <asp:Button runat="server" ID="btnSerach" CssClass="button" Text="<%$Resources:Language,BtnQuery %>" OnClick="btnSerach_Click"/>
                 <asp:Button runat="server" ID="btnReset" CssClass="button" Text="<%$Resources:Language,BtnReset %>" OnClick="btnReset_Click"/>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRedDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Gold" Enabled="false"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBlueDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_BindGold" Enabled="false"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMoney %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_Money" Enabled="false"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBindMoney %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="F_BindMoney" Enabled="false"></asp:TextBox>
                </span>
            </div>
           <div class="switchLine">
               <span> <asp:Label runat="server" Text="<%$Resources:Language,LblReimburse%>"></asp:Label></span>
           </div>
            <div class="switchLine">
               <span> <asp:Label runat="server" Text="<%$Resources:Language,LblOrderNumber%>"></asp:Label>:</span>
               <span>
                   <asp:TextBox runat="server" ID="TransactionID"></asp:TextBox>
               </span>
           </div>
           <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRedDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Gold"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBlueDiamond %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="BindGold"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMoney %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Money"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblBindMoney %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="BindMoney"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Item1ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item1"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="Item1Num"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item1Num"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Item2ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item2"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="Item2Num"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item2Num"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Item3ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item3"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="Item3Num"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item3Num"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Item4ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item4"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="Item4Num"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item4Num"></asp:TextBox>
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="Item5ID"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item5"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="Item5Num"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="Item5Num"></asp:TextBox>
                </span>
            </div>

            <div class="switchLine">
                <asp:CheckBox runat="server" ID="ckbSendEmail" OnClick="isSend();" Text="<%$Resources:Language,LblSendMail %>" />
            </div>
            <div id="divSend">
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblEmailTitle %>"></asp:Label>：
                </span>
                <span>
                    <asp:TextBox ID="mailTitle" runat="server" />
                </span>
            </div>
            <div class="switchLine">
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblSendPeople %>"></asp:Label>：
                </span>
                <span>
                    <asp:TextBox ID="txtMailSendName" runat="server" />
                </span>
            </div>
            <div class="switchLine">
                <span style="position: relative; top: -85px;">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblMailContent %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox ID="txtMailContent"  TextMode="MultiLine" Height="150" Width="600" runat="server" MaxLength="200" />
                </span>
            </div>
           </div>
            <div class="switchLine">
                <asp:Button ID="btnConfirm" runat="server" CssClass="button"  Text="<%$Resources:Language,LblConfirm%>" OnClick="btnConfirm_Click"/>
            </div>
        </div>
        <div class="titletip">
        </div>
        <div class="gridv">
            <br />
            <asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label>
            <br />
            <br />
        </div>
    </div>
    </form>
    <div style="display: none;">
        <asp:Label runat="server" Text="<%$Resources:Language,Msg_OuterNetHide %>"> </asp:Label>
    </div>
</body>
</html>
