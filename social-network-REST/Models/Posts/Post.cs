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

        public Guid Id { get; private set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Content { get; set; }

        public bool Updated { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public List<Guid> Likes { get; set; } = new List<Guid>();


    }
}
