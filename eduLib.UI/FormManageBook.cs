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
            // TODO: Upload Book
        }

        // ── EDIT BOOK ──
        private void btneditbuku_Click(object sender, EventArgs e)
        {
            // TODO: Edit Book
        }

        // ── DELETE BOOK ──
        private void btndeletebuku_Click(object sender, EventArgs e)
        {
            // TODO: Delete Book
        }

        // ── BACK TO DASHBOARD ──
        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}