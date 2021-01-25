using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Users;

namespace social_network_REST.Repositories.Users
{
    public interface IUserRepository
    {
        User GetUser(Guid id);
        IEnumerable<User> GetUsers();
        void Add(User user);
        bool UserNameIsUnique(User user);
    }
}
