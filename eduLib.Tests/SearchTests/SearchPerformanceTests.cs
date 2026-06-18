using Microsoft.VisualStudio.TestTools.UnitTesting;
using eduLib.Application.Search;
using eduLib.Infrastructure.API;
using eduLib.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace eduLib.Tests.SearchTests
{
    [TestClass]
    public class SearchPerformanceTests
    {
        [TestMethod]
        public void Performance_SearchService_Generics_Under100ms()
        {
            // Arrange: Menyiapkan BEBAN DATA BESAR (100.000 Buku)
            var searchService = new SearchService();
            var massiveLibrary = new List<Book>();

            for (int i = 0; i < 100000; i++)
            {
                massiveLibrary.Add(new Book
                {
                    Title = "Buku Pemrograman " + i,
                    Author = (i % 2 == 0) ? "Rifqie" : "Penulis Lain",
                    Year = 2000 + (i % 25)
                });
            }

            // Act: Memulai Stopwatch untuk mencari spesifik di antara 100k data
            var sw = Stopwatch.StartNew();

            // Teknik Generics: Mencari buku karangan Rifqie terbitan tahun 2024
            var results = searchService.FindBooks(massiveLibrary, b => b.Author == "Rifqie" && b.Year == 2024);

            sw.Stop();

            // Assert: Waktu pencarian dari 100.000 data menggunakan memori harus di bawah 100 milidetik
            // Sesuai standar NFR (Non-Functional Requirement) tugas besar
            Assert.IsTrue(sw.ElapsedMilliseconds < 100,
                $"Pencarian Generics gagal performa! Membutuhkan waktu {sw.ElapsedMilliseconds} ms.");
            Assert.IsTrue(results.Count > 0); // Memastikan data memang ditemukan
        }

        [TestMethod]
        public async Task Performance_ReviewApi_ConcurrentRequests_Under500ms()
        {
            // Arrange: Menyiapkan API Connector
            var apiConnector = new ReviewApiConnector();
            var stopwatch = new Stopwatch();

            // Menyiapkan 50 request ulasan yang akan dikirim secara BERSAMAAN (Concurrent)
            var concurrentTasks = new List<Task<bool>>();

            // Act
            stopwatch.Start();

            for (int i = 0; i < 50; i++)
            {
                // Menumpuk task tanpa menggunakan await di dalam loop
                concurrentTasks.Add(apiConnector.SendReviewAsync("Buku C#", "Komentar beban ke-" + i));
            }

            // Menunggu semua API calls selesai secara paralel
            await Task.WhenAll(concurrentTasks);

            stopwatch.Stop();

            // Assert: Waktu eksekusi 50 request API paralel harus sangat cepat
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 500,
                $"API Terlalu lambat saat di-spam! Membutuhkan waktu {stopwatch.ElapsedMilliseconds} ms.");

            // Memastikan semua request berhasil (true)
            Assert.IsTrue(concurrentTasks.All(t => t.Result == true));
        }
    }
}