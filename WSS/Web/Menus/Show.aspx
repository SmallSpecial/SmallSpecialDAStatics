<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="WSS.Web.Menus.Show" Title="œ‘ æ“≥" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		≤Àµ•±‡∫≈
	£∫</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_MenuID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		≤Àµ•√˚≥∆
	£∫</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_Name" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		∏∏±‡∫≈
	£∫</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_ParentID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		 «∑Ò∆Ù”√
	£∫</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_IsUsed" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




