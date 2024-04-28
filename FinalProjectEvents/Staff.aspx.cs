using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LibraryPasswordEncrypt;

namespace FinalProjectEvents
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies["UserCredentials"] != null)
            {
                // Retrieve username and password from cookie
                string username = HttpContext.Current.Request.Cookies["UserCredentials"]["Username"];

            // Check if the user is authenticated and authorized to access this page
            if (!IsUserInStaff(username))
                {
                    Response.Redirect("Unauthorized.aspx"); // Redirect to login page if not authenticated or authorized
                }
            }

            if (!IsPostBack)
            {
                // Load staff information and display it on the page
                LoadStaffInformation();
            }
        }
        // Check if user exists in the Staff.xml file
        private bool IsUserInStaff(string username)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Staff.xml"));
            XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{username}']");
            return userNode != null;
        }

        // Load staff information from XML
        private void LoadStaffInformation()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Staff.xml"));

            DataTable dtStaff = new DataTable();
            dtStaff.Columns.Add("Username");
            dtStaff.Columns.Add("Password");

            XmlNodeList userNodes = xmlDoc.SelectNodes("//User");
            foreach (XmlNode userNode in userNodes)
            {
                string username = userNode.SelectSingleNode("Username").InnerText;
                string password = userNode.SelectSingleNode("Password").InnerText;
                try
                {
                    var decryptedPassword = PasswordEncryptor.Decrypt(password);
                    dtStaff.Rows.Add(username, decryptedPassword);
                }
                catch
                {
                    dtStaff.Rows.Add(username, password);
                }
            }

            gvStaff.DataSource = dtStaff;
            gvStaff.DataBind();
        }

        // Save changes made to staff information
        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvStaff.Rows)
            {
                string oldUsername = ((TextBox)row.FindControl("txtUsername")).Attributes["data-oldusername"];
                string username = ((TextBox)row.FindControl("txtUsername")).Text;
                string newPassword = ((TextBox)row.FindControl("txtPassword")).Text;

                UpdateUserInfo(oldUsername, username, newPassword);
            }

            lblMessage.Text = "Changes saved successfully!";
        }

        protected void btnReturnToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }



        // Update user information in XML
        private void UpdateUserInfo(string oldUsername, string username, string newPassword)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Staff.xml"));

            XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{oldUsername}']");

            if (userNode != null)
            {
                var encryptedPass = PasswordEncryptor.Encrypt(newPassword);
                userNode.SelectSingleNode("Password").InnerText = encryptedPass;
                userNode.SelectSingleNode("Username").InnerText = username;

                xmlDoc.Save(Server.MapPath("~/App_Data/Staff.xml"));
            }
        }

        protected void Add(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Staff.xml"));

            string username = txtNewUsername.Text;
            string password = txtNewPassword.Text;   

            // Create a new user node
            XmlNode newUserNode = xmlDoc.CreateElement("User");

            // Create username and password elements
            XmlNode usernameNode = xmlDoc.CreateElement("Username");
            XmlNode passwordNode = xmlDoc.CreateElement("Password");

            // Set inner text of username and password elements
            usernameNode.InnerText = username;
            var encryptedPass = PasswordEncryptor.Encrypt(password);
            passwordNode.InnerText = encryptedPass;

            // Append username and password elements to the user node
            newUserNode.AppendChild(usernameNode);
            newUserNode.AppendChild(passwordNode);

            // Append the new user node to the XML document
            xmlDoc.DocumentElement.AppendChild(newUserNode);

            // Save the changes to the XML file
            xmlDoc.Save(Server.MapPath("~/App_Data/Staff.xml"));
            LoadStaffInformation();
        }



    }
}