using UniversityWebapi.Dtos.Faculty;
using UniversityWebapi.Repositories;

namespace UniversityWebapi.Services
{
    public class FacultyService(FacultyRepository facultyRepository)
    {
        readonly FacultyRepository _facultyRepository = facultyRepository;

        public async Task CreateFacultyAsync(FacultyCreateDto dto)
        {
            var faculty = new Models.Faculty
            {
                Name = dto.Name
            };

            await _facultyRepository.CreateAsync(faculty);
        }

        public async Task<Models.Faculty?> GetByIdAsync(int facultyId)
        {
            return await _facultyRepository.GetByIdAsync(facultyId);
        }

        public async Task<List<Models.Faculty>> GetAllFacultiesAsync()
        {
            return await _facultyRepository.GetAllAsync();
        }
    }
}