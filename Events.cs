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
    public partial class Events : Form

    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }

        public Events(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            Event_textbox.GotFocus += Event_textbox_GotFocus;
            Event_textbox.Leave += Event_textbox_Leave;

        }



       

        private void Event_textbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Event_textbox.Text))
            {
                Event_textbox.Text = "Event Name";
            }
        }

        private void Event_textbox_GotFocus(object sender, EventArgs e)
        {
            if (Event_textbox.Text == "Event Name")
            {
                Event_textbox.Text = string.Empty;
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

        private void AddEvent_Click(object sender, EventArgs e)
        {
            string eventName = Event_textbox.Text;
           

            if (string.IsNullOrEmpty(eventName) || eventName.Equals("Event Name"))
            {
                MessageBox.Show("Please enter a valid event name.");
                return; // exit the method
            }

            try
            {
                connection.Open();
                string query = "INSERT INTO Event (event_name, event_timestamp, department_id, organization_id) " +
                               "VALUES (@eventName, CURRENT_TIMESTAMP, @departmentId, @organizationId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventName", eventName);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                command.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);
                command.ExecuteNonQuery();
                MessageBox.Show("Event added successfully.");

                this.Hide();
                Organization_Admin_Interface OrgAdmin = new Organization_Admin_Interface(CurrentUserId, CurrentDepartmentId, CurrentOrganizationId);
                OrgAdmin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            
        }

        private void Events_Load(object sender, EventArgs e)
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
    }
}
