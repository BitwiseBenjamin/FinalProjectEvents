<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="FinalProjectEvents.Member" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Your Information</h2>
            <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <!-- Display user information -->
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label><br />
            <!-- Update user information form -->
            <h2>Update Information</h2>
            <asp:TextBox ID="txtNewUsername" runat="server" placeholder="New Username"></asp:TextBox><br />
            <asp:TextBox ID="txtNewEmail" runat="server" placeholder="New Email"></asp:TextBox><br />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        </div>
    </form>
</body>
</html>

