/*=================================  (Old) Ajax Start =========================================
* 修改标识:Tc 20080707
*===============================================================================================*/

function XMLHttpObject(url, Syne) {
    var XMLHttp = null
    var o = this
    this.url = url
    this.Syne = Syne

    this.sendData = function() {
        if (window.XMLHttpRequest) {
            XMLHttp = new XMLHttpRequest();
        }
        else if (window.ActiveXObject) {
            XMLHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }

        with (XMLHttp) {
            open("GET", this.url, this.Syne);
            onreadystatechange = o.CallBack;
            send(null);
        }
    }

    this.OnComplete = function(responseText, responseXml) {
        // Complete
    }

    this.OnAbort = function() {
        // Abort
    }

    this.OnError = function(status, statusText) {
        // Error
    }

    this.CallBack = function() {
        if (XMLHttp.readyState == 1) {
            //this.OnLoading();
        }
        else if (XMLHttp.readyState == 2) {
            //this.OnLoaded();
        }
        else if (XMLHttp.readyState == 3) {
            //this.OnInteractive();
        }
        else if (XMLHttp.readyState == 4) {
            if (XMLHttp.status == 0)
                o.OnAbort();
            else if (XMLHttp.status == 200 && XMLHttp.statusText == "OK")
                o.OnComplete(XMLHttp.responseText, XMLHttp.responseXML);
            else
                o.OnError(XMLHttp.status, XMLHttp.statusText, XMLHttp.responseText);
        }
    }
}

/**************************** (Old)Ajax End *************************/

function SelectAll(objID) {
    var obj = undefined == objID ? document : $(objID);
    var chkother = obj.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox' && chkother[i].id.indexOf("newOnly") == -1) {
            if (chkother[i].checked)
                chkother[i].checked = false;
            else
                chkother[i].checked = true;
        }
    }
}

function chkAll_true() {
    var chkall = document.all["chkAll"];
    var j = 0;
    var chkother = document.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                if (chkall.checked == true) {
                    chkother[i].checked = true;
                }
                else {
                    chkother[i].checked = false;
                }
            }
        }
    }
}
function chkAll22() {
    var chkall = document.all["chkAll2"];
    var chkother = document.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('CBL') > -1) {
                if (chkall.checked == true) {
                    chkother[i].checked = true;
                }
                else {
                    chkother[i].checked = false;
                }
            }
        }
    }
}

function chkchoose_true() {
    var chkall = document.all["chkAll"];
    var j = 0;
    var chkother = document.getElementsByTagName("input");
    var str = "";
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('input') > -1) {
                if (chkall.checked == true) {
                    chkother[i].checked = true;
                    str += chkother[i].value + ",";
                }
                else {
                    chkother[i].checked = false;
                }
            }
        }
    }
    $("strids").value = str;
}

function ChkSelete() {
    var chkother = document.getElementsByTagName("input");
    var j = 0;
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                if (chkother[i].checked == true) {
                    j++;
                }
            }
        }
    }

    if (j == 0) {
        sAlert("请至少选择一条记录！", "", true);
        return false;
    }

    return true;
}

function del() {
    var chkother = document.getElementsByTagName("input");
    var j = 0;
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                if (chkother[i].checked == true) {
                    j++;
                }
            }
        }
    }

    if (j == 0) {
        sAlert("请至少选择一条记录！", "", true);
        return false;
    }
    else {
        return confirm("确定删除吗？");
    }
}


function edit() {
    var chkother = document.getElementsByTagName("input");
    var j = 0;
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                if (chkother[i].checked == true) {
                    j++;
                }
            }
        }
    }

    if (j == 0) {
        return alertmsg('必须选择一条记录才能修改！', '', false);
    }
    else if (j > 1) {
        return alertmsg('不能同时修改多条记录！', '', false);
    }
}

function Rect() {
    var chkother = document.getElementsByTagName("input");
    var j = 0;
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                if (chkother[i].checked == true) {
                    j++;
                }
            }
        }
    }

    if (j == 0) {
        return alert('必须选择一条记录才能推荐！', '', false);
    }
}


function QueryString() {
    //构造参数对象并初始化 
    var name, value, i;

    // var str = location.href;//获得浏览器地址栏URL串 
    // var num = str.indexOf("?") 
    //str = str.substr(num+1);//截取“?”后面的参数串 
    var str = location.search.substr(1);

    var arrtmp = str.split("&"); //将各参数分离形成参数数组 
    for (i = 0; i < arrtmp.length; i++) {
        var num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num).toLowerCase(); //取得参数名称 
            value = arrtmp[i].substr(num + 1); //取得参数值 
            this[name] = value; //定义对象属性并初始化 
        }
    }
}
var objRequest = new QueryString(); //使用new运算符创建参数对象实例 

