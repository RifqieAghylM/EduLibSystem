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
        
        // Menampung referensi objek form pemanggil
        private readonly FormReview _formAsal;

        // Konstruktor default (tetap dipertahankan agar desainer WinForms tidak error)
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
            
            // Mendaftarkan tombol back untuk navigasi pulang
            btnBack.Click += btnBack_Click;
        }

        // --- EVENT HANDLER NAVIGASI: Kembali ke FormReview ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_formAsal != null)
            {
                // Memunculkan kembali halaman input ulasan yang disembunyikan
                _formAsal.Show();
            }
            // Menutup form pencarian ini, kembali dengan aman ke FormReview
            this.Close();
        }

        private bool IsInputValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var hasLetterRegex = new Regex(@"[a-zA-Z]");
            return hasLetterRegex.IsMatch(text);
        }

        /// <summary>
        /// EVENT HANDLER: Mencari Ulasan Fleksibel Berdasarkan Judul / Penulis (Parameterization di level GUI)
        /// </summary>
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
                // 1. Parameterization Tahap 1: Tembak endpoint /search milikmu untuk mencari buku secara fleksibel
                string searchUrl = $"{ApiBaseUrl}/search?keyword={Uri.EscapeDataString(searchKeyword)}";
                HttpResponseMessage searchResponse = await _httpClient.GetAsync(searchUrl);

                if (!searchResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Terjadi kesalahan sistem saat menarik data dari backend.", "Error (400)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchJson = await searchResponse.Content.ReadAsStringAsync();
                var foundBooks = JsonConvert.DeserializeObject<List<dynamic>>(searchJson);

                // Jika kata kunci judul/penulis tidak menghasilkan buku apapun di katalog
                if (foundBooks == null || foundBooks.Count == 0)
                {
                    MessageBox.Show($"Buku atau Penulis dengan kata kunci '{searchKeyword}' tidak ditemukan di sistem!",
                                    "Tidak Ditemukan (404)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool totalReviewsFound = false;

                // 2. Parameterization Tahap 2: Looping semua buku yang cocok (Bisa banyak buku jika mencari nama penulis)
                foreach (var book in foundBooks)
                {
                    string bookTitle = book.title;

                    // Tembak endpoint ulasan asli berdasarkan judul buku yang didapatkan otomatis dari hasil search
                    string reviewUrl = $"{ApiBaseUrl}/{bookTitle}/reviews";
                    HttpResponseMessage reviewResponse = await _httpClient.GetAsync(reviewUrl);

                    if (reviewResponse.IsSuccessStatusCode)
                    {
                        string reviewJson = await reviewResponse.Content.ReadAsStringAsync();
                        var reviews = JsonConvert.DeserializeObject<List<dynamic>>(reviewJson);

                        if (reviews != null && reviews.Count > 0)
                        {
                            totalReviewsFound = true;
                            var bookInfo = reviews[0].bookDetails;

                            // FORMAT TAMPILAN PERSIS SEPERTI FOTO KAMU
                            lstReviewsDisplay.Items.Add($"=== DETAIL BUKU ===");
                            lstReviewsDisplay.Items.Add($"Judul   : {bookInfo.title}");
                            lstReviewsDisplay.Items.Add($"Penulis : {bookInfo.author}");
                            lstReviewsDisplay.Items.Add($"Tahun   : {bookInfo.year}");
                            lstReviewsDisplay.Items.Add("=========================================================");
                            lstReviewsDisplay.Items.Add("");

                            foreach (var r in reviews)
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
                            
                            // Beri jarak pemisah ekstra jika penulis memiliki lebih dari 1 buku
                            lstReviewsDisplay.Items.Add(new string('-', 65));
                            lstReviewsDisplay.Items.Add("");
                        }
                    }
                }

                // Jika buku/penulisnya ada di katalog tapi belum ada yang pernah kasih review
                if (!totalReviewsFound)
                {
                    lstReviewsDisplay.Items.Add("Buku ditemukan di katalog, tetapi belum memiliki ulasan.");
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