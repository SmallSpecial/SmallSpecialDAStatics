<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticTemplate.aspx.cs" Inherits="WebWSS.Stats.StaticTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .load {
            height: 30px;
            background-color: #608e7b;
            width: 300px;
            position: absolute;
            top: 250px;
            left: 450px;
            padding: 50px;
        }
        .th-inner {
            width:80px;
        }
        .fixed-table-container {
            margin-top:20px;
        }
        .fixed-table-loading {
            display:none;
        }
        .timeSpans div {
            display:inline-block;
            margin-bottom: 25px;
        }
        .timeSpan {
            border:solid 2px #213838;
            margin-right: 10px;
            padding: 10px 10px;
            cursor:pointer;
        }
        .dayBack {
            cursor:pointer;
        }
        .timeSpanClick {
            color: red;
        }
        .queryBtn {
            padding: 7px;
            border: solid 2px #52cdda;
            cursor:pointer;
        }
        .pagination span {
            margin-right:10px;
            position: relative;
            top: 12px;
        }
        .pageIndex {

        }
        .pageBtn {
            margin-right:5px;
            z-index: 2;
            color: #23527c;
            background-color: #eee;
            border-color: #ddd;
        }
        #tip {
            position: absolute;
            background-color: #d45b5b;
            top: 300px;
        }
        input, select {
            height: 35px;
        }
    </style>
    <script src="../../Script/jquery-1.12.3.min.js" type="text/javascript"></script>
    <link href="../../Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap.js" type="text/javascript"></script> 
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-table.js"></script>
   <%-- <script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.11.1/bootstrap-table.min.js" type="text/javascript"></script>--%>
