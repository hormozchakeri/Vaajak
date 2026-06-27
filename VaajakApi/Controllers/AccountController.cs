using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaajak.Application.Dto.Account;
using Vaajak.Application.Dto.Primitives;
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

            try
            {
                var result = await _accountService.SigninAsync(signinDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet, Route("getAll")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationRequestDTO paginationRequestDTO)
        {
            try
            {
                var users = await _accountService.GetAllUsersAsync(paginationRequestDTO);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet, Route("profile")]
        public async Task<IActionResult> Profile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User id not exist on token");

                var result = await _accountService.GetProfileAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            try
            {
                var result = await _accountService.RefreshTokenAsync(dto.RefreshToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("admin-data")]
        //public IActionResult GetAdminData()
        //{
        //    return Ok("You are an admin!");
        //}

        //[Authorize(Policy = "AdminOnly")]
        //[HttpGet("admin-data")]
        //public IActionResult GetAdminDatas()
        //{
        //    return Ok("You are an admin!");
        //}

    }
}
