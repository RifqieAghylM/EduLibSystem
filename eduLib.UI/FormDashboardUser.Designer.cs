namespace eduLib.UI
{
    partial class FormDashboardUser
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
            lbltitledashboarduser = new Label();
            lblwelcomeuser = new Label();
            btnreadanddownloadbook = new Button();
            btnbookmark = new Button();
            btnreview = new Button();
            btnlogout = new Button();
            SuspendLayout();
            // 
            // lbltitledashboarduser
            // 
            lbltitledashboarduser.AutoSize = true;
            lbltitledashboarduser.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbltitledashboarduser.Location = new Point(288, 26);
            lbltitledashboarduser.Name = "lbltitledashboarduser";
            lbltitledashboarduser.Size = new Size(368, 50);
            lbltitledashboarduser.TabIndex = 0;
            lbltitledashboarduser.Text = "Dashboard User";
            // 
            // lblwelcomeuser
            // 
            lblwelcomeuser.AutoSize = true;
            lblwelcomeuser.Location = new Point(88, 110);
            lblwelcomeuser.Name = "lblwelcomeuser";
            lblwelcomeuser.Size = new Size(139, 25);
            lblwelcomeuser.TabIndex = 1;
            lblwelcomeuser.Text = "Welcome, User! ";
            // 
            // btnreadanddownloadbook
            // 
            btnreadanddownloadbook.Location = new Point(114, 176);
            btnreadanddownloadbook.Name = "btnreadanddownloadbook";
            btnreadanddownloadbook.Size = new Size(180, 126);
            btnreadanddownloadbook.TabIndex = 5;
            btnreadanddownloadbook.Text = "Read And Download Book";
            btnreadanddownloadbook.UseVisualStyleBackColor = true;
            btnreadanddownloadbook.Click += btnreadanddownloadbook_Click;
            // 
            // btnbookmark
            // 
            btnbookmark.Location = new Point(496, 176);
            btnbookmark.Name = "btnbookmark";
            btnbookmark.Size = new Size(201, 126);
            btnbookmark.TabIndex = 6;
            btnbookmark.Text = "Bookmark";
            btnbookmark.UseVisualStyleBackColor = true;
            btnbookmark.Click += btnbookmark_Click;
            // 
            // btnreview
            // 
            btnreview.Location = new Point(114, 336);
            btnreview.Name = "btnreview";
            btnreview.Size = new Size(180, 125);
            btnreview.TabIndex = 8;
            btnreview.Text = "Review";
            btnreview.UseVisualStyleBackColor = true;
            btnreview.Click += btnreview_Click;
            // 
            // btnlogout
            // 
            btnlogout.Location = new Point(496, 336);
            btnlogout.Name = "btnlogout";
            btnlogout.Size = new Size(201, 125);
            btnlogout.TabIndex = 9;
            btnlogout.Text = "Log Out";
            btnlogout.UseVisualStyleBackColor = true;
            btnlogout.Click += btnlogout_Click;
            // 
            // FormDashboardUser
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(778, 544);
            Controls.Add(btnlogout);
            Controls.Add(btnreview);
            Controls.Add(btnbookmark);
            Controls.Add(btnreadanddownloadbook);
            Controls.Add(lblwelcomeuser);
            Controls.Add(lbltitledashboarduser);
            Name = "FormDashboardUser";
            Text = "Form Dashboard User";
            Load += FormDashboard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbltitledashboarduser;
        private Label lblwelcomeuser;
        private Button btnreadanddownloadbook;
        private Button btnbookmark;
        private Button btnreview;
        private Button btnlogout;
    }
}
