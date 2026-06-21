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
            
        }

        private void btnupdateanddeletebook_Click(object sender, EventArgs e)
        {
         
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}