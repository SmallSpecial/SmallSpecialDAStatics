<%@ Page Title="�û���" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WSS.Web.Users.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!--Title -->

            <!--Title end -->

            <!--Add  -->

            <!--Add end -->

            <!--Search -->
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td style="width: 80px" align="right" class="tdbg">
                         <b>�ؼ��֣�</b>
                    </td>
                    <td class="tdbg">                       
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ"  OnClick="btnSearch_Click" >
                    </asp:Button>                    
                        
                    </td>
                    <td class="tdbg">
                    </td>
                </tr>
            </table>
            <!--Search end-->
            <br />
            <asp:GridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"  OnPageIndexChanging ="gridView_PageIndexChanging"
                    BorderWidth="1px" DataKeyNames="F_UserID" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="false" PageSize="10"  RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
                    <Columns>
                    <asp:TemplateField ControlStyle-Width="30" HeaderText="ѡ��"    >
                                <ItemTemplate>
                                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
		<asp:BoundField DataField="F_UserName" HeaderText="�û���" SortExpression="F_UserName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_PassWord" HeaderText="����" SortExpression="F_PassWord" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_DepartID" HeaderText="��������" SortExpression="F_DepartID" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_RoleID" HeaderText="��ɫ��" SortExpression="F_RoleID" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Sex" HeaderText="�ձ�" SortExpression="F_Sex" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Birthday" HeaderText="����" SortExpression="F_Birthday" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Email" HeaderText="����" SortExpression="F_Email" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_MobilePhone" HeaderText="�ƶ��绰" SortExpression="F_MobilePhone" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_RegTime" HeaderText="ע��ʱ��" SortExpression="F_RegTime" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_LastInTime" HeaderText="����¼ʱ��" SortExpression="F_LastInTime" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_IsUsed" HeaderText="F_IsUsed" SortExpression="F_IsUsed" ItemStyle-HorizontalAlign="Center"  /> 
                            
                            <asp:HyperLinkField HeaderText="��ϸ" ControlStyle-Width="50" DataNavigateUrlFields="F_UserID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                                Text="��ϸ"  />
                            <asp:HyperLinkField HeaderText="�༭" ControlStyle-Width="50" DataNavigateUrlFields="F_UserID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                                Text="�༭"  />
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="ɾ��"   Visible="false"  >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="ɾ��"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
               <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
                <tr>
                    <td style="width: 1px;">                        
                    </td>
                    <td align="left">
                        <asp:Button ID="btnDelete" runat="server" Text="ɾ��" OnClick="btnDelete_Click"/>                       
                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