<%--    <script src="../Script/bootstrap-3.3.7/js/bootstrap-paginator.js"></script>--%>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-select.js"></script>
    <script src="../../Script/Ui/GameConfig.<%= PageLanguage %>.js"></script>
    <script src="../../Script/Ui/Lang.<%=PageLanguage %>.js"></script>
    <script src="../../Script/Ui/GridConfig.js"></script>
     <%-- ie8- 不能调用json--%>
    <!--[if IE]>
        <script src="../../Script/Browser.js" type="text/javascript"></script>
    <![endif]-->
    <script type="text/javascript">
        var today = '<%= DateTime.Now.ToString("yyyy-MM-dd")%>';//今天
        var jsNow= stringConvertTime(today);
        var day = '<%= DateTime.Now.AddDays(-1)%>';//查询昨日数据
        var month = '<%=DateTime.Now.Month%>';
        var url = '<%= Request.QueryString%>';// var url = 'grid=ActionRoom&category=Grouding'; 首个参数为查询的对象 
        var hiddenTime = '<%= Request.QueryString["hiddenTimeSpanTool"]%>';
        var grid = url.split('&')[0].split('=')[1];//查询的对象
        var showDay = 30;//显示多少天内的数据
        var sltDay = 0;//当前操作选中了几天前
        var gridColumns = {
            'AuctionRoom': [
                    { field: 'Top', title: '数目' },
                    { field: 'Money', title: '金额' },
                    { field: 'User', title: '用户' },
                    { field: 'Role', title: '角色名' }
            ],
            'GMUser': [
                { field: 'UserId', title: lang.column.userNo },
                { field: 'UserName', title: lang.column.userName },
                { field: 'IP', title: lang.column.AuthorPCIP }
            ]
        }
        function InitGrid(columns, data) {
            var g = $('#grid');
            var limit = g.data(dataField.pageSize);
            initPageTool(data.Count);
            var rows = data.Data;
            //row :data('bootstrap.table')
            g.bootstrapTable({
                //url: '/VenderManager/TradeList',     //请求后台的URL（*）  
                method: 'post',           //请求方式（*）  
                toolbar: '#toolbar',        //工具按钮用哪个容器  
                striped: true,           //是否显示行间隔色  
                cache: false,            //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）  
                pagination: false,          //是否显示分页（*）  
                sortable: false,           //是否启用排序  
                sortOrder: "asc",          //排序方式  
                queryParams: function (pageParam) {
                    return pageParam;
                },//传递参数（*）
                sidePagination: "server",      //分页方式：client客户端分页，server服务端分页（*）  
                pageNumber: 1,            //初始化加载第一页，默认第一页  
                pageSize: limit,            //每页的记录行数（*）  
                pageList: [10, 25, 50, 100],    //可供选择的每页的行数（*）  
                strictSearch: true,
                clickToSelect: true,        //是否启用点击选中行  
                height: 460,            //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度  
                uniqueId: "id",           //每一行的唯一标识，一般为主键列  
                cardView: false,          //是否显示详细视图  
                detailView: false,          //是否显示父子表  
                columns: columns,
                data: rows
            });
        }
        function initPageTool(total) {//初始化分页工具
            var g = $('#grid');
            var limit = g.data(dataField.pageSize);//分页数
            var pageIndex = g.data(dataField.pageNumber);//页码 
            var pages = parseInt(total / limit + (total % limit > 0 ? 1 : 0));//总页数
            g.data(dataField.pageTotal, pages-1);//页数
            g.data(dataField.total, total);//数据量
            var p = pageIndex;
            var tmpl = '<li class="pageIndex"><span tabindex="{0}" >{1}</span></li>';
            var items = [];
            if (p <= 1) {
                p = 1;
            }
            if (p - 5 > 0) {
                p = p - 5;
            }
            var max = pages - p > 10 ? parseInt(p) + 9 : pages; //最多显示10个分页项
            while (p <= max) {
                items.push($.format(tmpl,p - 1, p));
                p++;
            }
            $('#pagion ul').html(items.join(""));
            var pagiontool = $('#pagion ul');
            var start = pageIndex * limit + 1;
            var end = (pageIndex+1) * limit;
            $('#pageInfo').text($.format(lang.tip.pageInfo,(pageIndex+1), pages, total, start, end));
            $('[tabindex=' + (pageIndex) + ']').parent().addClass('active');
            if (max== 0) {
                return;
            }
            $('.pageIndex>span').bind('click', function () {
                $('.active').removeClass('active');
                var obj = $(this);
                obj.parent().addClass('active');
                var g = $('#grid');
                var index =obj.attr('tabindex');
                g.data(dataField.pageNumber, parseInt(index));
                Query_InitGrid();
            });
        }
        function queryData(succ) {
            $('#tip').hide();
            var day = $('.timeSpanClick').attr('time');
            var g = $('#grid');
            var pindex = g.data(dataField.pageNumber);
            var pagesize = g.data(dataField.pageSize);
            var start = pindex * pagesize + 1;
            var end = (pindex + 1) * pagesize;
            $('.load').show();
            var zone = $('#sltZone').val();
            var other = 'zoneid=' + zone;
            $.ajax({
                url: 'StaticTemplate.aspx/GetAjaxData',
                //"{newsID:"+ newsID +",name:"+ name +"}",
                data: "{'tag':'" + grid + "','time':'" + day + "','otherParam':'" + other + "','start':" + start + ",'end':" + end + "}",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response,statue) {//在asp.net中返回数据没有使用json进行处理返回的数据为[d：data]
                    succ(response.d);
                }, error: function (response, statue) {
                    var json = JSON.stringify(response);
                    $('#tip').html(json);
                },
                complete: function () {
                    $('.load').hide();
                }
            });
        }
        function getAjax(url,succ) {
            $.ajax({
                url: url,
                type: 'post',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response, statue) {
                    if(succ)
                         succ(response, statue);
                },
                error: function (response, statue) {

                }
            });
        }
        $(function () {
            //判断当前是否显示时间列表
            if (hiddenTime!= "true") {
                DrawElement();
            }
            var grid = $('#grid');
            grid.data(dataField.pageNumber, 0);
            grid.data(dataField.pageSize, 30);
            queryCache();//加载缓存数据
            $('#btnRefresh').bind('click', function () {
                $('#grid').data(dataField.pageNumber, 0);
                Query_InitGrid();
            });
            $('#btnFirst').bind('click', function () {
                $('#grid').data(dataField.pageNumber, 0);
                Query_InitGrid();
            });
            $('#btnLast').bind('click', function () {
                var g = $('#grid');
                g.data(dataField.pageNumber,g.data(dataField.pageTotal));
                Query_InitGrid();
            });
        });
        function queryCache() {//所有缓存数据加载完毕之后查询列表页请求数据
            var cache = window.localStorage;
            if (cache[AppCache.GameZone] == undefined || cache[AppCache.GameZone]=='{}') {
                getAjax('StaticTemplate.aspx/GetAjaxCache', function (response, statue) {
                    var data = response.d.Data[AppCache.GameZone];
                    var json = JSON.stringify(data);
                    cache[AppCache.GameZone] = json;
                    bindZone();//ajax异步执行不能保证执行完之后才调用ajax外的语句
                    Query_InitGrid();
                });
            }
            else {
                bindZone();
                Query_InitGrid();
            }
        }
        function bindZone() {
            var itemTmpl = '<option  value="{0}">{1}</option>';//加载数据到元素上
            var zones = window.localStorage[AppCache.GameZone];
            var items = [];
            var data=JSON.parse(zones);
            for (var i in data) {
                items.push($.format(itemTmpl,i,data[i]));
            }
            $('#sltZone').html(items.join(''));
        }
        function DrawElement(mul) {//第几次回溯【>0 几天前，<0 几天后】
            var p = isNaN(parseInt(mul)) ? 1 : parseInt(mul);
            var lastS = '';
            if ($('.timeSpan').length > 0)
                lastS = $('.timeSpan').first().attr('before');
            var lastBefore = isNaN(parseInt(lastS)) ? 0 : parseInt(lastS);

            var num = lastBefore + showDay * p;
            var end = num - showDay;
            var span = [];
            span.push('<div><span>' + lang.label.time + ':</span></div>');
            span.push('<div><span class="dayBack"  mul="1" title="' + lang.tip.pursueForwrd + '"><%="<<" %></span></div>');
            // start  end 
            var theDay = $('.timeSpanClick').length == 0 ? today : $('.timeSpanClick').attr('.time');
            while (num > end) {
                var tmpl =$.format( '<div><span class="timeSpan" before="{0}" time="{1}">{1}</span></div>',num, GetBeforeDay(today, num));
                span.push(tmpl);
                num--;
            }
            if (span.length == 0) {
                return;//最多查询到名称
            }
            if (num>0) {
                span.push('<div><span class="dayBack"   mul="-1" title="' + lang.tip.pursueLater + '"><%=">>" %></span></div>');
            }
            span.push('<div><span class="queryBtn">' + lang.cmd.btnQuery + '</span></div>');
            $('.timeSpans').html(span.join(' '));
            $('.timeSpan').on('click', function () {
                $('.timeSpan').removeClass('timeSpanClick');
                var obj = $(this);
                obj.addClass('timeSpanClick');
            });
            $('.queryBtn').on('click', function () {
                Query_InitGrid();
            });
            $('.dayBack').on('click', function () {//日期回溯
                var obj = $(this);
                var mul = parseInt(obj.attr('mul'));
                if (isNaN(mul)) {//参数绑定失败
                    console.warn('bind dayBack of mul error');
                    return;
                }
                DrawElement(mul);
            });
            //初始加载默认选择昨天
            var lst = $('.timeSpan').last();
            lst.trigger('click');
            sltDay = lst.attr('before');
        }
        function stringConvertTime(timeStr) {
            var time = new Date(timeStr);
            return time;
        }
        function GetBeforeDay(now, before) {//now is string
            var time =stringConvertTime(now);
            time.setDate(time.getDate() - before);
            return time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate()
        }
        function Query_InitGrid() {
            queryData(function (json, statue) {
                if (json.Result == false) {
                    $('#tip').text(json.Message);
                    $('#tip').show();
                    return;
                }
                InitGrid(gridColumns[grid], json);
               
            });
        }
        function CacheConfig() {
            var cache = window.localStorage;
            if (cache.length == 0) {
                cache.setItem("gameOpidConfig",JSON.stringify( gameOpidConfig));
            }
            return cache;
        }
        function GetOpidConfig() {
            var cache = CacheConfig();
            return JSON.parse(cache.getItem("gameOpidConfig"));
        }
        var opidConfig = GetOpidConfig();
        window.onerror = function () {
            var msg = arguments;
            if (Reporter)
                Reporter.sender(msg);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top:10px;">
            <div>
                <div id="headTool">
                    <div class="timeSpans">
                       
                    </div>
                </div>
                <div id="toolbar" >
                    <div>
                        <label><%=GetResource("LblZone") %></label>
                        <span>
                            <select id="sltZone" class="btn dropdown-toggle btn-default" data-live-search="true"  >
                                <option  value="-1"><%= GetResource("lblAllZone") %></option>
                            </select>
                        </span>
                    </div>
                </div>
                <div id="pagion">
                         <div id="pageInfo" style="display:inline;margin-right: 45px;">

                        </div>
                        <div style="display:inline;">
                            <div class="pageBtn btn"  style="display:inline;">
                                    <span id="btnFirst">&laquo;</span>
                            </div>
                            <div  style="display:inline;">
                                <ul class="pagination" style="margin: 0;"> <%--此处margin: 0; 干扰bootstrap样式--%>
	                                <li class="active"><span tabindex="1">1</span></li>
	                                <li class="disabled" style="display:none;"><span tabindex="2">2</span></li> 
                                </ul>
                            </div>
                            <div class="pageBtn btn"  style="display:inline;">
                                <span id="btnLast">&raquo;</span>
                            </div>
                            <div class="pageBtn btn"  style="display:inline;">
                                    <span id="btnRefresh"><%= GetResource("BtnRefresh") %></span>
                            </div>
                     </div>
                </div>
                <table id="grid" data-toggle="table" class="table-striped"> </table>
            </div>
             <div class="load" style="display:none;">
                 <%=GetResource("Tip_PageLoading") %>
             </div>
        </div>
        <div id="tip">

        </div>
    </form>
</body>
</html>
