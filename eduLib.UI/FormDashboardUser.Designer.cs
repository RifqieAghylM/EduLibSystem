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
            btnhistory = new Button();
            btnreview = new Button();
            btnlogout = new Button();
            SuspendLayout();
            // 
            // lbltitledashboarduser
            // 
            lbltitledashboarduser.AutoSize = true;
            lbltitledashboarduser.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbltitledashboarduser.Location = new Point(200, 26);
            lbltitledashboarduser.Name = "lbltitledashboarduser";
            lbltitledashboarduser.Size = new Size(368, 50);
            lbltitledashboarduser.TabIndex = 0;
            lbltitledashboarduser.Text = "Dashboard User";
            // label1_Click dihapus — label judul tidak membutuhkan event Click
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
            btnreadanddownloadbook.Size = new Size(156, 114);
            btnreadanddownloadbook.TabIndex = 5;
            btnreadanddownloadbook.Text = "Read And Download Book";
            btnreadanddownloadbook.UseVisualStyleBackColor = true;
            btnreadanddownloadbook.Click += btnreadanddownloadbook_Click;
            // 
            // btnbookmark
            // 
            btnbookmark.Location = new Point(323, 176);
            btnbookmark.Name = "btnbookmark";
            btnbookmark.Size = new Size(155, 114);
            btnbookmark.TabIndex = 6;
            btnbookmark.Text = "Bookmark";
            btnbookmark.UseVisualStyleBackColor = true;
            btnbookmark.Click += btnbookmark_Click;
            // 
            // btnhistory
            // 
            btnhistory.Location = new Point(114, 343);
            btnhistory.Name = "btnhistory";
            btnhistory.Size = new Size(156, 109);
            btnhistory.TabIndex = 7;
            btnhistory.Text = "History";
            btnhistory.UseVisualStyleBackColor = true;
            btnhistory.Click += btnhistory_Click;
            // 
            // btnreview
            // 
            btnreview.Location = new Point(542, 177);
            btnreview.Name = "btnreview";
            btnreview.Size = new Size(151, 113);
            btnreview.TabIndex = 8;
            btnreview.Text = "Review";
            btnreview.UseVisualStyleBackColor = true;
            btnreview.Click += btnreview_Click;
            // 
            // btnlogout
            // 
            btnlogout.Location = new Point(323, 343);
            btnlogout.Name = "btnlogout";
            btnlogout.Size = new Size(155, 109);
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
            Controls.Add(btnhistory);
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
        private Button btnhistory;
        private Button btnreview;
        private Button btnlogout;
    }
}