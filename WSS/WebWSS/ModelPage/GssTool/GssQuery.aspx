<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GssQuery.aspx.cs" Inherits="WebWSS.ModelPage.GssTool.GssQuery" %>

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
        th{
            background-color:#C4D8ED;
        }
        td{
            height:20px;
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
        <asp:HiddenField ID="hidType" runat="server" />
        <asp:HiddenField ID="hidUserID" runat="server" />
        <asp:HiddenField ID="hidUserName" runat="server" />
        <asp:HiddenField ID="hidRoleID" runat="server" />
        <asp:HiddenField ID="hidRoleName" runat="server" />
        <div class="search">
            <div class="switchLine">
                <span style="margin-left:20px;">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblUserNo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbUserID"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,UserName %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbUserName"></asp:TextBox>
                </span>
                <asp:Button runat="server" ID="btnUserSearch" Text="<%$Resources:Language,Btn_AccountQuery %>" CssClass="button" OnClick="btnUserSearch_Click" />
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbRoleID"></asp:TextBox>
                </span>
                <span>
                    <asp:Label runat="server" Text="<%$Resources:Language,LblRoleName %>"></asp:Label>:
                </span>
                <span>
                    <asp:TextBox runat="server" ID="tbRoleName"></asp:TextBox>
                </span>
                <asp:Button runat="server" ID="btnRoleSearch" Text="<%$Resources:Language,Btn_RoleQuery %>" CssClass="button" OnClick="btnRoleSearch_Click" />
                <span style="margin-left:20px;">
                    <asp:Button runat="server" ID="btnReset" Text="<%$Resources:Language,BtnReset %>" CssClass="button"  OnClick="btnReset_Click"/>
                </span>
            </div>
        </div>
        <div style="height:400px; overflow:auto">
            <div style="margin-left: 20px; margin-top: 20px;">
                <asp:Label runat="server" Text="<%$Resources:Language,LblUserInfo %>"></asp:Label>
                <div role="tabpanel" id="divUserInfo" runat="server">
                </div>
            </div>
            <div style="margin-left: 20px; margin-top: 20px;">
                <asp:Label runat="server" Text="<%$Resources:Language,LblRoleInfo %>"></asp:Label>
                <div role="tabpanel" id="divRoleInfo" runat="server">
                </div>
            </div>
        </div>
        <div style="margin-top:20px;">
            <div class="itemtitle">
                <asp:Label runat="server" Text="<%$Resources:Language,LblGssOp %>"></asp:Label>
            </div>
            <div style="margin-left: 20px;">
                <div>
                    <div style="display: inline; float: left; width: 50%">
                        <div>
                            <asp:Label runat="server" Text="<%$Resources:Language,LblUserOrder %>"></asp:Label>
                        </div>
                        <table border="1" bordercolor="#a0c6e5" style="border-collapse: collapse; width: 96%">
                            <tr style="height: 60px;">
                                <td>
                                    <asp:Button runat="server" ID="Button7" Text="<%$Resources:Language,Account_btnCloseDown %>" CssClass="button" OnClientClick="return ShowBlock(1,1,1101);" />
                                    <asp:Button runat="server" ID="Button9" Text="<%$Resources:Language,Account_btnUnlock %>" CssClass="button" OnClientClick="return ShowBlock(1,2,1102);" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="display: inline; float: right; width: 50%">
                        <div>
                            <asp:Label runat="server" Text="<%$Resources:Language,LblRoleOrder %>"></asp:Label>
                        </div>
                        <table border="1" bordercolor="#a0c6e5" style="border-collapse: collapse; width: 96%">
                            <tr>
                                <td style="height: 60px;">
                                    <asp:Button runat="server" ID="Button2" Text="<%$Resources:Language,Account_btnCloseDown %>" CssClass="button" OnClientClick="return ShowBlock(2,1,2201);" />
                                    <asp:Button runat="server" ID="Button3" Text="<%$Resources:Language,Account_btnUnlock %>" CssClass="button" OnClientClick="return ShowBlock(2,2,2202);" />
                                    <asp:Button runat="server" ID="Button4" Text="<%$Resources:Language,Account_btnGag %>" CssClass="button" OnClientClick="return ShowBlock(2,1,2203);" />
                                    <asp:Button runat="server" ID="Button5" Text="<%$Resources:Language,BtnDisChatRecovery %>" CssClass="button" OnClientClick="return ShowBlock(2,2,2213);" />
                                    <asp:Button runat="server" ID="Button6" Text="<%$Resources:Language,BtnRoleRecovery %>" CssClass="button" OnClientClick="return ShowBlock(2,2,2208);" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!--弹出层，-->
        <div id="divNewBlock" style="border: solid 3px; padding: 10px; width: 30%; z-index: 1001; position: absolute; display: none; top: 30%; left: 30%;background:#C4D8ED;">
            <div style="padding: 3px 15px 3px 15px; text-align: left; vertical-align: middle;">
                <div class="itemtitle">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label>
                </div>
                <div class="itemtitle">
                    <span>
                        <asp:Label ID="lblUserIDContent" runat="server"></asp:Label>:
                    </span>
                    <span>
                        <asp:Label ID="lblUserID" runat="server"></asp:Label>
                    </span>
                </div>
                <div class="itemtitle">
                    <span>
                        <asp:Label ID="lblUserNameContent" runat="server"></asp:Label>:
                    </span>
                    <span>
                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    </span>
                </div>
                <div class="itemtitle" id="lockTime">
                    <span>
                        <asp:Label runat="server" Text="<%$Resources:Language,LblLockTime %>"></asp:Label>:
                    </span>
                    <span>
                        <asp:DropDownList runat="server" ID="ddlLockTime"></asp:DropDownList>
                    </span>
                </div>
                <div class="itemtitle">
                    <asp:Label runat="server" Text="<%$Resources:Language,LblOrderBak %>"></asp:Label>:
                </div>
                <div>
                    <asp:TextBox ID="tbBak" runat="server" TextMode="MultiLine" Width="100%" Height="100"></asp:TextBox>
                </div>
                <div style="text-align:center;margin-top:10px;">
                    <asp:Button ID="BtnOperation" runat="server" Text="<%$Resources:Language,LblConfirm %>" CssClass="button" OnClientClick="return Operate();" OnClick="BtnOperation_Click" />
                    <asp:Button ID="BttCancel" runat="server" Text="<%$Resources:Language,LblClose %>" CssClass="button" OnClientClick="return HideBlock();" />
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
    <script type="text/javascript" language="javascript">
        var userID;
        var userName;
        var roleID;
        var roleName;
        function tagscheckUser(a) {
            var lng = document.getElementsByTagName("tr").length;
            for (i = 0; i < lng; i++) {
                var temp = document.getElementsByTagName("tr")[i];
                if (a == temp) {
                    //选中的标签样式  
                    userID = temp.childNodes[0].textContent;
                    userName = temp.childNodes[1].textContent;
                    roleID = "";
                    roleName = "";
                    temp.style.background = "#ccc";
                } else {
                    //恢复原状  
                    temp.style.background = "";
                }
            }
        }
        function tagscheckRole(a) {
            var lng = document.getElementsByTagName("tr").length;
            for (i = 0; i < lng; i++) {
                var temp = document.getElementsByTagName("tr")[i];
                if (a == temp) {
                    //选中的标签样式  
                    userID = "";
                    userName = "";
                    roleID = temp.childNodes[0].textContent;
                    roleName = temp.childNodes[1].textContent;
                    temp.style.background = "#ccc";
                } else {
                    //恢复原状  
                    temp.style.background = "";
                }
            }
        }
        function HideBlock() {
            document.getElementById("divNewBlock").style.display = "none";
            return false;
        }
        //1用户2角色
        //1封停工具2解封工具
        function ShowBlock(UserOrRole, toolType, type) {
            if (toolType == 1) {
                document.getElementById("lblTitle").textContent = "<%=GetContent("lockTool")%>";
                document.getElementById("lockTime").style.display = "";
            }
            else if (toolType == 2) {
                document.getElementById("lblTitle").textContent = "<%=GetContent("unLockTool")%>";
                document.getElementById("lockTime").style.display = "none";
            }

            if (UserOrRole == 1) {
                if (userID) {
                    var set = SetBlock();
                    document.getElementById("divNewBlock").style.display = "";

                    document.getElementById("lblUserIDContent").textContent = "<%=GetContent("userID")%>";
                    document.getElementById("lblUserID").innerText = userID;
                    document.getElementById("lblUserNameContent").textContent = "<%=GetContent("userName")%>";
                    document.getElementById("lblUserName").innerText = userName;

                    $("#<%=hidType.ClientID %>").val(type);
                    $("#<%=hidUserID.ClientID %>").val(userID);
                    $("#<%=hidUserName.ClientID %>").val(userName);
                }
                else {
                    if (type == 1101) {
                        alert("<%=GetContent("selectLockUser")%>");
                    }
                    else if (type == 1102) {
                        alert("<%=GetContent("selectUnLockUser")%>");
                    }
                }
            }
            else if (UserOrRole == 2) {
                if (roleID) {
                    var set = SetBlock();
                    document.getElementById("divNewBlock").style.display = "";

                    document.getElementById("lblUserIDContent").textContent = "<%=GetContent("roleID")%>";
                    document.getElementById("lblUserID").innerText = roleID;
                    document.getElementById("lblUserNameContent").textContent = "<%=GetContent("roleName")%>";
                    document.getElementById("lblUserName").innerText = roleName;

                    $("#<%=hidType.ClientID %>").val(type);
                    $("#<%=hidRoleID.ClientID %>").val(roleID);
                    $("#<%=hidRoleName.ClientID %>").val(roleName);
                }
                else {
                    if (type == 2201) {
                        alert("<%=GetContent("selectLockRole")%>");
                    }
                    else if (type == 2202) {
                        alert("<%=GetContent("selectUnLockRole")%>");
                    }
                    else if (type == 2203) {
                        alert("<%=GetContent("selectDisRoleChatAdd")%>");
                    }
                    else if (type == 2213) {
                        alert("<%=GetContent("selectDisRoleChatDel")%>");
                    }
                    else if (type == 2208) {
                        alert("<%=GetContent("selectRoleRecovery")%>");
                    }
                }
            }
            
            return false;
        }

        function SetBlock() {
            var top = document.body.scrollTop;
            var left = document.body.scrollLeft;
            var height = document.body.clientHeight;
            var width = document.body.clientWidth;

            if (top == 0 && left == 0 && height == 0 && width == 0) {
                top = document.documentElement.scrollTop;
                left = document.documentElement.scrollLeft;
                height = document.documentElement.clientHeight;
                width = document.documentElement.clientWidth;
            }
            return { top: top, left: left, height: height, width: width };
        }

        function Operate() {
            return true;
        }
    </script>
</html>
