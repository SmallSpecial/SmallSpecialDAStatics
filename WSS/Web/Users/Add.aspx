<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="WSS.Web.Users.Add" Title="����ҳ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		�û���
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_UserName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_PassWord" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		��������
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_DepartID" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		��ɫ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_RoleID" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�ձ�
	��</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkF_Sex" Text="�ձ�" runat="server" Checked="False" />
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtF_Birthday" runat="server" Width="70px"  onfocus="setday(this)"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Email" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�ƶ��绰
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_MobilePhone" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		ע��ʱ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtF_RegTime" runat="server" Width="70px"  onfocus="setday(this)"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		����¼ʱ��
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtF_LastInTime" runat="server" Width="70px"  onfocus="setday(this)"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_IsUsed
	��</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkF_IsUsed" Text="F_IsUsed" runat="server" Checked="False" />
	</td></tr>
</table>
<script src="/js/calendar1.js" type="text/javascript"></script>

            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="����"
                    OnClick="btnSave_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="ȡ��"
                    OnClick="btnCancle_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
