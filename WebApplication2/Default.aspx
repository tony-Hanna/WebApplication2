<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Create new User</h2>
                <div class="form-group">
                    <asp:TextBox ID="Username" runat="server" CssClass="input-text" placeholder="Enter username"></asp:TextBox>
                    <asp:TextBox ID="Email" runat="server" CssClass="input-text" placeholder="Enter email"></asp:TextBox>
                    <asp:Button ID="addUser" runat="server" Text="Create User" OnClick="createUserButton" CssClass="btn btn-success" />
                </div>
                 <asp:Label ID="createErrorMessage" runat="server" CssClass="error-message"></asp:Label>
                <asp:Label ID="createSuccessMessage" runat="server" CssClass="success-message"></asp:Label>
             <h2>Users from API</h2>
            <asp:Label ID="NoUsersLabel" runat="server" CssClass="empty-message" Visible="false"
    Text="No users found. Create one above"></asp:Label>
                <asp:Repeater ID="RepeaterUsers" runat="server" OnItemCommand="RepeaterUsers_ItemCommand">
                    <ItemTemplate>
                        <div class="user-card">
                            <h3><%# Eval("Username") %></h3>
                            <p><b>ID:</b> <%# Eval("Id") %></p>
                            <p><b>Email:</b> <%# Eval("Email") %></p>

                            <asp:Button ID="btnDelete" runat="server" 
                                        CommandArgument='<%# Eval("Id") %>' 
                                        CommandName="DeleteUser"
                                        Text="Delete"
                                        CssClass="btn btn-danger" />
                            <asp:Button ID="btnEdit" runat="server" 
                                        CommandArgument='<%# Eval("Id") + "," + Eval("Username") %>'
                                        CommandName="EditUser"
                                        Text="Edit"
                                        CssClass="btn btn-primary" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <hr />
                 <asp:Panel ID="EditPanel" runat="server" Visible="false" CssClass="edit-form">
                 <h3>Edit User</h3>
                 <asp:TextBox ID="txtUserId" runat="server" ReadOnly="true" CssClass="input-text" /><br />
                 <asp:TextBox ID="txtUsername" runat="server" CssClass="input-text" /><br />
                 <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submit" CssClass="btn btn-success" />
                 <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancel" CssClass="btn btn-secondary" />
            </asp:Panel>
                <h2>Search user by ID</h2>
                <div class="form-group">
                    <asp:TextBox ID="TextUserId" runat="server" CssClass="input-text" placeholder="Enter user ID"></asp:TextBox>
                    <asp:Button ID="searchUser" runat="server" Text="Search" OnClick="searchButton" CssClass="btn btn-primary" />
                </div>

                <h2>Search result</h2>
                <asp:GridView ID="GridViewOneUser" runat="server" AutoGenerateColumns="true" CssClass="gridview"></asp:GridView>
                <asp:Label ID="errorMessage" runat="server" CssClass="error-message"></asp:Label>


                <asp:Label ID="repeaterErrorMessage" runat="server" CssClass="error-message"></asp:Label>
  
        </div>
    </form>
</body>
</html>
