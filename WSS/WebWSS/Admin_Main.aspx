<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Main.aspx.cs" Inherits="WebWSS.Admin_Main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="img/Style.css" rel="stylesheet" type="text/css"></link>

    <script type="text/javascript" src="img/Charts/FusionCharts.js"></script>

</head>
<body style="overflow-x: auto; overflow-y: scroll;">
    <form id="form1" runat="server" visible="false">
    <div style="border: 1px solid #fff;">
        <div style="background-color: #C4D8ED; font-size: 9pt; font-weight: normal; padding: 5px 0px 1px 5px;
            height: 30px;">
            <asp:Label ID="LabelTitle" runat="server" Text=" <b>数据总览</b>"></asp:Label>
            当前时间:<%=DateTime.Now.ToString() %><br />
        </div>
        <table class="td2" style="width: 99.5%;">
            <tr>
                <td>
                    <asp:Label ID="lblInfo0" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblInfo1" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="ss">
                    </div>
                </td>
            </tr>
        </table>
        <div style="background-color: #C4D8ED; font-size: 9pt; font-weight: bold; padding: 5px 0px 1px 5px;
            height: 30px;">
            <asp:Label ID="Label1" runat="server" Text=" 数据图表"></asp:Label>
        </div>
        <table class="td2" style="width: 99.5%;">
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td width="800px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal><asp:Literal ID="Literal3"
                        runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <%--    <script type="text/javascript">
        // document.getElementById("ss").innerHTML = document.getElementById("mychart").innerHTML;
            
    </script>--%>
</body>
</html>
