<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Main_Data.aspx.cs"
    Inherits="WebWSS.Admin_Main_Data" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="img/Style.css" rel="stylesheet" type="text/css"></link>
</head>
<body style="overflow-x: auto; overflow-y: scroll;">
    <form id="form1" runat="server">
    <div style="border: 1px solid #fff;">
        <div style="background-color: #C4D8ED; font-size: 9pt; font-weight: bold; padding: 5px 0px 1px 5px;
            height: 30px;">
            <asp:Label ID="Label2" runat="server" Text="统计运行状态"></asp:Label>
            当前时间:<%=DateTime.Now.ToString() %>
        </div>
        <table class="td2" style="width: 99.5%;">
            <tr>
                <td width="6px">
                </td>
                <td style="vertical-align: text-top">
                    <br />
                    <asp:Label ID="lblInfo2" runat="server" Text=""></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
        <div style="background-color: #C4D8ED; font-size: 9pt; font-weight: bold; padding: 5px 0px 1px 5px;
            height: 30px;">
            <asp:Label ID="Label1" runat="server" Text="运行信息"></asp:Label>
        </div>
        <table class="td2" style="width: 99.5%;">
            <tr>
                <td width="2px">
                </td>
                <td style="vertical-align: text-top">
 
                  
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%"  BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="100" ShowFooter="false">
                            <Columns>
                                <asp:BoundField DataField="F_ID" HeaderText="编号" />
                                <asp:BoundField DataField="F_RunID" HeaderText="批次" />
                                <asp:BoundField DataField="F_Msg" HeaderText="信息" />
                                <asp:BoundField DataField="F_DateTime" HeaderText="<%$Resources:Language,LblTime %>" />
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
                        <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
                  
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
