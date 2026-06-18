using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using eduLib.Application.Tracking;
using eduLib.Core.Enums;

namespace eduLib.Tests.TrackingTest
{
    [TestClass]
    public class TrackingPerformanceTests  
    {
        // automata - transisi state berulang
        [TestMethod]
        public void Performance_Automata_StateTransition1000Times_Under50ms()
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                var machine = new ReadingStateMachine();
                machine.UpdateProgress(1, 100);    // NotStarted -> Reading
                machine.UpdateProgress(100, 100);  // Reading -> Completed
                machine.UpdateProgress(0, 100);    // Completed -> NotStarted
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 50,
                $"[Automata] 1000x transisi state terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 50ms)");
        }

        // Table-driven - simpan bookmark massal
        [TestMethod]
        public void Performance_TableDriven_SaveBookmark1000Times_Under10ms()
        {
            var manager = new BookmarkManager();
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                manager.SaveBookmark("Book" + i, i * 2);
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 10,
                $"[Table-driven] 1000x SaveBookmark terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 10ms)");
        }

        // Table-driven - baca bookmark massal
        [TestMethod]
        public void Performance_TableDriven_GetBookmark1000Times_Under10ms()
        {
            var manager = new BookmarkManager();
            for (int i = 0; i < 1000; i++)
                manager.SaveBookmark("Book" + i, i);

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                var page = manager.GetBookmark("Book" + i);
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 10,
                $"[Table-driven] 1000x GetBookmark terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 10ms)");
        }

        // Gabungan Automata + Table-driven
        [TestMethod]
        public void Performance_Combined_AutomataAndBookmark_Under50ms()
        {
            var machine = new ReadingStateMachine();
            var manager = new BookmarkManager();
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 500; i++)
            {
                machine.UpdateProgress(i % 100, 100);
                manager.SaveBookmark("Book" + i, i % 100);
                manager.GetBookmark("Book" + i);
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 50,
                $"[Combined] 500x Automata+Bookmark terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 50ms)");
        }
    }
}