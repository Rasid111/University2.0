using UniversityWebapi.Database;
using UniversityWebapi.Models;

namespace UniversityWebapi.Repositories
{
    public class StudentProfileRepository(UniversityDbContext dbContext)
    {
        readonly UniversityDbContext _dbContext = dbContext;

        public StudentProfile? GetById(int id)
        {
            return _dbContext.StudentProfiles.FirstOrDefault(sp => sp.Id == id);
        }

        public async Task CreateAsync(StudentProfile studentProfile)
        {
            await _dbContext.StudentProfiles.AddAsync(studentProfile);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(StudentProfile studentProfile)
        {
            _dbContext.StudentProfiles.Update(studentProfile);
            _dbContext.SaveChanges();
        }

        public void Remove(StudentProfile studentProfile)
        {
            _dbContext.StudentProfiles.Remove(studentProfile);
            _dbContext.SaveChanges();
        }
    }
}