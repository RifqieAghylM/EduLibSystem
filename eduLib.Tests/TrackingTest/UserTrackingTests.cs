using Microsoft.VisualStudio.TestTools.UnitTesting;
using eduLib.Application.Tracking;
using eduLib.Core.Enums;
using System;
using System.Diagnostics;

namespace eduLib.Tests.TrackingTests
{
    [TestClass]
    public class UserTrackingTests
    {
        [TestMethod]
        public void Automata_StateTransitions_AreCorrect()
        {
            var machine = new ReadingStateMachine();

            // Act 1: Mulai membaca
            machine.UpdateProgress(10, 100);
            Assert.AreEqual(ReadingState.Reading, machine.CurrentState);

            // Act 2: Selesai membaca
            machine.UpdateProgress(100, 100);
            Assert.AreEqual(ReadingState.Completed, machine.CurrentState);
        }

        [TestMethod]
        public void SaveBookmark_NegativePage_ThrowsException()
        {
            var manager = new BookmarkManager();

            // Menggunakan Try-Catch-Fail untuk menguji Defensive Programming
            try
            {
                manager.SaveBookmark("B001", -5);
                Assert.Fail("Seharusnya melempar ArgumentException karena halaman negatif.");
            }
            catch (ArgumentException)
            {
                // Sukses: Exception yang tepat dilempar
            }
        }

        [TestMethod]
        public void BookmarkTable_PerformanceTest_ExecutesUnder50ms()
        {
            var manager = new BookmarkManager();
            var sw = Stopwatch.StartNew();

            // Mensimulasikan penyimpanan massal ke dalam Table-driven memory
            for (int i = 0; i < 1000; i++)
            {
                manager.SaveBookmark("Book" + i, i);
            }

            sw.Stop();

            // Performance Testing sesuai syarat tugas CLO 2
            Assert.IsTrue(sw.ElapsedMilliseconds < 50, $"Eksekusi terlalu lambat: {sw.ElapsedMilliseconds}ms");
        }
        [TestMethod]
        public void Profiling_Tracking_DictionaryLoad()
        {
            var manager = new BookmarkManager();
            var sw = Stopwatch.StartNew();

            // Stress test: Menulis dan menimpa (update/insert) 500.000 data ke struktur Table-driven
            for (int i = 0; i < 500000; i++)
            {
                manager.SaveBookmark("Buku_ID_" + i, i % 100);
            }

            sw.Stop();

            // 500.000 operasi Dictionary harus selesai di bawah 500ms
            Assert.IsTrue(sw.ElapsedMilliseconds < 500,
                $"Stress test terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 500ms)");
        }
    }
}