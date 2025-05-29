using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Database;
using UniversityWebapi.Models;

namespace UniversityWebapi.Repositories
{
    public class FacultyRepository(UniversityDbContext dbContext)
    {
        private readonly UniversityDbContext _dbContext = dbContext;

        public async Task CreateAsync(Faculty faculty)
        {
            await _dbContext.Faculties.AddAsync(faculty);
            await _dbContext.SaveChangesAsync();
        }
    
        public async Task<Faculty?> GetByIdAsync(int id)
        {
            return await _dbContext.Faculties
                .Include(f => f.Majors)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Faculty>> GetAllAsync()
        {
            return await _dbContext.Faculties
                .Include(f => f.Majors)
                .ToListAsync();
        }
    }
}