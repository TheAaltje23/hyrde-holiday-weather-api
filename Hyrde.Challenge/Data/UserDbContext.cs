using Microsoft.EntityFrameworkCore;
using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}