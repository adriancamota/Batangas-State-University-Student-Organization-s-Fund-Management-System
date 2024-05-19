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
    public partial class Fund : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Fund(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            LoadOrganizationBalance();
            ShowCheckbox.CheckedChanged += new EventHandler(ShowCheckbox_CheckedChanged);
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
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Expenses expenses = new Expenses(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            expenses.ShowDialog();
        }
        private void ShowCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowCheckbox.Checked)
            {
                // Show the actual balance
                LoadOrganizationBalance();
            }
            else
            {
                // Encrypt the balance with at least 5 dots
                int minDots = 5;
                string encryptedBalance = EncryptWithDots(OrganizationBalance.Text, minDots);
                OrganizationBalance.Text = encryptedBalance;
            }
        }

     
        private void Fund_Load(object sender, EventArgs e)
        {
            ShowCheckbox.Checked = true;
        }

        private string EncryptWithDots(string text, int minDots)
        {
            // Ensure that there are at least minDots
            int dotLength = Math.Max(minDots, text.Length);

            // Generate dots
            string dot = new string('\u25CF', dotLength); // Unicode character for a dot

            // Return the dots
            return dot;
        }


        private void label1_Click(object sender, EventArgs e)
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CashFlows cashflow = new CashFlows(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            cashflow.ShowDialog();
        }
        private void LoadOrgBalance()
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
                    if (balance == 0)
                    {

                        this.Hide();
                        Org_Initial_Balance1 orgbalance = new Org_Initial_Balance1(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
                        orgbalance.ShowDialog(); 

                    }
                    else
                    {
                        // Show prompt that balance is not zero
                        MessageBox.Show("Organization balance is not zero. Cannot proceed.");
                    }
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LoadOrgBalance();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            reports.ShowDialog(); 
        }
    }
}
