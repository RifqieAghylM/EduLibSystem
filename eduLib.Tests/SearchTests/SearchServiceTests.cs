using Microsoft.VisualStudio.TestTools.UnitTesting;
using eduLib.Application.Search;
using eduLib.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics;

namespace eduLib.Tests.SearchTests
{
    [TestClass]
    public class SearchServiceTests
    {
        private List<Book> _library;
        private SearchService _searchService;

        [TestInitialize]
        public void Setup()
        {
            _searchService = new SearchService();
            _library = new List<Book>
            {
                new Book { Title = "C# Programming", Author = "Rifqie", Year = 2024 },
                new Book { Title = "Clean Code", Author = "Robert Martin", Year = 2008 }
            };
        }

        [TestMethod]
        public void FindBooks_SearchByAuthor_ReturnsCorrectBook()
        {
            // Act: Mencari buku berdasarkan penulis (Generics Lambda)
            var result = _searchService.FindBooks(_library, b => b.Author == "Rifqie");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("C# Programming", result[0].Title);
        }

        [TestMethod]
        public void Search_Performance_Under50Milliseconds()
        {
            // Performance Testing sesuai syarat CLO 2
            var largeLibrary = new List<Book>();
            for (int i = 0; i < 1000; i++) largeLibrary.Add(new Book { Title = "Book " + i });

            var sw = Stopwatch.StartNew();
            _searchService.FindBooks(largeLibrary, b => b.Title == "Book 999");
            sw.Stop();

            // Target performa pencarian dalam 1000 data memori
            Assert.IsTrue(sw.ElapsedMilliseconds < 50, $"Pencarian lambat: {sw.ElapsedMilliseconds}ms");
        }

        [TestMethod]
        public void Profiling_Search_LargeDataFiltering()
        {
            var searchService = new SearchService();
            var largeLibrary = new List<Book>();

            // Buat 500.000 data buku
            for (int i = 0; i < 500000; i++)
            {
                largeLibrary.Add(new Book { Title = "Book " + i, Author = "Author" });
            }

            // Stress test: Pencarian spesifik berulang-ulang
            for (int i = 0; i < 100; i++)
            {
                searchService.FindBooks(largeLibrary, b => b.Title == "Book 499999");
            }

            // Jeda 5 detik agar grafik Diagnostic Tools bisa di-screenshot
            System.Threading.Thread.Sleep(5000);
        }
    }
}