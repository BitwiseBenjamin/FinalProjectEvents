<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FinalProjectEvents._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Management System</title>
    <script src="Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        function getWeatherForecast() {
            var areaCode = document.getElementById("areaCode").value;

            $.ajax({
                type: "POST",
                url: "Default.aspx/GetWeatherForecast",
                data: JSON.stringify({ areaCode: areaCode }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    displayWeatherForecast(response.d);
                },
                error: function (xhr, status, error) {
                    console.error("Failed to get weather forecast: " + error);
                }
            });
        }


        function onSuccess(data) {
            displayWeatherForecast(data);
        }

        function onFailure(error) {
            console.log("Failed to get weather forecast: " + error.get_message());
        }

        function displayWeatherForecast(data) {
            var weatherResult = document.getElementById("weatherResult");
            weatherResult.innerHTML = ""; // Clear previous content

            console.log(data)

            if (data && data.length > 0) {
                var forecastList = document.createElement("ul");

                for (var i = 0; i < data.length; i++) {
                    var forecast = data[i];
                    var listItem = document.createElement("li");
                    var date = new Date(forecast.dt * 1000); // Convert Unix timestamp to milliseconds
                    listItem.textContent = "Date: " + date.toDateString() + ", Temp: " + forecast.main.temp + "°K, Weather: " + forecast.weather[0].description;
                    forecastList.appendChild(listItem);
                }

                weatherResult.appendChild(forecastList);
            } else {
                weatherResult.textContent = "No weather forecast available.";
            }
        }

</script>

</head>
<body>
    <form id="form1" runat="server">
        

        <div>
            <h1>Welcome to the Event Management System</h1>
            <!-- Explanation of functionality -->
            <p>Here you can manage your events and activities. Sign up now to get started!</p>

            <!-- Buttons to access Member and Staff pages -->
            <asp:Button ID="btnMemberPage" runat="server" Text="Member Page" OnClick="btnMemberPage_Click" />
            <asp:Button ID="btnStaffPage" runat="server" Text="Staff Page" OnClick="btnStaffPage_Click" />
            
            <!-- Service directory -->
            <h2>Service Directory</h2>
            <!-- Get Weather Forecast -->
            <div>
                <input type="text" id="areaCode" placeholder="Area Code" />
                
                <button type="button" onclick="getWeatherForecast()">Get Weather Forecast: zach</button>
            </div>
            <div id="weatherResult"></div>
            
            <!-- GridView for displaying service directory -->
            <asp:GridView ID="gvServiceDirectory" runat="server">
            </asp:GridView>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    </form>
</body>
</html>
