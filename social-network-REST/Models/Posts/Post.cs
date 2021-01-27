using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using social_network_REST.Dtos.Posts;

namespace social_network_REST.Models.Posts
{
    public class Post
    {

        private const string _stringMessage = "Content must be between 5 and 400 characters long";
        public Post(PostDto postDto)
        {
            Id = Guid.NewGuid();
            UserId = postDto.UserId;
            Content = postDto.Content;
            LastUpdate = DateTime.Now;
        }

        public void Update()
        {
            Updated = true;
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        public Guid Id { get; private set; }

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

        /// <summary>
        /// A bool that checks if the Post has been updated
        /// </summary>
        /// <example>true/false</example>
        public bool Updated { get; private set; }

        /// <summary>
        /// A DateTime, tracking the last time the Post was updated
        /// </summary>
        /// <example>2021-11-11T12:00:00.0000000+01:00</example>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// A list of Guids, which are the Id's of Users
        /// </summary>
        /// <example>[bc3fd254-3eea-4da8-8f45-dbd69030c306, bc3fd254-3eea-4da8-8f45-dbd69030c306]</example>
        public List<Guid> Likes { get; set; } = new List<Guid>();


    }
}
