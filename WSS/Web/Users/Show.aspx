<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="WSS.Web.Users.Show" Title="��ʾҳ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		�û�ID
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_UserID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�û���
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_UserName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_PassWord" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		��������
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_DepartID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		��ɫ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_RoleID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�ձ�
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_Sex" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_Birthday" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_Email" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�ƶ��绰
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_MobilePhone" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		ע��ʱ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_RegTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����¼ʱ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblF_LastInTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_IsUsed
	��</td>
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




