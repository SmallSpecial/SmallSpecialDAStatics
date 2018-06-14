function killerr() {return true}
window.onerror=killerr

function oCopy(str)
{
var clipBoardContent=str;
window.clipboardData.setData("Text",clipBoardContent);
alert("代码复制成功");
}
//gridview 鼠标经过时变色
var _oldColor;
function SetNewColor(source) {

    _oldColor = source.style.backgroundColor;

    source.style.backgroundColor = '#D9EDFA';

}

function SetOldColor(source) {

    source.style.backgroundColor = _oldColor;

}
//onMouseMove="this.className='td3'" onMouseOut="this.className='td2'">
/*生成html*/
function Cre(i) {
  if(i.style.display == "none"){
    i.style.display="";
  }else{
    i.style.display="none";
  }
}

//选取所有
function CheckAll(form)  {
  for (var i=0;i<form.elements.length;i++)    {
    var e = form.elements[i];
    if (e.name != 'chkall')e.checked = form.chkall.checked;
   }
  }

//设定图片显示尺寸
var flag=false;
function DrawImage(ImgD,w,h){
var image=new Image();
image.src=ImgD.src;
if(image.width>0 && image.height>0){
  flag=true;
  if(image.width/image.height>= w/h){
    if(image.width>w){
      ImgD.width=w;
      ImgD.height=(image.height*w)/image.width;
    }else{
      ImgD.width=image.width;
      ImgD.height=image.height;
    }
  }else{
    if(image.height>h){
      ImgD.height=h;
      ImgD.width=(image.width*h)/image.height;
    }else{
      ImgD.width=image.width;
      ImgD.height=image.height;
    }
  }
}
}

function show(i,n,t){
if (i.style.display == "none") {
		i.style.display = "";
		n.style.display = "";
		t.style.background = "url('img/Menu_BG.gif')";
	}else{
		i.style.display = "none";
		n.style.display = "none";
		t.style.background = "url('img/Menu_BG1.gif')";
	}
}

function TopType(){
if(Top.style.display == ""){
Top.style.display = "none";
TopPic.src="img/p_5.gif"
}else{
Top.style.display = "";
TopPic.src="img/p_6.gif"
}
}

function pTopType(){
parent.Top.style.display = "";
parent.TopPic.src="img/p_6.gif"
}
function pTopTypeC(){
parent.Top.style.display = "none";
parent.TopPic.src="img/p_5.gif"
}


function MenuType(){
if(menu.style.display == ""){
menukey.innerHTML = ">> 显示菜单";
menu.style.display = "none";
}else{
menukey.innerHTML = "<< 隐藏菜单";
menu.style.display = "";
}
}

// 文章处理 Begin =============================================
//转向文章新闻添加
function WM_LinkCheck(){
  if(document.myform.WM_UseLinkUrl.checked==true){
    document.myform.WM_LinkUrl.disabled=false;
    document.myform.WM_LinkUrl.style.color='red';
     ArticleContent1.style.display='none';
  }
  else{
    document.myform.WM_LinkUrl.disabled=true;
    ArticleContent1.style.display='';
  }
}

//新闻添加
function doChange(objText, objDrop){
	if (!objDrop) return;
	var str = objText.value;
	var arr = str.split("|");
	objDrop.length=0;
	for (var i=0; i<arr.length; i++){
	objDrop.options[i] = new Option(arr[i], arr[i]);
	PicUrl=objDrop.options[i].value;
	}
	document.myform.WM_DefaultPicUrl.value = PicUrl;
	myform.WM_PicPreview.src = PicUrl;
	myform.WM_PicPreview.alt = PicUrl;
}
// 文章处理 End =============================================


//菜单书签
var mID=0;
function menudis(newID){
  if(mID!=newID){
	ShowM[mID].style.display="none";
	tit[mID].className="mLabel";
	titfont[mID].className="mLabeltit";
	ShowM[newID].style.display="";
	tit[newID].className="mLabel1";
	titfont[newID].className="mLabeltit1";
	mID = newID;
  }
}

