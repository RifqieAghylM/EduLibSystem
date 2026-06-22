using eduLib.Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace eduLib.Infrastructure.Storage
{
    public class MongoBookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly IGridFSBucket _gridFS;
        private readonly IMongoCollection<Review> _reviewsCollection;
        // set koneksi
        public MongoBookRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _booksCollection = database.GetCollection<Book>("BooksData");
            _gridFS = new GridFSBucket(database); // Inisialisasi GridFS
            _reviewsCollection = database.GetCollection<Review>("Reviews");
        }

        // fitur upload buku
        public async Task<string> UploadBookAsync(Book bookMetadata, Stream pdfFileStream, string fileName)
        {

            var gridFsId = await _gridFS.UploadFromStreamAsync(fileName, pdfFileStream);
            
            bookMetadata.GridFsFileId = gridFsId.ToString();
            
            await _booksCollection.InsertOneAsync(bookMetadata);

            return bookMetadata.Id;
        }
        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _booksCollection.Find(book => book.Title.ToLower().Trim() == title.ToLower().Trim()).AnyAsync();

        }
        // fitur update buku
        public async Task<bool> UpdateBookAsync(string id, Book updatedBook)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var update = Builders<Book>.Update
                .Set(b => b.Title, updatedBook.Title)
                .Set(b => b.Author, updatedBook.Author)
                .Set(b => b.Year, updatedBook.Year);
            // GridFsFileId tidak diubah kecuali ingin mengganti file PDF-nya juga
            var result = await _booksCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
        public async Task<bool> TitleExistsForOtherBookAsync(string title, string currentBookId)
        {
            return await _booksCollection.Find(book => book.Title.ToLower().Trim() == title.ToLower().Trim() && book.Id != currentBookId).AnyAsync();

        }

        // fitur delete buku
        public async Task<bool> DeleteBookAsync(string id)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var book = await _booksCollection.Find(filter).FirstOrDefaultAsync();

            if (book == null) return false;
            if (!string.IsNullOrEmpty(book.GridFsFileId))
            {
                var gridFsObjectId = new MongoDB.Bson.ObjectId(book.GridFsFileId);
                await _gridFS.DeleteAsync(gridFsObjectId);
            }
            var result = await _booksCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        // fitur search buku berdasarkan judul atau penulis
        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            var filter = Builders<Book>.Filter.Or(
                Builders<Book>.Filter.Regex(b => b.Title, new BsonRegularExpression(keyword, "i")),
                Builders<Book>.Filter.Regex(b => b.Author, new BsonRegularExpression(keyword, "i"))
            );

            return await _booksCollection.Find(filter).ToListAsync();
        }
        // fitur review: tambah review
        public async Task<Review> AddReviewAsync(Review review)
        {
            review.Date = DateTime.UtcNow;
            await _reviewsCollection.InsertOneAsync(review);
            return review;
        }

        // fitur review: get review berdasarkan judul buku
        public async Task<List<Review>> GetReviewsByBookTitleAsync(string title)
        {
            var filter = Builders<Review>.Filter.Regex(r => r.BookTitle, new BsonRegularExpression(title, "i"));

            // Format bertingkat yang valid untuk MongoDB Driver C#
            return await _reviewsCollection.Find(filter).SortByDescending(r => r.Date).ToListAsync();
        }
        // fitur download
        public async Task<byte[]> DownloadPdfAsync(string gridFsId)
        {
            var objectId = new ObjectId(gridFsId);
            return await _gridFS.DownloadAsBytesAsync(objectId);
        }
        // fitur baca online
        public async Task<Stream> GetPdfStreamAsync(string gridFsId)
        {
            var objectId = new MongoDB.Bson.ObjectId(gridFsId);
            // menampilkan file PDF sebagai Stream
            return await _gridFS.OpenDownloadStreamAsync(objectId);
        }
    }
}