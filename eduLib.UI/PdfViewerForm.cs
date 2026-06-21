using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace eduLib.UI
{
    public partial class PdfViewerForm : Form
    {
        // Ganti WebView2 dengan WebBrowser bawaan Windows .NET Framework (Anti-Blank)
        private WebView2 webBrowser;
        private string _filePath;

        public PdfViewerForm(string filePath, string bookTitle)
        {
            InitializeComponent();
            this._filePath = filePath;
            this.Text = $"Reading: {bookTitle}";
            this.Width = 1000;
            this.Height = 750;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inisialisasi Kontrol WebBrowser Standar
            webBrowser = new WebView2();
            webBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(webBrowser);

            // Jalankan penayangan dokumen
            InitializePdfView(filePath);
        }

        private async void InitializePdfView(string filePath)
        {
            try
            {
                // validasi file PDF sebelum ditampilkan
                if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                {
                    MessageBox.Show("File PDF tidak ditemukan atau datanya kosong dari server API!",
                                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string pdfUri = new Uri(filePath).AbsoluteUri;

                string fullScreenPdfUri = $"{pdfUri}#view=FitH";

                await webBrowser.EnsureCoreWebView2Async(null);
                webBrowser.CoreWebView2.Settings.AreDevToolsEnabled = false;
                webBrowser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                webBrowser.CoreWebView2.Navigate(fullScreenPdfUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat dokumen PDF: " + ex.Message, "Render Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PdfViewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Bersihkan komponen dari memori RAM
                if (webBrowser != null)
                {
                    webBrowser.Dispose();
                }

                if (File.Exists(this._filePath))
                {
                    File.Delete(this._filePath);
                }
            }
            catch (IOException ioEx)
            {
                System.Diagnostics.Debug.WriteLine($"Gagal menghapus file temp: {ioEx.Message}");
            }
        }
    }
}