//后台书签
function dis(tit,show,num,pic){
  for(g=1;g<=num;g++){
	document.all("tit"+g).background="Img/"+pic+"_1.gif";
	document.all("show"+g).style.display="none";
	document.all("tit"+g).className="Label";
  }
  if(tit.style.display=="none"){
    tit.background="Img/"+pic+"_1.gif";
	show.style.display="none";
	tit.className="Label";
	}
	else{
	tit.background="Img/"+pic+".gif";
	show.style.display="";
	tit.className="Label1";
	}
}

//System_Templates.asp
function temp(i,n){
if (n.style.display == "none") {
		i.src = "img/-.gif";
		n.style.display = "";
	}else{
		i.src = "img/+.gif";
		n.style.display = "none";
	}
}

// 显示无模式对话框
function ShowDialog(url, width, height) {
	var arr = showModalDialog(url, window, "dialogWidth:" + width + "px;dialogHeight:" + height + "px;help:no;scroll:no;status:no");
}

function preview() /* Preview for the "Compilation" */
	{
		if(document.myform.HtmlCode.value.length>0)
			{
				pr=window.open("","Preview","scrollbars=1,width=800,height=500");
				pr.document.write(document.myform.HtmlCode.value);
			}
		else alert('无任何内容可供预览!')
}

//添加广告表单
function ads(i){
  switch(i){
    case "text":
      ad1.style.display = "none";
      ad2.style.display = "";
      ad3.style.display = "";
      ad4.style.display = "none";
      ad5.style.display = "";
      ad6.style.display = "none";
	  break;
    case "pic":
      ad1.style.display = "";
      ad2.style.display = "none";
      ad3.style.display = "";
      ad4.style.display = "";
      ad5.style.display = "";
      ad6.style.display = "none";
	  break;
    case "swf":
      ad1.style.display = "";
      ad2.style.display = "none";
      ad3.style.display = "none";
      ad4.style.display = "";
      ad5.style.display = "none";
      ad6.style.display = "none";
	  break;
    case "code":
      ad1.style.display = "none";
      ad2.style.display = "none";
      ad3.style.display = "none";
      ad4.style.display = "none";
      ad5.style.display = "none";
      ad6.style.display = "";
	  break;
  }
}

//添加频道
function Channel_Show(){
		Channel.style.display = "";
}
function Channel_Close(){
		Channel.style.display = "none";
}

/*标签添加输出格式选择*/
function Label(i){
  switch(i){
    case "1":
      table.style.display = "none";
      div.style.display = "none";
	  break;
    case "2":
      table.style.display = "";
      div.style.display = "none";
	  break;
    case "3":
      table.style.display = "none";
      div.style.display = "";
	  break;
  }
}

/*标签添加输出格式选择*/
function Label2(i){
  switch(i){
    case "1":
      table1.style.display = "";
      div1.style.display = "none";
      table2.style.display = "";
      div2.style.display = "none";
	  break;
    case "2":
      table1.style.display = "none";
      div1.style.display = "";
      table2.style.display = "none";
      div2.style.display = "";
	  break;
  }
}

//上传操作 Begin ============================================
//上传完成后返回参数处理
function upback(Spic,str){
  if(str!=""){
  EditContent.InsertLabel('<IMG SRC=' + str + ' onload=\"DrawImage(this, 500, 500)\">');
  if(Spic!=""){
      document.myform.Pic.value = Spic;
      Add(Spic);
      PicPreview.innerHTML = "<Img src="+Spic+" onload=\'DrawImage(this, 118, 118);\'>";
    }else{
      document.myform.Pic.value = str;
      Add(str);
      PicPreview.innerHTML = "<Img src="+str+" onload=\'DrawImage(this, 118, 118);\'>";
    }
  }
  closeWithIframe();
}

