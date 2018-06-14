<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CDKeyBatchImport.aspx.cs"
    Inherits="WebWSS.CDKey.CDKeyBatchImport" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

    <script type="text/javascript" src="../img/ProgressBar.js"></script>

    <style>
        .statusTable
        {
            width: 100px;
            border: solid 1px #000000;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }
        .statusTable td
        {
            height: 20px;
        }
    </style>
     <script type="text/javascript">
         var tip = '<asp:Literal runat="server" Text="<%$Resources:Language,CDK_SurePublishDatas%>" />';
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchList.aspx" Text="<%$Resources:Language,CDK_BatchManage %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyList.aspx" Text="<%$Resources:Language,CDK_CDKList %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyCreate.aspx" Text="<%$Resources:Language,CDK_CDKeyCreate %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyBatchImport.aspx" Enabled="False" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/cdkey/CDKeyImport.aspx" Text="<%$Resources:Language,CDK_CDKImport %>"></asp:LinkButton>
        </div>
        <div class="gridv" id="divMain" runat="Server">
            1.<asp:Label runat="server" Text="<%$Resources:Language,CDK_ImportFile %>"></asp:Label>
            <table class="Text" cellspacing="1" cellpadding="0" width="95%" bgcolor="#1d82d0"
                border="0">
                <tr bgcolor="#ffffff">
                    <td valign="top">
                        <table class="Text" cellspacing="1" cellpadding="0" width="100%" border="0">
                            <tr height="30">
                                <td style="width: 120px" width="120">
                                    &nbsp;<font face="宋体">
                                                <asp:Label runat="server" Text="<%$Resources:Language,CDK_PleaseWaitImportFile %>"></asp:Label>
                                          </font>
                                </td>
                                <td align="left" width="370">
                                    <input runat="server" id="FileExcel"  lang="ko" style="width: 330px;height:30px;" type="file" size="42" name="FilePhoto"  onchange="filechange();"/>
                                </td>
                                <td class="hint">
                                    <asp:Button ID="BtnImport" Text="<%$Resources:Language,CDK_Import %>" CssClass="buttonbl" runat="server" OnClick="BtnImport_Click">
                                    </asp:Button>
                                    &nbsp;
                                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_FileFormatIs %>"></asp:Label>
                                    <font color="red" ><asp:Label runat="server"  Text="<%$Resources:Language,CDK_BatchManage %>"></asp:Label></font>
                                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_ImportExcel %>"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            2.<asp:Label runat="server" Text="<%$Resources:Language,CDK_PublishToService %>"></asp:Label>
            <br />
            <table border="0" cellspacing="0" cellpadding="0" width="95%" style="border: 1px solid #1d82d0;">
                <tr valign="bottom">
                    <td width="497px">
                        <asp:CheckBox ID="cbReset" runat="server" Text="<%$Resources:Language,CDK_IsResetOriginData %>" Font-Bold="True" /><br />
                        <asp:Label ID="LblMessagePub" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnPublish" runat="server" Text="<%$Resources:Language,CDK_Publish %>" CssClass="buttonbl" OnClientClick='return confirmFun();'
                            Style="margin: 8px 0px 8px 0px;" OnClick="btnPublish_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:DropDownList ID="ddlKeyType" runat="server" Visible="false">
                <asp:ListItem Text="<%$Resources:Language,LblAllSelect %>"></asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server" Font-Size="12px" Width="95%" CellPadding="3"
                BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False"
                PageSize="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Language,CDK_DataStatue %>">
                        <ItemTemplate>
                            <%#Eval("F_State").ToString().Length > 0 ? "<font color='red'>" + Eval("F_State") + "</font>" :  GetResource("CDK_Ready")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_BatchID" HeaderText="<%$Resources:Language,CDK_BatchNo %>" />
                    <asp:TemplateField HeaderText="<%$Resources:Language,CDK_CardType %>">
                        <ItemTemplate>
                            <%#GetTypeName(ddlKeyType, Eval("F_KeyType").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_KeyCount" HeaderText="<%$Resources:Language,CDK_GenerateNumber %>" />
                    <asp:BoundField DataField="F_ExcelID" HeaderText="<%$Resources:Language,CDK_AwardNo %>" />
                    <asp:BoundField DataField="F_ItemNum" HeaderText="<%$Resources:Language,CDK_AwardNumber %>" />
                    <asp:BoundField DataField="F_IsPublish" HeaderText="<%$Resources:Language,CDK_IsPublish %>" />
                    <asp:BoundField DataField="F_CreateTime" HeaderText="<%$Resources:Language,CDK_GenerateTime %>" />
                    <asp:BoundField DataField="F_Note" HeaderText="<%$Resources:Language,CDK_Remark %>" />
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#D1DDF1" />
            </asp:GridView>
        </div>
        <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        <br />
        <b><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label>:

        </b>
        <asp:Label runat="server" Text="<%$Resources:Language,CDK_TipImportExcelCSV %>"></asp:Label>
        <br />
        <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
    </div>
    </form>
</body>
</html>
