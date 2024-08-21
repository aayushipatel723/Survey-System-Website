using Survey_System.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Email and Password are required.";
                return;
            }

            // Connection string
            string connectionString = DB_Utils.getConnectionString();

            // SQL query to check user credentials
            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    int userCount = (int)command.ExecuteScalar();

                    if (userCount > 0)
                    {
                        // Successful login
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        // Invalid credentials
                        lblMessage.Text = "Invalid Email or Password.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }
    }
}