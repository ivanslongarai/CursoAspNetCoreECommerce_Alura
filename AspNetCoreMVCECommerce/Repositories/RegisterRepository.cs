using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;

namespace CodeHome.Repositories
{

    public interface IRegisterRepository
    {

    }

    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
