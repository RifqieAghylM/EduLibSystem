using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace eduLib.UI
{
    public partial class FormViewReviews : Form
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7053/api/Books";

        private readonly FormReview _formAsal;

        public FormViewReviews()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            btnSearchReview.Click += btnSearchReview_Click;
        }

        // TWEAK NAVIGASI: Overload constructor untuk menerima instansi FormReview asal
        public FormViewReviews(FormReview formAsal) : this()
        {
            _formAsal = formAsal;

            btnBack.Click += btnBack_Click;
        }

        // --- EVENT HANDLER NAVIGASI: Kembali ke FormReview ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_formAsal != null)
            {
                _formAsal.Show();
            }
            this.Close();
        }

        private bool IsInputValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var hasLetterRegex = new Regex(@"[a-zA-Z]");
            return hasLetterRegex.IsMatch(text);
        }

        private async void btnSearchReview_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearchTitle.Text.Trim();

            if (!IsInputValid(searchKeyword))
            {
                MessageBox.Show("Format pencarian salah! Input tidak boleh hanya berupa angka atau kosong. Harus mengandung huruf.",
                                "Secure Coding Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstReviewsDisplay.Items.Clear();

            try
            {
                // 1. Tembak endpoint katalog umum untuk mendapatkan daftar buku
                string searchUrl = $"{ApiBaseUrl}/search?keyword={Uri.EscapeDataString(searchKeyword)}";
                HttpResponseMessage searchResponse = await _httpClient.GetAsync(searchUrl);

                if (!searchResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Terjadi kesalahan sistem saat menarik data dari backend.", "Error (400)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchJson = await searchResponse.Content.ReadAsStringAsync();
                var foundBooks = JsonConvert.DeserializeObject<List<dynamic>>(searchJson);

                if (foundBooks == null || foundBooks.Count == 0)
                {
                    MessageBox.Show($"Buku atau Penulis dengan kata kunci '{searchKeyword}' tidak ditemukan di sistem!",
                                    "Tidak Ditemukan (404)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool totalReviewsFound = false;

                // 2. Looping setiap buku yang ditemukan di katalog
                foreach (var book in foundBooks)
                {
                    string currentTargetTitle = book.title;

                    // Ambil semua ulasan yang ditarik dari backend
                    string reviewUrl = $"{ApiBaseUrl}/{Uri.EscapeDataString(currentTargetTitle)}/reviews";
                    HttpResponseMessage reviewResponse = await _httpClient.GetAsync(reviewUrl);

                    if (reviewResponse.IsSuccessStatusCode)
                    {
                        string reviewJson = await reviewResponse.Content.ReadAsStringAsync();
                        var allReviews = JsonConvert.DeserializeObject<List<dynamic>>(reviewJson);

                        if (allReviews != null && allReviews.Count > 0)
                        {
                            var filteredReviews = new List<dynamic>();
                            foreach (var r in allReviews)
                            {
                                string reviewBookTitle = r.bookDetails?.title;
                                if (reviewBookTitle != null && reviewBookTitle.Equals(currentTargetTitle, StringComparison.OrdinalIgnoreCase))
                                {
                                    filteredReviews.Add(r);
                                }
                            }

                            if (filteredReviews.Count > 0)
                            {
                                totalReviewsFound = true;
                                var bookInfo = filteredReviews[0].bookDetails;

                                lstReviewsDisplay.Items.Add($"=== DETAIL BUKU ===");
                                lstReviewsDisplay.Items.Add($"Judul   : {bookInfo.title}");
                                lstReviewsDisplay.Items.Add($"Penulis : {bookInfo.author}");
                                lstReviewsDisplay.Items.Add($"Tahun   : {bookInfo.year}");
                                lstReviewsDisplay.Items.Add("=========================================================");
                                lstReviewsDisplay.Items.Add("");

                                // Tampilkan komentar ulasan yang sudah terfilter murni
                                foreach (var r in filteredReviews)
                                {
                                    string username = r.username;
                                    string comment = r.comment;
                                    string dateStr = r.date;

                                    if (DateTime.TryParse(dateStr, out DateTime parsedDate))
                                    {
                                        lstReviewsDisplay.Items.Add($"[{parsedDate.ToString("dd-MM-yyyy HH:mm")}] @{username} menulis:");
                                    }
                                    else
                                    {
                                        lstReviewsDisplay.Items.Add($"[@{username} menulis:]");
                                    }

                                    lstReviewsDisplay.Items.Add($"   > \"{comment}\"");
                                    lstReviewsDisplay.Items.Add("");
                                }

                                lstReviewsDisplay.Items.Add(new string('-', 65));
                                lstReviewsDisplay.Items.Add("");
                            }
                        }
                    }
                }

                if (!totalReviewsFound)
                {
                    lstReviewsDisplay.Items.Add("Buku ditemukan di katalog, tetapi belum memiliki ulasan yang sesuai.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal terhubung ke server backend: {ex.Message}",
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}