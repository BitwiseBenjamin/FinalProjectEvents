using System;
using System.Web;
using System.Xml;

public static class Util
{
    public static bool UserCookieIsValid()
    {
        // Check if user cookie exists
        if (HttpContext.Current.Request.Cookies["UserCredentials"] != null)
        {
            // Retrieve username and password from cookie
            string username = HttpContext.Current.Request.Cookies["UserCredentials"]["Username"];
            string password = HttpContext.Current.Request.Cookies["UserCredentials"]["Password"];

            // Check if user exists in XML document using the retrieved credentials
            if (UserExists(username, password))
            {
                // User cookie is valid
                return true;
            }
        }
        return false;
    }

    public static bool UserExists(string username, string password)
    {
        // Load XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/Users.xml"));

        // Search for user node with matching username and password
        XmlNode userNode = xmlDoc.SelectSingleNode($"//User[Username='{username}' and Password='{password}']");

        // Return true if user exists, false otherwise
        return userNode != null;
    }
}
