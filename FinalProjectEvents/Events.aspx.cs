using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;

namespace FinalProjectEvents {
    public partial class Events : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                RetrieveUserEvents();
            }
        }

        private void RetrieveUserEvents() {
            string username = getUsername();

            if (username != "null") {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/Events.xml")); // Assuming events.xml is in the same directory

                XmlNodeList eventList = doc.SelectNodes("//Event[Host='" + username + "']");

                string htmlContent = "";

                if (eventList.Count > 0) {
                    foreach (XmlNode eventNode in eventList) {
                        string name = eventNode.SelectSingleNode("Name").InnerText;
                        string date = eventNode.SelectSingleNode("Date").InnerText;
                        string location = eventNode.SelectSingleNode("Location").InnerText;

                        htmlContent += "<div class='event'>";
                        htmlContent += "<div><strong>Name:</strong> " + name + "</div>";
                        htmlContent += "<div><strong>Date:</strong> " + date + "</div>";
                        htmlContent += "<div><strong>Location:</strong> " + location + "</div>";
                        htmlContent += "</div>";


                    }
                }
                else {
                    htmlContent = "You have not created any events.";
                }

                // Set the content to the Literal control
                litEvents.Text = htmlContent;
            }
        }


        private string getUsername() {
            if (Request.Cookies["UserCredentials"] != null) {
                string credentials = Request.Cookies["UserCredentials"].Value;

                string[] pairs = credentials.Split('&');
                string username = null;

                foreach (string pair in pairs) {
                    if (pair.StartsWith("Username=")) {
                        username = pair.Substring("Username=".Length);
                        break;
                    }
                }

                if (username != null) {
                    return username;

                }
                else {
                    Response.Write("Username not found.");
                    return "null";
                }
            }
            else {
                Response.Write("Cookie not found.");
                return "";
            }
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

           protected void btnGoToCreateEvent_Click(object sender, EventArgs e) {
            Response.Redirect("CreateEvent.aspx");
        }
    }
}
