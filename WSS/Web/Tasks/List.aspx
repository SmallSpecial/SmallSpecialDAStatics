<%@ Page Title="ϵͳ��־��" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WSS.Web.Tasks.List" %>
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
                    BorderWidth="1px" DataKeyNames="F_ID" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="false" PageSize="10"  RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
                    <Columns>
                    <asp:TemplateField ControlStyle-Width="30" HeaderText="ѡ��"    >
                                <ItemTemplate>
                                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
		<asp:BoundField DataField="F_ID" HeaderText="F_ID" SortExpression="F_ID" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Title" HeaderText="F_Title" SortExpression="F_Title" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Note" HeaderText="F_Note" SortExpression="F_Note" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_From" HeaderText="F_From" SortExpression="F_From" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Type" HeaderText="F_Type" SortExpression="F_Type" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_JinjiLevel" HeaderText="F_JinjiLevel" SortExpression="F_JinjiLevel" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_GameName" HeaderText="F_GameName" SortExpression="F_GameName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_GameZone" HeaderText="F_GameZone" SortExpression="F_GameZone" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_GUserID" HeaderText="F_GUserID" SortExpression="F_GUserID" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_GRoleName" HeaderText="F_GRoleName" SortExpression="F_GRoleName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Tag" HeaderText="F_Tag" SortExpression="F_Tag" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_DateTime" HeaderText="F_DateTime" SortExpression="F_DateTime" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_State" HeaderText="F_State" SortExpression="F_State" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_Telphone" HeaderText="F_Telphone" SortExpression="F_Telphone" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="F_DutyMan" HeaderText="F_DutyMan" SortExpression="F_DutyMan" ItemStyle-HorizontalAlign="Center"  /> 
                            
                            <asp:HyperLinkField HeaderText="��ϸ" ControlStyle-Width="50" DataNavigateUrlFields="F_ID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                                Text="��ϸ"  />
                            <asp:HyperLinkField HeaderText="�༭" ControlStyle-Width="50" DataNavigateUrlFields="F_ID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
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
