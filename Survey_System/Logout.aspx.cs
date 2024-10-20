using System;
using System.Web;

namespace Survey_System
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();

            // Abandon the session
            Session.Abandon();

            // Optionally, set the response cookie to expire
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var cookie = new HttpCookie("ASP.NET_SessionId");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Redirect to the home page or login page
            Response.Redirect("Home.aspx");
        }
    }
}
