using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Users;
using social_network_REST.Dtos.Users;

namespace social_network_REST.Repositories.Users
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
        public DictionaryUserRepository()
        {
            var userDto = new UserDto
            {
                UserName = "Pelle",
                Email = "Pelle@gmail.com"

            };
            var userDto1 = new UserDto
            {
                UserName = "Anders",
                Email = "Anders@gmail.com"

            };
            var user = new User(userDto);
            var user1 = new User(userDto1);

            _users.Add(user.Id, user);
            _users.Add(user1.Id, user1);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Select(e => e.Value);
        }

        public User GetUser(Guid id)
        {
            _users.TryGetValue(id, out User result);
            return result;
        }

        public User Add(UserDto userDto)
        {
            

            if (UserNameIsNotUnique(userDto))
            {
                throw new NonUniqueUserName();
            }

            var user = new User(userDto);

            _users.Add(user.Id, user);

            return user;

        }

        public bool UserNameIsNotUnique(UserDto userDto)
        {
            return _users.Any(e => e.Value.UserName == userDto.UserName);
        }

       
    }
}
