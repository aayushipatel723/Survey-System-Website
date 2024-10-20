using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class SubmitResponse : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadQuestions();
            }
        }

        private void LoadQuestions()
        {
            string formId = Request.QueryString["FormID"];

            if (string.IsNullOrEmpty(formId))
            {
                Response.Write("Error: FormID not supplied.");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT QuestionID, QuestionText, FieldType FROM SurveyQuestions WHERE FormID = @FormID", conn);
                cmd.Parameters.AddWithValue("@FormID", formId);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Question> questions = new List<Question>();
                            while (reader.Read())
                            {
                                Question question = new Question
                                {
                                    QuestionID = reader["QuestionID"].ToString(),
                                    QuestionText = reader["QuestionText"].ToString(),
                                    FieldType = reader["FieldType"].ToString()
                                };
                                questions.Add(question);
                            }

                            QuestionsRepeater.DataSource = questions;
                            QuestionsRepeater.DataBind();
                        }
                        else
                        {
                            Response.Write("No questions found for the specified FormID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        protected void QuestionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var question = (Question)e.Item.DataItem;

                TextBox responseTextBox = (TextBox)e.Item.FindControl("ResponseTextBox");
                Repeater optionsRepeater = (Repeater)e.Item.FindControl("OptionsRepeater");
                Repeater checkBoxOptionsRepeater = (Repeater)e.Item.FindControl("CheckBoxOptionsRepeater");

                // Check FieldType and load options accordingly
                if (question.FieldType == "TextBox")
                {
                    responseTextBox.Visible = true;
                }
                else if (question.FieldType == "Radio")
                {
                    optionsRepeater.Visible = true;
                    LoadRadioOptions(optionsRepeater, question.QuestionID);
                }
                else if (question.FieldType == "Checkbox")
                {
                    checkBoxOptionsRepeater.Visible = true;
                    LoadCheckBoxOptions(checkBoxOptionsRepeater, question.QuestionID);
                }
            }
        }

        private void LoadRadioOptions(Repeater repeater, string questionId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT OptionID, OptionText FROM SurveyOptions WHERE QuestionID = @QuestionID", conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        repeater.DataSource = reader;
                        repeater.DataBind();
                    }
                }
            }
        }

        private void LoadCheckBoxOptions(Repeater repeater, string questionId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT OptionID, OptionText FROM SurveyOptions WHERE QuestionID = @QuestionID", conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        repeater.DataSource = reader;
                        repeater.DataBind();
                    }
                }
            }
        }

        protected void SubmitResponseButton_Click(object sender, EventArgs e)
        {
            string formId = Request.QueryString["FormID"];
            string responderId = Session["UserID"].ToString(); // Get the ID of the responder
            int responseId;

            // Insert into SurveyResponses to get ResponseID
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO SurveyResponses (FormID, ResponderID, ResponseDate) VALUES (@FormID, @ResponderID, @ResponseDate); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@FormID", formId);
                cmd.Parameters.AddWithValue("@ResponderID", responderId);
                cmd.Parameters.AddWithValue("@ResponseDate", DateTime.Now);

                conn.Open();
                responseId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            foreach (RepeaterItem item in QuestionsRepeater.Items)
            {
                HiddenField questionIdField = (HiddenField)item.FindControl("QuestionID");
                TextBox responseTextBox = (TextBox)item.FindControl("ResponseTextBox");
                Repeater optionsRepeater = (Repeater)item.FindControl("OptionsRepeater");
                Repeater checkBoxOptionsRepeater = (Repeater)item.FindControl("CheckBoxOptionsRepeater");

                string questionId = questionIdField.Value;

                // Handling text response
                if (responseTextBox != null && responseTextBox.Visible)
                {
                    string responseText = responseTextBox.Text;
                    if (!string.IsNullOrEmpty(responseText))
                    {
                        SaveResponse(responseId, questionId, responseText, null);
                    }
                }

                // Handling radio button options
                if (optionsRepeater != null && optionsRepeater.Visible)
                {
                    foreach (RepeaterItem optionItem in optionsRepeater.Items)
                    {
                        RadioButton optionRadioButton = (RadioButton)optionItem.FindControl("OptionRadioButton");
                        HiddenField optionIdField = (HiddenField)optionItem.FindControl("OptionID");

                        if (optionRadioButton != null && optionRadioButton.Checked)
                        {
                            string optionId = optionIdField.Value;
                            SaveResponse(responseId, questionId, null, optionId);
                        }
                    }
                }

                // Handling checkbox options
                if (checkBoxOptionsRepeater != null && checkBoxOptionsRepeater.Visible)
                {
                    foreach (RepeaterItem optionItem in checkBoxOptionsRepeater.Items)
                    {
                        CheckBox optionCheckBox = (CheckBox)optionItem.FindControl("OptionCheckBox");
                        HiddenField optionIdField = (HiddenField)optionItem.FindControl("OptionID");

                        if (optionCheckBox != null && optionCheckBox.Checked)
                        {
                            string optionId = optionIdField.Value;
                            SaveResponse(responseId, questionId, null, optionId);
                        }
                    }
                }
            }

            // Redirect to thank you page or display success message
            Response.Redirect("ThankYou.aspx");
        }

        private void SaveResponse(int responseId, string questionId, string responseText, string optionId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Update the table name to QuestionResponses
                SqlCommand cmd = new SqlCommand("INSERT INTO QuestionResponses (ResponseID, QuestionID, ResponseText, SelectedOptionID) VALUES (@ResponseID, @QuestionID, @ResponseText, @OptionID)", conn);
                cmd.Parameters.AddWithValue("@ResponseID", responseId);
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                cmd.Parameters.AddWithValue("@ResponseText", (object)responseText ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OptionID", (object)optionId ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }              

        public class Question
    {
        public string QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string FieldType { get; set; }
    }
}