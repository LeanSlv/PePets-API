using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PePets_API.Models;

namespace PePets_API.Data
{
    public class PePetsDbContext : IdentityDbContext<User>
    {
        public PePetsDbContext(DbContextOptions<PePetsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().
                Property(t => t._images).HasColumnName("Images");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(a => a.User).HasForeignKey(a => a.UserId);
        }

        public DbSet<Post> Posts { get; set; }
    }
}
