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
using System.Drawing.Printing;

namespace Funds_Management_System
{
    public partial class withdrawreceipt : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public withdrawreceipt(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            LoadLatestTransactions();
            LoadLatestReceipt();

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
        private void LoadLatestReceipt()
        {


            try
            {
                string query = @"SELECT r.receipt_id, si.gsuite_id, t.transaction_name, r.payment_title, r.amount, r.notes, o.organization_name, COALESCE(e.event_name, 'No Event') AS event_name, t.paymentMethod, r.receipt_type, r.receipt_timestamp 
                        FROM Receipts r 
                    JOIN UserProfile u ON r.user_id = u.user_id 
                    JOIN StudentInfo si ON u.user_id = si.user_id 
                    LEFT JOIN Receipts_Transactions rt ON r.receipt_id = rt.receipt_id 
                    LEFT JOIN Transactions t ON rt.transaction_id = t.transaction_id 
                    LEFT JOIN Organization o ON r.organization_id = o.organization_id 
                    LEFT JOIN Event e ON t.event_id = e.event_id 
                    WHERE r.user_id = @userId 
                    ORDER BY r.receipt_timestamp DESC LIMIT 1;
                    ";


                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", CurrentUserId);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve data from the reader and use it as needed
                    int receiptNumber = reader.GetInt32("receipt_id");
                    string gsuiteId = reader.GetString("gsuite_id");

                    string paymentName = reader.GetString("payment_title");
                    decimal amount = reader.GetDecimal("amount");
                    string notes = reader.GetString("notes");




                    DateTime date = reader.GetDateTime("receipt_timestamp");

                    string transactionName = reader.IsDBNull(reader.GetOrdinal("transaction_name")) ? "" : reader.GetString("transaction_name");

                    string organization = reader.IsDBNull(reader.GetOrdinal("organization_name")) ? "" : reader.GetString("organization_name");
                  
                   
                    string cashType = reader.IsDBNull(reader.GetOrdinal("receipt_type")) ? "" : reader.GetString("receipt_type");


                    // Now you have the latest receipt data, you can display it on labels or wherever you want
                    receipt_number.Text = receiptNumber.ToString();
                    gsuite.Text = gsuiteId;
                    userid.Text = CurrentUserId.ToString();

                    amounts.Text = "P " + amount.ToString("#,##0.00");
                    notess.Text = notes;
                    organizations.Text = organization;
                   
                   
                    cash_type.Text = cashType;
                    dates.Text = date.ToString();

                }
                else
                {
                    MessageBox.Show("No receipt found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadLatestTransactions()
        {
            try
            {
                string query = @"SELECT t.transaction_id, t.transaction_name, CONCAT('P', t.amount) AS amount, COALESCE(e.event_name, 'No Event') AS event_name, t.gcash_reference_number
                 FROM Transactions t
                 LEFT JOIN Event e ON t.event_id = e.event_id
                 WHERE t.transaction_timestamp = (SELECT MAX(transaction_timestamp) FROM Transactions)";


                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void withdrawreceipt_Load(object sender, EventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void PrintPanel(Panel panel, PrintPageEventArgs e, Rectangle printableArea)
        {
            Rectangle panelArea = panel.Bounds;

            // Define the scaling factor to reduce the size of the content
            float scale = 0.8f; // Adjust this value to set the desired reduction (e.g., 0.8 for 80% reduction)

            // Calculate the centered position
            int centerX = printableArea.Left;
            int centerY = printableArea.Top;

            // Create a transformation matrix for scaling
            e.Graphics.TranslateTransform(centerX, centerY);
            e.Graphics.ScaleTransform(scale, scale);

            // Capture the panel's content to a bitmap
            Bitmap bitmap = new Bitmap(panelArea.Width, panelArea.Height);

            // Draw the panel's background onto the bitmap
            panel.DrawToBitmap(bitmap, new Rectangle(0, 0, panelArea.Width, panelArea.Height));

            // Draw the captured bitmap onto the print page
            e.Graphics.DrawImage(bitmap, 0, 0);


        }


    

        private void Print_Click(object sender, EventArgs e)
        {
            // Show print dialog to allow user to select printer and configure settings
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                // Set the printer settings of printDocument1 according to user selection
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                // Print the panel
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
              Rectangle printableArea = e.MarginBounds;

            // Print Panel 1 content
            PrintPanel(panelToPrint, e, printableArea);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
