using System;
using System.Collections.Generic;

namespace eduLib.Application.Tracking
{
    
    public class BookmarkManager
    {
        // menggunakan struktur tabel Dictionary di memori
        private readonly Dictionary<string, int> _bookmarkTable = new Dictionary<string, int>();

        public void SaveBookmark(string bookId, int page)
        {
            // Defensive Programming    
            if (string.IsNullOrWhiteSpace(bookId))
                throw new ArgumentNullException(nameof(bookId), "ID Buku tidak boleh kosong.");
            if (page < 0)
                throw new ArgumentException("Halaman bookmark tidak boleh bernilai negatif.");

            // Update atau Insert ke dalam tabel memori
            if (_bookmarkTable.ContainsKey(bookId))
            {
                _bookmarkTable[bookId] = page;
            }
            else
            {
                _bookmarkTable.Add(bookId, page);
            }
        }

        public int GetBookmark(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
                throw new ArgumentNullException(nameof(bookId));

            // Pengambilan data dari tabel
            return _bookmarkTable.ContainsKey(bookId) ? _bookmarkTable[bookId] : 0;
        }
    }
}