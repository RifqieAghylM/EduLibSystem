using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormDashboardUser : Form
    {
        public FormDashboardUser()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            lblwelcomeuser.Text = "Welcome, User!";
        }

        // ── READ AND DOWNLOAD BOOK ──
        private void btnreadanddownloadbook_Click(object sender, EventArgs e)
        {
            // Instansiasi/membuat objek dari form halaman milikmu
            ReadDownloadForm readDownloadPage = new ReadDownloadForm();

            // Set agar posisi form muncul pas di tengah layar laptop
            readDownloadPage.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            readDownloadPage.ShowDialog();
        }

        // ── BOOKMARK ──
        private void btnbookmark_Click(object sender, EventArgs e)
        {
            // TODO: Bookmark
        }

        // ── HISTORY ──
        private void btnhistory_Click(object sender, EventArgs e)
        {
            // TODO: History
        }

        // ── REVIEW ──
        private void btnreview_Click(object sender, EventArgs e)
        {
            // TODO: Review
        }

        // ── LOGOUT ──
        private void btnlogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
                this.Close(); // FormLogin akan muncul kembali
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}