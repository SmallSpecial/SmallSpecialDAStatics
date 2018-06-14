<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameConfigTools.aspx.cs"
    Inherits="WSS.Web.Admin.GameTool.GameConfigTools" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .search-item
        {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
        }
        .dot-label
        {
            font-weight: normal;
            font-size: 11px;
            color: Gray;
        }
        .ceshi
        {
            background-color: Red;
        }
        .list-itemgamezone
        {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
        }
        h3
        {
            display: block;
            font: inherit;
            font-weight: bold;
            color: #222;
        }
        .icon-exclamation
        {
            padding-left: 25px !important;
            background: url(/icons/exclamation-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .icon-accept
        {
            padding-left: 25px !important;
            background: url(/icons/accept-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .x-bluebg
        {
            background-color: #dfe8f6;
        }
    </style>
</head>

<script language="javascript">
    var isdodelete = function(grid) {
        Ext.MessageBox.confirm('提示', '是否要删除这些记录', function(btn) { if (btn == "yes") { grid.deleteSelected(); } });
    }
    var getName = function(value) {
        //        var r = Store1.getById(value);
        if (Ext.isEmpty(value)) {
            return "顶级";
        }
        //        

        GridPanel1.startEditing(0, 2);
        cbF_ZoneState.setValue(value);
        return cbF_ZoneState.getRawValue();

    }
    var getZoneState
    //刷新STORE,本地分页时用
    function refreshstore(storei, pagintoolbar) {

        if (storei.warningOnDirty && storei.isDirty() && !storei.silentMode) {
            storei.silentMode = false;
            Ext.MessageBox.confirm(
                storei.dirtyWarningTitle,
                storei.dirtyWarningText,
                function(btn, text) {
                    if (btn == "yes") {
                        storei.reload(true); pagintoolbar.changePage(0);
                    }
                }
            );
        }
        else {
            storei.reload(true);
            pagintoolbar.changePage(0);
        }
        
    }

    function validData(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......
            if (Ext.isEmpty(rs[i].data.F_ZoneName)) {
                grid.startEditing(i, 1);
                Ext.ex
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_ZoneState)) {
                grid.startEditing(i, 2);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_ZoneLine)) {
                grid.startEditing(i, 3);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneAttrib)) {
                grid.startEditing(i, 4);
                return false;

            }
            if (Ext.isEmpty(rs[i].data.F_ChargeType)) {
                grid.startEditing(i, 5);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_CurVersion)) {
                grid.startEditing(i, 6);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_BigZoneID)) {
                grid.startEditing(i, 7);
                return false;
            }
        }
        return true;
    }
    function validData2(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......
            if (Ext.isEmpty(rs[i].data.F_Name)) {
                grid.startEditing(i, 1);
                Ext.ex
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_ZoneID)) {
                grid.startEditing(i, 2);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_MaxUser)) {
                grid.startEditing(i, 3);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_Ip)) {
                grid.startEditing(i, 4);
                return false;

            }
            if (Ext.isEmpty(rs[i].data.F_Port)) {
                grid.startEditing(i, 5);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_State)) {
                grid.startEditing(i, 6);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_DBISID)) {
                grid.startEditing(i, 7);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_PingPort)) {
                grid.startEditing(i, 7);
                return false;
            }
        }
        return true;
    }

    function validData3(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......
            if (Ext.isEmpty(rs[i].data.F_IP)) {
                grid.startEditing(i, 1);
                Ext.ex
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_GSNAME)) {
                grid.startEditing(i, 2);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_NGSID)) {
                grid.startEditing(i, 3);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_ZONEID)) {
                grid.startEditing(i, 4);
                return false;

            }
            if (Ext.isEmpty(rs[i].data.F_GSSceneID)) {
                grid.startEditing(i, 5);
                return false;
            }
        }
        return true;
    } 
</script>

