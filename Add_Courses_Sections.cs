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
using System.Management;

namespace Funds_Management_System
{
    public partial class Add_Courses_Sections : Form

    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Add_Courses_Sections(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            InitializeDataGrid();
            ViewAllCoursesButton.Click += ViewAllCoursesButton_Click;


            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            CourseCombobox.Items.AddRange(new string[] { "Course" });
            CourseCombobox.SelectedIndex = 0;

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;

            CourseCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            // Set the font size to 9
            CourseCombobox.Font = new Font(CourseCombobox.Font.FontFamily, 9);


            CourseName.GotFocus += CourseName_GotFocus;
            CourseName.Leave += CourseName_Leave;
            sectionname.GotFocus += Sectionname_GotFocus;
            sectionname.Leave += Sectionname_Leave;
            Abbreviation.GotFocus += Abbreviation_GotFocus;
            Abbreviation.Leave += Abbreviation_Leave;
        }

     
        private void Abbreviation_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Abbreviation.Text))
            {
                Abbreviation.Text = "Abbreviation";
            }
        }

        private void Abbreviation_GotFocus(object sender, EventArgs e)
        {
            if (Abbreviation.Text == "Abbreviation")
            {
                Abbreviation.Text = string.Empty;
            }
        }

        private void Sectionname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sectionname.Text))
            {
                sectionname.Text = "Section Name";
            }
        }
        private void Sectionname_GotFocus(object sender, EventArgs e)
        {
            if (sectionname.Text == "Section Name")
            {
                sectionname.Text = string.Empty;
            }
        }

        private void CourseName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CourseName.Text))
            {
                CourseName.Text = "Course Name";
            }
        }

        private void CourseName_GotFocus(object sender, EventArgs e)
        {
            if (CourseName.Text == "Course Name")
            {
                CourseName.Text = string.Empty;
            }
        }

        private void InitializeDataGrid()
        {

            // Set the DataGridViewAutoSizeColumnsMode property to fill to automatically adjust column widths.
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Loop through each column and set the AutoSizeMode to AllCells except for the last column, which should fill.
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Index != dataGridView1.Columns.Count - 1)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

        }
        private void LoadAbbreviations()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT abbreviation FROM course WHERE department_id = @departmentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string abbreviation = reader.GetString("abbreviation");
                        CourseCombobox.Items.Add(abbreviation); // Add abbreviation to the ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading course abbreviations: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }



        private void InsertCourse(string courseName, string abbreviation)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "INSERT INTO Course (course_name, department_id, abbreviation) " +
                               "VALUES (@courseName, @departmentId, @abbreviation)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                command.Parameters.AddWithValue("@abbreviation", abbreviation);

                command.ExecuteNonQuery();

                MessageBox.Show("Course added successfully!");

                CourseCombobox.Items.Clear();
                LoadAbbreviations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding course: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        private void InsertSection(string sectionName, int courseId)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "INSERT INTO Section (section_name, course_id) " +
                               "VALUES (@sectionName, @courseId)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@sectionName", sectionName);
                command.Parameters.AddWithValue("@courseId", courseId);

                command.ExecuteNonQuery();

                MessageBox.Show("Section added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding section: " + ex.Message);
            }
            finally
            {
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
        private void Add_Courses_Sections_Load(object sender, EventArgs e)
        {
            LoadAbbreviations();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Department_Admin_Interface DeptAccess = new Department_Admin_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
            DeptAccess.ShowDialog();
        }

        private void CourseCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddCourseButton_Click(object sender, EventArgs e)
        {
            string courseName = CourseName.Text.Trim();
            string abbreviation = Abbreviation.Text.Trim();

            // Check if the entered data is the same as the placeholder text
            if (courseName == "Course Name" || abbreviation == "Abbreviation")
            {
                MessageBox.Show("Please enter valid course name and abbreviation.");
                return;
            }

            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(abbreviation))
            {
                MessageBox.Show("Please enter both course name and abbreviation.");
                return;
            }

            InsertCourse(courseName, abbreviation);
        }

        private void AddSectionButton_Click(object sender, EventArgs e)
        {
            string sectionName = sectionname.Text.Trim();
            string selectedAbbreviation = CourseCombobox.SelectedItem?.ToString();

            // Check if the selected item is "Course" and ignore the action
            if (selectedAbbreviation == "Course")
            {
                // Optionally, you can show a message to inform the user
                MessageBox.Show("Please select a valid course abbreviation.");
                return;
            }

            // Continue with the rest of the logic
            if (string.IsNullOrEmpty(sectionName))
            {
                MessageBox.Show("Please enter section name.");
                return;
            }

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Retrieve the course ID corresponding to the selected abbreviation
                string courseIdQuery = "SELECT course_id FROM Course WHERE abbreviation = @abbreviation";
                MySqlCommand courseIdCommand = new MySqlCommand(courseIdQuery, connection);
                courseIdCommand.Parameters.AddWithValue("@abbreviation", selectedAbbreviation);

                int courseId = Convert.ToInt32(courseIdCommand.ExecuteScalar());

                InsertSection(sectionName, courseId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding section: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
        private void RetrieveCourseAndSectionDetails()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                string searchQuery = @"SELECT CONCAT(c.course_name, ' (', c.abbreviation, ')') AS CourseAndSections,
                            s.section_name,
                            c.course_id,
                            s.section_id 
                FROM Course c
                INNER JOIN Section s ON c.course_id = s.course_id
                WHERE c.department_id = @departmentId";




                MySqlCommand command = new MySqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);

                DataTable table = new DataTable();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                }

                // Bind the DataTable to dataGridView1
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving course and section details: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }


        private void Button_Click(object sender, EventArgs e)
        {
            RetrieveCourseAndSectionDetails();
        }

     

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Loop through each selected row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Check if the row is not a new row
                    if (!row.IsNewRow)
                    {
                        int sectionId = Convert.ToInt32(row.Cells["section_id"].Value);

                        // Delete the section record from the Section table
                        string deleteSectionQuery = "DELETE FROM Section WHERE section_id = @sectionId";
                        MySqlCommand deleteSectionCommand = new MySqlCommand(deleteSectionQuery, connection);
                        deleteSectionCommand.Parameters.AddWithValue("@sectionId", sectionId);
                        deleteSectionCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Selected sections deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting sections: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            // Refresh DataGridView after deletion
            RetrieveCourseAndSectionDetails();
        }

        private void ViewAllCoursesButton_Click(object sender, EventArgs e)
        {

            try
            {
                // Open connection
                connection.Open();

                // Construct the SQL query to select all courses
                string query = @"
            SELECT *
            FROM Course";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Clear existing data in DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;

                // Make DataGridView read-only
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving all courses: " + ex.Message);
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

        private void DeleteCourse_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Check if any row is selected in the DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Retrieve the course ID of the selected course
                    int courseId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["course_id"].Value);

                    // Delete the course record from the Course table
                    string deleteCourseQuery = "DELETE FROM Course WHERE course_id = @courseId";
                    MySqlCommand deleteCourseCommand = new MySqlCommand(deleteCourseQuery, connection);
                    deleteCourseCommand.Parameters.AddWithValue("@courseId", courseId);
                    deleteCourseCommand.ExecuteNonQuery();

                    MessageBox.Show("Course deleted successfully!");

                    // Refresh DataGridView after deletion
                    RetrieveCourseAndSectionDetails();
                }
                else
                {
                    MessageBox.Show("Please select a course to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting course: " + ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}

