using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProjectEvents
{
    public partial class Unauthorized : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to the default page after a delay
            Response.AddHeader("REFRESH", "3;URL=Default.aspx");
        }

    }
}