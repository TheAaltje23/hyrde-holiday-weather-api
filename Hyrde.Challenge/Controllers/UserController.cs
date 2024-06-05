using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Hyrde.Challenge.Dto;
using Hyrde.Challenge.Services;

namespace Hyrde.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        // READ
        [HttpGet("User/{id:long}")]
        public async Task<ResponseDto> GetUserById(long id)
        {
            _logger.LogInformation("Received request for user with id: {id}", id);

            User? user = await _service.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("User not found or error occurred for user with id: {id}", id);
                return new ResponseDto(false, null, ["User not found or error occurred while retrieving user data"], "Validation failed");
            }

            var userDto = new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username
            };

            _logger.LogInformation("User data retrieved successfully for user with id: {id}", id);
            return new ResponseDto(true, userDto, null, "User retrieved successfully");
        }

        [HttpGet("User/{username}")]
        public async Task<ResponseDto> GetUserByUsername(string username)
        {
            _logger.LogInformation("Received request for user with username: {username}", username);

            User? user = await _service.GetUserByUsername(username);

            if (user == null)
            {
                _logger.LogWarning("User not found or error occurred for user with username: {username}", username);
                return new ResponseDto(false, null, ["User not found or error occurred while retrieving user data"], "Validation failed");
            }

            var userDto = new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username
            };

            _logger.LogInformation("User data retrieved successfully for user with username: {username}", username);
            return new ResponseDto(true, userDto, null, "User retrieved successfully");
        }

        [HttpGet("User/All")]
        public async Task<ResponseDto> GetAllUsers()
        {
            _logger.LogInformation("Received request for all users");

            var users = await _service.GetAllUsers();

            if (users == null)
            {
                _logger.LogWarning("Users not found or error occurred");
                return new ResponseDto(false, null, ["Users not found or error occurred while retrieving user data"], "Validation failed");
            }

            var userDtos = users.Select(user => new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username,
            });

            _logger.LogInformation("Users retrieved successfully");
            return new ResponseDto(true, userDtos, null, userDtos.Count() + " users retrieved successfully");
        }

        [HttpPost]
        public async Task<ResponseDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            _logger.LogInformation("Received request to create a new user");

            if (createUserDto == null)
            {
                _logger.LogWarning("Invalid user creation request: user data is null");
                return new ResponseDto(false, null, ["Invalid user data"], "Validation failed");
            }

            var user = new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password
            };

            await _service.CreateUser(user);

            _logger.LogInformation("User created successfully");
            return new ResponseDto(true, user, null, "User created successfully");

        }
    }
}