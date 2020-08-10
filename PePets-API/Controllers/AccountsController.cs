using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PePets_API.Models;
using PePets_API.Repositories;
using PePets_API.ViewModels;

namespace PePets_API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromBody]LoginViewModel model)
        {
            ClaimsIdentity identity = await GetIdentity(model.UserName, model.Password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }
        private async Task<ClaimsIdentity> GetIdentity(string userName, string password)
        {
            User user = await _accountRepository.GetByNameAsync(userName);
            if(user != null)
            {
                if(user.PasswordHash == password)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, ClaimsIdentity.DefaultNameClaimType);
                    return claimsIdentity;
                }
            }

            return null;
        }
    }
}
