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
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(25, 70);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(627, 27);
            txtSearch.TabIndex = 5;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.SkyBlue;
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Location = new Point(658, 70);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 27);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(25, 119);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(733, 349);
            dgvBooks.TabIndex = 3;
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(250, 489);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(150, 40);
            btnDownload.TabIndex = 2;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnRead
            // 
            btnRead.BackColor = Color.White;
            btnRead.FlatStyle = FlatStyle.System;
            btnRead.Location = new Point(25, 489);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(150, 40);
            btnRead.TabIndex = 1;
            btnRead.Text = "Read";
            btnRead.UseVisualStyleBackColor = false;
            btnRead.Click += btnRead_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(25, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(294, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Read And Download Book";
            lblTitle.Click += lblTitle_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.Salmon;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(658, 489);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 40);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // ReadDownloadForm
            // 
            ClientSize = new Size(782, 553);
            Controls.Add(btnBack);
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
        private Button btnBack;
    }
}