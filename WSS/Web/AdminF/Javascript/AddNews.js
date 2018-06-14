function ShowTopicType() {
    var dWidth = 400;
    var dHeight = 300;
    var scrollPos = new getScrollPos();
    var pageSize = new getPageSize();

    $("SelTopicType_bg").style.display = "block";
    $("SelTopicType_bg").style.height = (pageSize.height + scrollPos.scrollY) + "px";
    $("SelTopicType").style.display = "block";

    var x = Math.round(pageSize.width / 2) - (dWidth / 2) + scrollPos.scrollX;
    var y = Math.round(pageSize.height / 2) - (dHeight / 2) + scrollPos.scrollY;
    $("SelTopicType").style.width = dWidth + "px";
    $("SelTopicType").style.height = dHeight + "px";
    $("SelTopicType").style.left = x + 'px';
    $("SelTopicType").style.top = y + 'px';
    if (!IE()) {
        $("SelTopicType").TopicTypeInit();
    }
    else {
        $("SelTopicType").contentWindow.TopicTypeInit();
    }
}

function CloseTopicType() {
    $("SelTopicType_bg").style.display = "none";
    $("SelTopicType").style.display = "none";
}

function TopicTypeInit() {
    var objid = $("seltopictypelist").getElementsByTagName("input");
    var objname = $("seltopictypelist").getElementsByTagName("span");
    var objparentid = parent.$("hidTopicID");
    var objparentname = parent.$("topicnames");

    var arrid = objparentid.value.split(",");
    var tmpname = "";
    for (var i = 0; i < objid.length; i++) {
        for (var j = 0; j < arrid.length; j++) {
            if (objid[i].value == arrid[j]) {
                objid[i].checked = true;
                tmpname += " &nbsp; " + objname[i].innerHTML;
            }
        }
    }
    objparentname.innerHTML = tmpname;
}
function TopicTypeInsert() {
    var objid = $("seltopictypelist").getElementsByTagName("input");
    var objname = $("seltopictypelist").getElementsByTagName("span");
    var objparentid = parent.$("hidTopicID");
    var objparentname = parent.$("topicnames");

    var ids = "";
    var names = "";
    for (var i = 0; i < objid.length; i++) {
        if (objid[i].checked) {
            ids += "" == ids ? "" : ",";
            ids += objid[i].value;
            names += " &nbsp; " + objname[i].innerHTML;
        }
    }
    objparentid.value = ids;
    objparentname.innerHTML = names;
    parent.CloseTopicType();
}

//绑定不同类型
function TypeChange() {
    if (document.getElementById("rbcommonnews").checked == true) {
        document.getElementById("TR1").style.display = "none";
        document.getElementById("TR9").style.display = "none";
        document.getElementById("TR3").style.display = "block";
        document.getElementById("TR4").style.display = "block";
        document.getElementById("TR5").style.display = "block";
        document.getElementById("TR7").style.display = "none";
        document.getElementById("TD1").style.display = "none";
        $("trUploadFile").style.display = "block";
        document.getElementById("rbpicurl").checked = false;
        document.getElementById("rbpicupload").checked = false;
    }

    if (document.getElementById("rbpicnews").checked == true) {
        document.getElementById("TR1").style.display = "none";
        document.getElementById("TR3").style.display = "block";
        document.getElementById("TR4").style.display = "block";
        document.getElementById("TR5").style.display = "block";
        document.getElementById("TD1").style.display = "block";
        document.getElementById("TR7").style.display = "block";
        document.getElementById("TR9").style.display = "block";
        $("trUploadFile").style.display = "block";
        if (document.getElementById("hdpicurl").value != "")
            document.getElementById("imgsrc").src = document.getElementById("hdpicurl").value;
    }

    if (document.getElementById("rbcaptionnews").checked == true) {
        document.getElementById("TR1").style.display = "block";
        document.getElementById("TR3").style.display = "none";
        document.getElementById("TR4").style.display = "none";
        document.getElementById("TR5").style.display = "none";
        document.getElementById("TD1").style.display = "block";
        document.getElementById("TR7").style.display = "block";

        $("trUploadFile").style.display = "none";

        if (document.getElementById("hdpicurl").value != "")
            document.getElementById("imgsrc").src = document.getElementById("hdpicurl").value;
    }

}

// 页面加载JS
function ChangePicType() {
    if (document.getElementById("rbpicurl").checked == true) {
        document.getElementById("TR8").style.display = "block";
        document.getElementById("TR9").style.display = "none";
    }
    else if (document.getElementById("rbpicupload").checked == true) {
        document.getElementById("TR8").style.display = "none";
        document.getElementById("TR9").style.display = "block";
    }

    if (document.getElementById("rbpicurl").checked == false && document.getElementById("rbpicupload").checked == false) {
        document.getElementById("rbpicupload").checked = true;
    }
}

