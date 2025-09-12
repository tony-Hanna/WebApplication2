<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Users from API</h2>
            <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <hr />

            <h2>Search user by ID</h2>
            <asp:TextBox ID="TextUserId" runat="server" placeholder="Enter user ID"></asp:TextBox>
            <asp:Button ID="searchUser" runat="server" Text="Search" OnClick="searchButton"/>


            <h2>search result</h2>
            <asp:GridView ID="GridViewOneUser" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <asp:Label ID="errorMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
