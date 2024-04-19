using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FinalProjectEvents
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear previous login message
                lblMessage.Visible = false;

                if (Util.UserCookieIsValid())
                {
                    // User cookie is valid, redirect to member page or dashboard
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnSignupPage_Click(object sender, EventArgs e)
        {
            // Handle member page button click
            Response.Redirect("Signup.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if user exists in XML document
            // Check if user exists in XML document
            if (Util.UserExists(username, password))
            {
                // Store username and password in a cookie
                HttpCookie userCookie = new HttpCookie("UserCredentials");
                userCookie["Username"] = username;
                userCookie["Password"] = password;
                // Set cookie expiration date (optional)
                userCookie.Expires = DateTime.Now.AddDays(1); // Example: expires in 1 day
                Response.Cookies.Add(userCookie);

                // Redirect user to home page or dashboard
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Display error message if login fails
                lblMessage.Visible = true;
                lblMessage.Text = "Invalid username or password. Please try again.";
            }
        }

        private bool UserExists(string username, string password)
        {
            // Load XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Users.xml"));

            // Search for user node with matching username and password
            XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{username}' and Password='{password}']");

            // Return true if user exists, false otherwise
            return userNode != null;
        }

        public bool UserCookieIsValid()
        {
            // Check if user cookie exists
            if (Request.Cookies["UserCredentials"] != null)
            {
                // Retrieve username and password from cookie
                string username = Request.Cookies["UserCredentials"]["Username"];
                string password = Request.Cookies["UserCredentials"]["Password"];

                // Check if user exists in XML document using the retrieved credentials
                if (UserExists(username, password))
                {
                    // User cookie is valid
                    return true;
                }
            }
            return false;
        }

      

        

     

    }
}