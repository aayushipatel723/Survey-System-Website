using System;
using System.Web.UI;

namespace Survey_System
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserEmail"] != null)
                {
                    string userType = Session["UserType"] as string;
                    navLogout.Visible = true;

                    if (userType == "Surveyer")
                    {
                        navCreateSurvey.Visible = true;
                        navViewForms.Visible = true;
                    }
                    else if (userType == "Respondent")
                    {
                        navSurveys.Visible = true;
                    }
                }
                else
                {
                    navLogout.Visible = false;
                }
            }
        }
    }
}
