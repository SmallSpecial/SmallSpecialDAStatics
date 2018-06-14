<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WSS.Web.Admin.GameTool.WebForm2" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
    var checkBlank = function(grid) {
        if (check.gridValid(grid, ['SizeName'])) {
            return true;
        }
        return false;
    };
    var check = {
        gridValid: function(grid, fields) {
            var rs = grid.store.modified || [];
            for (var i = 0; i < rs.length; i++) {
                for (var j = 0; j < fields.length; j++) {
                    if (Ext.isEmpty(rs[i].data[fields[j]])) {
                        grid.startEditing(this.getRowIndex(grid, rs[i]), this.getColIndex(grid, fields[j]));
                        return false;
                    }
                }
            }
            return true;
        },
        getRowIndex: function(grid, record) {
            return grid.store.indexOf(record);
        },
        getColIndex: function(grid, dataIndex) {
            return grid.getColumnModel().findColumnIndex(dataIndex);
        }
    };
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None" />
    <ext:Viewport Layout="Fit" runat="server">
    <Body>
        <ext:GridPanel
        ID="gpSize"
        runat="server"
        Title="Grid验证示例"
        TrackMouseOver="true"
        StripeRows="true">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Disk">
                            <Listeners>
                                <Click Handler="if(checkBlank(#{gpSize})){#{gpSize}.save();}" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column DataIndex="SizeSn" Header="组号" Width="100">
                        <Editor>
                            <ext:TextField runat="server" MaxLength="10" Regex="///d{4}/" />
                        </Editor>
                    </ext:Column>
                    <ext:Column DataIndex="SizeName" Header="尺码" Width="200">
                        <Editor>
                            <ext:TextField runat="server" MaxLength="10" AllowBlank="false" />
                        </Editor>
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" />
            </SelectionModel>
        </ext:GridPanel>
    </Body>
</ext:Viewport>
    </div>
    </form>
</body>
</html>


