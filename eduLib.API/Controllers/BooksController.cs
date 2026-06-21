using eduLib.Core.Entities;
using eduLib.Core.Enums;
using eduLib.Infrastructure.Storage;
using Microsoft.AspNetCore.Mvc;
using eduLib.Application.Auth;        
using eduLib.Application.Tracking;
using System.Text.RegularExpressions;

namespace eduLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MongoBookRepository _repo;

        public BooksController(IConfiguration config)
        {
            string mongoAtlasConnString = config.GetConnectionString("MongoAtlas"); // koneksi db
            _repo = new MongoBookRepository(mongoAtlasConnString, "book");
        }
        // fitur search
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string keyword)
        {
            var results = await _repo.SearchBooksAsync(keyword);
            return Ok(results); // Mengembalikan JSON Response secara otomatis
        }

        // fitur upload buku baru
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromForm] string title, [FromForm] string author, [FromForm] int year, IFormFile pdfFile)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title wajib diisi.");

            if (string.IsNullOrWhiteSpace(author))
                return BadRequest("Author wajib diisi.");

            // Title tidak boleh hanya angka
            if (title.All(char.IsDigit))
                return BadRequest("Title tidak boleh hanya berisi angka.");

            // Author tidak boleh hanya angka
            if (author.All(char.IsDigit))
                return BadRequest("Author tidak boleh hanya berisi angka.");

            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("File tidak valid.");

            string extension = Path.GetExtension(pdfFile.FileName).ToLower();

            string[] allowedExtensions =
            {
                ".pdf"
            };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Hanya file PDF yang diperbolehkan.");

            bool titleExists = await _repo.TitleExistsAsync(title);

            if (titleExists)
            {
                return BadRequest("Judul buku sudah digunakan.");
            }

            Book bookMetadata = new Book
            {
                Title = title.Trim(),
                Author = author.Trim(),
                Year = year
            };

            using var stream = pdfFile.OpenReadStream();

            var bookId = await _repo.UploadBookAsync(
                bookMetadata,
                stream,
                pdfFile.FileName);

            return Ok(new
            {
                Message = "Upload Sukses",
                BookId = bookId
            });
        }

        // fitur update buku
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromForm] string title, [FromForm] string author, [FromForm] int year)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Judul buku wajib diisi.");

            if (title.All(char.IsDigit))
                return BadRequest("Judul buku tidak boleh hanya angka.");

            if (string.IsNullOrWhiteSpace(author))
                return BadRequest("Nama author wajib diisi.");

            if (author.All(char.IsDigit))
                return BadRequest("Nama author tidak boleh hanya angka.");

            var updatedBook = new Book
            {
                Title = title.Trim(),
                Author = author.Trim(),
                Year = year
            };

            var success = await _repo.UpdateBookAsync(id, updatedBook);

            if (!success)
                return NotFound("Buku tidak ditemukan.");

            return Ok(new
            {
                Message = "Metadata buku berhasil diperbarui."
            });
        }
        

        // fitur delete buku
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var isDeleted = await _repo.DeleteBookAsync(id);

            if (!isDeleted)
                return NotFound(new { Message = "Buku tidak ditemukan." });

            return Ok(new { Message = "Buku beserta file PDF berhasil dihapus secara permanen." });
        }

        // --- 1. MEMBUAT REVIEW ---
        [HttpPost("review")]
        public async Task<IActionResult> AddReview([FromForm] string title, [FromForm] string username, [FromForm] string comment)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(username))
                return BadRequest("Judul buku (title) dan Username wajib diisi!");

            var hasLetterRegex = new Regex(@"[a-zA-Z]");
            if (!hasLetterRegex.IsMatch(title) || !hasLetterRegex.IsMatch(username))
            {
                return BadRequest("Input tidak boleh hanya berupa angka! Harus mengandung huruf.");
            }

            var cleanTitle = title.Trim();

            // 1. Ambil data kandidat buku dari database
            var targetBook = await _repo.SearchBooksAsync(cleanTitle);

            // 2. PERBAIKAN: Menggunakan '==' agar C# mengecek kapital secara MUTLAK (Case-Sensitive)
            // Jika "Apakah ini" == "apakah ini", hasilnya akan FALSE (Buku dianggap tidak ketemu)
            var book = targetBook.FirstOrDefault(b => b.Title == cleanTitle);

            if (book == null)
                return NotFound(new { Message = $"Buku dengan judul '{title}' tidak ditemukan di sistem. (Pastikan huruf kapital/kecil sudah benar)" });

            var newReview = new Review
            {
                BookId = book.Id,
                BookTitle = book.Title,
                BookAuthor = book.Author,
                BookYear = book.Year,
                Username = username.Trim(),
                Comment = comment,
                Date = DateTime.UtcNow
            };

            await _repo.AddReviewAsync(newReview);
            return Ok(new { Message = "Review berhasil disimpan!", Data = newReview });
        }

        // --- 2. MENAMPILKAN REVIEW ---
        [HttpGet("{title}/reviews")]
        public async Task<IActionResult> GetBookReviews([FromRoute] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Masukkan judul buku untuk melihat ulasannya.");

            var hasLetterRegex = new Regex(@"[a-zA-Z]");
            if (!hasLetterRegex.IsMatch(title))
            {
                return BadRequest("Input judul tidak boleh hanya berupa angka. Harus mengandung huruf.");
            }

            var cleanTitle = title.Trim();

            // 1. Ambil data kandidat buku dari database
            var targetBook = await _repo.SearchBooksAsync(cleanTitle);

            // 2. PERBAIKAN: Menggunakan '==' agar pencarian review juga wajib sama persis kapitalnya
            var book = targetBook.FirstOrDefault(b => b.Title == cleanTitle);

            if (book == null)
            {
                return NotFound(new { Message = $"Buku dengan judul '{title}' tidak ditemukan di dalam sistem. (Pastikan huruf kapital/kecil sudah benar)" });
            }

            // 3. Ambil review berdasarkan judul buku yang dicari client
            var reviews = await _repo.GetReviewsByBookTitleAsync(cleanTitle);

            var formattedResult = reviews.Select(r => new {
                IdReview = r.Id,
                Username = r.Username,
                Comment = r.Comment,
                Date = r.Date,
                BookDetails = new
                {
                    Title = r.BookTitle,
                    Author = r.BookAuthor,
                    Year = r.BookYear
                }
            });

            return Ok(formattedResult);
        }

        [HttpGet("download/{gridFsId}")] // fitur download buku
        public async Task<IActionResult> DownloadBookPdf(string gridFsId)
        {
            if (string.IsNullOrWhiteSpace(gridFsId) || gridFsId.Length != 24)
            {
                return BadRequest(new { Message = "Pre-condition gagal: ID GridFS harus berupa 24 karakter heksadesimal." });
            }
            try
            {
                var fileBytes = await _repo.DownloadPdfAsync(gridFsId);
                if (fileBytes == null || fileBytes.Length == 0)
                {
                    return StatusCode(500, new { Message = "Post-condition gagal: Berkas berhasil ditarik, tetapi data kosong (corrupt)." });
                }
                return File(fileBytes, "application/pdf", "downloaded_book.pdf");
            }
            catch
            {
                return NotFound(new { Message = "File PDF tidak ditemukan di GridFS." });
            }
        }
        [HttpGet("read/{gridFsId}")] // fitur baca buku online
        public async Task<IActionResult> ReadBookPdfOnline(string gridFsId)
        {
            if (string.IsNullOrWhiteSpace(gridFsId) || gridFsId.Length != 24)
            {
                return BadRequest(new { Message = "Pre-condition gagal: ID GridFS tidak valid." });
            }
            try
            {
                var pdfStream = await _repo.GetPdfStreamAsync(gridFsId);
                if (pdfStream == null || pdfStream.Length == 0)
                {
                    return StatusCode(500, new { Message = "Post-condition gagal: Aliran data (stream) PDF tidak dapat diakses." });
                }
                return File(pdfStream, "application/pdf");
            }
            catch
            {
                return NotFound(new { Message = "File PDF tidak ditemukan untuk dibaca." });
            }
        }

        // fitur Tracking pakai Automata
        [HttpPost("track-progress")]
        public IActionResult UpdateProgress([FromQuery] string bookId, [FromQuery] int currentPage, [FromQuery] int totalPage)
        {
            // 1. Hitung statusnya menggunakan Automata

            var automata = new ReadingStateMachine();
            automata.UpdateProgress(currentPage, totalPage);

            // 2. Simpan halamannya menggunakan Table-driven (BookmarkManager)
            var bookmarkMgr = new BookmarkManager();
            bookmarkMgr.SaveBookmark(bookId, currentPage);

            // 3. Kembalikan respons yang lebih lengkap dan masuk akal
            return Ok(new
            {
                IdBuku = bookId,
                HalamanTerakhir = currentPage,
                StatusBacaan = automata.CurrentState.ToString(),
                Pesan = $"Progres buku '{bookId}' berhasil diperbarui ke sistem."
            });
        }
        //fitur tambah bookmark
        [HttpPost("bookmark")]
        public IActionResult SaveBookmark([FromQuery] string bookId, [FromQuery] int page)
        {
            var bookmarkMgr = new BookmarkManager(); // Menggunakan logic Tegar
            bookmarkMgr.SaveBookmark(bookId, page);
            return Ok(new { Message = $"Bookmark buku {bookId} di halaman {page} berhasil disimpan." });
        }

        // fitur login
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            var users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", UserRole = Role.Admin },
            new User { Username = "user",  Password = "user123",  UserRole = Role.User  }
        };

            var auth = new AuthService(users);
            try
            {
                var user = auth.Login(username, password);

                return Ok(new
                {
                    Message = "Login Berhasil",
                    Username = user.Username,
                    Role = user.UserRole.ToString(),
                    Menu = auth.GetUserMenus(user)
                });
            }
            catch
            {
                return Unauthorized("Username atau Password salah.");
            }
        }
    }
}