//优惠券上传完成后返回参数处理
var ii=0;
function upcou(Spath,path) {
    var sel1 = document.myform.PicList.value;
    var sel2 = plist.innerHTML;
	var path1 = "<div id=l_"+ii+">"+path+"　　<a style='cursor:pointer !important;cursor:hand;' onclick=PicReset('"+Spath+"')>做为缩略图</a>　<a  style='cursor:pointer !important;cursor:hand;' onclick=dlist('"+ii+"','"+path+"','"+Spath+"')>删除</a></div>";
	if(sel1!=""){
	  sel1 = sel1 +'|' + path;
	  sel2 = sel2 + path1;
	}else{
	  sel1 = path;
	  sel2 = path1;
	}
    document.myform.PicList.value = sel1;
    plist.innerHTML = sel2;
    document.myform.Pic.value = Spath;
	PicPreview.innerHTML = "<Img src="+Spath+" onload=\'DrawImage(this, 118, 118);\'>";
    closeWithIframe();
    ii = ii+1;
}
function dlist(id,str,sStr){
  document.getElementById("l_"+id).style.display = "none";
  var sel2 = document.myform.PicList.value;
  var sel1 = document.myform.Pic.value;
  var str1="";
  var forpic="";
  sel = sel2.split("|");
  for(i=0;i<=sel.length;i++){
    forpic = sel[i];
    if(typeof(forpic) == "undefined"){forpic="";}
    if(forpic != "" && forpic != str){
	  if(str1!=""){
	    str1 = str1+"|"+sel[i];
	  }else{
	    str1 = sel[i];
	  }
	}
  }
  if(typeof(str1) == "undefined"){str1="";}

  if(sel1==sStr || str1==""){
    PicReset("");
  }
  document.myform.PicList.value = str1;
}

function Add(str) {
	var sel2=document.myform.PicSelect;
	sel2.options[sel2.length]=new Option(str,str);
	sel2.selectedIndex = sel2.length-1;
}

function PicReset(str){
document.myform.Pic.value = str;
if (str == ""){
PicPreview.innerHTML = "<Img src=img/nopic.gif onload=\'DrawImage(this, 118, 118);\'>";
}else{
PicPreview.innerHTML = "<Img src="+str+" onload=\'DrawImage(this, 118, 118);\'>";
}
}

//礼品回传操作
function upshop(pic){
  document.myform.Pic.value = pic;
  closeWithIframe();
}

//用户头像回传操作
function upother(str){
  document.myform.Pic.value = str;
  document.images['PicShow'].src=str;
  closeWithIframe();
}

//广告图片回传操作
function upad(str){
  document.myform.Pic.value = str;
  closeWithIframe();
}
//上传操作 End ============================================

function sift(){
  if (myform.c.value !=""){
    if (myform.c.value == "|"){
	  myform.c.value = "";
      myform.c.focus();
      alert('条件不能为“|”');
	}else{
      var sift;
      var siftv = myform.Leach.value;
      sift = myform.a.children[myform.a.selectedIndex].value + '|' + myform.b.children[myform.b.selectedIndex].value + '|'  + myform.c.value + '|' + myform.d.children[myform.d.selectedIndex].value;
      if(siftv.indexOf(sift)!=-1){
	    alert('此过滤器规则在下面列表中已存在');
	    myform.c.focus();
	    return false;
	  }
	  if (siftv!=""){
	    myform.Leach.value =  myform.Leach.value+'\n'+sift;
	  }else{
	    myform.Leach.value =  sift;
	  }
	  myform.c.value = "";
	  myform.c.focus();
	}
  }else{
    myform.c.focus();
    alert('条件不完整');
  }
}

function ad_pic(){
  if (myform.L.value =="" || myform.Pic.value =="" ){
    myform.L.focus();
    alert('输入不完整');
  }else{
    if(myform.P.value !=""){
	  myform.P.value = myform.L.value+'|'+myform.Pic.value+'\n'+myform.P.value;
	}else{
	  myform.P.value = myform.L.value+'|'+myform.Pic.value;
	}
	myform.L.value = "";
	myform.Pic.value = "";
  }
}

/*打开选项控制示例*/
var Skey
Skey=0;
function Select(){
  if(Skey==0){
    Skey=1;
    SelectPic.style.display="";
  }else{
    Skey=0;
    SelectPic.style.display="none";
  }
}

//ajax提示框 Begin ========================================
var Obj=''
document.onmouseup=MUp
document.onmousemove=MMove

