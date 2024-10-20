using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class ViewSurveys : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    string userId = Session["UserID"].ToString();
                    LoadSurveys(userId);
                }
                else
                {
                    // Redirect to login if no user session exists
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx"); // Redirect to your homepage
        }
        private void LoadSurveys(string userId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT FormID, FormName, CreatedDate FROM SurveyForms WHERE CreatedBy = @CreatedBy";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CreatedBy", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        NoFormsLabel.Visible = false;
                    }
                    else
                    {
                        GridView1.Visible = false;
                        NoFormsLabel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadSurveys(Session["UserID"].ToString());  // Re-load data on page change
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewResponses")
            {
                int formId = Convert.ToInt32(e.CommandArgument);
                // Redirect to the ViewResponses page and pass the FormID as a query parameter
                Response.Redirect($"ViewResponses.aspx?FormID={formId}");
            }
            else if (e.CommandName == "DeleteForm")
            {
                int formId = Convert.ToInt32(e.CommandArgument);
                DeleteForm(formId);
                LoadSurveys(Session["UserID"].ToString());  // Refresh the GridView after deletion
            }
        }

        private void DeleteForm(int formId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Delete QuestionResponses first
                    string deleteResponsesQuery = "DELETE FROM QuestionResponses WHERE QuestionID IN (SELECT QuestionID FROM SurveyQuestions WHERE FormID = @FormID)";
                    SqlCommand deleteResponsesCmd = new SqlCommand(deleteResponsesQuery, con);
                    deleteResponsesCmd.Parameters.AddWithValue("@FormID", formId);
                    deleteResponsesCmd.ExecuteNonQuery();

                    // Delete SurveyOptions
                    string deleteOptionsQuery = "DELETE FROM SurveyOptions WHERE QuestionID IN (SELECT QuestionID FROM SurveyQuestions WHERE FormID = @FormID)";
                    SqlCommand deleteOptionsCmd = new SqlCommand(deleteOptionsQuery, con);
                    deleteOptionsCmd.Parameters.AddWithValue("@FormID", formId);
                    deleteOptionsCmd.ExecuteNonQuery();

                    // Delete SurveyQuestions
                    string deleteQuestionsQuery = "DELETE FROM SurveyQuestions WHERE FormID = @FormID";
                    SqlCommand deleteQuestionsCmd = new SqlCommand(deleteQuestionsQuery, con);
                    deleteQuestionsCmd.Parameters.AddWithValue("@FormID", formId);
                    deleteQuestionsCmd.ExecuteNonQuery();

                    // Delete SurveyResponses
                    string deleteSurveyResponsesQuery = "DELETE FROM SurveyResponses WHERE FormID = @FormID";
                    SqlCommand deleteSurveyResponsesCmd = new SqlCommand(deleteSurveyResponsesQuery, con);
                    deleteSurveyResponsesCmd.Parameters.AddWithValue("@FormID", formId);
                    deleteSurveyResponsesCmd.ExecuteNonQuery();

                    // Finally, delete the form
                    string deleteFormQuery = "DELETE FROM SurveyForms WHERE FormID = @FormID";
                    SqlCommand deleteFormCmd = new SqlCommand(deleteFormQuery, con);
                    deleteFormCmd.Parameters.AddWithValue("@FormID", formId);
                    deleteFormCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }
    }
}
