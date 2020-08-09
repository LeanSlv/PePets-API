using Microsoft.EntityFrameworkCore;
using PePets_API.Models;

namespace PePets_API.Data
{
    public class PePetsDbContext : DbContext
    {
        public PePetsDbContext(DbContextOptions<PePetsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
    }
}
