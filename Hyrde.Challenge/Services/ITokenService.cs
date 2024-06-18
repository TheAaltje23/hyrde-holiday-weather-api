using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}