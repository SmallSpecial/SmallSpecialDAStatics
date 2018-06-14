<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLog.aspx.cs" Inherits="WSS.Web.Admin.SysManage.SysLog" %>

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
    var trimhtml = function(value) {
        return Ext.util.Format.ellipsis(Ext.util.Format.stripTags(value), 35, true);
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
        object F_Value = e.NewValues["F_Note"];
        if (F_Value == null || F_Value.ToString().Length == 0)
        {
            e.Cancel = true;
            this.cancel = true;
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
        SelectCommand="SELECT [F_ID], [F_UserID], [F_UserName], [F_Note], [F_URL], [F_DateTime] FROM [T_SysLog]">
    </asp:SqlDataSource>
    <ext:Store ID="Store1" runat="server" DataSourceID="SqlDataSource1" OnAfterAjaxEvent="Store1_AfterAjaxEvent"
        OnBeforeAjaxEvent="Store1_BeforeAjaxEvent" UseIdConfirmation="true" OnBeforeRecordInserted="Store1_BeforeRecordInserted"
        OnAfterRecordInserted="Store1_AfterRecordInserted" OnRefreshData="Store1_RefershData"
        DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？" DirtyWarningTitle="系统警告">
        <Reader>
            <ext:JsonReader ReaderID="F_ID">
                <Fields>
                    <ext:RecordField Name="F_ID" />
                    <ext:RecordField Name="F_UserID" />
                    <ext:RecordField Name="F_UserName" />
                    <ext:RecordField Name="F_Note" />
                    <ext:RecordField Name="F_URL" />
                    <ext:RecordField Name="F_DateTime" Type="Date" DateFormat="Y-m-dTh:i:s" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_ID" Direction="desc" />
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
                    <ext:Panel ID="Panel1" runat="server" Title="描述" Height="70" BodyStyle="padding: 5px;"
                        Frame="true" Icon="Information">
                        <Body>
                            日志管理
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel ID="Panel2" runat="server" Height="300" Header="false">
                        <Body>
                            <ext:FitLayout ID="FitLayout1" runat="server">
                                <ext:GridPanel ID="GridPanel1" runat="server" Title="日志管理" AutoExpandColumn="F_Note"
                                    StoreID="Store1" Border="false" Icon="VcardEdit" SelectionMemory="Disabled" TrackMouseOver="True">
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="F_ID" DataIndex="F_ID" Header="编号" Width="20px">
                                            </ext:Column>
                                            <ext:Column DataIndex="F_UserName" Header="用户名" Width="40px">
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Note" Header="操作内容">
                                                <Renderer Fn="trimhtml" />
                                            </ext:Column>
                                                <ext:Column DataIndex="F_URL" Header="URL地址" Width="40px">
                                            </ext:Column>
                                            <ext:Column DataIndex="F_DateTime" Header="操作时间" Width="30px">
                                                <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" ForceFit="true" />
                                    </View>
                                    <Plugins>
                                        <ext:RowExpander ID="RowExpander1" runat="server" Collapsed="true">
                                            <Template ID="Template1" runat="server">
                       <hr /> <p><b>用户:</b> {F_UserName}</p><br/>
                        <p><b>时间:</b> {F_DateTime}</p><br/>
                        <p><b>内容:</b></p><p> {F_Note}</p><hr />
                                            </Template>
                                        </ext:RowExpander>
                                        <ext:GridFilters runat="server" ID="GridFilters1">
                                            <Filters>
                                                <ext:NumericFilter DataIndex="F_ID" />
                                                <ext:StringFilter DataIndex="F_UserName" />
                                                <ext:StringFilter DataIndex="F_Note" />
                                                <ext:DateFilter DataIndex="F_DateTime">
                                                    <DatePickerOptions runat="server" TodayText="当天">
                                                    </DatePickerOptions>
                                                </ext:DateFilter>
                                            </Filters>
                                        </ext:GridFilters>
                                    </Plugins>
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
