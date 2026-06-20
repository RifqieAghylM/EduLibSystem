namespace eduLib.UI
{
    partial class FormManageBook
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
            lblmanagebook = new Label();
            btnuploadbuku = new Button();
            btndeletebuku = new Button();
            btneditbuku = new Button();
            btnback = new Button();
            SuspendLayout();
            // 
            // lblmanagebook
            // 
            lblmanagebook.AutoSize = true;
            lblmanagebook.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblmanagebook.Location = new Point(235, 55);
            lblmanagebook.Name = "lblmanagebook";
            lblmanagebook.Size = new Size(299, 50);
            lblmanagebook.TabIndex = 0;
            lblmanagebook.Text = "Manage Book";
            // 
            // btnuploadbuku
            // 
            btnuploadbuku.Location = new Point(99, 194);
            btnuploadbuku.Name = "btnuploadbuku";
            btnuploadbuku.Size = new Size(193, 101);
            btnuploadbuku.TabIndex = 1;
            btnuploadbuku.Text = "Upload Book";
            btnuploadbuku.UseVisualStyleBackColor = true;
            btnuploadbuku.Click += btnuploadbuku_Click;
            // 
            // btndeletebuku
            // 
            btndeletebuku.Location = new Point(99, 355);
            btndeletebuku.Name = "btndeletebuku";
            btndeletebuku.Size = new Size(185, 101);
            btndeletebuku.TabIndex = 2;
            btndeletebuku.Text = "Delete Book";
            btndeletebuku.UseVisualStyleBackColor = true;
            btndeletebuku.Click += btndeletebuku_Click;
            // 
            // btneditbuku
            // 
            btneditbuku.Location = new Point(471, 194);
            btneditbuku.Name = "btneditbuku";
            btneditbuku.Size = new Size(202, 101);
            btneditbuku.TabIndex = 3;
            btneditbuku.Text = "Edit Book";
            btneditbuku.UseVisualStyleBackColor = true;
            btneditbuku.Click += btneditbuku_Click;
            // 
            // btnback
            // 
            btnback.Location = new Point(471, 355);
            btnback.Name = "btnback";
            btnback.Size = new Size(202, 101);
            btnback.TabIndex = 4;
            btnback.Text = "Back";
            btnback.UseVisualStyleBackColor = true;
            btnback.Click += btnback_Click;
            // 
            // FormManageBook
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(778, 544);
            Controls.Add(btnback);
            Controls.Add(btneditbuku);
            Controls.Add(btndeletebuku);
            Controls.Add(btnuploadbuku);
            Controls.Add(lblmanagebook);
            Name = "FormManageBook";
            Text = "Form Manage Book";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblmanagebook;
        private Button btnuploadbuku;
        private Button btndeletebuku;
        private Button btneditbuku;
        private Button btnback;
    }
}