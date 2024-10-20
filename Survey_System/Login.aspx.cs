using Survey_System.Utils;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey_System
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Clear session if already logged in
            if (Session["UserEmail"] != null)
            {
                Response.Redirect("Home.aspx");
            }
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

            // SQL query to check user credentials and retrieve user type
            string query = "SELECT Id, Email, User_type FROM [User] WHERE Email = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Successful login
                        if (reader.Read())
                        {
                            string userEmail = reader["Email"].ToString();
                            string userType = reader["User_type"].ToString();
                            string userId = reader["Id"].ToString(); // Use 'Id' from schema

                            // Set session variables
                            Session["UserEmail"] = userEmail;
                            Session["UserType"] = userType;
                            Session["UserId"] = userId; // Store 'Id' in session

                            // Redirect to the home page or dashboard
                            Response.Redirect("Home.aspx");
                        }
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
