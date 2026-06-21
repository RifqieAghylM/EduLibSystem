using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormDashboardUser : Form
    {
        public FormDashboardUser()
        {
            InitializeComponent();
            this.btnreview.Click += new System.EventHandler(this.btnreview_Click);
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
        // ── REVIEW ──
        private void btnreview_Click(object sender, EventArgs e)
        {
            // 1. Buat objek FormReview sambil melempar parameter role "User"
            FormReview reviewPage = new FormReview("User");

            // 2. Set posisi agar muncul di tengah layar
            reviewPage.StartPosition = FormStartPosition.CenterScreen;

            // 3. Tampilkan form review milikmu
            reviewPage.Show();

            // 4. Sembunyikan Dashboard User agar UI tidak menumpuk berantakan
            this.Hide();
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