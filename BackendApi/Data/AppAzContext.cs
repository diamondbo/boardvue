using Microsoft.EntityFrameworkCore;

namespace api.data
{
    public class AppAzContext : DbContext
    {
        public AppAzContext(DbContextOptions<AppAzContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.newId);
        }

    }

}