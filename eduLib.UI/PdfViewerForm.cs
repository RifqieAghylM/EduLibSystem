using Microsoft.Web.WebView2.WinForms;

namespace eduLib.UI
{
    public partial class PdfViewerForm : Form
    {
        private WebView2 webView;
        private string _filePath;
        public PdfViewerForm(string filePath, string bookTitle)
        {
            InitializeComponent();
            this._filePath = filePath;
            this.Text = $"Reading: {bookTitle}";
            this.Width = 1000;
            this.Height = 750;
            this.StartPosition = FormStartPosition.CenterScreen;

            webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            this.Controls.Add(webView);

            // render pdf
            InitializeWebView(filePath);
        }
        private async void InitializeWebView(string filePath)
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);

                webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
                webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

                webView.CoreWebView2.Navigate(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat engine render PDF: " + ex.Message);
            }
        }
        private void PdfViewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (webView != null)
                {
                    webView.Dispose();
                    webView = null;
                }
                if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
            catch (IOException ioEx)
            {
                System.Diagnostics.Debug.WriteLine($"Gagal menghapus file temp: {ioEx.Message}");
            }
        }
    }
}
