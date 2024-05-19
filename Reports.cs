using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Funds_Management_System
{
    public partial class Reports : Form
    {
        MySqlConnection connection;
        private int CurrentUserId { get; set; }
        private int CurrentDepartmentId { get; set; }

        private int CurrentOrganizationId { get; set; }

     
        public Reports(int currentUserId, int currentDepartmentId, int currentOrganizationId)
        {
            InitializeComponent();
            InitializeConnection();

            CurrentUserId = currentUserId;
            CurrentDepartmentId = currentDepartmentId;
            CurrentOrganizationId = currentOrganizationId;

            LoadReportData();
            LoadReportDataGridSource();
            LoadReportDataGridExpense();

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
           
    }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the area of the panel to be printed
            Rectangle panelArea = panelToPrint.Bounds;
            Bitmap bitmap = new Bitmap(panelArea.Width, panelArea.Height);

            // Capture the panel's content to a bitmap
            panelToPrint.DrawToBitmap(bitmap, new Rectangle(0, 0, panelArea.Width, panelArea.Height));

            // Draw the captured bitmap onto the print page
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
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
        private void Reports_Load(object sender, EventArgs e)
        {



        }


        private void LoadReportDataGridSource()
        {
            try
            {
                connection.Open();

                string query = @"   SELECT 
                'Initial balance' AS Source,
                (SELECT FORMAT (amount,2) FROM Transactions WHERE transaction_name = 'Initial balance' AND cash_type = 'Deposit' AND organization_id = @organizationId) AS Total_Amount_Collected,
                1 AS Number_of_Related_Transactions 

            UNION ALL

           SELECT 
            pb.payment_title AS billing_name,
            FORMAT(SUM(t.amount), 2) AS total_amount,
            COUNT(t.transaction_id) AS transaction_count
        FROM 
            Transactions t
        JOIN 
            PaymentBilling pb ON t.billing_id = pb.billing_id
        JOIN 
            Organization o ON pb.organization_id = o.organization_id
        WHERE 
            t.cash_type = 'Deposit' AND
            o.department_id = @departmentId AND
            pb.organization_id = @organizationId
        GROUP BY 
            t.billing_id, pb.payment_title";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                command.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                SourcesDataGrid.DataSource = dataTable;

                reader.Close();
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
        private void LoadReportDataGridExpense()
        {
            try
            {
                connection.Open();

                string query = @" SELECT 
                e.event_name as  Related_Event,
                FORMAT(SUM(t.amount), 2) AS Total_Amount_Withdraw,  -- Format the amount column
                COUNT(*) AS Total_Withdraw_Count
            FROM 
                Transactions t
            JOIN 
                Organization o ON t.organization_id = o.organization_id
            JOIN
                Event e ON t.event_id = e.event_id
            WHERE 
                t.cash_type = 'Withdraw' AND
                o.department_id = @departmentId AND
                t.organization_id = @organizationId
            GROUP BY 
                t.event_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                command.Parameters.AddWithValue("@organizationId", CurrentOrganizationId);
                MySqlDataReader reader = command.ExecuteReader();



                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ExpensesDataGrid.DataSource = dataTable;

                reader.Close();
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

        private void LoadReportData()
        {
            try
            {
                connection.Open();

                string query = @"SELECT 
                        r.report_id,
                        o.organization_name,
                        r.report_name,
                        DATE(r.report_date) AS report_date,
                        r.remaining_balance,
                        r.total_generated_funds,
                        r.total_expenses
                       
                    FROM 
                        Reports r
                    INNER JOIN 
                        Organization o ON r.organization_id = o.organization_id
                    INNER JOIN 
                        Department d ON o.department_id = d.department_id
                    WHERE 
                        o.organization_id = @orgId and
                        d.department_id = @departmentId
                    ORDER BY 
                        r.report_id DESC
                    LIMIT 1";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orgId", CurrentOrganizationId);
                command.Parameters.AddWithValue("@departmentId", CurrentDepartmentId);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Display data in labels
                    lblReportId.Text = reader["report_id"].ToString();
                    lblReportId2.Text = reader["report_id"].ToString();
                    lblOrganizationName.Text = reader["organization_name"].ToString();
                    lblOrganizationName2.Text = reader["organization_name"].ToString();
                    lblReportName.Text = reader["report_name"].ToString();
                    lblReportDate.Text = ((DateTime)reader["report_date"]).ToString("MMMM dd, yyyy");

                    // Format numerical values with commas for thousands separation
                    decimal remainingBalance = (decimal)reader["remaining_balance"];
                    decimal totalGeneratedFunds = (decimal)reader["total_generated_funds"];
                    decimal totalExpenses = (decimal)reader["total_expenses"];

                    lblRemainingBalance.Text = "P " +(string.Format("{0:#,0.00}",  remainingBalance ) + " only.");
                    lblTotalGeneratedFunds.Text = "P " +( string.Format("{0:#,0.00}",  totalGeneratedFunds));
                    lblTotalGeneratedFunds2.Text = "P " + (string.Format("{0:#,0.00}",  totalGeneratedFunds));
                    lblTotalExpenses.Text = "P " +( string.Format("{0:#,0.00}", totalExpenses));
                    lblTotalExpenses2.Text = "P " +( string.Format("{0:#,0.00}",  totalExpenses));


                }
                else
                {
                    MessageBox.Show("No data found.");
                }

                reader.Close();
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
    
    private void ExpensesDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void lblOrganizationName_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
        private void PrintPanel(Panel panel, PrintPageEventArgs e, Rectangle printableArea)
        {// Define the area of the panel to be printed
            Rectangle panelArea = panel.Bounds;

            // Calculate the scaling factor to fit the content within the printable area
            float scaleX = (float)printableArea.Width / panelArea.Width;
            float scaleY = (float)printableArea.Height / panelArea.Height;
            float scale = Math.Min(scaleX, scaleY);

            // Calculate the centered position
            int centerX = (printableArea.Width - (int)(panelArea.Width * scale)) / 2 + printableArea.Left;
            int centerY = (printableArea.Height - (int)(panelArea.Height * scale)) / 2 + printableArea.Top;

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
        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {// Define the printable area (e.g., paper size)
            Rectangle printableArea = e.MarginBounds;

            // Print Panel 1 content
            PrintPanel(panelToPrint, e, printableArea);

           
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Show print dialog to allow user to select printer and configure settings
            if (printDialog2.ShowDialog() == DialogResult.OK)
            {
                // Set the printer settings of printDocument1 according to user selection
                printDocument2.PrinterSettings = printDialog2.PrinterSettings;

                // Print the panel
                printDocument2.Print();
            }
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the printable area (e.g., paper size)
            Rectangle printableArea = e.MarginBounds;

            // Print Panel 1 content
            PrintPanel(ToPrint2, e, printableArea);
        }
    }
}
