namespace eduLib.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvBooks;

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

            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();

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

            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.grpBookmark.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.SuspendLayout();

            //  Title  
            this.lblTitle.Text = "Book Tracking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.AutoSize = true;

            //  Search Buku 
            this.lblSearch.Text = "Cari Buku:";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(20, 48);
            this.lblSearch.AutoSize = true;

            this.txtSearch.Location = new System.Drawing.Point(120, 45);
            this.txtSearch.Width = 540;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnSearch.Text = "Cari";
            this.btnSearch.Location = new System.Drawing.Point(670, 43);
            this.btnSearch.Size = new System.Drawing.Size(90, 26);
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            //  Tabel hasil pencarian
            this.dgvBooks.Location = new System.Drawing.Point(20, 75);
            this.dgvBooks.Size = new System.Drawing.Size(740, 105);
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.MultiSelect = false;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvBooks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellClick);

            //  Book ID terpilih (read-only) 
            this.lblBookId.Text = "Book ID terpilih:";
            this.lblBookId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBookId.Location = new System.Drawing.Point(20, 191);
            this.lblBookId.AutoSize = true;

            this.txtBookId.Location = new System.Drawing.Point(150, 188);
            this.txtBookId.Width = 610;
            this.txtBookId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBookId.ReadOnly = true;
            this.txtBookId.BackColor = System.Drawing.SystemColors.Control;

            //  Bookmark Group
            this.grpBookmark.Text = "Bookmark";
            this.grpBookmark.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpBookmark.Location = new System.Drawing.Point(20, 218);
            this.grpBookmark.Size = new System.Drawing.Size(740, 120);

            this.lblBookmarkPage.Text = "Halaman:";
            this.lblBookmarkPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBookmarkPage.Location = new System.Drawing.Point(15, 28);
            this.lblBookmarkPage.AutoSize = true;

            this.txtBookmarkPage.Location = new System.Drawing.Point(100, 25);
            this.txtBookmarkPage.Width = 110;
            this.txtBookmarkPage.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnSaveBookmark.Text = "Simpan Bookmark";
            this.btnSaveBookmark.Location = new System.Drawing.Point(15, 58);
            this.btnSaveBookmark.Size = new System.Drawing.Size(160, 30);
            this.btnSaveBookmark.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnSaveBookmark.Click += new System.EventHandler(this.btnSaveBookmark_Click);

            this.btnGetBookmark.Text = "Lihat Bookmark";
            this.btnGetBookmark.Location = new System.Drawing.Point(185, 58);
            this.btnGetBookmark.Size = new System.Drawing.Size(160, 30);
            this.btnGetBookmark.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnGetBookmark.Click += new System.EventHandler(this.btnGetBookmark_Click);

            this.lblBookmarkResult.Text = "Hasil: -";
            this.lblBookmarkResult.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBookmarkResult.Location = new System.Drawing.Point(15, 95);
            this.lblBookmarkResult.AutoSize = true;
            this.lblBookmarkResult.MaximumSize = new System.Drawing.Size(700, 0);
            this.lblBookmarkResult.ForeColor = System.Drawing.Color.DarkBlue;

            this.grpBookmark.Controls.Add(this.lblBookmarkPage);
            this.grpBookmark.Controls.Add(this.txtBookmarkPage);
            this.grpBookmark.Controls.Add(this.btnSaveBookmark);
            this.grpBookmark.Controls.Add(this.btnGetBookmark);
            this.grpBookmark.Controls.Add(this.lblBookmarkResult);

            //  Reading Progress Group 
            this.grpProgress.Text = "Reading Progress";
            this.grpProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpProgress.Location = new System.Drawing.Point(20, 348);
            this.grpProgress.Size = new System.Drawing.Size(740, 230);

            this.lblCurrentPage.Text = "Halaman saat ini:";
            this.lblCurrentPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentPage.Location = new System.Drawing.Point(15, 30);
            this.lblCurrentPage.AutoSize = true;

            this.txtCurrentPage.Location = new System.Drawing.Point(160, 27);
            this.txtCurrentPage.Width = 110;
            this.txtCurrentPage.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.lblTotalPage.Text = "Total halaman:";
            this.lblTotalPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalPage.Location = new System.Drawing.Point(300, 30);
            this.lblTotalPage.AutoSize = true;

            this.txtTotalPage.Location = new System.Drawing.Point(430, 27);
            this.txtTotalPage.Width = 110;
            this.txtTotalPage.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnUpdateProgress.Text = "Update Progress";
            this.btnUpdateProgress.Location = new System.Drawing.Point(15, 65);
            this.btnUpdateProgress.Size = new System.Drawing.Size(160, 30);
            this.btnUpdateProgress.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnUpdateProgress.Click += new System.EventHandler(this.btnUpdateProgress_Click);

            this.btnGetProgress.Text = "Lihat Progress";
            this.btnGetProgress.Location = new System.Drawing.Point(185, 65);
            this.btnGetProgress.Size = new System.Drawing.Size(160, 30);
            this.btnGetProgress.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnGetProgress.Click += new System.EventHandler(this.btnGetProgress_Click);

            this.lblProgressResult.Text = "Status: -";
            this.lblProgressResult.Location = new System.Drawing.Point(15, 105);
            this.lblProgressResult.AutoSize = true;
            this.lblProgressResult.ForeColor = System.Drawing.Color.Black;
            this.lblProgressResult.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.progressBar1.Location = new System.Drawing.Point(15, 140);
            this.progressBar1.Size = new System.Drawing.Size(700, 28);

            this.grpProgress.Controls.Add(this.lblCurrentPage);
            this.grpProgress.Controls.Add(this.txtCurrentPage);
            this.grpProgress.Controls.Add(this.lblTotalPage);
            this.grpProgress.Controls.Add(this.txtTotalPage);
            this.grpProgress.Controls.Add(this.btnUpdateProgress);
            this.grpProgress.Controls.Add(this.btnGetProgress);
            this.grpProgress.Controls.Add(this.lblProgressResult);
            this.grpProgress.Controls.Add(this.progressBar1);

            //  Form 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "Book Tracking";

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.lblBookId);
            this.Controls.Add(this.txtBookId);
            this.Controls.Add(this.grpBookmark);
            this.Controls.Add(this.grpProgress);

            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.grpBookmark.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}