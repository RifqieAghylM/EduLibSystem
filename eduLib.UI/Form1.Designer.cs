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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBookId = new System.Windows.Forms.Label();
            this.txtBookId = new System.Windows.Forms.TextBox();

            this.grpBookmark = new System.Windows.Forms.GroupBox();
            this.lblBookmarkPage = new System.Windows.Forms.Label();
            this.txtBookmarkPage = new System.Windows.Forms.TextBox();
            this.btnSaveBookmark = new System.Windows.Forms.Button();
            this.btnGetBookmark = new System.Windows.Forms.Button();
            this.lblBookmarkResult = new System.Windows.Forms.Label();

            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnUpdateProgress = new System.Windows.Forms.Button();
            this.btnGetProgress = new System.Windows.Forms.Button();
            this.lblProgressResult = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();

            this.grpBookmark.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.SuspendLayout();

            // ===== Title =====
            this.lblTitle.Text = "eduLib - Book Tracking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.AutoSize = true;

            // ===== Book ID =====
            this.lblBookId.Text = "Book ID:";
            this.lblBookId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookId.Location = new System.Drawing.Point(30, 75);
            this.lblBookId.AutoSize = true;

            this.txtBookId.Location = new System.Drawing.Point(150, 72);
            this.txtBookId.Width = 500;
            this.txtBookId.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===== Bookmark Group =====
            this.grpBookmark.Text = "Bookmark";
            this.grpBookmark.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpBookmark.Location = new System.Drawing.Point(30, 120);
            this.grpBookmark.Size = new System.Drawing.Size(720, 160);

            this.lblBookmarkPage.Text = "Halaman:";
            this.lblBookmarkPage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookmarkPage.Location = new System.Drawing.Point(20, 35);
            this.lblBookmarkPage.AutoSize = true;

            this.txtBookmarkPage.Location = new System.Drawing.Point(120, 32);
            this.txtBookmarkPage.Width = 120;
            this.txtBookmarkPage.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnSaveBookmark.Text = "Simpan Bookmark";
            this.btnSaveBookmark.Location = new System.Drawing.Point(20, 75);
            this.btnSaveBookmark.Size = new System.Drawing.Size(180, 35);
            this.btnSaveBookmark.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSaveBookmark.Click += new System.EventHandler(this.btnSaveBookmark_Click);

            this.btnGetBookmark.Text = "Lihat Bookmark";
            this.btnGetBookmark.Location = new System.Drawing.Point(210, 75);
            this.btnGetBookmark.Size = new System.Drawing.Size(180, 35);
            this.btnGetBookmark.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGetBookmark.Click += new System.EventHandler(this.btnGetBookmark_Click);

            this.lblBookmarkResult.Text = "Hasil: -";
            this.lblBookmarkResult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookmarkResult.Location = new System.Drawing.Point(20, 120);
            this.lblBookmarkResult.AutoSize = true;
            this.lblBookmarkResult.ForeColor = System.Drawing.Color.DarkBlue;

            this.grpBookmark.Controls.Add(this.lblBookmarkPage);
            this.grpBookmark.Controls.Add(this.txtBookmarkPage);
            this.grpBookmark.Controls.Add(this.btnSaveBookmark);
            this.grpBookmark.Controls.Add(this.btnGetBookmark);
            this.grpBookmark.Controls.Add(this.lblBookmarkResult);

            // ===== Reading Progress Group =====
            this.grpProgress.Text = "Reading Progress";
            this.grpProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpProgress.Location = new System.Drawing.Point(30, 300);
            this.grpProgress.Size = new System.Drawing.Size(720, 250);

            this.lblCurrentPage.Text = "Halaman saat ini:";
            this.lblCurrentPage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentPage.Location = new System.Drawing.Point(20, 35);
            this.lblCurrentPage.AutoSize = true;

            this.txtCurrentPage.Location = new System.Drawing.Point(180, 32);
            this.txtCurrentPage.Width = 120;
            this.txtCurrentPage.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblTotalPage.Text = "Total halaman:";
            this.lblTotalPage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalPage.Location = new System.Drawing.Point(330, 35);
            this.lblTotalPage.AutoSize = true;

            this.txtTotalPage.Location = new System.Drawing.Point(470, 32);
            this.txtTotalPage.Width = 120;
            this.txtTotalPage.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnUpdateProgress.Text = "Update Progress";
            this.btnUpdateProgress.Location = new System.Drawing.Point(20, 80);
            this.btnUpdateProgress.Size = new System.Drawing.Size(180, 35);
            this.btnUpdateProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdateProgress.Click += new System.EventHandler(this.btnUpdateProgress_Click);

            this.btnGetProgress.Text = "Lihat Progress";
            this.btnGetProgress.Location = new System.Drawing.Point(210, 80);
            this.btnGetProgress.Size = new System.Drawing.Size(180, 35);
            this.btnGetProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGetProgress.Click += new System.EventHandler(this.btnGetProgress_Click);

            this.lblProgressResult.Text = "Status: -";
            this.lblProgressResult.Location = new System.Drawing.Point(20, 130);
            this.lblProgressResult.AutoSize = true;
            this.lblProgressResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblProgressResult.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);

            this.progressBar1.Location = new System.Drawing.Point(20, 170);
            this.progressBar1.Size = new System.Drawing.Size(670, 30);

            this.grpProgress.Controls.Add(this.lblCurrentPage);
            this.grpProgress.Controls.Add(this.txtCurrentPage);
            this.grpProgress.Controls.Add(this.lblTotalPage);
            this.grpProgress.Controls.Add(this.txtTotalPage);
            this.grpProgress.Controls.Add(this.btnUpdateProgress);
            this.grpProgress.Controls.Add(this.btnGetProgress);
            this.grpProgress.Controls.Add(this.lblProgressResult);
            this.grpProgress.Controls.Add(this.progressBar1);

            // ===== Form =====
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "eduLib - Bookmark & Reading Progress";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblBookId);
            this.Controls.Add(this.txtBookId);
            this.Controls.Add(this.grpBookmark);
            this.Controls.Add(this.grpProgress);

            this.grpBookmark.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}