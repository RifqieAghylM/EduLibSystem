using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using eduLib.Core.Interfaces; // Pastikan namespace ini sesuai dengan letak IEntity-mu

namespace eduLib.Core.Entities
{
    // Menambahkan : IEntity untuk memecahkan Error CS0311
    public class Book : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        // Dibutuhkan oleh Program.cs (JSON System) untuk memecahkan Error CS0117
        public string PdfPath { get; set; }
        // Dibutuhkan oleh MongoDB & GridFS (API System)
        public string GridFsFileId { get; set; }
    }
}