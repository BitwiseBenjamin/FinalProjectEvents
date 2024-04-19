using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FinalProjectEvents
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;

            // Check if user already exists in XML document
            if (!Util.UserExists(username, password))
            {
                // Add user to XML document
                AddUser(username, password, email);

                // Display success message or redirect to login page
                lblMessage.Visible = true;
                lblMessage.Text = "User created successfully. Please log in with your new credentials.";
            }
            else
            {
                // Display error message if user already exists
                lblMessage.Visible = true;
                lblMessage.Text = "User already exists. Please choose a different username.";
            }
        }

        protected void btnLoginPage_Click(object sender, EventArgs e)
        {
            // Handle member page button click
            Response.Redirect("Login.aspx");
        }

        private void AddUser(string username, string password, string email)
        {
            // Load XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Users.xml"));

            // Create a new user node
            XmlNode newUserNode = xmlDoc.CreateElement("User");

            // Create username and password elements
            XmlNode usernameNode = xmlDoc.CreateElement("Username");
            usernameNode.InnerText = username;
            XmlNode passwordNode = xmlDoc.CreateElement("Password");
            passwordNode.InnerText = password;
            XmlNode emailNode = xmlDoc.CreateElement("Email");
            emailNode.InnerText = email;

            // Append username and password elements to user node
            newUserNode.AppendChild(usernameNode);
            newUserNode.AppendChild(passwordNode);
            newUserNode.AppendChild(emailNode);

            // Append user node to the root element of the XML document
            xmlDoc.DocumentElement.AppendChild(newUserNode);

            // Save the XML document
            xmlDoc.Save(Server.MapPath("~/App_Data/Users.xml"));
        }
    }
}