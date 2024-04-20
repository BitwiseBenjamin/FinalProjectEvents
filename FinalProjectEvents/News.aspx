<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="FinalProjectEvents.News" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>News</title>
    <style>
        .news-link {
            color: blue;
            cursor: pointer;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>News Page</h1>
        <p>Emmanuel Cortes Castaneda</p>
        <div>
            <asp:Label ID="NewsFocusLabel" runat="server" Text="Enter any topic you'd like!"></asp:Label>
            <asp:TextBox ID="NewsFocusTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="BtnGetNews" runat="server" Text="Get News" OnClick="BtnGetNews_Click" />
        </div>
        <div>
            <asp:BulletedList ID="UrlList" runat="server"></asp:BulletedList>
        </div>
    </form>
</body>
</html>
