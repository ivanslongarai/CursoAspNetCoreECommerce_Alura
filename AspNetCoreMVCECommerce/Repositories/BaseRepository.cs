using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeHome.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext Context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(ApplicationContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }
    }
}
