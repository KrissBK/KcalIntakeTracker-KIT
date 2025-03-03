using AutoMapper;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;
using Microsoft.AspNetCore.Mvc;
using KcalIntakeTracker_KIT.Dto;
using Microsoft.Identity.Client;

namespace KcalIntakeTracker_KIT.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;


        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        public IActionResult GetUser(int userId)
        {
            var user = _mapper
                .Map<UserDto>(_userRepository
                .GetUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            return Ok(user);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]

        public IActionResult CreateUser([FromQuery] int userID, [FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUsers()
                .Where(u => u.UserId == userCreate.UserId)
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("UserId", "User ID already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the user with the ID {userCreate.UserId}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created user");

        }

        [HttpPatch("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public IActionResult UpdateUser(int userId, [FromBody] UserDto userUpdate)
        {
            if (userUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound($"User item with the ID {userId} not found.");
            }

            var user = _mapper.Map<User>(userUpdate);
            user.UserId = userId;

            if (userUpdate == null)
                {
                ModelState.AddModelError("", $"Something went wrong updating the user with the ID {userId}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated user");
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound($"User with the ID {userId} not found.");
            }

            var user = _mapper.Map<User>(_userRepository.GetUser(userId));

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the user with the ID {userId}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted user");
        }


    }
}
