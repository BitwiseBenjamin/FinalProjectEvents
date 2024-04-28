<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="FinalProjectEvents.Staff" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Staff Page</h1>
            <asp:Button ID="btnReturnToDefault" runat="server" Text="Return to Default Page" OnClick="btnReturnToDefault_Click" />

            <!-- Display staff information -->
            <asp:GridView ID="gvStaff" runat="server" AutoGenerateColumns="False" DataKeyNames="Username">
                <Columns>
                    <asp:TemplateField HeaderText="Username">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("Username") %>' ClientIDMode="Static" data-oldusername='<%# Eval("Username") %>' data-oldpassword='<%# Eval("Password") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("Password") %>' ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>

            <!-- Input fields for adding a new staff member -->
            <h2>Add New Staff Member</h2>
            <asp:TextBox ID="txtNewUsername" runat="server" placeholder="Enter Username"></asp:TextBox>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            <asp:Button ID="btnAddStaff" runat="server" Text="Add Staff Member" OnClick="Add" />
            <br />
           
        </div>
    </form>
</body>
</html>
