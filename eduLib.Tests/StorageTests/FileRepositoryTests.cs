using Microsoft.VisualStudio.TestTools.UnitTesting;
using eduLib.Infrastructure.Storage;
using eduLib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace eduLib.Tests.StorageTests
{
    [TestClass]
    public class FileRepositoryTests
    {
        private FileRepository<PdfDocument> _repository;

        [TestInitialize]
        public void Setup()
        {
            // Setup simulasi Runtime Config batas ukuran 50MB
            _repository = new FileRepository<PdfDocument>(50);
        }

        [TestMethod]
        public void Upload_ValidDocument_ShouldAddToStorage()
        {
            var doc = new PdfDocument { Id = "PDF-01", FileName = "Buku_A.pdf", FileSizeMB = 10 };
            _repository.Upload(doc);

            Assert.AreEqual(1, _repository.GetAll().Count);
            Assert.AreEqual("Buku_A.pdf", _repository.GetById("PDF-01").FileName);
        }

        [TestMethod]
        public void Update_ExistingDocument_ShouldModifyData()
        {
            var doc = new PdfDocument { Id = "PDF-02", FileName = "Buku_Lama.pdf" };
            _repository.Upload(doc);

            // Act: Update nama file
            var updatedDoc = new PdfDocument { Id = "PDF-02", FileName = "Buku_Baru_Revisi.pdf" };
            _repository.Update(updatedDoc);

            // Assert
            var result = _repository.GetById("PDF-02");
            Assert.AreEqual("Buku_Baru_Revisi.pdf", result.FileName);
        }

        [TestMethod]
        public void Delete_ExistingDocument_ShouldRemoveFromStorage()
        {
            var doc = new PdfDocument { Id = "PDF-03", FileName = "Buku_Hapus.pdf" };
            _repository.Upload(doc);

            // Act
            _repository.Delete("PDF-03");

            // Assert
            Assert.AreEqual(0, _repository.GetAll().Count);
            try
            {
                _repository.Delete("PDF-03"); // Mencoba hapus lagi harus error
                Assert.Fail("Seharusnya melempar KeyNotFoundException");
            }
            catch (KeyNotFoundException) { }
        }

        [TestMethod]
        public void PerformanceTest_BulkUploadUpdateDelete_Under100ms()
        {
            var sw = Stopwatch.StartNew();

            // Bulk Upload
            for (int i = 0; i < 500; i++)
            {
                _repository.Upload(new PdfDocument { Id = "P" + i, FileName = "File" + i });
            }

            // Bulk Update
            _repository.Update(new PdfDocument { Id = "P250", FileName = "File250_Updated" });

            // Bulk Delete
            for (int i = 0; i < 100; i++)
            {
                _repository.Delete("P" + i);
            }

            sw.Stop();

            // Performance Testing: Total operasi CRUD 600x harus sangat cepat
            Assert.IsTrue(sw.ElapsedMilliseconds < 100, $"Terlalu lambat: {sw.ElapsedMilliseconds}ms");
        }
    }
}