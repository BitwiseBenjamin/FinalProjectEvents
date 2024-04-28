<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="FinalProjectEvents.services" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                    <asp:Button ID="btnReturnToDefault" runat="server" Text="Return to Default Page" OnClick="btnReturnToDefault_Click" />

        <div class="services">
            <h2>Service Directory</h2>
            <!-- Get Weather Forecast -->
            <h2>Get weather forecast</h2>
            <p>Zachary Rundstrom</p>
            <div>
                <input type="text" id="areaCode" runat="server" placeholder="Area Code" />
                <asp:Button type="button" runat="server" OnClick="GetWeatherForecast" Text="Get Weather Forecast" CssClass="custom-button" />
            </div>
            <div ID="weatherResult" runat="server"></div>
    
            <!-- GridView for displaying service directory -->
            <asp:GridView ID="gvServiceDirectory" runat="server">
            </asp:GridView>

            <h2>Dll encryption library</h2>
            <p>Zachary Rundstrom</p>
            <div>
                <input type="text" id="passToEncrypt" runat="server" placeholder="Password" />
                <asp:Button type="button" runat="server" OnClick="EncryptIt" Text="Encryption Time" CssClass="custom-button" />
            </div>
            <div ID="encryptedPassword" runat="server"></div>

            <h2>Get News</h2>
            <p>Emmanuel Cortes Castaneda</p>
            <div>
                <asp:Label ID="NewsFocusLabel" runat="server" Text="Enter any topic you'd like!"></asp:Label>
                <asp:TextBox ID="NewsFocusTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="BtnGetNews" runat="server" Text="Get News" OnClick="BtnGetNews_Click" />
            </div>
            <div>
                <asp:BulletedList ID="UrlList" runat="server"></asp:BulletedList>
            </div>
        </div>
    </form>
</body>
</html>
