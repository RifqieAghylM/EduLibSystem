using eduLib.Core.Entities;
using eduLib.Core.Enums;
using eduLib.Infrastructure.Storage;
using eduLib.Infrastructure.Viewer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eduLib.Application.Auth;        // Agar kenal AuthService (Azka)
using eduLib.Application.Tracking;    // Agar kenal ReadingStateMachine & BookmarkManager (Tegar)
using eduLib.Infrastructure.Viewer;  // Agar kenal PdfMetadataReader (Raka)
using eduLib.Core.Entities;           // Agar kenal kelas User & Book
using eduLib.Core.Enums;              // Agar kenal Role
using System.IO;
using System.Threading.Tasks;

namespace eduLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MongoBookRepository _repo;

        public BooksController()
        {
            // Menggunakan koneksi MongoDB Atlas asli milik kelompokmu!
            string mongoAtlasConnString = "mongodb+srv://raka:rootz@cluster0.1qgyljt.mongodb.net/?appName=Cluster0";

            // Nama database-nya adalah "book"
            _repo = new MongoBookRepository(mongoAtlasConnString, "book");
        }

        // [GET] /api/books/search?keyword=C#
        // Dipakai oleh Swagger / Postman untuk fitur FR-04 (Pencarian)
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string keyword)
        {
            var results = await _repo.SearchBooksAsync(keyword);
            return Ok(results); // Mengembalikan JSON Response secara otomatis
        }

        // [POST] /api/books/upload
        // Dipakai di Postman (Body -> form-data) untuk fitur FR-02 (Upload File)
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromForm] string title, [FromForm] string author, [FromForm] int year, IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("File PDF tidak valid.");

            var bookMetadata = new Book { Title = title, Author = author, Year = year };

            using var stream = pdfFile.OpenReadStream();
            var bookId = await _repo.UploadBookAsync(bookMetadata, stream, pdfFile.FileName);

            return Ok(new { Message = "Upload Sukses", BookId = bookId });
        }

        // [GET] /api/books/download/{gridFsId}
        // Dipakai browser / Postman untuk fitur FR-06 (Unduh)
        [HttpGet("download/{gridFsId}")]
        public async Task<IActionResult> DownloadBookPdf(string gridFsId)
        {
            try
            {
                var fileBytes = await _repo.DownloadPdfAsync(gridFsId);
                return File(fileBytes, "application/pdf", "downloaded_book.pdf");
            }
            catch
            {
                return NotFound("File PDF tidak ditemukan di GridFS.");
            }
        }

        // --- FITUR RAKA (Anggota 4): Download & Metadata ---
        [HttpGet("metadata/{fileName}")]
        public IActionResult GetMetadata(string fileName)
        {
            var pdfReader = new PdfMetadataReader(); // Menggunakan logic Raka
            var metadata = pdfReader.ExtractMetadata(fileName);
            return Ok(new { Metadata = metadata });
        }

        // --- FITUR TEGAR (Anggota 5): Tracking & Automata ---
        [HttpPost("track-progress")]
        public IActionResult UpdateProgress([FromQuery] int currentPage, [FromQuery] int totalPage)
        {
            var automata = new ReadingStateMachine(); // Menggunakan logic Tegar
            automata.UpdateProgress(currentPage, totalPage);
            return Ok(new { Status = automata.CurrentState.ToString() });
        }

        [HttpPost("bookmark")]
        public IActionResult SaveBookmark([FromQuery] string bookId, [FromQuery] int page)
        {
            var bookmarkMgr = new BookmarkManager(); // Menggunakan logic Tegar
            bookmarkMgr.SaveBookmark(bookId, page);
            return Ok(new { Message = $"Bookmark buku {bookId} di halaman {page} berhasil disimpan." });
        }

        // --- FITUR AZKA (Anggota 1): Auth & Role Check ---
        // (Simulasi sederhana, biasanya menggunakan JWT Token)
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            // Dummy user list sesuai logic Azka
            var users = new List<User> { new User { Username = "admin", Password = "123", UserRole = Role.Admin } };
            var auth = new AuthService(users);

            try
            {
                var user = auth.Login(username, password);
                return Ok(new { Message = "Login Berhasil", Role = user.UserRole.ToString() });
            }
            catch
            {
                return Unauthorized("Username atau Password salah.");
            }
        }
    }
}