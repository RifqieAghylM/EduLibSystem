using System;
using eduLib.Core.Interfaces;

namespace eduLib.Infrastructure.Viewer
{
    // Teknik: Code Reuse / Library (Mendemonstrasikan integrasi pustaka PDF eksternal seperti iText7)
    public class PdfMetadataReader : IPdfReader
    {
        public string ExtractMetadata(string filePath)
        {
            // Precondition
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path PDF tidak boleh kosong.");
            if (!filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Format file tidak valid. Harus berekstensi .pdf");
            // Catatan: Di dunia nyata, di sinilah kode pemanggilan iText7/PdfSharp diletakkan.
            // Untuk CLO 2, kita mensimulasikan hasil dari library tersebut.
            return $"[iText7 Mock] Metadata dari {filePath}: 150 Halaman, Author: eduLib Publisher";
        }
    }
}