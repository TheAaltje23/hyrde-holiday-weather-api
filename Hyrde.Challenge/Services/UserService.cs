using Hyrde.Challenge.Models;
using Hyrde.Challenge.Data;
using Microsoft.EntityFrameworkCore;

namespace Hyrde.Challenge.Services
{
    public class UserService : IUserService
    {
 
        private readonly ILogger<UserService> _logger;
        private readonly UserDbContext _context;

        public UserService(ILogger<UserService> logger, UserDbContext context)
        {
            _logger = logger;
            _context= context;
        }

        // READ
        public async Task<User?> GetUserById(long id)
        {
            _logger.LogInformation("Fetching user by id: {id}", id);
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            _logger.LogInformation("Fetching user by username: {username}", username);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>?> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");
            return await _context.Users.ToListAsync();
        }

        // CREATE
        public async Task CreateUser(User user)
        {
            _logger.LogInformation("Creating user");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateUser(User user)
        {
            _logger.LogInformation("Updating user");
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteUser(long id) 
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _logger.LogInformation("Deleting user with id: {id}", id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            } else
            {
                _logger.LogInformation("User with id: {id}", id + " doesn't exist");
            }
        }
    }
}