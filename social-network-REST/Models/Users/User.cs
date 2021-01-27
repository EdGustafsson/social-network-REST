using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using social_network_REST.Dtos.Users;

namespace social_network_REST.Models.Users
{
    public class User
    {
        
        public User(UserDto userDto)
        {
            Id = Guid.NewGuid();
            UserName = userDto.UserName;
            Email = userDto.Email;


        }

        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        public Guid Id { get; private set; }

        /// <summary>
        /// A human readable name, using only alphanumeric characters
        /// </summary>
        /// <example>Anders123</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Username can only contain alphanumeric characters")]
        public string UserName { get; set; }

        /// <summary>
        /// An Email
        /// </summary>
        /// <example>Anders@gmail.com</example>
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        

        
    }
}
