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
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<User> GetUser(Guid id)
        {

            var user = _userRepository.GetUser(id);

            if (user is null)
                return NotFound(user);
            return user;

        }

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
