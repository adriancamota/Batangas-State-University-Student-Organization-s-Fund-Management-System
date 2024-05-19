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
    public partial class Organization_Admin_Interface : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Organization_Admin_Interface(int currentUserId,int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            SearchTextbox.Text = "Name/Gsuite";

            SearchTextbox.GotFocus += SearchTextbox_GotFocus;
            SearchTextbox.Leave += SearchTextbox_Leave;
        }

        private void SearchTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextbox.Text))
            {
                SearchTextbox.Text = "Name/Gsuite";
            }
        }

        private void SearchTextbox_GotFocus(object sender, EventArgs e)
        {
            if (SearchTextbox.Text == "Name/Gsuite")
            {
                SearchTextbox.Text = string.Empty;
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
        private void Organization_Admin_Interface_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            

            this.Hide();
            User_Interface UserAccess = new User_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            UserAccess.ShowDialog();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Events evemtsForm = new Events(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            evemtsForm.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create_Payment paymentform = new Create_Payment(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            paymentform.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Fund fund = new Fund(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
              fund.ShowDialog();
        }
        private void PopulateOrgDataGridView(string searchQuery)
        {

            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query with placeholders for dynamic input
                string query = @"SELECT u.user_id, 
                       CONCAT(u.last_name, ', ', u.first_name, ' ', COALESCE(u.middle_initial, ''), ' ', COALESCE(u.suffix, '')) AS full_name,
                       s.section_name,
                       si.gsuite_id,
                        (SELECT 
                        COUNT(t.transaction_id) 
                     FROM 
                        Transactions t
                     WHERE 
                        t.user_id = u.user_id
                        AND t.billing_id IS NOT NULL
                    ) AS number_of_paid_payments
                FROM UserProfile u
                JOIN StudentInfo si ON u.user_id = si.user_id
                JOIN Section s ON si.section_id = s.section_id
                JOIN 
                    Department d ON si.department_id = d.department_id
                JOIN 
                    Organization o ON d.department_id = o.department_id
                LEFT JOIN PaymentBilling pb ON u.user_id = pb.user_id
                LEFT JOIN Transactions t ON pb.billing_id = t.billing_id
                WHERE (u.user_id LIKE @searchQuery 
                        OR CONCAT(u.last_name, ' ', u.first_name) LIKE @searchQuery 
                        OR CONCAT(u.first_name, ' ', u.last_name) LIKE @searchQuery
                        OR CONCAT(u.last_name, ', ', u.first_name, ' ', COALESCE(u.middle_initial, ''), ' ', COALESCE(u.suffix, '')) LIKE @searchQuery
                        OR s.section_name LIKE @searchQuery
                        OR si.gsuite_id LIKE @searchQuery) -- Search by gsuite_id
                    AND si.department_id = @departmentId
                 AND o.organization_id = @organizationId
    
                GROUP BY u.user_id, full_name, s.section_name, si.gsuite_id
                ORDER BY u.last_name ASC, s.section_id ASC;




                    "; // Filter by department_id

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%"); // Add wildcard search
                cmd.Parameters.AddWithValue("@departmentId", CurrentDepartmentId); // Filter by department_id
                cmd.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

              

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Allow DataGridView to allow user edits
                dataGridView1.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating data grid view: " + ex.Message);
            }
            finally
            {
                // Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void PopulateAllOrgDataGridView()
        {

            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query with placeholders for dynamic input
                                string query = @"SELECT 
                        u.user_id, 
                        CONCAT(u.last_name, ', ', u.first_name, ' ', COALESCE(u.middle_initial, ''), ' ', COALESCE(u.suffix, '')) AS full_name,
                        s.section_name,
                        si.gsuite_id,
                        (SELECT 
                            COUNT(t.transaction_id) 
                         FROM 
                            Transactions t
                         WHERE 
                            t.user_id = u.user_id
                            AND t.billing_id IS NOT NULL
                        ) AS number_of_paid_payments
                    FROM 
                        UserProfile u
                    JOIN 
                        StudentInfo si ON u.user_id = si.user_id
                    JOIN 
                        Section s ON si.section_id = s.section_id
                    JOIN 
                        Course c ON s.course_id = c.course_id
                    JOIN 
                        Department d ON si.department_id = d.department_id
                    JOIN 
                        Organization o ON d.department_id = o.department_id
                    WHERE 
                        si.department_id = @departmentId
                        AND o.organization_id = @organizationId
                    GROUP BY 
                        u.user_id, 
                        full_name, 
                        s.section_name, 
                        si.gsuite_id
                    ORDER BY 
                        u.last_name ASC, 
                        s.section_id ASC;







                    "; // Filter by department_id

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@departmentId", CurrentDepartmentId); // Filter by department_id
                cmd.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

             
              

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Allow DataGridView to allow user edits
                dataGridView1.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating data grid view: " + ex.Message);
            }
            finally
            {
                // Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
        private void PopulateOrgDataGridView()
        {
            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query
                string query = @"SELECT o.position,
                                CONCAT(u.last_name, ', ', u.first_name, ' ', COALESCE(u.middle_initial, ''), ' ', COALESCE(u.suffix, '')) AS full_name,
                                si.gsuite_id,
                                s.section_name
                         FROM Officer o
                         JOIN UserProfile u ON o.user_id = u.user_id
                         JOIN StudentInfo si ON u.user_id = si.user_id
                         JOIN Section s ON si.section_id = s.section_id
                         WHERE o.organization_id = @organizationId";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Allow DataGridView to allow user edits
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating data grid view: " + ex.Message);
            }
            finally
            {
                // Close connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, EventArgs e)
        {
            string searchQuery = SearchTextbox.Text;
            PopulateOrgDataGridView(searchQuery);
        }

        private void ViewUnpaid_Click(object sender, EventArgs e)
        {
            PopulateAllOrgDataGridView();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            PopulateOrgDataGridView();
        }
    }
}
