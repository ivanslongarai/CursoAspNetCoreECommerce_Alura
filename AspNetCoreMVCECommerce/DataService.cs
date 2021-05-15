using System.Collections.Generic;
using System.IO;
using CodeHome.Models;
using CodeHome.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CodeHome
{
    class DataService : IDataService
    {
        private readonly ApplicationContext Context;
        private readonly IProductRepository ProductRepository;

        public DataService(ApplicationContext context, IProductRepository productRepository)
        {
            this.Context = context;
            this.ProductRepository = productRepository;

        }

        public void DbInitialize()
        {
            Context.Database.EnsureCreated();
            List<Book> books = GetBooks();
            ProductRepository.SaveProducts(books);
        }

        private static List<Book> GetBooks()
        {
            var json = File.ReadAllText("books.json");
            var books = JsonConvert.DeserializeObject<List<Book>>(json);
            return books;
        }
    }

}
