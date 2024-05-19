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
using System.Web.Util;


namespace Funds_Management_System
{
    public partial class User_Interface : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int organization_id { get; set; }
        public User_Interface(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            organization_id = currentOrganizationId;
            LoadTransactionsForCurrentUser();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dataGridView1.ClearSelection();


            PopulatePaymentTitles();
            populateMethodCombobox();

            LoadUserInfo();

            UserId.Text = currentUserId.ToString();


            Pin.Enabled = false;

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

            paymentTitle.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);
            paymentMethod.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            ReferenceNum.GotFocus += ReferenceNum_GotFocus;
            ReferenceNum.Leave += ReferenceNum_Leave;

            Notes.GotFocus += Notes_GotFocus;
            Notes.Leave += Notes_Leave;

            Pin.GotFocus += Pin_GotFocus;
            Pin.Leave += Pin_Leave;

            // Set the font size to 9
            paymentTitle.Font = new Font(paymentTitle.Font.FontFamily, 9);
            paymentTitle.Font = new Font(paymentTitle.Font.FontFamily, 9);
            PinTextbox();
            Pin.TabStop = false;




            ReferenceNum.Visible = false;
            Notes.Text = "Notes (optional)";
            ReferenceNum.Text = "Gcash Reference Number";


            Pin.Visible = false;
            pin1.Visible = false;
            pin2.Visible = false;
            pin3.Visible = false;
            pin4.Visible = false;
            pin5.Visible = false;
            pin6.Visible = false;
            pin7.Visible = false;
            pin8.Visible = false;
            pin9.Visible = false;
            pin0.Visible = false;
            deleteButton.Visible = false;

            PayButton.Visible = false;
            backButton2.Visible = false;

