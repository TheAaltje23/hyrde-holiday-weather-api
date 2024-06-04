using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface IUserService
    {
        Task<User?> GetUserById(long id);
        Task<User?> GetUserByUsername(string username);
        Task<IEnumerable<User>?> GetAllUsers();
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(long id);
    }
}