using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eduLib.Core.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BookId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        // --- TAMBAHAN: Menyimpan informasi lengkap detail buku ---
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public int BookYear { get; set; }
    }
}
