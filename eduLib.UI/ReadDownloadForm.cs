using eduLib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class ReadDownloadForm : Form
    {
        // Sesuaikan URL ini dengan port saat backend kalian di-run
        private readonly string baseUrl = "http://localhost:5096/api/books";

        public ReadDownloadForm()
        {
            InitializeComponent();
        }

        // --- 1. FITUR PENCARIAN BUKU ---
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Masukkan kata kunci pencarian!");
                return;
            }

            btnSearch.Enabled = false;
            btnSearch.Text = "Mencari...";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/search?keyword={keyword}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();

                        // Parse JSON dari API Backend
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var books = JsonSerializer.Deserialize<List<Book>>(jsonResult, options);

                        dgvBooks.DataSource = books;

                        // Sembunyikan kolom ID agar UI lebih bersih
                        if (dgvBooks.Columns["Id"] != null) dgvBooks.Columns["Id"].Visible = false;
                        if(dgvBooks.Columns["GridFsFileId"] != null) dgvBooks.Columns["GridFsFileId"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengambil data dari server.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi API Error: " + ex.Message);
                }
            }

            btnSearch.Enabled = true;
            btnSearch.Text = "Cari Buku";
        }

        // --- 2. FITUR DOWNLOAD PERMANEN ---
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            // 1. Mengambil gridFsId (bukan Id biasa) dan judul dari baris yang dipilih
            if (!IsBookSelected(out string gridFsId, out string selectedTitle)) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Pilih Lokasi Penyimpanan Buku";
                saveFileDialog.FileName = $"{selectedTitle}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string targetPath = saveFileDialog.FileName;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            // 2. Tembak ke endpoint download menggunakan gridFsId kalian
                            // Sesuaikan kata "download" di bawah jika nama endpoint kalian berbeda (misal: /read/{gridFsId})
                            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/download/{gridFsId}");

                            if (response.IsSuccessStatusCode)
                            {
                                byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                                // Simpan file fisik PDF ke laptop
                                File.WriteAllBytes(targetPath, pdfBytes);

                                MessageBox.Show($"Buku \"{selectedTitle}\" berhasil diunduh dan disimpan!",
                                                "Download Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string errorDetails = await response.Content.ReadAsStringAsync();
                                MessageBox.Show($"API Menolak Request!\nStatus: {response.StatusCode}\nDetail: {errorDetails}",
                                                "Gagal Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Koneksi API Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // --- 3. FITUR BACA ONLINE (STREAM TO TEMP FILE) ---
        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (!IsBookSelected(out string selectedId, out string selectedTitle)) return;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Menggunakan endpoint /read/ sesuai API yang dibuat
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/read/{selectedId}");

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                        // Buat file sementara (Temporary File) untuk dibaca
                        string tempFile = Path.Combine(Path.GetTempPath(), $"{selectedTitle}_ReadOnline.pdf");
                        File.WriteAllBytes(tempFile, pdfBytes);

                        System.Threading.Thread thread = new System.Threading.Thread(() =>
                        {
                            // Buat dan jalankan Form baca HD di thread khusus ini
                            PdfViewerForm viewer = new PdfViewerForm(tempFile, selectedTitle);
                            System.Windows.Forms.Application.Run(viewer); // Memberikan mesin loop terpisah agar rendering Edge lancar
                        });

                        // Kunci status thread menjadi STA murni demi kebutuhan WebView2
                        thread.SetApartmentState(System.Threading.ApartmentState.STA);

                        // Nyalakan layarnya
                        thread.Start();
                    }
                    else
                    {
                        // BARIS INI YANG DITAMBAHKAN UNTUK MENGECEK ALASAN PENOLAKAN API
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"API Menolak Request!\nStatus: {response.StatusCode}\nDetail: {errorDetails}", "Error dari Server");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi API Error: " + ex.Message);
                }
            }
        }

        // --- FUNGSI BANTUAN UNTUK CEK PILIHAN TABEL ---
        private bool IsBookSelected(out string gridFsId, out string title)
        {
            gridFsId = string.Empty;
            title = string.Empty;

            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan klik salah satu buku di tabel terlebih dahulu!", "Peringatan");
                return false;
            }

            gridFsId = dgvBooks.SelectedRows[0].Cells["GridFsFileId"].Value.ToString();
            title = dgvBooks.SelectedRows[0].Cells["Title"].Value.ToString();
            // Validasi jika ternyata buku tersebut tidak punya file PDF di database
            if (string.IsNullOrWhiteSpace(gridFsId))
            {
                MessageBox.Show("Buku ini tidak memiliki file PDF terkait di database!", "Peringatan");
                return false;
            }
            return true;
        }
    }
}
