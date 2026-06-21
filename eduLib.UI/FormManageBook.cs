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

        // ── UPLOAD BOOK ──
        private void btnuploadbuku_Click(object sender, EventArgs e)
        {
            EduLibAdmin1 uploadBook = new EduLibAdmin1();

            // Set agar posisi form muncul pas di tengah layar laptop
            uploadBook.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            uploadBook.ShowDialog();
        }

        // ── EDIT BOOK ──
        private void btneditbuku_Click(object sender, EventArgs e)
        {
            KelolaAdmin editBook = new KelolaAdmin();

            // Set agar posisi form muncul pas di tengah layar laptop
            editBook.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            editBook.ShowDialog();
        }

        // ── DELETE BOOK ──
        private void btndeletebuku_Click(object sender, EventArgs e)
        {
            KelolaAdmin deleteBook = new KelolaAdmin();

            // Set agar posisi form muncul pas di tengah layar laptop
            deleteBook.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            deleteBook.ShowDialog();
        }

        // ── BACK TO DASHBOARD ──
        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}