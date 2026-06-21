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
        private void btnuploadbook_Click(object sender, EventArgs e)
        {
            // TODO: Upload Book
        }

        // ── EDIT BOOK ──
        private void btnupdateanddeletebook_Click(object sender, EventArgs e)
        {
            // TODO: Update & Delete Book
        }

        // ── BACK TO DASHBOARD ──
        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}