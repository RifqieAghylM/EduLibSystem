using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormDashboardAdmin : Form
    {
        public FormDashboardAdmin()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            lblwelcomeadmin.Text = "Welcome, Admin!";
        }

        private void btnmanagebook_Click(object sender, EventArgs e)
        {
            FormManageBook formManage = new FormManageBook();
            formManage.FormClosed += (s, args) => this.Show();
            formManage.Show();
            this.Hide();
        }

        private void btnreadanddownloadbook_Click(object sender, EventArgs e)
        {
            ReadDownloadForm readDownloadPage = new ReadDownloadForm();

            readDownloadPage.FormClosed += (s, args) => this.Show();

            readDownloadPage.StartPosition = FormStartPosition.CenterScreen;

            readDownloadPage.Show();

            this.Hide();
        }

        // ── REVIEW ──
        private void btnreview_Click(object sender, EventArgs e)
        {
            // 1. Buat objek FormReview sambil melempar parameter role "Admin"
            FormReview reviewPage = new FormReview("Admin");

            reviewPage.FormClosed += (s, args) => this.Show();

            // 2. Set posisi agar muncul di tengah layar
            reviewPage.StartPosition = FormStartPosition.CenterScreen;

            // 3. Tampilkan form review milikmu
            reviewPage.Show();

            // 4. Sembunyikan Dashboard Admin agar UI tidak menumpuk berantakan
            this.Hide();
        }

        private void btnbookmark_Click(object sender, EventArgs e)
        {
            // Panggil FormBookmark dengan menyertakan parameter Admin
            FormBookmark bookmarkPage = new FormBookmark("Admin");
            bookmarkPage.FormClosed += (s, args) => this.Show();
            bookmarkPage.StartPosition = FormStartPosition.CenterScreen;
            bookmarkPage.Show();
            this.Hide(); // Sembunyikan dashboard utama
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

        private void lblwelcomeadmin_Click(object sender, EventArgs e)
        {

        }
    }
}
