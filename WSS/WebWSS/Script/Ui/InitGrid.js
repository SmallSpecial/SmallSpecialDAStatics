//rely the jquery and bootstrap
function drawGrid(gridEle, rows, columns,whereTotal,toolEle,keepPagion) {
    var g = $(gridEle);
    var limit = GetPageLimit();
    var totla = whereTotal == undefined ? rows.length : whereTotal;
    var pageEle = toolEle;// 'pagion';
    if (pageEle == undefined || pageEle.length == 0)
    {
        pageEle = $('#pagion');
    }
    if (keepPagion != true)
    {
        initPageTool(g, totla, pageEle);
    }
    var limit = parseInt(GetPageLimit());
    if (rows.length > limit) {
        var pageRow = [];
        for (var i = 0; i < limit; i++) {
            pageRow.push(rows[i]);
        }
        rows = pageRow;
    }
    //row :data('bootstrap.table')
    g.bootstrapTable({
        //url: '/VenderManager/TradeList',     //请求后台的URL（*）  
        method: 'post',           //请求方式（*）  
        toolbar:  '#toolbar',        //工具按钮用哪个容器  
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
    $('.fixed-table-loading').hide();
}
function initPageTool(gridEle, total, toolEle) {//初始化分页工具
    var g = gridEle;
    var limit = GetPageLimit();//分页数
    if (isNaN(limit)) {
        limit = GetPageLimit();
        g.data(dataField.pageSize, limit);
    };
    var pageIndex = g.data(dataField.pageNumber);//页码 
    if (isNaN(pageIndex))
    {
        pageIndex =0;
        g.data(dataField.pageNumber, pageIndex);
    }
    var pages = parseInt(total / limit + (total % limit > 0 ? 1 : 0));//总页数
    pages = pages == 0 ? 1 : pages;
    g.data(dataField.pageTotal, pages - 1);//页数
    g.data(dataField.total, total);//数据量
    var p = pageIndex;
    var tmpl = '<li class="pageIndex"><span tabindex="{0}" >{1}</span></li>';
    var items = [];
    
    if (p <= 1) {
        p = 1;
    }
    if (p - 5 > 0) {
        p = p - 5;
    } else {
        p = 1;
    }
    var max = pages - p > 10 ? parseInt(p) + 9 : pages; //最多显示10个分页项
    while (p <= max) {
        items.push($.format(tmpl, p - 1, p));
        p++;
    }
    var pagiontool = toolEle.find(' ul');
    pagiontool.html(items.join(""));
   
    var start = pageIndex * limit + 1;
    var end = (pageIndex + 1) * limit;
    toolEle.find('.pageInfo').text($.format(lang.tip.pageInfo, (pageIndex + 1), pages, total, start, end));
    $('[tabindex=' + (pageIndex) + ']').parent().addClass('active');
    if (max == 0) {
        return;
    }
    var parentId = toolEle.attr('id');
    $('.pageIndex>span').bind('click', function () {//内部不能调用toolEle
        var lis = $(this).closest('ul');//查找该列表的上级元素 ul
        lis.find('.active').removeClass('active');
        var obj = $(this);
        obj.parent().addClass('active');
        var g = $('#grid');
        var index = obj.attr('tabindex');
        g.data(dataField.pageNumber, parseInt(index));
    });
}
function bindZone(zoneEle) {
    if (zoneEle == undefined)
    {
        return;
    }
    var itemTmpl = '<option  value="{0}">{1}</option>';//加载数据到元素上
    var zones = window.localStorage[AppCache.GameZone];
    var items = [];
    var data = JSON.parse(zones);
    for (var i in data) {
        items.push($.format(itemTmpl, i, data[i]));
    }
    $(zoneEle).html(items.join(''));
}
function webUrl() {
    var host = "http://"+window.location.host;
    return host;
}
function queryZoneCache(succ) {//所有缓存数据加载完毕之后查询列表页请求数据
    var cache = window.localStorage;
    if (succ == undefined) {
        succ = function () { }
    }
    if (cache[AppCache.GameZone] == undefined || cache[AppCache.GameZone] == '{}') {
        $.ajax({
            url: webUrl() + '/stats/usermanage/statictemplate.aspx/GetAjaxCache',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response, statue) {
                var data = response.d.Data[AppCache.GameZone];
                var json = JSON.stringify(data);
                cache[AppCache.GameZone] = json;
                succ();
            },
            error: function (response,statue) {

            }
        });
    }
    else {
        succ();
    }
}
function SetPageLimit(limit) {
    if (limit < 1)
    {
        limit = 1;
    }
    window.localStorage[AppCache.PageLimit] = limit;
}
function GetPageLimit() {
    var limit = window.localStorage[AppCache.PageLimit];
    if (limit == undefined)
    {
        limit = 30;
        SetPageLimit(limit);
    }
    return limit;
}
function AspxAjaxPost(url,param, succCall) {//aspx app request ajax rule:the url container the param
    $.ajax({
        url: url,
        data:param,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response, statue) {
            if(succCall)
                succCall(response, statue);
        },
        error: function (response, statue) {

        }
    });
};
var DrwaTimeTools = {//绘制时间按钮列表
    stringConvertTime: function (timeStr) {
        var time = new Date(timeStr);
        return time;
    },
    GetBeforeDay:function (now, before) {//now is string
        var time = DrwaTimeTools.stringConvertTime(now);
        time.setDate(time.getDate() - before);
        var month = time.getMonth() + 1;
        var monthStr = month < 10 ? "0" + month : month;
        return time.getFullYear() + '-' + monthStr + '-' + time.getDate()
    },
    DrawElement: function (timesEle, showElesCount, today, mul, queryCall) {//点击查询的触发事件，第几次回溯【>0 几天前，<0 几天后】，今天【2017-08-21】
        var p = isNaN(parseInt(mul)) ? 1 : parseInt(mul);
        var lastS = '';
        if ($('.timeSpan').length > 0)
            lastS = $('.timeSpan').first().attr('before');
        var lastBefore = isNaN(parseInt(lastS)) ? 0 : parseInt(lastS);
        var showDay = showElesCount ? showElesCount : 30;//显示多少天内的数据
        var num = lastBefore + showDay * p;
        var end = num - showDay;
        var span = [];
        span.push('<div><span>' + lang.label.time + ':</span></div>');
        span.push('<div><span class="dayBack"  mul="1" title="' + lang.tip.pursueForwrd + '"> << </span></div>');
        // start  end 
        var theDay = $('.timeSpanClick').length == 0 ? today : $('.timeSpanClick').attr('.time');
        while (num >= end) {
            var tmpl = $.format('<div><span class="timeSpan" before="{0}" time="{1}">{1}</span></div>', num, DrwaTimeTools.GetBeforeDay(today, num));
            span.push(tmpl);
            num--;
        }
        if (span.length == 0) {
            return;//最多查询到名称
        }
        if (num > 0) {
            span.push('<div><span class="dayBack"   mul="-1" title="' + lang.tip.pursueLater + '"> >> </span></div>');
        }
        if (queryCall) {
            span.push('<div><span class="queryBtn">' + lang.cmd.btnQuery + '</span></div>');
        }
        $(timesEle).html(span.join(' '));
        $('.timeSpan').on('click', function () {
            $('.timeSpan').removeClass('timeSpanClick');
            var obj = $(this);
            obj.addClass('timeSpanClick');
        });
        $('.queryBtn').on('click', function () {
            queryCall();
        });
        $('.dayBack').on('click', function () {//日期回溯
            var obj = $(this);
            var mul = parseInt(obj.attr('mul'));
            if (isNaN(mul)) {//参数绑定失败
                console.warn('bind dayBack of mul error');
                return;
            }
            DrwaTimeTools.DrawElement(timesEle, showDay, today, mul, queryCall);
        });
        //初始加载默认选择昨天
        var lst = $('.timeSpan').last();
        lst.trigger('click');
        sltDay = lst.attr('before');
    }
};