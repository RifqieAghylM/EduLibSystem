using eduLib.Core.Entities;
using System.Text.Json;
using eduLib.Infrastructure.API;


namespace eduLib.UI
{
    public partial class ReadDownloadForm : Form
    {
        private readonly string baseUrl = ApiHelper.GetBaseUrl();
        private static readonly HttpClient client = new HttpClient();
        public ReadDownloadForm()
        {
            InitializeComponent();
        }

        // fitur search buku
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Masukkan kata kunci pencarian!");
                return;
            }

            btnSearch.Enabled = false;
            btnSearch.Text = "Searching...";

            try
            {
                // Keamanan: Proteksi URL dari karakter aneh/ilegal via EscapeDataString
                string safeKeyword = Uri.EscapeDataString(keyword);
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/search?keyword={safeKeyword}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // Parse JSON dari API Backend
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var books = JsonSerializer.Deserialize<List<Book>>(jsonResult, options);

                    dgvBooks.DataSource = books;

                    // Sembunyikan kolom ID agar UI lebih bersih
                    if (dgvBooks.Columns["Id"] != null) dgvBooks.Columns["Id"].Visible = false;
                    if (dgvBooks.Columns["GridFsFileId"] != null) dgvBooks.Columns["GridFsFileId"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Gagal mengambil data dari server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Koneksi API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnSearch.Enabled = true;
            btnSearch.Text = "Cari Buku";
        }

        // fitur download buku
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
                    btnDownload.Enabled = false; // Cegah spam-klik

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync($"{baseUrl}/download/{gridFsId}");

                        if (response.IsSuccessStatusCode)
                        {
                            byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                            await File.WriteAllBytesAsync(targetPath, pdfBytes); // Non-blocking I/O

                            MessageBox.Show($"Buku \"{selectedTitle}\" berhasil diunduh!", "Download Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string errorDetails = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"API Menolak Request!\nStatus: {response.StatusCode}\nDetail: {errorDetails}", "Gagal Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Koneksi API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btnDownload.Enabled = true;
                    }
                }
            }
        }
        // button kembali ke halaman sebelumnya
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Menutup halaman eksplorasi ini dan otomatis kembali ke Menu Utama yang memanggilnya
            this.Close();
        }

        // fitur read
        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (!IsBookSelected(out string selectedId, out string selectedTitle)) return;
            btnRead.Enabled = false;

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/read/{selectedId}");

                if (response.IsSuccessStatusCode)
                {
                    byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                    // Keamanan & Robustness: Pastikan judul bersih dari karakter ilegal Windows (seperti :, \, / dll)
                    string safeTitle = GetSafeFilename(selectedTitle);
                    string tempFile = Path.Combine(Path.GetTempPath(), $"{safeTitle}_ReadOnline.pdf");

                    await File.WriteAllBytesAsync(tempFile, pdfBytes);

                    // Panggil langsung di UI Thread. WebView2 asinkronus dari sananya, jadi tidak akan macet.
                    PdfViewerForm viewer = new PdfViewerForm(tempFile, selectedTitle);
                    viewer.Show();
                }
                else
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"API Menolak Request!\nStatus: {response.StatusCode}\nDetail: {errorDetails}", "Error dari Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Koneksi API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRead.Enabled = true;
            }
        }

        // cek apakah buku terpilih dan memiliki file PDF terkait di database
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
        // Fungsi membersihkan karakter ilegal Windows untuk nama file
        private string GetSafeFilename(string filename)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }
            return filename;
        }
    }
}