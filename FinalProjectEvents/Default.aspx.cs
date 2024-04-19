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
                // Display member page content
                // Code to display member-specific content

            }

        }

        [WebMethod]
        public static object GetWeatherForecast(string areaCode)
        {
            string apiKey = "87356ca0fe937eabf1471207df97cf1d";
            string geoApiUrl = $"http://api.openweathermap.org/geo/1.0/zip?zip={areaCode}&appid={apiKey}";

            using (var client = new WebClient())
            {
                string geoContent = client.DownloadString(geoApiUrl);

                dynamic geoData = JsonConvert.DeserializeObject(geoContent);

                double lat = geoData.lat;
                double lon = geoData.lon;

                string weatherApiUrl = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}";
                string weatherContent = client.DownloadString(weatherApiUrl);

                dynamic weatherData = JsonConvert.DeserializeObject(weatherContent);
                var weatherList = weatherData.list;

                var limit5WeatherList = new List<dynamic>();
                for (int i = 0; i < 5 && i < weatherList.Count; i++)
                {
                    limit5WeatherList.Add(weatherList[i]);
                }

                return limit5WeatherList;
            }
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
    }
}