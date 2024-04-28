<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalProjectEvents.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Events N Stuff - Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .top-bar {
            background-color: #f7f7f7;
            color: #000000;
            padding: 10px 0;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            z-index: 999;
            text-align: center;
            width: 100%;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Add box shadow */
        }
        .container {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            padding: 40px;
            width: 300px;
        }
        .container h1 {
            font-size: 24px;
            margin-bottom: 20px;
            text-align: center;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            display: block;
            font-size: 14px;
            margin-bottom: 5px;
        }
        .form-group input[type="text"],
        .form-group input[type="password"] {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 14px;
        }
        .btn-login
        {
            background-color: #0056b3;
            border: none;
            color: #fff;
            padding: 10px 20px;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            width: 100%;
            transition: background-color 0.3s ease;
        }
       
        .btn-signup {
            background-color: #fff;
            border: none;
            color: #0056b3;
            padding: 10px 20px;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            width: 100%;
            transition: background-color 0.3s ease;
        }
        .btn-login:hover,
        .btn-signup:hover {
            background-color: #808080;
        }
        .btn-signup {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <div class="top-bar">
        <h2>Events N Stuff</h2>
    </div>
    <form class="container" runat="server">
        <h1>Login</h1>
        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label>
        <div class="form-group">
            <label for="txtUsername">Username</label>
             <asp:TextBox ID="txtUsername" type="text" runat="server" />
        </div>
        <div class="form-group">

            <label for="txtPassword">Password </label>
            <asp:TextBox ID="txtPassword" type="password" runat="server"  />
        </div>
        <asp:Button ID="btnLogin" CssClass="btn-login" runat="server" Text="Login" OnClick="btnLogin_Click" />
        <asp:Button ID="btnSignupPage" CssClass="btn-signup" runat="server" Text="Signup" OnClick="btnSignupPage_Click" />
    </form>
</body>
</html>