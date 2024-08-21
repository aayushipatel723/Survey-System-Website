using Survey_System.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            string confPassword = tbConfPassword.Text;
            string userType = rblUserType.SelectedValue;
            string profession = tbProfession.Text;
            string organization = tbOrganization.Text;
            string dobText = tbDob.Text; // Date of Birth as text
            string gender = rblGender.SelectedValue;

            if (password != confPassword)
            {
                // Handle password mismatch
                return;
            }

            DateTime dob;
            if (!DateTime.TryParse(dobText, out dob))
            {
                // Handle invalid date format
                // For example, show an error message to the user
                return;
            }

            // Database connection string from Web.config
            string connectionString = DB_Utils.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the email already exists
                string checkEmailQuery = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, conn);
                checkEmailCmd.Parameters.AddWithValue("@Email", email);
                int emailCount = (int)checkEmailCmd.ExecuteScalar();

                if (emailCount > 0)
                {
                    // Email already exists, handle accordingly
                    Response.Redirect("Home.aspx"); // Redirect to home or error page
                    return;
                }

                // Insert new user
                string insertQuery = "INSERT INTO [User] (Email, Password, User_type, Profession, Organization, Dob, Gender) VALUES (@Email, @Password, @User_type, @Profession, @Organization, @Dob, @Gender)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@Email", email);
                insertCmd.Parameters.AddWithValue("@Password", password);
                insertCmd.Parameters.AddWithValue("@User_type", userType);
                insertCmd.Parameters.AddWithValue("@Profession", profession);
                insertCmd.Parameters.AddWithValue("@Organization", organization);
                insertCmd.Parameters.AddWithValue("@Dob", dob); // Use DateTime type
                insertCmd.Parameters.AddWithValue("@Gender", gender);

                insertCmd.ExecuteNonQuery();
            }
            // Redirect to login page on successful registration
            Response.Redirect("Login.aspx");
        }            
    }
}