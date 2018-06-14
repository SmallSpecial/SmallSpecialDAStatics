<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskAdd.aspx.cs"  ValidateRequest="false" 
    Inherits="WSS.Web.Admin.Task.TaskAdd" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../../style/master.css" media="all" />

    <script type="text/javascript" src="../../fckeditor/fckeditor.js"></script>

    <script src="../../fckeditor/fckapi.js" type="text/javascript"></script>

    <script type="text/javascript">
        var getFckText = function() { var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor'); return oEditor.GetXHTML(true); };


    </script>

    <style type="text/css">
        .star
        {
            color: Red;
        }
    </style>

    <script type="text/javascript">
        

        var getdic = function(value) {
        var r = StoreDic.getById(value);
        if (Ext.isEmpty(r)) {
            return "未知";
            }

            return r.data.F_Value;
        }
        var template = '<span style="color:{0};">{1}</span>';
        var pctChange = function(value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value + '%');
        }
//        var departmentRenderer = function(value) {
//        var r = #{cbFrom}.getStore().getById(value);
//            
//            if (Ext.isEmpty(r)) {
//                return "1111";
//            }

//            return r.data.Name;
//        }

//        Coolite.AjaxMethods.gettypename("ddd", { success: function(result) { Ext.Msg.alert('Failure', result); ; } })
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server">
        <Listeners>
            <DocumentReady Handler="var oFCKeditor = new FCKeditor('fckHtmlEditor');oFCKeditor.BasePath = '/fckeditor/';oFCKeditor.Width = '580px';oFCKeditor.Skin = 'silver';oFCKeditor.Height = '300px';oFCKeditor.ReplaceTextarea();" />
        </Listeners>
    </ext:ScriptManager>
    <ext:Store ID="StoreTasks" OnRefreshData="MyData_Refresh" runat="server" AutoLoad="true">
        <Reader>
            <ext:JsonReader ReaderID="F_ID">
                <Fields>
                    <ext:RecordField Name="F_ID" Type="Int" />
                    <ext:RecordField Name="F_Title" />
                    <ext:RecordField Name="F_Note" />
                    <%--   <ext:RecordField Name="F_From" />--%>
                    <ext:RecordField Name="F_Type" />
                    <ext:RecordField Name="F_State" />
                    <ext:RecordField Name="F_JinjiLevel" />
                    <ext:RecordField Name="F_DateTime" Type="Date" DateFormat="Y-m-dTh:i:s" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        <ext:Store ID="StoreDic" OnRefreshData="MyData_Refresh" runat="server" AutoLoad="true">
        <Reader>
            <ext:JsonReader ReaderID="F_DicID">
                <Fields>
                    <ext:RecordField Name="F_DicID" Type="Int" />
                    <ext:RecordField Name="F_Value" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Hidden runat="server" ID="GetAction" />
    <ext:Panel ID="NewsPanel" Border="false" runat="server" AutoHeight="true" Header="false" Visible="False">
        <Body>
            <ext:GridPanel ID="GridPanelNewsList" AutoHeight="true" ContextMenuID="NewsMI" AutoScroll="false"
                Draggable="false" EnableColumnResize="false" EnableColumnMove="false" Border="false"
                runat="server" StoreID="StoreTasks" StripeRows="True">
                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:Label ID="Label11" Height="30" Text="标题:&nbsp;" runat="server" />
                            <ext:TextField ID="tftitle"  MaxLength="30" MaxLengthText="不能超过60个字符(30个汉字)" runat="server" Width="100">
                            </ext:TextField>
                            <ext:Label ID="Label13"  Text="&nbsp;内容:&nbsp;" runat="server" />
                            
                            <ext:TextField ID="tfnote"  MaxLength="30" MaxLengthText="不能超过60个字符(30个汉字)" runat="server" >
                            </ext:TextField>
                            <ext:Button ID="Button1" runat="server" Text="查找" Icon="FolderExplore">
                                 <AjaxEvents>
                                    <Click OnEvent="Search_Click">
                                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                            <ext:Button ID="Button2" runat="server" Text="全部" Icon="FolderTable">
                                 <AjaxEvents>
                                    <Click OnEvent="SearchAll_Click">
                                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                            <ext:Button ID="btnAdd" runat="server" Text="添加工单" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{NewsWindow}.setTitle('添加工单','iconadd');#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{GetAction}.setRawValue('add');#{NewsWindow}.show();#{txtTitle}.focus(true);" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnUpdate" runat="server" Text="修改工单" Icon="Anchor">
                                <Listeners>
                                    <Click Handler="Coolite.AjaxMethods.Update_Click({success:function(result){var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML(result);#{NewsWindow}.setTitle('修改工单','iconup');#{NewsWindow}.show();}});" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnDelete" runat="server" Text="删除选中的工单" Icon="Delete">
                                <AjaxEvents>
                                    <Click OnEvent="Delete_Click">
                                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <ColumnModel ID="ColumnModelTitle" IDMode="Legacy" Height="30" runat="server">
                    <Columns>
                        <ext:Column ColumnID="F_State" DataIndex="F_State" Header="状态" Sortable="true" Width="60px"
                            Align="Center">
                            <Renderer Fn="getdic" />
                        </ext:Column>
                        <ext:Column ColumnID="F_Type" DataIndex="F_Type" Width="80" Header="工单类型" Sortable="true">
                            <Renderer Fn="getdic" />
                        </ext:Column>
                        <ext:Column ColumnID="F_ID" DataIndex="F_ID" Width="80" Header="工单编号" Sortable="true">
                        </ext:Column>
                        <ext:Column ColumnID="F_Title" DataIndex="F_Title" Width="200" Header="工单标题" Sortable="true">
                        
                        </ext:Column>
                        <ext:Column ColumnID="F_Note" DataIndex="F_Note" Width="300" Header="工单内容" Sortable="true">
                        </ext:Column>
                        <ext:Column ColumnID="F_DateTime" DataIndex="F_DateTime" Width="115" Header="更新时间"
                            Sortable="true">
                            <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel SelectedRecordID="F_ID" ID="RowSelectionModel1" runat="server"
                        SingleSelect="True">
                        <CustomConfig>
                            <ext:ConfigItem Name="checkOnly" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </ext:RowSelectionModel>
                </SelectionModel>
                <LoadMask ShowMask="true" />
                <SaveMask ShowMask="true" Msg="正在保存,请稍候..." />
                <BottomBar>
                    <ext:PagingToolbar ID="pagecut" runat="server" StoreID="StoreTasks" PageSize="10">
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
        </Body>
    </ext:Panel>
    <ext:Menu ID="NewsMI" runat="server">
        <Items>
            <ext:MenuItem ID="Mad" Icon="Add" runat="server" Text="添加工单">
                <Listeners>
                    <Click Handler="#{NewsWindow}.setTitle('添加工单','iconadd');#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{GetAction}.setRawValue('add');#{NewsWindow}.show();#{txtTitle}.focus(true);" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="Medit" Icon="Anchor" runat="server" Text="修改工单">
                <Listeners>
                    <Click Handler="Coolite.AjaxMethods.Update_Click({success:function(result){var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML(result);#{NewsWindow}.show();}});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="Mdel" runat="server" Icon="Delete" Text="删除选中的工单">
                <AjaxEvents>
                    <Click OnEvent="Delete_Click">
                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                    </Click>
                </AjaxEvents>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Window ID="NewsWindow" AutoHeight="true" Border="false" Modal="false" 
        Width="620" runat="server" Collapsible="False" Icon="Add" Title="新增工单" X="25" Y="10" Closable="False" Collapsed="false">
        <Body>
            <ext:FormPanel ID="FormPanelNews" MonitorValid="true" MonitorPoll="500" Shadow="Sides"
                runat="server" Header="false" BodyStyle="padding:5px;" ButtonAlign="Right" Width="610">
                <Body>
                    <ext:FormLayout ID="FormLayout1" runat="server" LabelAlign="Top">
                        <ext:Anchor Horizontal="70%">
                            <ext:TextField ID="txtTitle" AllowBlank="false" BlankText="请输入工单标题!" MaxLength="30"
                                MaxLengthText="标题不能超过60个字符(30个汉字)" runat="server" FieldLabel="工单标题">
                            </ext:TextField>
                        </ext:Anchor>
                        <ext:Anchor Horizontal="80%">
                            <ext:Panel ID="Panel2" Border="false" runat="server" AutoWidth="true" BodyStyle="padding:3px;">
                                <Body>
                                    <ext:TableLayout ID="TableLayout1" runat="server" Columns="6">
                                        <ext:Cell>
                                            <ext:Label ID="Label1" Height="30" Text="信息来源:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbFrom" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                ReadOnly="True" AllowBlank="False">
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label2" Height="30" Text="&nbsp;&nbsp;紧急程度:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell ColSpan="3">
                                            <ext:ComboBox ID="cbJinjiLevel" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                ReadOnly="True" AllowBlank="False">
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label5" Height="30" Text="游戏名称:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbGameName" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                ReadOnly="True" AllowBlank="False">
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label3" Height="30" Text="&nbsp;&nbsp;工单类型:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbType" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                ReadOnly="True" AllowBlank="False">
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label4" Height="30" Text="&nbsp;&nbsp;职责人:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:ComboBox ID="cbDutyMan" runat="server" Width="100" BlankText="不能为空" EmptyText="请选择"
                                                ReadOnly="True" AllowBlank="False">
                                            </ext:ComboBox>
                                        </ext:Cell>
                                        <ext:Cell>
                                            <ext:Label ID="Label6" Height="30" Text="玩家电话:&nbsp;&nbsp;" runat="server" />
                                        </ext:Cell>
                                        <ext:Cell ColSpan="5">
                                            <ext:TextField ID="txtTelphone" AllowBlank="false" BlankText="此项不能为空!" runat="server"
                                                FieldLabel="工单作者/来源" MaxLength="16" MaxLengthText="玩家电话最长16位">
                                            </ext:TextField>
                                        </ext:Cell>
                                    </ext:TableLayout>
                                </Body>
                            </ext:Panel>
                        </ext:Anchor>
                        <ext:Anchor Horizontal="80%">
                            <ext:Panel ID="EditPanel" Border="false" runat="server" AutoWidth="true">
                                <Body>
                                    <ext:Label ID="la" Height="30" Text="工单内容:" runat="server" />
                                    <br />
                                    <ext:TextArea ID="fckHtmlEditor" runat="server"  AllowBlank="False" >
                                    </ext:TextArea>
                                </Body>
                            </ext:Panel>
                        </ext:Anchor>
                    </ext:FormLayout>
                </Body>
                <Buttons>
                    <ext:Button ID="btnSubmit" Type="Submit" runat="server" Icon="Disk" Text="提 交">
                        <Listeners>
                            <Click Handler="if(#{FormPanelNews}.getForm().isValid()){return true;}else{#{txtTitle}.focus(true); return false;}" />
                        </Listeners>
                        <AjaxEvents>
                            <Click OnEvent="Check_btnSubmit">
                                <ExtraParams>
                                    <ext:Parameter Name="fckParameter" Mode="Raw" Value="getFckText()">
                                    </ext:Parameter>
                                </ExtraParams>
                                <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                            </Click>
                        </AjaxEvents>
                    </ext:Button>
                </Buttons>
                <BottomBar>
                    <ext:StatusBar ID="NewsStatus" runat="server" />
                </BottomBar>
                <Listeners>
                    <ClientValidation Handler="#{NewsStatus}.setStatus({text: valid ? '该表单已经通过验证' : '表单未通过验证', iconCls: valid ? 'icon-accept' : 'icon-exclamation'});" />
                </Listeners>
            </ext:FormPanel>
        </Body>
        <Listeners>
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
