<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserActiveMountStatics.aspx.cs" Inherits="WebWSS.Stats.UserManage.UserActiveMountStatics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .timeSpans div {
            display:inline-block;
            height: 50px;
        }
        .timeSpan {
            border:solid 2px #213838;
            margin-right: 10px;
            padding: 10px 10px;
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
    </style>
    <link href="../../Script/CommonCss/common.css" rel="stylesheet" />
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <script src="../../Script/Ui/Lang.<%= PageLanguage  %>.js"></script>
    <script src="../../Script/Ui/GridConfig.js"></script>
     <script src="../../Script/Browser.js"></script>
    <script src="../../Script/Ui/GameConfig.<%= PageLanguage  %>.js"></script>
    <link href="../../Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-table.js"></script>
    <script src="../../Script/Ui/InitGrid.js"></script>
    <script type="text/javascript">
        var web = webUrl();
        var today = '<%= DateTime.Now.ToString("yyyy-MM-dd")%>';//今天
        var day = '<%= DateTime.Now.AddDays(-1)%>';//查询昨日数据
        var url =  'Stats/UserManage/StaticTemplate.aspx/GetAjaxData';
       
        $(function () {
            DrawElement(btnQueryClick);
            queryZoneCache(function () {
                bindZone($('#sltZone'));
                query();
            });
            $('#btnRefresh').on('click', function () {
                query();
            });
        });
        var query = function (dateStr) {
           
            var zone = $('#sltZone').val();
            $('#tip').hide();
            if (zone == undefined) {
                $('#tip').html('error');
                $('#tip').show();
                return;
            }
            if (dateStr == undefined){
                dateStr = today;
            }
            var param = "{'tag':'" + gridList.UserActiveMountStatics + "','time':'" + dateStr + "','otherParam':'zoneid=" + zone + "','start':0,'end':30}";
            var gridUrl = web + "/" + url;
            AspxAjaxPost(gridUrl, param, function (response, statue) {
                var json=response.d;
                drawGrid($('#grid'), json.Data, gridConfig.UserActiveMountStatics.Columns, json.Count);
            });
        };
        function btnQueryClick() {
            var time = $('.timeSpanClick').text();
            query(time);
        }
        function stringConvertTime(timeStr) {
            var time = new Date(timeStr);
            return time;
        }
        function GetBeforeDay(now, before) {//now is string
            var time = stringConvertTime(now);
            time.setDate(time.getDate() - before);
            return time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate()
        }
        var showDay = 30;//显示多少天内的数据
        function DrawElement(queryCall,mul) {//第几次回溯【>0 几天前，<0 几天后】
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
            while (num >= end) {
                var tmpl = $.format('<div><span class="timeSpan" before="{0}" time="{1}">{1}</span></div>', num, GetBeforeDay(today, num));
                span.push(tmpl);
                num--;
            }
            if (span.length == 0) {
                return;//最多查询到名称
            }
            if (num > 0) {
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
                queryCall();
            });
            $('.dayBack').on('click', function () {//日期回溯
                var obj = $(this);
                var mul = parseInt(obj.attr('mul'));
                if (isNaN(mul)) {//参数绑定失败
                    console.warn('bind dayBack of mul error');
                    return;
                }
                DrawElement(queryCall,mul);
            });
            //初始加载默认选择昨天
            var lst = $('.timeSpan').last();
            lst.trigger('click');
            sltDay = lst.attr('before');
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
                    <div style="display:inline-block;">
                        <label><%=GetResource("LblZone") %></label>
                        <span>
                            <select id="sltZone" class="btn dropdown-toggle btn-default" data-live-search="true"  >
                                <option  value="-1"><%= GetResource("lblAllZone") %></option>
                            </select>
                        </span>
                    </div>
                    <div id="pagion" style="display:inline-block;">
                         <div class="pageInfo" style="display:inline;margin-right: 45px;">

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
