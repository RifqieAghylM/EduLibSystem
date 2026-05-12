using eduLib.Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eduLib.Infrastructure.Storage
{
    public class MongoBookRepository
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly IGridFSBucket _gridFS;

        // Constructor membaca connection string (nantinya dari appsettings.json)
        public MongoBookRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _booksCollection = database.GetCollection<Book>("BooksData");
            _gridFS = new GridFSBucket(database); // Inisialisasi GridFS
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

        // --- FITUR RIFQIE: Cari Buku dari MongoDB ---
        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            var filter = Builders<Book>.Filter.Or(
                Builders<Book>.Filter.Regex(b => b.Title, new BsonRegularExpression(keyword, "i")),
                Builders<Book>.Filter.Regex(b => b.Author, new BsonRegularExpression(keyword, "i"))
            );

            return await _booksCollection.Find(filter).ToListAsync();
        }

        // --- FITUR RAKA: Unduh File PDF dari GridFS ---
        public async Task<byte[]> DownloadPdfAsync(string gridFsId)
        {
            var objectId = new ObjectId(gridFsId);
            return await _gridFS.DownloadAsBytesAsync(objectId);
        }
        public async Task<Stream> GetPdfStreamAsync(string gridFsId)
        {
            var objectId = new MongoDB.Bson.ObjectId(gridFsId);
            // OpenDownloadStreamAsync mengembalikan stream murni
            return await _gridFS.OpenDownloadStreamAsync(objectId);
        }
    }
}