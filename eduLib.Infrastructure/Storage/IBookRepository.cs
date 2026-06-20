using eduLib.Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace eduLib.Infrastructure.Storage
{
    public interface IBookRepository
    {
        Task<string> UploadBookAsync(Book bookMetadata, Stream pdfFileStream, string fileName);
        Task<bool> UpdateBookAsync(string id, Book updatedBook);
        Task<bool> DeleteBookAsync(string id);
        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<Review> AddReviewAsync(Review review);
        Task<List<Review>> GetReviewsByBookTitleAsync(string title); 
        Task<byte[]> DownloadPdfAsync(string gridFsId);
        Task<Stream> GetPdfStreamAsync(string gridFsId);
        Task<bool> TitleExistsForOtherBookAsync(string title, string currentBookId);
    }
}