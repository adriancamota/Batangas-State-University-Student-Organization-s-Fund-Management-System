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
    public partial class Creators_Admin_Interface : Form
    {
        MySqlConnection connection;
        
        public Creators_Admin_Interface()

        {
            InitializeComponent();
            InitializeConnection();
            PopulateDepartmentComboBox();
            InitializeTextBoxEvents();
            

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

     private void ShowAllUsers()
        {

        }
        

        private void InsertDepartment(string departmentName, string abbreviation)
        {
            try
            {
                connection.Open();

                string query = @"INSERT INTO Department (department_name, abbreviation)
                        VALUES (@DepartmentName, @Abbreviation)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@Abbreviation", abbreviation);
               

                cmd.ExecuteNonQuery();

                MessageBox.Show("Department inserted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting department: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void PopulateDepartmentComboBox()
        {
            try
            {
                connection.Open();

                DepartmentCombobox.Items.AddRange(new string[] { "Department" });
                DepartmentCombobox.SelectedIndex = 0;

                string query = "SELECT abbreviation FROM Department";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string abbreviation = reader.GetString("abbreviation");
                    DepartmentCombobox.Items.Add(abbreviation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating department combo box: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void AdminInterface_Load(object sender, EventArgs e)
        {
          


            DepartmentNameTextbox.TabStop = false;
            AbbrTextbox.TabStop = false;
            SearchTextbox.TabStop =false;

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

           
            DepartmentCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

           
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string departmentName = DepartmentNameTextbox.Text;
            string abbreviation = AbbrTextbox.Text;

            if (departmentName != "Department Name" && abbreviation != "Abbreviation")
            {

                InsertDepartment(departmentName, abbreviation);
            }
            else
            {
                MessageBox.Show("Error: Please Provide Details " );
            }
           
        }
        private void InitializeTextBoxEvents()
        {

            DepartmentNameTextbox.GotFocus += DepartmentNameTextbox_GotFocus;
            AbbrTextbox.GotFocus += AbbrTextbox_GotFocus;
            SearchTextbox.GotFocus += SearchTextbox_GotFocus;

            DepartmentNameTextbox.Leave += DepartmentNameTextbox_Leave;
            AbbrTextbox.Leave += AbbrTextbox_Leave;
            SearchTextbox.Leave += SearchTextbox_Leave;

        }

        private void SearchTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextbox.Text))
            {
                SearchTextbox.Text = "Name";
            }
        }

        private void SearchTextbox_GotFocus(object sender, EventArgs e)
        {
            if (SearchTextbox.Text == "Name")
            {
                SearchTextbox.Text = string.Empty;
            }
        }

        private void AbbrTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AbbrTextbox.Text))
            {
                AbbrTextbox.Text = "Abbreviation";
            }
        }

        private void AbbrTextbox_GotFocus(object sender, EventArgs e)
        {
            if (AbbrTextbox.Text == "Abbreviation")
            {
                AbbrTextbox.Text = string.Empty;
            }
        }

        private void DepartmentNameTextbox_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(DepartmentNameTextbox.Text))
            {
                DepartmentNameTextbox.Text = "Department Name";
            }
        }

        private void DepartmentNameTextbox_GotFocus(object sender, EventArgs e)
        {
            if (DepartmentNameTextbox.Text == "Department Name")
            {
                DepartmentNameTextbox.Text = string.Empty;
            }
        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
        }

       

        private void Button_Click(object sender, EventArgs e)
        {
            string searchQuery = SearchTextbox.Text;
            PopulateDepartmentDataGridView(searchQuery);
        }   

        private void PopulateDepartmentDataGridView(string searchQuery)
        {
            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query with placeholders for dynamic input
                string query = @"
            SELECT u.user_id,
                   CONCAT_WS(' ', u.last_name, u.first_name, IFNULL(CONCAT(SUBSTRING(u.middle_initial, 1, 1), '.'), ''), u.suffix) AS full_name,
                   d.abbreviation AS department,
                   s.section_name,
                   u.department_access
            FROM UserProfile u
            LEFT JOIN StudentInfo si ON u.user_id = si.user_id
            LEFT JOIN Course c ON si.course_id = c.course_id
            LEFT JOIN Section s ON si.section_id = s.section_id
            LEFT JOIN Department d ON si.department_id = d.department_id";

                // Add WHERE clause conditionally based on DepartmentCombobox selection
                if (DepartmentCombobox.SelectedItem != null && DepartmentCombobox.SelectedItem.ToString() != "Department")
                {
                    query += @"
                WHERE (d.abbreviation = @department)
                AND (u.user_id LIKE @searchQuery OR u.last_name LIKE @searchQuery OR u.first_name LIKE @searchQuery)";
                }
                else
                {
                    query += @"
                WHERE (u.user_id LIKE @searchQuery OR u.last_name LIKE @searchQuery OR u.first_name LIKE @searchQuery)";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%"); // Add wildcard search

                // Add department parameter if a specific department is selected
                if (DepartmentCombobox.SelectedItem != null && DepartmentCombobox.SelectedItem.ToString() != "Department")
                {
                    cmd.Parameters.AddWithValue("@department", DepartmentCombobox.SelectedItem.ToString());
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Make the department_access column editable
                dataTable.Columns["department_access"].ReadOnly = false;

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




        private void DepartmentCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepartment = DepartmentCombobox.SelectedItem.ToString();
            // Check if the selected department is "Department", and if so, do nothing
            if (selectedDepartment != "Department")
            {
                SelectUsersByDepartment(selectedDepartment);
            }
        }

        private void SelectUsersByDepartment(string department)
        {
            try
            {
                connection.Open();

                // Construct the SQL query to select users based on the selected department
                string query = @"
            SELECT u.user_id,
                   CONCAT_WS(' ', u.last_name, u.first_name, IFNULL(CONCAT(SUBSTRING(u.middle_initial, 1, 1), '.'), ''), u.suffix) AS full_name,
                   d.abbreviation AS department,
                   s.section_name,
                   u.department_access
            FROM UserProfile u
            LEFT JOIN StudentInfo si ON u.user_id = si.user_id
            LEFT JOIN Course c ON si.course_id = c.course_id
            LEFT JOIN Section s ON si.section_id = s.section_id
            LEFT JOIN Department d ON si.department_id = d.department_id
            WHERE d.abbreviation = @department";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@department", department);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Make the department_access column editable
                dataTable.Columns["department_access"].ReadOnly = false;

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Allow DataGridView to allow user edits
                dataGridView1.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting users by department: " + ex.Message);
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

        private void DepartmentNameTextbox_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveChangesButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Iterate through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the last row, which is the new row for adding new records
                    if (!row.IsNewRow)
                    {
                        // Extract the values from DataGridView
                        int userId = Convert.ToInt32(row.Cells["user_id"].Value);
                        bool departmentAccess = Convert.ToBoolean(row.Cells["department_access"].Value);

                        // Update the corresponding record in the database
                        string updateQuery = @"UPDATE UserProfile 
                                       SET department_access = @departmentAccess 
                                       WHERE user_id = @userId";

                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                        updateCmd.Parameters.AddWithValue("@departmentAccess", departmentAccess);
                        updateCmd.Parameters.AddWithValue("@userId", userId);

                        updateCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Department Authorization changed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                connection.Open();

                // Iterate through selected rows in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Extract the user_id from the selected row
                    int userId = Convert.ToInt32(row.Cells["user_id"].Value);

                    // Construct the SQL delete query
                    string deleteQuery = "DELETE FROM UserProfile WHERE user_id = @userId";

                    // Create and execute the delete command
                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                    deleteCmd.Parameters.AddWithValue("@userId", userId);
                    deleteCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Selected records deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting records: " + ex.Message);
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

        private void ShowAllUsersButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query to select all users
                string query = @"
            SELECT u.user_id,
                   CONCAT_WS(' ', u.last_name, u.first_name, IFNULL(CONCAT(SUBSTRING(u.middle_initial, 1, 1), '.'), ''), u.suffix) AS full_name,
                   
                   d.abbreviation AS department,
                   s.section_name, u.department_access
            FROM UserProfile u
            LEFT JOIN StudentInfo si ON u.user_id = si.user_id
            LEFT JOIN Course c ON si.course_id = c.course_id
            LEFT JOIN Section s ON si.section_id = s.section_id
            LEFT JOIN Department d ON si.department_id = d.department_id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Make the department_access column editable
                dataTable.Columns["department_access"].ReadOnly = false;

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Allow DataGridView to allow user edits
                dataGridView1.ReadOnly = false;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving all users: " + ex.Message);
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

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();   
            Developers developers = new Developers();
            developers.ShowDialog();
        }
    }
}
