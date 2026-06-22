using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using eduLib.Core.Interfaces;

namespace eduLib.Core.Entities
{
    public class UserBookTracking : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BookId { get; set; }
        public int BookmarkedPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string ReadingState { get; set; } = "NotStarted";
    }
}