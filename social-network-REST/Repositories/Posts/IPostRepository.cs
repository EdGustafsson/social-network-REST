using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Posts;
using social_network_REST.Dtos.Posts;

namespace social_network_REST.Repositories.Posts
{
    public interface IPostRepository
    {

        Post GetPost(Guid id);
        IEnumerable<Post> GetPosts();
        void Delete(Post post);
        Post Add(PostDto postDto);
        void ApplyPatch(Post post, Dictionary<string, object> patches);
        void LikePost(Post post, Guid userId);
        void UnlikePost(Post post, Guid userId);
    }
}
