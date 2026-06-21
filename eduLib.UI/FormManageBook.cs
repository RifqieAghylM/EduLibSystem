using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormManageBook : Form
    {
        public FormManageBook()
        {
            InitializeComponent();
        }

        private void btnuploadbook_Click(object sender, EventArgs e)
        {
            EduLibAdmin1 uploadBook = new EduLibAdmin1();

            uploadBook.FormClosed += (s, args) => this.Show();

            // Set agar posisi form muncul pas di tengah layar laptop
            uploadBook.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            uploadBook.Show();

            this.Hide();
        }

        private void btnupdateanddeletebook_Click(object sender, EventArgs e)
        {
            KelolaAdmin updateanddeletebook = new KelolaAdmin();

            updateanddeletebook.FormClosed += (s, args) => this.Show();

            // Set agar posisi form muncul pas di tengah layar laptop
            updateanddeletebook.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            updateanddeletebook.Show();

            this.Hide();
        }

        // ── BACK TO DASHBOARD ──
        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}