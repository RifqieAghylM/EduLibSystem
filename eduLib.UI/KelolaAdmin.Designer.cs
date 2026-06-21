namespace eduLib.UI
{
    partial class KelolaAdmin
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
            buttonCari = new Button();
            textBoxSearch = new TextBox();
            label2 = new Label();
            dataGridViewBuku = new DataGridView();
            buttonBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBuku).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoEllipsis = true;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(209, 59);
            label1.Name = "label1";
            label1.Size = new Size(229, 40);
            label1.TabIndex = 0;
            label1.Text = "KELOLA BUKU";
            // 
            // buttonCari
            // 
            buttonCari.BackColor = SystemColors.ActiveCaption;
            buttonCari.Location = new Point(535, 137);
            buttonCari.Name = "buttonCari";
            buttonCari.Size = new Size(150, 46);
            buttonCari.TabIndex = 1;
            buttonCari.Text = "Cari";
            buttonCari.UseVisualStyleBackColor = false;
            buttonCari.Click += buttonCari_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(159, 141);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(355, 39);
            textBoxSearch.TabIndex = 2;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 144);
            label2.Name = "label2";
            label2.Size = new Size(116, 32);
            label2.TabIndex = 3;
            label2.Text = "Cari Buku";
            // 
            // dataGridViewBuku
            // 
            dataGridViewBuku.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBuku.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBuku.Location = new Point(21, 192);
            dataGridViewBuku.Name = "dataGridViewBuku";
            dataGridViewBuku.RowHeadersWidth = 82;
            dataGridViewBuku.Size = new Size(719, 227);
            dataGridViewBuku.TabIndex = 4;
            dataGridViewBuku.CellContentClick += dataGridViewBuku_CellContentClick;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.IndianRed;
            buttonBack.Location = new Point(535, 449);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(150, 46);
            buttonBack.TabIndex = 5;
            buttonBack.Text = "Back";
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += buttonBack_Click;
            // 
            // KelolaAdmin
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(774, 529);
            Controls.Add(buttonBack);
            Controls.Add(dataGridViewBuku);
            Controls.Add(label2);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonCari);
            Controls.Add(label1);
            Name = "KelolaAdmin";
            Text = "KelolaAdmin";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBuku).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonCari;
        private TextBox textBoxSearch;
        private Label label2;
        private DataGridView dataGridViewBuku;
        private Button buttonBack;
    }
}