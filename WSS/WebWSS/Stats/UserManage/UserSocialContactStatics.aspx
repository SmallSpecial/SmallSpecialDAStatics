<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSocialContactStatics.aspx.cs" Inherits="WebWSS.Stats.UserManage.UserAddFriendStatics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Script/CommonCss/common.css" rel="stylesheet" />
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <link href="../../Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-table.js"></script>
    <script src="../../Script/Ui/Lang.<%= PageLanguage %>.js"></script>
    <script src="../../Script/Ui/GridConfig.js"></script>
    <script src="../../Script/Ui/InitGrid.js"></script>
    <script type="text/javascript">
        var title = '';//用户添加好友日志
        var time = '<%= DateTime.Now.ToString(SmallDateTimeFormat)%>';
        var grid = '<%=Request.QueryString["grid"] %>';//根据参数来选择加载数据的类型
        if (grid.trim() == '') {
            grid = gridList.UserAddFriendLog;
        }
        var paramTpl = "{'tag':'" + grid + "','time':'{0}','otherParam':'zoneid={1}','start':" + 0 + ",'end':" + 30 + "}";
        $(function () {
            DrwaTimeTools.DrawElement($('.timeSpans'), 30, time, 1, bindQuery);
            queryZoneCache(function () {
                bindZone($('#sltZone'));
                bindQuery();
            });
        });
        function bindQuery() {
            var zone = $('#sltZone').val();
            var t = $('.timeSpan.timeSpanClick').html();
            if (!t)
                t = time;
            var param = $.format(paramTpl, t, zone);
            $('.load').show();
            AspxAjaxPost(webUrl() + '/Stats/UserManage/StaticTemplate.aspx/GetAjaxData', param,
                function (response, statue) {
                    $('.load').hide();
                    var arr = response.d;
                    var tip = $('#tip');
                    if (!arr.Result) {
                        tip.show();
                        tip.text(arr.Message);
                        return;
                    } else {
                        tip.hide();
                        if (arr.Data==null||arr.Data.length < 2) {
                            tip.text(lang.tip.error);
                            tip.show();
                            return;
                        }
                        var add = arr.Data[0];//添加的好友
                        var del = arr.Data[1];
                        $(add).each(function (i, ele) {
                            ele.index = i;
                        });
                        $(del).each(function (i, ele) {
                            ele.index = i;
                        });
                        var addg = $('#addEle');
                        addg.find('ul').data('rows', { data: add, container: addg });
                        var delg = $('#delEle');
                        delg.find('ul').data('rows', { data: del, container: delg });
                        drawGridWithBindPage($('#addGrid'), add, gridConfig.UserAddFriendLog.Columns, add.length, addg.find('.pagion'));
                        drawGridWithBindPage($('#delGrid'), del, gridConfig.UserAddFriendLog.Columns, del.length, delg.find('.pagion'));
                        $('.pageIndex span').bind('click', function () {
                            //选择页码进行页码数据替换显示
                            var obj = $(this);
                            pageIndexQuery(obj);
                        });
                    }
            });
        }
        function drawGridWithBindPage(gridEle,row,columns,total,pagionEle,keepPagion) {
            drawGrid(gridEle, row, columns, total, pagionEle, keepPagion);
        }
        function pageIndexQuery(obj) {
            var ul = obj.closest('ul');
            if (ul.length == 0) {//此处重绘表格时会再次进入
                return;
            }
            var d = ul.data('rows');
            var data = d.data;
            if (data.length == 0) {
                return;
            }
            var limit = parseInt(GetPageLimit());//每页数量
            var select = parseInt(obj.attr('tabindex'));//选择第几页
            var start = select * limit ;
            var end = (select + 1) * limit;
            if (end > data.length) {
                end = data.length - 1;
            }
            var row = [];
            for (var i = start; i <= end; i++) {
                row.push(data[i]);
            }
            var grid = d.container.find('table[id]');
            drawGridWithBindPage(grid, row, gridConfig.UserAddFriendLog.Columns, data.length, d.container.find('.pagion'),true);
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
                     <div style="display:inline-block;">
                        <label><%=GetResource("LblZone") %></label>
                        <span>
                            <select id="sltZone" class="btn dropdown-toggle btn-default" data-live-search="true"  >
                                <option  value="-1"><%= GetResource("lblAllZone") %></option>
                            </select>
                        </span>
                    </div>
                </div>
                <div id="toolbarTop" >
                   
                    
                </div>
                
               <div>
                   <div id="addEle" style="display:inline-block;width:47%;">
                       <div class="pagion" style="display:inline-block;">
                             <div class="pageInfo" style="display:inline;margin-right: 45px;">

                            </div>
                            <div style="display:inline;">
                                <div class="pageBtn btn"  style="display:inline;">
                                        <span class="btnFirst">&laquo;</span>
                                </div>
                                <div  style="display:inline;">
                                    <ul class="pagination" style="margin: 0;"> <%--此处margin: 0; 干扰bootstrap样式--%>
	                                    <li class="active"><span tabindex="1">1</span></li>
	                                    <li class="disabled" style="display:none;"><span tabindex="2">2</span></li> 
                                    </ul>
                                </div>
                                <div class="pageBtn btn"  style="display:inline;">
                                    <span class="btnLast">&raquo;</span>
                                </div>
                                <div class="pageBtn btn"  style="display:inline;">
                                        <span class="btnRefresh"><%= GetResource("BtnRefresh") %></span>
                                </div>
                            </div>
                        </div>
                       <table id="addGrid" data-toggle="table" class="table-striped"> </table>
                   </div>
                    <div id="delEle" style="display:inline-block;width:47%;margin-left:50px;">
                        <div class="pagion" style="display:inline-block;">
                             <div class="pageInfo" style="display:inline;margin-right: 45px;">

                            </div>
                            <div style="display:inline;">
                                <div class="pageBtn btn"  style="display:inline;">
                                        <span class="btnFirst">&laquo;</span>
                                </div>
                                <div  style="display:inline;">
                                    <ul class="pagination" style="margin: 0;"> <%--此处margin: 0; 干扰bootstrap样式--%>
	                                    <li class="active"><span tabindex="1">1</span></li>
	                                    <li class="disabled" style="display:none;"><span tabindex="2">2</span></li> 
                                    </ul>
                                </div>
                                <div class="pageBtn btn"  style="display:inline;">
                                    <span class="btnLast">&raquo;</span>
                                </div>
                                <div class="pageBtn btn"  style="display:inline;">
                                        <span class="btnRefresh"><%= GetResource("BtnRefresh") %></span>
                                </div>
                            </div>
                        </div>
                         <table id="delGrid" data-toggle="table" class="table-striped"> </table>
                    </div>
               </div>
            </div>
             <div class="load" style="display:none;">
                 <%=GetResource("Tip_PageLoading") %>
             </div>
        </div>
        <div id="tip" style="color:red;">

        </div>
    </form>
</body>
</html>
