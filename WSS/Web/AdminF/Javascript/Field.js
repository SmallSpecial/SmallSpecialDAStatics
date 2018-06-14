﻿function field(objName,divtop,divfield,cbisfull,hdddelids) {
    var index = 0;
    var divtopobj = $(divtop);
    var divfieldobj = $(divfield);
    var cbisfullobj = $(cbisfull);
    var hdddelidsobj = $(hdddelids);
    var istop = false;//是否是顶级类
    this.topfieldarr = [];//顶级类的字段
    this.fieldarr = [];//自身类的字段
    this.inheritfield = [];//继承的字段
    this.topfield_ul = [];
    
    this.Init = function(fieldarr, topobj) {
        istop = topobj.istop;
        //添加已有的字段信息
        this.fieldarr = fieldarr;
        for(var i = 0; i < this.fieldarr.length; i ++) {
            this.AddLine(divfieldobj,this.fieldarr[i]);
        }
        
        //如果为顶级类则退出
        if(topobj.istop) return;
        
        //添加顶级类字段信息
        this.topfieldarr = topobj.topfieldarr;
        for(var i = 0; i < this.topfieldarr.length; i ++) {
            this.AddLine(divtopobj,this.topfieldarr[i],true);
        }
        
        cbisfullobj.checked = topobj.inherit.isinherit;
        //隐藏未继承的顶级类字段并勾选已经继承的
        this.inheritfield = topobj.inherit.fieldarr;
        if(undefined == this.inheritfield) this.inheritfield = [];
        
        for(var i = 0; i < this.topfield_ul.length; i ++) {
            $("txtEName" + this.topfield_ul[i]).disabled = true;
            $("txtCName" + this.topfield_ul[i]).disabled = true;
            $("txtdesp" + this.topfield_ul[i]).disabled = true;
            $("txtSelect" + this.topfield_ul[i]).disabled = true;
            $("seltype" + this.topfield_ul[i]).disabled = true;
            $("txtFieldSize" + this.topfield_ul[i]).disabled = true;
            //如果完全继承
            if(topobj.inherit.isinherit) {
                $("cbIsFull").checked = true;
                $("topid" + this.topfield_ul[i]).checked = true;
                $("topid" + this.topfield_ul[i]).disabled = true;
            }
            else if(!topobj.inherit.isinherit && undefined == topobj.inherit.fieldarr) {
                $("cbIsFull").checked = true;
                $("topid" + this.topfield_ul[i]).checked = true;
                $("topid" + this.topfield_ul[i]).disabled = true;
            }
            else if(this.IsInherit($F("topid" + this.topfield_ul[i]))) {
                $("topid" + this.topfield_ul[i]).checked = true;
            }
            else {
                $("ul" + this.topfield_ul[i]).style.display = "none";
            }
        }
    }
    
    this.IsInherit = function (id) {
        for(var i = 0; i < this.inheritfield.length; i++) {
            if(parseInt(id) == parseInt(this.inheritfield[i])) {
                return true;
            }
        }
        return false;
    }
    
    this.ShowTopAll = function() {
        for(var i = 0; i < this.topfield_ul.length; i ++) {
            $("ul" + this.topfield_ul[i]).style.display = "block";
        }
    }
    
    this.SelectTopAll = function() {
        this.ShowTopAll();
        for(var i = 0; i < this.topfield_ul.length; i ++) {
            $("topid" + this.topfield_ul[i]).checked = cbisfullobj.checked;
            $("topid" + this.topfield_ul[i]).disabled = cbisfullobj.checked;
        }
    }
    
    this.Check = function() {
        var msg = "";
        var inputfieldarr = divfieldobj.getElementsByTagName("input");
        for(var i = 0; i < inputfieldarr.length; i++) {
            if(inputfieldarr[i].id.indexOf("txtEName") > 0) {
                for(var j = 0; j < this.topfield_ul.length; j++) {
                    if(cbisfullobj.checked || $("topid" + this.topfield_ul[j]).checked) {
                        if(inputfieldarr[i].value == $("txtEName" + this.topfield_ul[j]).value) {
                            msg += "字段" + inputfieldarr[i].value + "已被父类继承，无法添加该字段<br />";
                        }
                    }                    
                }
            }
        }
        if(msg != "") {
            sAlert(msg);
            return false;
        }
        return true;
    }
    
    this.AddNewLine = function() {
        var newobj = {id:'',ename:'',cname:'',desp:'',type:'',select:'',fieldsize:'50'};
        this.AddLine(divfieldobj,newobj);
    }
    
    this.AddLine = function(docobj,fieldobj,istop) {
        insertHtml("beforeend",docobj,this.GetHtml(fieldobj,istop));
    }
    
    this.Delete = function(objindex) {
        if("" == $F("id" + objindex)) {
            if(confirm("确定删除吗？")) {
                this.DeleteHtmlObj(objindex);
            }
        }
        else if(istop) {
            var msg = '该类是顶级类，子类可能已经继承该类，您将要的操作是：<br />';
            msg += '<a href="javascript:' + objName + '.DeleteChild(1,' + objindex + ');">删除该类以及被继承的子类</a> ';
            msg += '<a href="javascript:' + objName + '.DeleteChild(2,' + objindex + ');">复制该类字段到子类并删除</a> ';
            msg += '<a href="javascript:sClose();">取消操作</a>';
            sAlert(msg);
        }
        else {
            if(confirm("确定删除吗？")) {
                this.DeleteChild(0,objindex);
            }
        }
    }
    
    this.DeleteChild = function(actype,objindex) {        
        this.AddDelID(actype,$F("id" + objindex));
        this.DeleteHtmlObj(objindex);
        sClose();
    }
    
    this.DeleteHtmlObj = function(objindex) {
        if($("ul" + objindex)) {
            divfieldobj.removeChild($("ul" + objindex));
        }
    }
    //actype 0表示直接删除（针对子类有效），无其他操作 1删除继承的所有子类 2复制给所有子类 然后删除
    this.AddDelID = function(actype,id) {
        hdddelidsobj.value += "" == hdddelidsobj.value ? "" : ",";
        hdddelidsobj.value += actype + "|" + id;
    }
    
    this.GetHtml = function(obj,istop) {
        var str = '<ul id="ul'+index+'">';
        if(undefined == istop) {
            str += '<li class="fieldlistid"><input type="hidden" id="id' + index + '" name="id" value="' + obj.id + '" /></li>';
        }
        else {
            this.topfield_ul[this.topfield_ul.length] = index;
            str += '<li class="fieldlistid"><input type="checkbox" id="topid' + index + '" name="topid" value="' + obj.id + '" /></li>';
        }
        str += '<li><input  size="16"  id="txtEName'+index+'" ' + (istop ? '' : 'name="txtEName" ') + ' type="text" value="' + obj.ename + '" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emEName'+index+'" style="display:none;">↑字段英文名</em></li>';
        str += '<li><input  size="16"  id="txtCName'+index+'" ' + (istop ? '' : 'name="txtCName" ') + ' type="text" value="' + obj.cname + '" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emCName'+index+'" style="display:none;">↑字段中文名</em></li>';
        str += '<li><textarea id="txtdesp'+index+'" ' + (istop ? '' : 'name="txtdesp" ') + ' cols="19" rows="1" onblur="taunfocusalt(this);" onfocus="tafocusalt(this);" >' + obj.desp + '</textarea><em id="emdesp'+index+'" style="display:none;">↑字段说明文字</em></li>';        
        str += '<li><textarea id="txtSelect'+index+'" ' + (istop ? '' : 'name="txtselect"') + ' cols="19" rows="1" onblur="taunfocusalt(this);" onfocus="tafocusalt(this);" >' + obj.select + '</textarea><em id="emSelect'+index+'" style="display:none;">↑字段预留值</em></li>';
        str += '<li>' + this.GetType(obj.type,istop) + '</li>';
        str += '<li><input id="txtFieldSize'+index+'" class="field_size" size="16" ' + (istop ? '' : 'name="txtFieldSize"') + ' type="text" value="' + obj.fieldsize + '" onblur="unfocusalt(this);" onfocus="focusalt(this);"  /><em id="emFieldSize'+index+'" style="display:none;">↑字段大小，小于8000</em></li> ';
        if(undefined == istop) {
            str += '<li><a href="javascript:' + objName + '.Delete('+index+');"><img src="../images/fielddel.gif" alt="删除"/></a></li>';
        }
        str += "</ul>";
        index++;
        return str;
    }
    
    this.GetType = function(value,istop) {
        value = value.toLowerCase();
        var str = '<select id="seltype' + index + '" ' + (istop ? '' : 'name="seltype"') + '>';
        str += '<option value="Text"' + ('text' == value ? ' selected="selected"' : '') + '>文本框</option>';
        str += '<option value="textarea"' + ('textarea' == value ? ' selected="selected"' : '') + '>多行文本区</option>';
        str += '<option value="Select"' + ('select' == value ? ' selected="selected"' : '') + '>下拉框</option>';
        str += '<option value="Radio"' + ('radio' == value ? ' selected="selected"' : '') + '>单选框</option>';
        str += '<option value="Checkbox"' + ('checkbox' == value ? ' selected="selected"' : '') + '>复选框</option>';
        return str;
    }
}
