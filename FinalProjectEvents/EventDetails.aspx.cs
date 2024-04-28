using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FinalProjectEvents {
    public partial class EventDetails : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadEventDetails();
                LoadRelatedNews();
            }
        }

        private void LoadEventDetails() {
            string eventName = Request.QueryString["eventName"];
            if (!string.IsNullOrEmpty(eventName)) {
                eventName = Server.UrlDecode(eventName);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/Events.xml"));

                XmlNode eventNode = doc.SelectSingleNode("//Event[Name='" + eventName + "']");
                if (eventNode != null) {
                    lblName.Text = eventNode.SelectSingleNode("Name").InnerText;
                    lblDate.Text = eventNode.SelectSingleNode("Date").InnerText;
                    lblLocation.Text = eventNode.SelectSingleNode("Location").InnerText;
                }
                else {
                    lblMessage.Text = "Event not found.";
                }
            }
            else {
                lblMessage.Text = "No event specified.";
            }
        }


        private async void LoadRelatedNews() {
            string eventName = Request.QueryString["eventName"];
            if (!string.IsNullOrEmpty(eventName)) {
                eventName = Server.UrlDecode(eventName);
                var articles = await FetchNewsAsync(new string[] { eventName });
                UrlList.Items.Clear();
                foreach (var article in articles) {
                    ListItem item = new ListItem { Text = article, Value = article, Attributes = { ["onclick"] = $"window.open('{article}', '_blank');" } };
                    UrlList.Items.Add(item);
                }
            }
        }

        private async Task<List<string>> FetchNewsAsync(string[] topics) {
            var results = new List<string>();
            using (HttpClient client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("User-Agent", "ASPNET News Fetcher/1.0");

                foreach (string topic in topics) {
                    string apiUrl = $"https://newsapi.org/v2/everything?pageSize=10&q={topic}&apiKey=247752a4ffaf467b89bb293cb1a9ce36";

                    try {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode) {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(jsonResponse);
                            var articles = json["articles"];

                            foreach (var article in articles) {
                                string url = article["url"].ToString();
                                results.Add(url);
                            }
                        }
                    }
                    catch (Exception ex) {
                        // Handle exceptions appropriately
                        Console.WriteLine($"Exception in fetching news: {ex.Message}");
                        results.Add("Exception occurred");
                    }
                }
            }
            return results;
        }

    }
}