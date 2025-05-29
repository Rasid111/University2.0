using Microsoft.AspNetCore.Mvc;
using UniversityWebapi.Dtos.Major;
using UniversityWebapi.Services;

namespace UniversityWebapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class MajorController(MajorService majorService) : ControllerBase
    {
        readonly MajorService _majorService = majorService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var major = await _majorService.GetByIdAsync(id);
            if (major == null)
            {
                return NotFound("Major not found");
            }
            return Ok(major);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MajorCreateDto dto)
        {
            await _majorService.CreateMajorAsync(dto);
            return NoContent();
        }
    }
}