            Pin.UseSystemPasswordChar = false;


        }

        private void Pin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Pin.Text))
            {
               Pin.Text = "Enter your pin";
            }
        }

        private void Pin_GotFocus(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = string.Empty;
            }
        }

        private void Notes_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Notes.Text))
            {
                Notes.Text = "Notes (optional)";
            }
        
         }

        private void Notes_GotFocus(object sender, EventArgs e)
        {

            if (Notes.Text == "Notes (optional)")
            {
               Notes.Text = string.Empty;
            }
        }

        private void ReferenceNum_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ReferenceNum.Text))
            {
               ReferenceNum.Text = "Gcash Reference Number";
            }
        }

        private void ReferenceNum_GotFocus(object sender, EventArgs e)
        {
            if (ReferenceNum.Text == "Gcash Reference Number")
            {
              ReferenceNum.Text = string.Empty;
            }
        }

        private void populateMethodCombobox()
        {
            // Add items to the ComboBox using AddRange
            paymentMethod.Items.AddRange(new string[] { "Payment Method", "Cash", "Gcash" });

            // Set the default selected item to "Payment Method"
            paymentMethod.SelectedIndex = 0;
        }

        private void LoadTransactionsForCurrentUser()
        {
            try
            {
                connection.Open(); // Open the provided connection

                // SQL query to select transactions for the current user with non-null billing IDs
                string query = @"
                               SELECT 
                        PB.payment_title AS Paid,
                        R.receipt_id as Receipt_Id
                    FROM 
                        Transactions T
                    JOIN 
                        PaymentBilling PB ON T.billing_id = PB.billing_id
                    LEFT JOIN 
                        Receipts_Transactions RT ON T.transaction_id = RT.transaction_id
                    LEFT JOIN 
                        Receipts R ON RT.receipt_id = R.receipt_id
                    WHERE 
                        T.user_id = @userId
                    AND T.billing_id IS NOT NULL
order by receipt_id Desc";
                    

                // Create MySqlCommand object
                using (MySqlCommand checkTransactionCommand = new MySqlCommand(query, connection))
                {
                    // Add parameter
                    checkTransactionCommand.Parameters.AddWithValue("@userId", CurrentUserId);

                    // Execute the command
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(checkTransactionCommand))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    // Set the selection mode to FullRowSelect
                  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Close the provided connection
            }

        }

        private void LoadUserInfo()
        {
            try
            {
                connection.Open(); // Open the provided connection

                // SQL query to select transactions for the current user with non-null billing IDs
                string query = @"
            SELECT 
                CONCAT(
                    COALESCE(u.first_name, ''), ' ', 
                    COALESCE(u.middle_initial, ''), ' ', 
                    COALESCE(u.last_name, ''), ' ', 
                    COALESCE(u.suffix, '')
                ) AS FullName,
                u.user_id AS UserID,
                COALESCE(s.gsuite_id, '') AS GsuiteID,
                COALESCE(d.department_name, '') AS Department,
                COALESCE(c.course_name, '') AS Course,
                COALESCE(sec.section_name, '') AS Section
            FROM 
                UserProfile u
            JOIN 
                StudentInfo s ON u.user_id = s.user_id
            JOIN 
                Department d ON s.department_id = d.department_id
            JOIN 
                Course c ON s.course_id = c.course_id
            JOIN 
                Section sec ON s.section_id = sec.section_id
            WHERE
                u.user_id = @userId";

                using (MySqlCommand checkTransactionCommand = new MySqlCommand(query, connection))
                {
                    // Add parameter
                    checkTransactionCommand.Parameters.AddWithValue("@userId", CurrentUserId);

                    // Create MySqlCommand object
                    using (MySqlDataReader reader = checkTransactionCommand.ExecuteReader())
                     {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string fullName = reader["FullName"].ToString();
                                string gsuiteId = reader["GsuiteID"].ToString();
                                string department = reader["Department"].ToString();
                                string course = reader["Course"].ToString();
                                string section = reader["Section"].ToString();

                                // Display the retrieved data in appropriate controls (e.g., labels, textboxes)
                                Nameee.Text = fullName;
                                Gsuite.Text = gsuiteId;
                               Department.Text = department;
                                Course.Text = course;
                                Section.Text = section;
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified ID.");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Close the provided connection
            }

        }

        private void InitializeConnection()
        {
            try
            {
                string connectionString = "server=127.0.0.1;user=root;database=fund management system;password=";
                connection = new MySqlConnection(connectionString); // Corrected MySqlConnection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
          
        }

        private void PopulatePaymentTitles()
        {

            try
            {


                paymentTitle.Items.Add("Choose payment");
                paymentTitle.SelectedIndex = 0;

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT payment_title FROM PaymentBilling", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())

                {
                    paymentTitle.Items.Add(reader["payment_title"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }



        private void UserInterface_Load(object sender, EventArgs e)
        {

        }

        private void CheckOrganizationAccess()
        {
            try
            {
                connection.Open();

                // Query to check organization_access for the current user
                string query = "SELECT organization_access FROM userprofile WHERE user_id = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", CurrentUserId);

                // Assuming @organization_access is a parameter representing access condition
                bool organizationAccessCondition = true; // Set the organization access condition here
                command.Parameters.AddWithValue("@organization_access", organizationAccessCondition);

                // Execute the query to fetch organization_access
                object organizationAccessObj = command.ExecuteScalar();

                if (organizationAccessObj != null && organizationAccessObj != DBNull.Value)
                {
                    bool organizationAccess = Convert.ToBoolean(organizationAccessObj);

                    if (organizationAccess)
                    {
                        this.Hide();
                        Organization_Admin_Interface OrgAdmin = new Organization_Admin_Interface(CurrentUserId, CurrentDepartmentId, organization_id);
                        OrgAdmin.ShowDialog();


                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to use this feature.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void CheckDepartmentAccess()
        {

            try
            {
                connection.Open();

                // Query to check department_access for the current user
                string query = "SELECT department_access FROM userprofile WHERE user_id = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", CurrentUserId);

                // Assuming @department_access is a parameter representing access condition
                bool departmentAccessCondition = true; // Set the department access condition here
                command.Parameters.AddWithValue("@department_access", departmentAccessCondition);

                // Execute the query to fetch department_access
                object departmentAccessObj = command.ExecuteScalar();

                if (departmentAccessObj != null && departmentAccessObj != DBNull.Value)
                {
                    bool departmentAccess = Convert.ToBoolean(departmentAccessObj);

                    if (departmentAccess)
                    {



                        this.Hide();
                        Department_Admin_Interface DeptAccess = new Department_Admin_Interface(CurrentUserId, CurrentDepartmentId, organization_id);
                        DeptAccess.ShowDialog();


                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to use this feature.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }



        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            School_Information_Update update = new School_Information_Update(CurrentUserId, CurrentDepartmentId, organization_id);
            update.ShowDialog();
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            CheckDepartmentAccess();
        }

        private void OrgAdmin_Click(object sender, EventArgs e)
        {
            CheckOrganizationAccess();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Reset user-related data
            CurrentUserId = 0;
            CurrentDepartmentId = 0;
            organization_id = 0;

            // Show the login form
            this.Hide();
            LogInForm loginForm = new LogInForm();
            loginForm.Show();


        }

      

        private void paymentTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                string connectionString = "server=127.0.0.1;user=root;database=fund management system;password=";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT organization_id, event_id, amount FROM PaymentBilling WHERE payment_title = @paymentTitle", connection);
                    cmd.Parameters.AddWithValue("@paymentTitle", paymentTitle.SelectedItem.ToString());
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int organizationId = Convert.ToInt32(reader["organization_id"]);
                        int eventId = Convert.ToInt32(reader["event_id"]);
                        decimal amount = Convert.ToDecimal(reader["amount"]);


                        // Close the reader before opening new connections
                        reader.Close();

                        // Retrieve organization name
                        MySqlCommand orgCmd = new MySqlCommand("SELECT organization_name FROM Organization WHERE organization_id = @organizationId", connection);
                        orgCmd.Parameters.AddWithValue("@organizationId", organizationId);
                        string organizationName = orgCmd.ExecuteScalar().ToString();

                        // Retrieve event name
                        MySqlCommand eventCmd = new MySqlCommand("SELECT event_name FROM Event WHERE event_id = @eventId", connection);
                        eventCmd.Parameters.AddWithValue("@eventId", eventId);
                        string eventName = eventCmd.ExecuteScalar().ToString();

                        // Update labels
                        organizationLabel.Text = "Organization: " + organizationName;
                        eventLabel.Text = "Event: " + eventName;
                        amountLabel.Text = "Amount: P " + amount.ToString("#,##0.00");

                    }
                    reader.Close(); // Close the reader after retrieving all necessary data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pin1_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "1";
            }
            else
            {
                Pin.Text += "1";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin2_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "2";
            }
            else
            {
                Pin.Text += "2";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin3_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pinn")
            {
                Pin.Text = "3";
            }
            else
            {
                Pin.Text += "3";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin4_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "4";
            }
            else
            {
                Pin.Text += "4";

            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin5_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "5";
            }
            else
            {
                Pin.Text += "5";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin6_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "6";
            }
            else
            {
                Pin.Text += "6";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin7_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "7";
            }
            else
            {
                Pin.Text += "7";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin8_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "8";
            }
            else
            {
                Pin.Text += "8";
               
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin9_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "9";
            }
            else
            {
                Pin.Text += "9";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin0_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin")
            {
                Pin.Text = "0";
            }
            else
            {
                Pin.Text += "0";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (Pin.Text.Length > 0)
            {
                PinTextbox();
            }
            Pin.UseSystemPasswordChar = false;
        }
        private void PinTextbox()
        {
            Pin.Text = "Enter your pin";
        }



        private void paymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected item is "Gcash"
            if (paymentMethod.SelectedItem.ToString() == "Gcash")
            {
                // Set the value of the ReferenceNum TextBox
                ReferenceNum.Visible = true;
                ReferenceNum.TabStop = false;
                ReferenceNum.Text = "Gcash Reference Number";


            }
            else
            {
                // If the selected item is not "Gcash", clear the TextBox and disable it

                ReferenceNum.Visible = false;
            }
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

        private void PayButton_Click(object sender, EventArgs e)
        {
            int organization_id = 0;

           

            // Check if the PIN is correct
            if (!VerifyPin(Pin.Text))
            {
                MessageBox.Show("Incorrect PIN. Please try again.");
                return;
            }

            // Connection must be valid and open
            if (connection.State != ConnectionState.Open)
                connection.Open();

            try
            {
                // Fetch organization_id based on the selected combobox value
                string selectedOrganization = organizationLabel.Text.Replace("Organization: ", "");
                string fetchOrganizationIdQuery = "SELECT organization_id FROM Organization WHERE organization_name = @organizationName";

                using (MySqlCommand fetchOrganizationIdCommand = new MySqlCommand(fetchOrganizationIdQuery, connection))
                {
                    fetchOrganizationIdCommand.Parameters.AddWithValue("@organizationName", selectedOrganization);
                    organization_id = Convert.ToInt32(fetchOrganizationIdCommand.ExecuteScalar());
                }

                // Check if all necessary fields are filled
                if (paymentTitle.SelectedItem == null || string.IsNullOrWhiteSpace(paymentTitle.SelectedItem.ToString()) || paymentMethod.SelectedItem == null || (paymentMethod.SelectedItem.ToString() == "Gcash" && string.IsNullOrEmpty(ReferenceNum.Text)) || string.IsNullOrWhiteSpace(Notes.Text))
                {
                    MessageBox.Show("Please fill all required fields.");
                    return;
                }

                // Check if the payment title has already been paid
                string selectedPaymentTitle = paymentTitle.SelectedItem.ToString();
                string checkTransactionQuery = @"SELECT COUNT(*) FROM Transactions AS T
                                        JOIN PaymentBilling AS PB ON T.billing_id = PB.billing_id
                                        WHERE T.user_id = @userId 
                                        AND T.organization_id = @orgId 
                                        AND T.transaction_name = 'Payments of Individual Students' 
                                        AND PB.payment_title = @paymentTitle";

                using (MySqlCommand checkTransactionCommand = new MySqlCommand(checkTransactionQuery, connection))
                {
                    checkTransactionCommand.Parameters.AddWithValue("@userId", CurrentUserId);
                    checkTransactionCommand.Parameters.AddWithValue("@orgId", organization_id); // Use organization_id instead of selectedOrganization
                    checkTransactionCommand.Parameters.AddWithValue("@paymentTitle", selectedPaymentTitle);

                    int transactionCount = Convert.ToInt32(checkTransactionCommand.ExecuteScalar());
                    if (transactionCount > 0)
                    {
                        MessageBox.Show("You have already paid this item.");
                        return;
                    }
                }

                // Retrieve event ID, billing ID, amount, and payment method from the database
                int eventId = 0;
                int billingId = 0;
                decimal amount = 0;

                string selectedPaymentMethod = paymentMethod.SelectedItem.ToString();

                string query = @"SELECT event_id, billing_id, amount FROM PaymentBilling WHERE payment_title = @paymentTitle";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paymentTitle", selectedPaymentTitle);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            eventId = reader.GetInt32("event_id");
                            billingId = reader.GetInt32("billing_id");
                            amount = reader.GetDecimal("amount");
                        }
                    }
                }

                // Determine cash type based on payment method (assuming cashType is Withdraw or Deposit)
                string cashType = "Deposit";
                // Prepare the SQL query to insert a new transaction
                string insertTransactionQuery = @"INSERT INTO Transactions (user_id, organization_id, transaction_name, amount, event_id, billing_id, gcash_reference_number, notes, transaction_timestamp, paymentMethod, cash_type) 
                                        VALUES (@userId, @orgId, @transactionName, @amount, @eventId, @billingId, @gcashRefNum, @notes, NOW(), @paymentMethod, @cashType)";

                // Prepare the SQL query to insert a new receipt
                string insertReceiptQuery = @"INSERT INTO Receipts (user_id, organization_id, payment_title, amount, receipt_timestamp, gcash_reference_number, notes, receipt_type) 
                                    VALUES (@userId, @orgId, @paymentTitle, @amount, NOW(), @gcashRefNum, @notes, @receiptType)";

                // Begin transaction
                try
                {
                    // Begin transaction
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // List to store transaction IDs
                            List<long> transactionIdsList = new List<long>();

                            // Insert into Transactions table
                            using (MySqlCommand insertTransactionCommand = new MySqlCommand(insertTransactionQuery, connection, transaction))
                            {
                                insertTransactionCommand.Parameters.AddWithValue("@userId", CurrentUserId);
                                insertTransactionCommand.Parameters.AddWithValue("@orgId", organization_id);
                                insertTransactionCommand.Parameters.AddWithValue("@transactionName", "Payments of Individual Students");
                                insertTransactionCommand.Parameters.AddWithValue("@amount", amount);
                                insertTransactionCommand.Parameters.AddWithValue("@eventId", eventId);
                                insertTransactionCommand.Parameters.AddWithValue("@billingId", billingId);

                                if (selectedPaymentMethod == "Cash" )
                                {
                                    insertTransactionCommand.Parameters.AddWithValue("@gcashRefNum", DBNull.Value);
                                }
                                else
                                {
                                    insertTransactionCommand.Parameters.AddWithValue("@gcashRefNum", ReferenceNum.Text);
                                }

                                insertTransactionCommand.Parameters.AddWithValue("@notes", (Notes.Text == "Notes (optional)" || string.IsNullOrWhiteSpace(Notes.Text)) ? "" : Notes.Text);
                                insertTransactionCommand.Parameters.AddWithValue("@paymentMethod", selectedPaymentMethod);
                                insertTransactionCommand.Parameters.AddWithValue("@cashType", cashType);

                                insertTransactionCommand.ExecuteNonQuery();

                                // Retrieve the transaction ID
                                long transactionId = insertTransactionCommand.LastInsertedId;

                                // Add the transaction ID to the list
                                transactionIdsList.Add(transactionId);
                            }

                            // Insert into Receipts table
                            using (MySqlCommand insertReceiptCommand = new MySqlCommand(insertReceiptQuery, connection, transaction))
                            {
                                insertReceiptCommand.Parameters.AddWithValue("@userId", CurrentUserId);
                                insertReceiptCommand.Parameters.AddWithValue("@orgId", organization_id);
                                insertReceiptCommand.Parameters.AddWithValue("@paymentTitle", selectedPaymentTitle);
                                insertReceiptCommand.Parameters.AddWithValue("@amount", amount);

                                if (selectedPaymentMethod == "Cash" )
                                {
                                    insertReceiptCommand.Parameters.AddWithValue("@gcashRefNum", DBNull.Value);
                                }
                                else
                                {
                                    insertReceiptCommand.Parameters.AddWithValue("@gcashRefNum", ReferenceNum.Text);
                                }

                                insertReceiptCommand.Parameters.AddWithValue("@notes", (Notes.Text == "Notes (optional)" || string.IsNullOrWhiteSpace(Notes.Text)) ? "" : Notes.Text);
                                insertReceiptCommand.Parameters.AddWithValue("@receiptType", cashType);

                                insertReceiptCommand.ExecuteNonQuery();

                                // Retrieve the receipt ID
                                long receiptId = insertReceiptCommand.LastInsertedId;

                                // Insert into Receipts_Transactions table
                                foreach (var transId in transactionIdsList)
                                {
                                    string insertReceiptsTransactionsQuery = "INSERT INTO Receipts_Transactions (receipt_id, transaction_id) VALUES (@receiptId, @transactionId)";
                                    using (MySqlCommand insertReceiptsTransactionsCommand = new MySqlCommand(insertReceiptsTransactionsQuery, connection, transaction))
                                    {
                                        insertReceiptsTransactionsCommand.Parameters.AddWithValue("@receiptId", receiptId);
                                        insertReceiptsTransactionsCommand.Parameters.AddWithValue("@transactionId", transId);
                                        insertReceiptsTransactionsCommand.ExecuteNonQuery();
                                    }
                                }
                            }


                            // Calculate the new balance by adding the transaction amount to the current balance
                            decimal newBalance = 0;


                            // Fetch the current balance of the organization
                            string fetchBalanceQuery = "SELECT balance FROM Organization WHERE organization_id = @orgId";

                            using (MySqlCommand fetchBalanceCommand = new MySqlCommand(fetchBalanceQuery, connection))
                            {
                                fetchBalanceCommand.Parameters.AddWithValue("@orgId", organization_id);
                                newBalance = Convert.ToDecimal(fetchBalanceCommand.ExecuteScalar()) + amount; // Add the transaction amount
                            }

                            // Update the organization's balance in the database
                            string updateBalanceQuery = "UPDATE Organization SET balance = @newBalance WHERE organization_id = @orgId";

                            using (MySqlCommand updateBalanceCommand = new MySqlCommand(updateBalanceQuery, connection))
                            {
                                updateBalanceCommand.Parameters.AddWithValue("@newBalance", newBalance);
                                updateBalanceCommand.Parameters.AddWithValue("@orgId", organization_id);
                                updateBalanceCommand.ExecuteNonQuery();
                            }

                            // Fetch the latest transaction date
                            DateTime latestTransactionDate = DateTime.MinValue; // Initialize with a default value
                            string fetchLatestTransactionDateQuery = "SELECT MAX(transaction_timestamp) FROM Transactions WHERE organization_id = @orgId";

                            using (MySqlCommand fetchLatestTransactionDateCommand = new MySqlCommand(fetchLatestTransactionDateQuery, connection))
                            {
                                fetchLatestTransactionDateCommand.Parameters.AddWithValue("@orgId", organization_id);
                                object result = fetchLatestTransactionDateCommand.ExecuteScalar();
                                if (result != DBNull.Value)
                                {
                                    latestTransactionDate = Convert.ToDateTime(result);
                                }
                            }

                            // Fetch the remaining balance and total assets from the Organization table
                            decimal remainingBalance = 0;
                          

                            string fetchBalanceAndAssetsQuery = "SELECT balance FROM Organization WHERE organization_id = @orgId";

                            using (MySqlCommand fetchBalanceAndAssetsCommand = new MySqlCommand(fetchBalanceAndAssetsQuery, connection))
                            {
                                fetchBalanceAndAssetsCommand.Parameters.AddWithValue("@orgId", organization_id);
                                using (MySqlDataReader reader = fetchBalanceAndAssetsCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        remainingBalance = reader.GetDecimal("balance");
                                        
                                    }
                                }
                            }
                           
                            // Fetch the total generated funds (total deposit amount)
                            decimal totalGeneratedFunds = 0;
                            string fetchTotalGeneratedFundsQuery = @"SELECT SUM(amount) FROM Transactions 
                                        WHERE organization_id = @orgId AND cash_type = 'Deposit'";

                            using (MySqlCommand fetchTotalGeneratedFundsCommand = new MySqlCommand(fetchTotalGeneratedFundsQuery, connection))
                            {
                                fetchTotalGeneratedFundsCommand.Parameters.AddWithValue("@orgId", organization_id);
                                object result = fetchTotalGeneratedFundsCommand.ExecuteScalar();
                                if (result != DBNull.Value)
                                {
                                    totalGeneratedFunds = Convert.ToDecimal(result);
                                }
                            }

                            // Fetch the total expenses (total withdrawal amount)
                            decimal totalExpenses = 0;
                            string fetchTotalExpensesQuery = @"SELECT SUM(amount) FROM Receipts 
                                   WHERE organization_id = @orgId AND receipt_type = 'Withdraw'";

                            using (MySqlCommand fetchTotalExpensesCommand = new MySqlCommand(fetchTotalExpensesQuery, connection))
                            {
                                fetchTotalExpensesCommand.Parameters.AddWithValue("@orgId", organization_id);
                                object result = fetchTotalExpensesCommand.ExecuteScalar();
                                if (result != DBNull.Value)
                                {
                                    totalExpenses = Convert.ToDecimal(result);
                                }
                            }

                           

                            // Fetch the latest related transaction ID
                            int latestTransactionId = 0;
                            string fetchLatestTransactionIdQuery = "SELECT transaction_id FROM Transactions WHERE organization_id = @orgId ORDER BY transaction_timestamp DESC LIMIT 1";

                            using (MySqlCommand fetchLatestTransactionIdCommand = new MySqlCommand(fetchLatestTransactionIdQuery, connection))
                            {
                                fetchLatestTransactionIdCommand.Parameters.AddWithValue("@orgId", organization_id);
                                object result = fetchLatestTransactionIdCommand.ExecuteScalar();
                                if (result != DBNull.Value)
                                {
                                    latestTransactionId = Convert.ToInt32(result);
                                }
                            }

                          
                            // Fetch the latest related receipt ID
                            int latestReceiptId = 0;
                            string fetchLatestReceiptIdQuery = "SELECT receipt_id FROM Receipts WHERE organization_id = @orgId ORDER BY receipt_timestamp DESC LIMIT 1";

                            using (MySqlCommand fetchLatestReceiptIdCommand = new MySqlCommand(fetchLatestReceiptIdQuery, connection))
                            {
                                fetchLatestReceiptIdCommand.Parameters.AddWithValue("@orgId", organization_id);
                                object result = fetchLatestReceiptIdCommand.ExecuteScalar();
                                if (result != DBNull.Value)
                                {
                                    latestReceiptId = Convert.ToInt32(result);
                                }
                            }


                            // Insert data into the Reports table
                            string insertReportQuery = @"INSERT INTO Reports (department_id, organization_id, report_name, report_date, report_author, remaining_balance, total_generated_funds, total_expenses,  related_transaction_id,receipt_id) 
                             VALUES (@departmentId, @orgId, @reportName, @reportDate, @reportAuthor, @remainingBalance, @totalGeneratedFunds, @totalExpenses,  @relatedTransactionId, @relatedReceiptId)";

                            using (MySqlCommand insertReportCommand = new MySqlCommand(insertReportQuery, connection))
                            {
                                insertReportCommand.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                                insertReportCommand.Parameters.AddWithValue("@orgId", organization_id);
                                insertReportCommand.Parameters.AddWithValue("@reportName", "Statement of Cash Flow");
                                insertReportCommand.Parameters.AddWithValue("@reportDate", latestTransactionDate);
                                insertReportCommand.Parameters.AddWithValue("@reportAuthor", CurrentUserId);
                                insertReportCommand.Parameters.AddWithValue("@remainingBalance", remainingBalance);
                                insertReportCommand.Parameters.AddWithValue("@totalGeneratedFunds", totalGeneratedFunds);
                                insertReportCommand.Parameters.AddWithValue("@totalExpenses", totalExpenses);
                              
                                insertReportCommand.Parameters.AddWithValue("@relatedTransactionId", latestTransactionId);
                                insertReportCommand.Parameters.AddWithValue("@relatedReceiptId", latestReceiptId);

                                insertReportCommand.ExecuteNonQuery();
                            }




                            // Commit transaction if all operations succeed
                            transaction.Commit();
                            MessageBox.Show("Payment Records Inserted Successfully.");
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if any error occurs
                            transaction.Rollback();
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close connection
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            this.Hide();
            Reciept reciept = new Reciept(CurrentUserId, CurrentDepartmentId, this.organization_id);
            reciept.ShowDialog();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (paymentTitle.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a payment title.");
                return;
            }

            // Check if the payment method is selected
            if (paymentMethod.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            // Check if the Notes TextBox contains the default text
            string notes = Notes.Text.Trim();
            if (notes == "Notes (optional)")
            {
                // Set notes to null if it contains the default text
                notes = null;
            }
            string gcash = ReferenceNum.Text.Trim();
            // Check if the payment method is "Gcash" and if ReferenceNum is empty
            if (paymentMethod.SelectedItem.ToString() == "Gcash" && gcash == "Gcash Reference Number")
            {
                MessageBox.Show("Please enter the Gcash Reference Number.");
                return;
            }


            if (paymentTitle.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a payment title.");
                return;
            }

            // Check if the payment method is selected
            if (paymentMethod.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            paymentTitle.Visible = false;
            organizationLabel.Visible = false;
            eventLabel.Visible = false;
            amountLabel.Visible = false;
            paymentMethod.Visible = false;
            Notes.Visible = false;
            ReferenceNum.Visible = false;

            Pin.Visible = true;
            pin1.Visible = true;
            pin2.Visible = true;
            pin3.Visible = true;
            pin4.Visible = true;
            pin5.Visible = true;
            pin6.Visible = true;
            pin7.Visible = true;
            pin8.Visible = true;
            pin9.Visible = true;
            pin0.Visible    = true;
            deleteButton.Visible = true;

            PayButton.Visible = true;
            PayButton2.Visible = false;
            backButton2.Visible = true;


        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            paymentTitle.Visible = true;
            organizationLabel.Visible = true;
            eventLabel.Visible = true;
            amountLabel.Visible = true;
            paymentMethod.Visible = true;
            Notes.Visible = true;

        
          
            if (paymentMethod.SelectedItem.ToString() == "Gcash" )
            {
               ReferenceNum.Visible=true;
            }

            Pin.Visible = false;
            pin1.Visible = false;
            pin2.Visible = false;
            pin3.Visible =false;
            pin4.Visible = false;
            pin5.Visible = false;
            pin6.Visible = false;
            pin7.Visible = false;
            pin8.Visible = false;
            pin9.Visible = false;
            pin0.Visible = false;
            deleteButton.Visible = false;

            PayButton.Visible = false;
            PayButton2.Visible = true;
            backButton2.Visible= false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
