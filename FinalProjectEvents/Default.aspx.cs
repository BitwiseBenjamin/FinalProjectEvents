using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Profile;
using System.Web.Services.Description;
using System.Web.Http;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Web.UI.HtmlControls;



namespace FinalProjectEvents
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Util.UserCookieIsValid())
            {
                // Redirect user to login page if not authenticated
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadEvents();
                    GetWeatherForecast();
                }

            }

        }

        private void LoadEvents() {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/App_Data/Events.xml"));  // Adjust path as necessary
            XmlNodeList eventNodes = doc.SelectNodes("/Events/Event");

            foreach (XmlNode eventNode in eventNodes) {
                var eventBox = new HtmlGenericControl("div");
                eventBox.Attributes["class"] = "event-box";

                // Event Name as a hyperlink
                var eventName = new HtmlAnchor();
                string name = eventNode.SelectSingleNode("Name").InnerText;
                eventName.HRef = $"EventDetails.aspx?eventName={HttpUtility.UrlEncode(name)}";
                eventName.InnerText = name;
                eventBox.Controls.Add(eventName);

                // Event Date
                var eventDate = new HtmlGenericControl("p");
                string date = eventNode.SelectSingleNode("Date").InnerText;
                eventDate.InnerText = "Date: " + date;
                eventBox.Controls.Add(eventDate);

                // Event Location
                var eventLocation = new HtmlGenericControl("p");
                string location = eventNode.SelectSingleNode("Location").InnerText;
                eventLocation.InnerText = "Location: " + location;
                eventBox.Controls.Add(eventLocation);

                // Add the div box to a placeholder or panel on your ASPX page
                eventGrid.Controls.Add(eventBox); // Assume eventGrid is your PlaceHolder or Panel
            }


        }


        protected void GetWeatherForecast()
        {
            try
            {
                string areacode = "85281";
                /* string apiUrl = "http://localhost:51987/Service1.svc/weatherforecast?areaCode=" + areaCode;

                 HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                 request.Method = "GET";
                 request.ContentType = "application/json; charset=utf-8";

                 using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                 using (Stream responseStream = response.GetResponseStream())
                 using (StreamReader reader = new StreamReader(responseStream))
                 {
                     string json = reader.ReadToEnd();
                     dynamic data = JsonConvert.DeserializeObject(json);
                     onSuccess(data);
                 }*/

                // Call the OpenWeatherMap API to get latitude and longitude using area code
                string apiKey = "87356ca0fe937eabf1471207df97cf1d";
                string geoApiUrl = $"http://api.openweathermap.org/geo/1.0/zip?zip={areacode}&appid={apiKey}";

                using (var client = new HttpClient())
                {
                    HttpResponseMessage geoResponse = client.GetAsync(geoApiUrl).Result;
                    string geoContent = geoResponse.Content.ReadAsStringAsync().Result;

                    dynamic geoData = JsonConvert.DeserializeObject(geoContent);

                    // Extract latitude and longitude
                    double lat = geoData.lat;
                    double lon = geoData.lon;

                    // Call the OpenWeatherMap API to get weather forecast data
                    string weatherApiUrl = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}";
                    HttpResponseMessage weatherResponse = client.GetAsync(weatherApiUrl).Result;
                    string weatherContent = weatherResponse.Content.ReadAsStringAsync().Result;

                    dynamic weatherData = JsonConvert.DeserializeObject(weatherContent);
                    JArray weatherList1 = weatherData.list;
                    dynamic[] weatherList = weatherList1.ToObject<dynamic[]>();

                    // Dictionary to store weather forecast data for each day
                    Dictionary<string, dynamic> dailyWeather = new Dictionary<string, dynamic>();

                    foreach (var forecast in weatherList)
                    {
                        string date = forecast.dt_txt.ToString().Split(' ')[0];

                        if (!dailyWeather.ContainsKey(date))
                        {
                            dailyWeather[date] = forecast;
                        }
                    }

                    // Convert the dictionary values to an array
                    var weatherArray = dailyWeather.Values.ToArray();
                    // Limit the weather array to the first 5 elements
                    dynamic[] limit5WeatherArray = new dynamic[1];
                    for (int i = 0; i < 1 && i < weatherArray.Length; i++)
                    {
                        limit5WeatherArray[i] = weatherArray[i];
                    }


                    onSuccess(limit5WeatherArray);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();
                        onFailure(error);
                    }
                }
                else
                {
                    onFailure(ex.Message);
                }
            }
        }

 
        protected void onSuccess(dynamic[] data)
        {
            // Process the successful response data
            // For example, update the UI with the received weather forecast data
            // Assuming you have HTML elements with IDs to display the data
            string forecastHtml = "";
            foreach (var forecast in data)
            {
                string date = forecast.dt_txt;
                string description = forecast.weather[0].description;
                string temp = forecast.main.temp;
                string humidity = forecast.main.humidity;

                forecastHtml += $"<div class=\"weather\"> {description} {((float.Parse(temp) - 273.15) * 9 / 5 + 32).ToString("F1")}F {humidity}% humidity </div>";
            }

            // Update the HTML element with ID "weatherForecast" with the forecastHtml
            weatherResult.InnerHtml = forecastHtml;
        }

        protected void onFailure(string error)
        {
            // Process the error response
            // For example, display an error message to the user
            // Assuming you have an HTML element with ID to display error messages
            weatherResult.InnerText = $"Failed to get weather forecast: {error}";
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("services.aspx");
        }

        protected void btnMemberPage_Click(object sender, EventArgs e)
        {
            // Handle member page button click
            Response.Redirect("Member.aspx");
        }

        protected void btnStaffPage_Click(object sender, EventArgs e)
        {
            // Handle staff page button click
            Response.Redirect("Staff.aspx");
        }

        protected void btnEventsPage_Click(object sender, EventArgs e) {
            Response.Redirect("Events.aspx");
        }
    }
}