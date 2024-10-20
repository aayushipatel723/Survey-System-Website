using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Survey_System
{
    public partial class AddQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Handle the form submission if needed
            if (IsPostBack)
            {
                SaveQuestionsToDatabase();
            }
        }

        private void SaveQuestionsToDatabase()
        {
            int formId = Convert.ToInt32(Request.QueryString["FormID"]); // Retrieve FormID from the query string
            int questionCount = Convert.ToInt32(Request.Form["NumberOfQuestions"]);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString))
            {
                con.Open();

                for (int i = 0; i < questionCount; i++)
                {
                    string questionText = Request.Form[$"QuestionText{i}"];
                    string fieldType = Request.Form[$"FieldType{i}"];

                    // Insert question into SurveyQuestions table
                    string insertQuestionQuery = "INSERT INTO SurveyQuestions (FormID, QuestionText, FieldType) OUTPUT INSERTED.QuestionID VALUES (@FormID, @QuestionText, @FieldType)";
                    SqlCommand questionCmd = new SqlCommand(insertQuestionQuery, con);
                    questionCmd.Parameters.AddWithValue("@FormID", formId); // Use the retrieved FormID
                    questionCmd.Parameters.AddWithValue("@QuestionText", questionText);
                    questionCmd.Parameters.AddWithValue("@FieldType", fieldType);

                    // Get the generated QuestionID
                    int questionId = (int)questionCmd.ExecuteScalar();

                    // If the question type is RadioButton or CheckBox, insert options
                    if (fieldType == "RadioButton" || fieldType == "CheckBox")
                    {
                        int numberOfOptions = Convert.ToInt32(Request.Form[$"NumberOfOptions{i}"]);
                        for (int j = 0; j < numberOfOptions; j++)
                        {
                            string optionText = Request.Form[$"OptionText{i}_{j}"];
                            if (!string.IsNullOrWhiteSpace(optionText)) // Ensure the option text is not empty
                            {
                                string insertOptionQuery = "INSERT INTO SurveyOptions (QuestionID, OptionText) VALUES (@QuestionID, @OptionText)";
                                SqlCommand optionCmd = new SqlCommand(insertOptionQuery, con);
                                optionCmd.Parameters.AddWithValue("@QuestionID", questionId);
                                optionCmd.Parameters.AddWithValue("@OptionText", optionText);
                                optionCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            // Optionally, redirect after saving
            Response.Redirect("ViewSurveys.aspx"); // Replace with your success page
        }

    }
}
