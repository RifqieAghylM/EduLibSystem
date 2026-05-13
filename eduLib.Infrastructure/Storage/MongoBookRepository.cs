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

        // --- FITUR RIFKI: Upload PDF ke GridFS & Simpan Metadata ---
        public async Task<string> UploadBookAsync(Book bookMetadata, Stream pdfFileStream, string fileName)
        {
            // 1. Upload file fisik PDF ke GridFS
            var gridFsId = await _gridFS.UploadFromStreamAsync(fileName, pdfFileStream);
            // 2. Hubungkan ID GridFS ke Metadata Buku
            bookMetadata.GridFsFileId = gridFsId.ToString();
            // 3. Simpan Metadata ke Collection biasa
            await _booksCollection.InsertOneAsync(bookMetadata);

            return bookMetadata.Id;
        }
        // --- FITUR UPDATE: Memperbarui Metadata Buku ---
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

        // --- FITUR DELETE: Menghapus Metadata dan File di GridFS ---
        public async Task<bool> DeleteBookAsync(string id)
        {
            // 1. Cari buku terlebih dahulu untuk mendapatkan ID GridFS-nya
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var book = await _booksCollection.Find(filter).FirstOrDefaultAsync();

            if (book == null) return false;
            // 2. Hapus file PDF dari GridFS (jika ada)
            if (!string.IsNullOrEmpty(book.GridFsFileId))
            {
                var gridFsObjectId = new MongoDB.Bson.ObjectId(book.GridFsFileId);
                await _gridFS.DeleteAsync(gridFsObjectId);
            }
            // 3. Hapus metadata buku dari Collection
            var result = await _booksCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        // --- FITUR RIFQIE: Cari Buku dari MongoDB ---
        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            var filter = Builders<Book>.Filter.Or(
                Builders<Book>.Filter.Regex(b => b.Title, new BsonRegularExpression(keyword, "i")),
                Builders<Book>.Filter.Regex(b => b.Author, new BsonRegularExpression(keyword, "i"))
            );

            return await _booksCollection.Find(filter).ToListAsync();
        }
        // --- FITUR REVIEW: Tambah Review Baru ---
        public async Task<Review> AddReviewAsync(Review review)
        {
            // Set waktu saat ini (UTC) agar sesuai dengan format di MongoDB
            review.Date = DateTime.UtcNow;

            await _reviewsCollection.InsertOneAsync(review);
            return review;
        }

        // --- FITUR REVIEW: Ambil Semua Review ---
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            // Mengambil semua review dan mengurutkannya dari yang terbaru
            return await _reviewsCollection.Find(_ => true)
                                           .SortByDescending(r => r.Date)
                                           .ToListAsync();
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