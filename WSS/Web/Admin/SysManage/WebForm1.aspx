<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WSS.Web.Admin.SysManage.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:WebServeDBConnectionString %>" 
            DeleteCommand="DELETE FROM [T_Department] WHERE [F_DepartID] = @F_DepartID" 
            InsertCommand="INSERT INTO [T_Department] ([F_ParentID], [F_DepartName], [F_Note]) VALUES (@F_ParentID, @F_DepartName, @F_Note)" 
            SelectCommand="SELECT [F_DepartID], [F_ParentID], [F_DepartName], [F_Note] FROM [T_Department]" 
            
            
            
            
            UpdateCommand="UPDATE [T_Department] SET [F_ParentID] = @F_ParentID, [F_DepartName] = @F_DepartName, [F_Note] = @F_Note WHERE [F_DepartID] = @F_DepartID">
            <DeleteParameters>
                <asp:Parameter Name="F_DepartID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="F_ParentID" Type="Int32" />
                <asp:Parameter Name="F_DepartName" Type="String" />
                <asp:Parameter Name="F_Note" Type="String" />
                <asp:Parameter Name="F_DepartID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="F_ParentID" Type="Int32" />
                <asp:Parameter Name="F_DepartName" Type="String" />
                <asp:Parameter Name="F_Note" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
