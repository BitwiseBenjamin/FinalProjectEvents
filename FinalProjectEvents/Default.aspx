<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FinalProjectEvents._Default" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        /* Header styles */
        .header {
            background-color: #f7f7f7;
            color: #000000;
            padding:10px 0;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            z-index: 999;
            text-align: center;
            width: 100%;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Add box shadow */
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        /* Logo styles */
        .logo {
            font-size: 24px;
            margin-left: 20px; /* Adjusted margin to left */
        }

        /* Menu styles */
        .menu ul {
            list-style: none;
            padding: 0;
            margin: 0;
            display: flex;
        }

        .menu ul li {
            margin: 0 5px; /* Add spacing between buttons */
        }

        /* Button styles */
        .custom-button {
            padding: 10px 10px;
            background-color: white; /* Green background */
            color: black;
            border: none;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 5px;
            transition-duration: 0.4s;
        }

        .custom-button:hover {
            background-color: #45a049; /* Darker green on hover */
        }

        .content {
            padding-top: 70px; /* Adjust as needed based on the header height */
            width: 100vw;
        }

        /* Events grid styles */
        .events {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr)); /* Adjust grid column width as needed */
            gap: 20px; /* Add gap between grid items */
        }

        .event-box {
            background-color: #a8efc6; /* Light gray background */
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Add box shadow */
        }

        .event-grid {
            display: grid;
            grid-template-columns: repeat(5, 1fr);
            grid-gap: 20px;
        }


        .event-box h3 {
            margin-top: 0; /* Remove default margin */
        }
        .weather {
             background-color: #7bbef1; /* Light gray background */
             padding: 20px;
             border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h1 class="logo">Events N Stuff</h1>
            <div class="menu">
                <ul>
                    <li><asp:Button ID="Button1" runat="server" Text="Profile" OnClick="btnMemberPage_Click" CssClass="custom-button" /></li>
                    <li><asp:Button ID="Button2" runat="server" Text="Admin" OnClick="btnStaffPage_Click" CssClass="custom-button" /></li>
                    <li><asp:Button ID="Button3" runat="server" Text="Manage Events" OnClick="btnEventsPage_Click" CssClass="custom-button" /></li>
                    <li><asp:Button ID="Button4" runat="server" Text="Test Services" OnClick="btnReturnToDefault_Click" CssClass="custom-button" /></li>
                </ul>
            </div>
        </div>

        

        <div class="content">
            <h2>Today Weather</h2>
            <div ID="weatherResult" runat="server"></div>
            <h2>All Events</h2>
            <div id="eventGrid" runat="server" class="event-grid"></div>

        </div>
    </form>
</body>
</html>
