<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailLog.aspx.cs" Inherits="WebWSS.Stats.EmailLog" %>

<%@ Register Src="../Common/ControlDateSelect.ascx" TagName="ControlDateSelect" TagPrefix="uc1" %>
<%@ Register Src="../Common/ControlChartSelect.ascx" TagName="ControlChartSelect"
    TagPrefix="uc2" %>
<%@ Register Src="../Common/ControlChart.ascx" TagName="ControlChart" TagPrefix="uc3" %>
<%@ Register src="../Common/ControlOutFile.ascx" tagname="ControlOutFile" tagprefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>
    <script language='JavaScript' src='../img/Admin.Js'></script>
    <script type="text/javascript" src='../img/GetDate.Js'></script>
    <style type="text/css">
        a.tooltips {
            clear: both;
            color: #006699;
            position: relative;
            z-index: 2;
            font-size: 12pt;
        }

            a.tooltips:hover {
                z-index: 3;
                background: none;
            }

            a.tooltips span {
                display: none;
            }

            a.tooltips:hover span {
                display: block;
                position: absolute;
                top: -140px;
                left: 50px;
                width: 15em;
                border: 1px solid #006699;
                background-color: #ccffff;
                padding: 3px;
                color: black;
                font-weight: normal;
                font-size: 9pt;
                filter: alpha(opacity=80);
            }
    </style>
    <script type="text/javascript">
        var t_id = setInterval(animate, 20);
        var pos = 0; var dir = 2;
        var len = 0;
        function animate() {
            var elem = document.getElementById('progress');
            if (elem != null) {
                if (pos == 0)
                    len += dir;
                if (len > 32 || pos > 79)
                    pos += dir;
                if (pos > 79)
                    len -= dir;
                if (pos > 79 && len == 0)
                    pos = 0;
                elem.style.left = pos; elem.style.width = len;
            }
        }
        function remove_loading() {
            this.clearInterval(t_id);
            var targelem = document.getElementById('loader_container');
            targelem.style.display = 'none';
            targelem.style.visibility = 'hidden';
        }
    </script>
    <style>
        #loader_container {
            text-align: center;
            position: absolute;
            top: 40%;
            width: 100%;
            left: 0;
        }

        #loader {
            font-family: Tahoma, Helvetica, sans;
            font-size: 11.5px;
            color: #000000;
            background-color: #FFFFFF;
            padding: 10px 0 16px 0;
            margin: 0 auto;
            display: block;
            width: 130px;
            border: 1px solid #5a667b;
            text-align: left;
            z-index: 2;
        }

        #progress {
            height: 5px;
            font-size: 1px;
            width: 1px;
            position: relative;
            top: 1px;
            left: 0px;
            background-color: #8894a8;
        }

        #loader_bg {
            background-color: #e4e7eb;
            position: relative;
            top: 8px;
            left: 8px;
            height: 7px;
            width: 113px;
            font-size: 1px;
        }
    </style>
