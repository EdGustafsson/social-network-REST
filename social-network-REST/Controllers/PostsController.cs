using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using social_network_REST.Repositories.Posts;
using social_network_REST.Models.Posts;
using social_network_REST.Dtos.Posts;
using social_network_REST.Repositories.Users;

namespace social_network_REST.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostsController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            
                return _postRepository.GetPosts();

        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<Post> GetPost(Guid id)
         {
            try
            {
                return _postRepository.GetPost(id);
            }
            catch (ArgumentException)
            {
                return NotFound(id);
            }
        }

        [HttpPost]
        public ActionResult<Post> CreatePost(PostDto postDto)
        {
            try
            {
                var user = _userRepository.GetUser(postDto.UserId);

                if (user is null)
                    return NotFound(user);

                var post = _postRepository.Add(postDto);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{postid:guid}/{userid:guid}")]
        public ActionResult DeletePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetPost(postId);

            if (post is null)
                return BadRequest($"Invalid Post Id");

            if (userId == post.UserId)
            {
                _postRepository.Delete(post);
                return Ok();
            }
            else
            {
                return BadRequest("Invalid User Id");
            }
                

            
        }

        [HttpPatch]
        [Route("edit/{postid:guid}/{userid:guid}")]
        public ActionResult UpdatePost(Guid postId, Guid userId, Dictionary<string, object> patches)
        {
            var post = _postRepository.GetPost(postId);

            if (post is null)
                return BadRequest($"Invalid Post Id");

            if(userId == post.UserId)
            {
                post.Update();
                _postRepository.ApplyPatch(post, patches);
                return Ok();
            }
            else
            {
                return BadRequest("Invalid User Id");
            }

        }

        [HttpPatch]
        [Route("like/{postid:guid}/{userid:guid}")]
        public ActionResult LikePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetPost(postId);
            var user = _userRepository.GetUser(userId);

            if (post is null)
            {
                return BadRequest($"Invalid Post Id");
            }
            else if(user is null)
            {
                return BadRequest($"Invalid User Id");
            }
                   

            
            if (post.Likes.Contains(userId))
            {
                return BadRequest("User already liked this post");
            }
            else
            {
                _postRepository.LikePost(post, userId);
                return Ok();
            }

        }

        [HttpPatch]
        [Route("unlike/{postid:guid}/{userid:guid}")]
        public ActionResult UnlikePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetPost(postId);
            var user = _userRepository.GetUser(userId);

            if (post is null)
            {
                return BadRequest($"Invalid Post Id");
            }
            else if (user is null)
            {
                return BadRequest($"Invalid User Id");
            }


            if (post.Likes.Contains(userId))
            {
                _postRepository.UnlikePost(post, userId);
                return Ok();
            }
            else
            {
                return BadRequest("User has not liked this post");
            }
        }
    }
}
