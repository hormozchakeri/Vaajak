using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaajak.Application.Dto.Packages;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Application.Services.Packages;

namespace VaajakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestDTO paginationRequestDTO)
        {
            var packages = await _packageService.GetAllAsync(paginationRequestDTO);
            return Ok(packages);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var package = await _packageService.GetPackageById(id);

            if (package == null)
                return NotFound();

            return Ok(package);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePackage([FromBody] CreatePackageDto createPackageDto)
        {
            try
            {
                var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(ownerId))
                    return Unauthorized();

                var package = await _packageService.CreatePackage(createPackageDto, ownerId);
                return Ok(package);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("my-packages")]
        public async Task<IActionResult> GetMyPackages()
        {
            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(ownerId))
                return Unauthorized();

            var packages = await _packageService.GetMyPackagesAsync(ownerId);
            return Ok(packages);
        }
    }
}
