using UniversityWebapi.Dtos.Group;
using UniversityWebapi.Repositories;

namespace UniversityWebapi.Services
{
    public class GroupService(GroupRepository groupRepository)
    {
        readonly GroupRepository _groupRepository = groupRepository;

        public async Task CreateGroupAsync(GroupCreateDto dto)
        {
            var group = new Models.Group
            {
                Name = dto.Name,
                MajorId = dto.MajorId
            };

            await _groupRepository.CreateAsync(group);
        }
        public async Task<Models.Group?> GetByIdAsync(int groupId)
        {
            return await _groupRepository.GetByIdAsync(groupId);
        }
        public async Task<List<Models.Group>> GetGroupsByMajorIdAsync(int majorId)
        {
            return await _groupRepository.GetAllAsync();
        }
    }
}