using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCrypt;
using Microsoft.Extensions.Logging;
using static System.ComponentModel.Design.ObjectSelectorEditor;


namespace Funds_Management_System
{
    public partial class Create_Payment : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }

        public Create_Payment(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;


            PopulateEventComboBox();
            PinTextbox();

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

           EventCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
           EventCombobox.Font = new Font(EventCombobox.Font.FontFamily, 9);


            Pin.Enabled = false;

            paymentTitleTextBox.Text = "Payment Title";
            amountTextBox.Text = "Amount";
            notesTextBox.Text = "Notes (optional)";

            paymentTitleTextBox.GotFocus += PaymentTitleTextBox_GotFocus;
            paymentTitleTextBox.Leave += PaymentTitleTextBox_Leave;

            amountTextBox.GotFocus += AmountTextBox_GotFocus;
            amountTextBox.Leave += AmountTextBox_Leave;

            notesTextBox.GotFocus += NotesTextBox_GotFocus;
            notesTextBox.Leave += NotesTextBox_Leave;

        }

        private void NotesTextBox_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(notesTextBox.Text))
            {
                notesTextBox.Text = "Notes (optional)";
            }
        }

        private void NotesTextBox_GotFocus(object sender, EventArgs e)
        {
            if (notesTextBox.Text == "Notes (optional)")
            {
                notesTextBox.Text = string.Empty;
            }
        }

        private void AmountTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountTextBox.Text))
            {
                amountTextBox.Text = "Amount";
            }
        }

        private void AmountTextBox_GotFocus(object sender, EventArgs e)
        {
            if (amountTextBox.Text == "Amount")
            {
                amountTextBox.Text = string.Empty;
            }
        }

        private void PaymentTitleTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(paymentTitleTextBox.Text))
            {
                paymentTitleTextBox.Text = "Payment Title";
            }
        }

        private void PaymentTitleTextBox_GotFocus(object sender, EventArgs e)
        {
            if (paymentTitleTextBox.Text == "Payment Title")
            {
                paymentTitleTextBox.Text = string.Empty;
            }
        }
        private void PinTextbox()
        {
            Pin.Text = "Enter your pin to confirm";
        }
        private bool VerifyPin(string enteredPin)
        {
            try
            {
                // Query to fetch PIN from the database based on the user ID
                string query = "SELECT pin FROM UserProfile WHERE user_id = @userId";

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@userId", CurrentUserId);
                    string storedPin = command.ExecuteScalar()?.ToString();

                    // Compare entered pin with the stored pin
                    return enteredPin == storedPin;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error verifying PIN: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
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

        private void PopulateEventComboBox()
        {
            try
            {
                // Open the connection if it's not already open
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Clear existing items before adding new ones
                EventCombobox.Items.Clear();

                EventCombobox.Items.Add("Event");
                EventCombobox.SelectedIndex = 0;


                // SQL query to select event names for the current department
                string query = "SELECT event_name FROM event WHERE department_id = @departmentId";

                // Create and execute the command with parameterized query
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);

                    // Execute the query and read the results
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Retrieve event name from the reader
                            string eventName = reader.GetString("event_name");

                            // Add event name to the combo box
                            EventCombobox.Items.Add(eventName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message if an exception occurs
                MessageBox.Show("Error populating event combo box: " + ex.Message);
            }
            finally
            {
                // Close the connection if it was opened
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }

        }

     

     

     
        

    

   
      
        

    
      

        private void Create_Payment_Load(object sender, EventArgs e)
        {

        }

       

      

        private void Generate_Click(object sender, EventArgs e)
        {


            // Check if payment title is still the default placeholder text
            if (paymentTitleTextBox.Text == "Payment Title")
            {
                MessageBox.Show("Please enter a valid payment title.");
                return;
            }
            if (EventCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select an event.");
                return;
            }
            // Check if amount is still the default placeholder text or empty
            if (amountTextBox.Text == "Amount" || string.IsNullOrEmpty(amountTextBox.Text))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            if (!VerifyPin(Pin.Text))
            {
                MessageBox.Show("Incorrect PIN. Please try again.");
                return;
            }

            try
            {
                // Open the connection
                connection.Open();

                // Get the selected event name from the combo box
                string selectedEventName = EventCombobox.SelectedItem.ToString();

                // Query to retrieve the event ID based on the selected event name
                string eventQuery = "SELECT event_id FROM event WHERE event_name = @eventName";

                // Create and execute the command to get the event ID
                using (MySqlCommand eventCmd = new MySqlCommand(eventQuery, connection))
                {
                    eventCmd.Parameters.AddWithValue("@eventName", selectedEventName);
                    int eventId = Convert.ToInt32(eventCmd.ExecuteScalar());

                    // Insert command to insert payment details into the PaymentBilling table
                    string insertQuery = "INSERT INTO PaymentBilling (user_id, organization_id, payment_title, amount, generated_timestamp, notes, event_id) " +
                                         "VALUES (@userId, @organizationId, @paymentTitle, @amount, NOW(), @notes, @eventId)";


                    string Note = notesTextBox.Text;
                    // Create and execute the command to insert payment details
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@userId", CurrentUserId);
                        insertCmd.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);
                        insertCmd.Parameters.AddWithValue("@paymentTitle", paymentTitleTextBox.Text);
                        insertCmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(amountTextBox.Text));
                        insertCmd.Parameters.AddWithValue("@notes", Note == "Notes (optional)" ? DBNull.Value : (object)Note);

                        insertCmd.Parameters.AddWithValue("@eventId", eventId);

                        // Execute the insert query
                        insertCmd.ExecuteNonQuery();

                        MessageBox.Show("Item Generated Successfully.");

                        this.Hide();
                        Organization_Admin_Interface OrgAdmin = new Organization_Admin_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
                        OrgAdmin.ShowDialog();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting payment record: " + ex.Message);
            }
            finally
            {
                // Close the connection
                connection.Close();
            }
        }

        private void EventCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Organization_Admin_Interface OrgAdmin = new Organization_Admin_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            OrgAdmin.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pin1_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "1";
            }
            else
            {
                Pin.Text += "1";
            }
        }

        private void pin2_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "2";
            }
            else
            {
                Pin.Text += "2";
            }
        }

        private void pin3_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "3";
            }
            else
            {
                Pin.Text += "3";
            }
        }

        private void pin4_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "4";
            }
            else
            {
                Pin.Text += "4";
            }
        }

        private void pin5_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "5";
            }
            else
            {
                Pin.Text += "5";
            }
        }

        private void pin6_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "6";
            }
            else
            {
                Pin.Text += "6";
            }
        }

        private void pin7_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "7";
            }
            else
            {
                Pin.Text += "7";
            }
        }

        private void pin8_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "8";
            }
            else
            {
                Pin.Text += "8";
            }
        }

        private void pin9_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "9";
            }
            else
            {
                Pin.Text += "9";
            }
        }

        private void pin0_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = "0";
            }
            else
            {
                Pin.Text += "0";
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            if (Pin.Text.Length > 0)
            {
                PinTextbox();
            }
        }
    }
}
