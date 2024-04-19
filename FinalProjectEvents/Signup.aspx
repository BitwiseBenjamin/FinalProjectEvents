<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="FinalProjectEvents.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Signup</h1>
            <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            <asp:Button ID="btnLoginPage" runat="server" Text="Login Page" OnClick="btnLoginPage_Click" />
        </div>
    </form>
</body>
</html>

