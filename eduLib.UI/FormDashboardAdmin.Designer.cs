namespace eduLib.UI
{
    partial class FormDashboardAdmin
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
            lbltitledashboardadmin = new Label();
            lblwelcomeadmin = new Label();
            btnmanagebook = new Button();
            btnreadanddownloadbook = new Button();
            btnbookmark = new Button();
            btnreview = new Button();
            btnlogout = new Button();
            SuspendLayout();
            // 
            // lbltitledashboardadmin
            // 
            lbltitledashboardadmin.AutoSize = true;
            lbltitledashboardadmin.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbltitledashboardadmin.Location = new Point(266, 32);
            lbltitledashboardadmin.Name = "lbltitledashboardadmin";
            lbltitledashboardadmin.Size = new Size(395, 50);
            lbltitledashboardadmin.TabIndex = 0;
            lbltitledashboardadmin.Text = "Dashboard Admin";
            // 
            // lblwelcomeadmin
            // 
            lblwelcomeadmin.AutoSize = true;
            lblwelcomeadmin.Location = new Point(60, 100);
            lblwelcomeadmin.Name = "lblwelcomeadmin";
            lblwelcomeadmin.Size = new Size(152, 25);
            lblwelcomeadmin.TabIndex = 1;
            lblwelcomeadmin.Text = "Welcome, Admin!";
            // 
            // btnmanagebook
            // 
            btnmanagebook.Location = new Point(63, 169);
            btnmanagebook.Name = "btnmanagebook";
            btnmanagebook.Size = new Size(149, 104);
            btnmanagebook.TabIndex = 2;
            btnmanagebook.Text = "Manage Book";
            btnmanagebook.UseVisualStyleBackColor = true;
            btnmanagebook.Click += btnmanagebook_Click;
            // 
            // btnreadanddownloadbook
            // 
            btnreadanddownloadbook.Location = new Point(303, 169);
            btnreadanddownloadbook.Name = "btnreadanddownloadbook";
            btnreadanddownloadbook.Size = new Size(159, 104);
            btnreadanddownloadbook.TabIndex = 5;
            btnreadanddownloadbook.Text = "Read And Download Book";
            btnreadanddownloadbook.UseVisualStyleBackColor = true;
            btnreadanddownloadbook.Click += btnreadanddownloadbook_Click;
            // 
            // btnbookmark
            // 
            btnbookmark.Location = new Point(63, 337);
            btnbookmark.Name = "btnbookmark";
            btnbookmark.Size = new Size(159, 106);
            btnbookmark.TabIndex = 7;
            btnbookmark.Text = "Bookmark";
            btnbookmark.UseVisualStyleBackColor = true;
            btnbookmark.Click += btnbookmark_Click;
            // 
            // btnreview
            // 
            btnreview.Location = new Point(557, 173);
            btnreview.Name = "btnreview";
            btnreview.Size = new Size(137, 100);
            btnreview.TabIndex = 9;
            btnreview.Text = "Review";
            btnreview.UseVisualStyleBackColor = true;
            btnreview.Click += btnreview_Click;
            // 
            // btnlogout
            // 
            btnlogout.Location = new Point(303, 337);
            btnlogout.Name = "btnlogout";
            btnlogout.Size = new Size(159, 106);
            btnlogout.TabIndex = 10;
            btnlogout.Text = "Log Out";
            btnlogout.UseVisualStyleBackColor = true;
            btnlogout.Click += btnlogout_Click;
            // 
            // FormDashboardAdmin
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Control;
            ClientSize = new Size(778, 544);
            Controls.Add(btnlogout);
            Controls.Add(btnreview);
            Controls.Add(btnbookmark);
            Controls.Add(btnreadanddownloadbook);
            Controls.Add(btnmanagebook);
            Controls.Add(lblwelcomeadmin);
            Controls.Add(lbltitledashboardadmin);
            Name = "FormDashboardAdmin";
            Text = "Form Dashboard Admin";
            Load += FormDashboard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbltitledashboardadmin;
        private Label lblwelcomeadmin;
        private Button btnmanagebook;
        private Button btnreadanddownloadbook;
        private Button btnbookmark;
        private Button btnreview;
        private Button btnlogout;
    }
}