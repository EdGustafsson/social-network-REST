using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace social_network_REST.Dtos.Posts
{
    public class PostDto
    {
        private const string _stringMessage = "Content must be between 5 and 400 characters long";

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Content { get; set; }


    }
}