// 图片预览JS
function PreviewImage(num) {
    if (num == "1") {
        if (document.getElementById("tbpinurl").value != "") {
            if (document.getElementById("tbpinurl").value.search(/^http:\/\//) == -1) {
                return alertmsg('图片链接地址格式错误，请重新输入.', '', false);
            }

            var value = GetPicType(document.getElementById("tbpinurl").value);
            if (value == true) {
                if (document.getElementById("rbpicurl").checked == true && document.getElementById("tbpinurl").value != null)
                    ;
                else
                    return alertmsg('图片输入方式或链接地址有误', '', false);
            }
            else {
                document.getElementById("tbpinurl").focus();
                return alertmsg('图片链接格式错误,正确的格式为:' + document.getElementById("imagetype").value + ',请重新输入地址.', '', false);
            }
        }
    }
    if (num == '2') {
        if (document.getElementById("FileUpload").value != "") {
            var value = GetPicType(document.getElementById("FileUpload").value);
            if (value == true) {
                if (document.getElementById("rbpicupload").checked == true && document.getElementById("FileUpload").value != null)
                    document.getElementById("imgPreview").filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = document.getElementById("FileUpload").value;
                else
                    return alertmsg('图片输入方式或链接地址有误', '', false);
            }
            else {
                return alertmsg('图片格式错误,正确的格式为:' + document.getElementById("imagetype").value + ',请重新输入地址.', '', false);
            }
        }
    }
}

// JS验证图片后缀名 
function GetPicType(varpic) {
    if (varpic != "" && varpic != null) {
        var temp = varpic.split('.');
        var len = temp.length;
        var fileExt = temp[len - 1].toLowerCase();
        var type = new Array();
        if (document.getElementById("imagetype").value != "" || document.getElementById("imagetype").value != null) {
            type = document.getElementById("imagetype").value.split(';');
            for (var i = 0; i < type.length; i++) {
                var j = 0;
                j = fileExt.indexOf(type[i]);
                if (j >= 0) {
                    return true;
                    continue;
                }
            }
            return false;
        }
        return true;
    }
    else
        return false;
}

//提交时验证
function Input() {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances.newsBody;
    // Get the editor contents
    var content = oEditor.getData()
    // var newsBody = FCKeditorAPI.GetInstance('newsBody').GetXHTML(true);
    if (content == "") {
        return alertmsg('新闻正文不能为空', '', false);
    }
    if (document.getElementById("rbcommonnews").checked == false && document.getElementById("rbpicnews").checked == false && document.getElementById("rbcaptionnews").checked == false) {
        return alertmsg('新闻类型至少选择一种类型.', '', false);
    }

    if (document.getElementById("tbnewsname").value == "") {
        return alertmsg('新闻标题不能为空.', '', false);
    }

    if (document.getElementById("hdgetid").value == "") {
        return alertmsg("请先选择新闻栏目.", '', false);
    }
    if (document.getElementById("rbcaptionnews").checked == true) {
        if (document.getElementById("tblinkaddress").value == "") {
            return alertmsg('你已选择标题新闻,链接地址不能为空.', '', false);
        }

        if (document.getElementById("tblinkaddress").value.search(/^http:\/\//) == -1) {
            return alertmsg('链接地址格式有误,请重新输入.', '', false);
        }
    }

    if (document.getElementById("rbpicnews").checked == true || document.getElementById("rbcaptionnews").checked == true) {
        if (document.getElementById("rbpicurl").checked == false && document.getElementById("rbpicupload").checked == false) {
            return alertmsg('请选择图片URL或上传图片.', '', false);
        }

        if (document.getElementById("rbpicurl").checked == true) {
            if (document.getElementById("tbpinurl").value == "") {
                return alertmsg('你已选择图片链接,请输入链接地址.', '', false);
            }

            if (document.getElementById("tbpinurl").value.search(/^http:\/\//) == -1) {
                return alertmsg('链接地址格式错误，请重新输入.', '', false);
            }
        }

        if (document.getElementById("rbpicupload").checked == true) {
            if (!IsUploadFile()) {
                return alertmsg('你已选择上传图片,请选择上传的图片.', '', false);
            }
        }
    }

    if (document.getElementById("rbcommonnews").checked == true || document.getElementById("rbpicnews").checked == true) {
        if (document.getElementById("tbnewsauthor").value == "" && document.getElementById("ddlnewsauthor").value == "-1") {
            return alertmsg('你选择了图片或普通新闻,请选择或输入新闻作者.', '', false);
        }

        if (document.getElementById("tbnewsorigin").value == "" && document.getElementById("ddlnewsorigin").value == "-1") {
            return alertmsg('你选择了图片或普通新闻,请选择或输入新闻来源.', '', false);
        }
    }
}

//是否数字
function IsNum() {
    var txt = document.getElementById("tbcount").value;
    if (checknumber(txt)) {
        return alertmsg('只允许输入数字.', '', false);
    }
    return true;
}

//是否数字验证
function checknumber(string) {
    var letters = "1234567890";
    var i;
    var c;
    for (i = 0; i < string.length; i++) {
        c = string.charAt(i);
        if (letters.indexOf(c) == -1) {
            return true;
        }
    }
    return false;
}
//获得新闻作者
function getauthor() {
    if (document.getElementById("ddlnewsauthor").value != "") {
        document.getElementById("tbnewsauthor").value = document.getElementById("ddlnewsauthor").value;
    }
}

//获得新闻来源
function getorigin() {
    if (document.getElementById("ddlnewsorigin").value != "") {
        document.getElementById("tbnewsorigin").value = document.getElementById("ddlnewsorigin").value;
    }
}



//页面初始时绑定类型
function AddNewsPageInit() {
    TypeChange();
    ChangePicType();

    if (document.getElementById("rbpicurl").checked == false && document.getElementById("rbpicupload").checked == false) {
        document.getElementById("rbpicupload").checked = true;
    }
}