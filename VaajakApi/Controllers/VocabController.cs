using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Application.Dto.Vocabs;
using Vaajak.Application.Services.Vocabs;
using Vaajak.Domain.Repositories.Vocabs;
using Vaajak.Persistence.Contexts;
using VaajakApi.Mappers;

namespace VaajakApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VocabController : ControllerBase
    {
        private readonly IVocabService _vocabService;

        public VocabController(IVocabService vocabService)
        {
            _vocabService = vocabService;
        }

        //private readonly DatabaseContext _context;
        //public VocabController(DatabaseContext context)
        //{
        //    _context = context;
        //}

        [HttpGet, Route("All")]
        public async Task<IActionResult> GetAll(Guid packageId, [FromQuery] PaginationRequestDTO pagination) {
            var vocabs = await _vocabService.GetAllAsync(packageId, pagination);
                //Vocabs.ToList().Select(s => s.ToVocabsDto());

            return Ok(vocabs);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var vocab = await _vocabService.GetById(id);

            if (vocab == null)
            {
                return NotFound();
            }

            return Ok(vocab);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateVocab([FromBody] CreateVocabDto createVocabDto)
        {
            var vocab = await _vocabService.CreateVocab(createVocabDto);
            if(vocab == null)
            {
                return BadRequest();
            }
            return Ok(vocab);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateVocab([FromBody] UpdateVocabDto updateVocabDto)
        {
            var vocab = await _vocabService.UpdateVocab(updateVocabDto);

            if (vocab == null)
            {
                return NotFound();
            }
            return Ok(vocab);
        }

        [HttpPost("bulk-import")]
        public async Task<IActionResult> BulkImport([FromBody] BulkImportVocabRequestDto request)
        {
            var result = await _vocabService.BulkImportAsync(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVocab([FromQuery] Guid id)
        {
            try
            {
                // Call the service to delete the vocab
                bool isDeleted = await _vocabService.DeleteById(id);

                // Check if the delete operation was successful
                if (!isDeleted)
                {
                    // Return a 404 Not Found response if the vocab was not found
                    return NotFound(new { Message = "Vocab not found." });
                }

                // Return a 204 No Content response indicating successful deletion
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework of your choice)
                Console.WriteLine($"An error occurred while deleting vocab: {ex.Message}");

                // Return a 500 Internal Server Error response
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }
    }
}
