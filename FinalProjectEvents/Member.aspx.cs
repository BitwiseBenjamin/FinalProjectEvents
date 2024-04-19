using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace FinalProjectEvents
{
    public partial class Member : Page
    {
        public string Username { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve username from session or cookie
                if (HttpContext.Current.Request.Cookies["UserCredentials"] != null)
                {
                    // Retrieve username and password from cookie
                    String username = HttpContext.Current.Request.Cookies["UserCredentials"]["Username"];
                    Username = username;
                    DisplayUserInfo(username);
                }
                else
                {
                    // If username not found in session, redirect to login page
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void DisplayUserInfo(string username)
        {
            // Load XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/Users.xml"));

            // Search for user node with matching username
            XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{username}']");

            if (userNode != null)
            {
                // Retrieve user information
                string usernameValue = userNode.SelectSingleNode("Username").InnerText;
                string emailValue = userNode.SelectSingleNode("Email").InnerText;

                // Display user information
                lblUsername.Text = $"Username: {usernameValue}";
                lblEmail.Text = $"Email: {emailValue}";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text;
            string newEmail = txtNewEmail.Text;

            String oldUsername = HttpContext.Current.Request.Cookies["UserCredentials"]["Username"];

            // Update user information in XML document
            UpdateUserInfo(oldUsername, newUsername, newEmail);

            // Refresh user information display
            DisplayUserInfo(newUsername);

            // Show success message
            lblMessage.Visible = true;
            lblMessage.Text = Username;
        }


        private void UpdateUserInfo(string username, string newUsername, string newEmail)
        {
            try
            {
                // Load XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/Users.xml"));

                Console.WriteLine("username: ");
                Console.WriteLine(username);

                // Search for user node with matching username
                XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{username}']");

                if (userNode != null)
                {
                    // Update username and email
                    userNode.SelectSingleNode("Username").InnerText = newUsername;
                    userNode.SelectSingleNode("Email").InnerText = newEmail;

                    String password = userNode.SelectSingleNode("Password").InnerText;

                    // Save changes to XML document
                    xmlDoc.Save(Server.MapPath("~/App_Data/Users.xml"));



                    // Store username and password in a cookie
                    HttpCookie userCookie = new HttpCookie("UserCredentials");
                    userCookie["Username"] = username;
                    userCookie["Password"] = password;
                    // Set cookie expiration date (optional)
                    userCookie.Expires = DateTime.Now.AddDays(1); // Example: expires in 1 day
                    Response.Cookies.Add(userCookie);
                }
            }
            catch (Exception ex)
             {
                    // Log the exception
                    LogException(ex);
             }
        
         }

        private void LogException(Exception ex)
        {
            // Define the path to the log file
            string logFilePath = Server.MapPath("~/App_Data/ErrorLog.txt");

            // Write the exception details to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now}] An error occurred: {ex.Message}");
                writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                writer.WriteLine();
            }
        }
    }
}
