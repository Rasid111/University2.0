using Microsoft.AspNetCore.Mvc;
using UniversityWebapi.Dtos.Faculty;
using UniversityWebapi.Services;

namespace UniversityWebapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class FacultyController(FacultyService facultyService) : ControllerBase
    {
        readonly FacultyService _facultyService = facultyService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var faculty = await _facultyService.GetByIdAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found");
            }
            return Ok(faculty);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FacultyCreateDto dto)
        {
            await _facultyService.CreateFacultyAsync(dto);
            return NoContent();
        }
    }
}