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

namespace Funds_Management_System
{
    public partial class Org_Initial_Balance1 : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Org_Initial_Balance1(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            Amount.Text = "Insert your Initial Balance";
            Pin.Text = "Enter your pin to confirm";

            Amount.GotFocus += Amount_GotFocus;
            Amount.Leave += Amount_Leave;

            Pin.GotFocus += Pin_GotFocus;
            Pin.Leave += Pin_Leave;

            
        }
 
        private void Pin_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Pin.Text))
            {
               Pin.Text = "Enter your pin to confirm";
            }
        }

        private void Pin_GotFocus(object sender, EventArgs e)
        {
            if (Pin.Text == "Enter your pin to confirm")
            {
                Pin.Text = string.Empty;
            }
         
        }

        private void Amount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Amount.Text))
            {
               Amount.Text = "Insert your Initial Balance";
            }
        }

        private void Amount_GotFocus(object sender, EventArgs e)
        {
            if (Amount.Text == "Insert your Initial Balance")
            {
                Amount.Text = string.Empty;
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
        private void Org_Initial_Balance1_Load(object sender, EventArgs e)
        {

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

        private void guna2Button5_Click(object sender, EventArgs e)
        {// Check if the PIN is correct
            if (!VerifyPin(Pin.Text))
            {
                MessageBox.Show("Incorrect PIN. Please try again.");
                return;
            }

            // Check if the entered amount is valid
            if (!decimal.TryParse(Amount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            try
            {
                // Connection must be valid and open
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Determine cash type based on payment method (assuming cashType is Withdraw or Deposit)
                string cashType = "Deposit";

                // Prepare the SQL query to insert a new transaction
                string insertTransactionQuery = @"INSERT INTO Transactions (user_id, organization_id, transaction_name, amount, transaction_timestamp, cash_type) 
                                          VALUES (@userId, @orgId, @transactionName, @amount, NOW(), @cashType)";

                // Prepare the SQL query to insert a new receipt
                string insertReceiptQuery = @"INSERT INTO Receipts (user_id, organization_id, payment_title, amount, receipt_timestamp, receipt_type) 
                                      VALUES (@userId, @orgId, @paymentTitle, @amount, NOW(), @receiptType)";

                // Prepare the SQL query to update organization's balance
                string updateBalanceQuery = "UPDATE Organization SET balance = balance + @amount WHERE organization_id = @orgId";

                // Begin transaction
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert the transaction
                        using (MySqlCommand insertTransactionCommand = new MySqlCommand(insertTransactionQuery, connection, transaction))
                        {
                            // Set parameters for the transaction insertion
                            insertTransactionCommand.Parameters.AddWithValue("@userId", CurrentUserId);
                            insertTransactionCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                            insertTransactionCommand.Parameters.AddWithValue("@transactionName", "Initial Balance");
                            insertTransactionCommand.Parameters.AddWithValue("@amount", amount); // Use parsed amount
                            insertTransactionCommand.Parameters.AddWithValue("@cashType", cashType);

                            // Execute the transaction insertion query
                            insertTransactionCommand.ExecuteNonQuery();

                            // Get the last inserted transaction ID
                            long transactionId = insertTransactionCommand.LastInsertedId;

                            // Insert the receipt
                            using (MySqlCommand insertReceiptCommand = new MySqlCommand(insertReceiptQuery, connection, transaction))
                            {
                                // Set parameters for the receipt insertion
                                insertReceiptCommand.Parameters.AddWithValue("@userId", CurrentUserId);
                                insertReceiptCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                                insertReceiptCommand.Parameters.AddWithValue("@paymentTitle", "Initial Balance");
                                insertReceiptCommand.Parameters.AddWithValue("@amount", amount); // Use parsed amount
                                insertReceiptCommand.Parameters.AddWithValue("@receiptType", cashType);

                                // Execute the receipt insertion query
                                insertReceiptCommand.ExecuteNonQuery();

                                // Get the last inserted receipt ID
                                long lastReceiptId = insertReceiptCommand.LastInsertedId;

                                // Create and execute the INSERT command for Receipts_Transaction table
                                string insertReceiptTransactionQuery = "INSERT INTO receipts_transactions (receipt_id, transaction_id) " +
                                                                        "VALUES (@receiptId, @transactionId)";
                                using (MySqlCommand cmdReceiptTransaction = new MySqlCommand(insertReceiptTransactionQuery, connection, transaction))
                                {
                                    cmdReceiptTransaction.Parameters.AddWithValue("@receiptId", lastReceiptId);
                                    cmdReceiptTransaction.Parameters.AddWithValue("@transactionId", transactionId);
                                    cmdReceiptTransaction.ExecuteNonQuery();
                                }
                            }
                        }

                        // Update organization's balance
                        using (MySqlCommand updateBalanceCommand = new MySqlCommand(updateBalanceQuery, connection, transaction))
                        {
                            updateBalanceCommand.Parameters.AddWithValue("@amount", amount); // Use parsed amount
                            updateBalanceCommand.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                            updateBalanceCommand.ExecuteNonQuery();
                        }


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
                        string insertReportQuery = @"INSERT INTO Reports (department_id, organization_id, report_name, report_date, report_author, remaining_balance, total_generated_funds, total_expenses,  related_transaction_id,receipt_id) 
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



                        // Commit transaction if all operations succeed
                        transaction.Commit();
                        MessageBox.Show("Organization Initial Balance added successfully.");
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

            this.Hide();
            Initial_Receipt initialreceipt = new Initial_Receipt(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            initialreceipt.ShowDialog();




        }
    }
}
