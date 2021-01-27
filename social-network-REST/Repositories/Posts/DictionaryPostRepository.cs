using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Posts;
using social_network_REST.Dtos.Posts;
using social_network_REST.Repositories.Users;

namespace social_network_REST.Repositories.Posts
{
    public class DictionaryPostRepository : IPostRepository
    {
        private readonly Dictionary<Guid, Post> _posts = new Dictionary<Guid, Post>();

        public IEnumerable<Post> GetPosts()
        {
            return _posts.Select(e => e.Value);
        }

        public Post Add(PostDto postDto)
        {

            var post = new Post(postDto);

            _posts.Add(post.Id, post);
            return post;
        }

        public void Delete(Post post)
        {
            _posts.Remove(post.Id);
        }

        public Post GetPost(Guid id)
        {
            
            _posts.TryGetValue(id, out Post result);
            return result;
        }

        public void ApplyPatch(Post post, Dictionary<string, object> patches)
        {
            ApplyPatch<Post>(post, patches);
        }

        private void ApplyPatch<P>(P original, Dictionary<string, object> patches)
        {
            var properties = original.GetType().GetProperties();
            foreach (var patch in patches)
            {
                foreach (var prop in properties)
                {
                    if (string.Equals(patch.Key, prop.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        prop.SetValue(original, patch.Value);
                    }
                }
            }
        }

        public void LikePost(Post post, Guid userId)
        {
            post.Likes.Add(userId);
        }

        public void UnlikePost(Post post, Guid userId)
        {
            post.Likes.Remove(userId);
        }
    }
}
