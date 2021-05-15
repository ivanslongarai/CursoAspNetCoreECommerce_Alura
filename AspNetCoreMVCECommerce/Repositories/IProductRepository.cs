using System.Collections.Generic;
using CodeHome.Models;

namespace CodeHome.Repositories
{
    public interface IProductRepository
    {
        void SaveProducts(List<Book> books);
        IList<Product> GetProducts();
    }
}