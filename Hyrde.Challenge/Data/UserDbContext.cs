using Microsoft.EntityFrameworkCore;
using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(50)
                      .UseCollation("utf8mb4_bin"); // Case-sensitive
            });
        }
    }
}