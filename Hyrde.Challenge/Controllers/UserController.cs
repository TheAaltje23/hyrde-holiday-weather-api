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

            _logger.LogInformation("User data retrieved successfully for user with id: {id}", id);
            return new ResponseDto(true, user, null, "User retrieved successfully");
        }

        [HttpGet("User/{username}")]
        public async Task<ResponseDto> GetUserByUsername(string username)
        {
            _logger.LogInformation("Hi");
            return new ResponseDto(true, null , null, "Hi");
        }

        [HttpGet("User/All")]
        public async Task<ResponseDto> GetAllUsers()
        {
            _logger.LogInformation("Hi");
            return new ResponseDto(true, null, null, "Hi");
        }
    }
}