using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using eduLib.Core.Entities;
using eduLib.Infrastructure.API;

namespace eduLib.UI
{
    public partial class FormBookmark : Form
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string ApiBaseUrl = ApiHelper.GetBaseUrl();
        private bool _isBookSelectedFromTable = false;

        // 1. Variabel global untuk menyimpan peran pengakses
        private string _currentUserRole;

        // 2. Konstruktor menerima parameter role ("Admin" atau "User")
        public FormBookmark(string userRole)
        {
            InitializeComponent();

            // Simpan peran pengakses
            this._currentUserRole = userRole;

            // 3. Pasang kabel pengikat elektronik untuk tombol Back agar aktif merespon
            this.btnBackDashboard.Click += new System.EventHandler(this.btnBackDashboard_Click);

            // Pengamanan lifecycle agar memori RAM bersih saat ditutup
            this.FormClosed += (s, args) => this.Dispose();
        }

        // 4. PERBAIKAN LOGIKA TOMBOL BACK DINAMIS (Bukan cuma Close lagi!)
        private void btnBackDashboard_Click(object sender, EventArgs e)
        {
            if (this._currentUserRole != null && this._currentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Jika rolenya Admin, buka kembali Dashboard Admin
                FormDashboardAdmin adminDashboard = new FormDashboardAdmin();
                adminDashboard.Show();
            }
            else
            {
                // Jika rolenya User, buka kembali Dashboard User milik Azka
                FormDashboardUser userDashboard = new FormDashboardUser();
                userDashboard.Show();
            }

            // Tutup halaman bookmark saat ini
            this.Close();
        }

        // ===== SEARCH BUKU (SUDAH FIX BUG URL GANDA) =====
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Masukkan kata kunci pencarian (judul atau penulis).");
                return;
            }

            ResetBookSelection();

            try
            {
                // PERBAIKAN: Deteksi otomatis agar kata "/Books" tidak tumpuk ganda di URL
                string url;
                if (ApiBaseUrl.EndsWith("/Books", StringComparison.OrdinalIgnoreCase))
                {
                    url = $"{ApiBaseUrl}/search?keyword={Uri.EscapeDataString(keyword)}";
                }
                else
                {
                    url = $"{ApiBaseUrl}/Books/search?keyword={Uri.EscapeDataString(keyword)}";
                }

                var response = await _client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mencari buku: " + json);
                    dgvBooks.DataSource = null;
                    return;
                }

                var books = JsonSerializer.Deserialize<List<Book>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Book>();

                dgvBooks.DataSource = books;

                HideColumnIfExists("PdfPath");
                HideColumnIfExists("GridFsFileId");

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

        private void HideColumnIfExists(string columnName)
        {
            if (dgvBooks.Columns.Contains(columnName))
                dgvBooks.Columns[columnName].Visible = false;
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvBooks.Rows[e.RowIndex].DataBoundItem is not Book selectedBook) return;

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
                // PERBAIKAN: Menyesuaikan route path tracking agar tetap merujuk base URL murni jika ada "/Books"
                string baseUrlMurni = ApiBaseUrl.Replace("/Books", "");
                var url = $"{baseUrlMurni}/Tracking/bookmark?bookId={Uri.EscapeDataString(txtBookId.Text)}&page={page}";
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
                string baseUrlMurni = ApiBaseUrl.Replace("/Books", "");
                var url = $"{baseUrlMurni}/Tracking/bookmark?bookId={Uri.EscapeDataString(txtBookId.Text)}";
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
                string baseUrlMurni = ApiBaseUrl.Replace("/Books", "");
                var url = $"{baseUrlMurni}/Tracking/reading-progress?bookId={Uri.EscapeDataString(txtBookId.Text)}&currentPage={currentPage}&totalPage={totalPage}";
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
                string baseUrlMurni = ApiBaseUrl.Replace("/Books", "");
                var url = $"{baseUrlMurni}/Tracking/reading-progress?bookId={Uri.EscapeDataString(txtBookId.Text)}";
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