using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vaajak.Application.Dto.Account;
using Vaajak.Application.Services.Account;
using Vaajak.Domain.Entities;

namespace VaajakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, IAccountService accountService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Signin(SigninDto signinDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(signinDto.Username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userManager.
        }

    }
}
