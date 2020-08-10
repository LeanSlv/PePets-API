using Microsoft.AspNetCore.Identity;
using PePets_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PePets_API.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        IEnumerable<User> GetAll();
        Task<User> GetByIdAsync(string userId);
        Task<User> GetByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
