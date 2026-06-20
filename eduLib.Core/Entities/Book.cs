using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using eduLib.Core.Interfaces; 
namespace eduLib.Core.Entities
{
    public class Book : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string GridFsFileId { get; set; }
    }
}