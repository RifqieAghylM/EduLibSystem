namespace eduLib.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBookId;
        private System.Windows.Forms.TextBox txtBookId;

        private System.Windows.Forms.GroupBox grpBookmark;
        private System.Windows.Forms.Label lblBookmarkPage;
        private System.Windows.Forms.TextBox txtBookmarkPage;
        private System.Windows.Forms.Button btnSaveBookmark;
        private System.Windows.Forms.Button btnGetBookmark;
        private System.Windows.Forms.Label lblBookmarkResult;

        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.TextBox txtTotalPage;
        private System.Windows.Forms.Button btnUpdateProgress;
        private System.Windows.Forms.Button btnGetProgress;
        private System.Windows.Forms.Label lblProgressResult;
        private System.Windows.Forms.ProgressBar progressBar1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblBookId = new Label();
            txtBookId = new TextBox();
            grpBookmark = new GroupBox();
            lblBookmarkPage = new Label();
            txtBookmarkPage = new TextBox();
            btnSaveBookmark = new Button();
            btnGetBookmark = new Button();
            lblBookmarkResult = new Label();
            grpProgress = new GroupBox();
            lblCurrentPage = new Label();
            txtCurrentPage = new TextBox();
            lblTotalPage = new Label();
            txtTotalPage = new TextBox();
            btnUpdateProgress = new Button();
            btnGetProgress = new Button();
            lblProgressResult = new Label();
            progressBar1 = new ProgressBar();
            grpBookmark.SuspendLayout();
            grpProgress.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(311, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "eduLib - Book Tracking";
            // 
            // lblBookId
            // 
            lblBookId.AutoSize = true;
            lblBookId.Font = new Font("Segoe UI", 10F);
            lblBookId.Location = new Point(30, 75);
            lblBookId.Name = "lblBookId";
            lblBookId.Size = new Size(74, 23);
            lblBookId.TabIndex = 1;
            lblBookId.Text = "Book ID:";
            // 
            // txtBookId
            // 
            txtBookId.Font = new Font("Segoe UI", 10F);
            txtBookId.Location = new Point(150, 72);
            txtBookId.Name = "txtBookId";
            txtBookId.Size = new Size(500, 30);
            txtBookId.TabIndex = 2;
            txtBookId.TextChanged += txtBookId_TextChanged;
            // 
            // grpBookmark
            // 
            grpBookmark.Controls.Add(lblBookmarkPage);
            grpBookmark.Controls.Add(txtBookmarkPage);
            grpBookmark.Controls.Add(btnSaveBookmark);
            grpBookmark.Controls.Add(btnGetBookmark);
            grpBookmark.Controls.Add(lblBookmarkResult);
            grpBookmark.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBookmark.Location = new Point(30, 120);
            grpBookmark.Name = "grpBookmark";
            grpBookmark.Size = new Size(720, 160);
            grpBookmark.TabIndex = 3;
            grpBookmark.TabStop = false;
            grpBookmark.Text = "Bookmark";
            // 
            // lblBookmarkPage
            // 
            lblBookmarkPage.AutoSize = true;
            lblBookmarkPage.Font = new Font("Segoe UI", 10F);
            lblBookmarkPage.Location = new Point(20, 35);
            lblBookmarkPage.Name = "lblBookmarkPage";
            lblBookmarkPage.Size = new Size(82, 23);
            lblBookmarkPage.TabIndex = 0;
            lblBookmarkPage.Text = "Halaman:";
            // 
            // txtBookmarkPage
            // 
            txtBookmarkPage.Font = new Font("Segoe UI", 10F);
            txtBookmarkPage.Location = new Point(120, 32);
            txtBookmarkPage.Name = "txtBookmarkPage";
            txtBookmarkPage.Size = new Size(120, 30);
            txtBookmarkPage.TabIndex = 1;
            // 
            // btnSaveBookmark
            // 
            btnSaveBookmark.Font = new Font("Segoe UI", 9F);
            btnSaveBookmark.Location = new Point(20, 75);
            btnSaveBookmark.Name = "btnSaveBookmark";
            btnSaveBookmark.Size = new Size(180, 35);
            btnSaveBookmark.TabIndex = 2;
            btnSaveBookmark.Text = "Simpan Bookmark";
            btnSaveBookmark.Click += btnSaveBookmark_Click;
            // 
            // btnGetBookmark
            // 
            btnGetBookmark.Font = new Font("Segoe UI", 9F);
            btnGetBookmark.Location = new Point(210, 75);
            btnGetBookmark.Name = "btnGetBookmark";
            btnGetBookmark.Size = new Size(180, 35);
            btnGetBookmark.TabIndex = 3;
            btnGetBookmark.Text = "Lihat Bookmark";
            btnGetBookmark.Click += btnGetBookmark_Click;
            // 
            // lblBookmarkResult
            // 
            lblBookmarkResult.AutoSize = true;
            lblBookmarkResult.Font = new Font("Segoe UI", 10F);
            lblBookmarkResult.ForeColor = Color.DarkBlue;
            lblBookmarkResult.Location = new Point(20, 120);
            lblBookmarkResult.Name = "lblBookmarkResult";
            lblBookmarkResult.Size = new Size(62, 23);
            lblBookmarkResult.TabIndex = 4;
            lblBookmarkResult.Text = "Hasil: -";
            lblBookmarkResult.Click += lblBookmarkResult_Click;
            // 
            // grpProgress
            // 
            grpProgress.Controls.Add(lblCurrentPage);
            grpProgress.Controls.Add(txtCurrentPage);
            grpProgress.Controls.Add(lblTotalPage);
            grpProgress.Controls.Add(txtTotalPage);
            grpProgress.Controls.Add(btnUpdateProgress);
            grpProgress.Controls.Add(btnGetProgress);
            grpProgress.Controls.Add(lblProgressResult);
            grpProgress.Controls.Add(progressBar1);
            grpProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpProgress.Location = new Point(30, 300);
            grpProgress.Name = "grpProgress";
            grpProgress.Size = new Size(720, 250);
            grpProgress.TabIndex = 4;
            grpProgress.TabStop = false;
            grpProgress.Text = "Reading Progress";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Segoe UI", 10F);
            lblCurrentPage.Location = new Point(20, 35);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(141, 23);
            lblCurrentPage.TabIndex = 0;
            lblCurrentPage.Text = "Halaman saat ini:";
            // 
            // txtCurrentPage
            // 
            txtCurrentPage.Font = new Font("Segoe UI", 10F);
            txtCurrentPage.Location = new Point(180, 32);
            txtCurrentPage.Name = "txtCurrentPage";
            txtCurrentPage.Size = new Size(120, 30);
            txtCurrentPage.TabIndex = 1;
            // 
            // lblTotalPage
            // 
            lblTotalPage.AutoSize = true;
            lblTotalPage.Font = new Font("Segoe UI", 10F);
            lblTotalPage.Location = new Point(330, 35);
            lblTotalPage.Name = "lblTotalPage";
            lblTotalPage.Size = new Size(121, 23);
            lblTotalPage.TabIndex = 2;
            lblTotalPage.Text = "Total halaman:";
            // 
            // txtTotalPage
            // 
            txtTotalPage.Font = new Font("Segoe UI", 10F);
            txtTotalPage.Location = new Point(470, 32);
            txtTotalPage.Name = "txtTotalPage";
            txtTotalPage.Size = new Size(120, 30);
            txtTotalPage.TabIndex = 3;
            // 
            // btnUpdateProgress
            // 
            btnUpdateProgress.Font = new Font("Segoe UI", 9F);
            btnUpdateProgress.Location = new Point(20, 80);
            btnUpdateProgress.Name = "btnUpdateProgress";
            btnUpdateProgress.Size = new Size(180, 35);
            btnUpdateProgress.TabIndex = 4;
            btnUpdateProgress.Text = "Update Progress";
            btnUpdateProgress.Click += btnUpdateProgress_Click;
            // 
            // btnGetProgress
            // 
            btnGetProgress.Font = new Font("Segoe UI", 9F);
            btnGetProgress.Location = new Point(210, 80);
            btnGetProgress.Name = "btnGetProgress";
            btnGetProgress.Size = new Size(180, 35);
            btnGetProgress.TabIndex = 5;
            btnGetProgress.Text = "Lihat Progress";
            btnGetProgress.Click += btnGetProgress_Click;
            // 
            // lblProgressResult
            // 
            lblProgressResult.AutoSize = true;
            lblProgressResult.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblProgressResult.ForeColor = Color.DarkGreen;
            lblProgressResult.Location = new Point(20, 130);
            lblProgressResult.Name = "lblProgressResult";
            lblProgressResult.Size = new Size(85, 25);
            lblProgressResult.TabIndex = 6;
            lblProgressResult.Text = "Status: -";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(20, 170);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(670, 30);
            progressBar1.TabIndex = 7;
            // 
            // Form1
            // 
            ClientSize = new Size(800, 600);
            Controls.Add(lblTitle);
            Controls.Add(lblBookId);
            Controls.Add(txtBookId);
            Controls.Add(grpBookmark);
            Controls.Add(grpProgress);
            Name = "Form1";
            Text = "eduLib - Bookmark & Reading Progress";
            grpBookmark.ResumeLayout(false);
            grpBookmark.PerformLayout();
            grpProgress.ResumeLayout(false);
            grpProgress.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}