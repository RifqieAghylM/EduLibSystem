namespace eduLib.UI
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbltitle = new Label();
            lblusername = new Label();
            lblpassword = new Label();
            txtusername = new TextBox();
            txtpassword = new TextBox();
            btnlogin = new Button();
            SuspendLayout();
            // 
            // lbltitle
            // 
            lbltitle.AutoSize = true;
            lbltitle.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbltitle.Location = new Point(310, 80);
            lbltitle.Name = "lbltitle";
            lbltitle.Size = new Size(162, 50);
            lbltitle.TabIndex = 0;
            lbltitle.Text = "Edulib";
            lbltitle.Click += label1_Click;
            // 
            // lblusername
            // 
            lblusername.AutoSize = true;
            lblusername.Location = new Point(138, 200);
            lblusername.Name = "lblusername";
            lblusername.Size = new Size(100, 25);
            lblusername.TabIndex = 1;
            lblusername.Text = "Username :";
            lblusername.Click += label2_Click;
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(142, 260);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(96, 25);
            lblpassword.TabIndex = 2;
            lblpassword.Text = "Password :";
            // 
            // txtusername
            // 
            txtusername.Location = new Point(251, 200);
            txtusername.Name = "txtusername";
            txtusername.Size = new Size(350, 31);
            txtusername.TabIndex = 3;
            txtusername.TextChanged += txtusername_TextChanged;
            // 
            // txtpassword
            // 
            txtpassword.Location = new Point(251, 260);
            txtpassword.Name = "txtpassword";
            txtpassword.Size = new Size(350, 31);
            txtpassword.TabIndex = 4;
            txtpassword.TextChanged += txtpassword_TextChanged;
            // 
            // btnlogin
            // 
            btnlogin.Location = new Point(340, 340);
            btnlogin.Name = "btnlogin";
            btnlogin.Size = new Size(147, 53);
            btnlogin.TabIndex = 5;
            btnlogin.Text = "Login";
            btnlogin.UseVisualStyleBackColor = true;
            btnlogin.Click += btnlogin_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 544);
            Controls.Add(btnlogin);
            Controls.Add(txtpassword);
            Controls.Add(txtusername);
            Controls.Add(lblpassword);
            Controls.Add(lblusername);
            Controls.Add(lbltitle);
            Name = "FormLogin";
            Text = "Form Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbltitle;
        private Label lblusername;
        private Label lblpassword;
        private TextBox txtusername;
        private TextBox txtpassword;
        private Button btnlogin;
    }
}