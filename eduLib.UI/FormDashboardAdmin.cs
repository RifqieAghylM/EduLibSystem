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

        private void btnreview_Click(object sender, EventArgs e)
        {

        }

        private void btnbookmark_Click(object sender, EventArgs e)
        {
           
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