function MDown(Object){
Obj=Object.id
document.all(Obj).setCapture()
pX=event.x-document.all(Obj).style.pixelLeft;
pY=event.y-document.all(Obj).style.pixelTop;
}

function MMove(){
if(Obj!=''){
  document.all(Obj).style.left=event.x-pX;
  document.all(Obj).style.top=event.y-pY;
  }
}

function MUp(){
if(Obj!=''){
  document.all(Obj).releaseCapture();
  Obj='';
  }
}
document.write('<div id="massage_box" style="display:none">loading...</div>');
document.write('<div id="bgDiv" style="display:none"></div>');
function openWithIframe(tit,url,w,h){
    //屏幕背景变暗
	var sWidth,sHeight;
	sWidth=document.body.clientWidth;
	sHeight=document.body.scrollHeight;
	if(sHeight<window.screen.height){sHeight=window.screen.height;}
	document.getElementById("bgDiv").style.display="";
	document.getElementById("bgDiv").style.position="absolute";
	document.getElementById("bgDiv").style.top="0";
	document.getElementById("bgDiv").style.background="#000000";
	document.getElementById("bgDiv").style.filter="Alpha(Opacity=30);";
	document.getElementById("bgDiv").style.left="0";
	document.getElementById("bgDiv").style.width=sWidth + "px";
	document.getElementById("bgDiv").style.height=sHeight + "px";
	document.getElementById("bgDiv").style.zIndex = "10000";
    //显示层
    document.getElementById("massage_box").style.left = (document.body.clientWidth - w) / 2;
    //document.getElementById("massage_box").style.top = (screen.height - h) / 2-80;
    document.getElementById("massage_box").style.screenx = (document.body.clientWidth - w) / 2;//仅适用于Netscape
    document.getElementById("massage_box").style.screeny = (screen.height - h) / 2-80;//仅适用于Netscape
    document.getElementById("massage_box").style.width = w+"px";
    document.getElementById("massage_box").style.height = h+"px";
    document.getElementById("massage_box").className = "massage_box";
	var popiframe;
	popiframe = '<div style="border-width:1 1 3 1; width:100%; height:100%; background:#fff; color:#666666; font-size:12px; ">'
	popiframe += '<div onmousedown=MDown(massage_box) style="background:#666666; height:20px; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px;color:#fff;cursor:move;padding:0 0 4px 0">'
	popiframe += '<div style="display:inline; width:200px; position:absolute;padding:3px 0 0 5px" id=pop_title>'+tit+'</div>'
	popiframe += '<span onClick="closeWithIframe()" style="float:right; display:inline; cursor:pointer;padding:3px 5px 0 0;font-size:12px">关闭</span>'
	popiframe += '</div>'
	popiframe += '<div style="padding:5px" ><iframe src="'+url+'" width="'+(w-11)+'px"  height="'+(h-36)+'px" frameborder=0 scrolling=no></iframe></div>'
	popiframe += '</div>'
    document.getElementById("massage_box").innerHTML=popiframe;
    document.getElementById("massage_box").style.display = "";
}
function closeWithIframe(){
    document.getElementById("massage_box").style.display="none";
    document.getElementById("bgDiv").style.display="none";
}
//ajax提示框功能========================================

//公用ajax
function InitAjax(){
var ajax=false;
try { ajax = new ActiveXObject("Msxml2.XMLHTTP"); }
catch (e) { try { ajax = new ActiveXObject("Microsoft.XMLHTTP"); } catch (E) { ajax = false; } }
if (!ajax && typeof XMLHttpRequest!='undefined') { ajax = new XMLHttpRequest(); }
return ajax;}
function ToAjax(url,Post){
	if (url!=""){
		var getinfo = "";
		var ajax = InitAjax();
		ajax.open("POST", url, true);
		ajax.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded; charset=GB2312");
		ajax.send(Post);
	    ajax.onreadystatechange = function(){
		  if (ajax.readyState == 4){getinfo = ajax.responseText;}
	      AjaxShow.innerHTML = getinfo;
		  }
	}
}

var a = 0;
function Edit(i){if (a==0){  a = 1;  i.style.display = "";}else{ a = 0; i.style.display = "none";}}
var b = 0;
function Edit2(i){if (b==0){  b = 1;  i.style.display = "";}else{  b = 0;  i.style.display = "none";}}

