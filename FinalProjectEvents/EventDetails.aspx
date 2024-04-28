<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="FinalProjectEvents.EventDetails" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Details</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 20px;
            color: #333;
        }

        .event-details-container {
            background-color: #ffffff;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 20px;
            max-width: 600px;
            margin: 20px auto;
            box-shadow: 0 4px 8px rgba(0,0,0,0.05);
        }

        h1 {
            color: #2a2a2a;
            font-size: 36px;
        }

        .event-label {
            font-weight: bold;
            margin-top: 16px;
        }

        .event-info {
            margin-bottom: 12px;
        }

        .message {
            color: red;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <div class="event-details-container">
        <h1>Event Details</h1>
        <div class="event-info"><span class="event-label">Name:</span> <asp:Label ID="lblName" runat="server" /></div>
        <div class="event-info"><span class="event-label">Date:</span> <asp:Label ID="lblDate" runat="server" /></div>
        <div class="event-info"><span class="event-label">Location:</span> <asp:Label ID="lblLocation" runat="server" /></div>
        <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        <hr />
        <h2>News Links</h2>
        <div class="event-news-container">
            <form runat="server">
                <asp:BulletedList ID="UrlList" runat="server" DisplayMode="HyperLink" CssClass="news-links-list"></asp:BulletedList>
            </form>
        </div>

    </div>
</body>
</html>

