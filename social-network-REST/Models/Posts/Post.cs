using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_REST.Models.Posts
{
    public class Post
    {
        public Guid Id { get; }
        public Guid UserId { get; set;  }
        public string Content { get; set; }
        public bool Updated { get; private set; }
        public DateTime LastUpdate { get; set; }
        public List<Guid> Likes { get; set; }


        public Post()
        {
            Id = Guid.NewGuid();
            LastUpdate = DateTime.Now;

        }

        public void Update()
        {
            Updated = true;
        }
    }
}
