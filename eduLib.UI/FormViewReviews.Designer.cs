namespace eduLib.UI
{
    partial class FormViewReviews
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
            txtSearchTitle = new TextBox();
            btnSearchReview = new Button();
            lstReviewsDisplay = new ListBox();
            btnBack = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(262, 38);
            label1.Name = "label1";
            label1.Size = new Size(336, 37);
            label1.TabIndex = 0;
            label1.Text = "DAFTAR ULASAN BUKU";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 131);
            label2.Name = "label2";
            label2.Size = new Size(174, 20);
            label2.TabIndex = 1;
            label2.Text = "Masukkan Judul/Penulis";
            // 
            // txtSearchTitle
            // 
            txtSearchTitle.Location = new Point(192, 128);
            txtSearchTitle.Name = "txtSearchTitle";
            txtSearchTitle.Size = new Size(555, 27);
            txtSearchTitle.TabIndex = 2;
            // 
            // btnSearchReview
            // 
            btnSearchReview.Location = new Point(192, 386);
            btnSearchReview.Name = "btnSearchReview";
            btnSearchReview.Size = new Size(94, 29);
            btnSearchReview.TabIndex = 3;
            btnSearchReview.Text = "Cari Review";
            btnSearchReview.UseVisualStyleBackColor = true;
            // 
            // lstReviewsDisplay
            // 
            lstReviewsDisplay.FormattingEnabled = true;
            lstReviewsDisplay.Location = new Point(192, 176);
            lstReviewsDisplay.Name = "lstReviewsDisplay";
            lstReviewsDisplay.Size = new Size(555, 204);
            lstReviewsDisplay.TabIndex = 4;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(653, 400);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 5;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // FormViewReviews
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(782, 553);
            Controls.Add(btnBack);
            Controls.Add(lstReviewsDisplay);
            Controls.Add(btnSearchReview);
            Controls.Add(txtSearchTitle);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormViewReviews";
            Text = "FormViewReviews";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtSearchTitle;
        private Button btnSearchReview;
        private ListBox lstReviewsDisplay;
        private Button btnBack;
    }
}