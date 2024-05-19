namespace Funds_Management_System
{
    partial class Reciept
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reciept));
            this.BackButton = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.receipt_number = new System.Windows.Forms.Label();
            this.payment_name = new System.Windows.Forms.Label();
            this.payment_method = new System.Windows.Forms.Label();
            this.amounts = new System.Windows.Forms.Label();
            this.organizations = new System.Windows.Forms.Label();
            this.cash_type = new System.Windows.Forms.Label();
            this.dates = new System.Windows.Forms.Label();
            this.userid = new System.Windows.Forms.Label();
            this.gsuite = new System.Windows.Forms.Label();
            this.events = new System.Windows.Forms.Label();
            this.transaction_name = new System.Windows.Forms.Label();
            this.notess = new System.Windows.Forms.Label();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panelToPrint = new Guna.UI2.WinForms.Guna2Panel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelToPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.BackButton.BorderRadius = 5;
            this.BackButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BackButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BackButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BackButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BackButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.BackButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BackButton.ForeColor = System.Drawing.Color.White;
            this.BackButton.Image = ((System.Drawing.Image)(resources.GetObject("BackButton.Image")));
            this.BackButton.Location = new System.Drawing.Point(17, 15);
            this.BackButton.Margin = new System.Windows.Forms.Padding(2);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(40, 39);
            this.BackButton.TabIndex = 125;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(177)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(53, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 126;
            this.label3.Tag = "";
            this.label3.Text = "Log Out";
            // 
            // receipt_number
            // 
            this.receipt_number.AutoSize = true;
            this.receipt_number.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receipt_number.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.receipt_number.Location = new System.Drawing.Point(42, 122);
            this.receipt_number.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.receipt_number.Name = "receipt_number";
            this.receipt_number.Size = new System.Drawing.Size(240, 36);
            this.receipt_number.TabIndex = 127;
            this.receipt_number.Tag = "";
            this.receipt_number.Text = "Receipt Number";
            // 
            // payment_name
            // 
            this.payment_name.AutoSize = true;
            this.payment_name.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payment_name.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.payment_name.Location = new System.Drawing.Point(160, 260);
            this.payment_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.payment_name.Name = "payment_name";
            this.payment_name.Size = new System.Drawing.Size(191, 17);
            this.payment_name.TabIndex = 128;
            this.payment_name.Tag = "";
            this.payment_name.Text = "Payment Name (id NUMBER)";
            this.payment_name.Click += new System.EventHandler(this.payment_name_Click);
            // 
            // payment_method
            // 
            this.payment_method.AutoSize = true;
            this.payment_method.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payment_method.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.payment_method.Location = new System.Drawing.Point(174, 409);
            this.payment_method.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.payment_method.Name = "payment_method";
            this.payment_method.Size = new System.Drawing.Size(113, 17);
            this.payment_method.TabIndex = 130;
            this.payment_method.Tag = "";
            this.payment_method.Text = "Payment Method";
            this.payment_method.Click += new System.EventHandler(this.payment_method_Click);
            // 
            // amounts
            // 
            this.amounts.AutoSize = true;
            this.amounts.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amounts.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.amounts.Location = new System.Drawing.Point(114, 371);
            this.amounts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.amounts.Name = "amounts";
            this.amounts.Size = new System.Drawing.Size(57, 18);
            this.amounts.TabIndex = 129;
            this.amounts.Tag = "";
            this.amounts.Text = "Amount";
            this.amounts.Click += new System.EventHandler(this.amounts_Click);
            // 
            // organizations
            // 
            this.organizations.AutoSize = true;
            this.organizations.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.organizations.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.organizations.Location = new System.Drawing.Point(145, 312);
            this.organizations.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.organizations.Name = "organizations";
            this.organizations.Size = new System.Drawing.Size(90, 18);
            this.organizations.TabIndex = 131;
            this.organizations.Tag = "";
            this.organizations.Text = "Organization";
            this.organizations.Click += new System.EventHandler(this.organizations_Click);
            // 
            // cash_type
            // 
            this.cash_type.AutoSize = true;
            this.cash_type.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cash_type.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cash_type.Location = new System.Drawing.Point(165, 436);
            this.cash_type.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cash_type.Name = "cash_type";
            this.cash_type.Size = new System.Drawing.Size(84, 17);
            this.cash_type.TabIndex = 132;
            this.cash_type.Tag = "";
            this.cash_type.Text = "CASH TYPE";
            this.cash_type.Click += new System.EventHandler(this.cash_type_Click);
            // 
            // dates
            // 
            this.dates.AutoSize = true;
            this.dates.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dates.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dates.Location = new System.Drawing.Point(160, 461);
            this.dates.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dates.Name = "dates";
            this.dates.Size = new System.Drawing.Size(35, 18);
            this.dates.TabIndex = 133;
            this.dates.Tag = "";
            this.dates.Text = "date";
            this.dates.Click += new System.EventHandler(this.dates_Click);
            // 
            // userid
            // 
            this.userid.AutoSize = true;
            this.userid.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userid.Location = new System.Drawing.Point(116, 188);
            this.userid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userid.Name = "userid";
            this.userid.Size = new System.Drawing.Size(55, 17);
            this.userid.TabIndex = 134;
            this.userid.Tag = "";
            this.userid.Text = "User Id";
            this.userid.Click += new System.EventHandler(this.userid_Click);
            // 
            // gsuite
            // 
            this.gsuite.AutoSize = true;
            this.gsuite.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gsuite.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gsuite.Location = new System.Drawing.Point(123, 211);
            this.gsuite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gsuite.Name = "gsuite";
            this.gsuite.Size = new System.Drawing.Size(48, 17);
            this.gsuite.TabIndex = 135;
            this.gsuite.Tag = "";
            this.gsuite.Text = "Gsuite";
            this.gsuite.Click += new System.EventHandler(this.gsuite_Click);
            // 
            // events
            // 
            this.events.AutoSize = true;
            this.events.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.events.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.events.Location = new System.Drawing.Point(155, 336);
            this.events.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.events.Name = "events";
            this.events.Size = new System.Drawing.Size(43, 17);
            this.events.TabIndex = 136;
            this.events.Tag = "";
            this.events.Text = "Event";
            this.events.Click += new System.EventHandler(this.events_Click);
            // 
            // transaction_name
            // 
            this.transaction_name.AutoSize = true;
            this.transaction_name.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transaction_name.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.transaction_name.Location = new System.Drawing.Point(180, 236);
            this.transaction_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.transaction_name.Name = "transaction_name";
            this.transaction_name.Size = new System.Drawing.Size(121, 17);
            this.transaction_name.TabIndex = 137;
            this.transaction_name.Tag = "";
            this.transaction_name.Text = "Transaction Name";
            this.transaction_name.Click += new System.EventHandler(this.transaction_name_Click);
            // 
            // notess
            // 
            this.notess.AutoSize = true;
            this.notess.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notess.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.notess.Location = new System.Drawing.Point(94, 286);
            this.notess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.notess.Name = "notess";
            this.notess.Size = new System.Drawing.Size(43, 17);
            this.notess.TabIndex = 138;
            this.notess.Tag = "";
            this.notess.Text = "Notes";
            this.notess.Click += new System.EventHandler(this.notess_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.guna2Button2.BorderRadius = 5;
            this.guna2Button2.BorderThickness = 1;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button2.Image")));
            this.guna2Button2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button2.Location = new System.Drawing.Point(55, 668);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(154, 39);
            this.guna2Button2.TabIndex = 139;
            this.guna2Button2.Text = "Confirm";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(45, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 140;
            this.label1.Tag = "";
            this.label1.Text = "User Id: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(45, 211);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 141;
            this.label2.Tag = "";
            this.label2.Text = "Gsuite Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(45, 236);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 17);
            this.label4.TabIndex = 142;
            this.label4.Tag = "";
            this.label4.Text = "Transaction Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(45, 260);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 143;
            this.label5.Tag = "";
            this.label5.Text = "Payment Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(45, 286);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 144;
            this.label6.Tag = "";
            this.label6.Text = "Note:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(45, 312);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 17);
            this.label7.TabIndex = 145;
            this.label7.Tag = "";
            this.label7.Text = "Organization:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(45, 336);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 146;
            this.label8.Tag = "";
            this.label8.Text = "Related Event:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(45, 371);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 17);
            this.label9.TabIndex = 147;
            this.label9.Tag = "";
            this.label9.Text = "Amount:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(45, 409);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 17);
            this.label10.TabIndex = 148;
            this.label10.Tag = "";
            this.label10.Text = "Payment Method:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(43, 436);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 17);
            this.label11.TabIndex = 149;
            this.label11.Tag = "";
            this.label11.Text = "Transaction Type:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(43, 462);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 17);
            this.label12.TabIndex = 150;
            this.label12.Tag = "";
            this.label12.Text = "Date and Time:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.Location = new System.Drawing.Point(164, 37);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(154, 42);
            this.label13.TabIndex = 151;
            this.label13.Text = "Fund Management\r\nSystem\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(91, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 153;
            this.pictureBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.Location = new System.Drawing.Point(165, 14);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(149, 17);
            this.label14.TabIndex = 152;
            this.label14.Text = "BatStatesU Organization";
            // 
            // panelToPrint
            // 
            this.panelToPrint.BackColor = System.Drawing.Color.White;
            this.panelToPrint.BorderColor = System.Drawing.Color.White;
            this.panelToPrint.BorderRadius = 5;
            this.panelToPrint.BorderThickness = 3;
            this.panelToPrint.Controls.Add(this.pictureBox1);
            this.panelToPrint.Controls.Add(this.label12);
            this.panelToPrint.Controls.Add(this.label13);
            this.panelToPrint.Controls.Add(this.label11);
            this.panelToPrint.Controls.Add(this.label14);
            this.panelToPrint.Controls.Add(this.label10);
            this.panelToPrint.Controls.Add(this.label9);
            this.panelToPrint.Controls.Add(this.receipt_number);
            this.panelToPrint.Controls.Add(this.label8);
            this.panelToPrint.Controls.Add(this.payment_name);
            this.panelToPrint.Controls.Add(this.label7);
            this.panelToPrint.Controls.Add(this.amounts);
            this.panelToPrint.Controls.Add(this.label6);
            this.panelToPrint.Controls.Add(this.payment_method);
            this.panelToPrint.Controls.Add(this.label5);
            this.panelToPrint.Controls.Add(this.organizations);
            this.panelToPrint.Controls.Add(this.label4);
            this.panelToPrint.Controls.Add(this.cash_type);
            this.panelToPrint.Controls.Add(this.label2);
            this.panelToPrint.Controls.Add(this.dates);
            this.panelToPrint.Controls.Add(this.label1);
            this.panelToPrint.Controls.Add(this.userid);
            this.panelToPrint.Controls.Add(this.gsuite);
            this.panelToPrint.Controls.Add(this.notess);
            this.panelToPrint.Controls.Add(this.events);
            this.panelToPrint.Controls.Add(this.transaction_name);
            this.panelToPrint.Location = new System.Drawing.Point(57, 87);
            this.panelToPrint.Margin = new System.Windows.Forms.Padding(2);
            this.panelToPrint.Name = "panelToPrint";
            this.panelToPrint.Size = new System.Drawing.Size(427, 577);
            this.panelToPrint.TabIndex = 154;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.guna2Button1.BorderRadius = 5;
            this.guna2Button1.BorderThickness = 1;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.Location = new System.Drawing.Point(329, 668);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(154, 39);
            this.guna2Button1.TabIndex = 155;
            this.guna2Button1.Text = "Print";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // Reciept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(547, 723);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.panelToPrint);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.guna2Button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reciept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reciept";
            this.Load += new System.EventHandler(this.Reciept_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelToPrint.ResumeLayout(false);
            this.panelToPrint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button BackButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label receipt_number;
        private System.Windows.Forms.Label payment_name;
        private System.Windows.Forms.Label payment_method;
        private System.Windows.Forms.Label amounts;
        private System.Windows.Forms.Label organizations;
        private System.Windows.Forms.Label cash_type;
        private System.Windows.Forms.Label dates;
        private System.Windows.Forms.Label userid;
        private System.Windows.Forms.Label gsuite;
        private System.Windows.Forms.Label events;
        private System.Windows.Forms.Label transaction_name;
        private System.Windows.Forms.Label notess;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private Guna.UI2.WinForms.Guna2Panel panelToPrint;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}