﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="FinalProjectEvents.Events" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Events</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Events Page</h1>
        <p>Emmanuel Cortes Castaneda</p>
        <asp:Button ID="btnNewsPage" runat="server" Text="News Page" OnClick="btnNewsPage_Click" />
        <h2>Create new event</h2>
        <div>
            <asp:Label ID="lblEventName" runat="server" AssociatedControlID="txtEventName" Text="Event Name:" />
            <asp:TextBox ID="txtEventName" runat="server" /><br />
            
            <asp:Label ID="lblEventDate" runat="server" AssociatedControlID="txtEventDate" Text="Event Date:" />
            <asp:TextBox ID="txtEventDate" runat="server" TextMode="Date" /><br />
            
            <asp:Label ID="lblEventLocation" runat="server" AssociatedControlID="txtEventLocation" Text="Location:" />
            <asp:TextBox ID="txtEventLocation" runat="server" /><br />
            
            <asp:Button ID="btnAddEvent" runat="server" Text="Add Event" OnClick="CreateEvent_Click" />
        </div>
        <br />
        <h2>Events List</h2>
        <asp:GridView ID="GridViewEvents" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
