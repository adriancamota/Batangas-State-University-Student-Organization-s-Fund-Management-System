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
    public partial class CashFlows : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public CashFlows(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();


            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            
            SearchTextbox.Visible = false;
            LoadOrganizationBalance();

       



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

        private void LoadOrganizationBalance()
        {
            try
            {
                connection.Open();
                string query = "SELECT balance FROM Organization WHERE organization_id = @orgId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    // Convert balance to double
                    double balance = Convert.ToDouble(result);
                    // Format balance with commas for thousands and millions
                    string formattedBalance = string.Format("{0:N}", balance);
                    // Display formatted balance in your UI control
                    // For example, if you have a label named lblBalance:
                    OrganizationBalance.Text = formattedBalance;
                }
                else
                {
                    MessageBox.Show("Organization not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void CashFlows_Load(object sender, EventArgs e)
        {

        }
        private void LoadTransactions()
        {
            try
            {
                string query = "SELECT \r\n    t.transaction_id,\r\n    t.cash_type,\r\n    t.transaction_name,\r\n    CONCAT(IF(t.cash_type = 'Withdraw', '-', '+'), t.amount) AS Amount,\r\n    pb.payment_title AS billing_name,\r\n    t.notes,\r\n    CONCAT(u.first_name, ' ', u.last_name) AS full_name,\r\n    sec.section_name,\r\n    s.gsuite_id,\r\n    o.abbreviation AS organization_abbreviation,\r\n    e.event_name,\r\n    t.transaction_timestamp\r\nFROM \r\n    Transactions t\r\nJOIN \r\n    UserProfile u ON t.user_id = u.user_id\r\nJOIN \r\n    Organization o ON t.organization_id = o.organization_id\r\nLEFT JOIN \r\n    Event e ON t.event_id = e.event_id\r\nLEFT JOIN\r\n    StudentInfo s ON u.user_id = s.user_id\r\nLEFT JOIN\r\n    Section sec ON s.section_id = sec.section_id\r\nLEFT JOIN\r\n    PaymentBilling pb ON t.billing_id = pb.billing_id\r\nWHERE \r\n    t.organization_id = @OrganizationId \r\nORDER BY \r\n    transaction_id Asc ;\r\n";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizationId", CurrentOrganizationId);


                connection.Open();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Add a new column for the computed balance
                dataTable.Columns.Add("Computed Balance", typeof(string));

                decimal previousAmount = 0;

                // Iterate through each row to calculate the computed balance
                foreach (DataRow row in dataTable.Rows)
                {
                    string amountString = row["Amount"].ToString();
                    decimal currentAmount = Convert.ToDecimal(amountString.Substring(1)); // Remove the sign to get the numeric amount

                    // Calculate computed balance
                    decimal computedBalance = previousAmount + (amountString.StartsWith("-") ? -currentAmount : currentAmount);

                    // Update the row with computed balance
                    row["Computed Balance"] = $"{previousAmount}{(previousAmount == 0 ? "" : (amountString.StartsWith("-") ? "-" : "+"))}{currentAmount}={computedBalance}";

                    // Update previous amount for next iteration
                    previousAmount = computedBalance;
                }

                // Assign the DataTable to the DataGridView
                DataGridView.DataSource = dataTable;

                // Handle the RowPrePaint event to set row colors
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        private void WithdrawLoadTransactions(string searchCriteria)
        {
            try
            {
                string query = @"SELECT 
                            t.transaction_id,
                            t.cash_type,
                            t.transaction_name,
                            t.amount,
                            CONCAT(u.first_name, ' ', u.last_name) AS full_name,
                            sec.section_name,
                            s.gsuite_id,
                            o.abbreviation AS organization,
                            e.event_name as related_event,
                            t.transaction_timestamp
                        FROM 
                            Transactions t
                        JOIN 
                            UserProfile u ON t.user_id = u.user_id
                        JOIN 
                            Organization o ON t.organization_id = o.organization_id
                        LEFT JOIN 
                            Event e ON t.event_id = e.event_id
                        LEFT JOIN
                            StudentInfo s ON u.user_id = s.user_id
                        LEFT JOIN
                            Section sec ON s.section_id = sec.section_id
                        LEFT JOIN
                            PaymentBilling pb ON t.billing_id = pb.billing_id
                        WHERE 
                            t.organization_id = @OrganizationId
                            AND t.cash_type = 'Withdraw'
                            AND (u.first_name LIKE @SearchCriteria
                                OR u.last_name LIKE @SearchCriteria
                                OR s.gsuite_id LIKE @SearchCriteria
                                OR u.user_id = @SearchCriteria)
                               Order By transaction_id

                                ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizationId", CurrentOrganizationId);
                command.Parameters.AddWithValue("@SearchCriteria", "%" + searchCriteria + "%");

                connection.Open();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Assign the DataTable to the DataGridView
                DataGridView.DataSource = dataTable;

                // Handle the RowPrePaint event to set row colors

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void DepositLoadTransactions(string searchCriteria)
        {
            try
            {
                string query = @"SELECT 
                            t.transaction_id,
                            t.cash_type,
                            t.transaction_name,
                            t.amount,
                            pb.payment_title AS billing_name,
                            t.notes,
                            CONCAT(u.first_name, ' ', u.last_name) AS full_name,
                            sec.section_name,
                            s.gsuite_id,
                            o.abbreviation as Organization,
                            e.event_name as related_event,
                            t.transaction_timestamp
                        FROM 
                            Transactions t
                        JOIN 
                            UserProfile u ON t.user_id = u.user_id
                        JOIN 
                            Organization o ON t.organization_id = o.organization_id
                        LEFT JOIN 
                            Event e ON t.event_id = e.event_id
                        LEFT JOIN
                            StudentInfo s ON u.user_id = s.user_id
                        LEFT JOIN
                            Section sec ON s.section_id = sec.section_id
                        LEFT JOIN
                            PaymentBilling pb ON t.billing_id = pb.billing_id
                        WHERE 
                            t.organization_id = @OrganizationId
                            AND t.cash_type = 'Deposit'
                            AND (u.first_name LIKE @SearchCriteria
                                OR u.last_name LIKE @SearchCriteria
                                OR s.gsuite_id LIKE @SearchCriteria
                                OR u.user_id = @SearchCriteria)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizationId", CurrentOrganizationId);
                command.Parameters.AddWithValue("@SearchCriteria", "%" + searchCriteria + "%");

                connection.Open();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Assign the DataTable to the DataGridView
                DataGridView.DataSource = dataTable;

                // Handle the RowPrePaint event to set row colors

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void TransactionHistory_Click(object sender, EventArgs e)
        {
           
            SearchTextbox.Visible = false;
            LoadTransactions();

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            SearchTextbox.Visible = true;
            WithdrawLoadTransactions(SearchTextbox.Text);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
            SearchTextbox.Visible = true;
            DepositLoadTransactions(SearchTextbox.Text);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT 
                            receipt_id as Receipt_number , user_id,payment_title as Payment, amount as Total_Amount, receipt_type as Transaction,gcash_reference_number as Reference_Number,notes as Notes
                        FROM 
                           receipts

                                ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizationId", CurrentOrganizationId);
               

                connection.Open();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Assign the DataTable to the DataGridView
                DataGridView.DataSource = dataTable;

                // Handle the RowPrePaint event to set row colors

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }

}
