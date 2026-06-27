using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vaajak.Domain.Common.Auth;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Account;
using Vaajak.Persistence.Contexts;

namespace Vaajak.Persistence.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountRepository(DatabaseContext dbContext, UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = dbContext;
            _userManager = userManager;
            //_signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IEnumerable<User?>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> SignupAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<User?> SigninAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            //if (user == null || !await _userManager.CheckPasswordAsync(user, password, isPersistent: false, lockoutOnFailure: false))
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            return isPasswordValid ? user : null;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            return await _jwtTokenGenerator.GenerateJwtTokenAsync(user);
        }

    }
}