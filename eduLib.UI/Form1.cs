using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace eduLib.UI
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client = new HttpClient();


        private const string BaseUrl = "https://localhost:7053/api/Tracking";

        public Form1()
        {
            InitializeComponent();
        }

        //  BOOKMARK 
        private async void btnSaveBookmark_Click(object sender, EventArgs e)
        {
            if (!ValidateBookId()) return;

            if (!int.TryParse(txtBookmarkPage.Text, out int page))
            {
                MessageBox.Show("Halaman harus berupa angka.");
                return;
            }

            try
            {
                var url = $"{BaseUrl}/bookmark?bookId={txtBookId.Text}&page={page}";
                var response = await _client.PostAsync(url, null);
                var result = await response.Content.ReadAsStringAsync();

                lblBookmarkResult.Text = response.IsSuccessStatusCode
                    ? $"Tersimpan: halaman {page}"
                    : $"Gagal: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnGetBookmark_Click(object sender, EventArgs e)
        {
            if (!ValidateBookId()) return;

            try
            {
                var url = $"{BaseUrl}/bookmark?bookId={txtBookId.Text}";
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //  READING PROGRESS 
        private async void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            if (!ValidateBookId()) return;

            if (!int.TryParse(txtCurrentPage.Text, out int currentPage) ||
                !int.TryParse(txtTotalPage.Text, out int totalPage))
            {
                MessageBox.Show("Halaman harus berupa angka.");
                return;
            }

            try
            {
                var url = $"{BaseUrl}/reading-progress?bookId={txtBookId.Text}&currentPage={currentPage}&totalPage={totalPage}";
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnGetProgress_Click(object sender, EventArgs e)
        {
            if (!ValidateBookId()) return;

            try
            {
                var url = $"{BaseUrl}/reading-progress?bookId={txtBookId.Text}";
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
                    lblProgressResult.Text = "Belum ada data progress";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ===== Helper =====
        private bool ValidateBookId()
        {
            if (string.IsNullOrWhiteSpace(txtBookId.Text))
            {
                MessageBox.Show("Book ID tidak boleh kosong.");
                return false;
            }
            return true;
        }

        private void txtBookId_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblBookmarkResult_Click(object sender, EventArgs e)
        {

        }
    }
}