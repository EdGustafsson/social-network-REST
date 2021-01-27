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

        /// <summary>
        /// A unique Guid id of a User Object
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// A string of between 5 and 400 letters.
        /// </summary>
        /// <example>Hello, GME to the moon!</example>
        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Content { get; set; }


    }
}
