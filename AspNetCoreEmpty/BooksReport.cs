using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CursoAspNetCoreECommerce
{
    public class BooksReport : IBooksReport
    {
        private readonly IBooksCatalog booksCatalog;

        public BooksReport(IBooksCatalog booksCatalog)
        {
            this.booksCatalog = booksCatalog;
        }

        public async Task Print(HttpContext context)
        {
            foreach (var book in booksCatalog.GetBooks())
                await context.Response.WriteAsync($"{book.Id,-10}{book.Name,-40}{book.Price.ToString("C"),10}\r\n");

        }
    }
}
