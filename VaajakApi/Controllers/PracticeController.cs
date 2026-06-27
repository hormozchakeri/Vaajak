using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaajak.Application.Dto.Practice;
using Vaajak.Application.Services.Practice;

namespace VaajakApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PracticeController : ControllerBase
{
    private readonly IPracticeService _practiceService;

    public PracticeController(IPracticeService practiceService)
    {
        _practiceService = practiceService;
    }

    [HttpGet("daily-session")]
    public async Task<IActionResult> GetDailySession([FromQuery] int cardsPerPackage = 15)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var clampedLimit = Math.Clamp(cardsPerPackage, 5, 50);
        var cards = await _practiceService.GetDailySessionAsync(userId, clampedLimit);
        return Ok(cards);
    }

    [HttpGet("session/{packageId:guid}")]
    public async Task<IActionResult> GetSession(Guid packageId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var cards = await _practiceService.GetSessionAsync(userId, packageId);
        return Ok(cards);
    }

    [HttpPost("rate")]
    public async Task<IActionResult> Rate([FromBody] RateCardDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _practiceService.RateCardAsync(userId, dto);
        return NoContent();
    }

    [HttpGet("progress/{packageId:guid}")]
    public async Task<IActionResult> GetProgress(Guid packageId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var progress = await _practiceService.GetProgressAsync(userId, packageId);
        return Ok(progress);
    }
}
