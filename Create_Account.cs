using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Funds_Management_System
{
    public partial class Create_Account : Form
    {
        MySqlConnection connection;

        public Create_Account()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeGenderComboBox();

            InitializeTextBoxEvents();

        }

        private void InitializeTextBoxEvents()
        {


            FirstNameTextbox.GotFocus += FirstNameTextbox_GotFocus;
            LastNameTextbox.GotFocus += LastNameTextbox_GotFocus;
            MiddleInitialTextbox.GotFocus += MiddleInitialTextbox_GotFocus;
            UsernameTextbox.GotFocus += UsernameTextbox_GotFocus;
            PasswordTextbox.GotFocus += PasswordTextbox_GotFocus;
            ConfirmPasswordTextbox.GotFocus += ConfirmPasswordTextbox_GotFocus;
            EmailTextbox.GotFocus += EmailTextbox_GotFocus;
            ContactNumberTextbox.GotFocus += ContactNumberTextbox_GotFocus;
            AddressTextbox.GotFocus += AddressTextbox_GotFocus;
            SuffixTextBox.GotFocus += SuffixTextBox_GotFocus;
   

            FirstNameTextbox.Leave += FirstNameTextbox_Leave;
            LastNameTextbox.Leave += LastNameTextbox_Leave;
            MiddleInitialTextbox.Leave += MiddleInitialTextbox_Leave;
            UsernameTextbox.Leave += UsernameTextbox_Leave;
            PasswordTextbox.Leave += PasswordTextbox_Leave;
            ConfirmPasswordTextbox.Leave += ConfirmPasswordTextbox_Leave;
            EmailTextbox.Leave += EmailTextbox_Leave;
            ContactNumberTextbox.Leave += ContactNumberTextbox_Leave;
            AddressTextbox.Leave += AddressTextbox_Leave;
            SuffixTextBox.Leave += SuffixTextBox_Leave;
       
        }

        

        private void InitializeConnection()
        {
            try
            {
                string connectionString = "server=127.0.0.1;user=root;database=fund management system;password=";
                connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InitializeGenderComboBox()
        {
            // Add items to the GenderComboBox
            GenderComboBox.Items.AddRange(new string[] { "Gender", "Male", "Female" });
            GenderComboBox.SelectedIndex = 0; // Set the default selection to the first item ("Gender")
        }


        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            

            FirstNameTextbox.TabStop = false;
            LastNameTextbox.TabStop = false;
            MiddleInitialTextbox.TabStop = false;
            UsernameTextbox.TabStop = false;
            PasswordTextbox.TabStop = false;
            ConfirmPasswordTextbox.TabStop = false;
            EmailTextbox.TabStop = false;
            ContactNumberTextbox.TabStop = false;
            AddressTextbox.TabStop = false;

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

            GenderComboBox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
            GenderComboBox.Font = new Font(GenderComboBox.Font.FontFamily, 9);
        }

        private void InsertUserProfile(string firstName, string lastName, string middleInitial, string suffix, string gender, DateTime birthday, string address, string email, string contactNumber, string username, string password)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Hash the password using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                string query = @"INSERT INTO UserProfile (first_name, last_name, middle_initial, suffix, gender, birthday, address, email, contact_number, username, password_hash)
                VALUES (@FirstName, @LastName, @MiddleInitial, @Suffix, @Gender, @Birthday, @Address, @Email, @ContactNumber, @Username, @Password)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@MiddleInitial", middleInitial);

                // Check if the suffix is "Suffix", if so, set it to DBNull.Value, otherwise, set it to the suffix value
                cmd.Parameters.AddWithValue("@Suffix", suffix == "Suffix" ? DBNull.Value : (object)suffix);

                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Birthday", birthday);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword); // Use hashed password

                cmd.ExecuteNonQuery();

                MessageBox.Show("Account Created Successfully.");

                this.Hide();
                LogInForm form = new LogInForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting user profile: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private bool ValidateInput()
        {
            // Check if any textbox contains placeholder text
            if (FirstNameTextbox.Text == "First Name" ||
                LastNameTextbox.Text == "Last Name" ||
                MiddleInitialTextbox.Text == "Middle Initial" ||
               
                UsernameTextbox.Text == "Username" ||
                PasswordTextbox.Text == "Password" ||
                ConfirmPasswordTextbox.Text == "Confirm Password" ||
                EmailTextbox.Text == "Email" ||
                ContactNumberTextbox.Text == "Contact Number" ||
                AddressTextbox.Text == "Address")
            {
                MessageBox.Show("Please fill in all fields before submitting.");
                return false;
            }
            if (PasswordTextbox.Text != ConfirmPasswordTextbox.Text)
            {
                MessageBox.Show("Password and confirm password do not match.");
                return false;
            }

            // Additional validation checks can be added here if needed

            return true;
        }

        private void CreateAccountButton_Click_1(object sender, EventArgs e)
        {

            if (ValidateInput())
            {
                string firstName = FirstNameTextbox.Text;
                string lastName = LastNameTextbox.Text;
                string middleInitial = MiddleInitialTextbox.Text;
                string suffix = SuffixTextBox.Text;
                string gender = GenderComboBox.SelectedItem.ToString();
                DateTime birthday = BirthdayDatePicker.Value;
                string address = AddressTextbox.Text;
                string email = EmailTextbox.Text;
                string contactNumber = ContactNumberTextbox.Text;
                string username = UsernameTextbox.Text;
                string password = PasswordTextbox.Text;

                InsertUserProfile(firstName, lastName, middleInitial, suffix, gender, birthday, address, email, contactNumber, username, password);
            }
        }



        private void SuffixTextBox_GotFocus(object sender, EventArgs e)
        {
            if (SuffixTextBox.Text == "Suffix")
            {
                SuffixTextBox.Text = string.Empty;
            }
        }

        private void SuffixTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SuffixTextBox.Text))
            {
                SuffixTextBox.Text = "Suffix";
            }
        }


        private void AddressTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AddressTextbox.Text))
            {
                AddressTextbox.Text = "Address";
            }
        }

        private void AddressTextbox_GotFocus(object sender, EventArgs e)
        {
            if(AddressTextbox.Text == "Address")
            {
                AddressTextbox.Text = string.Empty;
            }
        }

        private void EmailTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextbox.Text))
            {
               EmailTextbox.Text = "Email";
            }
        }

        private void ContactNumberTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ContactNumberTextbox.Text))
            {
                ContactNumberTextbox.Text = "Contact Number";
            }
        }


        private void ConfirmPasswordTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConfirmPasswordTextbox.Text))
            {
                ConfirmPasswordTextbox.Text = "Confirm Password";
            }
        }

        private void PasswordTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextbox.Text))
            {
                PasswordTextbox.Text = "Password";
            }
        }

    
        private void UsernameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextbox.Text))
            {
                UsernameTextbox.Text = "Username";
            }
        }

        private void MiddleInitialTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MiddleInitialTextbox.Text))
            {
                MiddleInitialTextbox.Text = "Middle Initial";
            }
        }

        private void LastNameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LastNameTextbox.Text))
            {
                LastNameTextbox.Text = "Last Name";
            }
        }

        private void FirstNameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameTextbox.Text))
            {
                FirstNameTextbox.Text = "First Name";
            }
        }


        private void EmailTextbox_GotFocus(object sender, EventArgs e)
        {
            if(EmailTextbox.Text == "Email")
            {
                EmailTextbox.Text = string.Empty;
            }

        }

    
        private void ContactNumberTextbox_GotFocus(object sender, EventArgs e)
        {
            if (ContactNumberTextbox.Text == "Contact Number")
            {
                ContactNumberTextbox.Text = string.Empty;
            }
        }

        private void ConfirmPasswordTextbox_GotFocus(object sender, EventArgs e)
        {
            if (ConfirmPasswordTextbox.Text == "Confirm Password")
            {
                ConfirmPasswordTextbox.Text = string.Empty;
            }
        }

        private void PasswordTextbox_GotFocus(object sender, EventArgs e)
        {
            if (PasswordTextbox.Text == "Password")
            {
                PasswordTextbox.Text = string.Empty;
            }
        }

   
        private void MiddleInitialTextbox_GotFocus(object sender, EventArgs e)
        {

            if (MiddleInitialTextbox.Text == "Middle Initial")
            {
                MiddleInitialTextbox.Text = string.Empty;
            }
        }

        private void FirstNameTextbox_GotFocus(object sender, EventArgs e)
        {

            if (FirstNameTextbox.Text == "First Name")
            {
                FirstNameTextbox.Text = string.Empty;
            }
        }

        private void LastNameTextbox_GotFocus(object sender, EventArgs e)
        {

            if (LastNameTextbox.Text == "Last Name")
            {
                LastNameTextbox.Text = string.Empty;
            }
        }

        private void UsernameTextbox_GotFocus(object sender, EventArgs e)
        {
            if (UsernameTextbox.Text == "Username")
            {
                UsernameTextbox.Text = string.Empty;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void SuffixTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
