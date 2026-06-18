using System.Threading.Tasks;
using MongoDB.Driver;
using eduLib.Core.Entities;

namespace eduLib.Infrastructure.Storage
{
    public class MongoTrackingRepository
    {
        private readonly IMongoCollection<UserBookTracking> _bookmarkCollection;
        private readonly IMongoCollection<UserBookTracking> _progressCollection;

        public MongoTrackingRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _bookmarkCollection = database.GetCollection<UserBookTracking>("bookmarks");
            _progressCollection = database.GetCollection<UserBookTracking>("reading_progress");
        }

        // === BOOKMARK ===
        public async Task SaveBookmarkAsync(string bookId, int page)
        {
            var filter = Builders<UserBookTracking>.Filter.Eq(x => x.BookId, bookId);
            var update = Builders<UserBookTracking>.Update
                .Set(x => x.BookmarkedPage, page);

            await _bookmarkCollection.UpdateOneAsync(filter, update,
                new UpdateOptions { IsUpsert = true });
        }

        public async Task<int> GetBookmarkAsync(string bookId)
        {
            var result = await _bookmarkCollection
                .Find(x => x.BookId == bookId)
                .FirstOrDefaultAsync();

            return result?.BookmarkedPage ?? 0;
        }

        // === READING PROGRESS ===
        public async Task SaveProgressAsync(string bookId, int currentPage, int totalPages, string state)
        {
            var filter = Builders<UserBookTracking>.Filter.Eq(x => x.BookId, bookId);
            var update = Builders<UserBookTracking>.Update
                .Set(x => x.CurrentPage, currentPage)
                .Set(x => x.TotalPages, totalPages)
                .Set(x => x.ReadingState, state);

            await _progressCollection.UpdateOneAsync(filter, update,
                new UpdateOptions { IsUpsert = true });
        }

        public async Task<UserBookTracking?> GetProgressAsync(string bookId)
        {
            return await _progressCollection
                .Find(x => x.BookId == bookId)
                .FirstOrDefaultAsync();
        }
    }
}