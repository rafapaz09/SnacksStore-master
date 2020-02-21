using SnacksStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksStore.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllWithRol();

        User GetByIdWithRol(int id);

        User Authenticate(string username, string password);
    }
}
