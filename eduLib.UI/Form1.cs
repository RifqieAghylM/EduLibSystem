using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace eduLib.UI
{
    // Model sederhana untuk menampung hasil pencarian buku dari API
    public class BookResult
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
    }

    public partial class Form1 : Form
    {
        private readonly HttpClient _client = new HttpClient();

        // GANTI sesuai port API kamu (cek Properties/launchSettings.json di eduLib.API)
        private const string ApiBaseUrl = "https://localhost:7053/api";

        // Flag: true hanya jika BookId diisi lewat klik tabel hasil pencarian
        private bool _isBookSelectedFromTable = false;

        public Form1()
        {
            InitializeComponent();
        }

        // ===== SEARCH BUKU =====
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Masukkan kata kunci pencarian (judul atau penulis).");
                return;
            }

            // Reset pilihan setiap kali search baru dilakukan
            ResetBookSelection();

            try
            {
                var url = $"{ApiBaseUrl}/Books/search?keyword={Uri.EscapeDataString(keyword)}";
                var response = await _client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mencari buku: " + json);
                    dgvBooks.DataSource = null;
                    return;
                }

                var books = JsonSerializer.Deserialize<List<BookResult>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<BookResult>();

                dgvBooks.DataSource = books;

                if (books.Count == 0)
                {
                    MessageBox.Show("Buku tidak ditemukan di database.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal terhubung ke server. Pastikan API sedang berjalan.");
            }
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvBooks.Rows[e.RowIndex].DataBoundItem is not BookResult selectedBook) return;

            txtBookId.Text = selectedBook.Id;
            _isBookSelectedFromTable = true;
        }

        private void ResetBookSelection()
        {
            txtBookId.Text = string.Empty;
            _isBookSelectedFromTable = false;
            lblBookmarkResult.Text = "Hasil: -";
            lblProgressResult.Text = "Status: -";
            progressBar1.Value = 0;
        }

        // ===== BOOKMARK =====
        private async void btnSaveBookmark_Click(object sender, EventArgs e)
        {
            if (!ValidateBookSelected(lblBookmarkResult)) return;

            if (!int.TryParse(txtBookmarkPage.Text, out int page))
            {
                lblBookmarkResult.Text = "Gagal: Halaman harus berupa angka.";
                return;
            }
            if (page < 0)
            {
                lblBookmarkResult.Text = "Gagal: Halaman tidak boleh negatif.";
                return;
            }

            try
            {
                var url = $"{ApiBaseUrl}/Tracking/bookmark?bookId={Uri.EscapeDataString(txtBookId.Text)}&page={page}";
                var response = await _client.PostAsync(url, null);
                var result = await response.Content.ReadAsStringAsync();

                lblBookmarkResult.Text = response.IsSuccessStatusCode
                    ? $"Tersimpan: halaman {page}"
                    : $"Gagal: {result}";
            }
            catch (Exception)
            {
                lblBookmarkResult.Text = "Gagal: tidak bisa terhubung ke server.";
            }
        }

        private async void btnGetBookmark_Click(object sender, EventArgs e)
        {
            if (!ValidateBookSelected(lblBookmarkResult)) return;

            try
            {
                var url = $"{ApiBaseUrl}/Tracking/bookmark?bookId={Uri.EscapeDataString(txtBookId.Text)}";
                var response = await _client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonDocument.Parse(json);
                    var page = data.RootElement.GetProperty("bookmarkedPage").GetInt32();
                    lblBookmarkResult.Text = $"Terakhir di halaman: {page}";
                }
                else
                {
                    lblBookmarkResult.Text = $"Gagal: {json}";
                }
            }
            catch (Exception)
            {
                lblBookmarkResult.Text = "Gagal: tidak bisa terhubung ke server.";
            }
        }

        // ===== READING PROGRESS =====
        private async void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            if (!ValidateBookSelected(lblProgressResult)) return;

            if (!int.TryParse(txtCurrentPage.Text, out int currentPage) ||
                !int.TryParse(txtTotalPage.Text, out int totalPage))
            {
                lblProgressResult.Text = "Gagal: Halaman harus berupa angka.";
                return;
            }

            if (currentPage < 0 || totalPage < 0)
            {
                lblProgressResult.Text = "Gagal: Halaman tidak boleh negatif.";
                return;
            }

            if (totalPage == 0)
            {
                lblProgressResult.Text = "Gagal: Total halaman harus lebih dari 0.";
                return;
            }

            if (currentPage > totalPage)
            {
                lblProgressResult.Text = "Gagal: Halaman saat ini tidak valid.";
                return;
            }

            try
            {
                var url = $"{ApiBaseUrl}/Tracking/reading-progress?bookId={Uri.EscapeDataString(txtBookId.Text)}&currentPage={currentPage}&totalPage={totalPage}";
                var response = await _client.PostAsync(url, null);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonDocument.Parse(json);
                    var state = data.RootElement.GetProperty("state").GetString();

                    lblProgressResult.Text = $"Status: {state} ({currentPage}/{totalPage} halaman)";
                    progressBar1.Maximum = totalPage;
                    progressBar1.Value = currentPage;
                }
                else
                {
                    lblProgressResult.Text = $"Gagal: {json}";
                }
            }
            catch (Exception)
            {
                lblProgressResult.Text = "Gagal: tidak bisa terhubung ke server.";
            }
        }

        private async void btnGetProgress_Click(object sender, EventArgs e)
        {
            if (!ValidateBookSelected(lblProgressResult)) return;

            try
            {
                var url = $"{ApiBaseUrl}/Tracking/reading-progress?bookId={Uri.EscapeDataString(txtBookId.Text)}";
                var response = await _client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonDocument.Parse(json);
                    var currentPage = data.RootElement.GetProperty("currentPage").GetInt32();
                    var totalPages = data.RootElement.GetProperty("totalPages").GetInt32();
                    var state = data.RootElement.GetProperty("state").GetString();

                    lblProgressResult.Text = $"Status: {state} ({currentPage}/{totalPages} halaman)";
                    progressBar1.Maximum = totalPages;
                    progressBar1.Value = currentPage;
                }
                else
                {
                    lblProgressResult.Text = "Gagal: Belum ada data progress.";
                }
            }
            catch (Exception)
            {
                lblProgressResult.Text = "Gagal: tidak bisa terhubung ke server.";
            }
        }

        // ===== Helper =====
        // Cek bahwa BookId benar-benar dipilih lewat klik tabel, bukan cuma "tidak kosong"
        private bool ValidateBookSelected(Label resultLabel)
        {
            if (!_isBookSelectedFromTable || string.IsNullOrWhiteSpace(txtBookId.Text))
            {
                resultLabel.Text = "Gagal: Pilih buku dari hasil pencarian terlebih dahulu.";
                return false;
            }
            return true;
        }
    }
}