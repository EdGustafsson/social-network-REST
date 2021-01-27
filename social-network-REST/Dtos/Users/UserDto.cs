using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace social_network_REST.Dtos.Users
{
    public class UserDto
    {
        /// <summary>
        /// A human readable name, using only alphanumeric characters
        /// </summary>
        /// <example>Anders123</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Username can only contain alphanumeric character")]
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
