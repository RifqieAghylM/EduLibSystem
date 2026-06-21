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
            btnuploadbook = new Button();
            btnupdateanddeletebook = new Button();
            btnback = new Button();
            SuspendLayout();
            // 
            // lblmanagebook
            // 
            lblmanagebook.AutoSize = true;
            lblmanagebook.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblmanagebook.Location = new Point(273, 55);
            lblmanagebook.Name = "lblmanagebook";
            lblmanagebook.Size = new Size(299, 50);
            lblmanagebook.TabIndex = 0;
            lblmanagebook.Text = "Manage Book";
            // 
            // btnuploadbook
            // 
            btnuploadbook.Location = new Point(99, 194);
            btnuploadbook.Name = "btnuploadbook";
            btnuploadbook.Size = new Size(193, 101);
            btnuploadbook.TabIndex = 1;
            btnuploadbook.Text = "Upload Book";
            btnuploadbook.UseVisualStyleBackColor = true;
            btnuploadbook.Click += btnuploadbook_Click;
            // 
            // btnupdateanddeletebook
            // 
            btnupdateanddeletebook.Location = new Point(471, 194);
            btnupdateanddeletebook.Name = "btnupdateanddeletebook";
            btnupdateanddeletebook.Size = new Size(202, 101);
            btnupdateanddeletebook.TabIndex = 3;
            btnupdateanddeletebook.Text = "Update And Delete Book";
            btnupdateanddeletebook.UseVisualStyleBackColor = true;
            btnupdateanddeletebook.Click += btnupdateanddeletebook_Click;
            // 
            // btnback
            // 
            btnback.Location = new Point(274, 354);
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
            Controls.Add(btnupdateanddeletebook);
            Controls.Add(btnuploadbook);
            Controls.Add(lblmanagebook);
            Name = "FormManageBook";
            Text = "Form Manage Book";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblmanagebook;
        private Button btnuploadbook;
        private Button btnupdateanddeletebook;
        private Button btnback;
    }
}