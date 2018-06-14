<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCount.aspx.cs" Inherits="GSSGameWeb.WebCount" %>
 var hff = window.screen.height; 
 var wff = window.screen.width; 
 var url="<%=NowURL %>&w="+wff+"&h="+hff+"";

function createXMLHttp() {
	if(window.XMLHttpRequest){
		return new XMLHttpRequest();
	} else if(window.ActiveXObject){
		return new ActiveXObject("Microsoft.XMLHTTP");
	} 
	//throw new Error("XMLHttp object could be created.");
}
function ajaxRead(file,fun){
	var xmlObj = createXMLHttp();

	xmlObj.onreadystatechange = function(){
		if(xmlObj.readyState == 4){
			if (xmlObj.status ==200){
				obj = xmlObj.responseXML;
				eval(fun);
			}
			else{
				//alert("读取文件出错,错误号为 [" + xmlObj.status  + "]");
			}
		}
	}
	xmlObj.open ('GET', file, true);
	xmlObj.send (null);
}
ajaxRead(url,"");
