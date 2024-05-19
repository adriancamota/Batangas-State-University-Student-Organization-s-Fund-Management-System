using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BCrypt;

namespace Funds_Management_System
{
    public partial class Expenses : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Expenses(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;


            ExpensesDataGridView.ColumnCount = 4;
            ExpensesDataGridView.Columns[0].Name = "Transaction Name/Product name";
            ExpensesDataGridView.Columns[1].Name = "Amount";
            ExpensesDataGridView.Columns[2].Name = "Gcash Reference Number";
            ExpensesDataGridView.Columns[3].Name = "Related Event";
            Transaction_Name.Text = "Transaction Name";
            Amount.Text = "Amount";
            ReferenceNum.Text = "Reference Number";

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

            EventCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
          EventCombobox.Font = new Font(EventCombobox.Font.FontFamily, 9);


            Transaction_Name.GotFocus += Transaction_Name_GotFocus;
            Transaction_Name.Leave += Transaction_Name_Leave;

            Amount.GotFocus += Amount_GotFocus;
            Amount.Leave += Amount_Leave;

            ReferenceNum.GotFocus += ReferenceNum_GotFocus;
            ReferenceNum.Leave += ReferenceNum_Leave;

            PopulateEventComboBox();

            Save.Visible = false;
            Delete.Visible = false;
        }

