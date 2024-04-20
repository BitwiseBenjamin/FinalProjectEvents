using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;

namespace FinalProjectEvents {
    public partial class Events : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadEvents();
            }
        }

        protected void CreateEvent_Click(object sender, EventArgs e) {
            AddEvent(txtEventName.Text, txtEventDate.Text, txtEventLocation.Text);
            LoadEvents(); // reload grid 
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

            // Append elements to the event node
            newEventNode.AppendChild(nameNode);
            newEventNode.AppendChild(dateNode);
            newEventNode.AppendChild(locationNode);

            xmlDoc.DocumentElement.AppendChild(newEventNode);

            xmlDoc.Save(Server.MapPath("~/App_Data/Events.xml"));
        }

        private void LoadEvents() {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Events.xml"));

            var events = xmlDoc.DocumentElement.ChildNodes
                          .Cast<XmlNode>()
                          .Select(evt => new {
                              Name = evt["Name"]?.InnerText,
                              Date = evt["Date"]?.InnerText,
                              Location = evt["Location"]?.InnerText
                          });

            GridViewEvents.DataSource = events;
            GridViewEvents.DataBind();
        }

        protected void btnNewsPage_Click(object sender, EventArgs e) {
            Response.Redirect("News.aspx");
        }
    }
}
