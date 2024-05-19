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
    public partial class Department_Admin_Interface : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Department_Admin_Interface(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            InitializedTextBoxEvents();
            InitializeOrganizationComboBox();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

            PositionTextBox.Text = "Position";

            OrganizationCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
            OrganizationCombobox.Font = new Font(OrganizationCombobox.Font.FontFamily, 9);

            OrganizationCombobox2.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
            OrganizationCombobox2.Font = new Font(OrganizationCombobox2.Font.FontFamily, 9);

            DeleteOrganizationButton.Visible = false;
        }


        private void PopulateOrganizationDataGridView(string abbreviation)
        {
            try
            {
                connection.Open();

                string query = "SELECT organization_name,abbreviation,created_timestamp FROM Organization WHERE abbreviation = @abbreviation";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@abbreviation", abbreviation);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating DataGridView: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void InitializeOrganizationComboBox()
        {

            OrganizationCombobox.Items.AddRange(new string[] { "Organization" });
            OrganizationCombobox.SelectedIndex = 0;
            OrganizationCombobox2.Items.AddRange(new string[] { "Organization" });
            OrganizationCombobox2.SelectedIndex = 0;
            try
            {
                connection.Open();

                string query = "SELECT Abbreviation FROM Organization";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                

                while (reader.Read())
                {
                    OrganizationCombobox2.Items.Add(reader["Abbreviation"].ToString());
                    OrganizationCombobox.Items.Add(reader["Abbreviation"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing organization ComboBox: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void InitializedTextBoxEvents()
        {

            OrganizationTextbox.GotFocus += OrganizationTextbox_GotFocus;
            AbbrTextbox.GotFocus += AbbrTextbox_GotFocus;
            SearchTextbox.GotFocus += SearchTextbox_GotFocus;

            OrganizationTextbox.Leave += OrganizationTextbox_Leave;
            AbbrTextbox.Leave += AbbrTextbox_Leave;
            SearchTextbox.Leave += SearchTextbox_Leave;

            PositionTextBox.GotFocus += PositionTextBox_GotFocus;
            PositionTextBox.Leave += PositionTextBox_Leave;
        }

        private void PositionTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PositionTextBox.Text))
            {
                PositionTextBox.Text = "Position";
            }
        }

        private void PositionTextBox_GotFocus(object sender, EventArgs e)
        {
            if (PositionTextBox.Text == "Position")
            {
                PositionTextBox.Text = string.Empty;
            }
        }

        private void SearchTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AbbrTextbox.Text))
            {
                AbbrTextbox.Text = "Name";
            }
        }

        private void SearchTextbox_GotFocus(object sender, EventArgs e)
        {
            if (AbbrTextbox.Text == "Name")
            {
                AbbrTextbox.Text = string.Empty;
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
        private void OrganizationTextbox_GotFocus(object sender, EventArgs e)
        {
            if (OrganizationTextbox.Text == "Organization Name")
            {
                OrganizationTextbox.Text = string.Empty;
            }

        }

        private void OrganizationTextbox_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(OrganizationTextbox.Text))
            {
                OrganizationTextbox.Text = "Organization Name";
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

        private void InsertOrganization(string organizationName, string abbreviation)
        {
            try
            {
                connection.Open();

                string query = @"INSERT INTO Organization (organization_name, abbreviation,department_id)
                        VALUES (@OrganizationName, @Abbreviation, @departmentId)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrganizationName", organizationName);
                cmd.Parameters.AddWithValue("@Abbreviation", abbreviation);
                cmd.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Organization inserted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting Organization: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string organizationName = OrganizationTextbox.Text;
            string abbreviation = AbbrTextbox.Text;
           


            if (organizationName != "Organization Name" && abbreviation != "Abbreviation")
            {
                InsertOrganization(organizationName, abbreviation);

                RefreshOrganizationComboBox();
                RefreshOrganizationDataGridView();
            }
            else
            {
                MessageBox.Show("Error: Please Provide Details ");
            }
        }
        private void PopulateOrgDataGridView(string searchQuery)
        {

            try
            {
                // Open connection
                connection.Open();

             
                string query = @"SELECT u.user_id,
       s.gsuite_id,
       CONCAT(u.last_name, ', ', u.first_name, ' ', COALESCE(u.middle_initial, ''), ' ', COALESCE(u.suffix, '')) AS full_name,
       se.section_name,
       o.position AS position,
       org.abbreviation AS organization_abbreviation,
       u.organization_access
FROM UserProfile u
JOIN StudentInfo s ON u.user_id = s.user_id
JOIN Section se ON s.section_id = se.section_id
LEFT JOIN Officer o ON u.user_id = o.user_id AND s.department_id = o.department_id
LEFT JOIN Organization org ON o.organization_id = org.organization_id
WHERE (s.gsuite_id LIKE @searchQuery 
       OR u.last_name LIKE @searchQuery 
       OR u.first_name LIKE @searchQuery)
      AND s.department_id = @departmentId

ORDER BY position IS NOT NULL DESC, org.abbreviation





                    "; // Filter by department_id

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%"); // Add wildcard search
                cmd.Parameters.AddWithValue("@departmentId", CurrentDepartmentId); // Filter by department_id

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Make the organization_access column editable
                dataTable.Columns["organization_access"].ReadOnly = false;

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

              
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


        private void Department_Admin_Interface_Load(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, EventArgs e)
        {

            OrganizationCombobox.SelectedIndex = 0;
            OrganizationCombobox2.SelectedIndex = 0;
            string searchQuery = SearchTextbox.Text;
            PopulateOrgDataGridView(searchQuery);
            DeleteOrganizationButton.Visible = false;
            DeleteOfficerButton.Visible = false;
            DeleteOfficerButton.Visible = true;

           


        }

        private void SaveChangesButton_Click_1(object sender, EventArgs e)
        {
    
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Interface UserAccess = new User_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            UserAccess.ShowDialog();
        }
            private void ExitButton_Click(object sender, EventArgs e)
            {
                this.Close();

            }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Courses_Sections CourseSection = new Add_Courses_Sections(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            CourseSection.ShowDialog();
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrganizationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOrganizationName = OrganizationCombobox.SelectedItem.ToString();
            PopulateOrganizationDataGridView(selectedOrganizationName);
            DeleteOrganizationButton.Visible = true;
            DeleteOfficerButton.Visible = false;
        }

       

     

        // Method to delete the organization from the database
        private void DeleteOrganization(string abbreviation)
        {
            try
            {
                connection.Open();

                // Construct the delete query
                string deleteQuery = "DELETE FROM Organization WHERE abbreviation = @abbreviation";

                MySqlCommand cmd = new MySqlCommand(deleteQuery, connection);
                cmd.Parameters.AddWithValue("@abbreviation", abbreviation);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Organization deleted successfully.");

                    

                }
                else
                {
                    MessageBox.Show("Organization not found or unable to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting organization: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void RefreshOrganizationComboBox()
        {
            OrganizationCombobox.Items.Clear(); // Clear existing items
            InitializeOrganizationComboBox(); // Repopulate the ComboBox
        }

        private void RefreshOrganizationDataGridView()
        {
            // If needed, you can also add logic here to maintain the current selection or scroll position
            string selectedOrganizationName = OrganizationCombobox.SelectedItem?.ToString();
            PopulateOrganizationDataGridView(selectedOrganizationName); // Repopulate the DataGridView
        }

        private void DeleteOrgButton_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Check if the selected row does not match the result of the SQL query
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string organizationAbbreviation = selectedRow.Cells["organization_abbreviation"].Value.ToString();

                // Add additional conditions based on the SQL query
                string gsuiteId = selectedRow.Cells["gsuite_id"].Value.ToString();
                string sectionName = selectedRow.Cells["section_name"].Value.ToString();
                string position = selectedRow.Cells["position"].Value.ToString();
                string organizationAccess = selectedRow.Cells["organization_access"].Value.ToString();

                // Check if any of the conditions based on the SQL query are met
                if (!string.IsNullOrEmpty(gsuiteId) || !string.IsNullOrEmpty(sectionName) ||
                    !string.IsNullOrEmpty(position) || !string.IsNullOrEmpty(organizationAccess))
                {
                    MessageBox.Show("Please select Organization at the Combobox below to delete.");
                    return;
                }

                // Confirm with the user before deleting
                DialogResult result = MessageBox.Show("Are you sure you want to delete the organization with abbreviation: " + organizationAbbreviation + "?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Proceed with deletion
                    // Call a method to delete the organization from the database
                    DeleteOrganization(organizationAbbreviation);

                    // Refresh organization-related data
                    RefreshOrganizationComboBox();
                    RefreshOrganizationDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
        private void InsertOfficer(int userId, string position, int departmentId, int organizationId)
        {
            try
            {
                connection.Open();
                string query = @"INSERT INTO Officer (position, user_id, department_id, organization_id)
                        VALUES (@Position, @UserId, @DepartmentId, @OrganizationId)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                cmd.Parameters.AddWithValue("@OrganizationId", organizationId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Officer inserted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting Officer: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        // Method to get organization_id based on abbreviation
        private int GetOrganizationId(string abbreviation)
        {
            int organizationId = -1;
            try
            {
                connection.Open();
                string query = "SELECT organization_id FROM Organization WHERE abbreviation = @abbreviation";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@abbreviation", abbreviation);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    organizationId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting organization id: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return organizationId;
        }

        // Method to update organization_access in UserProfile table
        private void UpdateOrganizationAccess(int userId, bool organizationAccess)
        {
            try
            {
                connection.Open();
                string query = "UPDATE UserProfile SET organization_access = @OrganizationAccess WHERE user_id = @UserId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrganizationAccess", organizationAccess);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Organization access updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating organization access: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected organization abbreviation from ComboBox2
                string selectedOrganizationAbbreviation = OrganizationCombobox2.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedOrganizationAbbreviation) || selectedOrganizationAbbreviation == "Organization")
                {
                    MessageBox.Show("Please select an organization from the ComboBox.");
                    return;
                }

                // Get the selected row in the DataGridView
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the value of the user ID from the selected row
                int userId = Convert.ToInt32(selectedRow.Cells["user_id"].Value);

                // Get the values from the selected row
                string position = PositionTextBox.Text; // Assuming PositionTextBox is the name of your TextBox for position
                bool organizationAccess = true; // Set organization access to true

                // Get CurrentDepartmentId from the class property
                int departmentId = CurrentDepartmentId;

                // Get the organization_id based on the selected abbreviation
                int organizationId = GetOrganizationId(selectedOrganizationAbbreviation);

                // Insert data into Officer table
                InsertOfficer(userId, position, departmentId, organizationId);

                // Update organization_access in UserProfile table
                UpdateOrganizationAccess(userId, organizationAccess);
                PopulateOrgDataGridView(SearchTextbox.Text);


            }
            else
            {
                MessageBox.Show("Please select a row in the DataGridView.");
            }
        }

        private void DeleteOfficer(int userId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Officer WHERE user_id = @UserId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Officer deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Officer: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateOrganizationAccessFalse(int userId, bool organizationAccess)
        {
            try
            {
                connection.Open();
                string query = "UPDATE UserProfile SET organization_access = @OrganizationAccess WHERE user_id = @UserId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrganizationAccess", organizationAccess);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Organization access updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating organization access: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                    // Proceed with officer deletion
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int userId = Convert.ToInt32(selectedRow.Cells["user_id"].Value);

                    // Delete entry in the Officer table
                    DeleteOfficer(userId);

                    // Update organization access to false
                    UpdateOrganizationAccessFalse(userId, false);

                    // Remove the selected row from the DataGridView
                    dataGridView1.Rows.Remove(selectedRow);
                
              
            }
            else
            {
                MessageBox.Show("Please select a row in the DataGridView.");
            }
        }
    }
}

