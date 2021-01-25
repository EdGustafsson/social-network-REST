using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Users;

namespace social_network_REST.Repositories.Users
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
        public DictionaryUserRepository()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Pelle",
                Email = "Pelle@gmail.com"

            };
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Anders",
                Email = "Anders@gmail.com"

            };
            _users.Add(user.Id, user);
            _users.Add(user1.Id, user1);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Select(e => e.Value);
        }

        public User GetUser(Guid id)
        {
            return _users[id];
        }
    }
}
