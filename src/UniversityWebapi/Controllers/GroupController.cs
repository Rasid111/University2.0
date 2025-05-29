using Microsoft.AspNetCore.Mvc;
using UniversityWebapi.Dtos.Group;
using UniversityWebapi.Services;

namespace UniversityWebapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class GroupController(GroupService groupService) : ControllerBase
    {
        readonly GroupService _groupService = groupService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound("Group not found");
            }
            return Ok(group);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateDto dto)
        {
            await _groupService.CreateGroupAsync(dto);
            return NoContent();
        }
    }
}