</head>
<body style="overflow-x: hidden; overflow-y: scroll;" onload="remove_loading();">
    <div id="loader_container">
        <div id="loader">
            <div align="center">
                <asp:Label runat="server" Text="<%$ Resources:Language,Tip_PageLoading %>"></asp:Label>
            </div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:Label ID="LabelTitle" runat="server" Text="Releact Data"></asp:Label>
            >>
            <asp:Label runat="server" Text="Email信息统计"></asp:Label>
        </div>
        <div class="search">
            <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
            </asp:DropDownList>

            <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" Text="邮件类型"></asp:Label>:
            <asp:TextBox runat="server" ID="tbEmailType"></asp:TextBox>

            <asp:Label runat="server" Text="角色编号"></asp:Label>:
            <asp:TextBox runat="server" ID="tbRoleID"></asp:TextBox>
            &nbsp;
            <asp:Literal runat="server" Text="开始时间"></asp:Literal>:
            <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10">
            </asp:TextBox>
            <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'>
                <img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>

            <asp:Literal runat="server" Text="结束时间"></asp:Literal>:
            <asp:TextBox ID="tboxTimeE" runat="server" Width="80px" MaxLength="10">
            </asp:TextBox>
            <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeE"), "yyyy-mm-dd", null, null, null, "11");'>
                <img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>

            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>            
            <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" />
            <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false /> &nbsp;
                <asp:Button ID="btnOutExcel" runat="server" Text="EXCEL" CssClass="buttonbl" onclick="ExportExcel" />
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
            <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:
            <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   
            <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>: 
            <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
     <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="false"></asp:Label>
     <asp:Label ID="lblDeType" runat="server" Text="43" Visible="true"></asp:Label>
        </div>
       <div class="gridv" style="overflow:auto;width:100%;">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12pt" Width="3600px" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="true">
                 <Columns>
                    <%--<asp:BoundField DataField="Mail_Title" HeaderText="Number" />--%>
                    <asp:BoundField DataField="Mail_Content" HeaderText="邮件内容" HeaderStyle-Width="1000" />
                    <asp:BoundField DataField="Mail_TYPE" HeaderText="邮件类型" HeaderStyle-Width="100" />
                    <asp:TemplateField HeaderText="邮件类型" ItemStyle-Width="400">
                        <ItemTemplate>
                            <%#GetMailType(Eval("Mail_TYPE").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Receiver_ID" HeaderText="接收人角色ID" HeaderStyle-Width="300" />
                    <%--<asp:BoundField DataField="Sender_Name" HeaderText="Number" />--%>
                    <asp:BoundField DataField="Send_Time" HeaderText="发送时间" HeaderStyle-Width="400" />
                    <asp:BoundField DataField="Invalid_Time" HeaderText="失效时间" HeaderStyle-Width="400" />
                    <asp:BoundField DataField="State_Modi_Time" HeaderText="操作时间" HeaderStyle-Width="400" />
                    <asp:TemplateField HeaderText="邮件状态" ItemStyle-Width="100">
                        <ItemTemplate>
                            <%#GetMailState(Eval("Mail_State").ToString(),Eval("IsDelete").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Money" HeaderText="金币"  HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Money_Sliver" HeaderText="银币" HeaderStyle-Width="100" />
                    <asp:BoundField DataField="TongBao" HeaderText="红钻" HeaderStyle-Width="100" />
                    <asp:BoundField DataField="TongBei" HeaderText="蓝钻" HeaderStyle-Width="100" />
                    <asp:BoundField DataField="ItemBak1" HeaderText="Item1" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBakID1" HeaderText="数量" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBak2" HeaderText="Item2" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBakID2" HeaderText="数量" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBak3" HeaderText="Item3" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBakID3" HeaderText="数量" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBak4" HeaderText="Item4" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBakID4" HeaderText="数量" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBak5" HeaderText="Item5" HeaderStyle-Width="200" />
                    <asp:BoundField DataField="ItemBakID5" HeaderText="数量" HeaderStyle-Width="200" />
                    <asp:TemplateField HeaderText="是否删除" ItemStyle-Width="100">
                        <ItemTemplate>
                            <%#GetIsDelete(Eval("IsDelete").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                    <div style="text-align: right; color: Blue;margin-right:70px;">
                        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First" Text="<%$Resources:Language,LblHomePage %>" OnClick="lbtnF_Click">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page" Text="<%$Resources:Language,LblPrviousPage %>" OnClick="lbtnP_Click">
                        </asp:LinkButton>
                        &nbsp;
                        第
                        <asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='1'>
                        </asp:Label>
                        页/共
                        &nbsp;
                        <asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text=''>
                        </asp:Label>
                        &nbsp;
                        页
                        <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next" Text="<%$Resources:LAnguage,LblNextPage %>" OnClick="lbtnN_Click">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                             Text="<%$Resources:Language,LblEndPage %>" OnClick="lbtnE_Click"></asp:LinkButton>
                        &nbsp;
                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini"></asp:TextBox>页<asp:Button ID="Button3" runat="server" OnClick="Go_Click" Text="<%$Resources:Language,LblGoto %>" />&nbsp;
                    </div>
            <asp:Label ID="lblerro" runat="server"  Text="<%$Resources:Language,Tip_Sign %>"  ForeColor="#FF6600"></asp:Label>
            <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
            
        </div>
    </div>
    <br /><br /><br /><br /><br />
    <span style="display:none"><asp:Label ID="lblDebug" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
    </form>
</body>
</html>
