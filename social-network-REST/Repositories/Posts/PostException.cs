using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_REST.Repositories.Posts
{
    public class PostException : Exception
    {
        public PostException(string message) : base(message)
        {
        }
    }

    public class NonUniqueUserName : PostException
    {
        public NonUniqueUserName(string message = "The UserName was not unique") : base(message)
        {
        }
    }

    public class NonUniqueId : PostException
    {
        public NonUniqueId(string message = "The Id was not unique") : base(message)
        {
        }
    }
}
