using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;
using eduLib.API.Controllers;
using eduLib.Infrastructure.Storage;

namespace eduLib.Tests.ViewerTests
{
    [TestClass]
    public class ReadandDownloadTests
    {
        private Mock<IBookRepository> _mockRepo;
        private BooksController _controller;
        [TestInitialize]
        public void Setup()
        {
            // Inisialisasi Mock Repository
            _mockRepo = new Mock<IBookRepository>();
            // Masukkan mock ke dalam Controller
            //_controller = new BooksController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task DownloadBookPdf_FileExists_ReturnsFileContentResult()
        {
            string dummyGridFsId = "60d5ec49f0a2c5a5d8b4c123";
            byte[] fakePdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 };
            _mockRepo.Setup(repo => repo.DownloadPdfAsync(dummyGridFsId))
                     .ReturnsAsync(fakePdfBytes);
            var result = await _controller.DownloadBookPdf(dummyGridFsId);
            var fileResult = result as FileContentResult;
            Assert.IsNotNull(fileResult, "Result harus berupa FileContentResult.");
            Assert.AreEqual("application/pdf", fileResult.ContentType);
            Assert.AreEqual("downloaded_book.pdf", fileResult.FileDownloadName);
            CollectionAssert.AreEqual(fakePdfBytes, fileResult.FileContents);
        }

        [TestMethod]
        public async Task DownloadBookPdf_FileNotFound_ReturnsNotFound()
        {
            string wrongGridFsId = "invalid_id";
            _mockRepo.Setup(repo => repo.DownloadPdfAsync(wrongGridFsId))
                     .ThrowsAsync(new System.Exception("File not found in GridFS"));
            var result = await _controller.DownloadBookPdf(wrongGridFsId);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("File PDF tidak ditemukan di GridFS.", notFoundResult.Value);
        }
        [TestMethod]
        public async Task ReadBookPdfOnline_FileExists_ReturnsFileStreamResult()
        {
            string dummyGridFsId = "6a030db07120995be3f8adbd";
            var fakeStream = new MemoryStream(new byte[] { 1, 2, 3 });
            _mockRepo.Setup(repo => repo.GetPdfStreamAsync(dummyGridFsId))
                     .ReturnsAsync(fakeStream);
            var result = await _controller.ReadBookPdfOnline(dummyGridFsId);
            var fileStreamResult = result as FileStreamResult;
            Assert.IsNotNull(fileStreamResult, "Result harus berupa FileStreamResult.");
            Assert.AreEqual("application/pdf", fileStreamResult.ContentType);
            Assert.IsTrue(string.IsNullOrEmpty(fileStreamResult.FileDownloadName));
            Assert.AreSame(fakeStream, fileStreamResult.FileStream);
        }

        [TestMethod]
        public async Task ReadBookPdfOnline_FileNotFound_ReturnsNotFound()
        {
            string wrongGridFsId = "invalid_id";
            _mockRepo.Setup(repo => repo.GetPdfStreamAsync(wrongGridFsId))
                     .ThrowsAsync(new System.Exception("Stream error"));
            var result = await _controller.ReadBookPdfOnline(wrongGridFsId);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("File PDF tidak ditemukan untuk dibaca.", notFoundResult.Value);
        }
        [TestMethod]
        public async Task DownloadBookPdf_PerformanceTest_ShouldExecuteInUnder100Milliseconds()
        {
            string dummyGridFsId = "60d5ec49f0a2c5a5d8b4c123";
            byte[] fakePdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 };
            _mockRepo.Setup(repo => repo.DownloadPdfAsync(dummyGridFsId))
                     .ReturnsAsync(fakePdfBytes);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = await _controller.DownloadBookPdf(dummyGridFsId);
            stopwatch.Stop(); 
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            Assert.IsNotNull(result);
            Assert.IsTrue(elapsedMs < 100, $"Performance test gagal: Waktu eksekusi {elapsedMs}ms melebihi batas 100ms.");
        }
    }
}