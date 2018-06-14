function LabelClassDisplay(objID,objli)
{
    var obj = $(objID);
    if("none" == obj.style.display)
    {
        $(objli).getElementsByTagName("img")[0].src="../images/bg_spread.gif";
        obj.style.display = "block";
    }
    else
    {
        $(objli).getElementsByTagName("img")[0].src="../images/bg_shrink.gif";
        obj.style.display = "none";
    }
}
function SetLabelClass2(objid)
{    
    var arr = $("class2").getElementsByTagName("div");
    for(var i=0;i<arr.length;i++)
    {
        if(arr[i].id == "classitem" + objid){
            var inputarr = arr[i].getElementsByTagName("input");
            var cc = false;
            for (var i=0;i<inputarr.length;i++)
            {
                if(0 == i)
                    cc = inputarr[i].checked;
                if(cc)
                    inputarr[i].checked = false;
                else
                    inputarr[i].checked = true;
            }
            return;    
        }            
    }
    var inputarrcls1 = $("pnlSuperClass").getElementsByTagName("input");
    for (var i=0;i<inputarrcls1.length;i++)	{
		if(inputarrcls1[i].id == "input_0_" + objid)
		    inputarrcls1[i].checked = true;
	}
    
    var tmpdiv = document.createElement("div");
    tmpdiv.id = "classitem" + objid;    
    tmpdiv.className = "class2item";
    var html = "<ul>"
    var obj = $("li_" + objid);
    var inputarr = obj.getElementsByTagName("input");
    
    for (var i=0;i<inputarr.length;i++)
	{
		if(inputarr[i].id.indexOf("input_" + objid + "_")!=-1)
		{
		    if(inputarr[i].type=='checkbox')
                html += "<li><input id=\"input2_" + inputarr[i].value.split("|")[0] + "\" checked=\"checked\" type=\"checkbox\" value=\"" + inputarr[i].value + "\" />" + unescape(inputarr[i].value.split("|")[1]) + "</li>";
	    }
	}
	
	html += "</ul>"
	tmpdiv.innerHTML = html;	
	$("class2").appendChild(tmpdiv);
}
function LabelClassAssembled() {
    var obj1 = GetClassValue("pnlSuperClass");
    if(obj1.id == "") {
        sAlert("请选择一级分类！");
        return;
    }
    var obj2 = GetClassValue("class2");
    var html = "<div><input type=\"text\" readonly=\"true\" value=\"" + unescape(obj1.name) + "\" maxlength=\"100\" size=\"80\" /><br/><textarea rows=\"4\" cols=\"100\" readonly=\"true\">" + unescape(obj2.name) + "</textarea><br/><br/></div>";
    $("tdComClass").innerHTML += html;
    $("hddValue").value += "" == $("hddValue").value ? "" : "$"
    $("hddValue").value += obj1.value + "#" + obj2.value;
    LabelClassClearCls2();
}
function LabelClassClear() {
    LabelClassClearCls2();
    $("hddValue").value = "";
    $("tdComClass").innerHTML = "";
}
function LabelClassClearCls2(){
    var arr = $("pnlSuperClass").getElementsByTagName("input");
    for(var i=0;i<arr.length;i++) {
        if(arr[i].type=='checkbox') {
            if(arr[i].checked) {
                arr[i].checked = false;
            }
        }
    }
    var arrdiv = $("class2").getElementsByTagName("div");
    for(var i=0;i<arrdiv.length;) {
        $("class2").removeChild(arrdiv[i]);
    }
}

function GetClassValue(strDivID) {
    var arr = $(strDivID).getElementsByTagName("input");    
    var strid = "";
    var strname = "";
    var strvalue = "";
    for(var i=0;i<arr.length;i++) {
        if(arr[i].type=='checkbox') {
            if(arr[i].checked) {
                strid += "" == strid ? "" : ",";
                strid += arr[i].value.split("|")[0];
                strname += "" == strname ? "" : " ";
                strname += arr[i].value.split("|")[1].trim();
                strvalue += "" == strvalue ? "" : ",";
                strvalue += arr[i].value;
            }
        }
    }
    return {
        id:strid,
        name:strname,
        value:strvalue
    }
}


/*******************************************************************/




function delall(){
    var arrs = $("pnlSuperClass").getElementsByTagName("input"); 
    var str = "";
    var num = 0;
    for(var i=0;i<arrs.length;i++){
        if(arrs[i].type == 'checkbox'){
            if(arrs[i].checked){
                str += arrs[i].value + ",";
                num =1;
            }
        }
    }
    if(num == 0){
        alert("请选择要删除的分类！");
        return false;
    }
    $("strdel").value = str;
}

function createordelete(tip,whichone){
  var arrs = $("pnlSuperClass").getElementsByTagName("input"); 
    var str = "";
    var num = 0;
    for(var i=0;i<arrs.length;i++){
        if(arrs[i].type == 'checkbox'){
            if(arrs[i].checked){
                str += arrs[i].value + ",";
                num =1;
            }
        }
    }
    if(num == 0){
        alert(tip);
        return false;
    }
    $("strids").value = str;
    $("cod").value = whichone;
}



//add by liujia 2008-11-18 地区标签的信息页
function ShowToPage(obj) {
    if("job" == obj.value) {
        $("rblToPage").style.display = "none";
    }
    else {
        $("rblToPage").style.display = "block";
    }
}
