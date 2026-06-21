namespace eduLib.UI
{
    partial class EduLibAdmin1
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
            label5 = new Label();
            textBoxTitle = new TextBox();
            textBoxAuthor = new TextBox();
            textBoxYear = new TextBox();
            buttonBrowser = new Button();
            buttonUpload = new Button();
            buttonReset = new Button();
            buttonBack = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(202, 34);
            label1.Name = "label1";
            label1.Size = new Size(327, 40);
            label1.TabIndex = 0;
            label1.Text = "BOOK MANAGEMENT";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 126);
            label2.Name = "label2";
            label2.Size = new Size(60, 32);
            label2.TabIndex = 1;
            label2.Text = "Title";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 190);
            label3.Name = "label3";
            label3.Size = new Size(87, 32);
            label3.TabIndex = 2;
            label3.Text = "Author";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 260);
            label4.Name = "label4";
            label4.Size = new Size(58, 32);
            label4.TabIndex = 3;
            label4.Text = "Year";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 330);
            label5.Name = "label5";
            label5.Size = new Size(175, 32);
            label5.TabIndex = 4;
            label5.Text = "File Buku (PDF)";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(228, 123);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(327, 39);
            textBoxTitle.TabIndex = 5;
            textBoxTitle.TextChanged += textBoxTitle_TextChanged;
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.Location = new Point(228, 183);
            textBoxAuthor.Name = "textBoxAuthor";
            textBoxAuthor.Size = new Size(327, 39);
            textBoxAuthor.TabIndex = 6;
            textBoxAuthor.TextChanged += textBoxAuthor_TextChanged;
            // 
            // textBoxYear
            // 
            textBoxYear.Location = new Point(228, 253);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.Size = new Size(327, 39);
            textBoxYear.TabIndex = 7;
            textBoxYear.TextChanged += textBoxYear_TextChanged;
            // 
            // buttonBrowser
            // 
            buttonBrowser.Location = new Point(228, 323);
            buttonBrowser.Name = "buttonBrowser";
            buttonBrowser.Size = new Size(327, 46);
            buttonBrowser.TabIndex = 8;
            buttonBrowser.Text = "Browser";
            buttonBrowser.UseVisualStyleBackColor = true;
            buttonBrowser.Click += buttonBrowser_Click;
            // 
            // buttonUpload
            // 
            buttonUpload.Location = new Point(35, 431);
            buttonUpload.Name = "buttonUpload";
            buttonUpload.Size = new Size(175, 46);
            buttonUpload.TabIndex = 9;
            buttonUpload.Text = "Upload";
            buttonUpload.UseVisualStyleBackColor = true;
            buttonUpload.Click += buttonUpload_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(240, 431);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(175, 46);
            buttonReset.TabIndex = 10;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonBack
            // 
            buttonBack.Location = new Point(463, 431);
            buttonBack.Name = "buttonBack";
            buttonBack.RightToLeft = RightToLeft.No;
            buttonBack.Size = new Size(175, 46);
            buttonBack.TabIndex = 11;
            buttonBack.Text = "Back";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // EduLibAdmin1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(774, 529);
            Controls.Add(buttonBack);
            Controls.Add(buttonReset);
            Controls.Add(buttonUpload);
            Controls.Add(buttonBrowser);
            Controls.Add(textBoxYear);
            Controls.Add(textBoxAuthor);
            Controls.Add(textBoxTitle);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EduLibAdmin1";
            Text = "EduLibAdmin1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxTitle;
        private TextBox textBoxAuthor;
        private TextBox textBoxYear;
        private Button buttonBrowser;
        private Button buttonUpload;
        private Button buttonReset;
        private Button buttonBack;
    }
}