/**
=====================================设置SEO信息 开始================================================
**/
//复制内容
function copyinfo(num) {
    switch (num) {
        case 1:

            $("tbsupplytitle").value = $("tbIndextitle").value;
            $("tbsupplydsp").value = $("tbindexdsp").value;
            $("tbsupplykw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("supply").checked = true;
            else
                $("supply").checked = false;



            $("hidSupplyNav").value = $("hidIndexNav").value;
            $("supplyNav").innerHTML = $("indexNav").innerHTML;
            break;
        case 2:
            $("tbdemandtitle").value = $("tbIndextitle").value;
            $("tbdemanddsp").value = $("tbindexdsp").value;
            $("tbdemandkw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("demand").checked = true;
            else
                $("demand").checked = false;

            $("hidDemandNav").value = $("hidIndexNav").value;
            $("demandNav").innerHTML = $("indexNav").innerHTML;
            break;
        case 3:
            $("tbbusinesstitle").value = $("tbIndextitle").value;
            $("tbbusinessdsp").value = $("tbindexdsp").value;
            $("tbbusinesskw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("business").checked = true;
            else
                $("business").checked = false;

            $("hidBusinessNav").value = $("hidIndexNav").value;
            $("businessNav").innerHTML = $("indexNav").innerHTML;
            break;
        case 4:
            $("tbengagetitle").value = $("tbIndextitle").value;
            $("tbengagedsp").value = $("tbindexdsp").value;
            $("tbengagekw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("engage").checked = true;
            else
                $("engage").checked = false;

            $("hidEngageNav").value = $("hidIndexNav").value;
            $("engageNav").innerHTML = $("indexNav").innerHTML;
            break;
        case 5:
            $("tbcorporationtitle").value = $("tbIndextitle").value;
            $("tbcorporationdsp").value = $("tbindexdsp").value;
            $("tbcorporationkw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("corporation").checked = true;
            else
                $("corporation").checked = false;

            $("hidCorporationNav").value = $("hidIndexNav").value;
            $("corporationNav").innerHTML = $("indexNav").innerHTML;
            break;
        case 6:
            $("tbaddresstitle").value = $("tbIndextitle").value;
            $("tbaddressdsp").value = $("tbindexdsp").value;
            $("tbaddresskw").value = $("tbindexkw").value;

            if ($("index").checked == true)
                $("address").checked = true;
            else
                $("address").checked = false;

            $("hidAddressNav").value = $("hidIndexNav").value;
            $("addressNav").innerHTML = $("indexNav").innerHTML;
            break;
    }
}

//新增关键词导航
function addkeyword() {
    if ($("txtname").value == "") {
        return alertmsg('关键字不能为空！', '', false);
    }
    else if ($("txturl").value == "") {
        return alertmsg('链接地址不能为空！', '', false);
    }
    else {
        switch ($("hidkeyflag").value) {
            case "1":
                $("hidindexNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("indexNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "2":
                $("hidSupplyNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("supplyNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "3":
                $("hidDemandNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("demandNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "4":
                $("hidBusinessNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("businessNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "5":
                $("hidEngageNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("engageNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "6":
                $("hidCorporationNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("corporationNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;
            case "7":
                $("hidAddressNav").value += "," + $("txtname").value + "$" + $("txturl").value;
                $("addressNav").innerHTML += "<a href=\"" + $("txturl").value + "\" title=\"" + $("txtname").value + "\">" + $("txtname").value + "</a>";
                break;

        }

        $("addnav").style.display = "none";
    }
}

//显示添加关键词导航添加层
function showaddnav(num) {
    $("txtname").value = "";
    $("txturl").value = "";
    $("hidkeyflag").value = num;
    $("addnav").style.display = "block";
    $("addnav").style.height = document.documentElement.scrollHeight;
    $("seoalphaBox").style.height = document.documentElement.scrollHeight + "px";
    if (navigator.appName == "Microsoft Internet Explorer")
        $("seoalphaBox").style.width = document.documentElement.scrollWidth + "px";
    else
        $("seoalphaBox").style.width = document.documentElement.scrollWidth + "px";
}

//初始化关键字导航信息
function onloadnav() {

    if ($("hidAddressNav").value != "") {
        var links = $("hidAddressNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("addressNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

    if ($("hidCorporationNav").value != "") {
        var links = $("hidCorporationNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("corporationNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

    if ($("hidEngageNav").value != "") {
        var links = $("hidEngageNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("engageNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

    if ($("hidBusinessNav").value != "") {
        var links = $("hidBusinessNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("businessNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }


    if ($("hidDemandNav").value != "") {
        var links = $("hidDemandNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("demandNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

    if ($("hidSupplyNav").value != "") {
        var links = $("hidSupplyNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("supplyNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

    if ($("hidindexNav").value != "") {
        var links = $("hidindexNav").value.split(',');
        for (var i = 0; i < links.length; i++) {
            var urls = links[i].split('$');
            $("indexNav").innerHTML += "<a href=\"" + urls[1] + "\" title=\"" + urls[0] + "\">" + urls[0] + "</a>";
        }
    }

}

/**
=====================================设置SEO信息 结束================================================
**/
/**
=====================================标签管理 开始================================================
**/
//选择标签样式类别
function selecttablename(num) {
    var select = $("scontent").getElementsByTagName("select");

    for (var i = 0; i < select.length; i++) {
        if (i + 1 != num) {
            select[i].style.display = "none";
            if (i + 1 < 8) {
                $("sel" + (i + 1)).style.display = "none";
            }
        }
        else {
            select[i].style.display = "block";
            if (num < 8 || num == 16) {
                $("sel" + num).style.display = "block";
            }
            else {
                var tmp = $("hidTableName").value;
                if (tmp < 8) {
                    $("sel" + tmp).style.display = "block";
                }
            }
        }
    }
}
//初始化
function InitListBox() {
    if ($("hidTableName").value != "") {
        selecttablename($("hidTableName").value)
    }
}

//选择标签样式
//2008-6-3 update By 蘇哥拉笛
function selectcolumuname(obj) {
    tagName = obj.name;
    tag = $(tagName).options.value;
    var myField;
    myField = $('txtConent');
    if (document.selection) {
        myField.focus();
        sel = document.selection.createRange();
        sel.text = tag;
        myField.focus();
    } else if (myField.selectionStart || myField.selectionStart == '0') {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd;
        var cursorPos = endPos;
        myField.value = myField.value.substring(0, startPos)
                                          + tag
                                          + myField.value.substring(endPos, myField.value.length);
        cursorPos += tag.length;
        myField.focus();
        myField.selectionStart = cursorPos;
        myField.selectionEnd = cursorPos;
    } else {
        myField.value += tag;
        myField.focus();
    }
}

//添加标签
function AddLabel() {
    if ($("tbName").value == "") {
        return alertmsg('名称不能为空！', '', false);
    } else {
        return true;
    }
    if ($("hidLT_ID").value == "") {
        return alertmsg('请选择所属栏目！', '', false);
    } else {
        return true;
    }
    if ($("tbContent").value == "") {
        return alertmsg('标签内容不能为空！', '', false);
    } else {
        return true;
    }
    if ($("txtConent").value == "") {
        return alertmsg('主体循环标记不能为空！', '', false);
    } else {
        return true;
    }
}

function setLabelValue(value) {
    if (value != "") {
        $("tbContent").value = "";
        $("tbContent").value = "{";
        $("tbContent").value += value;
        $("tbContent").value += "}";
    }
    $("Div_window").style.display = "none";
    $("window").style.display = "none";

    IsDisplaySelect(value);
    //selecttablename($F("hidTableName"));
}

//设置是否显示 
//08-06-24 Add By蘇哥拉笛
function IsDisplaySelect(value) {
    var num1 = (value.indexOf("$")) + 1;
    var num2 = value.indexOf("┆");
    var name = value.substring(num1, num2);
    var select = $("scontent").getElementsByTagName("select");

    switch (name) {
        case "SupplyList":
        case "SupplyPageList":
        case "SupplyKeyPageList":
        case "DemandList":
        case "DemandPageList":
        case "MachiningKeyPageList":
        case "BusinessList":
        case "BusinessPageList":
        case "InvestmentKeyPageList":
        case "SurrogateList":
        case "SurrogatePageList":
        case "ServiceKeyPageList":
        case "ShowList":
        case "ShowPageList":
        case "ExhibitionKeyPageList":
        case "BrandList":
        case "BrandPageList":
        case "EngageList":
        case "EngagePageList":
        case "CorporationList":
        case "CorporationPageList":
        case "UserNews":
            //case "AssociatorList":
            //case "AssociatorPageList":
            //case "TopicList":
            //case "TopicPageList":
            selecttablename($F("hidTableName"));
            break;
        default:
            name = "lst_" + value.substring(num1, num2);

            //临时处理
            if (name.toLowerCase() == "lst_associatorpagelist") name = "lst_AssociatorList";
            if (name.toLowerCase() == "lst_newspagelist") name = "lst_NewsList";
            if (name.toLowerCase() == "lst_topicpagelist") name = "lst_TopicList";

            for (var i = 0; i < select.length; i++) {
                if (select[i].id == name) {
                    select[i].style.display = "block";
                } else {
                    if (i + 1 < 8) {
                        $("sel" + (i + 1)).style.display = "none";
                    }
                    select[i].style.display = "none";
                }
            }
            break;
    }
}


function setChildNum(num) {
    $("hidTableName").value = num;
}

//弹出设置模态窗口
function setshow(num) {
    var url = "";
    var strset = "";
    var dWidth = 520;
    var dHeight = 320;
    $("hidTableName").value = num;

    switch (num) {
        case 1:
            url = "SupplySet.aspx";
            break;
        case 2:
            url = "DemandSet.aspx";
            break;
        case 3:
            url = "BusinessSet.aspx";
            break;
        case 4:
            url = "SurrogateSet.aspx";
            break;
        case 5:
            url = "ShowSet.aspx";
            break;
        case 6:
            url = "BrandSet.aspx";
            break;
        case 7:
            url = "EngageSet.aspx";
            break;
        case 8:
            url = "CorporationSet.aspx";
            break;
        case 9:
            url = "NewsSet.aspx";
            break;
        case 11:
            url = "AssociatorSet.aspx";
            dWidth = 440;
            dHeight = 300;
            break;
        case 12:
            url = "TopicSet.aspx";
            break;
        case 13:
        case 15:
            url = "currencyset.aspx";
            dWidth = 440;
            dHeight = 300;

            break;
    }
    var scrollPos = new getScrollPos();
    var pageSize = new getPageSize();

    $("Div_window").style.height = (pageSize.height + scrollPos.scrollY) + "px";

    $("Div_window").style.background = "#000";
    $("Div_window").style.filter = "alpha(opacity=30)";
    $("Div_window").style.opacity = 0.9;
    $("Div_window").style.MozOpacity = 1;
    $("Div_window").style.display = "block";

    $("window").src = url;
    $("window").style.width = dWidth + "px";
    $("window").style.display = "block";
    $("window").style.height = dHeight + "px";

    var x = Math.round(pageSize.width / 2) - (dWidth / 2) + scrollPos.scrollX;
    var y = 100;

    $("window").style.display = 'block';
    $("window").style.left = x + 'px';
    $("window").style.top = y + 'px';
}

function iframeload(obj) {
    //    if(!IE()){ 
    //		obj.height=obj.contentDocument.body.scrollHeight;}
    //	else{
    //		obj.style.height=obj.contentWindow.document.body.scrollHeight + 10;}

}

//获取选择的资讯栏目
function lselectnewtype() {
    $("hidPT_ID").value = $("newstitleids").value;
}

//获取选择的企业类别
function lselectusertype() {
    $("hidPT_ID").value = $("companytypeids").value;
}

//关闭标签层
function closewindows() {
    parent.$("Div_window").style.display = "none";
    parent.$("window").style.display = "none";
}

//验证信息标签设置
function isNumer(obj) {
    if (obj.value != "") {
        if (obj.value.search(/^[-\+]?\d+$/) == -1) {
            obj.value = "";
            return alertmsg("输入的不是数字！", '', false);
        }
    }
}

function labelvalidate(num) {
    //    switch(num)
    //    {
    //        case 1:
    //            if($("typeids").value!="")
    //            {
    //                $("hidptid").value = $("typeids").value;
    //            }
    //            break;
    //        case 2:
    //            if($("processtypeids").value!="")
    //            {
    //                $("hidptid").value = $("processtypeids").value;
    //            }
    //            break;
    //        case 3:
    //            if($("suogatetypeids").value!="")
    //            {
    //                $("hidptid").value = $("suogatetypeids").value;
    //            }
    //            break;
    //        case 4:
    //            if($("servertypeids").value!="")
    //            {
    //                $("hidptid").value = $("servertypeids").value;
    //            }
    //            break;
    //        case 5:
    //            if($("cooperatetypeids").value!="")
    //            {
    //                $("hidptid").value = $("cooperatetypeids").value;
    //            }
    //            break;
    //        case 6:
    //            if($("jobtypeids").value!="")
    //            {
    //                $("hidptid").value = $("jobtypeids").value;
    //            }
    //            break;    
    //        case 7:
    //            if($("companytypeids").value!="")
    //            {
    //                $("hidptid").value = $("companytypeids").value;
    //            }
    //            break;
    //        case 8:
    //            if($("newstitleids").value!="")
    //            {
    //                $("hdgetid").value = $("newstitleids").value;
    //            }
    //            break;
    //    }
}
function setlabelTableStyle(obj) {
    var arrli = $("labelTable").getElementsByTagName("li");

    for (i = 0; i < arrli.length; i++) {
        arrli[i].className = "";
    }
    obj.className = "current";
}

//信息标签设置
function infoshow(num, obj) {
    setlabelTableStyle(obj);
    switch (num) {
        case 1:
            $("base").style.display = "block";
            $("page").style.display = "none";
            $("key").style.display = "none";
            break;
        case 2:
            $("base").style.display = "none";
            $("page").style.display = "block";
            $("key").style.display = "none";
            break;
        case 3:
            $("base").style.display = "none";
            $("page").style.display = "none";
            $("key").style.display = "block";
            break;
    }
}



//08-11-17 add tc
function setHeaderLabelStyle(num) {
    var arrli = $("labelTable").getElementsByTagName("td");

    for (i = 0; i < arrli.length; i++) {
        arrli[i].className = "";
    }
    $("tdHeader" + num).className = "contentllabeltd";
}

/**
=====================================标签管理 结束================================================
**/

//管理员权限添加
function AdminAdd() {
    if ($("ddlRose").value == "-1") {
        return alertmsg('请选择要添加的角色!', '', false);
    }
    if ($("txtName").value == "") {
        return alertmsg('登陆用户名必须填写!', '', false);
    }
    if ($("txtName").value.length < 4) {
        return alertmsg('登陆用户名长度不能小于4个字符!', '', false);
    }
    if ($("txtPwd").value == "") {
        return alertmsg('密码不能为空!', '', false);
    }
    if ($("txtPwd").value.length < 6) {
        return alertmsg('为了您的帐户安全,密码长度不能小于6个字符!', '', false);
    }
    if ($("txtPwd2").value == "") {
        return alertmsg('请在次输入密码!', '', false);
    }
    if ($("txtPwd").value != $("txtPwd2").value) {
        return alertmsg('两次输入密码不一样,请从新输入!', '', false);
    }
}
//管理员权限修改
function AdminEdit() {
    if ($("ddlUpdate").value == "-1") {
        return alertmsg('请选择角色!', '', false);
    }

    if ($F("txtYuanPwd").trim() != "") {
        if ($F("txtNewPwd").trim() == "") {
            return alertmsg('请输入新密码!', '', false);
        }
        if ($F("txtOKpwd").trim() == "") {
            return alertmsg('请在次输入密码!', '', false);
        }
        if ($F("txtNewPwd").trim() != $F("txtOKpwd").trim()) {
            return alertmsg('两次输入密码不一样,请从新输入!', '', false);
        }
    }
}



//添加角色
function roleadd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入角色名称！', '', false);
    }
}
//修改角色
function roleedit() {
    if ($("TextBox1").value == "") {
        return alertmsg('请输入角色名称！', '', false);
    }
}



//用户等级管理开始
function usergradeadd() {
    if ($("txtName").value == "") {
        return alertmsg('用户等级名必须填写！', '', false);
    }
    if ($("ymoney").value == "") {
        return alertmsg('请输入年租金！', '', false);
    }

    if ($("mmoney").value == "") {
        return alertmsg('请输入月租金！', '', false);
    }

    if ($("ymoney").value.search(/^[0-9]+$/) != -1 || $("ymoney").value.search(/^([0-9]+)|([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)$/) != -1) {
        $("ymoney").value = Math.round(parseFloat($("ymoney").value) * 100) / 100
    }
    else {
        return alertmsg('年租金输入格式错误！\n 例：88.88', '', false)
    }

    if ($("mmoney").value.search(/^[0-9]+$/) != -1 || $("mmoney").value.search(/^([0-9]+)|([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)$/) != -1) {
        $("mmoney").value = Math.round(parseFloat($("mmoney").value) * 100) / 100
    }
    else {
        return alertmsg('月租金格输入格式错误！\n 例：88.88', '', false)
    }
}
function usergradeedit() {

    if ($("txtname1").value == "") {
        return alertmsg('用户等级名必须填写!', '', false);
    }

    if ($("mmoney1").value == "") {
        return alertmsg('请输入月租金！', '', false);
    }
    if ($("ymoney1").value == "") {
        return alertmsg('请输入年租金！', '', false);
    }

    if ($("ymoney1").value.search(/^[0-9]+$/) != -1 || $("ymoney1").value.search(/^([0-9]+)|([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)$/) != -1) {
        $("ymoney1").value = Math.round(parseFloat($("ymoney1").value) * 100) / 100
    }
    else {
        return alertmsg('年租金输入格式错误！\n 例：88.88', '', false)
    }
    if ($("mmoney1").value.search(/^[0-9]+$/) != -1 || $("mmoney1").value.search(/^([0-9]+)|([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)$/) != -1) {
        $("mmoney1").value = Math.round(parseFloat($("mmoney1").value) * 100) / 100
    }
    else {
        return alertmsg('月租金格输入格式错误！\n 例：88.88', '', false)
    }
}
//用户等级管理开始结束


//财务信息开始
function FinanceInfo() {

    if ($("ddlRose").value == "-1") {
        return alertmsg('选择财务类型！', '', false);
    }
    if ($("txtName").value == "") {
        return alertmsg('请输入财务费用！', '', false);
    }
    //    else 
    //	{   
    //        IsFloat($("txtName"))
    //     }    
    if ($("txtuser").value == "") {
        return alertmsg('请输入用户姓名！', '', false);
    }
}
function FinanceInfoEdit() {
    if ($("ddlUpdate").value == "-1") {
        return alertmsg('选择财务类型！', '', false);
    }

    if ($("txtuser1").value == "") {
        return alertmsg('请输入用户姓名！', '', false);
    }
    if ($("txtNewPwd").value == "") {
        return alertmsg('请输入财务费用！', '', false);
    }
    else {
        IsFloat($("txtName"))
    }
}
//财务信息结束


function block() {
    $("add").style.display = 'block';
    window.location.href = "#addtable"
}
function quit() {
    $("add").style.display = 'none'
}
function Exit() {
    $("update").style.display = 'none';
}
function load() {
    switch ($("key").value) {
        case "1":
            $("add").style.display = "none";
            break;
        case "2":
            $("update").style.display = "block";
            break;
        default:
            break;
    }
}



//财务类别开始
function FinanceTypeAdd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入财务类别！', '', false);
    }
}
function FinanceTypeedit() {
    if ($("name").value == "") {
        return alertmsg('请输入财务类别！', '', false);
    }
}
//财务类型结束




//支付方式开始
function paymathodadd() {
    if ($("txtName").value == "") {
        return alertmsg('支付方式必须填写！', '', false);
    }
    if ($("remark").value == "") {
        return alertmsg('备注必须填写！', '', false);
    }
}
function paymathodedit() {
    if ($("txtUpdate").value == "") {
        return alertmsg('支付方式必须填写！', '', false);
    }
    if ($("remark1").value == "") {
        return alertmsg('备注必须填写！', '', false);
    }
}


//支付方式结束
//产品类别添加
function ProductAdd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入产品类别！', '', false);
    }
}
//产品类别修改
function Productedit() {
    if ($("txtName").value == "") {
        return alertmsg('请输入产品类别！', '', false);
    }
}

//服务器添加
function ServerAdd() {
    if ($("txtName").value == "") {
        return alertmsg('服务器名称必须填写！', '', false);
    }
    if ($("serverpath").value == "") {
        return alertmsg('服务器物理路径必须填写！', '', false);
    }
    if ($("serversul").value == "") {
        return alertmsg('服务器虚拟路径必须填写！', '', false);
    }
}
//服务器修改
function ServerEdit() {
    if ($("txtname1").value == "") {
        return alertmsg('服务器名称必须填写！', '', false);
    }
    if ($("serverpath1").value == "") {
        return alertmsg('服务器物理路径必须填写！', '', false);
    }
    if ($("serversul1").value == "") {
        return alertmsg('服务器虚拟路径必须填写！', '', false);
    }
}
//添加角色
function addrole() {
    if ($("txtName").value == "") {
        return alertmsg('请输入角色名称！', '', false);
    }
}
//修改角色 
function editrole() {
    if ($("TextBox1").value == "") {
        return alertmsg('请输入角色名称！', '', false);
    }
}


//用户类型修改

function usertypeedit() {
    if (document.getElementById("txtName").value == "") {
        return alertmsg('请输入用户类别名称！', '', false);
    }
}


//岗位添加信息和修改
function postadd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入岗位名称！', '', false);
    }
}
function postedit() {
    if ($("Textbox1").value == "") {
        return alertmsg('请输入岗位名称！', '', false);
    }
}
//岗位添加信息和修改


//过滤字管理
function keywordadd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入过滤字名称！', '', false);
    }
}
function keywordedit() {
    if ($("TextBox1").value == "") {
        return alertmsg('请输入过滤字名称', '', false);
    }
}
//过滤字管理


//用户等级设置
function CheckUserGradePopedmoSetting() {
    if ($("refashtime").value == "") {
        return alertmsg('刷新时间不能为空！', '', false)
    }
    if ($("refashnum").value == "") {
        return alertmsg('请填写一天内最多刷新的次数！', '', false)
    }
    if ($("refashnum").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('刷新次数只能为整数！', '', false);
    }
    if ($("seecontactsnum").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('查看联系方式条数只能是整数！', '', false);
    }
    if ($("uploadpicnum").value == "") {
        return alertmsg('上传图片的张数不能为空！', '', false);
    }
    if ($("uploadpicnum").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('上传图片的张数只能是整数！', '', false);
    }
    if ($("LimitDate").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('限定的天数只能是整数！', '', false);
    }
    if ($("refashtimes").value == "") {
        return alertmsg('刷新记录条数不能为空！', '', false);
    }
    if ($("refashtimes").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('刷新记录条数只能为整数!', '', false);
    }

    if ($("ddldebaseusergrade").value == "-1") {
        return alertmsg('请选择该等级用户到期后所降低至的级别', '', false);
    }
    var writenum = document.getElementsByTagName("input");
    var str = "";
    for (var i = 0; i < writenum.length; i++) {
        if (writenum[i].type == 'text') {
            if (writenum[i].id.indexOf('messagevalue') > -1) {
                if (writenum[i].value == "" || writenum[i].value.search(/^[-\+]?\d+$/) == -1) {
                    return alertmsg('请填写完整的条目数，且必须为整数!', '', false);
                } else {
                    if ($("HidLimitDate").value == 1) {
                        str += writenum[i].name + "-0,"
                    } else {
                        str += writenum[i].name + "-" + writenum[i].value + ","
                    }
                }
            }
        }
    }
    $("HidMessagenum").value = str;
}


//登陆
function load1() {
    $("txtName").focus();
}
function scrCode() {
    $("imgCode").src = 'PassCode.aspx';
}
function rest() {
    $("txtName").value = "";
    $("txtPwd").value = "";
    $("txtCode").value = "";
    $("imgCode").src = 'PassCode.aspx';

}
function KeyDown() {
    var gk = event.keyCode;
    if (gk == 13) {
        event.keyCode = 9;
        return;
    }
}
function KeyDown1() {
    var gk = event.keyCode;
    if (gk == 13) {
        getlogin();
    }
}


function getlogin() {

    if ($F("txtUserName") == "") {
        return alertmsg('用户名不能为空,请输入用户名！', '', false);
    }
    else if ($("txtPassWord").value == "") {
        return alertmsg('密码不能为空,请输入密码！', '', false);
    }
    else if ($("txtCode").value == "") {
        return alertmsg('请输入验证玛！', '', false);
    }
    else {
        return true;
    }
}


//发送电子邮件
function EmailAdd() {
    if ($("lbtitle").value == "") {
        return alertmsg('标题必须填写！', '', false);
    }
    if ($("lbcontent").value.length < 0) {
        return alertmsg('内容必须填写！', '', false);
    }
}
function search() {
    if ($("TextBox1").value == "") {
        return alertmsg('请输入公司名称！', '', false);
    }
}


//充值信息
function getinputmoney() {
    if ($("ddlRose").value == "-1") {
        return alertmsg('请选择充值方式', '', false)
    }
    if ($("dfinance").value == "-1") {
        return alertmsg('请选择财务类别', '', false)
    }
    if ($("txtmoney").value == "") {
        return alertmsg('请输入充值金额', '', false)
    }
    else {
        IsFloat($("txtmoney"))
    }

}

function IsFloat(obj) {
    var tempValue = obj.value.replace(/(^\s+)|(\s+$)/g, '');
    if (!tempValue)
    { return false }
    if (/^-?\d+(\.\d+)?$/.test(tempValue)) {
        obj.value = parseFloat(tempValue).toFixed(2);
    }
    else {
        $("tbbaseprice").value = "";
        $("tbbaseprice").focus();
        return alertmsg('请输入合法的浮点数.', '', false);
    }
}

//修改邮件信息
function EditEmail() {
    if ($("lbtitle").value == "") {
        return alertmsg('请输入标题信息！', '', false);
    }
    if ($("lbcontent").value.length < 0) {
        return alertmsg('请输入内容信息！', '', false);
    }
}
/**
 
=====================================功能菜单 开始================================================
**/
function menuchange(num) {
    var left = window.parent.left.document;

    $("basic1").className = "";
    $("basic2").className = "";
    $("basic3").className = "";
    $("basic4").className = "";
    $("basic5").className = "";


    left.getElementById("basic1").style.display = "none";
    left.getElementById("basic2").style.display = "none";
    left.getElementById("basic3").style.display = "none";
    left.getElementById("basic4").style.display = "none";
    left.getElementById("basic5").style.display = "none";

    switch (num) {
        case 1:
            $("basic1").className = "on";
            left.getElementById("basic1").style.display = "block";
            break;
        case 2:
            $("basic2").className = "on";
            left.getElementById("basic2").style.display = "block";
            break;
        case 3:
            $("basic3").className = "on";
            left.getElementById("basic3").style.display = "block";
            break;
        case 4:
            $("basic4").className = "on";
            left.getElementById("basic4").style.display = "block";
            break;
        case 5:
            $("basic5").className = "on";
            left.getElementById("basic5").style.display = "block";
            break;
       
    }
}

/**
=====================================功能菜单 结束================================================
**/



/**
=====================================自定义字段 开始================================================
**/

function FiledSelTop(ckobj, typeID, showDiv) {
    if (ckobj.checked) {
        $(showDiv).innerHTML = "";
        $(showDiv).style.display = "none";
        return true;
    }
    if ("" == $F(typeID) || "0" == $F(typeID)) {
        sAlert("请先选择类别");
        return false;
    }
    var ajax = new Ajax("xy037", "&typeid=" + $F(typeID) + "&modulename=" + $F("ddlmodule"))
    ajax.onSuccess = function() {
        if (ajax.state.result == 1) {
            $(showDiv).style.display = "block";
            if (ajax.data) {
                var html = "请选择要继承的字段：<br /><ul>";
                for (var i = 0; i < ajax.data.filedItem.length; i++) {
                    html += "<li><input type=\"checkbox\" id=\"topfiled" + i + "\" name=\"topfiled\" value=\"" + ajax.data.filedItem[i].id + "\" /><label for=\"topfiled" + i + "\">" + ajax.data.filedItem[i].name + "</label></li>";
                }
                html += "</ul>";
                $(showDiv).innerHTML = html;
            }
            else {
                $(showDiv).innerHTML = "没有需要继承的字段";
            }
        }
        else {
            sAlert("顶级类不需要继承！");
            ckobj.checked = true;
            $(showDiv).innerHTML = "";
            $(showDiv).style.display = "none";
        }
    }
}

var addnum = 1;
var htmlArray = new Array();

function addline() {
    htmlArray[0] = '<td><input  size="16"  id="txtEName' + addnum + '" title="" name="txtEName" type="text" value="" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emEName' + addnum + '" style="display:none;">↑字段英文名</em></td> ';
    htmlArray[1] = '<td><input  size="16"  id="txtCName' + addnum + '" title="" name="txtCName" type="text" value="" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emCName' + addnum + '" style="display:none;">↑字段中文名</em></td>';
    htmlArray[2] = '<td><textarea name="txtdesp" cols="19" rows="1" id="txtdesp' + addnum + '" title="" onblur="taunfocusalt(this);" onfocus="tafocusalt(this);" ></textarea><em id="emdesp' + addnum + '" style="display:none;">↑字段说明文字</em></td>';
    htmlArray[3] = '<td><select name="seltype"><option selected="selected" value="Text" >文本框</option><option value="Textarea"> 多行文本区 </option><option value="Select">下拉框</option><option value="Radio">单选框</option><option value="Checkbox">复选框</option></select></td>';
    htmlArray[4] = '<td><textarea name="txtselect" cols="19" rows="1" id="txtselect' + addnum + '" title="" onblur="taunfocusalt(this);" onfocus="tafocusalt(this);" ></textarea><em id="emselect' + addnum + '" style="display:none;">↑字段预留值</em></td>';
    htmlArray[5] = '<td><input  size="16"  id="txtFieldSize' + addnum + '" title="" name="txtFieldSize" type="text" value="50" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emFieldSize' + addnum + '" style="display:none;">↑字段大小，小于8000</em></td> ';
    //htmlArray[5]='<td><input id="chkbunique'+addnum+'" type="checkbox"  onclick="checkonclick(\'unique\',\'unique'+addnum+'\',this);" /><span id="unique'+addnum+'">重复</span> <input id="chkbnull'+addnum+'" type="checkbox"  onclick="checkonclick(\'null\',\'null'+addnum+'\',this);"  /><span id="null'+addnum+'">选填</span> <input id="chkbtag'+addnum+'" type="checkbox"  onclick="checkonclick(\'tag\',\'tag'+addnum+'\',this);"  /><span id="tag'+addnum+'">一般</span></td>';
    htmlArray[6] = '<td><img src="../images/fielddel.gif" alt="删除" name="imgdelete" onclick="deleteline(this);" /></td>';

    var table = document.all.productfield;
    var newRow = table.insertRow();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    for (var i = 0; i < newRow.cells.length; i++) {
        newRow.cells[i].innerHTML = htmlArray[i];
    }
    addnum++;
}


function deleteline(obj) {
    var chks = document.getElementsByName("imgdelete");
    if (chks.length == 0) { alert("没有可以删除的行!"); return; }
    var rowindex = -1;
    for (var i = 0; i < chks.length; i++) {
        if (chks[i] == obj) { rowindex = i; }
    }
    if (rowindex < 0) { alert("没有选择要删除的行!"); return; }
    if (rowindex == 0) { alertmsg("该行不能删除！", '', false); }
    else if (confirm("真的要删除第" + eval(rowindex + 1) + "行吗?"))
    { document.all.productfield.deleteRow(rowindex); }
}

function addFiled() {
    if ($("typeids").value != undefined && $("typeids").value != "") {
        $("hidPT_ID").value = $("typeids").value;
    }
    if ($("hidPT_ID").value == "") {
        return alertmsg("请选择产品分类！", "", false);
    }
    else {


        var input = document.getElementsByTagName("input");
        for (var i = 0; i < input.length; i++) {
            if (input[i].type == 'checkbox') {
                if (input[i].id.indexOf('chkbunique') > -1) {
                    if (input[i].checked == true) {
                        $("hidUnique").value += ",1";
                    }
                    else {
                        $("hidUnique").value += ",0";
                    }
                }
                else if (input[i].id.indexOf('chkbnull') > -1) {
                    if (input[i].checked == true) {
                        $("hidNull").value += ",1";
                    }
                    else {
                        $("hidNull").value += ",0";
                    }
                }
                else if (input[i].id.indexOf('chkbtag') > -1) {
                    if (input[i].checked == true) {
                        $("hidTag").value += ",1";
                    }
                    else {
                        $("hidTag").value += ",0";
                    }
                }
            }
        }
        return true;
    }
}

function checkonclick(type, name, obj) {

    switch (type) {
        case "unique":
            if (obj.checked == true)
                $(name).innerHTML = "唯一";
            else
                $(name).innerHTML = "重复";
            break;
        case "null":
            if (obj.checked == true)
                $(name).innerHTML = "必填";
            else
                $(name).innerHTML = "选填";
            break;
        case "tag":
            if (obj.checked == true)
                $(name).innerHTML = "标签";
            else
                $(name).innerHTML = "一般";
            break;
    }
}

//获取焦点提示信息
function focusalt(obj) {
    var txtname = obj.id;
    var enname = "em" + txtname.substring(3);
    $(enname).style.display = "block";
}
//失去焦点提示信息
function unfocusalt(obj) {
    var txtname = obj.id;
    var enname = "em" + txtname.substring(3);

    $(enname).style.display = "none";
}

//获取焦点提示信息
function tafocusalt(obj) {
    var txtname = obj.id;
    var enname = "em" + txtname.substring(3);
    obj.rows = 5;
    $(enname).style.display = "block";
}
//失去焦点提示信息
function taunfocusalt(obj) {
    var txtname = obj.id;
    var enname = "em" + txtname.substring(3);
    obj.rows = 1;
    $(enname).style.display = "none";
}

//修改初始化
function pinitline() {

    var lines = $("hidline").value.split('|');


    addnum = 1;
    var table = document.all.productfield;
    for (var j = 0; j < lines.length; j++) {
        var newRow = table.insertRow();
        newRow.insertCell();
        newRow.insertCell();
        newRow.insertCell();
        newRow.insertCell();
        newRow.insertCell();
        newRow.insertCell();
        newRow.insertCell();
        var cells = lines[j].split('$');
        for (var i = 0; i < newRow.cells.length; i++) {

            newRow.cells[i].innerHTML = cells[i];
        }
        addnum++;
    }

}
function pinitaddline() {
    var url = "GetLine.aspx?PT_ID=" + $("hidPT_ID").value;

    var XMLDoc1 = new XMLHttpObject(url, false);
    XMLDoc1.sendData();
    var msg = XMLDoc1.getText();
    var lines = msg.split('|');

    var table = document.all.productfield;
    for (var j = 0; j < lines.length; j++) {
        if (j == 0) {
            var values = lines[j].split('$');
            $("txtEName0").value = values[0];
            $("txtCName0").value = values[1];
            $("txtdesp0").value = values[2];
            $("seltype0").value = values[3];
            $("txtselect0").value = values[4];
            if (values[5] == "true") {
                $("chkbunique0").checked = true;
            }
            if (values[6] == "true") {
                $("chkbnull0").checked = true;
            }
            if (values[7] == "true") {
                $("chkbtag0").checked = true;
            }

        }
        else {
            var newRow = table.insertRow();
            newRow.insertCell();
            newRow.insertCell();
            newRow.insertCell();
            newRow.insertCell();
            newRow.insertCell();
            newRow.insertCell();
            newRow.insertCell();

            var cells = lines[j].split('$');
            for (var i = 0; i < newRow.cells.length; i++) {
                newRow.cells[i].innerHTML = cells[i];
            }
            addnum++;
        }
    }
}


//选择模块获取该模块信息类别
function selectmodule() {
    var moduleName = $F("ddlmodule");
    var arr = new Array("supply", "machining", "business", "service", "show");
    for (var i = 0; i < arr.length; i++) {
        $(arr[i]).style.display = "none";
    }
    switch (moduleName) {
        case "offer":
            $("supply").style.display = "block";
            break;
        case "machining":
            $("machining").style.display = "block";
            break;
        case "investment":
            $("business").style.display = "block";
            break;
        case "service":
            $("service").style.display = "block";
            break;
        case "exhibition":
            $("show").style.display = "block";
            break;
    }
}
/**
=====================================自定义字段 结束================================================
**/

/**
=====================================关键字信息添加与修改 开始================================================
**/
function GetKeyword() {
    var temp = $("KIID").value;
    if (temp == 0) {
        $("add").style.display = "none";
        $("update").style.display = "none";
    }
    else if (temp < 0) {
        $("update").style.display = "none";
        $("add").style.display = "block";
    }
    else {
        $("update").style.display = "block";
        $("add").style.display = "none";
    }
}

function quitadd() {
    $("KIID").value = 0;
    $("add").style.display = "none";
}

function quitupdate() {
    $("KIID").value = 0;
    $("update").style.display = "none";
}

function AddKeyword() {
    $("add").style.display = "block";
    window.location.href = "#addtable";
}

/**
=====================================关键字信息添加与修改 结束================================================
**/

/**
===================================== 诚信指数设置 开始================================================
**/
function faithset() {
    if ($("tbbase").value == "") {
        return alertmsg('请初始完成基本资料后诚信指数增加值！', '', false);
    }
    else if ($("tbbase").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('初始的诚信指数应为整数！', '', false);
    }

    if ($("gfaith").value == "") {
        return alertmsg('请填写个人资料恶意填写处罚扣的诚信指数！', '', false);
    }
    else if ($("gfaith").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("gfaithuu").value == "") {
        return alertmsg('请填写个人资料恶意填写处罚扣的UU币！', '', false);
    }
    else if ($("gfaithuu").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('UU币为整数！', '', false);
    }

    if ($("gerrfaith").value == "") {
        return alertmsg('请填写个人资料普通错误处罚扣的诚信指数！', '', false);
    }
    else if ($("gerrfaith").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("gerrfaithuu").value == "") {
        return alertmsg('请填写个人资料普通错误处罚扣的UU币！', '', false);
    }
    else if ($("gerrfaithuu").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('UU币为整数！', '', false);
    }

    if ($("tbhot").value == "") {
        return alertmsg('请初始完成高级资料后诚信指数增加值！', '', false);
    }
    else if ($("tbhot").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('初始的诚信指数应为整数！', '', false);
    }

    if ($("hfaith").value != "") {
        if ($("hfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("hfaithuu").value != "") {
        if ($("hfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }

    if ($("herrfaith").value != "") {
        if ($("herrfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("herrfaithuu").value != "") {
        if ($("herrfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }

    if ($("tblicence").value == "") {
        return alertmsg('请初始上传营业执照后诚信指数增加值！', '', false);
    }
    else if ($("tblicence").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('初始的诚信指数应为整数！', '', false);
    }

    if ($("tbcertificate").value == "") {
        return alertmsg('请初始上传其他资质证书后诚信指数增加值！', '', false);
    }
    else if ($("tbcertificate").value.search(/^[-\+]?\d+$/) == -1) {
        return alertmsg('初始的诚信指数应为整数！', '', false);
    }

    if ($("userfaith").value != "") {
        if ($("userfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("userfaithuu").value != "") {
        if ($("userfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }

    if ($("usererrfaith").value != "") {
        if ($("usererrfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("usererrfaithuu").value != "") {
        if ($("usererrfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }

    if ($("businessfaith").value != "") {
        if ($("businessfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("businessfaithuu").value != "") {
        if ($("businessfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }

    if ($("businesserrfaith").value != "") {
        if ($("businesserrfaith").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('诚信指数为整数！', '', false);
    }

    if ($("businesserrfaithuu").value != "") {
        if ($("businesserrfaithuu").value.search(/^[-\+]?\d+$/) == -1)
            return alertmsg('UU币为整数！', '', false);
    }
}
/**
===================================== 诚信指数设置 结束================================================
**/
//管理员给用户回复留言
function adminmessage() {
    if ($("title").value == "") {
        return alertmsg('请输入回复标题！', '', false);
    }
    if ($("content").value == "") {
        return alertmsg('请输入回复内容！', '', false);
    }
}



//管理员给用户留言
function messageadd() {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances.lbcontent;
    // Get the editor contents
    var content = oEditor.getData()
    // var lbcontent = FCKeditorAPI.GetInstance('lbcontent').GetXHTML(true);
    if (content == "") {
        return alertmsg('请输入内容', '', false);
    }
    if ($("lbtitle").value == "") {
        return alertmsg('请输入标题！', '', false);
    }
    if ($("lbcontent").value.length > 4000) {
        return alertmsg('内容长度超出范围！', '', false);
    }

}
//添加省份
function ProvinceAdd() {
    if ($("txtName").value == "") {
        return alertmsg('请输入省份名称！', '', false);
    }
}



//修改省份
function ProvinceUpdate() {
    if ($("txtYuanPwd").value == "") {
        return alertmsg('请输入省份名称！', '', false);
    }
}

/*******************  自定义模块相关 **************************/


function AddInfoType() {
    var i = Number(document.getElementById("InfoTypeTotal").value);
    i++;
    var string = new Array();

    string[0] = '<input type="text" id="tbid' + i + '" class="m_i" runat="server" readonly="readonly" value="' + i + '"/>';
    string[1] = '<input type="text" id="tbprefix' + i + '" name="tbprefix' + i + '"/>';
    string[2] = '<input type="text" id="tbpostfix' + i + '" name="tbpostfix' + i + '"/>';
    string[3] = '<input type="radio" name="rb' + i + '" value="sell" checked="checked" onclick="SetInfoTypeValue(' + i + ');"/>供&nbsp;<input type="radio" name="rb' + i + '" value="buy" onclick="SetInfoTypeValue(' + i + ');"/>求<input type="hidden" id="hidInfoType_' + i + '" name="hidInfoType_' + i + '" value="sell" />';
    string[4] = '<a href="javascript:void(null);" onclick="DeleteInfoType(' + i + ');"><img src="../images/delete1.gif" alt="删除"/></a>';

    var table = document.getElementById("TableInfoType");
    var newRow = table.insertRow();
    newRow.id = "tr" + i;
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();
    newRow.insertCell();

    for (var m = 0; m < 5; m++) {
        if (m == 4) {
            newRow.cells[m].id = "tdDel_" + i;
            newRow.cells[m].style.display = "";
        }

        newRow.cells[m].innerHTML = string[m];
    }

    if ((i - 1) > 1)
        document.getElementById("tdDel_" + (i - 1)).style.display = "none";


    document.getElementById("InfoTypeTotal").value = i;
    //GetNewsClass(document.getElementById("addtable").value);
}


function DeleteInfoType(num) {
    var i = Number(document.getElementById("InfoTypeTotal").value);
    i--;

    var table = document.getElementById("TableInfoType");

    table.deleteRow(num);

    if (i != 1) document.getElementById("tdDel_" + i).style.display = "";

    document.getElementById("InfoTypeTotal").value = i;
}

function SetInfoTypeValue(index) {
    var eles = document.getElementsByName("rb" + index);

    for (i = 0; i < eles.length; i++) {
        if (eles[i].checked) {
            document.getElementById("hidInfoType_" + index).value = eles[i].value;
        }
    }
}

//2008-11-5 后台编辑信息选择类别更新自定义方法
var isconfirm = false;
function SelectTypeIDOnClick() {
    if (!isconfirm) {
        if (window.confirm("如果修改信息类别，原有的自定义字段数据将丢失！\n是否继续修改？")) {
            isconfirm = true;
        }
        else {
            return false;
        }
    }
    return true;
}
function SelectTypeIDOnChange() {
    var ajax = new Ajax("XY032", "&module=" + this.ModuleName + "&typeid=" + $F(this.InputTxtID));
    ajax.onSuccess = function() {
        if (ajax.state.result == 1) {
            document.getElementById("tabFieldBody").innerHTML = "<table>" + unescape(ajax.data.html) + "</table>";
        }
    }
}



/*********************后台发送短信*****************************/

function person() {
    document.getElementById("b23").style.display = "";
    document.getElementById("b22").style.display = "none";
    document.getElementById("lic").className = "usertype";
    document.getElementById("lip").className = "cur_usertype";
    //rbt1();
    // rbt2();
}

function company() {
    document.getElementById("b22").style.display = "";
    document.getElementById("b23").style.display = "none";
    document.getElementById("lic").className = "cur_usertype";
    document.getElementById("lip").className = "usertype";
    //per();
}

function rbtchanage(str) {
    if (str == 0) {
        document.getElementById("panugp").style.display = "";
        document.getElementById("pansearch").style.display = "none";
        document.getElementById("rbtugp").checked = true;
        rbt1();
    }
    if (str == 1) {
        document.getElementById("panugp").style.display = "none";
        document.getElementById("pansearch").style.display = "";
        document.getElementById("rbtsearch").checked = true;
        rbt2();
    }

}

function rbtchanageperson(str) {
    if (str == 0) {
        document.getElementById("personserach").style.display = "none";
        document.getElementById("Radio1").checked = true;
        document.getElementById("personall").value = 1;
    }
    if (str == 1) {
        document.getElementById("personserach").style.display = "";
        document.getElementById("Radio2").checked = true;
        document.getElementById("personall").value = 0;
    }

}

function rbt1() {
    var chkother = document.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport1') > -1) {
                chkother[i].checked = false;
            }
        }
    }
}

function rbt2() {
    var chkother = document.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('CBL') > -1) {
                chkother[i].checked = false;
            }
        }
    }
}

function per() {
    var chkother = document.getElementsByTagName("input");
    for (var i = 0; i < chkother.length; i++) {
        if (chkother[i].type == 'checkbox') {
            if (chkother[i].id.indexOf('chkExport') > -1) {
                chkother[i].checked = false;
            }
        }
    }
}

function CheckSelectClassNumber() {
    var arr = $("pnlSuperClass").getElementsByTagName("input");
    var strid = "";
    var nonum = 0;
    for (var i = 0; i < arr.length; i++) {
        //if(arr[i].type=='checkbox') 
        if (arr[i].type == 'radio') {
            if (arr[i].checked) {
                nonum = 1;
                strid = arr[i].value;
            }
        }
    }
    if (nonum == 0) {
        sAlert("请选择一个要转移到的分类！");
        return false;
    }
    $("hidptid").value = strid;
}

//改变多行文本框的行数
//eleId:元素Id
//action:操作标识 add:加,sub：减
//maxRow:最大行数
//minRow:最小行数
//step:步长，即每次改变的行数
function ChangeTextRow(eleId, action, maxRow, minRow, step) {
    var ele = document.getElementById(eleId);

    if (!ele) return;

    if (action == "add") {
        if (ele.rows < maxRow) {
            ele.rows = ele.rows + step;
        }
    }

    if (action == "sub") {
        if (ele.rows > minRow) {
            ele.rows = ele.rows - step;
        }
    }
}

//GridView行目标移上事件
//tc 09-02-18
function __XY_GV_Row_MouseOver(obj) {
    var tds = obj.childNodes;
    for (i = 0; i < tds.length; i++) {
        tds[i].style.backgroundColor = "#F2F9FD";
    }
}
//GridView行目标移走事件
//tc 09-02-18
function __XY_GV_Row_MouseOut(obj) {
    var tds = obj.childNodes;
    for (i = 0; i < tds.length; i++) {
        tds[i].style.backgroundColor = "#ffffff";
    }
}