function TagEdit(str,n){
  var econ = document.getElementById(str).innerText;
  if(econ==""){econ=myform.str.value;}
  document.getElementById(str).innerHTML="<input name="+str+" style='width:100%' value="+econ+">";
  var ncon = document.getElementById(n).innerText;
  if(ncon==""){ncon=myform.n.value;}
  document.getElementById(n).innerHTML="<input name="+n+" style='width:100%' value="+ncon+">";
}


//生成自定义选项JS
function DBType(s,str){
  if(str==1){
    D_B[s-1].style.display = "none";
    D_B_1[s-1].style.display = "";
	D_B_2[s-1].style.display = "none";
  }else{
    D_B[s-1].style.display = "";
    D_B_1[s-1].style.display = "none";
	D_B_2[s-1].style.display = "";
  }
}
//添加店铺时AJAX调用自定义选项
//t 1分类信息2店铺 sid 分类ID id 内容id
function dbajax(t,sid,id){
		var getinfo = "";
		var ajax = InitAjax();
		ajax.open("POST","../Inc/WM.DB.Ajax.asp" , true);
		ajax.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded; charset=GB2312");
		ajax.send("T="+t+"&SortID="+sid+"&ID="+id+"&F=Class");
	    ajax.onreadystatechange = function(){
		  if (ajax.readyState == 4){getinfo = ajax.responseText;}
	      DBShow.innerHTML = getinfo;
		  if(getinfo!=""){DBtr.style.display="";}else{DBtr.style.display="none";}
		}
}
function areadbajax(sid,carea,isedit){
		var getinfo = "";
		var ajax = InitAjax();
		ajax.open("POST","../Inc/WM.DB.Ajax.asp" , true);
		ajax.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded; charset=GB2312");
		ajax.send("SortID="+sid+"&IsEdit="+isedit+"&F=Area&C="+escape(carea));
	    ajax.onreadystatechange = function(){
		  if (ajax.readyState == 4){getinfo = ajax.responseText;}
		  if (isedit==0 && carea!=""){
	        ChAreaDBShow.innerHTML = getinfo;
		  }else{
	        AreaDBShow.innerHTML = getinfo;
		    if(getinfo!=""){AreaDBtr.style.display="";}else{AreaDBtr.style.display="none";}
		  }
		}
}

function replaceInput(obj,event){
	if(event.keyCode!=37 && event.keyCode!=39){
      var IsTitle=/[^\u4E00-\u9FA50-9a-zA-Z_\.]/g;
	  if(IsTitle.test(obj.value)){
	    obj.value=obj.value.replace(IsTitle,'');
	  }
	}
}

function rUserName(str){
  if(str =="") {
    document.getElementById("AjaxShow").innerHTML = "<font color=red><strong>用户名不能为空</strong></font>";
    document.myform.UserName.focus();
    document.myform.UserName.select();
	return false;
  }
  document.getElementById("AjaxShow").innerHTML = "loading...";
  ToAjax("../Reg.asp","Action=Check&w=u&User="+escape(str))
}

//更换店铺所有者
function comuser(id){
    if (document.getElementById("e1_"+id).style.display == "none"){
	  document.getElementById("e1_"+id).style.display = "";
      document.getElementById("e2_"+id).style.display = "none";
    }else{
	  document.getElementById("e1_"+id).style.display = "none";
      document.getElementById("e2_"+id).style.display = "";
	}
}

function tocomuserajax(u,id){
	if (u==""){
		alert("所属用户不能为空");
        comuser(id);
	}else{
		var getinfo = "";
		var ajax = InitAjax();
		ajax.open("POST","Admin_Top_Ajax.asp" , true);
		ajax.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded; charset=GB2312");
		ajax.send("Action=RegComUser&u="+escape(u)+"&id="+id);
	    ajax.onreadystatechange = function(){
		  if (ajax.readyState == 4){getinfo = ajax.responseText;
		    if(getinfo!=""){alert(getinfo);}else{location.reload();}
		  }
		}
	}
}