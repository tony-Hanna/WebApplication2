 <%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Users from API</h2>
            <asp:Repeater ID="RepeaterUsers" runat="server" OnItemCommand="RepeaterUsers_ItemCommand">
                <ItemTemplate>
                    <div style="border:1px solid #ccc; border-radius:8px; padding:12px; margin:8px; width:200px; display:inline-block;">
                        <h3><%# Eval("Username") %></h3>
                        <h3><%# Eval("Id") %></h3>
                        <p><b>Email:</b> <%# Eval("Email") %></p>
                     

                        <asp:Button ID="btnDelete" runat="server" 
                                    CommandArgument='<%# Eval("Id") %>' 
                                    CommandName="DeleteUser"
                                    Text="Delete"/>
                        <asp:Button ID="btnEdit" runat="server" 
                                    CommandArgument='<%# Eval("Id") + "," + Eval("Username") %>'
                                    CommandName="EditUser"
                                    Text="Edit"/>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <hr />

            <h2>Search user by ID</h2>
            <asp:TextBox ID="TextUserId" runat="server" placeholder="Enter user ID"></asp:TextBox>
            <asp:Button ID="searchUser" runat="server" Text="Search" OnClick="searchButton"/>


            <h2>search result</h2>
            <asp:GridView ID="GridViewOneUser" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <asp:Label ID="errorMessage" runat="server" ForeColor="Red"></asp:Label>

            <h2>Create new User</h2>
            <asp:TextBox ID="Username" runat="server" placeholder="Enter username"></asp:TextBox>
            <asp:TextBox ID="Email" runat="server" placeholder="Enter email"></asp:TextBox>
            <asp:Button ID="addUser" runat="server" Text="Create User" OnClick="createUserButton"/>
            <asp:Label ID="createErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="createSuccessMessage" runat="server" ForeColor="Green"></asp:Label>
            <asp:Label ID="repeaterErrorMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:Panel ID="EditPanel" runat="server" Visible="false" CssClass="edit-form">
                <asp:TextBox ID="txtUserId" runat="server" ReadOnly="true" /><br />
                <asp:TextBox ID="txtUsername" runat="server" /><br />
                <asp:Button ID="submitButton" runat="server" text="submit" OnClick="submit"/>
                <asp:Button ID="cancelButton" runat="server" text="cancel" OnClick="cancel"/>
           </asp:Panel>
        </div>
    </form>
</body>
</html>
