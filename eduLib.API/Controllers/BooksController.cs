using eduLib.Core.Entities;
using eduLib.Core.Enums;
using eduLib.Infrastructure.Storage;
using Microsoft.AspNetCore.Mvc;
using eduLib.Application.Auth;        
using eduLib.Application.Tracking;               

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
                ".pdf",
                ".docx",
                ".doc"
                
            };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Hanya file PDF, DOCdan DOCX yang diperbolehkan.");

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
            var updatedMetadata = new Book { Title = title, Author = author, Year = year };

            var isUpdated = await _repo.UpdateBookAsync(id, updatedMetadata);

            if (!isUpdated)
                return NotFound(new { Message = "Buku tidak ditemukan atau tidak ada perubahan." });

            return Ok(new { Message = "Metadata buku berhasil diperbarui." });
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

        // fitur tambah review
        [HttpPost("review")]
        public async Task<IActionResult> AddReview([FromForm] string username, [FromForm] string comment)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(comment))
            {
                return BadRequest("Username dan Comment tidak boleh kosong.");
            }
            var newReview = new Review
            {
                Username = username,
                Comment = comment
                // Date di-set otomatis di Repository
            };
            var result = await _repo.AddReviewAsync(newReview);
            return Ok(new { Message = "Review berhasil ditambahkan!", Data = result });
        }
        // fitur ambil semua review
        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _repo.GetAllReviewsAsync();
            return Ok(reviews);
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
            // Dummy user list sesuai logic Azka
            var users = new List<User>
    {
        new User
        {
            Username = "admin",
            Password = "Admin123",
            UserRole = Role.Admin
        },
        new User
        {
            Username = "guru",
            Password = "Guru123",
            UserRole = Role.Guru
        },
        new User
        {
            Username = "pelajar",
            Password = "Pelajar123",
            UserRole = Role.Pelajar
        }
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