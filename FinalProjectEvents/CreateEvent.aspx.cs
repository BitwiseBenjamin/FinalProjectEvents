using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FinalProjectEvents {
    public partial class CreateEvent : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void CreateEvent_Click(object sender, EventArgs e) {
            AddEvent(txtEventName.Text, txtEventDate.Text, txtEventLocation.Text);

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


        private void AddEvent(string name, string date, string location) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Events.xml"));

            // new event node
            XmlNode newEventNode = xmlDoc.CreateElement("Event");

            XmlNode nameNode = xmlDoc.CreateElement("Name");
            nameNode.InnerText = name;
            XmlNode dateNode = xmlDoc.CreateElement("Date");
            dateNode.InnerText = date;
            XmlNode locationNode = xmlDoc.CreateElement("Location");
            locationNode.InnerText = location;

            XmlNode hostNode = xmlDoc.CreateElement("Host");
            string host = getUsername();
            hostNode.InnerText = host;

            // Append elements to the event node
            newEventNode.AppendChild(nameNode);
            newEventNode.AppendChild(dateNode);
            newEventNode.AppendChild(locationNode);
            newEventNode.AppendChild(hostNode);

            xmlDoc.DocumentElement.AppendChild(newEventNode);

            xmlDoc.Save(Server.MapPath("~/App_Data/Events.xml"));
        }
    }
}