using System;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormDashboardAdmin : Form
    {
        public FormDashboardAdmin()
        {
            InitializeComponent();
            this.btnreview.Click += new System.EventHandler(this.btnreview_Click);

            this.btnbookmark.Click += new System.EventHandler(this.btnbookmark_Click);
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            lblwelcomeadmin.Text = "Welcome, Admin!";
        }

        private void btnmanagebook_Click(object sender, EventArgs e)
        {
            FormManageBook formManage = new FormManageBook();
            formManage.ShowDialog();
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

        // ── REVIEW ──
        private void btnreview_Click(object sender, EventArgs e)
        {
            // 1. Buat objek FormReview sambil melempar parameter role "Admin"
            FormReview reviewPage = new FormReview("Admin");

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
