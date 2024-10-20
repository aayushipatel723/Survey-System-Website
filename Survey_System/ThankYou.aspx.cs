using System;
using System.Web.UI;

namespace Survey_System
{
    public partial class ThankYou : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRedirectHome_Click(object sender, EventArgs e)
        {
            // Redirect to Home.aspx
            Response.Redirect("Home.aspx");
        }
    }
}
