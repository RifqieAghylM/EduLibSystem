using eduLib.Infrastructure.Viewer;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using eduLib.Infrastructure.Viewer;

namespace eduLib.Tests.ViewerTests
{
    [TestClass]
    public class ViewerServiceTests
    {
        [TestMethod]
        public void ExtractMetadata_ValidPdf_ReturnsMockMetadata()
        {
            // Arrange
            var reader = new PdfMetadataReader();

            // Act
            var result = reader.ExtractMetadata("buku_rekayasa_perangkat_lunak.pdf");

            // Assert
            Assert.IsTrue(result.Contains("iText7 Mock"));
            Assert.IsTrue(result.Contains("buku_rekayasa_perangkat_lunak.pdf"));
        }

        [TestMethod]
        public void ExtractMetadata_NonPdf_ThrowsInvalidOperationException()
        {
            var reader = new PdfMetadataReader();

            // Menggunakan Try-Catch-Fail untuk menghindari error CS0117
            try
            {
                reader.ExtractMetadata("gambar_sampul.png");
                Assert.Fail("Seharusnya melempar InvalidOperationException karena bukan file PDF.");
            }
            catch (InvalidOperationException)
            {
                // Sukses: Defensive programming bekerja
            }
        }
    }
}