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

        private void btnreadanddownloadbook_Click(object sender, EventArgs e)
        {
            // Instansiasi/membuat objek dari form halaman milikmu
            ReadDownloadForm readDownloadPage = new ReadDownloadForm();

            // Set agar posisi form muncul pas di tengah layar laptop
            readDownloadPage.StartPosition = FormStartPosition.CenterScreen;

            // Tampilkan sebagai Dialog (mengunci menu utama di belakangnya)
            readDownloadPage.ShowDialog();
        }

        private void btnbookmark_Click(object sender, EventArgs e)
        {
   
        }

        // ── HISTORY ──
        private void btnhistory_Click(object sender, EventArgs e)
        {
            // TODO: History
        }

        private void btnreview_Click(object sender, EventArgs e)
        {
            FormReview reviewPage = new FormReview("User");

            reviewPage.StartPosition = FormStartPosition.CenterScreen;

            reviewPage.Show();

            this.Hide();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
                this.Close();
        }
    }
}
