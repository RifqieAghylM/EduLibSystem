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
            // Menutup form pencarian ini demi efisiensi resource memori
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
            string searchTitle = txtSearchTitle.Text.Trim();

            if (!IsInputValid(searchTitle))
            {
                MessageBox.Show("Format pencarian salah! Input judul tidak boleh hanya berupa angka. Harus mengandung huruf.",
                                "Secure Coding Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstReviewsDisplay.Items.Clear();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{ApiBaseUrl}/{searchTitle}/reviews");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    var reviews = JsonConvert.DeserializeObject<List<dynamic>>(jsonResult);

                    if (reviews == null || reviews.Count == 0)
                    {
                        lstReviewsDisplay.Items.Add("Belum ada ulasan untuk buku ini. Jadilah yang pertama mengulas!");
                        return;
                    }

                    var bookInfo = reviews[0].bookDetails;

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
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Buku dengan judul '{searchTitle}' tidak ditemukan di sistem!\n\nPastikan huruf kapital/kecil sudah benar (Case-Sensitive).",
                                    "Buku Tidak Ditemukan (404)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Terjadi kesalahan sistem saat menarik data dari backend.",
                                    "Error (400)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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