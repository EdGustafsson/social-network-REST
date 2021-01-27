using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using social_network_REST.Repositories.Users;
using social_network_REST.Models.Users;
using social_network_REST.Dtos.Users;

namespace social_network_REST.Controllers
{

    /// <summary>
    /// Controller for working with the Users
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Fetches all Users.
        /// </summary>
        /// <response code="200">Returns all Users</response>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        /// <summary>
        /// Fetches a User based on the given id.
        /// </summary>
        /// <param name="id">This is an id of an existing User</param>
        /// <response code="200">Returns the User with the given Id</response>
        /// <response code="404">No User with the given Id found </response>
        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<User> GetUser(Guid id)
        {

            var user = _userRepository.GetUser(id);

            if (user is null)
                return NotFound(user);
            return user;

        }

        /// <summary>
        /// Creates a new user, using the userDto.
        /// </summary>
        /// <param name="userDto">This is a new User object</param>
        /// <response code="201">Successfully created a new User</response>
        /// <response code="400">Failed to create a new User</response>
        [HttpPost]
        public ActionResult<User> CreateUser(UserDto userDto)
        {
            try
            {
                var user = _userRepository.Add(userDto);
                return user;
            }
            catch (UserException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
