using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            this.Text = $"Membaca: {bookTitle} (Mode HD)";
            this.Width = 1000; // Kita lebarkan sedikit agar puas bacanya
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
                // Begitu jendela baca ditutup, langsung hapus file PDF dari folder Temp
                if (File.Exists(this._filePath))
                {
                    File.Delete(this._filePath);
                }
            }
            catch { }
        }
    }
}
