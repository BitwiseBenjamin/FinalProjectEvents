<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="FinalProjectEvents.Events" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Events</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 20px;
            padding: 20px;
        }

        #container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .event {
            background-color: #fafafa;
            border: 1px solid #ddd;
            padding: 10px;
            border-radius: 8px;
        }

        .event strong {
            color: #333;
            font-size: 16px;
        }

        .event div {
            margin-bottom: 5px;
        }
    </style>

    
    <script>
        function deleteEvent(eventId) {
            __doPostBack('DeleteEvent', eventId);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Manage your Events </h1>
        <asp:Button ID="btnGoToCreateEvent" runat="server" Text="Create Event" OnClick="btnGoToCreateEvent_Click" />
        <asp:Button ID="btnReturnToDefault" runat="server" Text="Return to Default Page" OnClick="btnReturnToDefault_Click" />

         <h2>All your events event</h2>
         <div id="container">
            <asp:Literal ID="litEvents" runat="server"></asp:Literal>
         </div>
        
    </form>

</body>
</html>
