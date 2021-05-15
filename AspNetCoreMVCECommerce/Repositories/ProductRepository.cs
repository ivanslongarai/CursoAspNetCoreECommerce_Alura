using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeHome.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Product> GetProducts()
        {
            return dbSet.ToList();
        }

        public void SaveProducts(List<Book> books)
        {
            foreach (var book in books)
            {                
                if (!dbSet.Where(p => p.InternalId == book.InternalId).Any())
                    dbSet.Add(new Product(book.InternalId, book.Name, book.Price));
            }
            Context.SaveChanges();
        }

    }
    public class Book
    {
        public string InternalId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}
