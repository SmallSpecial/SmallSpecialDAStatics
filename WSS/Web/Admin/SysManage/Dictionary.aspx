﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="WSS.Web.Admin.SysManage.Dictionary" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script  language="javascript">
 var isdodelete =  function() {
Ext.MessageBox.confirm('提示', '是否要删除这些记录', function(btn) { if (btn == "yes") { GridPanel1.deleteSelected();} });
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
        object F_Value = e.NewValues["F_Value"];
        if (F_Value == null || F_Value.ToString().Length==0)
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
        DeleteCommand="DELETE FROM [T_Dictionary] WHERE [F_DicID] = @F_DicID" InsertCommand="INSERT INTO [T_Dictionary] ([F_DicID], [F_ParentID], [F_Value], [F_IsUsed], [F_Sort]) VALUES (@F_DicID, @F_ParentID, @F_Value, @F_IsUsed, @F_Sort)"
        SelectCommand="SELECT [F_DicID], [F_ParentID], [F_Value], [F_IsUsed], [F_Sort] FROM [T_Dictionary]"
        UpdateCommand="UPDATE [T_Dictionary] SET [F_ParentID] = @F_ParentID, [F_Value] = @F_Value, [F_IsUsed] = @F_IsUsed, [F_Sort] = @F_Sort WHERE [F_DicID] = @F_DicID">
        <DeleteParameters>
            <asp:Parameter Name="F_DicID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="F_ParentID" Type="Int32" />
            <asp:Parameter Name="F_Value" Type="String" />
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_Sort" Type="Int32" />
            <asp:Parameter Name="F_DicID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="F_DicID" Type="Int32" />
            <asp:Parameter Name="F_ParentID" Type="Int32" />
            <asp:Parameter Name="F_Value" Type="String" />
            <asp:Parameter Name="F_IsUsed" Type="Boolean" />
            <asp:Parameter Name="F_Sort" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <ext:Store ID="Store1" runat="server" DataSourceID="SqlDataSource1" OnAfterAjaxEvent="Store1_AfterAjaxEvent"
        OnBeforeAjaxEvent="Store1_BeforeAjaxEvent" UseIdConfirmation="true" OnBeforeRecordInserted="Store1_BeforeRecordInserted"
        OnAfterRecordInserted="Store1_AfterRecordInserted" OnRefreshData="Store1_RefershData" DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？" DirtyWarningTitle="系统警告">
        <Reader>
            <ext:JsonReader ReaderID="F_DicID">
                <Fields>
                    <ext:RecordField Name="F_DicID" />
                    <ext:RecordField Name="F_ParentID" />
                    <ext:RecordField Name="F_Value" />
                    <ext:RecordField Name="F_IsUsed" />
                    <ext:RecordField Name="F_Sort" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_DicID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('加载失败', e.message )" />
            <CommitFailed Handler="Ext.Msg.alert('提交失败', '原因: ' + msg)" />
            <SaveException Handler="Ext.Msg.alert('保存失败', e.message)" />
            <CommitDone Handler="Ext.Msg.alert('提交成功', '数据保存成功');" />
        </Listeners>
    </ext:Store>
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel runat="server" Title="描述" Height="60" BodyStyle="padding: 5px;" Frame="true"
                        Icon="Information">
                        <Body>
                            字典表管理
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel ID="Panel1" runat="server" Height="300" Header="false">
                        <Body>
                            <ext:FitLayout ID="FitLayout1" runat="server">
                                <ext:GridPanel ID="GridPanel1" runat="server" Title="字典表管理" AutoExpandColumn="F_Value"
                                    StoreID="Store1" Border="false" Icon="Images" SelectionMemory="Disabled"  TrackMouseOver="True">
                                    <ColumnModel runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="F_DicID" DataIndex="F_DicID" Header="编号">
                                                <Editor>
                                                    <ext:TextField ID="TextField1" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_Value" Header="值">
                                                <Editor>
                                                    <ext:TextField ID="TextField2" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column DataIndex="F_ParentID" Header="父编号">
                                                <Editor>
                                                    <ext:TextField ID="TextField3" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn  DataIndex="F_IsUsed" Header="是否使用">
                                                <Editor>
                                                    <ext:CheckBox  ID="TextField4" runat="server"  FieldLabel="是否使用" Checked="True" />
                                                </Editor>
                                            </ext:CheckColumn >
                                            <ext:Column DataIndex="F_Sort" Header="排序">
                                                <Editor>
                                                    <ext:TextField ID="TextField5" runat="server" Text="0" />
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel runat="server" />
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
    </form>
</body>
</html>
