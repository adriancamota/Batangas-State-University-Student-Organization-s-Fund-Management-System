namespace Funds_Management_System
{
    partial class Expenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Expenses));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.InsertButton = new Guna.UI2.WinForms.Guna2Button();
            this.ReferenceNum = new Guna.UI2.WinForms.Guna2TextBox();
            this.Amount = new Guna.UI2.WinForms.Guna2TextBox();
            this.Transaction_Name = new Guna.UI2.WinForms.Guna2TextBox();
            this.TotalAmountLabel = new System.Windows.Forms.Label();
            this.EventCombobox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ExpensesDataGridView = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Save = new Guna.UI2.WinForms.Guna2Button();
            this.Delete = new Guna.UI2.WinForms.Guna2Button();
            this.BackButton = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ExitButton = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ExpensesDataGridView)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // InsertButton
            // 
            this.InsertButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.InsertButton.BorderRadius = 5;
            this.InsertButton.BorderThickness = 1;
            this.InsertButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.InsertButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.InsertButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.InsertButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.InsertButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.InsertButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.InsertButton.ForeColor = System.Drawing.Color.White;
            this.InsertButton.Image = ((System.Drawing.Image)(resources.GetObject("InsertButton.Image")));
            this.InsertButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.InsertButton.Location = new System.Drawing.Point(11, 233);
            this.InsertButton.Margin = new System.Windows.Forms.Padding(2);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(172, 39);
            this.InsertButton.TabIndex = 99;
            this.InsertButton.Text = "INSERT ";
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // ReferenceNum
            // 
            this.ReferenceNum.BorderRadius = 5;
            this.ReferenceNum.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ReferenceNum.DefaultText = "";
            this.ReferenceNum.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ReferenceNum.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ReferenceNum.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ReferenceNum.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ReferenceNum.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ReferenceNum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ReferenceNum.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ReferenceNum.Location = new System.Drawing.Point(11, 174);
            this.ReferenceNum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ReferenceNum.Name = "ReferenceNum";
            this.ReferenceNum.PasswordChar = '\0';
            this.ReferenceNum.PlaceholderText = "";
            this.ReferenceNum.SelectedText = "";
            this.ReferenceNum.Size = new System.Drawing.Size(172, 39);
            this.ReferenceNum.TabIndex = 103;
            // 
            // Amount
            // 
            this.Amount.BorderRadius = 5;
            this.Amount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Amount.DefaultText = "";
            this.Amount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Amount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Amount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Amount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Amount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Amount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Amount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Amount.Location = new System.Drawing.Point(11, 118);
            this.Amount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Amount.Name = "Amount";
            this.Amount.PasswordChar = '\0';
            this.Amount.PlaceholderText = "";
            this.Amount.SelectedText = "";
            this.Amount.Size = new System.Drawing.Size(172, 39);
            this.Amount.TabIndex = 102;
            // 
            // Transaction_Name
            // 
            this.Transaction_Name.BorderRadius = 5;
            this.Transaction_Name.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Transaction_Name.DefaultText = "";
            this.Transaction_Name.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Transaction_Name.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Transaction_Name.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Transaction_Name.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Transaction_Name.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Transaction_Name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Transaction_Name.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Transaction_Name.Location = new System.Drawing.Point(11, 63);
            this.Transaction_Name.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Transaction_Name.Name = "Transaction_Name";
            this.Transaction_Name.PasswordChar = '\0';
            this.Transaction_Name.PlaceholderText = "";
            this.Transaction_Name.SelectedText = "";
            this.Transaction_Name.Size = new System.Drawing.Size(172, 39);
            this.Transaction_Name.TabIndex = 101;
            // 
            // TotalAmountLabel
            // 
            this.TotalAmountLabel.AutoSize = true;
            this.TotalAmountLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalAmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(177)))), ((int)(((byte)(160)))));
            this.TotalAmountLabel.Location = new System.Drawing.Point(22, 343);
            this.TotalAmountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TotalAmountLabel.Name = "TotalAmountLabel";
            this.TotalAmountLabel.Size = new System.Drawing.Size(0, 25);
            this.TotalAmountLabel.TabIndex = 105;
            this.TotalAmountLabel.Click += new System.EventHandler(this.TotalAmountLabel_Click);
            // 
            // EventCombobox
            // 
            this.EventCombobox.BackColor = System.Drawing.Color.Transparent;
            this.EventCombobox.BorderRadius = 5;
            this.EventCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.EventCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EventCombobox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EventCombobox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EventCombobox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.EventCombobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.EventCombobox.ItemHeight = 30;
            this.EventCombobox.Location = new System.Drawing.Point(11, 18);
            this.EventCombobox.Margin = new System.Windows.Forms.Padding(2);
            this.EventCombobox.Name = "EventCombobox";
            this.EventCombobox.Size = new System.Drawing.Size(174, 36);
            this.EventCombobox.TabIndex = 111;
            // 
            // ExpensesDataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.ExpensesDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ExpensesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExpensesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ExpensesDataGridView.ColumnHeadersHeight = 35;
            this.ExpensesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExpensesDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ExpensesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ExpensesDataGridView.Location = new System.Drawing.Point(26, 17);
            this.ExpensesDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.ExpensesDataGridView.Name = "ExpensesDataGridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExpensesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ExpensesDataGridView.RowHeadersVisible = false;
            this.ExpensesDataGridView.RowHeadersWidth = 51;
            this.ExpensesDataGridView.RowTemplate.Height = 24;
            this.ExpensesDataGridView.Size = new System.Drawing.Size(528, 305);
            this.ExpensesDataGridView.TabIndex = 113;
            this.ExpensesDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.ExpensesDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.ExpensesDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.ExpensesDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.ExpensesDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.ExpensesDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.ExpensesDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.ExpensesDataGridView.ThemeStyle.HeaderStyle.Height = 35;
            this.ExpensesDataGridView.ThemeStyle.ReadOnly = false;
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.Height = 24;
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ExpensesDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Save
            // 
            this.Save.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Save.BorderRadius = 5;
            this.Save.BorderThickness = 1;
            this.Save.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Save.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Save.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Save.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Save.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Save.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Save.ForeColor = System.Drawing.Color.White;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Save.Location = new System.Drawing.Point(447, 330);
            this.Save.Margin = new System.Windows.Forms.Padding(2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(107, 39);
            this.Save.TabIndex = 114;
            this.Save.Text = "Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Delete
            // 
            this.Delete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Delete.BorderRadius = 5;
            this.Delete.BorderThickness = 1;
            this.Delete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Delete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Delete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Delete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Delete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Delete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Delete.ForeColor = System.Drawing.Color.White;
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Delete.Location = new System.Drawing.Point(302, 330);
            this.Delete.Margin = new System.Windows.Forms.Padding(2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(107, 39);
            this.Delete.TabIndex = 115;
            this.Delete.Text = "Delete";
            this.Delete.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
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
            this.BackButton.Location = new System.Drawing.Point(11, 11);
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
            this.label3.Location = new System.Drawing.Point(47, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 19);
            this.label3.TabIndex = 126;
            this.label3.Tag = "";
            this.label3.Text = "Back";
            // 
            // ExitButton
            // 
            this.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.ExitButton.BorderRadius = 5;
            this.ExitButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ExitButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ExitButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ExitButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ExitButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.ExitButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.ImageSize = new System.Drawing.Size(40, 40);
            this.ExitButton.Location = new System.Drawing.Point(749, 11);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(40, 39);
            this.ExitButton.TabIndex = 124;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderRadius = 5;
            this.guna2Panel1.BorderThickness = 3;
            this.guna2Panel1.Controls.Add(this.EventCombobox);
            this.guna2Panel1.Controls.Add(this.InsertButton);
            this.guna2Panel1.Controls.Add(this.Transaction_Name);
            this.guna2Panel1.Controls.Add(this.Amount);
            this.guna2Panel1.Controls.Add(this.ReferenceNum);
            this.guna2Panel1.Location = new System.Drawing.Point(20, 102);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(192, 384);
            this.guna2Panel1.TabIndex = 127;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 58);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 129;
            this.pictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Label1.Location = new System.Drawing.Point(73, 58);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 21);
            this.Label1.TabIndex = 128;
            this.Label1.Text = "Expenses";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.Color.White;
            this.guna2Panel2.BorderRadius = 5;
            this.guna2Panel2.BorderThickness = 3;
            this.guna2Panel2.Controls.Add(this.ExpensesDataGridView);
            this.guna2Panel2.Controls.Add(this.TotalAmountLabel);
            this.guna2Panel2.Controls.Add(this.Save);
            this.guna2Panel2.Controls.Add(this.Delete);
            this.guna2Panel2.Location = new System.Drawing.Point(217, 102);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(572, 384);
            this.guna2Panel2.TabIndex = 128;
            // 
            // Expenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(798, 528);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Expenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expenses";
            this.Load += new System.EventHandler(this.Expenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ExpensesDataGridView)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button InsertButton;
        private Guna.UI2.WinForms.Guna2TextBox ReferenceNum;
        private Guna.UI2.WinForms.Guna2TextBox Amount;
        private Guna.UI2.WinForms.Guna2TextBox Transaction_Name;
        private System.Windows.Forms.Label TotalAmountLabel;
        private Guna.UI2.WinForms.Guna2ComboBox EventCombobox;
        private Guna.UI2.WinForms.Guna2DataGridView ExpensesDataGridView;
        private Guna.UI2.WinForms.Guna2Button Save;
        private Guna.UI2.WinForms.Guna2Button Delete;
        private Guna.UI2.WinForms.Guna2Button BackButton;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button ExitButton;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
    }
}