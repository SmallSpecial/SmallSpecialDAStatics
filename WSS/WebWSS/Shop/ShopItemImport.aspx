<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopItemImport.aspx.cs"
    Inherits="WebWSS.Shop.ShopItemImport" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>

    <script language='JavaScript' src='../img/Admin.Js'></script>

    <script type="text/javascript" src='../img/GetDate.Js'></script>

    <script type="text/javascript" src="../img/ProgressBar.js"></script>

    <script language="javascript" type="text/javascript">
        functionset(value) { //用来设置进度条的长度   
            var obj, Mywidth;
            obj = document.getElementById("processbar");
            Mywidth = obj.style.width; MywidthMywidth = Mywidth.replace("px", ""); Mywidth = parseInt(Mywidth); Mywidth++; Mywidth++; Mywidth++;
            obj.style.width = Mywidth + "px";
        }   
    </script>

    <script type="text/javascript">
            var ps2 = new ProgressBar();
            var times = 0;
            var timer = null;

            function startps() {
                ps2.StepManually = true;
                ps2.TickCount = 10;
                ps2.CompleteEvent = hide;
                ps2.show();
                timer = setInterval(getstep, 1000);
            }
            function getstep(){
            <%= ClientScript.GetCallbackEventReference(this, null, "stepping",null) %>;
            }
            
            function stepping(result) {
            
                     if(result!="")
                   {
                     var current=result.split(",")[0];
                     var count=result.split(",")[1];
               
                             times++;
                             times=current;
                            ps2.TipMessage = "数据正在导入中...，当前" + times + "条/共" + count + "条";
                            ps2.step(times);
                   }
                   else
                   {
                            times++;
                            ps2.TipMessage = "qqqq数据正在导入中...，当前" + times + "条/共" + ps2.TickCount + "条";
                            ps2.step(times);
                   }

           }

            function hide() {
                ps2.hide();
                clearInterval(timer);
                timer = null;
                times = 0;
            }

            function hide2() {
                ps2.hide();
            }
            function test2() {
                ps2.StepManually = false;
                ps2.AliveSeconds = 8;
                ps2.CompleteEvent = hide2;
                ps2.TipMessage = "数据正在导入中...";
                ps2.show();
            }
            function PublishData(){
                if(confirm('<asp:Literal runat="server" Text="<%$Resources:Language,CDK_SurePublishDatas%>" />'))
                {
                startps();
                return  true;
                }
                else
                {
                return false;
                }
            }
    </script>

    <style>
        .statusTable
        {
            width: 100px;
            border: solid 1px #000000;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }
        .statusTable td
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <asp:LinkButton ID="LinkButton0" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopItemList.aspx">商城商品</asp:LinkButton>
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopItemImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
            | &nbsp;
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopBaseItemList.aspx">基础道具</asp:LinkButton>
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="True" Font-Size="13px"
                PostBackUrl="~/Shop/ShopBaseItemImport.aspx" Text="<%$Resources:Language,CDK_BatchImport %>"></asp:LinkButton>
        </div>
        <div class="gridv" id="divMain" runat="Server">
            1.<asp:Label runat="server" Text="<%$Resources:Language,CDK_ImportFile %>"></asp:Label>
            <table class="Text" cellspacing="1" cellpadding="0" width="95%" bgcolor="#1d82d0"
                border="0">
                <tr bgcolor="#ffffff">
                    <td valign="top">
                        <table class="Text" cellspacing="1" cellpadding="0" width="100%" border="0">
                            <tr height="30">
                                <td style="width: 120px" width="120">
                                    &nbsp;<font face="宋体">
                                                <asp:Label runat="server" Text="<%$Resources:Language,CDK_PleaseWaitImportFile %>"></asp:Label>
                                          </font>
                                </td>
                                <td align="left" width="370">
                                    <input id="FileExcel" style="width: 330px" type="file" size="42" name="FilePhoto"
                                        runat="server"><font color="red"></font>
                                </td>
                                <td class="hint">
                                    <asp:Button ID="BtnImport" Text="<%$Resources:Language,CDK_Import %>" CssClass="buttonbl" runat="server" OnClick="BtnImport_Click">
                                    </asp:Button>
                                    &nbsp;<asp:Label runat="server" Text="<%$Resources:Language,CDK_FileFormatIs %>"></asp:Label>
                                    <font color="red">商城商品</font>
                                    <asp:Label runat="server" Text="<%$Resources:Language,CDK_ImportExcel %>"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            2.<asp:Label runat="server" Text="<%$Resources:Language,CDK_PublishToService %>"></asp:Label>
            <br />
            <table border="0" cellspacing="0" cellpadding="0" width="95%" style="border: 1px solid #1d82d0;">
                <tr valign="bottom">
                    <td width="497px">
                        <div id="divZoneList" runat="Server" style="margin: 8px 3px 8px 3px;">
                        </div>
                        <asp:Label ID="LblMessagePub" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbReset" runat="server" Text="<%$Resources:Language,CDK_IsResetOriginData %>" Font-Bold="True" /><br />
                        <asp:Button ID="btnPublish" runat="server" Text="<%$Resources:Language,CDK_Publish %>" CssClass="buttonbl" OnClientClick='return confirm("确认要发布数据这些服务器吗？");'
                            Style="margin: 8px 0px 8px 0px;" OnClick="btnPublish_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:DropDownList ID="ddlShopType" runat="server" Width="80px" Visible="false">
                <asp:ListItem Value="-1"  Text="<%$Resources:Language,LblAllType %>"></asp:ListItem>
                <asp:ListItem Value="0">元宝</asp:ListItem>
                <asp:ListItem Value="1">绑定元宝</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlItemType" runat="server" Width="80px" Visible="false">
                <asp:ListItem Value="-1"  Text="<%$Resources:Language,LblAllType %>"></asp:ListItem>
                <asp:ListItem Value="0">宝石</asp:ListItem>
                <asp:ListItem Value="1">法宝</asp:ListItem>
                <asp:ListItem Value="2">坐骑</asp:ListItem>
                <asp:ListItem Value="3">时装</asp:ListItem>
                <asp:ListItem Value="4">奇珍</asp:ListItem>
                <asp:ListItem Value="5">药水</asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server" Font-Size="12px" Width="95%" CellPadding="3"
                BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False"
                PageSize="5">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Language,CDK_DataStatue %>">
                        <ItemTemplate>
                            <%#Eval("F_State").ToString().Length > 0 ? "<font color='red'>" + Eval("F_State") + "</font>" : "准备"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_EXCELID" HeaderText="EXCEL编号" />
                    <asp:TemplateField HeaderText="商城类型">
                        <ItemTemplate>
                            <%#GetTypeName(ddlShopType, Eval("F_SHOPTYPE").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品类型">
                        <ItemTemplate>
                            <%#GetTypeName(ddlItemType, Eval("F_ITEMTYPE").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_ITEMNUMBER" HeaderText="商品数量" />
                    <asp:BoundField DataField="F_OLDPRICE" HeaderText="原价" />
                    <asp:BoundField DataField="F_NEWPRICE" HeaderText="现价" />
                    <asp:BoundField DataField="F_VIPRICE" HeaderText="VIP价" />
                    <asp:BoundField DataField="F_POSITION" HeaderText="商品位置" />
                    <asp:TemplateField HeaderText="是否新品">
                        <ItemTemplate>
                            <%#Eval("F_ISNEW").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_NEWPOS" HeaderText="新品位置" />
                    <asp:TemplateField HeaderText="是否热卖">
                        <ItemTemplate>
                            <%#Eval("F_ISHOTSALE").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_HOTPOS" HeaderText="热卖位置" />
                    <asp:TemplateField HeaderText="是否促销">
                        <ItemTemplate>
                            <%#Eval("F_ISPROMOTIONS").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_PROMOTIONPOS" HeaderText="促销位置" />
                    <asp:TemplateField HeaderText="包含礼品">
                        <ItemTemplate>
                            <%#Eval("F_USEGIFTS").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否限时">
                        <ItemTemplate>
                            <%#Eval("F_ISTIMESALE").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_LimitTimePos" HeaderText="限时位置" />
                    <asp:BoundField DataField="F_TimeStart" HeaderText="开始时间" />
                    <asp:BoundField DataField="F_TimeEnd" HeaderText="<asp:Literal runat="server" Text="<%$ Resources:Language,LblEndTime %>"></asp:Literal>:" />
                    <asp:TemplateField HeaderText="是否隐藏">
                        <ItemTemplate>
                            <%#Eval("F_ISHIDENITEM").ToString() == "1" ? "<font color='red'>是</font>" : "否"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_GiftID_0" HeaderText="礼品1编号" />
                    <asp:BoundField DataField="F_GiftNUM_0" HeaderText="礼品1数量" />
                    <asp:BoundField DataField="F_GiftID_1" HeaderText="礼品2编号" />
                    <asp:BoundField DataField="F_GiftNUM_1" HeaderText="礼品2数量" />
                    <asp:BoundField DataField="F_GiftID_2" HeaderText="礼品3编号" />
                    <asp:BoundField DataField="F_GiftNUM_2" HeaderText="礼品3数量" />
                    <asp:BoundField DataField="F_GiftID_3" HeaderText="礼品4编号" />
                    <asp:BoundField DataField="F_GiftNUM_3" HeaderText="礼品4数量" />
                    <asp:BoundField DataField="F_GiftID_4" HeaderText="礼品5编号" />
                    <asp:BoundField DataField="F_GiftNUM_4" HeaderText="礼品5数量" />
                    <asp:BoundField DataField="F_ItemInfo" HeaderText="商品说明" />
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#D1DDF1" />
            </asp:GridView>
        </div>
        <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        <br />
        <b><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label>:</b>
       <asp:Label runat="server" Text="<%$Resources:Language,CDK_TipImportExcelCSV %>"></asp:Label>
        <br />
        <span style="display: none"><asp:Label runat="server" Text="<%$Resources:Language,Tip_Sign %>"></asp:Label><asp:Label ID="lblinfo" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
    </div>
    </form>
</body>
</html>
