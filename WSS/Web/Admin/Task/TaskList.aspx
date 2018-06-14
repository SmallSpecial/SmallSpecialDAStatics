<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" ValidateRequest="false"
    Inherits="WSS.Web.Admin.Task.TaskList" %>

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
        var getFckText = function() { var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor'); oEditor.Width = 100; return oEditor.GetXHTML(true); };
    </script>

    <style type="text/css">
        .star
        {
            color: Red;
        }
    </style>
    <style type="text/css">
        .search-item {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
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

        var templated = '<span style="color:{0};font-size:30;">●</span>';
        var getdeng = function(value) {
            return String.format(templated, (value > 0) ? 'green' : 'red');
        }
        var trimhtml = function(value) {
        return Ext.util.Format.ellipsis(Ext.util.Format.stripTags(value),20,true);
        }

        //        var cellClick1= function(grid, rowIndex, columnIndex, e) {
        //            var t = e.getTarget();
        //            var record = grid.getStore().getAt(rowIndex);  // Get the Record
        //            var columnId = grid.getColumnModel().getColumnId(columnIndex); // Get column id

        //            if (t.className == 'imgEdit' && columnId == 'Edit') {
        //                openEmployeeDetails(record, t);
        //            }
        //        }


        var cellClick = function(command, record) {
            var rowid = record.data.F_ID;
            if (command == 'Delete') {
                // Ext.Msg.alert("e", record.data.F_ID);

                //                 Ext.Msg.Confirm("www","您确定要删除选中的数据项吗?",new MessageBox.ButtonsConfig{Yes = new MessageBox.ButtonConfig{Handler = "Coolite.AjaxMethods.DeleteNews(" + rowid + ");",Text = "确 定"},No = new MessageBox.ButtonConfig{Text = "取 消"}}).Show();

                // Ext.Msg.Confirm("成功提示信息", "这是确认框", new JFunction { Fn = "showReturn" }).Show();
                Coolite.AjaxMethods.DeleteCommd(rowid);

            }
            else {
                Coolite.AjaxMethods.Update_Click1(rowid, { success: function(result) { var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor'); oEditor.SetHTML(result); } });
            }


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
         <ext:Store runat="server" ID="Store1">        
            <Proxy>            
                <ext:HttpProxy Method="get" Url="UserHandler.ashx" >               
                </ext:HttpProxy>
            </Proxy>
            <Reader>
                <ext:ArrayReader>
                     <Fields>                        
                        <ext:RecordField Name="Name" Mapping="NAME" Type="String" />
                    </Fields>
                </ext:ArrayReader>
            </Reader>
        </ext:Store> 
    <ext:Hidden runat="server" ID="GetAction" />
    <ext:Hidden runat="server" ID="HF_id" />
    <ext:Panel ID="NewsPanel" Border="false" runat="server" AutoHeight="true" Header="false">
        <Body>
            <ext:GridPanel ID="GridPanelNewsList" AutoHeight="true" ContextMenuID="NewsMI" AutoScroll="false"
                Draggable="false" EnableColumnResize="true" EnableColumnMove="false" Border="false"
                runat="server" StoreID="StoreTasks" StripeRows="True" AutoExpandColumn="F_Note" TrackMouseOver="True">
                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:Label ID="Label11" Height="30" Text="标题:&nbsp;" runat="server" />
                            <ext:TextField ID="tftitle" MaxLength="30" MaxLengthText="不能超过60个字符(30个汉字)" runat="server"
                                Width="100">
                            </ext:TextField>
                            <ext:Label ID="Label13" Text="&nbsp;内容:&nbsp;" runat="server" />
                            <ext:TextField ID="tfnote" MaxLength="30" MaxLengthText="不能超过60个字符(30个汉字)" runat="server">
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
                                    <Click Handler="e.stopEvent();#{NewsWindow}.setTitle('添加工单','iconadd');#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{GetAction}.setRawValue('add');#{LabelHistory}.setText('',true);#{cbstate}.disable();#{NewsWindow}.show();#{txtTitle}.focus(true);" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnUpdate" runat="server" Text="修改工单" Icon="Anchor">
                                <Listeners>
                                    <Click Handler="e.stopEvent();Coolite.AjaxMethods.Update_Click({success:function(result){var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML(result);#{NewsWindow}.setTitle('修改工单','iconup');#{cbstate}.enable();#{NewsWindow}.show();}});" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnDelete" runat="server" Text="删除选中的工单" Icon="Delete">
                                <AjaxEvents>
                                    <Click OnEvent="Delete_Click">
                                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                            <ext:Button ID="Button3" runat="server" Text="转为历史工单" Icon="Bricks">
                                <AjaxEvents>
                                    <Click OnEvent="History_Click">
                                        <EventMask ShowMask="true" Msg="正在获取数据并执行操作..." />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <ColumnModel ID="ColumnModelTitle" IDMode="Legacy" Height="30" runat="server">
                    <Columns>
                        <ext:Column ColumnID="F_State" DataIndex="F_State" Header="预警" Sortable="true" Width="60px"
                            Align="Center">
                            <Renderer Fn="getdeng" />
                        </ext:Column>
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
                         <Renderer Fn="trimhtml" />
                        </ext:Column>
                        <ext:Column ColumnID="F_DateTime" DataIndex="F_DateTime" Width="115" Header="更新时间"
                            Sortable="true">
                            <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                        </ext:Column>
                        <ext:CommandColumn Width="60" Fixed="True" Resizable="False" Header="&nbsp;操作">
                            <Commands>
                                <ext:GridCommand Icon="ReportEdit" CommandName="Edit">
                                    <ToolTip Text="编辑" />
                                </ext:GridCommand>
                                <ext:CommandSeparator />
                                <ext:GridCommand Icon="Delete" CommandName="Delete">
                                    <ToolTip Text="删除" />
                                </ext:GridCommand>
                            </Commands>
                        </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <%--<ext:RowSelectionModel SelectedRecordID="F_ID" ID="RowSelectionModel1" runat="server"
                        SingleSelect="false">
                        <CustomConfig>
                            <ext:ConfigItem Name="checkOnly" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </ext:RowSelectionModel>--%>
                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" CheckOnly="true" />
                </SelectionModel>
                <LoadMask ShowMask="true" />
                <SaveMask ShowMask="true" Msg="正在保存,请稍候..." />
                <BottomBar>
                    <ext:PagingToolbar ID="pagecut" runat="server" StoreID="StoreTasks" PageSize="10">
                    </ext:PagingToolbar>
                </BottomBar>
                <Listeners><%--#{CheckboxSelectionModel1}.clearSelections();#{CheckboxSelectionModel1}.selectRow(row,true);--%>
                    <Command Handler="#{CheckboxSelectionModel1}.clearSelections();cellClick(command,record);e.stopEvent();" />
                </Listeners>
            </ext:GridPanel>
        </Body>
    </ext:Panel>
    <ext:Menu ID="NewsMI" runat="server">
        <Items>
            <ext:MenuItem ID="Mad" Icon="Add" runat="server" Text="添加工单">
                <Listeners>
                    <Click Handler="#{NewsWindow}.setTitle('添加工单','iconadd');#{FormPanelNews}.getForm().reset();var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML('');#{GetAction}.setRawValue('add');#{LabelHistory}.setText('',true);#{cbstate}.disable();#{NewsWindow}.show();#{txtTitle}.focus(true);" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="Medit" Icon="Anchor" runat="server" Text="修改工单">
                <Listeners>
                    <Click Handler="Coolite.AjaxMethods.Update_Click({success:function(result){var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML(result);#{cbstate}.enable();#{NewsWindow}.show();}});" />
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
    
    

    <ext:Window ID="NewsWindow" Border="false" Modal="true" ShowOnLoad="false" Width="850"
        runat="server" Collapsible="true" Icon="Application" Title="Title" Height="580px">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <West Collapsible="true" Split="true" MarginsSummary="5 0 0 5" CMarginsSummary="5 5 0 5"
                    MinWidth="130px">
                    <ext:FormPanel ID="FormPanelNews" MonitorValid="true" MonitorPoll="500" Shadow="Sides"
                        runat="server" Header="true" BodyStyle="padding:5px;" ButtonAlign="Right" Width="600"
                        Title="工单信息">
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
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label2" Height="30" Text="&nbsp;&nbsp;紧急程度:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:ComboBox ID="cbJinjiLevel" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label7" Height="30" Text="&nbsp;&nbsp;工单状态:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:ComboBox ID="cbstate" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label5" Height="30" Text="游戏名称:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:ComboBox ID="cbGameName" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label3" Height="30" Text="&nbsp;&nbsp;工单类型:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:ComboBox ID="cbType" runat="server" Width="80" BlankText="不能为空" EmptyText="请选择"
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                        <Listeners>
                                                            <Select Handler="Coolite.AjaxMethods.GetTemplate({success:function(result){var oEditor = FCKeditorAPI.GetInstance('fckHtmlEditor');oEditor.SetHTML(result);}});" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label4" Height="30" Text="&nbsp;&nbsp;职责人:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:ComboBox ID="cbDutyMan" runat="server" Width="100" BlankText="不能为空" EmptyText="请选择"
                                                        Editable="false" ReadOnly="True" AllowBlank="False">
                                                    </ext:ComboBox>
                                                </ext:Cell>
                                               <ext:Cell>
                                                    <ext:Label ID="Label8" Height="30" Text="用户名:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell>
                                                     <ext:ComboBox 
                                                                    ID="ComboBox1"
                                                                    runat="server" 
                                                                    StoreID="StoreTasks"
                                                                    DisplayField="F_Title" 
                                                                    ValueField="F_Title"
                                                                    TypeAhead="false"
                                                                    LoadingText="Searching..." 
                                                                    Width="180"
                                                                    PageSize="5"
                                                                    HideTrigger="true"
                                                                    ItemSelector="div.search-item"        
                                                                    MinChars="1">
                                                                    <Template ID="Template1" runat="server">
                                                                       <tpl for=".">
                                                                          <div class="search-item">
                                                                             <h3><span>${F_Title}</span>{F_Title}</h3>
                                                                             {F_Title}
                                                                          </div>
                                                                       </tpl>
                                                                    </Template>
                                                                </ext:ComboBox>
                                                </ext:Cell>
                                                <ext:Cell>
                                                    <ext:Label ID="Label6" Height="30" Text="玩家电话:&nbsp;&nbsp;" runat="server" />
                                                </ext:Cell>
                                                <ext:Cell ColSpan="3">
                                                    <ext:TextField ID="txtTelphone" AllowBlank="false" BlankText="此项不能为空!" runat="server"  MaxLength="16" MaxLengthText="玩家电话最长16位">
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
                                            <ext:TextArea ID="fckHtmlEditor" runat="server" AllowBlank="False" Width="100px">
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
                </West>
                <Center MarginsSummary="5 0 0 0">
                    <ext:TabPanel ID="TabPanel1" runat="server">
                        <Tabs>
                            <ext:Tab ID="Tab2" runat="server" Title="工单记录" BodyStyle="padding: 2px;" AutoScroll="True">
                                <Body>
                                    <ext:Label ID="LabelHistory" Text="&nbsp;暂无工单记录" runat="server" />
                                </Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </Center>
            </ext:BorderLayout>

            <script type="text/javascript">
                //                        var loadPageTasks = function(tabPanel, node) {
                //                            var tab = tabPanel.getItem(node.id);
                //                            if (!tab) {
                //                                tab = tabPanel.add({
                //                                    id: node.id,
                //                                    title: node.text,
                //                                    closable: true,
                //                                    autoLoad: {
                //                                        showMask: true,
                //                                        url: node.attributes.href,
                //                                        mode: 'iframe',
                //                                        maskMsg: '正在加载 ' + node.attributes.href + '...'
                //                                    },
                //                                    listeners: {
                //                                        update: {
                //                                            fn: function(tab, cfg) {
                //                                                cfg.iframe.setHeight(cfg.iframe.getSize().height - 20);
                //                                            },
                //                                            scope: this,
                //                                            single: true
                //                                        }
                //                                    }
                //                                });
                //                            }
                //                            tabPanel.setActiveTab(tab);
                //                        }
                function onItemClick(menuItem) {

                    var tab = TabPanelTasks.getItem(menuItem.text);
                    if (!tab) {
                        tab = TabPanelTasks.add({
                            id: menuItem.text,
                            title: menuItem.text,
                            closable: true,
                            autoLoad: {
                                showMask: true,
                                url: menuItem.href,
                                mode: 'iframe',
                                maskMsg: '正在加载 ' + menuItem.text + '...'
                            },
                            listeners: {
                                update: {
                                    fn: function(tab, cfg) {
                                        cfg.iframe.setHeight(cfg.iframe.getSize().height - 1);
                                    },
                                    scope: this,
                                    single: true
                                }
                            }
                        });
                    }
                    TabPanelTasks.setActiveTab(tab);
                }
            </script>

        </Body>
        <Listeners>
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
