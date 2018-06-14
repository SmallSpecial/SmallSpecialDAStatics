<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="WSS.Web.Admin.SysManage.Roles" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WSS.DBUtility" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>

<script language="javascript">
    var isdodelete = function() {
        Ext.MessageBox.confirm('提示', '是否要删除这些记录', function(btn) { if (btn == "yes") { GridPanel1.deleteSelected(); } });
    }
</script>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Bindmenu();

    }
    
    private void Bindmenu()
    {
        string sql = "SELECT F_MenuID, F_Name, F_URL, F_ParentID, F_IsUsed, F_Sort FROM T_Menus WHERE (F_URL is not NULL) ORDER BY F_MenuID";
        DataSet ds = DbHelperSQL.Query(sql);
        if (ds != null && ds.Tables != null)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Coolite.Ext.Web.ListItem li = new Coolite.Ext.Web.ListItem(dr["F_Name"].ToString(), dr["F_MenuID"].ToString());
                MultiSelect1.Items.Add(li);
            }
        }
    }

    private bool cancel;
    private string message;

    protected void Store1_BeforeRecordInserted(object sender, BeforeRecordInsertedEventArgs e)
    {
        //object F_Value = e.NewValues;
        object F_Value = e.NewValues["F_Note"];
        if (F_Value == null || F_Value.ToString().Length == 0)
        {
            e.Cancel = true;
            //  this.cancel = true;
            this.message = "值不能为空";
        }
    }

    protected void Store1_AfterRecordInserted(object sender, AfterRecordInsertedEventArgs e)
    {
        //The deleted and updated records confirms automatic (depending AffectedRows field)
        //But you can override this in AfterRecordUpdated and AfterRecordDeleted event
        //For insert we should set new id for refresh on client
        //If we don't set new id then old id will be used
        if (e.Confirmation.Confirm && !string.IsNullOrEmpty(insertedValue))
        {
            e.Confirmation.ConfirmRecord(insertedValue);
            insertedValue = "";
        }
    }

    private string insertedValue;
    protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        //use e.AffectedRows for ensure success action. The store read this value and set predefined Confirm depend on e.AffectedRows
        //The Confirm can be granted or denied in OnRecord....ed event
        insertedValue = e.Command.Parameters["@newId"].Value != null
                            ? e.Command.Parameters["@newId"].Value.ToString()
                            : "";
    }

    protected void Store1_AfterAjaxEvent(object sender, AfterAjaxEventArgs e)
    {
        if (e.Response.Success)
        {
            // set to .Success to false if we want to return a failure
            e.Response.Success = !cancel;
            e.Response.Msg = message;
        }
    }

    protected void Store1_BeforeAjaxEvent(object sender, BeforeAjaxEventArgs e)
    {
        //string emulError = e.Parameters["EmulateError"];
        //if (emulError == "1")
        //{
        //    throw new Exception("Emulating error");
        //}
    }

    protected void Store1_RefershData(object sender, StoreRefreshDataEventArgs e)
    {
        this.Store1.DataBind();
    }
</script>

