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

    /// <summary>
    /// Controller for working with the Posts
    /// </summary>
    [ApiController]
    [Route("api/posts")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostsController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Fetches all posts.
        /// </summary>
        /// <response code="200">Returns all Posts</response>
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            
                return _postRepository.GetPosts();

        }

        /// <summary>
        /// Fetches a Post based on the given id.
        /// </summary>
        /// <param name="id">This is an id of an existing Post</param>
        /// <response code="200">Returns the Post with the given Id</response>
        /// <response code="404">No Post with the given Id found </response>
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

        /// <summary>
        /// Creates a new post. Using the postDto.
        /// </summary>
        /// <param name="postDto">A new postDto object</param>
        /// <response code="201">Successfully created a new Post</response>
        /// <response code="404">No User with the given Id found </response>
        [HttpPost]
        public ActionResult<Post> CreatePost(PostDto postDto)
        {
            try
            {
                var user = _userRepository.GetUser(postDto.UserId);

                if (user is null)
                    return BadRequest("Invalid User Id"); ;

                var post = _postRepository.Add(postDto);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an existing post based on id. 
        /// </summary>
        /// <param name="postId">This is an id of an existing Post</param>
        /// <param name="userId">This is an id of an existing User</param>
        /// <response code="200">Successfully deleted an existing Post</response>
        /// <response code="400">Invalid Post Id or User Id</response>
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

        /// <summary>
        /// Updates properties on an existing post.
        /// </summary>
        /// <param name="postId">This is an id of an existing Post</param>
        /// <param name="userId">This is an id of an existing User</param>
        /// <param name="patches">This is the changes to the Post</param>
        /// <response code="200">Successfully updated the Post</response>
        /// <response code="400">Invalid Post Id or User Id</response>
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

        /// <summary>
        /// Adds the selected userId to the Likes list in the selected Post
        /// </summary>
        /// <param name="postId">This is an id of an existing Post</param>
        /// <param name="userId">This is an id of an existing User</param>
        /// <response code="200">Successfully added the UserId to the Post Likes list</response>
        /// <response code="400">Invalid Post Id or User Id</response>
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

        /// <summary>
        /// Removes the selected userId from the Likes list in the selected Post
        /// </summary>
        /// <param name="postId">This is an id of an existing Post</param>
        /// <param name="userId">This is an id of an existing User</param>
        /// <response code="200">Successfully removed the UserId from the Post Likes list</response>
        /// <response code="400">Invalid Post Id or User Id</response>
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
