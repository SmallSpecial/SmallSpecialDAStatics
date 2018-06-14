<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="WSS.Web.Tasks.Modify" Title="修改页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		F_ID
	：</td>
	<td height="25" width="*" align="left">
		<asp:label id="lblF_ID" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_Title
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Title" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_Note
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Note" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_From
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_From" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_Type
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Type" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_JinjiLevel
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_JinjiLevel" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_GameName
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_GameName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_GameZone
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_GameZone" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_GUserID
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_GUserID" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_GRoleName
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_GRoleName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_Tag
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Tag" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_DateTime
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_DateTime" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_State
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_State" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_Telphone
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_Telphone" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		F_DutyMan
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtF_DutyMan" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
</table>

            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="保存"
                    OnClick="btnSave_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消"
                    OnClick="btnCancle_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>

