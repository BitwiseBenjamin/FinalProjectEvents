<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FinalProjectEvents._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Management System</title>
   
   <script>
       function getWeatherForecast() {
           var areaCode = document.getElementById("areaCode").value;
           let resp = PageMethods.GetWeatherForecast(areaCode, onSuccess, onFailure);
           displayWeatherForecast(resp);
       }

       function onSuccess(data) { 
           data = [
               {"dt_txt": "2024-04-17 12:00:00",
                   "main": {
                       "temp": 25,
                       "humidity": 60
                   },
                   "weather": [
                       {
                           "description": "Clear sky"
                       }
                   ]
               },
               {
                   "dt_txt": "2024-04-18 12:00:00",
                   "main": {
                       "temp": 28,
                       "humidity": 55
                   },
                   "weather": [
                       {
                           "description": "Partly cloudy"
                       }
                   ]
               },
               {
                   "dt_txt": "2024-04-19 12:00:00",
                   "main": {
                       "temp": 20,
                       "humidity": 70
                   },
                   "weather": [
                       {
                           "description": "Rainy"
                       }
                   ]
               }
           ]

           displayWeatherForecast(data);
       }

       function onFailure(error) {
           console.log("Failed to get weather forecast: " + error.get_message());
       }

       function displayWeatherForecast(data) {
           var weatherResult = document.getElementById("weatherResult");
           weatherResult.innerHTML = ""; // Clear previous content

           if (data && data.length > 0) {
               var forecastContainer = document.createElement("div");
               forecastContainer.classList.add("forecast-container");

               data.forEach(function (forecast) {
                   var forecastItem = document.createElement("div");
                   forecastItem.classList.add("forecast-item");

                   var date = new Date(forecast.dt_txt);

                   var forecastDate = document.createElement("p");
                   forecastDate.textContent = "Date: " + date.toDateString();

                   var forecastTemp = document.createElement("p");
                   forecastTemp.textContent = "Temperature: " + forecast.main.temp + "°C";

                   var forecastDescription = document.createElement("p");
                   forecastDescription.textContent = "Weather: " + forecast.weather[0].description;

                   forecastItem.appendChild(forecastDate);
                   forecastItem.appendChild(forecastTemp);
                   forecastItem.appendChild(forecastDescription);

                   forecastContainer.appendChild(forecastItem);
               });

               weatherResult.appendChild(forecastContainer);
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
            <asp:Button ID="btnEventsPage" runat="server" Text="Events Page" OnClick="btnEventsPage_Click" />
            
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
