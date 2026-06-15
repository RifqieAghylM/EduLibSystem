using System.Text.Json;
using eduLib.Core.Entities;
using eduLib.Core.Enums;
using eduLib.Application.Auth;
using eduLib.Application.Search;
using eduLib.Application.Tracking;
using eduLib.Infrastructure.Storage;
using eduLib.Infrastructure.API;

namespace eduLib.ConsoleDemo
{
    class Program
    {
        // ── STATE GLOBAL ──
        static User _currentUser = null;
        static AuthService _authService;
        static FileRepository<Book> _fileRepo;
        static SearchService _searchService;
        static ReviewApiConnector _apiConnector;
        static ReadingStateMachine _readingMachine;
        static BookmarkManager _bookmarkMgr;
        static string _dbPath = "edulib_db.json";

        static async Task Main(string[] args)
        {
            InisialisasiSistem();

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     SELAMAT DATANG DI EDULIB         ║");
            Console.WriteLine("║     Sistem Perpustakaan Digital      ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            bool jalan = true;
            while (jalan)
            {
                if (_currentUser == null)
                {
                    // Belum login → tampilkan menu login
                    Console.WriteLine("\n─────────────────────────────────────");
                    Console.WriteLine("  1. Login");
                    Console.WriteLine("  0. Keluar");
                    Console.WriteLine("─────────────────────────────────────");
                    Console.Write("Pilih: ");
                    string pilihan = Console.ReadLine();

                    switch (pilihan)
                    {
                        case "1":
                            ProsesLogin();
                            break;
                        case "0":
                            jalan = false;
                            break;
                        default:
                            Console.WriteLine("❌ Pilihan tidak valid.");
                            break;
                    }
                }
                else
                {
                    // Sudah login → tampilkan menu utama
                    TampilMenuUtama();
                    Console.Write("Pilih: ");
                    string pilihan = Console.ReadLine();
                    await ProsesMenu(pilihan);

                    if (pilihan == "0")
                        jalan = false;
                }
            }

            Console.WriteLine("\nTerima kasih telah menggunakan EduLib!");
            Console.ReadLine();
        }

        // INISIALISASI
        static void InisialisasiSistem()
        {
            // Data user (simulasi database user)
            var users = new List<User>
            {
                new User { Username = "admin",  Password = "admin123", UserRole = Role.Admin },
                new User { Username = "siswa",  Password = "siswa123", UserRole = Role.Pelajar },
                new User { Username = "guru",   Password = "guru123",  UserRole = Role.Guru }
            };

            _authService = new AuthService(users);
            _fileRepo = new FileRepository<Book>(50);
            _searchService = new SearchService();
            _apiConnector = new ReviewApiConnector();
            _readingMachine = new ReadingStateMachine();
            _bookmarkMgr = new BookmarkManager();

            // Load data buku dari JSON
            var bukuDariJson = LoadDatabase(_dbPath);
            foreach (var b in bukuDariJson)
            {
                try { _fileRepo.Upload(b); } catch { }
            }
        }

        // ══════════════════════════════════════
        // LOGIN - Modul Azka
        // ══════════════════════════════════════
        static void ProsesLogin()
        {
            Console.WriteLine("\n─────────────────────────────────────");
            Console.WriteLine("             LOGIN EDULIB             ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("Username : ");
            string username = Console.ReadLine();
            Console.Write("Password : ");
            string password = Console.ReadLine();

            try
            {
                _currentUser = _authService.Login(username, password);
                Console.WriteLine($"\n✅ Login berhasil! Selamat datang, {_currentUser.Username}");
                Console.WriteLine($"   Role    : {_currentUser.UserRole}");

                var menus = _authService.GetUserMenus(_currentUser);
                Console.WriteLine($"   Akses   : {string.Join(", ", menus)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Login gagal: {ex.Message}");
            }
        }

        // ══════════════════════════════════════
        // MENU UTAMA
        // ══════════════════════════════════════
        static void TampilMenuUtama()
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine($"║  Login sebagai: {_currentUser.Username,-22}║");
            Console.WriteLine($"║  Role         : {_currentUser.UserRole,-22}║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  MENU UTAMA                          ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Lihat Semua Buku                 ║");
            Console.WriteLine("║  2. Cari Buku                        ║");
            Console.WriteLine("║  3. Kirim Ulasan Buku                ║");
            Console.WriteLine("║  4. Update Progres Membaca           ║");
            Console.WriteLine("║  5. Simpan Bookmark                  ║");
            Console.WriteLine("║  6. Lihat Bookmark                   ║");

            // Menu khusus Admin
            if (_currentUser.UserRole == Role.Admin)
            {
                Console.WriteLine("║  7. Upload Buku Baru [ADMIN]         ║");
            }

            Console.WriteLine("║  L. Logout                           ║");
            Console.WriteLine("║  0. Keluar                           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        }

        // PROSES MENU
        static async Task ProsesMenu(string pilihan)
        {
            Console.WriteLine();
            switch (pilihan.ToUpper())
            {
                case "1": LihatSemuaBuku(); break;
                case "2": CariBuku(); break;
                case "3": await KirimUlasan(); break;
                case "4": UpdateProgresMembaca(); break;
                case "5": SimpanBookmark(); break;
                case "6": LihatBookmark(); break;
                case "7":
                    if (_currentUser.UserRole == Role.Admin)
                        UploadBuku();
                    else
                        Console.WriteLine("❌ Akses ditolak. Fitur ini hanya untuk Admin.");
                    break;
                case "L": Logout(); break;
                case "0": break;
                default:
                    Console.WriteLine("❌ Pilihan tidak valid.");
                    break;
            }
        }

        // FITUR 1: Lihat Semua Buku - Modul Rifki
        static void LihatSemuaBuku()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("         DAFTAR KOLEKSI BUKU         ");
            Console.WriteLine("─────────────────────────────────────");
            var semuaBuku = _fileRepo.GetAll();
            if (semuaBuku.Count == 0)
            {
                Console.WriteLine("Belum ada buku dalam koleksi.");
                return;
            }
            foreach (var b in semuaBuku)
                Console.WriteLine($"  [{b.Id}] {b.Title} - {b.Author} ({b.Year})");
            Console.WriteLine($"\nTotal: {semuaBuku.Count} buku");
        }

        // FITUR 2: Cari Buku - Modul Rifqie
        static void CariBuku()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("           CARI BUKU                 ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("Masukkan kata kunci (judul/penulis): ");
            string keyword = Console.ReadLine();

            try
            {
                var hasil = _searchService.FindBooks(
                    _fileRepo.GetAll(),
                    b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                         b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                );

                if (hasil.Count == 0)
                {
                    Console.WriteLine($"Tidak ada buku dengan kata kunci '{keyword}'.");
                    return;
                }

                Console.WriteLine($"\nDitemukan {hasil.Count} buku:");
                foreach (var b in hasil)
                    Console.WriteLine($"  [{b.Id}] {b.Title} - {b.Author} ({b.Year})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // FITUR 3: Kirim Ulasan - Modul Rifqie (API)
        static async Task KirimUlasan()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("          KIRIM ULASAN BUKU          ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("Judul buku yang direview : ");
            string judul = Console.ReadLine();
            Console.Write("Ulasan Anda              : ");
            string ulasan = Console.ReadLine();

            try
            {
                bool berhasil = await _apiConnector.SendReviewAsync(judul, ulasan);
                Console.WriteLine(berhasil
                    ? $"✅ Ulasan berhasil dikirim untuk buku '{judul}'"
                    : "❌ Ulasan gagal dikirim (komentar tidak boleh kosong).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // FITUR 4: Update Progres - Modul Tegar
        static void UpdateProgresMembaca()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("       UPDATE PROGRES MEMBACA        ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("Halaman saat ini : ");
            string inputHalaman = Console.ReadLine();
            Console.Write("Total halaman    : ");
            string inputTotal = Console.ReadLine();

            try
            {
                int halaman = int.Parse(inputHalaman);
                int total = int.Parse(inputTotal);

                _readingMachine.UpdateProgress(halaman, total);
                Console.WriteLine($"\n✅ Status membaca : {_readingMachine.CurrentState}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // FITUR 5: Simpan Bookmark - Modul Tegar
        static void SimpanBookmark()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("          SIMPAN BOOKMARK            ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("ID Buku  : ");
            string bookId = Console.ReadLine();
            Console.Write("Halaman  : ");
            string inputHal = Console.ReadLine();

            try
            {
                int halaman = int.Parse(inputHal);
                _bookmarkMgr.SaveBookmark(bookId, halaman);
                Console.WriteLine($"\n✅ Bookmark disimpan: Buku {bookId} halaman {halaman}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // FITUR 6: Lihat Bookmark - Modul Tegar
        static void LihatBookmark()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("           LIHAT BOOKMARK            ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("ID Buku: ");
            string bookId = Console.ReadLine();

            try
            {
                int halaman = _bookmarkMgr.GetBookmark(bookId);
                Console.WriteLine(halaman == 0
                    ? $"Belum ada bookmark untuk buku '{bookId}'."
                    : $"\n✅ Bookmark buku '{bookId}': Halaman {halaman}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // FITUR 7: Upload Buku - Modul Rifki (Admin)
        static void UploadBuku()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("       UPLOAD BUKU BARU [ADMIN]      ");
            Console.WriteLine("─────────────────────────────────────");
            Console.Write("ID Buku  : ");
            string id = Console.ReadLine();
            Console.Write("Judul    : ");
            string judul = Console.ReadLine();
            Console.Write("Penulis  : ");
            string penulis = Console.ReadLine();
            Console.Write("Tahun    : ");
            string inputTahun = Console.ReadLine();

            try
            {
                int tahun = int.Parse(inputTahun);
                var bukuBaru = new Book
                {
                    Id = id,
                    Title = judul,
                    Author = penulis,
                    Year = tahun,
                    PdfPath = $"C:/eduLib/Uploads/{id}.pdf"
                };

                _fileRepo.Upload(bukuBaru);
                SaveDatabase(_dbPath, _fileRepo.GetAll());
                Console.WriteLine($"\n✅ Buku '{judul}' berhasil diunggah dan disimpan ke database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        // LOGOUT - Modul Azka
        static void Logout()
        {
            _authService.Logout();
            Console.WriteLine($"✅ {_currentUser.Username} berhasil logout.");
            _currentUser = null;
            _readingMachine = new ReadingStateMachine();
        }

        // JSON DATABASE HELPER
        static List<Book> LoadDatabase(string path)
        {
            if (!File.Exists(path))
            {
                var dummy = new List<Book>
                {
                    new Book { Id = "B001", Title = "Buku Panduan C#", Author = "Azka",  Year = 2024, PdfPath = "C:/eduLib/Uploads/B001.pdf" },
                    new Book { Id = "B002", Title = "Clean Code",      Author = "Rifki", Year = 2023, PdfPath = "C:/eduLib/Uploads/B002.pdf" }
                };
                SaveDatabase(path, dummy);
                return dummy;
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        static void SaveDatabase(string path, List<Book> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(data, options));
        }
    }
}