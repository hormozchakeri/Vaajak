using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaajak.Application.Dto.Arena;
using Vaajak.Application.Services.Arena;

namespace VaajakApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ArenaController : ControllerBase
{
    private readonly IArenaService _arenaService;

    public ArenaController(IArenaService arenaService)
    {
        _arenaService = arenaService;
    }

    [HttpGet("questions/{packageId:guid}")]
    public async Task<IActionResult> GetQuestions(Guid packageId, [FromQuery] int count = 25)
    {
        try
        {
            var questions = await _arenaService.GetQuestionsAsync(packageId, count);
            return Ok(questions);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromBody] ExamSubmitDto dto)
    {
        var result = await _arenaService.SubmitExamAsync(dto);
        return Ok(result);
    }
}
