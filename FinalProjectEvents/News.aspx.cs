using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace FinalProjectEvents {
    public partial class News : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected async void BtnGetNews_Click(object sender, EventArgs e) {
            var topics = NewsFocusTextBox.Text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var articles = await FetchNewsAsync(topics);
            UrlList.Items.Clear();
            foreach (var article in articles) {
                ListItem item = new ListItem { Text = article, Value = article };
                item.Attributes["class"] = "news-link";
                item.Attributes["onclick"] = $"window.open('{article}', '_blank');";
                UrlList.Items.Add(item);
            }

        }
        private async Task<List<string>> FetchNewsAsync(string[] topics) {
            var results = new List<string>();
            using (HttpClient client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("User-Agent", "emnlc/1.0");

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
                        else {
                            // unsuccessful response
                            string responseBody = await response.Content.ReadAsStringAsync();
                            results.Add(responseBody);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"Exception: {ex.Message}");
                        results.Add("Exception occurred");
                    }
                }
            }
            return results;
        }
    }
}
