namespace eduLib.UI
{
    partial class FormReview
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtBookTitle = new TextBox();
            txtUsername = new TextBox();
            rtbComment = new RichTextBox();
            btnSubmitReview = new Button();
            button1 = new Button();
            btnKeHalamanLihat = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(254, 30);
            label1.Name = "label1";
            label1.Size = new Size(326, 37);
            label1.TabIndex = 0;
            label1.Text = "TULIS REVIEWS BARU";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 129);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 1;
            label2.Text = "Judul Buku";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(31, 204);
            label3.Name = "label3";
            label3.Size = new Size(82, 20);
            label3.TabIndex = 2;
            label3.Text = "Username:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(31, 304);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 3;
            label4.Text = "Isi Ulasan:";
            // 
            // txtBookTitle
            // 
            txtBookTitle.Location = new Point(128, 129);
            txtBookTitle.Name = "txtBookTitle";
            txtBookTitle.Size = new Size(607, 27);
            txtBookTitle.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(128, 204);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(607, 27);
            txtUsername.TabIndex = 5;
            // 
            // rtbComment
            // 
            rtbComment.Location = new Point(128, 304);
            rtbComment.Name = "rtbComment";
            rtbComment.Size = new Size(607, 100);
            rtbComment.TabIndex = 6;
            rtbComment.Text = "";
            // 
            // btnSubmitReview
            // 
            btnSubmitReview.Location = new Point(128, 429);
            btnSubmitReview.Name = "btnSubmitReview";
            btnSubmitReview.Size = new Size(122, 29);
            btnSubmitReview.TabIndex = 7;
            btnSubmitReview.Text = "Kirim Review";
            btnSubmitReview.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(613, 429);
            button1.Name = "button1";
            button1.Size = new Size(122, 29);
            button1.TabIndex = 8;
            button1.Text = "Back";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnKeHalamanLihat
            // 
            btnKeHalamanLihat.Location = new Point(371, 429);
            btnKeHalamanLihat.Name = "btnKeHalamanLihat";
            btnKeHalamanLihat.Size = new Size(107, 29);
            btnKeHalamanLihat.TabIndex = 9;
            btnKeHalamanLihat.Text = "Lihat Review";
            btnKeHalamanLihat.UseVisualStyleBackColor = true;
            // 
            // FormReview
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(782, 553);
            Controls.Add(btnKeHalamanLihat);
            Controls.Add(button1);
            Controls.Add(btnSubmitReview);
            Controls.Add(rtbComment);
            Controls.Add(txtUsername);
            Controls.Add(txtBookTitle);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormReview";
            Text = "FormReview";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtBookTitle;
        private TextBox txtUsername;
        private RichTextBox rtbComment;
        private Button btnSubmitReview;
        private Button button1;
        private Button btnKeHalamanLihat;
    }
}