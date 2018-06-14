<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnLine1.aspx.cs" Inherits="WSS.Web.AdminF.Stats.UserOnLine1" %>

<%@ Register src="../UserControl/page.ascx" tagname="page" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="/adminf/css/style.css" type="text/css" rel="stylesheet" />
<link href="/adminf/CSS/xylib.css" type="text/css" rel="stylesheet" />
<script language ="javascript" type ="text/javascript" src ="/adminf/javascript/base.js"></script>
<script language ="javascript" type ="text/javascript" src ="/adminf/javascript/CheckedAll.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound"   
                runat="server" AutoGenerateColumns="False"  Font-Size="12px" Width="530px" 
                OnSorting="GridView1_Sorting" AllowSorting="True" CellPadding="3" 
                BorderWidth="1px" BorderStyle="None" AllowPaging="True" BackColor="White" 
                BorderColor="#CCCCCC" onpageindexchanging="GridView1_PageIndexChanging" >
          <Columns>
            <asp:BoundField DataField="F_ID" HeaderText="账号" SortExpression="F_ID" />
            <asp:BoundField DataField="F_ID" HeaderText="姓名"  SortExpression="F_ID"/>
            <asp:BoundField DataField="F_ID" HeaderText="性别"  SortExpression="F_ID" />
            <asp:BoundField DataField="F_ID" HeaderText="住址" SortExpression="F_ID" />
          </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" 
                    Font-Bold="True" ForeColor="White" />
            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#D1DDF1" ForeColor="#000066" />
            <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#D1DDF1" />
        </asp:GridView>
        

    </div>
    <table  width="100%" >
<tr>
<td class="right">    
<h1><a href="#">后台管理首页</a> >> 日志管理</h1>
<div class="main-setting">
<div class="itemtitle"><h3>日志管理</h3>
</div>


<table class="xy_tb xy_tb2">
    <tr>
<td class="content_action">标题：<asp:textbox id="txtname" runat="server" CssClass="input"></asp:textbox>&nbsp;
    管理员名称：<asp:TextBox ID="TextBox1" runat="server" CssClass="input"></asp:TextBox>
    模块名：<asp:TextBox ID="TextBox2" runat="server" CssClass="input"></asp:TextBox>
    <asp:button id="Button4" runat="server" CssClass="button" Text="搜索"></asp:button></td>
</tr>
<tr>
<td>
<asp:GridView HeaderStyle-CssClass="gv_header_style" ID="gvlist" runat="server" AutoGenerateColumns="False"  DataKeyNames="L_ID" width="100%" PageSize="20" GridLines="None" >
<Columns>
    <asp:BoundField DataField="L_ID" HeaderText="L_ID" Visible="False" />
<%--     <asp:TemplateField HeaderText="选择">
 <ItemTemplate>
<asp:CheckBox ID="chkExport" runat="server" />
</ItemTemplate> 
         <ItemStyle Width="5%" />
     </asp:TemplateField>--%>
    <asp:BoundField DataField="L_Title" HeaderText="标题" >
    <HeaderStyle  CssClass="gvLeft"/>
        <ItemStyle Width="45%" CssClass="gvLeft" />
    </asp:BoundField>
    <asp:BoundField DataField="UM_Name" HeaderText="管理员名称" >
   <HeaderStyle CssClass="gvLeft"/>
        <ItemStyle Width="10%" CssClass="gvLeft"/>
    </asp:BoundField>
       <asp:BoundField DataField="L_MF" HeaderText="模块名" >
           <ItemStyle Width="15%" />
       </asp:BoundField>
       <asp:BoundField DataField="L_addtime" HeaderText="日期" >
           <ItemStyle Width="20%" />
       </asp:BoundField>
    <asp:HyperLinkField DataNavigateUrlFields="L_ID" DataNavigateUrlFormatString="loginfo.aspx?L_ID={0}"
        HeaderText="查看" NavigateUrl="loginfo.aspx?L_ID={0}" Text="&lt;img src=&quot;../images/look.gif&quot; /&gt;" >
        <ItemStyle Width="5%" />
    </asp:HyperLinkField>
</Columns>
</asp:GridView>
</td>
</tr>
<%--<tr>
<td class="content_action"><input class="list_td04" id="chkAll" onclick="chkAll_true()" type="checkbox" name="chkAll"
runat="server"/>全选
<asp:Button ID="Button1" runat="server" Text="删除"  CssClass ="button"/>
    <asp:Button ID="btnDelAll" runat="server" CssClass="button"  
        Text="删除全部" /></td> 															
</tr>--%>
</table>
<p style="text-align:center;"><asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></p>
    <uc1:page ID="page2" runat="server" />
</div>
</td></tr>
</table>  
    </form>
</body>
</html>
