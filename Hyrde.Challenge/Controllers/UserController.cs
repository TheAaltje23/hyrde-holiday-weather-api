using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Hyrde.Challenge.Dto;
using Hyrde.Challenge.Services;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

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

            var user = await _service.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("User not found or error occurred for user with id: {id}", id);
                return new ResponseDto(false, null, ["User not found or error occurred while retrieving user data"], "User not found");
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
                return new ResponseDto(false, null, ["User not found or error occurred while retrieving user data"], "User not found");
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
                return new ResponseDto(false, null, ["Users not found or error occurred while retrieving user data"], "Users not found");
            }

            var userDtos = users.Select(user => new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username,
            });

            _logger.LogInformation("Users retrieved successfully");
            return new ResponseDto(true, userDtos, null, userDtos.Count() + " users retrieved successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<ResponseDto> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Received login request");

            // IS DATA COMPLETE?
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _logger.LogWarning("Invalid login request: login data is null");
                return new ResponseDto(false, null, ["Please enter your username and password"], "Invalid login request");
            }

            var existingUser = await _service.GetUserByUsername(loginDto.Username);

            if (existingUser == null)
            {
                _logger.LogInformation("Invalid login attempt: {username} not found", loginDto.Username);
                return new ResponseDto(false, null, ["Invalid username or password"], "Login failed");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.Password);

            if (!isPasswordValid)
            {
                _logger.LogInformation("Invalid login attempt: incorrect password");
                return new ResponseDto(false, null, ["Invalid username or password"], "Login failed");
            }
            
            _logger.LogInformation("User logged in successfully");
            return new ResponseDto(true, existingUser, null, "Login successful");
        }


        // CREATE
        [HttpPost("create")]
        public async Task<ResponseDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            _logger.LogInformation("Received request to create a new user");

            // IS DATA COMPLETE?
            if (string.IsNullOrWhiteSpace(createUserDto.Username) || string.IsNullOrWhiteSpace(createUserDto.Password))
            {
                _logger.LogWarning("Invalid user creation request: user data is null");
                return new ResponseDto(false, null, ["Invalid user creation request: user data is null"], "Invalid user data");
            }

            var existingUser = await _service.GetUserByUsername(createUserDto.Username);

            // DOES USER EXIST?
            if (existingUser != null)
            {
                _logger.LogWarning("User with the provided username already exists: {username}", existingUser.Username);
                return new ResponseDto(false, existingUser.Username, ["User with the provided username already exists"], "Username is already taken");
            }

            // LENGTH VALIDATION
            if (createUserDto.Username.Length < 8)
            {
                _logger.LogWarning("Username must be at least 8 characters long");
                return new ResponseDto(false, null, ["Username must be at least 8 characters long"], "Username does not meet length criteria");
            }

            if (createUserDto.Password.Length < 8)
            {
                _logger.LogWarning("Password must be at least 8 characters long");
                return new ResponseDto(false, null, ["Password must be at least 8 characters long"], "Password does not meet length criteria");
            }

            // HASH PASSWORD
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            // CREATE USER
            var newUser = new User
            {
                Username = createUserDto.Username,
                Password = hashedPassword
            };

            await _service.CreateUser(newUser);
            _logger.LogInformation("User created successfully");
            return new ResponseDto(true, null, null, "User created successfully");
        }

        // UPDATE
        [HttpPut("update/{id}")]
        public async Task<ResponseDto> UpdateUser([FromBody] UpdateUserDto updateUserDto, long id)
        {
            _logger.LogInformation("Received request to update a user");

            // IS DATA COMPLETE?
            if (string.IsNullOrWhiteSpace(updateUserDto.Username) && string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                _logger.LogWarning("Invalid user update request: user data is null");
                return new ResponseDto(false, null, ["Invalid user update request: user data is null"], "Invalid user data");
            }

            var existingUser = await _service.GetUserById(id);

            // DOES USER EXIST?
            if (existingUser == null)
            {
                _logger.LogWarning("User does not exist");
                return new ResponseDto(false, existingUser, ["User does not exist"], "User not found");
            }

            // LENGTH VALIDATION
            if (!string.IsNullOrWhiteSpace(updateUserDto.Username) && updateUserDto.Username.Length < 8)
            {
                _logger.LogWarning("Username must be at least 8 characters long");
                return new ResponseDto(false, null, ["Username must be at least 8 characters long"], "Username does not meet length criteria");
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password) && updateUserDto.Password.Length < 8)
            {
                _logger.LogWarning("Password must be at least 8 characters long");
                return new ResponseDto(false, null, ["Password must be at least 8 characters long"], "Password does not meet length criteria");
            }

            // UPDATE USER
            if (!string.IsNullOrWhiteSpace(updateUserDto.Username))
            {
                existingUser.Username = updateUserDto.Username;
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
            }

            await _service.UpdateUser(existingUser);
            _logger.LogInformation("User updated successfully");
            return new ResponseDto(true, null, null, "User updated successfully");
        }

        // DELETE
        [HttpDelete("delete/{id}")]
        public async Task<ResponseDto> DeleteUser(long id)
        {
            _logger.LogInformation("Received request to delete a user");
            var existingUser = await _service.GetUserById(id);

            // DOES USER EXIST?
            if (existingUser == null)
            {
                _logger.LogWarning("User does not exist");
                return new ResponseDto(false, existingUser, ["User does not exist"], "User not found");
            }

            // DELETE USER
            await _service.DeleteUser(existingUser.Id);
            _logger.LogInformation("User deleted successfully");
            return new ResponseDto(true, existingUser, null, "User deleted successfully");
        }
    }
}