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

        private void btnuploadbook_Click(object sender, EventArgs e)
        {
            EduLibAdmin1 uploadBook = new EduLibAdmin1();

            uploadBook.FormClosed += (s, args) => this.Show();

            uploadBook.StartPosition = FormStartPosition.CenterScreen;

            uploadBook.Show();

            this.Hide();
        }

        private void btnupdateanddeletebook_Click(object sender, EventArgs e)
        {
            KelolaAdmin updateanddeletebook = new KelolaAdmin();

            updateanddeletebook.FormClosed += (s, args) => this.Show();

            updateanddeletebook.StartPosition = FormStartPosition.CenterScreen;

            updateanddeletebook.Show();

            this.Hide();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}