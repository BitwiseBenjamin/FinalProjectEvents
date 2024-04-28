<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateEvent.aspx.cs" Inherits="FinalProjectEvents.CreateEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Event</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        form {
            max-width: 400px;
            margin: 50px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #333;
        }

        label {
            display: block;
            margin-bottom: 5px;
            color: #555;
        }

        input[type="text"],
        input[type="date"],
        .button {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 3px;
            box-sizing: border-box;
        }

        .button {
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
        }

        .button:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Create new event</h2>
        <div>
            <asp:Label ID="lblEventName" runat="server" AssociatedControlID="txtEventName" Text="Event Name:" />
            <asp:TextBox ID="txtEventName" runat="server" /><br />
    
            <asp:Label ID="lblEventDate" runat="server" AssociatedControlID="txtEventDate" Text="Event Date:" />
            <asp:TextBox ID="txtEventDate" runat="server" TextMode="Date" /><br />
    
            <asp:Label ID="lblEventLocation" runat="server" AssociatedControlID="txtEventLocation" Text="Location:" />
            <asp:TextBox ID="txtEventLocation" runat="server" /><br />
    
            <asp:Button ID="btnAddEvent" CssClass="button" runat="server" Text="Add Event" OnClick="CreateEvent_Click" />
        </div>
    </form>
</body>
</html>
