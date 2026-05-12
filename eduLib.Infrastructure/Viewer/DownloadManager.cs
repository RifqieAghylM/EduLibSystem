namespace eduLib.Infrastructure.Viewer
{
    public class DownloadManager
    {
        // TMembaca path target unduhan dari file eksternal
        public string TargetDownloadDirectory { get; private set; }
        public DownloadManager(string configDownloadPath)
        {
            // Defensive Programming (DbC)
            if (string.IsNullOrWhiteSpace(configDownloadPath))
                throw new ArgumentNullException(nameof(configDownloadPath), "Path konfigurasi unduhan dari JSON tidak boleh null.");
            TargetDownloadDirectory = configDownloadPath;
        }

        public string ProcessDownload(string bookId, string fileName)
        {
            // validasi input
            if (string.IsNullOrWhiteSpace(bookId) || string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Buku ID dan Nama file tidak valid.");
            // mockup download file ke direktori target (Runtime Config)
            return $"Berhasil mengunduh {fileName} ke direktori: {TargetDownloadDirectory}\\{bookId}.pdf";
        }
    }
}
