using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Repositories.Posts;
using social_network_REST.Models.Posts;

namespace social_network_REST.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        [HttpPost]
        public ActionResult<Post> CreatePost(Post post)
        {
                _postRepository.Add(post);
                return post;
        }

        //[HttpDelete]
        //[Route("{id:guid}")]
        //public ActionResult DeletePost(Guid id)
        //{
        //    var post = _postRepository.GetPost(id);
        //    _postRepository.Delete(post);

        //    return NoContent();
        //}

        [HttpPatch]
        [Route("{id:guid}")]
        public ActionResult UpdatePost(Guid id, Dictionary<string, object> patches)
        {
            var post = _postRepository.GetPost(id);
            
            _postRepository.ApplyPatch(post, patches);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:guid}/like")]
        public ActionResult LikePost(Guid id,[FromBody]Guid userId)
        {
            var post = _postRepository.GetPost(id);

            _postRepository.LikePost(post, userId);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:guid}/unlike")]
        public ActionResult UnlikePost(Guid id,[FromBody]Guid userId)
        {
            return NoContent();
        }
    }
}
