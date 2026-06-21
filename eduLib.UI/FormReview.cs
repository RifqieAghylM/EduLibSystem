using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class FormReview : Form
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7053/api/Books";

        // 1. Tambahkan variabel untuk menyimpan peran (Role) user saat ini
        private string _currentUserRole;

        // 2. Ubah konstruktor agar menerima parameter 'userRole'
        public FormReview(string userRole)
        {
            InitializeComponent();
            _httpClient = new HttpClient();

            // Simpan role yang dikirim dari Dashboard ("Admin" atau "User")
            this._currentUserRole = userRole;

            btnSubmitReview.Click += btnSubmitReview_Click;
            btnKeHalamanLihat.Click += btnKeHalamanLihat_Click;

            // PERBAIKAN: Menggunakan nama 'BackDashboard' sesuai komponen di desainer kamu
            BackDashboard.Click += BackDashboard_Click;
        }

        // --- LOGIKA TOMBOL BACK DINAMIS ---
        private void BackDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKeHalamanLihat_Click(object sender, EventArgs e)
        {
            FormViewReviews halamanLihat = new FormViewReviews(this);
            halamanLihat.Show();
            this.Hide();
        }

        // --- LOGIKA VALIDASI SECURE CODING ---
        private bool IsInputValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var hasLetterRegex = new Regex(@"[a-zA-Z]");
            return hasLetterRegex.IsMatch(text);
        }

        // --- LOGIKA POST REVIEW ---
        private async void btnSubmitReview_Click(object sender, EventArgs e)
        {
            string title = txtBookTitle.Text.Trim();
            string username = txtUsername.Text.Trim();
            string comment = rtbComment.Text.Trim();

            if (!IsInputValid(title) || !IsInputValid(username))
            {
                MessageBox.Show("Input tidak valid! Judul Buku dan Username tidak boleh hanya berupa angka. Harus mengandung huruf!",
                                "Secure Coding Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Isi ulasan/komentar tidak boleh kosong!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "title", title },
                    { "username", username },
                    { "comment", comment }
                };
                var content = new FormUrlEncodedContent(formData);

                HttpResponseMessage response = await _httpClient.PostAsync($"{ApiBaseUrl}/review", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Review kamu berhasil dikirim!", "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBookTitle.Clear();
                    txtUsername.Clear();
                    rtbComment.Clear();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Buku dengan judul '{title}' tidak ditemukan!\n\nPastikan huruf kapital/kecil sudah benar.",
                                    "Buku Tidak Ditemukan (404)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal terhubung ke server API: {ex.Message}", "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