        private void ReferenceNum_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ReferenceNum.Text))
            {
               ReferenceNum.Text = "Reference Number";
            }
        }

        private void ReferenceNum_GotFocus(object sender, EventArgs e)
        {
            if (ReferenceNum.Text == "Reference Number")
            {
                ReferenceNum.Text = string.Empty;
            }
        }

        private void Amount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Amount.Text))
            {
                Amount.Text = "Amount";
            }
        }

        private void Amount_GotFocus(object sender, EventArgs e)
        {
            if (Amount.Text == "Amount")
            {
                Amount.Text = string.Empty;
            }
        }

        private void Transaction_Name_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Transaction_Name.Text))
            {
                Transaction_Name.Text = "Transaction Name";
            }
        }

        private void Transaction_Name_GotFocus(object sender, EventArgs e)
        {
            if (Transaction_Name.Text == "Transaction Name")
            {
               Transaction_Name.Text = string.Empty;
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

               EventCombobox.Items.AddRange(new string[] { "Event" });
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


        private void Expenses_Load(object sender, EventArgs e)
        {

        }
      
        private void InsertButton_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(Transaction_Name.Text) ||
                Transaction_Name.Text == "Transaction Name" ||
                string.IsNullOrWhiteSpace(Amount.Text) ||
                Amount.Text == "Amount" ||
                string.IsNullOrWhiteSpace(ReferenceNum.Text) ||  // Add this condition
                EventCombobox.SelectedItem.ToString() == "Events" || EventCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

           

            // Add the data to the DataGridView
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(ExpensesDataGridView);



            // Set the values of the cells in the row
            row.Cells[0].Value = Transaction_Name.Text;
            row.Cells[1].Value = Amount.Text;
           
            string referencenum = ReferenceNum.Text.Trim();
            if (referencenum == "Reference Number")
            {
                row.Cells[2].Value = null;
            }
            else
            {
                row.Cells[2].Value = ReferenceNum.Text;
            }

            row.Cells[3].Value = EventCombobox.SelectedItem.ToString();

            // Add the row to the DataGridView
            ExpensesDataGridView.Rows.Add(row);

            Transaction_Name.Text = "Transaction Name";
            Amount.Text = "Amount";
            ReferenceNum.Text = "Reference Number";
            EventCombobox.SelectedIndex = 0;

            CalculateTotalAmount();
            UpdateTotalAmountLabel();


            Save.Visible = true;
            Delete.Visible = true;
        }

        private decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in ExpensesDataGridView.Rows)
            {
                // Check if the row is not a new row (empty row for data entry)
                if (!row.IsNewRow)
                {
                    // Get the value from the "Amount" column (assuming it's the second column, adjust if necessary)
                    if (decimal.TryParse(row.Cells["Amount"].Value?.ToString(), out decimal amount))
                    {
                        // Add the amount to the total
                        totalAmount += amount;
                    }
                }
            }

            return totalAmount;
        }

        private void UpdateTotalAmountLabel()
        {
            // Call the method to calculate the total amount
            decimal totalAmount = CalculateTotalAmount();

            // Update the text of the label with the total amount
            TotalAmountLabel.Text = "Total Amount: " + totalAmount.ToString("#,0.00");

        }
        private void Delete_Click(object sender, EventArgs e)

        {
            // Check if there is a selected row
            if (ExpensesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = ExpensesDataGridView.SelectedRows[0];

                // Remove the selected row from the DataGridView
                ExpensesDataGridView.Rows.Remove(selectedRow);

                // Update the total amount label after deleting the row
                UpdateTotalAmountLabel();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

            if (ExpensesDataGridView.Rows.Count == 1)
            {
                Delete.Visible = false;
                Save.Visible = false;   
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

            if (ExpensesDataGridView.Rows.Count == 1)
            {
                MessageBox.Show("No expenses to save.");
                return;
            }
            else
            {

                // List to store transaction IDs
                List<int> transactionIds = new List<int>();

                try
                {
                    // Open the connection
                    connection.Open();

                    // Get the timestamp of the latest transaction
                    DateTime latestTransactionTimestamp = DateTime.MinValue;

                    // Iterate through each row in the DataGridView
                    foreach (DataGridViewRow row in ExpensesDataGridView.Rows)
                    {
                        // Check if the row is not a new row (empty row for data entry)
                        if (!row.IsNewRow)
                        {
                            // Retrieve event name and organization name from the DataGridView
                            string selectedEventName = row.Cells["Related Event"].Value.ToString();
                            string transactionName = row.Cells["Transaction Name/Product name"].Value.ToString();

                            // Retrieve event id and organization id from the database based on the event name
                            GetEventIdAndOrganizationIdByNameFromDatabase(selectedEventName, out int eventId, out int organizationId);

                            if (eventId != -1 && organizationId != -1)
                            {
                                // Extract other values from the row
                                int userId = CurrentUserId;
                                decimal amount = decimal.Parse(row.Cells["Amount"].Value.ToString());
                                string gcashReferenceNumber = row.Cells["Gcash Reference Number"].Value != null ? row.Cells["Gcash Reference Number"].Value.ToString() : null;

                                // Check if the value is "Reference Number" and set it to null if so
                                if (gcashReferenceNumber == "Reference Number")
                                {
                                    gcashReferenceNumber = null;
                                }

                                // Create and execute the INSERT command for Transactions table
                                string insertTransactionQuery = "INSERT INTO Transactions (user_id, amount, gcash_reference_number, transaction_timestamp, cash_type, event_id, organization_id, transaction_name) " +
                                                                "VALUES (@userId, @amount, @gcashReferenceNumber, NOW(), 'Withdraw', @eventId, @organizationId, @transactionName)";
                                using (MySqlCommand cmdTransaction = new MySqlCommand(insertTransactionQuery, connection))
                                {
                                    cmdTransaction.Parameters.AddWithValue("@userId", userId);
                                    cmdTransaction.Parameters.AddWithValue("@amount", amount);
                                    cmdTransaction.Parameters.AddWithValue("@gcashReferenceNumber", gcashReferenceNumber);
                                    cmdTransaction.Parameters.AddWithValue("@eventId", eventId);
                                    cmdTransaction.Parameters.AddWithValue("@organizationId", organizationId);
                                    cmdTransaction.Parameters.AddWithValue("@transactionName", transactionName);
                                    cmdTransaction.ExecuteNonQuery();

                                    int lastTransactionId = (int)cmdTransaction.LastInsertedId;
                                    transactionIds.Add(lastTransactionId);

                                    // Update latest transaction timestamp if needed
                                    DateTime transactionTimestamp = DateTime.Now;
                                    if (transactionTimestamp > latestTransactionTimestamp)
                                    {
                                        latestTransactionTimestamp = transactionTimestamp;
                                    }
                                }

                                string updateBalanceQuery = "UPDATE Organization SET balance = balance - @amount WHERE organization_id = @organizationId";
                                using (MySqlCommand cmdUpdateBalance = new MySqlCommand(updateBalanceQuery, connection))
                                {
                                    cmdUpdateBalance.Parameters.AddWithValue("@amount", amount);
                                    cmdUpdateBalance.Parameters.AddWithValue("@organizationId", organizationId);
                                    cmdUpdateBalance.ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                MessageBox.Show($"Event '{selectedEventName}' or Organization '{selectedEventName}' not found.");
                            }
                        }
                    }

                    // Calculate total amount from the DataGridView
                    decimal totalAmount = CalculateTotalAmount();
                    // Create and execute the INSERT command for Receipts table
                    string insertReceiptQuery = "INSERT INTO Receipts (user_id, organization_id, amount, receipt_timestamp, notes, receipt_type) " +
                                                "VALUES (@userId, @organizationId, @amount, @receiptTimestamp, @notes, 'Withdraw')";
                    using (MySqlCommand cmdReceipt = new MySqlCommand(insertReceiptQuery, connection))
                    {
                        cmdReceipt.Parameters.AddWithValue("@userId", CurrentUserId);
                        cmdReceipt.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);
                        cmdReceipt.Parameters.AddWithValue("@amount", totalAmount);
                        cmdReceipt.Parameters.AddWithValue("@receiptTimestamp", latestTransactionTimestamp);
                        cmdReceipt.Parameters.AddWithValue("@notes", "Total amount for multiple transactions");
                        cmdReceipt.ExecuteNonQuery();

                        // Retrieve the last inserted receipt ID
                        int lastReceiptId = (int)cmdReceipt.LastInsertedId;

                        // Iterate through each transaction ID and insert into Receipts_Transaction table
                        foreach (int transactionId in transactionIds)
                        {
                            // Create and execute the INSERT command for Receipts_Transaction table
                            string insertReceiptTransactionQuery = "INSERT INTO receipts_transactions (receipt_id, transaction_id) " +
                                                                    "VALUES (@receiptId, @transactionId)";
                            using (MySqlCommand cmdReceiptTransaction = new MySqlCommand(insertReceiptTransactionQuery, connection))
                            {
                                cmdReceiptTransaction.Parameters.AddWithValue("@receiptId", lastReceiptId);
                                cmdReceiptTransaction.Parameters.AddWithValue("@transactionId", transactionId);
                                cmdReceiptTransaction.ExecuteNonQuery();
                            }
                        }
                    }

                    // Fetch the latest transaction date
                    DateTime latestTransactionDate = DateTime.MinValue; // Initialize with a default value
                    string fetchLatestTransactionDateQuery = "SELECT MAX(transaction_timestamp) FROM Transactions WHERE organization_id = @orgId";

                    using (MySqlCommand fetchLatestTransactionDateCommand = new MySqlCommand(fetchLatestTransactionDateQuery, connection))
                    {
                        fetchLatestTransactionDateCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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
                        fetchBalanceAndAssetsCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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
                        fetchTotalGeneratedFundsCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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
                        fetchTotalExpensesCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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
                        fetchLatestTransactionIdCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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
                        fetchLatestReceiptIdCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                        object result = fetchLatestReceiptIdCommand.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            latestReceiptId = Convert.ToInt32(result);
                        }
                    }



                    // Insert data into the Reports table
                    string insertReportQuery = @"INSERT INTO Reports (department_id, organization_id, report_name, report_date, report_author, remaining_balance, total_generated_funds, total_expenses,  related_transaction_id, receipt_id) 
                             VALUES (@departmentId, @orgId, @reportName, @reportDate, @reportAuthor, @remainingBalance, @totalGeneratedFunds, @totalExpenses,  @relatedTransactionId, @relatedReceiptId)";

                    using (MySqlCommand insertReportCommand = new MySqlCommand(insertReportQuery, connection))
                    {
                        insertReportCommand.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                        insertReportCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
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





                    // Show success message
                    MessageBox.Show("Withdraw Records Inserted Successfully.");

                    // Clear DataGridView
                    ExpensesDataGridView.Rows.Clear();

                    // Clear other related controls
                    // Assuming you have textboxes named Transaction_Name, Amount, ReferenceNum
                    Transaction_Name.Clear();
                    Amount.Clear();
                    ReferenceNum.Clear();

                    // Update total amount label
                    UpdateTotalAmountLabel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data to the database: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }

                this.Hide();
                withdrawreceipt withdraw = new withdrawreceipt(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
                withdraw.ShowDialog();
            }

           

        }

        private void GetEventIdAndOrganizationIdByNameFromDatabase(string eventName, out int eventId, out int organizationId)
        {
            // Query the database to retrieve event id and organization id based on the event name
            string query = "SELECT event_id, organization_id FROM event WHERE event_name = @eventName";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@eventName", eventName);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        eventId = Convert.ToInt32(reader["event_id"]);
                        organizationId = Convert.ToInt32(reader["organization_id"]);
                    }
                    else
                    {
                        eventId = -1; // Event not found
                        organizationId = -1; // Organization not found
                    }
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fund fund = new Fund(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            fund.ShowDialog();

        }

            private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TotalAmountLabel_Click(object sender, EventArgs e)
        {

        }
    }
    }
