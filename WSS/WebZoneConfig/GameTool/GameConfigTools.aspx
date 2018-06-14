<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameConfigTools.aspx.cs"
    Inherits="WebZoneConfig.GameTool.GameConfigTools" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>战区架设工具</title>
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

<script language="javascript" type="text/javascript">
    var isdodelete = function(grid) {
        Ext.MessageBox.confirm('<asp:Literal runat="server" Text="<%$Resources:Global,StringAlertTitle%>" />', '<asp:Literal runat="server" Text="<%$Resources:Global,StringIsToDelete%>" />', function(btn) { if (btn == "yes") { grid.deleteSelected(); } });
    }
    var getName = function(value) {
        //        var r = Store1.getById(value);
        if (Ext.isEmpty(value)) {
            return " ";
        }
        //        

        GridPanel1.startEditing(0, 2);
        cbF_ZoneState.setValue(value);
        return cbF_ZoneState.getRawValue();

    }
    var getZoneState
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

    var StringCommitFailed = '<asp:Literal runat="server" Text="<%$Resources:Global,StringCommitFailed%>" />';
    var StringCommitDoneT = '<asp:Literal runat="server" Text="<%$Resources:Global,StringCommitDoneT%>" />';
    var StringCommitDone = '<asp:Literal runat="server" Text="<%$Resources:Global,StringCommitDone%>" />';
    var StringLoadException = '<asp:Literal runat="server" Text="<%$Resources:Global,StringLoadException%>" />';
    var StringSaveException = '<asp:Literal runat="server" Text="<%$Resources:Global,StringSaveException%>" />';

    var StringZoneState0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneState0%>" />';
    var StringZoneState1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneState1%>" />';
    var StringZoneState2 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneState2%>" />';

    var StringZoneTestState = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneTestState%>" />';
    var StringZoneTestState0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneTestState0%>" />';

    var StringPressState0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringPressState0%>" />';
    var StringPressState1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringPressState1%>" />';
    var StringPressState2 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringPressState2%>" />';
    var StringPressState3 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringPressState3%>" />';

    var StringZoneLine0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneLine0%>" />';
    var StringZoneLine1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneLine1%>" />';
    var StringZoneLine2 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneLine2%>" />';

    var StringYes = '<asp:Literal runat="server" Text="<%$Resources:Global,StringYes%>" />';
    var StringNo = '<asp:Literal runat="server" Text="<%$Resources:Global,StringNo%>" />';

    var StringZoneAttrib21 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneAttrib21%>" />';
    var StringZoneAttrib22 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneAttrib22%>" />';
    var StringZoneAttrib23 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneAttrib23%>" />';
    var StringZoneAttrib20 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringZoneAttrib20%>" />';

    var StringChargeType0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringChargeType0%>" />';
    var StringChargeType1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringChargeType1%>" />';

    var StringBigZoneID0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringBigZoneID0%>" />';
    var StringBigZoneID1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringBigZoneID1%>" />';

    var StringLineState0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringLineState0%>" />';
    var StringLineState1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringLineState1%>" />';
    var StringLineState2 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringLineState2%>" />';
    var StringLineState3 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringLineState3%>" />';

    var StringCampID0 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringCampID0%>" />';
    var StringCampID1 = '<asp:Literal runat="server" Text="<%$Resources:Global,StringCampID1%>" />';

    var columnConfig = [];
    function getColumnIndex(columns, dataIndex) {//根据列匹配的filed获取列索引
        if (columnConfig.length == 0) {
            for (var i in columns) {
                if (columns[i].dataIndex == undefined) { continue; }
                columnConfig.push(columns[i].dataIndex + '=' + i);
            }
        }
        for (var i in columnConfig) {
            if (isNaN(i)) { continue; }
            var item = columnConfig[i].split('=');
            if (item[0] == dataIndex) {
                return item[1];
            }
        }
        return -1;
    }
    function validData(grid) {
        var columns = grid.colModel.config; 
        var rs = grid.getStore().data.items || [];//[.modified]需要定位到被修改的行的索引
        for (var i = 0; i < rs.length; i++) {
            var index = -1;
            var nameIndex = getColumnIndex(columns, "F_ZoneName");
            //下面可作出各种验证......
            var reg = /[^\x00-\xff]/g;
            var name = rs[i].data["F_ZoneName"];
            var eng = name.replace(reg, "");
            var len = eng.length + (name.length - eng.length) * 2;
            if (Ext.isEmpty(rs[i].data.F_ZoneName)) {//中文只能输入8个字符
                if (index != -1) {
                    grid.startEditing(i, nameIndex);
                    return false;
                }
            } else if (len >16) {
                if (reg.test(rs[i].data["F_ZoneName"])) {
                    grid.startEditing(i, nameIndex);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneState)) {
                index = getColumnIndex(columns, "F_ZoneState");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneLine)) {
                index = getColumnIndex(columns, "F_ZoneLine");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneAttrib0)) {
                index = getColumnIndex(columns, "F_ZoneAttrib0");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneAttrib1)) {
                index = getColumnIndex(columns, "F_ZoneAttrib1");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneAttrib2)) {
                index = getColumnIndex(columns, "F_ZoneAttrib2");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_ZoneAttrib4)) {
                index = getColumnIndex(columns, "F_ZoneAttrib4");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }

            }
            if (Ext.isEmpty(rs[i].data.F_ChargeType)) {
                index = getColumnIndex(columns, "F_ChargeType");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
            }
            if (Ext.isEmpty(rs[i].data.F_CurVersion)) {//该列被移除 然后出现列索引出错
                index = getColumnIndex(columns, "F_CurVersion");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                } 
            }
            if (Ext.isEmpty(rs[i].data.F_BigZoneID)) {
                index = getColumnIndex(columns, "F_BigZoneID");
                if (index != -1) {
                    grid.startEditing(i, index);
                    return false;
                }
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
            if (Ext.isEmpty(rs[i].data.F_Main_State)) {
                grid.startEditing(i, 6);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_Sub_State)) {
                grid.startEditing(i, 7);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_DBISID)) {
                grid.startEditing(i, 8);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_PingPort)) {
                grid.startEditing(i, 9);
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
    function validData4(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......
            if (Ext.isEmpty(rs[i].data.F_Key)) {
                grid.startEditing(i, 0);
                Ext.ex
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_Describe)) {
                grid.startEditing(i, 1);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_Value)) {
                grid.startEditing(i, 2);
                return false;
            }
        }
        return true;
    }
    function validData5(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......

            if (Ext.isEmpty(rs[i].data.F_CURVERSION)) {
                grid.startEditing(i, 1);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_LOWVERSION)) {
                grid.startEditing(i, 2);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_UPFILESIZE)) {
                grid.startEditing(i, 3);
                return false;

            }
            if (Ext.isEmpty(rs[i].data.F_DOWNFILESIZE)) {
                grid.startEditing(i, 4);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_TIME)) {
                grid.startEditing(i, 6);
                return false;
            }
        }
        return true;
    }
    function validData6(grid) {

        var rs = grid.getStore().modified || [];

        for (var i = 0; i < rs.length; i++) {

            //下面可作出各种验证......
            if (Ext.isEmpty(rs[i].data.F_CURVERSION)) {
                grid.startEditing(i, 1);
                return false;
            }

            if (Ext.isEmpty(rs[i].data.F_LOWVERSION)) {
                grid.startEditing(i, 2);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_UPFILESIZE)) {
                grid.startEditing(i, 3);
                return false;

            }
            if (Ext.isEmpty(rs[i].data.F_DOWNFILESIZE)) {
                grid.startEditing(i, 4);
                return false;
            }
            if (Ext.isEmpty(rs[i].data.F_TIME)) {
                grid.startEditing(i, 6);
                return false;
            }
        }
        return true;
    }
    function formatBigZoneName(value) {
        switch (value) { case '0': return StringBigZoneID0; case '1': return StringBigZoneID1; default: return StringZoneState2; }
    }
    var clientType = {
        UnLimit: { text:'<asp:Literal runat="server" Text="<%$Resources:Global,ServerType_NoLimit%>" />', value:undefined },
        Adnroid: { text: 'Android', value: 1 },
        IOS: { text: 'IOS', value: 2 },
        Mul:{ text:'<asp:Literal runat="server" Text="<%$Resources:Global,ServerType_Mixed%>" />', value: 3 }
    };
    function bindClientType(value) {//使用js来绑定combobox的数据源
        var cmbT = Ext.getCmp('cmbClientType');//判断是否绑定了数据源 
        var len = Object.getOwnPropertyNames(clientType).length;//应该存在多少组数据
        if (len != 0 && cmbT.store.data.items.length != len) {//没有绑定数据源
            var items = [];
            for (var p in clientType) {
                items.push([clientType[p].text, clientType[p].value]);
            }
            var store = new Ext.data.SimpleStore({
                data: items,
                fields:['text','value']
            });
            cmbT.store = store;
        }
        if (value == undefined) {
            return;
        }
        for (var p in clientType) {
            if (clientType[p].value == value) {
                return clientType[p].text;
            }
        }
        return clientType.UnLimit.text;
    }
    