<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WebServeDBConnectionString %>"
        DeleteCommand="DELETE FROM [T_Roles] WHERE [F_RoleID] = @F_RoleID" InsertCommand="INSERT INTO [T_Roles] ([F_IsUsed], [F_Power], [F_RoleName]) VALUES (@F_IsUsed, @F_Power, @F_RoleName)"
        SelectCommand="SELECT [F_RoleID], [F_IsUsed], [F_Power], [F_RoleName] FROM [T_Roles]"
        UpdateCommand="UPDATE [T_Roles] SET [F_IsUsed] = @F_IsUsed, [F_Power] = @F_Power, [F_RoleName] = @F_RoleName WHERE [F_RoleID] = @F_RoleID">
        <DeleteParameters>
            <asp:Parameter Name="F_RoleID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_Power" Type="String" />
            <asp:Parameter Name="F_RoleName" Type="String" />
            <asp:Parameter Name="F_RoleID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_Power" Type="String" />
            <asp:Parameter Name="F_RoleName" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <ext:Store ID="Store1" runat="server" DataSourceID="SqlDataSource1" OnAfterAjaxEvent="Store1_AfterAjaxEvent"
        OnBeforeAjaxEvent="Store1_BeforeAjaxEvent" UseIdConfirmation="true" OnBeforeRecordInserted="Store1_BeforeRecordInserted"
        OnAfterRecordInserted="Store1_AfterRecordInserted" OnRefreshData="Store1_RefershData"
        DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？" DirtyWarningTitle="系统警告">
        <Reader>
            <ext:JsonReader ReaderID="F_RoleID">
                <Fields>
                    <ext:RecordField Name="F_RoleID" Type="int" />
                    <ext:RecordField Name="F_IsUsed" Type="Boolean" />
                    <ext:RecordField Name="F_Power" Type="String" />
                    <ext:RecordField Name="F_RoleName" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_RoleID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('加载失败', e.message )" />
            <CommitFailed Handler="Ext.Msg.alert('提交失败', '原因: ' + msg)" />
            <SaveException Handler="Ext.Msg.alert('保存失败', e.message)" />
            <CommitDone Handler="Ext.Msg.alert('提交成功', '数据保存成功');" />
        </Listeners>
    </ext:Store>
    <ext:ViewPort ID="ViewPort1" runat="server" ContextMenuID="Menu1">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel ID="Panel1" runat="server" Title="描述" Height="60" BodyStyle="padding: 5px;"
                        Frame="true" Icon="Information">
                        <Body>
                           角色管理
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel ID="Panel2" runat="server" Height="300" Header="false">
                        <Body>
                            <ext:FitLayout ID="FitLayout1" runat="server">
                                <ext:GridPanel ID="GridPanel1" runat="server" Title="角色管理" AutoExpandColumn="F_Power"
                                    StoreID="Store1" Border="false" Icon="Group" SelectionMemory="Disabled" TrackMouseOver="True">
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="F_RoleID" DataIndex="F_RoleID" Header="编号">
                                            </ext:Column>
                                            <ext:Column DataIndex="F_RoleName" Header="角色名">
                                                <Editor>
                                                    <ext:TextField ID="TextField2" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Power" Header="权限">
                                                <Editor>
                                                    <ext:MultiSelect ID="MultiSelect1" runat="server" Width="300" Height="250">
                                          <%--              <Items>
                                                            <ext:ListItem Text="Item 1" Value="1" />
                                                        </Items>--%>
                                                    </ext:MultiSelect>
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn DataIndex="F_IsUsed" Header="是否使用">
                                                <Editor>
                                                    <ext:Checkbox ID="TextField5" runat="server" FieldLabel="是否使用" />
                                                </Editor>
                                            </ext:CheckColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                                    </SelectionModel>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolBar1" runat="server" PageSize="20" StoreID="Store1"
                                            DisplayInfo="false" />
                                    </BottomBar>
                                    <SaveMask ShowMask="true" />
                                    <LoadMask ShowMask="true" />
                                </ext:GridPanel>
                            </ext:FitLayout>
                        </Body>
                        <Buttons>
                            <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Disk">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.save();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnDelete" runat="server" Text="删除选中行" Icon="Delete">
                                <Listeners>
                                    <Click Handler="isdodelete()" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnInsert" runat="server" Text="添加" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.insertRecord(0, {});#{GridPanel1}.getView().focusRow(0);#{GridPanel1}.startEditing(0, 0);" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnRefresh" runat="server" Text="刷新" Icon="ArrowRefresh">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.reload();" />
                                </Listeners>
                            </ext:Button>
                        </Buttons>
                    </ext:Panel>
                </Center>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem1" runat="server" Text="刷新页面" Icon="PageRefresh">
                <Listeners>
                    <Click Handler="location.reload();" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    </form>
</body>
</html>
