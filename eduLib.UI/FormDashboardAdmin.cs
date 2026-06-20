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

        // ── MANAGE BOOK ──
        private void btnmanagebook_Click(object sender, EventArgs e)
        {
            FormManageBook formManage = new FormManageBook();
            formManage.ShowDialog();
        }

        // ── READ AND DOWNLOAD BOOK ──
        private void btnreadanddownloadbook_Click(object sender, EventArgs e)
        {
            // TODO: Read And Download Book
        }

        // ── REVIEW ──
        private void btnreview_Click(object sender, EventArgs e)
        {
            // TODO: Review
        }

        // ── HISTORY ──
        private void btnhistory_Click(object sender, EventArgs e)
        {
            // TODO: History
        }

        // ── BOOKMARK ──
        private void btnbookmark_Click(object sender, EventArgs e)
        {
            // TODO: Bookmark
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

        private void lblwelcomeadmin_Click(object sender, EventArgs e)
        {

        }
    }
}