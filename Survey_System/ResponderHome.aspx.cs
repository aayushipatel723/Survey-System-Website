using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class ResponderHome : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSurveys();
            }
        }

        private void LoadSurveys()
        {
            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT FormID, FormName FROM SurveyForms", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SurveysGridView.DataSource = reader;
                SurveysGridView.DataBind();
            }
        }

        protected void RespondButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string formId = button.CommandArgument;
            Response.Redirect($"SubmitResponse.aspx?FormID={formId}");
        }
    }
}
