using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using BCrypt;


namespace Funds_Management_System
{
    public partial class LogInForm : Form
    {
        MySqlConnection connection;
     

        public LogInForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
           

            ShowCheckbox.CheckedChanged += new EventHandler(ShowCheckbox_CheckedChanged);
        }
        private void ShowCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowCheckbox.Checked)
            {
                PasswordTextbox.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                PasswordTextbox.UseSystemPasswordChar = true; // Hide password
            }
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=127.0.0.1;user=root;database=fund management system;password=";
            connection = new MySqlConnection(connectionString);
        }

        private bool ValidatePassword(string enteredPassword, string hashedPassword)
        {
            // Use BCrypt to check if the entered password matches the hashed password
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }

        private MySqlDataReader Login(string username, string password)
        {
            try
            {
                connection.Open();
                string query = "SELECT UserProfile.*, StudentInfo.department_id, Officer.organization_id " +
                       "FROM UserProfile " +
                       "LEFT JOIN StudentInfo ON UserProfile.user_id = StudentInfo.user_id " +
                       "LEFT JOIN Officer ON UserProfile.user_id = Officer.user_id " +
                       "WHERE UserProfile.username = @username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string hashedPasswordFromDB = reader.GetString("password_hash");

                    if (ValidatePassword(password, hashedPasswordFromDB))
                    {
                        // Passwords match, return the reader
                        return reader;
                    }
                }

                // If no matching user or password doesn't match, return null
                reader.Close();
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Text;

            if (username == "admin" && password == "adminpass")
            {
                // Admin login, open admin form
                this.Hide();
                Creators_Admin_Interface adminForm = new Creators_Admin_Interface();
                adminForm.ShowDialog();
            }
            else
            {
                MySqlDataReader reader = Login(username, password);
                if (reader != null)
                {
                    MessageBox.Show("Log In Successfully!");
                    int currentUserId = reader.GetInt32("user_id");
                    int departmentId = reader.IsDBNull(reader.GetOrdinal("department_id")) ? 0 : reader.GetInt32("department_id");
                    int organizationId = reader.IsDBNull(reader.GetOrdinal("organization_id")) ? 0 : reader.GetInt32("organization_id");

                    reader.Close();
                    connection.Close();

                    this.Hide();
                    User_Interface userint = new User_Interface(currentUserId, departmentId, organizationId);
                    userint.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                }

                connection.Close();
            }
        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create_Account create = new Create_Account();
            create.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            PasswordTextbox.UseSystemPasswordChar = false;

           
            UsernameTextbox.TabStop = false;
            PasswordTextbox.TabStop = false;

          
            UsernameTextbox.GotFocus += UsernameTextbox_GotFocus;
            UsernameTextbox.Leave += UsernameTextbox_Leave;
            SetUsernamePlaceholder();

            PasswordTextbox.GotFocus += PasswordTextbox_GotFocus;
            PasswordTextbox.Leave += PasswordTextbox_Leave;
            SetPasswordPlaceholder();
        }

        private void UsernameTextbox_GotFocus(object sender, EventArgs e)
        {
            if (UsernameTextbox.Text == "Username")
            {
                UsernameTextbox.Text = string.Empty;
            }
        }

        private void PasswordTextbox_GotFocus(object sender, EventArgs e)
        {
            if (PasswordTextbox.Text == "Password")
            {
                PasswordTextbox.Text = string.Empty;
                PasswordTextbox.UseSystemPasswordChar = true;
            }
        }

        private void UsernameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextbox.Text))
            {
                SetUsernamePlaceholder();
            }
        }

        private void PasswordTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextbox.Text))
            {
                SetPasswordPlaceholder();
            }
        }

        private void SetUsernamePlaceholder()
        {
            if (string.IsNullOrEmpty(UsernameTextbox.Text))
            {
                UsernameTextbox.Text = "Username";
            }
        }

        private void SetPasswordPlaceholder()
        {
            if (string.IsNullOrEmpty(PasswordTextbox.Text))
            {
                PasswordTextbox.Text = "Password";
                PasswordTextbox.UseSystemPasswordChar = false;
            }
        }

        private void ShowCheckbox_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
