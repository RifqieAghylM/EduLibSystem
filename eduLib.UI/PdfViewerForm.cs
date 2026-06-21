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

            // Inisialisasi WebView2
            webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            this.Controls.Add(webView);

            // Jalankan fungsi asinkronus untuk merender PDF
            InitializeWebView(filePath);
        }
        private async void InitializeWebView(string filePath)
        {
            try
            {
                // Tunggu mesin Edge Chromium di latar belakang siap
                await webView.EnsureCoreWebView2Async(null);
                // Matikan klik kanan dan tombol F12 biar user gak bisa inspect engine browser
                webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
                webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                // Buka file PDF lokal dengan kualitas tajam murni
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
                // PENTING: Dispose WebView2 terlebih dahulu untuk melepaskan file lock
                if (webView != null)
                {
                    webView.Dispose();
                    webView = null;
                }
                // Hapus file temporary secara aman
                if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
            catch (IOException ioEx)
            {
                // Jangan biarkan catch kosong. Minimal catat ke Debug Log untuk tracing jika gagal
                System.Diagnostics.Debug.WriteLine($"Gagal menghapus file temp: {ioEx.Message}");
            }
        }
    }
}
