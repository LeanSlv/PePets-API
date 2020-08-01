using Microsoft.EntityFrameworkCore;
using PePets_API.Data;
using PePets_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PePets_API.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PePetsDbContext _context;

        public PostRepository(PePetsDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post post)
        {
            await SaveStateOfPostAsync(post, EntityState.Added);
        }

        public async Task UpdateAsync(Post post)
        {
            await SaveStateOfPostAsync(post, EntityState.Modified);
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _context.Posts
                .SingleOrDefaultAsync(sod => sod.Id == id);
        }

        public async Task<Post> GetByNameAsync(string name)
        {
            return await _context.Posts
                .SingleOrDefaultAsync(sod => sod.Title == name);
        }
        private async Task SaveStateOfPostAsync(Post post, EntityState state)
        {
            _context.Entry(post).State = state;
            await _context.SaveChangesAsync();
        }
    }
}
