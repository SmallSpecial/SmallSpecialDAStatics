<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemDropDayNow.aspx.cs" Inherits="WSS.Web.AdminF.Stats.ItemDropDayNow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="../CSS/wssstyle.css" rel="stylesheet" type="text/css" />
    <script src="../../js/DataPicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <h3>
                <asp:Label ID="LabelTitle" runat="server" Text=" 当天掉落统计(装备)"></asp:Label></h3>
        </div>
        <div class="search">
            大区:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged"  >
                <asp:ListItem>所有大区</asp:ListItem>
            </asp:DropDownList>
            服务器:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged" >
                <asp:ListItem>所有战区</asp:ListItem>
            </asp:DropDownList>
           <%-- 线路:--%><asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem>所有战线</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
             开始时间:<asp:TextBox ID="tboxTimeB" runat="server" Width="120px" MaxLength="20" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})" class="Wdate"></asp:TextBox>
                          结束时间:<asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="20" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})" class="Wdate"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>
                区域: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>  时间: <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25" 
                onrowcreated="GridView1_RowCreated" ShowFooter=true>
                <Columns>
                   
                    <asp:BoundField DataField="日期" HeaderText="日期" />
                    <asp:BoundField DataField="装备星级" HeaderText="装备星级" />
                    <asp:BoundField DataField="虎贲" HeaderText="虎贲" />
                    <asp:BoundField DataField="浪人" HeaderText="浪人" />
                    <asp:BoundField DataField="龙胆" HeaderText="龙胆" />
                    <asp:BoundField DataField="巧工" HeaderText="巧工" />
                    <asp:BoundField DataField="气功师" HeaderText="气功师" />
                    <asp:BoundField DataField="花灵" HeaderText="花灵" />
                    <asp:BoundField DataField="天师" HeaderText="天师" />
                    <asp:BoundField DataField="行者" HeaderText="行者" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             
            <asp:Label ID="lblerro" runat="server" Text="提示:没有相关数据!" ForeColor="#FF6600"></asp:Label>
            
           
           
           
         <div class="titletip">
            <h7>
              <br /><span class="tyellow">绿色(3星)装备</span> [<%=DropDownListVocation.Text%>] <span style="margin-left: 20px;">
            职业选择:<asp:DropDownList ID="DropDownListVocation" runat="server" Width="120" 
                  AutoPostBack="True" onselectedindexchanged="DropDownListVocation_SelectedIndexChanged"  >
                <asp:ListItem>所有职业</asp:ListItem>
                <asp:ListItem>虎贲</asp:ListItem>
                <asp:ListItem>浪人</asp:ListItem>
                <asp:ListItem>龙胆</asp:ListItem>
                <asp:ListItem>巧工</asp:ListItem>
                <asp:ListItem>气功师</asp:ListItem>
                <asp:ListItem>花灵</asp:ListItem>
                <asp:ListItem>天师</asp:ListItem>
                <asp:ListItem>行者</asp:ListItem>
            </asp:DropDownList></span></h7>
        </div>
            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
                         <div class="titletip">
            <h7>
              <br /><span class="tyellow">蓝色(4星)装备</span> [<%=DropDownListVocation.Text%>] </h7>
        </div>
            <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             
        <div class="titletip">
   
              <br /><span class="tyellow">黄色(5星)装备</span> [<%=DropDownListVocation.Text%>]
            
 
        </div>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             <div class="titletip">
            <h7>
              <br /><span class="tyellow">橙色(6星)装备</span> [<%=DropDownListVocation.Text%>] </h7>
        </div>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             <div class="titletip">
            <h7>
              <br /><span class="tyellow">红色(7星)装备</span> [<%=DropDownListVocation.Text%>] </h7>
        </div>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             <div class="titletip">
            <h7>
              <br /><span class="tyellow">紫色(8星)装备</span> [<%=DropDownListVocation.Text%>] </h7>
        </div>
            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
             <div class="titletip">
            <h7>
              <br /><span class="tyellow">神器装备</span> [<%=DropDownListVocation.Text%>] </h7>
        </div>
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" Font-Size="12px" Width="95%" 
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" PageSize="25" OnRowDataBound="GridView2_RowDataBound" ShowFooter=true   >
                <Columns>
                   
                    <asp:BoundField DataField="装备类别" HeaderText="装备类别" />
                    <asp:BoundField DataField="第一套" HeaderText="第一套(1级)" />
                    <asp:BoundField DataField="第二套" HeaderText="第二套(30级)" />
                    <asp:BoundField DataField="第三套" HeaderText="第三套(50级)" />
                    <asp:BoundField DataField="第四套" HeaderText="第四套(70级)" />
                    <asp:BoundField DataField="第五套" HeaderText="第五套(80级)" />
                    <asp:BoundField DataField="第六套" HeaderText="第六套(90级)" />
                    <asp:BoundField DataField="第七套" HeaderText="第七套(100级)" />
                    <asp:BoundField DataField="第八套" HeaderText="第八套" />
                    <asp:BoundField DataField="第九套" HeaderText="第九套" />
                    <asp:BoundField DataField="第十套" HeaderText="第十套" />
                    <asp:BoundField DataField="第十一套" HeaderText="第十一套" />
                    <asp:BoundField DataField="第十二套" HeaderText="第十二套" />
                    <asp:BoundField DataField="第十三套" HeaderText="第十三套" />
                    <asp:BoundField DataField="总计" HeaderText="总计" />
                   
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#005652" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#3f5c57" ForeColor="#FFFFFF" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#c9d4d1" />
            </asp:GridView>
        </div>
    </div> 

    </form>
</body>
</html>

