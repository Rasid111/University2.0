using UniversityWebapi.Dtos.Major;
using UniversityWebapi.Repositories;

namespace UniversityWebapi.Services
{
    public class MajorService(MajorRepository majorRepository)
    {
        readonly MajorRepository _majorRepository = majorRepository;

        public async Task CreateMajorAsync(MajorCreateDto dto)
        {
            var major = new Models.Major
            {
                Name = dto.Name,
                FacultyId = dto.FacultyId
            };

            await _majorRepository.CreateAsync(major);
        }
        public async Task<Models.Major?> GetByIdAsync(int majorId)
        {
            return await _majorRepository.GetByIdAsync(majorId);
        }
        public async Task<List<Models.Major>> GetAllMajorsAsync()
        {
            return await _majorRepository.GetAllAsync();
        }
    }
}