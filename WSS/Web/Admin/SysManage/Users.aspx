<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WSS.Web.Admin.SysManage.Users" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>

<script language="javascript">
    var isdodelete = function() {
    Ext.MessageBox.confirm('提示', '是否要删除这些记录', function(btn) { if (btn == "yes") { GridPanel1.deleteSelected(); if (!GridPanel1.hasSelection()) { btnDelete.disable(); } } });
    
    }
</script>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool cancel;
    private string message;

    protected void Store1_BeforeRecordInserted(object sender, BeforeRecordInsertedEventArgs e)
    {
        object F_Value = e.NewValues["F_ID"];
        if (F_Value == null || F_Value.ToString().Length == 0)
        {
            e.Cancel = true;
            this.cancel = true;
            this.message = "值不能为空";
            return;
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
        DeleteCommand="DELETE FROM [T_Users] WHERE [F_UserID] = @F_UserID" InsertCommand="INSERT INTO [T_Users] ([F_UserName], [F_PassWord], [F_DepartID], [F_RoleID], [F_Sex], [F_Birthday], [F_Email], [F_MobilePhone],[F_TelPhone], [F_RegTime], [F_LastInTime], [F_IsUsed], [F_RealName], [F_Note]) VALUES (@F_UserName, @F_PassWord, @F_DepartID, @F_RoleID, @F_Sex, @F_Birthday, @F_Email, @F_MobilePhone,@F_TelPhone, @F_RegTime, @F_LastInTime, @F_IsUsed, @F_RealName, @F_Note)"
        SelectCommand="SELECT [F_UserID], [F_UserName], [F_PassWord], [F_DepartID], [F_RoleID], [F_Sex], [F_Birthday], [F_Email], [F_MobilePhone], [F_RegTime], [F_LastInTime], [F_IsUsed], [F_RealName], [F_Note] FROM [T_Users]"
        UpdateCommand="UPDATE [T_Users] SET [F_UserName] = @F_UserName, [F_PassWord] = @F_PassWord, [F_DepartID] = @F_DepartID, [F_RoleID] = @F_RoleID, [F_Sex] = @F_Sex, [F_Birthday] = @F_Birthday, [F_Email] = @F_Email, [F_MobilePhone] = @F_MobilePhone,[F_TelPhone]=@F_TelPhone, [F_RegTime] = @F_RegTime, [F_LastInTime] = @F_LastInTime, [F_IsUsed] = @F_IsUsed, [F_RealName] = @F_RealName, [F_Note] = @F_Note WHERE [F_UserID] = @F_UserID">
        <DeleteParameters>
            <asp:Parameter Name="F_UserID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="F_UserName" Type="String" />
            <asp:Parameter Name="F_PassWord" Type="String" />
            <asp:Parameter Name="F_DepartID" Type="Int32" />
            <asp:Parameter Name="F_RoleID" Type="Int32" />
            <asp:Parameter Name="F_Sex" Type="Boolean" />
            <asp:Parameter Name="F_Birthday" Type="DateTime" />
            <asp:Parameter Name="F_Email" Type="String" />
            <asp:Parameter Name="F_MobilePhone" Type="String" />
            <asp:Parameter Name="F_TelPhone" Type="String" />
            <asp:Parameter Name="F_RegTime" Type="DateTime" />
            <asp:Parameter Name="F_LastInTime" Type="DateTime" />
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_RealName" Type="String" />
            <asp:Parameter Name="F_Note" Type="String" />
            <asp:Parameter Name="F_UserID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="F_UserName" Type="String" />
            <asp:Parameter Name="F_PassWord" Type="String" />
            <asp:Parameter Name="F_DepartID" Type="Int32" />
            <asp:Parameter Name="F_RoleID" Type="Int32" />
            <asp:Parameter Name="F_Sex" Type="Boolean" />
            <asp:Parameter Name="F_Birthday" Type="DateTime" />
            <asp:Parameter Name="F_Email" Type="String" />
            <asp:Parameter Name="F_MobilePhone" Type="String" />
            <asp:Parameter Name="F_TelPhone" Type="String" />
            <asp:Parameter Name="F_RegTime" Type="DateTime" />
            <asp:Parameter Name="F_LastInTime" Type="DateTime" />
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_RealName" Type="String" />
            <asp:Parameter Name="F_Note" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <ext:Store ID="Store1" runat="server" DataSourceID="SqlDataSource1" OnAfterAjaxEvent="Store1_AfterAjaxEvent"
        OnBeforeAjaxEvent="Store1_BeforeAjaxEvent" UseIdConfirmation="true" OnBeforeRecordInserted="Store1_BeforeRecordInserted"
        OnAfterRecordInserted="Store1_AfterRecordInserted" OnRefreshData="Store1_RefershData"
        DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？" DirtyWarningTitle="系统警告">
        <Reader>
            <ext:JsonReader ReaderID="F_UserID">
                <Fields>
                    <ext:RecordField Name="F_UserID" />
                    <ext:RecordField Name="F_UserName" Type="String" />
                    <ext:RecordField Name="F_PassWord" Type="String" />
                    <ext:RecordField Name="F_DepartID" Type="Int" />
                    <ext:RecordField Name="F_RoleID" Type="Int" />
                    <ext:RecordField Name="F_RealName" />
                    <ext:RecordField Name="F_Note" />
                    <ext:RecordField Name="F_Sex" Type="Boolean" />
                    <ext:RecordField Name="F_Birthday" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    <ext:RecordField Name="F_Email" Type="String" />
                    <ext:RecordField Name="F_MobilePhone" Type="String" />
                    <ext:RecordField Name="F_TelPhone" Type="String" />
                    <ext:RecordField Name="F_RegTime" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    <ext:RecordField Name="F_LastInTime" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    <ext:RecordField Name="F_IsUsed" Type="Boolean" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_UserID" Direction="ASC" />
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
                        Frame="true" Icon="Information" AutoHeight="True">
                        <Body>
                           用户管理
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel ID="Panel2" runat="server" Height="300" Header="false">
                        <Body>
                            <ext:FitLayout ID="FitLayout1" runat="server">
                                <ext:GridPanel ID="GridPanel1" runat="server" Title="用户管理" AutoExpandColumn="F_Note"
                                    StoreID="Store1" Border="false" Icon="UserB" SelectionMemory="Disabled" TrackMouseOver="True"
                                    ContextMenuID="Menu2">
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="F_UserID" DataIndex="F_UserID" Header="编号">
                                                <Editor>
                                                    <ext:TextField ID="TextField1" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_UserName" Header="用户名">
                                                <Editor>
                                                    <ext:TextField ID="TextField2" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_PassWord" Header="密码">
                                                <Editor>
                                                    <ext:TextField ID="TextField3" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_DepartID" Header="所属部门">
                                                <Editor>
                                                    <ext:TextField ID="TextField6" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_RoleID" Header="所属角色">
                                                <Editor>
                                                    <ext:TextField ID="TextField7" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_RealName" Header="真实姓名">
                                                <Editor>
                                                    <ext:TextField ID="TextField8" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Note" Header="备注">
                                                <Editor>
                                                    <ext:TextField ID="TextField5" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Sex" Header="性别">
                                                <Editor>
                                                    <ext:TextField ID="TextField9" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Birthday" Header="生日">
                                                <Editor>
                                                    <ext:TextField ID="TextField10" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Email" Header="电子邮箱">
                                                <Editor>
                                                    <ext:TextField ID="TextField11" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_MobilePhone" Header="移动电话">
                                                <Editor>
                                                    <ext:TextField ID="TextField12" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_TelPhone" Header="固定电话">
                                                <Editor>
                                                    <ext:TextField ID="TextField16" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_RegTime" Header="注册时间">
                                                <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d h:i:s')" />
                                                <Editor>
                                                    <ext:TextField ID="TextField13" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_LastInTime" Header="最后登录时间">
                                                <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d h:i:s')" />
                                                <Editor>
                                                    <ext:TextField ID="TextField14" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn DataIndex="F_IsUsed" Header="是否使用">
                                                <Editor>
                                                    <ext:Checkbox ID="TextField4" runat="server" FieldLabel="是否使用" Checked="True" />
                                                </Editor>
                                            </ext:CheckColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" >
                                                                        <Listeners>
                                    <RowSelect Handler="#{btnDelete}.enable();" />
                                    <RowDeselect Handler="if (!#{GridPanel1}.hasSelection()) {#{btnDelete}.disable();}" />
                                </Listeners>
</ext:RowSelectionModel>
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
                            <ext:Button ID="btnDelete" runat="server" Text="删除选中行" Icon="Delete" Disabled="True">
                                <Listeners>
                                    <Click Handler="isdodelete();" />
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
        <ext:Menu ID="Menu2" runat="server">
        <Items>
                    <ext:MenuItem ID="MenuItem5" runat="server" Text="添加" Icon="Add">
                <Listeners>
                    <Click Handler="#{GridPanel1}.insertRecord(0, {});#{GridPanel1}.getView().focusRow(0);#{GridPanel1}.startEditing(0, 0);" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem2" runat="server" Text="刷新数据" Icon="DatabaseRefresh">
                <Listeners>
                    <Click Handler="#{GridPanel1}.reload();" />
                </Listeners>
            </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem3" runat="server" Text="保存" Icon="Disk">
                <Listeners>
                    <Click Handler="#{GridPanel1}.save();" />
                </Listeners>
            </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem4" runat="server" Text="删除选中行" Icon="Delete">
                <Listeners>
                    <Click Handler="isdodelete();" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    </form>
</body>
</html>
