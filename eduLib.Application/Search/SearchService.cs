using System;
using System.Collections.Generic;
using System.Linq;
using eduLib.Core.Entities;

namespace eduLib.Application.Search
{
    public class SearchService
    {
        // Teknik: Parameterization/Generics menggunakan Func sebagai kriteria fleksibel
        public List<Book> FindBooks(List<Book> books, Func<Book, bool> query)
        {
            // Defensive Programming: Design by Contract (Precondition)
            if (books == null) throw new ArgumentNullException(nameof(books));

            return books.Where(query).ToList();
        }
    }
}