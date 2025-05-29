using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityWebapi.Dtos.StudentProfile;
using UniversityWebapi.Models;
using UniversityWebapi.Services;

namespace UniversityWebapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class StudentProfileController(UserManager<User> userManager, StudentProfileService studentProfileService) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly StudentProfileService _studentProfileService = studentProfileService;

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var studentProfile = _studentProfileService.GetById(id);
            if (studentProfile == null)
            {
                return NotFound("Student profile not found");
            }
            return Ok(studentProfile);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentProfileCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var createdStudentProfile = await _studentProfileService.CreateAsync(dto);
            
            user.StudentProfileId = createdStudentProfile.Id;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentProfile = _studentProfileService.GetById(id);
            if (studentProfile == null)
            {
                return NotFound("Student profile not found");
            }
;
            _studentProfileService.Remove(studentProfile);
            return NoContent();
        }
    }
}