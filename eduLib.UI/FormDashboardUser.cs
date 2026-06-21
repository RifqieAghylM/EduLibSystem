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

        private void btnreadanddownloadbook_Click(object sender, EventArgs e)
        {
            ReadDownloadForm readDownloadPage = new ReadDownloadForm();

            readDownloadPage.FormClosed += (s, args) => this.Show();
  
            readDownloadPage.StartPosition = FormStartPosition.CenterScreen;

            readDownloadPage.Show();

            this.Hide();
        }

        private void btnbookmark_Click(object sender, EventArgs e)
        {
            FormBookmark bookmarkPage = new FormBookmark("User");
            bookmarkPage.FormClosed += (s, args) => this.Show();
            bookmarkPage.StartPosition = FormStartPosition.CenterScreen;
            bookmarkPage.Show();
            this.Hide();
        }

        private void btnreview_Click(object sender, EventArgs e)
        {
            FormReview reviewPage = new FormReview("User");

            reviewPage.FormClosed += (s, args) => this.Show();

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
