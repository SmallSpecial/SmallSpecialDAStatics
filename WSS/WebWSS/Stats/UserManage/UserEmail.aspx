<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEmail.aspx.cs" Inherits="WebWSS.Stats.UserManage.UserEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .pageBtn {
            margin-right:5px;
            z-index: 2;
            color: #23527c;
            background-color: #eee;
            border-color: #ddd;
            position: relative;
            top: -12px;
        }
        .tool div {
            display:inline-block;
            margin-right:30px;
        }
        .btnQuery {
            cursor:pointer;
            border:solid 2px #ccc;
        }
         #tip {
            position: absolute;
            background-color: #d45b5b;
            top: 70px;
            left: 400px;
        }
    </style>
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <link href="../../Script/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-tab.js"></script>
    <script src="../../Script/bootstrap-3.3.7/js/bootstrap-table.js"></script>
    <script src="../../Script/Ui/Lang.<%=PageLanguage %>.js"></script>
    <script src="../../Script/Ui/MoneyStatic.<%=PageLanguage %>.js"></script>
    <script src="../../Script/Ui/GridConfig.js"></script>
    <script src="../../Script/Ui/InitGrid.js"></script>
    <script type="text/javascript" >
        var time = '<%= DateTime.Now.ToString("yyyy-MM-dd")%>';
        var grid = gridList.UserEmail;
        var limit =30;
        SetPageLimit(limit);
        var page = 0;
        var begin = page * limit + 1;
        var end = (page + 1) * limit;
        function getAjax(url,jsonstring, succ) {
            $.ajax({
                url: url,
                data:jsonstring,
                type: 'post',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response, statue) {
                    if (succ)
                        succ(response, statue);
                },
                error: function (response, statue) {
                    var json = JSON.stringify(response);
                    $('#tip').show();
                    $('#tip').html(json);
                }
            });
        }
        function Query(param) {
            $('#tip').hide();
            var sys = "{'tag':'" + grid + "','time':'" + time + "','otherParam':'zoneid=" + $('#sltZone option:selected').val() + "','start':" + begin + ",'end':" + end + "}";
            if (param == undefined) param = sys;
            var url = 'StaticTemplate.aspx/GetAjaxData';
            getAjax(url, param, function (response, statue) {
                var data = response.d;
                if (data.Result == false)
                {
                    $('#tip').show();
                    $('#tip').html(data.Message);
                    return;
                }
                var rows = data.Data;
                drawGrid($('#grid'), rows, gridConfig.UserEmail.Columns, data.Count);
            });
        }
        $(function () {
            queryZoneCache(function () {
                bindZone($('#sltZone'));
            });
            Query();
            $('#btnQuery').click(function () {
                var zone = $('#sltZone').val();
                var page = $('li.active>span').attr('tabindex');
                if (isNaN(page)) page = 1;
                page = parseInt(page);
                var start = page * limit + 1;
                var end = (page + 1) * limit;
                var sys = "{'tag':'" + grid + "','time':'" + time + "','otherParam':'zoneid=" + zone + "','start':" + start + ",'end':" + end + "}";
                Query(sys);
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <div id="pagion">
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
        <div id="tool" class="tool" style="margin-top:20px;">
              <div>
                <label><%=GetResource("LblZone") %></label>
                <span>
                    <select id="sltZone" class="btn dropdown-toggle btn-default" data-live-search="true"  >
                        <option  value="-1"><%= GetResource("lblAllZone") %></option>
                    </select>
                </span>
            </div>
              <div>
                  <span class="btnQuery" id="btnQuery"><%= GetResource("BtnQuery") %></span>
              </div>
        </div>
        <div id="grid" data-toggle="table" class="table-striped"></div>
    </div>
    <div id="tip">

    </div>
    </form>
</body>
</html>
