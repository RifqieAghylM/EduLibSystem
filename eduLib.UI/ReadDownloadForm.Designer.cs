namespace eduLib.UI
{
    partial class ReadDownloadForm
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
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvBooks = new DataGridView();
            btnDownload = new Button();
            btnRead = new Button();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(25, 70);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(350, 27);
            txtSearch.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(390, 65);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Cari Buku";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(25, 110);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(560, 250);
            dgvBooks.TabIndex = 3;
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(25, 380);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(150, 40);
            btnDownload.TabIndex = 2;
            btnDownload.Text = "Download PDF";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(190, 380);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(150, 40);
            btnRead.TabIndex = 1;
            btnRead.Text = "Baca Online";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(259, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Read And Download Book";
            lblTitle.Click += lblTitle_Click;
            // 
            // ReadDownloadForm
            // 
            ClientSize = new Size(601, 450);
            Controls.Add(lblTitle);
            Controls.Add(btnRead);
            Controls.Add(btnDownload);
            Controls.Add(dgvBooks);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Name = "ReadDownloadForm";
            Text = "EduLib";
            Load += ReadDownloadForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvBooks;
        private Button btnDownload;
        private Button btnRead;
        private Label lblTitle;
    }
}