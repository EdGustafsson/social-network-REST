using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Models.Posts;

namespace social_network_REST.Repositories.Posts
{
    public class DictionaryPostRepository : IPostRepository
    {
        private readonly Dictionary<Guid, Post> _posts = new Dictionary<Guid, Post>();

        public IEnumerable<Post> GetPosts()
        {
            return _posts.Select(e => e.Value);
        }

        public void Add(Post post)
        {
            _posts.Add(post.Id, post);
        }

        //public void Delete(Post post)
        //{
        //    _posts.Remove(post.Id);
        //}

        public Post GetPost(Guid id)
        {
            return _posts[id];
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
            throw new NotImplementedException();
        }
    }
}
