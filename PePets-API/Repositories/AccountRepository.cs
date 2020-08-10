using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PePets_API.Data;
using PePets_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PePets_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly PePetsDbContext _context;

        public AccountRepository(UserManager<User> userManager, PePetsDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(i => i.Posts).Include(i => i.AlreadyRatedUsers);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _context.Users.Include(i => i.Posts)
                .Include(i => i.AlreadyRatedUsers).SingleOrDefaultAsync(sod => sod.Id == userId);
        }

        public async Task<User> GetByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
