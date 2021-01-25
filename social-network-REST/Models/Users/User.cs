using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_REST.Models.Users
{
    public class User
    {

        public Guid Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public User()
        {
            Id = Guid.NewGuid();

        }

        
    }
}
