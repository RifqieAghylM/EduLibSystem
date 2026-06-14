using eduLib.Core.Entities; // Pastikan ini ada agar sistem kenal class Book & Review
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace eduLib.Infrastructure.Storage
{
    public interface IBookRepository
    {
        // --- Fitur Rifki & CRUD ---
        Task<string> UploadBookAsync(Book bookMetadata, Stream pdfFileStream, string fileName);
        Task<bool> UpdateBookAsync(string id, Book updatedBook);
        Task<bool> DeleteBookAsync(string id);

        // --- Fitur Rifqie ---
        Task<List<Book>> SearchBooksAsync(string keyword);

        // --- Fitur Review ---
        Task<Review> AddReviewAsync(Review review);
        Task<List<Review>> GetAllReviewsAsync();

        // --- Fitur Raka ---
        Task<byte[]> DownloadPdfAsync(string gridFsId);
        Task<Stream> GetPdfStreamAsync(string gridFsId);

        Task<bool> TitleExistsAsync(string title);
    }
}