<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None" />
    <!--store相关-->
    <ext:Store runat="server" ID="StoreBattleZone" DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？"
        DirtyWarningTitle="系统警告" RemotePaging="false">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="BattleZones.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="BattleZonesSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="15" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_ZoneID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_ZoneID" Type="Int" />
                    <ext:RecordField Name="F_ZoneName" />
                    <ext:RecordField Name="F_ZoneState" />
                    <ext:RecordField Name="F_ZoneLine" />
                    <ext:RecordField Name="F_ZoneAttrib" />
                    <ext:RecordField Name="F_ChargeType" />
                    <ext:RecordField Name="F_CurVersion" />
                    <ext:RecordField Name="F_BigZoneID" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_ZoneID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('加载失败', e.message )" />
            <CommitFailed Handler="Ext.Msg.alert('提交失败', '原因: ' + msg)" />
            <SaveException Handler="Ext.Msg.alert('保存失败', e.message)" />
            <CommitDone Handler="Ext.Msg.alert('提交成功', '数据保存成功');" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreBattleLine" DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？"
        DirtyWarningTitle="系统警告" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="BattleLines.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="BattleLinesSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="15" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_NGSID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_NGSID" Type="Int" />
                    <ext:RecordField Name="F_Name" />
                    <ext:RecordField Name="F_ZoneID" />
                    <ext:RecordField Name="F_MaxUser" />
                    <ext:RecordField Name="F_Ip" />
                    <ext:RecordField Name="F_Port" />
                    <ext:RecordField Name="F_State" />
                    <ext:RecordField Name="F_DBISID" />
                    <ext:RecordField Name="F_PingPort" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_NGSID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('加载失败', e.message )" />
            <CommitFailed Handler="Ext.Msg.alert('提交失败', '原因: ' + msg)" />
            <SaveException Handler="Ext.Msg.alert('保存失败', e.message)" />
            <CommitDone Handler="Ext.Msg.alert('提交成功', '数据保存成功');" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameServer" DirtyWarningText="数据发生改变您未保存。您确定要加载/刷新数据？"
        DirtyWarningTitle="系统警告" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="GameServers.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="GameServersSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="15" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_GSID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_GSID" Type="Int" />
                    <ext:RecordField Name="F_IP" />
                    <ext:RecordField Name="F_GSNAME" />
                    <ext:RecordField Name="F_NGSID" />
                    <ext:RecordField Name="F_ZONEID" />
                    <ext:RecordField Name="F_CampID" />
                    <ext:RecordField Name="F_GSSceneID" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_GSID" Direction="ASC" />
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
                           双击单元格可进入编辑状态,如果输入有误会有红色边框或波浪线显示,鼠标移至上面会有原因提示.
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 5 5">
                    <ext:TabPanel ID="TabPanel1" runat="server">
                        <Tabs>
                            <ext:Tab ID="Tab1" runat="server" Title="战区配置" BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout2" runat="server">
                                        <ext:Panel ID="Panel2" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout1" runat="server">
                                                    <ext:GridPanel ID="GridPanel1" runat="server" Title="战区配置" AutoExpandColumn="F_ZoneName"
                                                        StoreID="StoreBattleZone" Border="false" Icon="ComputerWrench" SelectionMemory="Disabled"
                                                        TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel1" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_ZoneID" DataIndex="F_ZoneID" Header="战区编号">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneName" Header="战区名称">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField2" runat="server" MaxLength="12" AllowBlank="False" MinLength="2" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneState" Header="战区状态">
                                                                    <Renderer Handler=" if(value==0){return '关闭';}if(value==1){return '开放';}if(value==2){return '测试';} " />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="cbF_ZoneState" runat="server" Editable="false" ReadOnly="True"
                                                                            AllowBlank="False" SelectOnFocus="True" ForceSelection="True">
                                                                            <%--     <SelectedItem Value="0" />--%>
                                                                            <Items>
                                                                                <ext:ListItem Text="关闭" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="开放" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="测试" Value="2" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneLine" Header="网络线路">
                                                                    <Renderer Handler="switch(value) {case '0':return '网通';case '1':return '电信';case '2':return '双线';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox1" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="网通" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="电信" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="双线" Value="2" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneAttrib" Header="战区属性">
                                                                    <Renderer Handler="switch(value) {case '0':return '新开';case '1':return '推荐';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox2" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="新开" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="推荐" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ChargeType" Header="收费类型">
                                                                    <Renderer Handler="switch(value) {case '0':return '免费';case '1':return '收费';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox3" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="免费" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="收费" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_CurVersion" Header="当前版本">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField3" runat="server" MaxLength="32" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_BigZoneID" Header="所属大区">
                                                                    <Renderer Handler="switch(value) {case '0':return '第一大区';case '1':return '第二大区';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox4" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="第一大区" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="第二大区" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar1" runat="server" PageSize="15" StoreID="StoreBattleZone"
                                                                bufferResize="true" DisplayInfo="true" DisplayMsg="显示项 {0} - {1} of {2}" EmptyMsg="没有信息可显示" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                        <Listeners>
                                                            <Show Handler="#{GridPanel1}.stopEditing(true);" />
                                                        </Listeners>
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData(#{GridPanel1})){#{GridPanel1}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnDelete" runat="server" Text="删除选中行" Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel1})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnInsert" runat="server" Text="添加" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel1}.insertRecord(0, {});#{GridPanel1}.getView().focusRow(0);#{GridPanel1}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnRefresh" runat="server" Text="刷新" Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreBattleZone,PagingToolBar1);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab2" runat="server" Title="战单条配置" BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout3" runat="server">
                                        <ext:Panel ID="Panel3" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout4" runat="server">
                                                    <ext:GridPanel ID="GridPanel2" runat="server" Title="战单条配置" AutoExpandColumn="F_Name"
                                                        StoreID="StoreBattleLine" Border="false" Icon="FolderWrench" SelectionMemory="Disabled"
                                                        TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel2" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_NGSID" DataIndex="F_NGSID" Header="网关编号">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Name" Header="单线名称">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField1" runat="server" MaxLength="15" AllowBlank="False" MinLength="2" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneID" Header="所属战区">
                                                                    <Renderer Handler=" var r = StoreBattleZone.getById(value);if(Ext.isEmpty(r)){ return '未知'}return r.data.F_ZoneName;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox5" StoreID="StoreBattleZone" DisplayField="F_ZoneName"
                                                                            ValueField="F_ZoneID" runat="server" Editable="false" ReadOnly="True" AllowBlank="False"
                                                                            SelectOnFocus="True" ForceSelection="True" Mode="Local">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_MaxUser" Header="最大允许在线人数">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField7" runat="server" MaxLength="10" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Ip" Header="网关服务器IP">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField8" runat="server" MaxLength="26" Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Port" Header="网关服务器端口">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField9" runat="server" MaxLength="6" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_State" Header="当前状态">
                                                                    <Renderer Handler="switch(value) {case '0':return '关闭';case '1':return '开启';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox6" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="关闭" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="开启" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_DBISID" Header="连接DBIS的编号">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField4" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_PingPort" Header="PING的端口">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField10" runat="server" MaxLength="6" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="15" StoreID="StoreBattleLine"
                                                                DisplayInfo="true" DisplayMsg="显示项 {0} - {1} of {2}" EmptyMsg="没有信息可显示" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button1" runat="server" Text="保存" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData2(#{GridPanel2})){#{GridPanel2}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button2" runat="server" Text="删除选中行" Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel2})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button3" runat="server" Text="添加" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel2}.insertRecord(0, {});#{GridPanel2}.getView().focusRow(0);#{GridPanel2}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button4" runat="server" Text="刷新" Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreBattleLine,PagingToolBar2);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab3" runat="server" Title="GameServer配置" BodyStyle="padding: 1px;"
                                BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout5" runat="server">
                                        <ext:Panel ID="Panel4" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout6" runat="server">
                                                    <ext:GridPanel ID="GridPanel3" runat="server" Title="GameServer配置" AutoExpandColumn="F_GSNAME"
                                                        StoreID="StoreGameServer" Border="false" Icon="FolderWrench" SelectionMemory="Disabled"
                                                        TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel3" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_GSID" DataIndex="F_GSID" Header="GS编号">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_IP" Header="GS的IP地址">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField5" runat="server" MaxLength="26" AllowBlank="False" MinLength="2" Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_GSNAME" Header="GS名称">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField11" runat="server" MaxLength="15" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_NGSID" Header="连接的NGS">
                                                                    <Renderer Handler=" var r = StoreBattleLine.getById(value);if(Ext.isEmpty(r)){ return '未知'}return r.data.F_Name;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox11" StoreID="StoreBattleLine" DisplayField="F_Name" ValueField="F_NGSID"
                                                                            runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZONEID" Header="所属战区">
                                                                    <Renderer Handler=" var r = StoreBattleZone.getById(value);if(Ext.isEmpty(r)){ return '未知'}return r.data.F_ZoneName;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox12" runat="server" StoreID="StoreBattleZone" DisplayField="F_ZoneName"
                                                                            ValueField="F_ZoneID" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_CampID" Header="阵营(国家)">
                                                                    <Renderer Handler="switch(value) {case '0':return '国家一';case '1':return '国家二';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox13" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="国家一" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="国家二" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_GSSceneID" Header="GS场景编号" Width="250px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField6" runat="server" MaxLength="400" AllowBlank="False" Regex="^(\d+\,)+$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar3" runat="server" PageSize="15" StoreID="StoreGameServer"
                                                                DisplayInfo="true" DisplayMsg="显示项 {0} - {1} of {2}" EmptyMsg="没有信息可显示" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button5" runat="server" Text="保存" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData3(#{GridPanel3})){#{GridPanel3}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button6" runat="server" Text="删除选中行" Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel13})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button7" runat="server" Text="添加" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel3}.insertRecord(0, {});#{GridPanel3}.getView().focusRow(0);#{GridPanel3}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button8" runat="server" Text="刷新" Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreGameServer,PagingToolBar3);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
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
