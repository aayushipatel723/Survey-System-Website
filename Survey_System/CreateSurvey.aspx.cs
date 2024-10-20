using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Survey_System
{
    public partial class CreateSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No need to handle anything in Page_Load for creating a survey.
        }

        protected void CreateSurveyButton_Click(object sender, EventArgs e)
        {
            string surveyName = FormName.Text;

            // Ensure the user is logged in and has a valid session
            if (Session["UserID"] != null)
            {
                string userId = Session["UserID"].ToString();  // Get the logged-in user's ID
                CreateSurveyInDatabase(surveyName, userId);    // Pass userId to the method
            }
            else
            {
                // Handle cases where the user is not logged in, maybe redirect to login page
                Response.Redirect("Login.aspx");
            }
        }

        private void CreateSurveyInDatabase(string surveyName, string userId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SurveyConString"]?.ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO SurveyForms (FormName, CreatedBy) OUTPUT INSERTED.FormID VALUES (@FormName, @CreatedBy)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FormName", surveyName);
                    cmd.Parameters.AddWithValue("@CreatedBy", userId);

                    // Capture the generated FormID
                    int formId = (int)cmd.ExecuteScalar();

                    // Redirect to Add Questions page with FormID
                    Response.Redirect($"AddQuestions.aspx?FormID={formId}");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

    }
}
