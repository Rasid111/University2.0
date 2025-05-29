using UniversityWebapi.Dtos.StudentProfile;
using UniversityWebapi.Models;
using UniversityWebapi.Repositories;

namespace UniversityWebapi.Services
{
    public class StudentProfileService(StudentProfileRepository studentProfileRepository)
    {
        private readonly StudentProfileRepository _studentProfileRepository = studentProfileRepository;

        public StudentProfile? GetById(int id)
        {
            return _studentProfileRepository.GetById(id);
        }

        public async Task<StudentProfile> CreateAsync(StudentProfileCreateDto dto)
        {
            var studentProfile = new StudentProfile
            {
                UserId = dto.UserId,
                GroupId = dto.GroupId,
            };

            await _studentProfileRepository.CreateAsync(studentProfile);
            return studentProfile;
        }

        public void Update(StudentProfileUpdateDto dto)
        {
            var studentProfile = GetById(dto.Id) ?? throw new KeyNotFoundException($"StudentProfile with ID {dto.Id} not found.");

            studentProfile.GroupId = dto.GroupId;
            _studentProfileRepository.Update(studentProfile);
        }

        public void Remove(StudentProfile studentProfile)
        {
            _studentProfileRepository.Remove(studentProfile);
        }
    }
}