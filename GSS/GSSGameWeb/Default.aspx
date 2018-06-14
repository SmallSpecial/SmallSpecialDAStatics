<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GSSGameWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>游戏反馈系统</title>

    <script type="text/javascript" src="javascript/common.js"></script>

    <script type="text/javascript" src="javascript/locations.js"></script>

    <script type="text/javascript" src="javascript/ajax.js"></script>

    <link rel="stylesheet" type="text/css" href="javascript/dnt.css" media="all">
    <link rel="stylesheet" href="javascript/float.css" type="text/css" />
    <link rel="stylesheet" href="javascript/main.css" type="text/css" />
    <style type="text/css">
        .pt-chatbox2
        {
            background-color: #FFFFFF;
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
        }
        .pt-chatbox2 textarea
        {
            background-color: #FFFFFF;
            width: 100%;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
            margin: 0px;
            padding: 0px;
            height: auto;
            overflow: auto;
        }
    </style>
</head>
<body scroll="no">
    <div id="append_parent">
    </div>
    <div id="success" style="position: absolute; z-index: 300; height: 120px; width: 284px;
        left: 50%; top: 50%; margin-left: -150px; margin-top: -80px;">
        <div id="Layer2" style="position: absolute; z-index: 300; width: 270px; height: 90px;
            background-color: #FFFFFF; border: solid #022739 1px; font-size: 14px;">
            <div id="Layer4" style="height: 26px; color: #fff; background: #21546c; line-height: 26px;
                padding: 0px 3px 0px 3px; font-weight: bolder;">
                操作提示</div>
            <div id="Layer5" style="height: 64px; line-height: 150%; padding: 0px 3px 0px 3px;"
                align="center">
                <br />
                <table>
                    <tr>
                        <td valign="top">
                            <img border="0" src="../images/ajax_loading.gif" />
                        </td>
                        <td valign="middle" style="font-size: 14px;">
                            正在执行当前操作, 请稍等...<br />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
        <div id="Layer3" style="position: absolute; width: 270px; height: 90px; z-index: 299;
            left: 4px; top: 5px; background-color: #E8E8E8;">
        </div>
    </div>

    <script>
        document.getElementById('success').style.display = "none"; 
    </script>

    <div class="uc_header">
        <ul class="f_tab">
            <li id="menu0" class="cur_tab"><a onclick="menuclick(0)" href="#">问题反馈</a> </li>
            <li id="menu1"><a onclick="menuclick(1)" href="#">投诉建议</a> </li>
            <li id="menu2" style="display: none"><a onclick="menuclick(2)" href="#">提交列表 </a>
            </li>
            <li id="menu3"><a href="#" onclick="menuclick(3)">问卷调查</a></li>
            <li id="menu4"><a href="#" onclick="menuclick(4)">在线咨询</a></li>
            <li style="display: none"><a target="_blank" href="http://xlj.1732.com">常见问题 </a>
            </li>
            <li style="display: none"><a id="notice" onmouseover="showMenu(this.id);" onmouseout="showMenu(this.id);"
                onclick="javascript:window.location.href=this.href;" href="usercpnotice.aspx"><cite
                    class="drop">通知 </cite></a></li>
        </ul>
    </div>
    <ul class="p_pop" id="notice_menu" style="display: none">
        <li><a href="#">全部通知</a></li>
        <li><a href="#">管理通知</a></li>
    </ul>
    <form id="postpm" method="post" name="postpm" action="File.ashx">
    <div id="maintaskadd" class="divadd">
        <div style="padding-left: 4px;">
            <h3>
                <em id="returnregmessage">&nbsp;</em>
            </h3>
            <div id="succeedmessage">
            </div>
        </div>
        <div id="taskaddui">
            <table class="tfm" summary="提交信息">
                <tbody>
                    <tr>
                        <th>
                            问题类型<font color="#dc054c">*</font>
                        </th>
                        <td colspan="3">
                            <select id="locus_0" style="display: none">
                                <option value="-1">----类型----</option>
                            </select>
                            <div style="float: left; border: 1px solid #7f9db9; width: 123px; height: 18px; overflow: hidden;">
                                <select id="locus_1" style="width: 125px; height: 24px; margin-left: -1px; margin-top: -2.5px;">
                                    <option value="-1">----问题类型----</option>
                                </select></div>
                            <span style="float: left; font-weight: 700; margin: 0px 10px 0px 20px;">具体问题</span>
                            <div style="float: left; border: 1px solid #7f9db9; width: 153px; height: 18px; overflow: hidden;">
                                <select id="locus_2" style="width: 155px; height: 24px; margin-left: -1px; margin-top: -2.5px;">
                                    <option value="-1">----具体问题----</option>
                                </select></div>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            温馨提示
                        </th>
                        <td colspan="3">
                            <span id="tasktips">如果您有什么建议或者要投诉什么，可在下方输入框里说明一下，然后点击“提交”按钮。游戏管理员将根据反馈的先后顺序回复您的问题。</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            描述内容<font color="#dc054c">*</font>
                        </th>
                        <td colspan="3">
                            <textarea id="textnote" style="width: 86%" cols="60" rows="6" onkeyup="checknote(this.value);"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            截图上传
                        </th>
                        <td colspan="3">
                            <div id="ddddd">
                                <table border="0" cellspacing="1" class="fu_list">
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <table border="0" cellspacing="0">
                                                    <thead>
                                                        <tr>
                                                            <td>
                                                                <div style="clear: both; float: left; margin-right: 10px">
                                                                    <a href="javascript:void(0);" class="files" id="idFile"></a>
                                                                </div>
                                                                上传 <b id="idExt"></b>图片,最大<b><%=maxuploadlength%>M</b>
                                                                <img id="idProcess" style="display: none; width: 110px;" src="images/loadingu.gif" />
                                                            </td>
                                                            <td width="60">
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="idFileList">
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td colspan="2" style="color: gray">
                                                温馨提示：最多可同时上传 <b id="idLimit"></b>个文件，。
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td colspan="2" align="center" id="idMsg">
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td colspan="2" align="center" id="idupbt">
                                                <input type="button" value="开始上传" id="idBtnupload" disabled="disabled" />
                                                &nbsp;&nbsp;&nbsp;
                                                <input type="button" value="全部取消" id="idBtndel" disabled="disabled" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系方式
                        </th>
                        <td colspan="3">
                            <span style="font-weight: 700; margin: 0px 8px 0px 0px;">QQ</span>
                            <input id="textqq" type="text" tabindex="1" value="" maxlength="13" size="10" onkeyup="checkqq(this.value);" />
                            <span style="font-weight: 700; margin: 0px 8px 0px 20px;">手机</span>
                            <input id="textmobile" type="text" tabindex="1" value="" maxlength="11" size="10"
                                onkeyup="checkmobile(this.value);" />
                            <span style="font-weight: 700; margin: 0px 8px 0px 20px;">验证码<font color="#dc054c">*</font></span>
                            <input id="textvercode" type="text" tabindex="1" value="" maxlength="4" size="2"
                                onkeyup="checkvercode();" />
                            <img src="verifyimagepage.aspx" id="imgvercode" style="cursor: hand; height: 26px;
                                vertical-align: middle;" onclick="this.src='VerifyImagePage.aspx?time=' + Math.random()"
                                title="点击刷新验证码" alt="点击切换验证码" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td colspan="3">
                            您提供的联系方式我们保证不会泄露给第三方
                            <input id="buttonadd" class="buttonadd" type="button" name="regsubmit" value="提 交"
                                onclick="addsubmit();" />
                            <input id="txtfileurl" type="text" style="display: none;" value="" size="16" />
                            <input id="txtparamr" type="text" style="display: none;" value="<%=paramrStr %>"
                                size="16" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
    <div style="clear: both">
    </div>
    <div id="maintasklist" class="divlist" style="display: none;">
        <h3>
            <em id="Em1"></em>
        </h3>
        <div id="Div1">
            <table class="datatable" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tbody>
                    <tr class="colplural">
                        <td width="14%">
                            &nbsp;&nbsp;&nbsp;编号
                        </td>
                        <td style="text-align: left" width="14%">
                            类别
                        </td>
                        <td width="38%">
                            提交内容
                        </td>
                        <td width="18%">
                            进度
                        </td>
                        <td>
                            最新时间
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                        </td>
                        <td>
                        </td>
                        <td>
                            暂不提供查询...
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="delete_msg">
                            <%--  <a onclick="$('id2').checked=true;$('favlist').submit();" href="#"></a>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div id="mainquest" class="divlist" style="display: none;">
        <h3>
            <em id="Em2">&nbsp;&nbsp;问卷调查,有奖品(每用户仅可获取一次,重新登录可见),寻龙记感谢您的支持!</em>
        </h3>
        <div id="divQuest0">
            <table class="datatable" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tbody>
                    <tr class="colplural">
                        <th>
                            1.
                        </th>
                        <td>
                            您的性别？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio1">
                                A.男</label><input id="Radio1" type="radio" value="1" name="R1" />
                            <label for="Radio2">
                                B.女</label><input id="Radio2" type="radio" value="9" name="R1" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            2.
                        </th>
                        <td>
                            您的年龄？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio3">
                                A.15岁以下</label><input id="Radio3" type="radio" value="2" name="R2" />
                            <label for="Radio4">
                                B.16-23</label><input id="Radio4" type="radio" value="10" name="R2" />
                            <label for="Radio5">
                                C.23-30</label><input id="Radio5" type="radio" value="18" name="R2" />
                            <label for="Radio6">
                                D.30以上</label><input id="Radio6" type="radio" value="26" name="R2" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            3.
                        </th>
                        <td>
                            您是以什么途径得知《寻龙记》这款游戏的？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio7">
                                A.游戏网络媒体，如多玩，17173等</label><input id="Radio7" type="radio" value="3" name="R3" />
                            <label for="Radio8">
                                B.官网宣传</label><input id="Radio8" type="radio" value="11" name="R3" />
                            <label for="Radio9">
                                C.微博转发</label><input id="Radio9" type="radio" value="19" name="R3" />
                            <label for="Radio10">
                                D.朋友推荐</label><input id="Radio10" type="radio" value="27" name="R3" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            4.
                        </th>
                        <td>
                            你喜欢什么类型的网络游戏？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio11">
                                A.体育竞技类/音乐类（比如，街头篮球）</label><input id="Radio11" type="radio" value="4" name="R4" />
                            <label for="Radio12">
                                B.冒险/益智/育成类</label><input id="Radio12" type="radio" value="12" name="R4" />
                            <label for="Radio13">
                                C.战略类</label><input id="Radio13" type="radio" value="20" name="R4" />
                            <br />
                            <label for="Radio14">
                                D.大型多人角色扮演类</label><input id="Radio14" type="radio" value="28" name="R4" />
                            <label for="Radio15">
                                E.格斗/射击类</label><input id="Radio15" type="radio" value="36" name="R4" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            5.
                        </th>
                        <td>
                            在上网的时间里玩网络游戏的时间？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio16">
                                A.上网时间全部用来玩网络游戏</label><input id="Radio16" type="radio" value="5" name="R5" />
                            <label for="Radio17">
                                B.上网的大部分时间用来玩网络游戏</label><input id="Radio17" type="radio" value="13" name="R5" />
                            <label for="Radio18">
                                C.偶尔玩一下网络游戏</label><input id="Radio18" type="radio" value="21" name="R5" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="padding: 1px 0px 1px 16px; width: 100px; float: left;">
                <input id="button2" class="buttonadd" type="button" name="regsubmit" value="下一步"
                    onclick="setquest(1);" />
            </div>
            <div id="msgq0" style="margin-top: 4px; float: left">
                &nbsp;</div>
        </div>
        <div id="divQuest1" style="display: none;">
            <table class="datatable" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tbody>
                    <tr class="colplural">
                        <th>
                            6.
                        </th>
                        <td>
                            你已经玩了多长时间的网络游戏了？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio48">
                                A.半年以下</label><input id="Radio48" type="radio" value="6" name="R6" />
                            <label for="Radio49">
                                B.半年到一年</label><input id="Radio49" type="radio" value="14" name="R6" />
                            <label for="Radio50">
                                C.一年到两年</label><input id="Radio50" type="radio" value="22" name="R6" />
                            <label for="Radio51">
                                D.两年以上</label><input id="Radio51" type="radio" value="30" name="R6" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            7.
                        </th>
                        <td>
                            你一般在什么地方玩网络游戏？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio52">
                                A.家里</label>
                            <input id="Radio52" type="radio" value="7" name="R7" />
                            <label for="Radio53">
                                B.网吧</label><input id="Radio53" type="radio" value="15" name="R7" />
                            <label for="Radio54">
                                C.学校宿舍或其他地方</label>
                            <input id="Radio54" type="radio" value="23" name="R7" />
                        </td>
                    </tr>
                    <tr class="colplural">
                        <th>
                            8.
                        </th>
                        <td>
                            您每个月愿意在游戏方面花费多少人民币？
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="Radio55">
                                A.不愿意</label><input id="Radio55" type="radio" value="8" name="R8" />
                            <label for="Radio56">
                                B.一百以内</label><input id="Radio56" type="radio" value="16" name="R8" />
                            <label for="Radio57">
                                C.五百以内</label><input id="Radio57" type="radio" value="24" name="R8" />
                            <label for="Radio58">
                                D.三千以内</label><input id="Radio58" type="radio" value="32" name="R8" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="padding: 1px 0px 1px 16px; width: 200px; float: left;">
                <input id="button1" class="buttonadd" type="button" name="regsubmit" value="上一步"
                    onclick="setquest(0);" />
                <input id="buttonAddRequest" class="buttonadd" type="button" name="regsubmit" value="提 交"
                    onclick="setquest(99);" />
            </div>
            <div id="msgq1" style="margin-top: 4px; float: left">
                &nbsp;</div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div id="maincs" class="divlist" style="display: none;">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tbody>
                <tr>
                    <td width="28">
                        <img src="images/border_ChatHead_left.gif" width="28" height="26">
                    </td>
                    <td style="background-image: url(images/bg_ChatHead.gif); background-repeat: repeat-x">
                        您正与 <strong></SPAN><span id="ToUserID">在线客服</span><span></strong> 交谈</SPAN>
                    </td>
                    <td style="width: 4px">
                        <img src="images/border_ChatHead_right.gif" width="4" height="26">
                    </td>
                </tr>
            </tbody>
        </table>
        <iframe src="BlankPage.htm" id="ChatListBox" width="100%" height="210px"></iframe>
        <table style="background-image: url(images/IconBG.gif); background-repeat: repeat-x"
            border="0" cellspacing="0" cellpadding="0" width="100%">
            <tbody>
                <tr>
                    <td width="6">
                    </td>
                    <%--                    <td width="40" align="middle">
                        <a title="设置文本" href="#">
                            <img onclick="PowerTalk_showOrHide1(1),PowerTalk_showOrHide(0),PowerTalk_showOrHide2(0)"
                                border="0" alt="设置文本" src="images/Icon_txt.gif" width="16" height="16"></a>
                    </td>
                    <td width="6">
                        <img src="images/IconSpacer.gif" width="6" height="26">
                    </td>
                    <td width="40" align="middle">
                        <a title="增加表情" href="#">
                            <img onclick="PowerTalk_showOrHide(1),PowerTalk_showOrHide1(0),PowerTalk_showOrHide2(0)"
                                border="0" alt="增加表情" src="images/Icon_Face.gif" width="16" height="16"></a>
                    </td>
                    <td width="6">
                        <img alt="" src="images/IconSpacer.gif" width="6" height="26">
                    </td>
                    <td width="40" align="middle">
                        <a title="保存通话记录" href="#">
                            <img onclick="PowerTalk_CmdSave()" border="0" alt="保存通话记录" src="images/Icon_SaveData.gif"
                                width="16" height="16"></a>
                    </td>
                    <td width="6">
                        <img alt="" src="images/IconSpacer.gif" width="6" height="26">
                    </td>
                    <td width="40" align="middle">
                        <a title="清屏" href="#">
                            <img onclick="PowerTalk_Cls();" alt="清屏" src="images/cas.gif" width="16" height="16"></a>
                    </td>--%>
                    <td align="middle">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td width="100%">
                        <div id="PowerTools">
                            <div style="text-align: left; line-height: 18px; width: 100%; padding-right: 5px;
                                float: left; height: 25px; overflow: hidden" id="ADList">
                                <ul style="padding-bottom: 0px; list-style-type: none; margin: 3px 0px 0px; padding-left: 0px;
                                    padding-right: 0px; padding-top: 0px">
                                    <li style="height: 25px">
                                        <div style="text-align: left; width: 100%; height: 25px; overflow: hidden" class="slideFilm">
                                            在线咨询</div>
                                    </li>
                                    <li style="height: 25px">
                                        <div style="text-align: left; width: 100%; height: 25px; overflow: hidden" class="slideFilm">
                                            咨询聊天</div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <iframe src="BlankPage.htm" id="SendMsg" width="100%" height="90px"></iframe>
        <div style="padding: 8px 60px 8px 0px; width: 300px; float: right;">
        请及时查看回复,寻龙记感谢您的支持!&nbsp;&nbsp;&nbsp;&nbsp;
            <span style="display: none"><b>用户:</b> <span id="MyUserId">角色53821</span> 按 Ctrl+Enter
                发送 </span>
            <input id="button3" class="buttonadd" type="button" name="regsubmit" value="发 送"
                onclick="SendMessage()" />
        </div>
        <%--        <table class="datatable" border="0" cellspacing="0" cellpadding="0" width="100%"
            style="border-width: 1px; border-color: #a3c66e">
            <tbody>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="colplural">
                    <th>
                    </th>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>--%>
        <div id="Div4" style="margin-top: 14px; float: left">
            &nbsp;</div>
    </div>

    <script type="text/javascript">
        //在线咨询开始
        var IframeChatListBox;
        var IframeSendMsg;
        if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
            IframeChatListBox = document.getElementById("ChatListBox");
            IframeSendMsg = document.getElementById("SendMsg");
            IframeSendMsg.contentWindow.document.designMode = "On";
            document.getElementById("KeyInput").style.visibility = 'hidden';
        }
        else {
            IframeChatListBox = frames.document.frames("ChatListBox");
            IframeSendMsg = frames.document.frames("SendMsg");
            IframeSendMsg.document.designMode = "on";
            IframeSendMsg.document.onkeydown = new Function("return HotKeyPress(IframeSendMsg.event);");
        }
        $("MyUserId").innerHTML = $("txtparamr").value.split('|')[2] != undefined ? $("txtparamr").value.split('|')[2] : "";


        function HotKeyPress(event) {
            if (event.ctrlKey && event.keyCode == 13) {
                SendMessage();

            }
        }

        function chatinit() {
            if (ChatListBox.document.body.innerHTML.length == 0) {
                 ChatListBox.document.body.innerHTML = ('<font color=red>你好，《寻龙记》客服专员为您服务！</font><br/>');
            }
        }

        setInterval('chatinit()', 1000);
        function SendMessage() {
            var OA_TIME = new Date();
            var Today = new Date();
            var tyear = Today.getYear();
            var tmonth = Today.getMonth() + 1;
            var tday = Today.getDate();
            var thour = Today.getHours();
            var tmini = Today.getMinutes();
            var tsecond = Today.getSeconds();
            var ts = Today.getSeconds();
            var MyUserStr = $("MyUserId").innerHTML;
            var ToUserStr = $("ToUserID").innerHTML;

            var ContentStr = SendMsg.document.body.innerHTML;
            if (ContentStr.length == 0) {
                // alert(ContentStr);
                return;
            }
            ChatListBox.document.body.innerHTML += ('<font color=#0078c9>' + $("txtparamr").value.split("|")[2] + '</font> <font color=gray>' + tyear + '/' + tmonth + '/' + tday + ' ' + thour + ':' + tmini + ':' + tsecond + '</font><br/>' + ContentStr + '<br/>');
            // ChatListBox.document.body.innerHTML += ('<font color=gray>发送失败(在线咨询功能暂停)</font><br/>');
            SendMsg.document.body.innerHTML = "";

            if (window.navigator.userAgent.indexOf('Firefox') >= 1) {
                var content = IframeChatListBox.contentDocument.body;
            }
            else {
                var content = IframeChatListBox.document.body;
            }
            content.scrollTop = content.scrollHeight;

            //var reg = new RegExp("</P>", "g")
            // var regt = new RegExp("<P>", "g")
            //alert("" + ContentStr.replace(/<P>/ig, "").replace(/<\/P>/ig, "") + "");
            ajaxRead("ajax.aspx?t=msgadd&note=" + escape("" + ContentStr.replace(/<P>/ig, "").replace(/<\/P>/ig, "").replace(/</ig, "") + "") + "&r=" + escape($("txtparamr").value) + "", "MsgAddResult(obj,'" + escape("" + ContentStr.replace(/<P>/ig, "").replace(/<\/P>/ig, "").replace(/</ig, "") + "") + "');");
        }

        function MsgAddResult(myUrl, dataPrams) {
            var res = obj.getElementsByTagName('result');
            var result = "";
            if (res[0] != null && res[0] != undefined) {
                if (res[0].childNodes.length > 1) {
                    result = res[0].childNodes[1].nodeValue;
                } else {
                    result = res[0].firstChild.nodeValue;
                }
            }
            if (result == "0") {

                //   var tips = "<font color=#022739>您的消息已经收到,感谢您的支持!</font>";
            }
            else {
                msgtt(result);
                ChatListBox.document.body.innerHTML += ('<font color=gray>' + result + '</font><br/>');
            }
        }
        function MSGList() {
            if ($('menu4').className == 'cur_tab') {
                ajaxRead("ajax.aspx?t=msglist&roleid=" + escape($("txtparamr").value.split("|")[1]) + "", "MSGListResult(obj,'');");
            }
        }
        function MSGListResult(myUrl, dataPrams) {
            var res = obj.getElementsByTagName('result');
            var result = "";
            if (res[0] != null && res[0] != undefined) {
                if (res[0].childNodes.length > 1) {
                    result = res[0].childNodes[1].nodeValue;
                } else {
                    result = res[0].firstChild.nodeValue;
                }
            }
            if (result != "0") {
                if (window.navigator.userAgent.indexOf('Firefox') >= 1) {
                    var content = IframeChatListBox.contentDocument.body;
                }
                else {
                    var content = IframeChatListBox.document.body;
                }
                content.innerHTML += result;
                content.scrollTop = content.scrollHeight;
            }

        }

        //在线咨询结束


        function stop() {
            return false;
        }
        document.oncontextmenu = stop;

        $("locus_2").onchange = function(e) {

            $("tasktips").innerHTML = gettips($("locus_2").value);
        }

        $("locus_1").onchange = function(e) {
            var length = 0;
            for (var i in ttype2s) {
                if (ttype2s[i].pvalue == $("locus_1").value) {
                    $("locus_2").options[length] = new Option(ttype2s[i].tname + "  ", ttype2s[i].tvalue);
                    length++;
                }
            }
            $("locus_2").options.length = length;
            $("locus_2").onchange(e);
        }

        $("locus_0").onchange = function(e) {
            var length = 0;
            for (var i in ttype1s) {
                if (ttype1s[i].pvalue == $("locus_0").value) {
                    $("locus_1").options[length] = new Option(ttype1s[i].tname + "  ", ttype1s[i].tvalue);
                    length++;
                }
            }
            $("locus_1").options.length = length;
            $("locus_1").onchange(e);
        }

        function initstate() {
            $("locus_0").options.length = ttype0s.length + 1;
            $("locus_0").options[0] = new Option("----问题类型----", "-1");
            i = 1;
            for (var ttype0 in ttype0s) {
                $("locus_0").options[i] = new Option(ttype0s[i - 1].tname, ttype0s[i - 1].tvalue);
                i++;
            }
            $("locus_0").selectedIndex = 1;
            $("locus_0").onchange(null);
        }
        initstate();

        function menuclick(id) {

            $('maintaskadd').style.display = 'none';
            $('maintasklist').style.display = 'none';
            $('mainquest').style.display = 'none';
            $('maincs').style.display = 'none';
            $('menu0').className = '';
            $('menu1').className = '';
            $('menu2').className = '';
            $('menu3').className = '';
            $('menu4').className = '';
            switch (id) {
                case 0:
                    $('maintaskadd').style.display = '';
                    $('menu0').className = 'cur_tab';

                    $("locus_0").selectedIndex = 1;
                    $("locus_0").onchange(null);
                    break;
                case 1:
                    $('maintaskadd').style.display = '';
                    $('menu1').className = 'cur_tab';

                    $("locus_0").selectedIndex = 2;
                    $("locus_0").onchange(null);
                    break;
                case 2:
                    $('maintasklist').style.display = '';
                    $('menu2').className = 'cur_tab'; ;
                    break;
                case 3:
                    $('mainquest').style.display = '';
                    $('menu3').className = 'cur_tab'; ;
                    break;
                case 4:
                    $('maincs').style.display = '';
                    $('menu4').className = 'cur_tab'; ;
                    break;
                default:

            }
            $("returnregmessage").innerHTML = '&nbsp;';
            $('returnregmessage').className = '';

        }

        /*调查问卷开始*/
        function setquest(id) {

            switch (id) {
                case 0:
                    $('divQuest0').style.display = '';
                    $('divQuest1').style.display = 'none';
                    break;
                case 1:
                    if (getSelectedText('R1').length == 0) {
                        $("msgq0").innerHTML = "请选择 1. 您的性别？ ";
                        $('msgq0').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R2').length == 0) {
                        $("msgq0").innerHTML = "请选择 2. 您的年龄？ ";
                        $('msgq0').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R3').length == 0) {
                        $("msgq0").innerHTML = "请选择 3. 您是以什么途径得知《寻龙记》这款游戏的？ ";
                        $('msgq0').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R4').length == 0) {
                        $("msgq0").innerHTML = "请选择 4. 你喜欢什么类型的网络游戏？ ";
                        $('msgq0').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R5').length == 0) {
                        $("msgq0").innerHTML = "请选择 5. 在上网的时间里玩网络游戏的时间？ ";
                        $('msgq0').className = 'onerror';
                        break
                    }

                    $("msgq0").innerHTML = "";
                    $('msgq0').className = '';
                    $('divQuest0').style.display = 'none';
                    $('divQuest1').style.display = '';

                    break;
                case 99:
                    if (getSelectedText('R6').length == 0) {
                        $("msgq1").innerHTML = "请选择 6. 你已经玩了多长时间的网络游戏了？ ";
                        $('msgq1').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R7').length == 0) {
                        $("msgq1").innerHTML = "请选择 7. 你一般在什么地方玩网络游戏？ ";
                        $('msgq1').className = 'onerror';
                        break
                    }
                    if (getSelectedText('R8').length == 0) {
                        $("msgq1").innerHTML = "请选择 8. 您每个月愿意在游戏方面花费多少人民币？ ";
                        $('msgq1').className = 'onerror';
                        break
                    }

                    $("msgq1").innerHTML = "";
                    $('msgq1').className = '';

                    $("buttonAddRequest").disabled = true;
                    runstatic(1);

                    var strRequest = getSelectedText('R1') + '|' + getSelectedText('R2') + '|' + getSelectedText('R3') + '|' + getSelectedText('R4') + '|' + getSelectedText('R5') + '|' + getSelectedText('R6') + '|' + getSelectedText('R7') + '|' + getSelectedText('R8') + '';

                    ajaxRead("ajax.aspx?t=addanswer&r=" + escape($("txtparamr").value) + "&as=" + escape(strRequest) + " ", "showquestresult(obj,'" + escape('answer') + "');")

                    break
                default:

            }
        }


        function showquestresult(obj, username) {
            var res = obj.getElementsByTagName('result');
            var result = "";
            if (res[0] != null && res[0] != undefined) {
                if (res[0].childNodes.length > 1) {
                    result = res[0].childNodes[1].nodeValue;
                } else {
                    result = res[0].firstChild.nodeValue;
                }
            }
            runstatic(0);
            $("buttonAddRequest").disabled = false;
            if (result == "0") {

                var tips = "<font color=#022739>您的调查问卷已经收到,感谢您的支持!</font>";
                $('msgq1').innerHTML = tips;
                $('msgq1').className = 'onright';
                showDialog("<img border=\"0\" src=\"../images/access_allow.gif\"  /> 您的调查问卷已经收到,感谢您的支持!", 'notice', '提示信息', '', 1);

            }
            else {
                msgtt(result);
                $("msgq1").innerHTML = result;
                $('msgq1').className = 'onerror';
            }

        }


        function getSelectedText(name) {
            var obj = document.getElementsByName(name);
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].checked) {
                    return obj[i].value;
                }
            }
            return "";
        }

        /*调查问卷结束*/

        var profile_txtnote_toolong = '您的描述内容超过 1000 个字符!';
        var profile_txtnote_tooshort = '您输入的描述内容小于10个字符!';
        function checknote(tnote) {
            var unlen = tnote.replace(/[^\x00-\xff]/g, "**").length;
            if (unlen < 10 || unlen > 1000) {
                $("returnregmessage").innerHTML = (unlen < 10 ? profile_txtnote_tooshort : profile_txtnote_toolong);
                $('returnregmessage').className = 'onerror';
            }
            else {
                $("returnregmessage").innerHTML = '&nbsp;';
                $('returnregmessage').className = '';
            }

        }


        function checkqq(tnote) {
            var reg = "^[0-9]{4,13}$";
            if ($("textqq").value.match(reg) == null && $("textqq").value.length > 0) {
                $("returnregmessage").innerHTML = "QQ号码填写不正确!";
                $('returnregmessage').className = 'onerror';
            }
            else {
                $("returnregmessage").innerHTML = '&nbsp;';
                $('returnregmessage').className = '';
            }

        }
        function checkmobile(tnote) {
            var reg = "^(13|15|18)[0-9]{9}$";
            if ($("textmobile").value.match(reg) == null && $("textmobile").value.length > 0) {
                $("returnregmessage").innerHTML = "手机号码填写不正确!";
                $('returnregmessage').className = 'onerror';
            }
            else {
                $("returnregmessage").innerHTML = '&nbsp;';
                $('returnregmessage').className = '';
            }
        }
        function checkvercode() {
            if ($("textvercode").value.length == 0) {
                $("returnregmessage").innerHTML = "验证码不能为空,请输入验证码!";
                $('returnregmessage').className = 'onerror';
            }
            else if ($("textvercode").value.length != 4) {
                $("returnregmessage").innerHTML = "验证码应该输入4个字符,请检查!";
                $('returnregmessage').className = 'onerror';
            }
            else {
                $("returnregmessage").innerHTML = '&nbsp;';
                $('returnregmessage').className = '';
            }
        }
        function addsubmit() {
            checknote($("textnote").value);
            if ($('returnregmessage').className != 'onerror') {
                checkqq($("textqq").value);
            }
            if ($('returnregmessage').className != 'onerror') {
                checkmobile($("textmobile").value);
            }
            if ($('returnregmessage').className != 'onerror') {
                checkvercode();
            }

            if ($('returnregmessage').className != 'onerror') {
                $("buttonadd").disabled = true;
                runstatic(1);
                $('idBtnupload').onclick();

            }
            else {
                runstatic(2);
            }
        }
        function IntervalCheck() {
            var actid = document.activeElement.id;
            switch (actid) {
                case "textnote":
                    checknote($("textnote").value);
                    break;
                case "textqq":
                    checkqq($("textqq").value);
                    break;
                case "textmobile":
                    checkmobile($("textmobile").value);
                    break;
                case "textnote":
                    checknote($("textnote").value);
                    break;
                case "textvercode":
                    checkvercode();
                    break;
                default:
            }
        }


        function savetaska() {

            if ($('returnregmessage').className != 'onerror') {
                ajaxRead("ajax.aspx?t=addtask&note=" + escape("【问题类型：" + GetSelectText("locus_1") + " 具体问题：" + GetSelectText("locus_2") + "】") + escape($("textnote").value) + "&type=" + escape($("locus_2").value.split("|")[1]) + "&r=" + escape($("txtparamr").value) + "&f=" + escape($("txtfileurl").value) + "&qq=" + $("textqq").value + "&mobile=" + $("textmobile").value + "&vercode=" + $("textvercode").value, "showaddresult(obj,'" + escape('sss') + "');");

            }
            else { $("buttonadd").disabled = false; }
        }

        function msgt(value) {
            bar = 0;
            document.getElementById('Layer5').innerHTML = "<br /><img border=\"0\" src=\"../images/check_error.gif\"  />  " + value;
            document.getElementById('success').style.display = "block";
            count();
        }
        function msgtt(value) {
            bar = 0;
            $("returnregmessage").innerHTML = value;
            $('returnregmessage').className = 'onerror';
            document.getElementById('Layer5').innerHTML = "<br /><img border=\"0\" src=\"../images/check_error.gif\"  />  " + value;
            document.getElementById('success').style.display = "block";
            count();
        }

        function showaddresult(obj, username) {
            var res = obj.getElementsByTagName('result');
            var result = "";
            if (res[0] != null && res[0] != undefined) {
                if (res[0].childNodes.length > 1) {
                    result = res[0].childNodes[1].nodeValue;
                } else {
                    result = res[0].firstChild.nodeValue;
                }
            }
            runstatic(0);
            $("buttonadd").disabled = false;
            $("imgvercode").src = 'VerifyImagePage.aspx?time=' + Math.random()
            $("textvercode").value = "";
            if (result == "0") {

                var tips = "<font color=#022739>您的问题已经收到,感谢您的支持!</font>";
                $('returnregmessage').innerHTML = tips;
                $('returnregmessage').className = 'onright';
                //  $('succeedmessage').style.display = "";
                //  $('taskaddui').style.display = "none";
                $('textqq').value = "";
                $('textmobile').value = "";
                $('textnote').value = "";

                showDialog("<img border=\"0\" src=\"../images/access_allow.gif\"  /> 您的问题已经收到,感谢您的支持!", 'notice', '提示信息', '', 1);

            }
            else {

                //$('returnregmessage').className = 'onerror';
                //  $('returnregmessage').innerHTML = result;
                msgtt(result);
                // $('succeedmessage').style.display = "none";
                // $('taskaddui').style.display = "";
            }

        }

        var bar = 0;
        function runstatic(runtype) {
            if (runtype == 1) {
                document.getElementById('Layer5').innerHTML = '<BR /><table><tr><td valign=top><img border=\"0\" src=\"../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >正在处理数据, 请稍等...<BR /></td></tr></table><BR />';
                document.getElementById('Layer5').style.witdh = '350';
                document.getElementById('success').style.witdh = '400';
                document.getElementById('success').style.display = "block";
            }
            else if (runtype == 2) {
                document.getElementById('Layer5').innerHTML = "<br /><img border=\"0\" src=\"../images/check_error.gif\"  />  " + $('returnregmessage').innerHTML;
                document.getElementById('success').style.display = "block";
                count();
            }
            else {
                document.getElementById('Layer5').innerHTML = '<BR /><table><tr><td valign=top><img border=\"0\" src=\"../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >处理完成, 请稍等...<BR /></td></tr></table><BR />';
                document.getElementById('success').style.display = "block";
                count();
            }
        }


        function clearflag() {
            bar = 0;
            document.getElementById('Layer5').innerHTML = "<br />处理完成, 请稍等...";
            document.getElementById('success').style.display = "block";
            count();
        }

        function count() {
            bar = bar + 2;
            if (bar < 25) { setTimeout("count()", 100); }
            else {
                document.getElementById('success').style.display = "none"; bar = 0;
            }
        }

        function run() {
            bar = 0;
            document.getElementById('Layer5').innerHTML = "<br />正在处理数据, 请稍等...";
            document.getElementById('success').style.display = "block";
            setInterval('runstatic()', 2000); //每次提交时间为2秒
        }

        function GetSelectText(name) {
            var obj = $(name);
            for (i = 0; i < obj.length; i++)
                if (obj[i].selected == true) {
                return obj[i].innerText;
            }
        }
    </script>

    <%--文件上传--%>

    <script type="text/javascript">

        var isIE = (document.all) ? true : false;
        //        var $ = function(id) {
        //            return "string" == typeof id ? document.getElementById(id) : id;
        //        };

        var Class = {
            create: function() {
                return function() {
                    this.initialize.apply(this, arguments);
                }
            }
        }

        var Extend = function(destination, source) {
            for (var property in source) {
                destination[property] = source[property];
            }
        }

        var Bind = function(object, fun) {
            return function() {
                return fun.apply(object, arguments);
            }
        }

        var Each = function(list, fun) {
            for (var i = 0, len = list.length; i < len; i++) { fun(list[i], i); }
        };

        //文件上传类
        var FileUpload = Class.create();
        FileUpload.prototype = {
            //表单对象，文件控件存放空间
            initialize: function(form, folder, options) {

                this.Form = $(form); //表单
                this.Folder = $(folder); //文件控件存放空间
                this.Files = []; //文件集合

                this.SetOptions(options);

                this.FileName = this.options.FileName;
                this._FrameName = this.options.FrameName;
                this.Limit = this.options.Limit;
                this.Distinct = !!this.options.Distinct;
                this.ExtIn = this.options.ExtIn;
                this.ExtOut = this.options.ExtOut;

                this.onIniFile = this.options.onIniFile;
                this.onEmpty = this.options.onEmpty;
                this.onNotExtIn = this.options.onNotExtIn;
                this.onExtOut = this.options.onExtOut;
                this.onLimite = this.options.onLimite;
                this.onSame = this.options.onSame;
                this.onFail = this.options.onFail;
                this.onIni = this.options.onIni;

                if (!this._FrameName) {
                    //为每个实例创建不同的iframe
                    this._FrameName = "uploadFrame_" + Math.floor(Math.random() * 1000);
                    //ie不能修改iframe的name
                    var oFrame = isIE ? document.createElement("<iframe name=\"" + this._FrameName + "\">") : document.createElement("iframe");
                    //为ff设置name
                    oFrame.name = this._FrameName;
                    oFrame.style.display = "none";
                    //在ie文档未加载完用appendChild会报错
                    document.body.insertBefore(oFrame, document.body.childNodes[0]);
                }

                //设置form属性，关键是target要指向iframe
                this.Form.target = this._FrameName;
                this.Form.method = "post";
                //注意ie的form没有enctype属性，要用encoding
                this.Form.encoding = "multipart/form-data";

                //整理一次
                this.Ini();
            },
            //设置默认属性
            SetOptions: function(options) {
                this.options = {//默认值
                    FileName: "", //文件上传控件的name，配合后台使用
                    FrameName: "", //iframe的name，要自定义iframe的话这里设置name
                    onIniFile: function() { }, //整理文件时执行(其中参数是file对象)
                    onEmpty: function() { }, //文件空值时执行
                    Limit: 0, //文件数限制，0为不限制
                    onLimite: function() { }, //超过文件数限制时执行
                    Distinct: true, //是否不允许相同文件
                    onSame: function() { }, //有相同文件时执行
                    ExtIn: [], //允许后缀名
                    onNotExtIn: function() { }, //不是允许后缀名时执行
                    ExtOut: [], //禁止后缀名，当设置了ExtIn则ExtOut无效
                    onExtOut: function() { }, //是禁止后缀名时执行
                    onFail: function() { }, //文件不通过检测时执行(其中参数是file对象)
                    onIni: function() { } //重置时执行
                };
                Extend(this.options, options || {});
            },
            //整理空间
            Ini: function() {
                //整理文件集合
                this.Files = [];
                //整理文件空间，把有值的file放入文件集合
                Each(this.Folder.getElementsByTagName("input"), Bind(this, function(o) {
                    if (o.type == "file") { o.value && this.Files.push(o); this.onIniFile(o); }
                }))
                //插入一个新的file
                var file = document.createElement("input");
                file.name = this.FileName; file.type = "file"; file.onchange = Bind(this, function() { this.Check(file); this.Ini(); });
                this.Folder.appendChild(file);
                //执行附加程序
                this.onIni();
            },
            //检测file对象
            Check: function(file) {
                //检测变量
                var bCheck = true;
                //空值、文件数限制、后缀名、相同文件检测
                if (!file.value) {
                    bCheck = false; this.onEmpty();
                } else if (this.Limit && this.Files.length >= this.Limit) {
                    bCheck = false; this.onLimite();
                } else if (!!this.ExtIn.length && !RegExp("\.(" + this.ExtIn.join("|") + ")$", "i").test(file.value)) {
                    //检测是否允许后缀名
                    bCheck = false; this.onNotExtIn();
                } else if (!!this.ExtOut.length && RegExp("\.(" + this.ExtOut.join("|") + ")$", "i").test(file.value)) {
                    //检测是否禁止后缀名
                    bCheck = false; this.onExtOut();
                } else if (getfilesize(file.value) > 10000) {
                    //检测是否超大小
                    bCheck = false; this.onExtOut();
                } else if (!!this.Distinct) {
                    Each(this.Files, function(o) { if (o.value == file.value) { bCheck = false; } })
                    if (!bCheck) { this.onSame(); }
                }
                //没有通过检测
                !bCheck && this.onFail(file);
            },
            //删除指定file
            Delete: function(file) {
                //移除指定file
                this.Folder.removeChild(file); this.Ini();
            },
            //删除全部file
            Clear: function() {
                //清空文件空间
                Each(this.Files, Bind(this, function(o) { this.Folder.removeChild(o); })); this.Ini();
            }
        }

        var fu = new FileUpload("postpm", "idFile", { Limit: 1, ExtIn: ["jpg", "gif", "png", "bmp"],
            onIniFile: function(file) { file.value ? file.style.display = "none" : this.Folder.removeChild(file); },
            onEmpty: function() { msgt("请选择一个文件"); },
            onLimite: function() { msgt("只可以上传一个图片"); },
            onSame: function() { msgt("已经有相同文件"); },
            onNotExtIn: function() { msgt("只允许上传" + this.ExtIn.join("，") + "文件"); },
            onFail: function(file) { this.Folder.removeChild(file); },
            onIni: function() {
                //显示文件列表
                var arrRows = [];
                if (this.Files.length) {
                    var oThis = this;
                    Each(this.Files, function(o) {
                        var a = document.createElement("a"); a.innerHTML = "取消"; a.href = "javascript:void(0);";
                        a.onclick = function() { oThis.Delete(o); return false; };
                        arrRows.push([o.value, a]);
                    });
                } else { arrRows.push(["<font color='gray'>没有添加文件</font>", "&nbsp;"]); }
                AddList(arrRows);
                //设置按钮
                $("idBtnupload").disabled = $("idBtndel").disabled = this.Files.length <= 0;
            }
        });

        var _isChekUpOverTime = 0;
        function CheckUpOverTime() {
            if (_isChekUpOverTime == 1) {
                if ($(fu._FrameName).innerHTML.indexOf("script") < 0&&$("returnregmessage").innerHTML.indexOf("上传失败") < 0) {
                    fu.Ini();
                    $("idFile").style.display = "";
                    $("idProcess").style.display = "none";
                    msgtt("上传超时,可能图片太大或网速不给力!");
                    $("buttonadd").disabled = false;
                    $("imgvercode").src = 'VerifyImagePage.aspx?time=' + Math.random()
                    $("textvercode").value = "";
                }
            }
        }


        $("idBtnupload").onclick = function() {
            //显示文件列表
            var arrRows = [];
            Each(fu.Files, function(o) { arrRows.push([o.value, "&nbsp;"]); });
            AddList(arrRows);

            fu.Folder.style.display = "none";
            $("idProcess").style.display = "";
            $("idMsg").innerHTML = "正在添加文件到反馈系统中，请稍候……<br />有可能因为网络问题，出现程序长时间无响应，请点击“<a href='?'><font color='red'>取消</font></a>”重新上传文件";

            fu.Form.action = "File.ashx?v=" + $("textvercode").value;
            fu.Form.submit();
            _isChekUpOverTime = 1;
            setTimeout("CheckUpOverTime()", <%=overtime %>); //上传超时时间设置,毫秒
        }



        //用来添加文件列表的函数
        function AddList(rows) {
            //根据数组来添加列表
            var FileList = $("idFileList"), oFragment = document.createDocumentFragment();
            //用文档碎片保存列表
            Each(rows, function(cells) {
                var row = document.createElement("tr");
                Each(cells, function(o) {
                    var cell = document.createElement("td");
                    if (typeof o == "string") { cell.innerHTML = o; } else { cell.appendChild(o); }
                    row.appendChild(cell);
                });
                oFragment.appendChild(row);
            })
            //ie的table不支持innerHTML所以这样清空table
            while (FileList.hasChildNodes()) { FileList.removeChild(FileList.firstChild); }
            FileList.appendChild(oFragment);
        }

        $("idLimit").innerHTML = fu.Limit;

        $("idExt").innerHTML = fu.ExtIn.join("，");

        $("idBtndel").onclick = function() { fu.Clear(); }

        function getfilesize(filepath) {
            //            var image = new Image();
            //            image.src = 'file:///' + filepath;
            //            return image.filesize;
            return 0;
        }
        //在后台通过window.parent来访问主页面的函数
        function Finish(msg) {
            _isChekUpOverTime = 0;
            if (msg.substring(0, 4) == "上传失败" || msg.substring(0, 2) == "30") {
                msgtt(msg);
                $("txtfileurl").value = "";
                $("textvercode").value = "";

                fu.Ini();
                $("idFile").style.display = "";
                $("idProcess").style.display = "none";

                $("imgvercode").src = 'VerifyImagePage.aspx?time=' + Math.random()
                $("textvercode").value = "";
                
            }
            else {
                
                $("txtfileurl").value = msg;
                fu.Clear();
                fu.Ini();
                $("idFile").style.display = "";
                $("idProcess").style.display = "none";
            }
        }
    </script>

    <%--初始化--%>

    <script type="text/javascript">
        var browser = navigator.appName
        var b_version = navigator.appVersion
        var version = b_version.split(";");
        var trim_Version = version[1].replace(/[ ]/g, "");
        if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE6.0") {
            setInterval('IntervalCheck()', 200);
        }
        else if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") {
            setInterval('IntervalCheck()', 200);
        }

        var pIndex=<%=Request.QueryString["i"]==null?"0": Request.QueryString["i"]%>;
        if (pIndex==3) {
        menuclick(4);
        }
        setInterval('MSGList();', 2000);
        //  showDialog("<img border=\"0\" src=\"../images/access_allow.gif\"  /> 信息提交成功,感谢您的支持!", 'notice', '提示信息', '', 1);
    </script>

</body>
</html>
