using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Survey_System
{
    public partial class ViewResponses : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int formId;
                if (int.TryParse(Request.QueryString["FormID"], out formId))
                {
                    LoadFormResponses(formId);
                }
                else
                {
                    Response.Write("Invalid Form ID.");
                }
            }
        }

        private void LoadFormResponses(int formId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT Q.QuestionText, QR.ResponseText, O.OptionText
                    FROM QuestionResponses QR
                    LEFT JOIN SurveyQuestions Q ON QR.QuestionID = Q.QuestionID
                    LEFT JOIN SurveyOptions O ON QR.SelectedOptionID = O.OptionID
                    WHERE Q.FormID = @FormID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FormID", formId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ResponsesGridView.DataSource = dt;
                ResponsesGridView.DataBind();
            }
        }
    }
}
