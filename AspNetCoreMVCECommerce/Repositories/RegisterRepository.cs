using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;

namespace CodeHome.Repositories
{

    public interface IRegisterRepository
    {
        Register Update(int registerId, Register newRegister);
    }

    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationContext context) : base(context)
        {
        }

        public Register Update(int registerId, Register newRegister)
        {
            var registerBD = dbSet.Where(r => r.Id == registerId).SingleOrDefault();
            if (registerBD == null)
                throw new ArgumentException("newRegister");
            registerBD.Update(newRegister);
            Context.SaveChanges();
            return registerBD;
        }
    }
}
