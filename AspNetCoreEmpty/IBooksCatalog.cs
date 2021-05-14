using System.Collections.Generic;

namespace CursoAspNetCoreECommerce
{
    public interface IBooksCatalog
    {
        List<Book> GetBooks();
    }
}