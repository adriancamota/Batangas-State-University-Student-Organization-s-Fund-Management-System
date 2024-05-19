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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.Extensions.Logging;
using Mysqlx.Crud;
using System.Web.Util;
using System.Drawing.Printing;

namespace Funds_Management_System
{
    public partial class Reciept : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }
        public Reciept(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();
            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

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
                    string formattedAmount = amount.ToString("#,##0.00");
                    string notes = reader.GetString("notes");
                  
                  
                   
                   
                    DateTime date = reader.GetDateTime("receipt_timestamp");

                    string transactionName = reader.IsDBNull(reader.GetOrdinal("transaction_name")) ? "" : reader.GetString("transaction_name");

                    string organization = reader.IsDBNull(reader.GetOrdinal("organization_name")) ? "" : reader.GetString("organization_name");
                    string eventName = reader.IsDBNull(reader.GetOrdinal("event_name")) ? "" : reader.GetString("event_name");
                    string paymentMethod = reader.IsDBNull(reader.GetOrdinal("paymentMethod")) ? "" : reader.GetString("paymentMethod");
                    string cashType = reader.IsDBNull(reader.GetOrdinal("receipt_type")) ? "" : reader.GetString("receipt_type");


                    // Now you have the latest receipt data, you can display it on labels or wherever you want
                    receipt_number.Text =  receiptNumber.ToString();
                    gsuite.Text =  gsuiteId;
                    userid.Text = CurrentUserId.ToString();
                    transaction_name.Text =  transactionName;
                    payment_name.Text = paymentName;
                    amounts.Text = "P" + amount.ToString("#,##0.00");


                   

                    notess.Text = notes;
                    organizations.Text =  organization;
                    events.Text = eventName;
                    payment_method.Text = paymentMethod;
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


        private void Reciept_Load(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void userid_Click(object sender, EventArgs e)
        {

        }

        private void transaction_name_Click(object sender, EventArgs e)
        {

        }

        private void organizations_Click(object sender, EventArgs e)
        {

        }

        private void notess_Click(object sender, EventArgs e)
        {

        }

        private void gsuite_Click(object sender, EventArgs e)
        {

        }

        private void events_Click(object sender, EventArgs e)
        {

        }

        private void amounts_Click(object sender, EventArgs e)
        {

        }

        private void payment_name_Click(object sender, EventArgs e)
        {

        }

        private void dates_Click(object sender, EventArgs e)
        {

        }

        private void cash_type_Click(object sender, EventArgs e)
        {

        }

        private void payment_method_Click(object sender, EventArgs e)
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
        

        private void guna2Button1_Click(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle printableArea = e.MarginBounds;

            // Print Panel 1 content
            PrintPanel(panelToPrint, e, printableArea);
        }
    }
}
