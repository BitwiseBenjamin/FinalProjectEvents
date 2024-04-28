using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LibraryPasswordEncrypt;

namespace FinalProjectEvents
{
    public partial class services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void GetWeatherForecast(object sender, EventArgs e)
        {
            try
            {
                string areacode = areaCode.Value;
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
                    dynamic[] limit5WeatherArray = new dynamic[5];
                    for (int i = 0; i < 5 && i < weatherArray.Length; i++)
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

        protected void EncryptIt(object sender, EventArgs e)
        {
            string password = passToEncrypt.Value;
            encryptedPassword.InnerText = PasswordEncryptor.Encrypt(password);
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

                forecastHtml += $"<div>Date: {date}, Description: {description}, Temp: {temp}, Humidity: {humidity}</div>";
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


        protected async void BtnGetNews_Click(object sender, EventArgs e)
        {
            var topics = NewsFocusTextBox.Text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var articles = await FetchNewsAsync(topics);
            UrlList.Items.Clear();
            foreach (var article in articles)
            {
                ListItem item = new ListItem { Text = article, Value = article };
                item.Attributes["class"] = "news-link";
                item.Attributes["onclick"] = $"window.open('{article}', '_blank');";
                UrlList.Items.Add(item);
            }

        }
        private async Task<List<string>> FetchNewsAsync(string[] topics)
        {
            var results = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "emnlc/1.0");

                foreach (string topic in topics)
                {
                    string apiUrl = $"https://newsapi.org/v2/everything?pageSize=10&q={topic}&apiKey=247752a4ffaf467b89bb293cb1a9ce36";

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(jsonResponse);
                            var articles = json["articles"];

                            foreach (var article in articles)
                            {
                                string url = article["url"].ToString();
                                results.Add(url);
                            }
                        }
                        else
                        {
                            // unsuccessful response
                            string responseBody = await response.Content.ReadAsStringAsync();
                            results.Add(responseBody);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                        results.Add("Exception occurred");
                    }
                }
            }
            return results;
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}