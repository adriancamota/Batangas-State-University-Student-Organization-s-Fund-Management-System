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
    public partial class School_Information_Update : Form


    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public School_Information_Update(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            InitializedTextbox();
            PopulateDepartmentComboBox();
            PinTextbox();

            Pin.Enabled = false;
           

            CourseCombobox.Items.AddRange(new string[] { "Course" });
            CourseCombobox.SelectedIndex = 0;

            SectionCombobox.Items.AddRange(new string[] { "Section" });
            SectionCombobox.SelectedIndex = 0;

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            Pin.UseSystemPasswordChar = false;
        }
        private void PinTextbox()
        {
            Pin.Text = "Create 4-digit pin";
        }
       
        private void InitializedTextbox()
        {
            GsuiteTextbox.GotFocus += GsuiteTextbox_GotFocus;
            GsuiteTextbox.Leave += GsuiteTextbox_Leave;

        }

        private void GsuiteTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GsuiteTextbox.Text))
            {
                GsuiteTextbox.Text = "Gsuite Id";
            }
        }

        private void GsuiteTextbox_GotFocus(object sender, EventArgs e)
        {
            if (GsuiteTextbox.Text == "Gsuite Id")
            {
                GsuiteTextbox.Text = string.Empty;
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
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Interface UserAccess = new User_Interface(CurrentUserId,CurrentDepartmentId, CurrentOrganizationId);
            UserAccess.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateDepartmentComboBox()
        {
            
                try
                {
                    connection.Open();

                    DepartmentCombobox.Items.AddRange(new string[] { "Department" });
                    DepartmentCombobox.SelectedIndex = 0;

                    string query = "SELECT CONCAT(department_name, ' (', abbreviation, ')') AS department_concat FROM Department";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string departmentItem = reader.GetString("department_concat");
                        DepartmentCombobox.Items.Add(departmentItem);
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


        private void School_Information_Update_Load(object sender, EventArgs e)
        {
            Pin.TabStop = false;
           GsuiteTextbox.TabStop = false;
         

            int redValue = 125;
            int greenValue = 137;
            int blueValue = 149;


            DepartmentCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);
            CourseCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);
            SectionCombobox.ForeColor = Color.FromArgb(redValue, greenValue, blueValue);

            DepartmentCombobox.Font = new Font(DepartmentCombobox.Font.FontFamily, 9);
            CourseCombobox.Font = new Font(CourseCombobox.Font.FontFamily, 9);
            SectionCombobox.Font = new Font(SectionCombobox.Font.FontFamily, 9);

           

            // Wire up the delete button click event to the deleteButton_Click method
            deleteButton.Click += deleteButton_Click_1;
        }

        private void DepartmentCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected item is not equal to "Department"
            if (DepartmentCombobox.SelectedItem.ToString() != "Department")
            {
                // Clear the existing items in the course ComboBox
                CourseCombobox.Items.Clear();

                try
                {
                    // Open the database connection
                    connection.Open();

                    CourseCombobox.Items.AddRange(new string[] { "Course" });
                    CourseCombobox.SelectedIndex = 0;

                    // Get the selected item from the department ComboBox
                    string selectedDepartmentItem = DepartmentCombobox.SelectedItem.ToString();

                    // Split the selected item to extract the department abbreviation
                    string[] splitDepartmentItem = selectedDepartmentItem.Split(' ');
                    string departmentAbbreviation = splitDepartmentItem.Last().Trim('(', ')');

                    // Query to select courses based on the selected department abbreviation
                    string query = "SELECT course_name FROM Course " +
                                   "INNER JOIN Department ON Course.department_id = Department.department_id " +
                                   "WHERE Department.abbreviation = @DepartmentAbbreviation";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Bind the parameter
                    cmd.Parameters.AddWithValue("@DepartmentAbbreviation", departmentAbbreviation);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string courseName = reader.GetString("course_name");
                        CourseCombobox.Items.Add(courseName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error populating course ComboBox: " + ex.Message);
                }
                finally
                {
                    // Close the database connection
                    connection.Close();
                }
            }
        }

        private void CourseCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                // Check if the selected item is not equal to "Course"
                if (CourseCombobox.SelectedItem.ToString() != "Course")
                {
                    // Clear the existing items in the section ComboBox
                    SectionCombobox.Items.Clear();

                    try
                    {
                        // Open the database connection
                        connection.Open();

                        SectionCombobox.Items.AddRange(new string[] { "Section" });
                        SectionCombobox.SelectedIndex = 0;

                        // Get the selected course from the course ComboBox
                        string selectedCourse = CourseCombobox.SelectedItem.ToString();

                        // Query to select sections based on the selected course
                        string query = "SELECT section_name FROM Section " +
                                       "INNER JOIN Course ON Section.course_id = Course.course_id " +
                                       "WHERE Course.course_name = @CourseName";

                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        // Bind the parameter
                        cmd.Parameters.AddWithValue("@CourseName", selectedCourse);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string sectionName = reader.GetString("section_name");
                            SectionCombobox.Items.Add(sectionName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error populating section ComboBox: " + ex.Message);
                    }
                    finally
                    {
                        // Close the database connection
                        connection.Close();
                    }
                }
            

        }

      
      
        private void Pin_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void pin1_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "1";
            }
            else
            {
                Pin.Text += "1";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin2_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "2";
            }
            else
            {
                Pin.Text += "2";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin3_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "3";
            }
            else
            {
                Pin.Text += "3";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin4_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "4";
            }
            else
            {
                Pin.Text += "4";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin5_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "5";
            }
            else
            {
                Pin.Text += "5";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin6_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "6";
            }
            else
            {
                Pin.Text += "6";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin7_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "7";
            }
            else
            {
                Pin.Text += "7";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin8_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "8";
            }
            else
            {
                Pin.Text += "8";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin9_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "9";
            }
            else
            {
                Pin.Text += "9";
            }
            Pin.UseSystemPasswordChar = true;
        }

        private void pin0_Click(object sender, EventArgs e)
        {
            if (Pin.Text == "Create 4-digit pin")
            {
                Pin.Text = "0";
            }
            else
            {
                Pin.Text += "0";
            }
            Pin.UseSystemPasswordChar = true;
        }

       

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Ensure PIN is exactly 4 digits
            if (Pin.Text.Length != 4 || !Pin.Text.All(char.IsDigit))
            {
                MessageBox.Show("PIN must be exactly 4 digits.");
                return;
            }

            // Retrieve selected department, course, and section
            string selectedDepartment = DepartmentCombobox.SelectedItem.ToString();
            string selectedCourse = CourseCombobox.SelectedItem.ToString();
            string selectedSection = SectionCombobox.SelectedItem.ToString();

            // Extract department abbreviation from the selected department
            string departmentAbbreviation = selectedDepartment.Split(' ').Last().Trim('(', ')');

            try
            {
                connection.Open();

                // Retrieve department_id based on abbreviation
                string departmentQuery = "SELECT department_id FROM Department WHERE abbreviation = @Abbreviation";
                MySqlCommand departmentCmd = new MySqlCommand(departmentQuery, connection);
                departmentCmd.Parameters.AddWithValue("@Abbreviation", departmentAbbreviation);
                int departmentId = Convert.ToInt32(departmentCmd.ExecuteScalar());

                // Retrieve course_id based on selected course
                string courseQuery = "SELECT course_id FROM Course WHERE course_name = @CourseName";
                MySqlCommand courseCmd = new MySqlCommand(courseQuery, connection);
                courseCmd.Parameters.AddWithValue("@CourseName", selectedCourse);
                int courseId = Convert.ToInt32(courseCmd.ExecuteScalar());

                // Retrieve section_id based on selected section
                string sectionQuery = "SELECT section_id FROM Section WHERE section_name = @SectionName";
                MySqlCommand sectionCmd = new MySqlCommand(sectionQuery, connection);
                sectionCmd.Parameters.AddWithValue("@SectionName", selectedSection);
                int sectionId = Convert.ToInt32(sectionCmd.ExecuteScalar());

                // Insert into StudentInfo table
                // Check if a record already exists for the user
                string checkQuery = "SELECT COUNT(*) FROM StudentInfo WHERE user_id = @UserId";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@UserId", CurrentUserId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0) // Record exists, perform update
                {
                    // Update the existing record in the StudentInfo table
                    string updateQuery = "UPDATE StudentInfo SET gsuite_id = @GsuiteId, department_id = @DepartmentId, " +
                                         "course_id = @CourseId, section_id = @SectionId WHERE user_id = @UserId";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@GsuiteId", GsuiteTextbox.Text);
                    updateCmd.Parameters.AddWithValue("@DepartmentId", departmentId); // Use the retrieved departmentId
                    updateCmd.Parameters.AddWithValue("@CourseId", courseId); // Use the retrieved courseId
                    updateCmd.Parameters.AddWithValue("@SectionId", sectionId); // Use the retrieved sectionId
                    updateCmd.Parameters.AddWithValue("@UserId", CurrentUserId);
                    updateCmd.ExecuteNonQuery(); // Execute the update command
                }
                else // Record doesn't exist, perform insert
                {
                    // Insert into StudentInfo table
                    string insertQuery = "INSERT INTO StudentInfo (user_id, gsuite_id, department_id, course_id, section_id) " +
                                         "VALUES (@UserId, @GsuiteId, @DepartmentId, @CourseId, @SectionId)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@UserId", CurrentUserId);
                    insertCmd.Parameters.AddWithValue("@GsuiteId", GsuiteTextbox.Text);
                    insertCmd.Parameters.AddWithValue("@DepartmentId", departmentId); // Use the retrieved departmentId
                    insertCmd.Parameters.AddWithValue("@CourseId", courseId); // Use the retrieved courseId
                    insertCmd.Parameters.AddWithValue("@SectionId", sectionId); // Use the retrieved sectionId
                    insertCmd.ExecuteNonQuery(); // Execute the insert command
                }

                // Optionally update UserProfile table with PIN
                string userProfileQuery = "UPDATE UserProfile SET pin = @Pin WHERE user_id = @UserId";
                MySqlCommand userProfileCmd = new MySqlCommand(userProfileQuery, connection);
                userProfileCmd.Parameters.AddWithValue("@Pin", Pin.Text);
                userProfileCmd.Parameters.AddWithValue("@UserId", CurrentUserId);
                userProfileCmd.ExecuteNonQuery();

                MessageBox.Show("School information updated successfully.");


                this.Hide();
                LogInForm form = new LogInForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating school information: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (Pin.Text.Length > 0)
            {
                PinTextbox();
            }

            Pin.UseSystemPasswordChar = false;



        }
    }
}
