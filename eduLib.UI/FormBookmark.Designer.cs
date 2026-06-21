namespace eduLib.UI
{
    partial class FormBookmark
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
            lblTitle = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvBooks = new DataGridView();
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
            btnBackDashboard = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            grpBookmark.SuspendLayout();
            grpProgress.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Book Tracking";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F);
            lblSearch.Location = new Point(20, 48);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(74, 20);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Cari Buku:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(120, 45);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(540, 27);
            txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 9F);
            btnSearch.Location = new Point(670, 43);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(90, 26);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Cari";
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeight = 29;
            dgvBooks.Font = new Font("Segoe UI", 8.5F);
            dgvBooks.Location = new Point(20, 75);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(740, 105);
            dgvBooks.TabIndex = 4;
            dgvBooks.CellClick += dgvBooks_CellClick;
            // 
            // lblBookId
            // 
            lblBookId.AutoSize = true;
            lblBookId.Font = new Font("Segoe UI", 9F);
            lblBookId.Location = new Point(20, 191);
            lblBookId.Name = "lblBookId";
            lblBookId.Size = new Size(116, 20);
            lblBookId.TabIndex = 5;
            lblBookId.Text = "Book ID terpilih:";
            // 
            // txtBookId
            // 
            txtBookId.BackColor = SystemColors.Control;
            txtBookId.Font = new Font("Segoe UI", 9F);
            txtBookId.Location = new Point(150, 188);
            txtBookId.Name = "txtBookId";
            txtBookId.ReadOnly = true;
            txtBookId.Size = new Size(610, 27);
            txtBookId.TabIndex = 6;
            // 
            // grpBookmark
            // 
            grpBookmark.Controls.Add(lblBookmarkPage);
            grpBookmark.Controls.Add(txtBookmarkPage);
            grpBookmark.Controls.Add(btnSaveBookmark);
            grpBookmark.Controls.Add(btnGetBookmark);
            grpBookmark.Controls.Add(lblBookmarkResult);
            grpBookmark.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpBookmark.Location = new Point(20, 218);
            grpBookmark.Name = "grpBookmark";
            grpBookmark.Size = new Size(740, 120);
            grpBookmark.TabIndex = 7;
            grpBookmark.TabStop = false;
            grpBookmark.Text = "Bookmark";
            // 
            // lblBookmarkPage
            // 
            lblBookmarkPage.AutoSize = true;
            lblBookmarkPage.Font = new Font("Segoe UI", 9F);
            lblBookmarkPage.Location = new Point(15, 28);
            lblBookmarkPage.Name = "lblBookmarkPage";
            lblBookmarkPage.Size = new Size(72, 20);
            lblBookmarkPage.TabIndex = 0;
            lblBookmarkPage.Text = "Halaman:";
            // 
            // txtBookmarkPage
            // 
            txtBookmarkPage.Font = new Font("Segoe UI", 9F);
            txtBookmarkPage.Location = new Point(100, 25);
            txtBookmarkPage.Name = "txtBookmarkPage";
            txtBookmarkPage.Size = new Size(110, 27);
            txtBookmarkPage.TabIndex = 1;
            // 
            // btnSaveBookmark
            // 
            btnSaveBookmark.Font = new Font("Segoe UI", 8.5F);
            btnSaveBookmark.Location = new Point(15, 58);
            btnSaveBookmark.Name = "btnSaveBookmark";
            btnSaveBookmark.Size = new Size(160, 30);
            btnSaveBookmark.TabIndex = 2;
            btnSaveBookmark.Text = "Simpan Bookmark";
            btnSaveBookmark.Click += btnSaveBookmark_Click;
            // 
            // btnGetBookmark
            // 
            btnGetBookmark.Font = new Font("Segoe UI", 8.5F);
            btnGetBookmark.Location = new Point(185, 58);
            btnGetBookmark.Name = "btnGetBookmark";
            btnGetBookmark.Size = new Size(160, 30);
            btnGetBookmark.TabIndex = 3;
            btnGetBookmark.Text = "Lihat Bookmark";
            btnGetBookmark.Click += btnGetBookmark_Click;
            // 
            // lblBookmarkResult
            // 
            lblBookmarkResult.AutoSize = true;
            lblBookmarkResult.Font = new Font("Segoe UI", 9F);
            lblBookmarkResult.ForeColor = Color.DarkBlue;
            lblBookmarkResult.Location = new Point(15, 95);
            lblBookmarkResult.MaximumSize = new Size(700, 0);
            lblBookmarkResult.Name = "lblBookmarkResult";
            lblBookmarkResult.Size = new Size(55, 20);
            lblBookmarkResult.TabIndex = 4;
            lblBookmarkResult.Text = "Hasil: -";
            // 
            // grpProgress
            // 
            grpProgress.Controls.Add(btnBackDashboard);
            grpProgress.Controls.Add(lblCurrentPage);
            grpProgress.Controls.Add(txtCurrentPage);
            grpProgress.Controls.Add(lblTotalPage);
            grpProgress.Controls.Add(txtTotalPage);
            grpProgress.Controls.Add(btnUpdateProgress);
            grpProgress.Controls.Add(btnGetProgress);
            grpProgress.Controls.Add(lblProgressResult);
            grpProgress.Controls.Add(progressBar1);
            grpProgress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpProgress.Location = new Point(20, 348);
            grpProgress.Name = "grpProgress";
            grpProgress.Size = new Size(740, 230);
            grpProgress.TabIndex = 8;
            grpProgress.TabStop = false;
            grpProgress.Text = "Reading Progress";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Segoe UI", 9F);
            lblCurrentPage.Location = new Point(15, 30);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(123, 20);
            lblCurrentPage.TabIndex = 0;
            lblCurrentPage.Text = "Halaman saat ini:";
            // 
            // txtCurrentPage
            // 
            txtCurrentPage.Font = new Font("Segoe UI", 9F);
            txtCurrentPage.Location = new Point(160, 27);
            txtCurrentPage.Name = "txtCurrentPage";
            txtCurrentPage.Size = new Size(110, 27);
            txtCurrentPage.TabIndex = 1;
            // 
            // lblTotalPage
            // 
            lblTotalPage.AutoSize = true;
            lblTotalPage.Font = new Font("Segoe UI", 9F);
            lblTotalPage.Location = new Point(300, 30);
            lblTotalPage.Name = "lblTotalPage";
            lblTotalPage.Size = new Size(106, 20);
            lblTotalPage.TabIndex = 2;
            lblTotalPage.Text = "Total halaman:";
            // 
            // txtTotalPage
            // 
            txtTotalPage.Font = new Font("Segoe UI", 9F);
            txtTotalPage.Location = new Point(430, 27);
            txtTotalPage.Name = "txtTotalPage";
            txtTotalPage.Size = new Size(110, 27);
            txtTotalPage.TabIndex = 3;
            // 
            // btnUpdateProgress
            // 
            btnUpdateProgress.Font = new Font("Segoe UI", 8.5F);
            btnUpdateProgress.Location = new Point(15, 65);
            btnUpdateProgress.Name = "btnUpdateProgress";
            btnUpdateProgress.Size = new Size(160, 30);
            btnUpdateProgress.TabIndex = 4;
            btnUpdateProgress.Text = "Update Progress";
            btnUpdateProgress.Click += btnUpdateProgress_Click;
            // 
            // btnGetProgress
            // 
            btnGetProgress.Font = new Font("Segoe UI", 8.5F);
            btnGetProgress.Location = new Point(185, 65);
            btnGetProgress.Name = "btnGetProgress";
            btnGetProgress.Size = new Size(160, 30);
            btnGetProgress.TabIndex = 5;
            btnGetProgress.Text = "Lihat Progress";
            btnGetProgress.Click += btnGetProgress_Click;
            // 
            // lblProgressResult
            // 
            lblProgressResult.AutoSize = true;
            lblProgressResult.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProgressResult.ForeColor = Color.Black;
            lblProgressResult.Location = new Point(15, 105);
            lblProgressResult.Name = "lblProgressResult";
            lblProgressResult.Size = new Size(77, 23);
            lblProgressResult.TabIndex = 6;
            lblProgressResult.Text = "Status: -";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(15, 140);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(700, 28);
            progressBar1.TabIndex = 7;
            // 
            // btnBackDashboard
            // 
            btnBackDashboard.Location = new Point(621, 184);
            btnBackDashboard.Name = "btnBackDashboard";
            btnBackDashboard.Size = new Size(94, 29);
            btnBackDashboard.TabIndex = 8;
            btnBackDashboard.Text = "Back";
            btnBackDashboard.UseVisualStyleBackColor = true;
            // 
            // FormBookmark
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 600);
            Controls.Add(lblTitle);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(dgvBooks);
            Controls.Add(lblBookId);
            Controls.Add(txtBookId);
            Controls.Add(grpBookmark);
            Controls.Add(grpProgress);
            Name = "FormBookmark";
            Text = "Book Tracking";
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            grpBookmark.ResumeLayout(false);
            grpBookmark.PerformLayout();
            grpProgress.ResumeLayout(false);
            grpProgress.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnBackDashboard;
    }
}