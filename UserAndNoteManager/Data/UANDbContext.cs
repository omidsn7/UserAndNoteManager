using Microsoft.EntityFrameworkCore;
using UserAndNoteManager.Models;

namespace UserAndNoteManager.Data
{
    public class UANDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public UANDbContext(DbContextOptions<UANDbContext> options) : base(options)
        {
               
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            entity.HasData(new User
            {
                ID = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Age = 24,
                Email = "Admin@Admin.Com",
                Website = "www.Admin.com"
            }));
            }
    }
}
