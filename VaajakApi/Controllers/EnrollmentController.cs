using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaajak.Application.Dto.Enrollment;
using Vaajak.Application.Services.Enrollment;

namespace VaajakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollRequestDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User id not found in token." });

            try
            {
                var result = await _enrollmentService.EnrollAsync(userId, dto.PackageId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("my-packages")]
        public async Task<IActionResult> GetMyPackages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User id not found in token." });

            var result = await _enrollmentService.GetMyPackagesAsync(userId);
            return Ok(result);
        }

        [HttpGet("is-enrolled/{packageId:guid}")]
        public async Task<IActionResult> IsEnrolled(Guid packageId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User id not found in token." });

            var enrolled = await _enrollmentService.IsEnrolledAsync(userId, packageId);
            return Ok(new { isEnrolled = enrolled });
        }
    }
}
