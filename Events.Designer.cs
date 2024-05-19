namespace Funds_Management_System
{
    partial class Events
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Events));
            this.Event_textbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.AddEvent = new Guna.UI2.WinForms.Guna2Button();
            this.ExitButton = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BackButton = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Event_textbox
            // 
            this.Event_textbox.BorderRadius = 5;
            this.Event_textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Event_textbox.DefaultText = "Event Name";
            this.Event_textbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Event_textbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Event_textbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Event_textbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Event_textbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Event_textbox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Event_textbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Event_textbox.Location = new System.Drawing.Point(148, 62);
            this.Event_textbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Event_textbox.Name = "Event_textbox";
            this.Event_textbox.PasswordChar = '\0';
            this.Event_textbox.PlaceholderText = "";
            this.Event_textbox.SelectedText = "";
            this.Event_textbox.Size = new System.Drawing.Size(154, 39);
            this.Event_textbox.TabIndex = 114;
            // 
            // AddEvent
            // 
            this.AddEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.AddEvent.BorderRadius = 5;
            this.AddEvent.BorderThickness = 1;
            this.AddEvent.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AddEvent.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AddEvent.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AddEvent.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AddEvent.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.AddEvent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AddEvent.ForeColor = System.Drawing.Color.White;
            this.AddEvent.Image = ((System.Drawing.Image)(resources.GetObject("AddEvent.Image")));
            this.AddEvent.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AddEvent.Location = new System.Drawing.Point(148, 119);
            this.AddEvent.Margin = new System.Windows.Forms.Padding(2);
            this.AddEvent.Name = "AddEvent";
            this.AddEvent.Size = new System.Drawing.Size(154, 39);
            this.AddEvent.TabIndex = 113;
            this.AddEvent.Text = "ADD EVENT";
            this.AddEvent.Click += new System.EventHandler(this.AddEvent_Click);
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
            this.ExitButton.Location = new System.Drawing.Point(551, 10);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(40, 39);
            this.ExitButton.TabIndex = 121;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(177)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(44, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 19);
            this.label3.TabIndex = 123;
            this.label3.Tag = "";
            this.label3.Text = "Back";
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
            this.BackButton.Location = new System.Drawing.Point(8, 6);
            this.BackButton.Margin = new System.Windows.Forms.Padding(2);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(40, 39);
            this.BackButton.TabIndex = 122;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 49);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 125;
            this.pictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.Label1.Location = new System.Drawing.Point(76, 49);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(60, 21);
            this.Label1.TabIndex = 124;
            this.Label1.Text = "Events";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.Color.White;
            this.guna2Panel2.BorderRadius = 5;
            this.guna2Panel2.BorderThickness = 3;
            this.guna2Panel2.Controls.Add(this.Event_textbox);
            this.guna2Panel2.Controls.Add(this.AddEvent);
            this.guna2Panel2.Location = new System.Drawing.Point(80, 99);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(451, 224);
            this.guna2Panel2.TabIndex = 126;
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Events";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Events";
            this.Load += new System.EventHandler(this.Events_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox Event_textbox;
        private Guna.UI2.WinForms.Guna2Button AddEvent;
        private Guna.UI2.WinForms.Guna2Button ExitButton;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button BackButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
    }
}