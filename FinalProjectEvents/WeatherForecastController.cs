// WeatherForecastController.cs

using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;

public class WeatherForecastController : ApiController
{
    [HttpGet]
    [AllowAnonymous]
    [Route("api/WeatherForecast")]
    public IHttpActionResult GetWeatherForecast(string areaCode)
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

            

            return Ok(limit5WeatherList);
        }
    }
}
