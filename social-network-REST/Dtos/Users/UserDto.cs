using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace social_network_REST.Dtos.Users
{
    public class UserDto
    {

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Username can only contain alphanumeric character")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        
    }
}
