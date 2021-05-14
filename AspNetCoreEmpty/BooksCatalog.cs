using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoAspNetCoreECommerce
{
    public class BooksCatalog : IBooksCatalog
    {
        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            books.Add(new Book("001", "Learning JS Programming", 12.99m));
            books.Add(new Book("002", "Learning Java Programming", 30.99m));
            books.Add(new Book("003", "Learning C# Programming", 25.99m));
            return books;
        }
    }
}
