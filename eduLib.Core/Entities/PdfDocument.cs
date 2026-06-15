using eduLib.Core.Interfaces;

namespace eduLib.Core.Entities
{
    public class PdfDocument : IEntity
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public int FileSizeMB { get; set; }
    }
}