</script>

<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None" CleanResourceUrl="False" />
    <!--store相关-->
    <ext:Store runat="server" ID="StoreBattleZone" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="false">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="BattleZones.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="BattleZonesSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="150" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_ZoneID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_ZoneID" Type="Int" />
                    <ext:RecordField Name="F_ZoneName" />
                    <ext:RecordField Name="F_ZoneState2" />
                    <ext:RecordField Name="F_ZoneState1" />
                    <ext:RecordField Name="F_ZoneState0" />
                    <ext:RecordField Name="F_ZoneState" />
                    <ext:RecordField Name="F_ZoneLine" />
                    <ext:RecordField Name="F_ZoneAttrib4" />
                    <ext:RecordField Name="F_ZoneAttrib2" />
                    <ext:RecordField Name="F_ZoneAttrib1" />
                    <ext:RecordField Name="F_ZoneAttrib0" />
                    <ext:RecordField Name="F_ChargeType" />
                    <ext:RecordField Name="F_CurVersion" />
                    <ext:RecordField Name="F_BigZoneID" />
                   <%-- 新增字段--%>
                    <ext:RecordField Name="F_ServerType"/>
                    <ext:RecordField Name="F_FaVersions_Cur"/>
                    <ext:RecordField Name="F_ReVersions_Cur"/>
                    <ext:RecordField Name="F_FaVersionsLow_And"/>
                    <ext:RecordField Name="F_ReVersionsLow_And"/>
                    <ext:RecordField Name="F_FaVersionsLow_Ios"/>
                    <ext:RecordField Name="F_ReVersionsLow_Ios"/>
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_ZoneID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreBattleLine" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="BattleLines.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="BattleLinesSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
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
                    <ext:RecordField Name="F_Main_State" />
                    <ext:RecordField Name="F_Sub_State" />
                    <ext:RecordField Name="F_DBISID" />
                    <ext:RecordField Name="F_PingPort" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_NGSID" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameServer" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="GameServers.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="GameServersSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
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
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameConfig" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="GameConfig.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="GameConfigSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_Key" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_Key" />
                    <ext:RecordField Name="F_Describe" />
                    <ext:RecordField Name="F_Value" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_Key" Direction="ASC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameVersionList" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="GameVersionList.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="GameVersionListSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_ID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_ID" Type="Int" />
                    <ext:RecordField Name="F_CURVERSION" />
                    <ext:RecordField Name="F_LOWVERSION" />
                    <ext:RecordField Name="F_UPFILESIZE" />
                    <ext:RecordField Name="F_DOWNFILESIZE" />
                    <ext:RecordField Name="F_FILENAME" />
                    <ext:RecordField Name="F_TIME" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_ID" Direction="DESC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:Store runat="server" ID="StoreGameSimpleVersionList" DirtyWarningText="<%$Resources:Global,StringIsRefresh%>"
        DirtyWarningTitle="<%$Resources:Global,StringAlertTitle%>" RemotePaging="False">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="GameSimpleVersionList.ashx" />
        </Proxy>
        <UpdateProxy>
            <ext:HttpWriteProxy Method="POST" Url="GameSimpleVersionListSave.ashx" />
        </UpdateProxy>
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="50" Mode="Raw" />
        </BaseParams>
        <Reader>
            <ext:JsonReader ReaderID="F_ID" Root="data" TotalProperty="totalCount">
                <Fields>
                    <ext:RecordField Name="F_ID" Type="Int" />
                    <ext:RecordField Name="F_CURVERSION" />
                    <ext:RecordField Name="F_LOWVERSION" />
                    <ext:RecordField Name="F_UPFILESIZE" />
                    <ext:RecordField Name="F_DOWNFILESIZE" />
                    <ext:RecordField Name="F_FILENAME" />
                    <ext:RecordField Name="F_TIME" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="F_ID" Direction="DESC" />
        <Listeners>
            <LoadException Handler="Ext.Msg.alert(StringLoadException, e.message )" />
            <CommitFailed Handler="Ext.Msg.alert(StringCommitFailed, '' + msg)" />
            <SaveException Handler="Ext.Msg.alert(StringSaveException, e.message)" />
            <CommitDone Handler="Ext.Msg.alert(StringCommitDone, StringCommitDoneT);" />
        </Listeners>
    </ext:Store>
    <ext:ViewPort ID="ViewPort1" runat="server" ContextMenuID="Menu1">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel ID="Panel1" runat="server" Title="<%$Resources:Global,StringToolTipsT%>"
                        Height="60" BodyStyle="padding: 5px;" Frame="true" Icon="Information" AutoHeight="True">
                        <Body>
                            <%=Resources.Global.StringToolTipsN%>
                            <br />
                            <ext:ToolbarButton ID="ToolbarButton0" runat="server" Icon="HouseGo" Text="<%$Resources:Global,StringLogOut%>"
                                Flat="True">
                                <AjaxEvents>
                                    <Click OnEvent="Logout_Click">
                                        <EventMask ShowMask="true" Msg="<%$Resources:Global,StringLogOutLoading%>" MinDelay="300" />
                                    </Click>
                                </AjaxEvents>
                            </ext:ToolbarButton>
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 5 5">
                    <ext:TabPanel ID="TabPanel1" runat="server">
                        <Tabs>
                            <ext:Tab ID="Tab1" runat="server" Title="<%$Resources:Global,StringZoneConfig%>"
                                BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout2" runat="server">
                                        <ext:Panel ID="Panel2" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout1" runat="server">
                                                    <ext:GridPanel ID="GridPanel1" runat="server" Title="<%$Resources:Global,StringZoneConfig%>"
                                                        AutoExpandColumn="F_BigZoneID" StoreID="StoreBattleZone" Border="false" Icon="ComputerWrench"
                                                        SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel1" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_ZoneID" DataIndex="F_ZoneID" Header="<%$Resources:Global,StringZoneID%>">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneName" Header="<%$Resources:Global,StringZoneName%>" Tooltip="<%$Resources:Global,BattleZoneInputLimit%>"
                                                                    Width="190px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField2" runat="server" MaxLength="12" AllowBlank="False" MinLength="2" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneState" Header="战区状态" Width="120px">
                                                                    <Renderer Handler="switch(value) {case '0':return '关闭';case '1':return '开放';case '64':return '对内调试 对外关闭';case '128':return '对内调试 对外隐藏';default:return '未知';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox7" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="关闭" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="开放" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="对内调试 对外关闭" Value="64" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="对内调试 对外隐藏" Value="128" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneLine" Header="<%$Resources:Global,StringZoneLine%>">
                                                                    <Renderer Handler="switch(value) {case '0':return StringZoneLine0;case '1':return StringZoneLine1;case '2':return StringZoneLine2;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox1" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneLine0%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneLine1%>" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneLine2%>" Value="2" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneAttrib0" Header="<%$Resources:Global,StringZoneAttrib0%>">
                                                                    <Renderer Handler="switch(value) {case '0':return StringNo;case '1':return StringYes;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox2" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringNo%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringYes%>" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneAttrib1" Header="<%$Resources:Global,StringZoneAttrib1%>">
                                                                    <Renderer Handler="switch(value) {case '0':return StringNo;case '1':return StringYes;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox9" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringNo%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringYes%>" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneAttrib2" Header="<%$Resources:Global,StringZoneAttrib2%>">
                                                                    <Renderer Handler="switch(value) {case '00':return StringZoneAttrib20;case '01':return StringZoneAttrib21;case '10':return StringZoneAttrib22;case '11':return StringZoneAttrib23;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox14" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneAttrib20%>" Value="00" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneAttrib21%>" Value="01" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneAttrib22%>" Value="10" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringZoneAttrib23%>" Value="11" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneAttrib4" Header="<%$Resources:Global,StringZoneAttrib4%>">
                                                                    <Renderer Handler="switch(value) {case '0':return StringNo;case '1':return StringYes;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox10" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringNo%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringYes%>" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ChargeType" Header="<%$Resources:Global,StringChargeType%>">
                                                                    <Renderer Handler="switch(value) {case '0':return StringChargeType0;case '1':return StringChargeType1;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox3" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringChargeType0%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringChargeType1%>" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                              <%--  <ext:Column DataIndex="F_CurVersion"   Header="<%$Resources:Global,StringCurVersion%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField3" runat="server" MaxLength="32" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>--%>
                                                                <ext:Column DataIndex="F_BigZoneID" Header="<%$Resources:Global,StringBigZoneID%>">
                                                                    <Renderer Handler="return formatBigZoneName(value)" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox4" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringBigZoneID0%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringBigZoneID1%>" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ServerType" Header="<%$Resources:Global,ClientType %>" Width="130">
                                                                    <Renderer Handler="return bindClientType(value)" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="cmbClientType" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="Android" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="IOS" Value="2" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_FaVersions_Cur" Width="140" Header="<%$Resources:Global,FormulaVersionsCur %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtFormulaVersions_Cur" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ReVersions_Cur" Width="140" Header="<%$Resources:Global,FResourceVersionsCur %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtFResourceVersions_Cur" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>

                                                                <ext:Column DataIndex="F_FaVersionsLow_And" Width="223" Header="<%$Resources:Global,F_FaVersionsLow_And %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtF_FaVersionsLow_And" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ReVersionsLow_And" Width="223" Header="<%$Resources:Global,F_ReVersionsLow_And %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtF_ReVersionsLow_And" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_FaVersionsLow_Ios" Width="223" Header="<%$Resources:Global,F_FaVersionsLow_Ios %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtF_FaVersionsLow_Ios" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                 <ext:Column DataIndex="F_ReVersionsLow_Ios" Width="223" Header="<%$Resources:Global,F_ReVersionsLow_Ios %>">
                                                                    <Editor>
                                                                        <ext:TextField ID="txtF_ReVersionsLow_Ios" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                 
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar1" runat="server" PageSize="150" StoreID="StoreBattleZone"
                                                                bufferResize="true" DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
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
                                                <ext:Button ID="btnSave" runat="server" Text="<%$Resources:Global,StringSave%>" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData(#{GridPanel1})){#{GridPanel1}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnDelete" runat="server" Text="<%$Resources:Global,StringDelete%>"
                                                    Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel1})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnInsert" runat="server" Text="<%$Resources:Global,StringAdd%>"
                                                    Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel1}.insertRecord(0, {});#{GridPanel1}.getView().focusRow(0);#{GridPanel1}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnRefresh" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreBattleZone,PagingToolBar1);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab2" runat="server" Title="<%$Resources:Global,StringLineConfig%>"
                                BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout3" runat="server">
                                        <ext:Panel ID="Panel3" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout4" runat="server">
                                                    <ext:GridPanel ID="GridPanel2" runat="server" Title="<%$Resources:Global,StringLineConfig%>"
                                                        AutoExpandColumn="F_PingPort" StoreID="StoreBattleLine" Border="false" Icon="FolderWrench"
                                                        SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel2" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_NGSID" DataIndex="F_NGSID" Header="<%$Resources:Global,StringLineNGSID%>">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Name" Header="<%$Resources:Global,StringLineName%>" Width="190px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField1" runat="server" MaxLength="15" AllowBlank="False" MinLength="2" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZoneID" Header="<%$Resources:Global,StringLineZoneID%>"
                                                                    Width="160px">
                                                                    <Renderer Handler=" var r = StoreBattleZone.getById(value);if(Ext.isEmpty(r)){ return StringZoneState2}return r.data.F_ZoneName;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox5" StoreID="StoreBattleZone" DisplayField="F_ZoneName"
                                                                            ValueField="F_ZoneID" runat="server" Editable="false" ReadOnly="True" AllowBlank="False"
                                                                            SelectOnFocus="True" ForceSelection="True" Mode="Local">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_MaxUser" Header="<%$Resources:Global,StringLineMaxUser%>"
                                                                    Width="140px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField7" runat="server" MaxLength="10" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Ip" Header="<%$Resources:Global,StringLineIP%>" Width="160px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField8" runat="server" MaxLength="26" Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Port" Header="<%$Resources:Global,StringLinePort%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField9" runat="server" MaxLength="6" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Main_State" Header="<%$Resources:Global,StringLineState%>" Width="110px">
                                                                    <Renderer Handler="switch(value) {case '0':return StringLineState0;case '1':return StringLineState1;case '64':return StringLineState2;case '128':return StringLineState3;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox6" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringLineState0%>" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringLineState1%>" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringLineState2%>" Value="64" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringLineState3%>" Value="128" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Sub_State" Header="显示状态">
                                                                    <Renderer Handler="switch(value) {case '0':return '显示';case '1':return '隐藏';default:return '显示';}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox15" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="否" Value="0" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="是" Value="1" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_DBISID" Header="<%$Resources:Global,StringLineDBISID%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField4" runat="server" MaxLength="5" AllowBlank="False" Regex="^[1-9]\d*$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_PingPort" Header="<%$Resources:Global,StringLinePingPort%>">
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
                                                            <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="50" StoreID="StoreBattleLine"
                                                                DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button1" runat="server" Text="<%$Resources:Global,StringSave%>" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData2(#{GridPanel2})){#{GridPanel2}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button2" runat="server" Text="<%$Resources:Global,StringDelete%>"
                                                    Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel2})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button3" runat="server" Text="<%$Resources:Global,StringAdd%>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel2}.insertRecord(0, {});#{GridPanel2}.getView().focusRow(0);#{GridPanel2}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button4" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreBattleLine,PagingToolBar2);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab3" runat="server" Title="<%$Resources:Global,StringGSConfig%>" BodyStyle="padding: 1px;"
                                BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout5" runat="server">
                                        <ext:Panel ID="Panel4" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout6" runat="server">
                                                    <ext:GridPanel ID="GridPanel3" runat="server" Title="<%$Resources:Global,StringGSConfig%>"
                                                        AutoExpandColumn="F_GSSceneID" StoreID="StoreGameServer" Border="false" Icon="FolderWrench"
                                                        SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel3" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_GSID" DataIndex="F_GSID" Header="<%$Resources:Global,StringGSID%>">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_IP" Header="<%$Resources:Global,StringGSIP%>" Width="160px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField5" runat="server" MaxLength="26" AllowBlank="False" MinLength="2"
                                                                            Regex="/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_GSNAME" Header="<%$Resources:Global,StringGSName%>" Width="160px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField11" runat="server" MaxLength="15" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_NGSID" Header="<%$Resources:Global,StringGSNGSID%>" Width="180px">
                                                                    <Renderer Handler=" var r = StoreBattleLine.getById(value);if(Ext.isEmpty(r)){ return StringZoneState2}return r.data.F_Name;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox11" StoreID="StoreBattleLine" DisplayField="F_Name" ValueField="F_NGSID"
                                                                            runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_ZONEID" Header="<%$Resources:Global,StringLineZoneID%>"
                                                                    Width="160px">
                                                                    <Renderer Handler=" var r = StoreBattleZone.getById(value);if(Ext.isEmpty(r)){ return StringZoneState2}return r.data.F_ZoneName;" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox12" runat="server" StoreID="StoreBattleZone" DisplayField="F_ZoneName"
                                                                            ValueField="F_ZoneID" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_CampID" Header="<%$Resources:Global,StringCampID%>">
                                                                    <Renderer Handler="switch(value) {case '1':return StringCampID0;case '2':return StringCampID1;default:return StringZoneState2;}" />
                                                                    <Editor>
                                                                        <ext:ComboBox ID="ComboBox13" runat="server" Editable="false" ReadOnly="True" AllowBlank="False">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringCampID0%>" Value="1" />
                                                                            </Items>
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources:Global,StringCampID1%>" Value="2" />
                                                                            </Items>
                                                                        </ext:ComboBox>
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_GSSceneID" Header="<%$Resources:Global,StringGSSceneID%>"
                                                                    Width="250px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField6" runat="server" MaxLength="400" AllowBlank="False"
                                                                            Regex="^(\d+\,)+$" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar3" runat="server" PageSize="50" StoreID="StoreGameServer"
                                                                DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button5" runat="server" Text="<%$Resources:Global,StringSave%>" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData3(#{GridPanel3})){#{GridPanel3}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button6" runat="server" Text="<%$Resources:Global,StringDelete%>"
                                                    Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel3})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button7" runat="server" Text="<%$Resources:Global,StringAdd%>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel3}.insertRecord(0, {});#{GridPanel3}.getView().focusRow(0);#{GridPanel3}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button8" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreGameServer,PagingToolBar3);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab4" runat="server" Title="<%$Resources:Global,StringGameConfig%>"
                                BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout7" runat="server">
                                        <ext:Panel ID="Panel5" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout8" runat="server">
                                                    <ext:GridPanel ID="GridPanel4" runat="server" Title="<%$Resources:Global,StringGameConfig%>"
                                                        AutoExpandColumn="F_Value" StoreID="StoreGameConfig" Border="false" Icon="FolderWrench"
                                                        SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel4" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_Key" DataIndex="F_Key" Header="<%$Resources:Global,StringGameKey%>"
                                                                    Width="230px">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Describe" Header="<%$Resources:Global,StringGameNote%>"
                                                                    Width="300px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField12" runat="server" MaxLength="256" AllowBlank="False"
                                                                            MinLength="2" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_Value" Header="<%$Resources:Global,StringGameValue%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField13" runat="server" MaxLength="256" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel4" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar4" runat="server" PageSize="50" StoreID="StoreGameConfig"
                                                                DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button9" runat="server" Text="<%$Resources:Global,StringSave%>" Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData4(#{GridPanel4})){#{GridPanel4}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button12" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreGameConfig,PagingToolBar4);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab5" runat="server" Title="<%$Resources:Global,StringGameVersionListConfig%>"
                                BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout9" runat="server">
                                        <ext:Panel ID="Panel6" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout10" runat="server">
                                                    <ext:GridPanel ID="GridPanel5" runat="server" Title="<%$Resources:Global,StringGameVersionListConfig%>"
                                                        AutoExpandColumn="F_TIME" StoreID="StoreGameVersionList" Border="false" Icon="FolderWrench"
                                                        SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel5" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_ID" DataIndex="F_ID" Header="<%$Resources:Global,StringGameVersionListID%>"
                                                                    Width="80px">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_CURVERSION" Header="<%$Resources:Global,StringGameVersionListCurVer%>"
                                                                    Width="150px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField14" runat="server" MaxLength="10" Regex="^[1-9]\d*$"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_LOWVERSION" Header="<%$Resources:Global,StringGameVersionListLowVer%>"
                                                                    Width="150px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField15" runat="server" MaxLength="10" Regex="^[1-9]\d*$"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_UPFILESIZE" Header="<%$Resources:Global,StringGameVersionListUpFileSize%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField16" runat="server" MaxLength="25" Regex="^\d*$" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_DOWNFILESIZE" Header="<%$Resources:Global,StringGameVersionListDownFileSize%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField17" runat="server" MaxLength="25" Regex="^\d*$" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_FILENAME" Header="<%$Resources:Global,StringGameVersionListFileName%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField18" runat="server" MaxLength="50" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_TIME" Header="<%$Resources:Global,StringGameVersionListTime%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField19" runat="server" MaxLength="20" Regex="(^\d{4}[-]\d{1,2}[-]\d{1,2}$)|(^\d{4}[-]\d{1,2}[-]\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}$)|(^\d{4}[-]\d{1,2}[-]\d{1,2} \d{1,2}:\d{1,2}$)"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel5" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar5" runat="server" PageSize="50" StoreID="StoreGameVersionList"
                                                                DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button10" runat="server" Text="<%$Resources:Global,StringSave%>"
                                                    Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData5(#{GridPanel5})){#{GridPanel5}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button11" runat="server" Text="<%$Resources:Global,StringDelete%>"
                                                    Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel5})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button15" runat="server" Text="<%$Resources:Global,StringAdd%>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel5}.insertRecord(0, {});#{GridPanel5}.getView().focusRow(0);#{GridPanel5}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button16" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreGameVersionList,PagingToolBar5);" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab6" runat="server" Title="<%$Resources:Global,StringGameSimpleVersionListConfig%>"
                                BodyStyle="padding: 1px;" BaseCls="x-bluebg">
                                <Body>
                                    <ext:FitLayout ID="FitLayout11" runat="server">
                                        <ext:Panel ID="Panel7" runat="server" Header="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout12" runat="server">
                                                    <ext:GridPanel ID="GridPanel6" runat="server" Title="<%$Resources:Global,StringGameSimpleVersionListConfig%>"
                                                        AutoExpandColumn="F_TIME" StoreID="StoreGameSimpleVersionList" Border="false"
                                                        Icon="FolderWrench" SelectionMemory="Disabled" TrackMouseOver="True">
                                                        <ColumnModel ID="ColumnModel6" runat="server">
                                                            <Columns>
                                                                <ext:Column ColumnID="F_ID" DataIndex="F_ID" Header="<%$Resources:Global,StringGameVersionListID%>"
                                                                    Width="80px">
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_CURVERSION" Header="<%$Resources:Global,StringGameVersionListCurVer%>"
                                                                    Width="150px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField20" runat="server" MaxLength="10" Regex="^[1-9]\d*$"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_LOWVERSION" Header="<%$Resources:Global,StringGameVersionListLowVer%>"
                                                                    Width="150px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField21" runat="server" MaxLength="10" Regex="^[1-9]\d*$"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_UPFILESIZE" Header="<%$Resources:Global,StringGameVersionListUpFileSize%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField22" runat="server" MaxLength="256" Regex="^\d*$" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_DOWNFILESIZE" Header="<%$Resources:Global,StringGameVersionListDownFileSize%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField23" runat="server" MaxLength="256" Regex="^\d*$" AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_FILENAME" Header="<%$Resources:Global,StringGameVersionListFileName%>"
                                                                    Width="200px">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField24" runat="server" MaxLength="50" />
                                                                    </Editor>
                                                                </ext:Column>
                                                                <ext:Column DataIndex="F_TIME" Header="<%$Resources:Global,StringGameVersionListTime%>">
                                                                    <Editor>
                                                                        <ext:TextField ID="TextField25" runat="server" MaxLength="20" Regex="(^\d{4}[-]\d{1,2}[-]\d{1,2}$)|(^\d{4}[-]\d{1,2}[-]\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}$)|(^\d{4}[-]\d{1,2}[-]\d{1,2} \d{1,2}:\d{1,2}$)"
                                                                            AllowBlank="False" />
                                                                    </Editor>
                                                                </ext:Column>
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" />
                                                        </SelectionModel>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolBar6" runat="server" PageSize="50" StoreID="StoreGameSimpleVersionList"
                                                                DisplayInfo="true" DisplayMsg=" {0} - {1} of {2}" EmptyMsg="<%$Resources:Global,StringNoInfo%>" />
                                                        </BottomBar>
                                                        <SaveMask ShowMask="true" />
                                                        <LoadMask ShowMask="true" />
                                                    </ext:GridPanel>
                                                </ext:FitLayout>
                                            </Body>
                                            <Buttons>
                                                <ext:Button ID="Button13" runat="server" Text="<%$Resources:Global,StringSave%>"
                                                    Icon="Disk">
                                                    <Listeners>
                                                        <Click Handler="if(validData6(#{GridPanel6})){#{GridPanel6}.save();}" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button14" runat="server" Text="<%$Resources:Global,StringDelete%>"
                                                    Icon="Delete">
                                                    <Listeners>
                                                        <Click Handler="isdodelete(#{GridPanel6})" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button17" runat="server" Text="<%$Resources:Global,StringAdd%>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="#{GridPanel6}.insertRecord(0, {});#{GridPanel6}.getView().focusRow(0);#{GridPanel6}.startEditing(0, 0);" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button18" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                                                    Icon="ArrowRefresh">
                                                    <Listeners>
                                                        <Click Handler="refreshstore(StoreGameSimpleVersionList,PagingToolBar6);" />
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
            <ext:MenuItem ID="MenuItem1" runat="server" Text="<%$Resources:Global,StringRefresh%>"
                Icon="PageRefresh">
                <Listeners>
                    <Click Handler="location.reload();" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    </form>
</body>
</html>
