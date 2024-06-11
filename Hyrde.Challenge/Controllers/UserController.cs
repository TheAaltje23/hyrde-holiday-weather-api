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
        [HttpGet("{id:long}")]
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

        [HttpGet("{username}")]
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

        [HttpGet("all")]
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

        [HttpPost("create")]
        public async Task<ResponseDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            _logger.LogInformation("Received request to create a new user");

            // IS DATA COMPLETE?
            if (string.IsNullOrWhiteSpace(createUserDto.Username) || string.IsNullOrWhiteSpace(createUserDto.Password))
            {
                _logger.LogWarning("Invalid user creation request: user data is null");
                return new ResponseDto(false, null, ["Invalid user data"], "Validation failed");
            }

            // DOES USER EXIST?
            var existingUser = await _service.GetUserByUsername(createUserDto.Username);
            if (existingUser != null)
            {
                _logger.LogWarning("User with the provided username already exists");
                return new ResponseDto(false, existingUser.Username, ["User with the provided username already exists"], "Username is already taken");
            }

            // LENGTH VALIDATION
            if (createUserDto.Username.Length < 8)
            {
                _logger.LogWarning("Username does not meet length criteria");
                return new ResponseDto(false, null, ["Username does not meet length criteria"], "Username must be at least 8 characters long");
            }

            if (createUserDto.Password.Length < 8)
            {
                _logger.LogWarning("Password does not meet length criteria");
                return new ResponseDto(false, null, ["Password does not meet length criteria"], "Password must be at least 8 characters long");
            }

            // CREATE USER
            var newUser = new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password
            };

            await _service.CreateUser(newUser);
            _logger.LogInformation("User created successfully");
            return new ResponseDto(true, newUser, null, "User created successfully");
        }

        [HttpPut("update/{id}")]
        public async Task<ResponseDto> UpdateUser([FromBody] UpdateUserDto updateUserDto, long id)
        {
            _logger.LogInformation("Received request to update a user");

            // IS DATA COMPLETE?
            if (string.IsNullOrWhiteSpace(updateUserDto.Username) || string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                _logger.LogWarning("Invalid user update request: user data is null");
                return new ResponseDto(false, null, ["Invalid user data"], "Validation failed");
            }

            // DOES USER EXIST?
            var existingUser = await _service.GetUserById(id);
            if (existingUser == null)
            {
                return new ResponseDto(false, existingUser, ["User does not exist"], "User not found");
            }

            // LENGTH VALIDATION
            if (updateUserDto.Username.Length < 8)
            {
                return new ResponseDto(false, null, ["Username does not meet length criteria"], "Username must be at least 8 characters long");
            }

            if (updateUserDto.Password.Length < 8)
            {
                return new ResponseDto(false, null, ["Password does not meet length criteria"], "Password must be at least 8 characters long");
            }

            // UPDATE USER
            existingUser.Username = updateUserDto.Username;
            existingUser.Password = updateUserDto.Password;

            await _service.UpdateUser(existingUser);
            _logger.LogInformation("User updated successfully");
            return new ResponseDto(true, existingUser, null, "User updated successfully");
        }
    }
}