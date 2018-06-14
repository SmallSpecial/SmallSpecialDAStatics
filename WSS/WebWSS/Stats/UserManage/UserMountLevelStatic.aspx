<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMountLevelStatic.aspx.cs" Inherits="WebWSS.Stats.UserManage.UserMountLevelStatic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <script src="../../Script/Ui/Lang.<%=  PageLanguage %>.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-table.js"></script>
    <script src="../../Script/Ui/GridConfig.js"></script>
    <script src="../../Script/Ui/InitGrid.js"></script>
    <style type="text/css">
        .timeSpans>div {
            display:inline-block;
            height: 50px;
        }.timeSpan {
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
    <script type="text/javascript">
        var time = '<%=DateTime.Now.ToString(SmallDateTimeFormat)%>';
        var param = "{'tag':'" + gridList.UserMountLevelStatic + "','time':'{0}','otherParam':'zoneid={1}','start':" + 0 + ",'end':" + 30 + "}"
        function query(time,zone) {
            AspxAjaxPost(webUrl() + '/Stats/UserManage/StaticTemplate.aspx/GetAjaxData',
                $.format(param, time, zone),
                function (response, statue) {
                    var json = response.d;
                    drawGrid($('#grid'), json.Data, gridConfig.UserMountLevelStatic.Columns, json.Count);
            });
        }
        $(function () {
            DrwaTimeTools.DrawElement($('.timeSpans'), 30, time, 1, bindQuery);
            queryZoneCache(function () {
                bindZone($('#sltZone'));
                bindQuery();
            });
            $('#btnRefresh').click(function () {
                var zone = $('#sltZone').val();
                $('.timeSpan').removeClass('timeSpanClick');
                $('.timeSpan[time=' + time + ']').addClass('timeSpanClick');
                query(time, zone);
            });
        });
        function bindQuery() {
            var queryTime = $('.timeSpan.timeSpanClick').html();
            var zone = $('#sltZone').val();
            query(queryTime, zone);
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
